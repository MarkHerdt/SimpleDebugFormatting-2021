using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SimpleDebugFormatting.ReferenceRemover
{
    /// <summary>
    /// Class to remove all script references to this Plugin from the Unity project
    /// </summary>
    //[CreateAssetMenu(menuName = "Simple Debug Formatting/Reference Remover", fileName = "ReferenceRemover_SDF_SOB")] // Commented out so it won't show up in the creation Menu
    internal class ReferenceRemover : ScriptableObject
    {
        #region Internals
            /// <summary>
            /// Is the reference search currently running?
            /// </summary>
            internal static bool isSearching;
            /// <summary>
            /// Number of C#-Files in this Unity Project
            /// </summary>
            internal static int filesInProject;
            /// <summary>
            /// Number of C#-Files that have been read through
            /// </summary>
            internal static int readFiles;
            /// <summary>
            /// Number of C#-Files that have been read through
            /// </summary>
            internal static int filesWithReferences;
            /// <summary>
            /// C#-Files with references to this Plugin that have been read through
            /// </summary>
            internal static int readFilesWithReferences;
            /// <summary>
            /// Count of all references to this Plugin
            /// </summary>
            internal static int totalReferences;
            /// <summary>
            /// Count of how many lines with references in them have been checked already
            /// </summary>
            internal static int linesChecked;
        #endregion
        
        #region Privates
            /// <summary>
            /// TokenSource for all Tasks in the "SearchPluginReferences()"-Method
            /// </summary>
            private CancellationTokenSource referenceSource;
            /// <summary>
            /// Token of the "referenceSource"
            /// </summary>
            private CancellationToken referenceToken;
            /// <summary>
            /// Regex pattern for "using"-directives
            /// </summary>
            private const string USING_DIRECTIVE = @"\busing\b\s+(\bstatic\b\s+|\w+\s*[=]\s*)?\bSimpleDebugFormatting\b\s*[;]?";
            /// <summary>
            /// Regex pattern for Method calls
            /// </summary>
            private const string METHOD_CALL = @"\b(Log|Warning|Error|Exception)_SDF\b\s*[(]?.*[)]?\s*[;]*";
            /// <summary>
            /// Regex pattern to get the count of "using"-directives
            /// </summary>
            private const string USING_COUNT = @"(?=\busing\b\s+(\bstatic\b\s+|\w+\s*[=]\s*)?\bSimpleDebugFormatting\b\s*[;]?)";
            /// <summary>
            /// Regex pattern to get the count of "Log_SDF"-Methods
            /// </summary>
            private const string LOG_COUNT = @"(?=\bLog_SDF\b\s*[(]?.*[)]?\s*[;]*)";
            /// <summary>
            /// Regex pattern to get the count of "Warning_SDF"-Methods
            /// </summary>
            private const string WARNING_COUNT = @"(?=\bWarning_SDF\b\s*[(]?.*[)]?\s*[;]*)";
            /// <summary>
            /// Regex pattern to get the count of "Error_SDF"-Methods
            /// </summary>
            private const string ERROR_COUNT = @"(?=\bError_SDF\b\s*[(]?.*[)]?\s*[;]*)";
            /// <summary>
            /// Regex pattern to get the count of "Exception_SDF"-Methods
            /// </summary>
            private const string EXCEPTION_COUNT = @"(?=\bException_SDF\b\s*[(]?.*[)]?\s*[;]*)";
        #endregion
        
        /// <summary>
        /// Is called when the Script reloads
        /// </summary>
        [DidReloadScripts]
        private static void OnReloadScript()
        {
            ResetValues();
        }
        
        /// <summary>
        /// Cancels all running Tasks
        /// </summary>
        internal void Cancel()
        {
            if (!isSearching) return;
            
                if (referenceSource != null) referenceSource.Cancel();
                isSearching = false;
        }
        
        /// <summary>
        /// Resets the values shown in the Inspector
        /// </summary>
        private static void ResetValues()
        { 
            filesInProject = 0; 
            readFiles = 0;
            filesWithReferences = 0;
            readFilesWithReferences = 0;
            totalReferences = 0;
            linesChecked = 0;
        }

        /// <summary>
        /// Searches the entire Unity Project for references from this Plugin
        /// </summary>
        internal async void SearchPluginReferences()
        {
            if (isSearching) return;

                isSearching = true;
                ResetValues();
                    
                referenceSource = new CancellationTokenSource();
                referenceToken = referenceSource.Token;
                
                // TODO: Implement Exclude folder(single folder/ all subfolder)/file
                // TODO: Implement exclude PluginFolder (with optional path in inspector)
                    
                // Singletons of "Settings" and "FormatPresets" can't be loaded on a different thread, so they need to be loaded here
                Settings.LoadScriptableObjectInEditor();
                FormatPresets.LoadScriptableObjectInEditor();
                    
                var _unityPath = Path.GetFullPath(AssetDatabase.GetAssetPath(this)).Replace(Application.dataPath.Replace('/', '\\'), "Assets");
                var _pluginPath = _unityPath.Remove(_unityPath.LastIndexOf("\\Simple Debug Formatting\\", StringComparison.Ordinal) + "\\Simple Debug Formatting\\".Length);
                
                // Get all C#-Scripts in this project
                var _scriptPaths = Directory.EnumerateFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories).ToArray();
                //var _scriptPaths = new[] { @"Assets\Scripts\Development\Test\Test.cs" };
                filesInProject = _scriptPaths.Length;
                
                var _scripts = new List<Script>();

                var _getReferences = Task.Run(() =>
                {
                    // Searches for Scripts that contain at least one reference to this Plugin
                    foreach (var _path in _scriptPaths)
                    {
                        if (referenceToken.IsCancellationRequested)
                            break;
                        
                        // Won't check Scripts from this Plugin
                        if (!_path.Contains(_pluginPath))
                        {
                            var _scriptContent = File.ReadLines(_path).ToArray();

                            if (_scriptContent.Any(HasReferences))
                            {
                                _scripts.Add(new Script(_path, _scriptContent));
                                filesWithReferences++;
                            }  
                        }
                        
                        readFiles++;
                    }

                    // Find all lines that contain a reference to this Plugin, in the Script
                    foreach (var _script in _scripts.TakeWhile(_Script => !referenceToken.IsCancellationRequested))
                    {
                        // Check each Line
                        for (var i = 0; i < _script.content.Length; i++)
                        {
                            if (referenceToken.IsCancellationRequested)
                                break;

                            if (!GetReferences(_script.content[i], i + 1, out var _line, out var _referenceCount)) continue;
                            
                                _script.lines.Add(_line);
                                totalReferences += _referenceCount;
                        }

                        readFilesWithReferences++;
                    }

                    // Every Script with a reference
                    foreach (var _script in _scripts.TakeWhile(_Script => !referenceToken.IsCancellationRequested))
                    {
                        // Every Line with a reference in the Script
                        foreach (var _line in _script.lines.TakeWhile(_Line => !referenceToken.IsCancellationRequested))
                        {
                            // Duplicate, so the saved "lineContent" in "_line" won't be changed
                            var _lineContent = string.Copy(_line.lineContent);

                            // Every Match in the Line
                            foreach (var _match in _line.matchCount.TakeWhile(_Match => !referenceToken.IsCancellationRequested))
                            {
                                for (var i = 0; i < _match.Value; i++)
                                {
                                    if (referenceToken.IsCancellationRequested)
                                        break;

                                    _line.AddIndices(CheckMatch(_lineContent, _match.Key, out var _newLineContent, out var _warning));
                                    // Overrides the LineContent with the content, of the in the previous Match removed Match
                                    _lineContent = _newLineContent;
                                    if (_warning) _line.SetWarning();
                                    
                                    linesChecked++;
                                }
                            }
                        }
                    }

                    // foreach (var _script in _scripts)
                    // {
                    //     foreach (var _line in _script.lines)
                    //     {
                    //         D.Log_SDF(_line.lineContent, "|", "Line: ", F.B, _line.lineIndex.ToString(), F.B, "\n", _line.indices);
                    //     }
                    // }

                    var _scriptLines = new List<ScriptLines>();
                    
                    foreach (var _script in _scripts)
                    {
                        var _tmpScript = new ScriptLines(_script.path);
                        
                        foreach (var _line in _script.lines)
                            _tmpScript.AddLine(_line);
                        
                        _scriptLines.Add(_tmpScript);
                    }
                    
                    D.Log_SDF(_scriptLines, F.NL);
                    
                }, referenceSource.Token);

                await _getReferences;
                
                D.Log_SDF("Finished");
                referenceSource.Dispose();
                isSearching = false;
        }
        
        /// <summary>
        /// Checks if the passed Line has a Reference to this Plugin
        /// </summary>
        /// <param name="_Line">The line to check for a reference</param>
        /// <returns>Returns "true" if a reference to this Plugin is found in the Line</returns>
        private static bool HasReferences(string _Line)
        {
            return Regex.IsMatch(_Line, USING_DIRECTIVE)  || Regex.IsMatch(_Line, METHOD_CALL);
        }

        /// <summary>
        /// Gets all matches and their count in a Line
        /// </summary>
        /// <param name="_LineContent">The content in this Line</param>
        /// <param name="_LineIndex">The index of this Line</param>
        /// <param name="_Line">Object that contains all data for this Line</param>
        /// <param name="_ReferenceCount">Total number of references, found in this Line</param>
        /// <returns>Returns "true" when a reference of this Plugin was found in this Line</returns>
        private static bool GetReferences(string _LineContent, int _LineIndex, out Line _Line, out int _ReferenceCount)
        {
            // TODO: Exclude Methods inside strings
            
            _Line = new Line(_LineContent, _LineIndex);
            
            var _usingDirective = Regex.IsMatch(_Line.lineContent, USING_DIRECTIVE);
            var _methodCall = Regex.IsMatch(_Line.lineContent, METHOD_CALL);
            var _usingCount = 0;
            var _logCount = 0;
            var _warningCount = 0;
            var _errorCount = 0;
            var _exceptionCount = 0;
            
            if (_usingDirective)
            {
                _usingCount = Regex.Matches(_Line.lineContent, USING_COUNT).Count;
                _Line.matchCount.Add(RegexMatch.Using, _usingCount);
            }
            if (_methodCall)
            {
                _logCount = Regex.Matches(_Line.lineContent, LOG_COUNT).Count;
                _warningCount = Regex.Matches(_Line.lineContent, WARNING_COUNT).Count;
                _errorCount = Regex.Matches(_Line.lineContent, ERROR_COUNT).Count;
                _exceptionCount = Regex.Matches(_Line.lineContent, EXCEPTION_COUNT).Count;

                if (_logCount > 0)
                    _Line.matchCount.Add(RegexMatch.Log, _logCount);
                if (_warningCount > 0)
                    _Line.matchCount.Add(RegexMatch.Warning, _warningCount);
                if (_errorCount > 0)
                    _Line.matchCount.Add(RegexMatch.Error, _errorCount);
                if (_exceptionCount > 0)
                    _Line.matchCount.Add(RegexMatch.Exception, _exceptionCount);
            }

            _ReferenceCount = _usingCount + _logCount + _warningCount + _errorCount + _exceptionCount;
            return _usingDirective || _methodCall;
        }

        /// <summary>
        /// Searches the Line for the passed Match
        /// </summary>
        /// <param name="_LineContent">The Line to search in</param>
        /// <param name="_Match">The Match to search for</param>
        /// <param name="_NewLineContent">LineContent, after the previous match has been removed from it (Only needed, when there are multiple matches in one Line)</param>
        /// <param name="_Warning">Is there a potential error in this Line?</param>
        /// <returns>Returns the Start and End Indices of the Match in the Line</returns>
        private static Indices CheckMatch(string _LineContent, RegexMatch _Match, out string _NewLineContent, out bool _Warning)
        {
            switch (_Match)
            {
                case RegexMatch.Using:
                    return GetIndices(_LineContent, "using ", out _NewLineContent, out _Warning);
                case RegexMatch.Log:
                    return GetIndices(_LineContent, "Log_SDF", out _NewLineContent, out _Warning);
                case RegexMatch.Warning:
                    return GetIndices(_LineContent, "Warning_SDF", out _NewLineContent, out _Warning);
                case RegexMatch.Error:
                    return GetIndices(_LineContent, "Error_SDF", out _NewLineContent, out _Warning);
                case RegexMatch.Exception:
                    return GetIndices(_LineContent, "Exception_SDF", out _NewLineContent, out _Warning);
                default:
                    _NewLineContent = _LineContent;
                    _Warning = false;
                    return new Indices(-1, -1);
            }
        }
        
        /// <summary>
        /// Gets the start and end indices of the reference, that will be removed
        /// </summary>
        /// <param name="_LineContent">The Line, the reference is in</param>
        /// <param name="_SearchWord">The word that is searched for in this Line</param>
        /// <param name="_NewLineContent">LineContent, where the previous reference is replaced with whitespaces, so it won't be found again</param>
        /// <param name="_Warning">Does this Line contain potential errors?</param>
        /// <returns>Returns the start and end Indices of the reference to remove</returns>
        private static Indices GetIndices(string _LineContent, string _SearchWord, out string _NewLineContent, out bool _Warning)
        {
            _Warning = false;

            var _searchWordIndex = _LineContent.IndexOf(_SearchWord, StringComparison.Ordinal);
            // Append Whitespaces, so the substring will have the same length as the original
            var _substring = StringBuilderUtility.Append("".PadRight(_searchWordIndex), StringBuilderUtility.Substring(_LineContent, _searchWordIndex));

            var _character = new CharacterCheck();
            var _startIndex = 0;
            var _endIndex = 0;
            
            // Get the index of the object before the Method (For Extension-Method calls)
            if (_SearchWord != "using ")
                // Right to left
                _character = CheckCharacter(_LineContent, _searchWordIndex, false, _character, out _startIndex, _SearchWord);
            
            // Left to right
            _character = CheckCharacter(_substring, 0, true, _character, out _endIndex);
            
            // Closing bracket of this Method-call is in a different Line
            if (_character.openingBracketCount > _character.closingBracketCount)
            {
                _endIndex = _LineContent.Length - 1;
                _Warning = true;
            }

            // TODO: Can probably be removed
            // _substring = StringBuilderUtility.Substring(_LineContent, _startIndex, _endIndex - _startIndex + 1);
            // D.Log_SDF(_substring.Length - _substring.TrimStart().Length, _substring.Length - _substring.TrimEnd().Length);
            // _startIndex += _substring.Length - _substring.TrimStart().Length;
            // _endIndex -= _substring.Length - _substring.TrimEnd().Length;

            // String before the StartIndex of the Match
            var _start = StringBuilderUtility.Substring(_LineContent, 0, _startIndex == 0 ? 0 : _startIndex - 1);
            // String after the EndIndex of the Match
            var _end = StringBuilderUtility.Substring(_LineContent, _endIndex + 1 == _LineContent.Length ? _LineContent.Length - 1 : _endIndex + 1);
            // Replaces the previous match with Whitespaces of the same length
            var _fill = "".PadRight(_LineContent.Length - (_start.Length + _end.Length));

            _NewLineContent = StringBuilderUtility.Append(_start, _fill, _end);
            
            return new Indices(_startIndex, _endIndex);
        }

        /// <summary>
        /// Searches for the Method start/end in the passed string
        /// </summary>
        /// <param name="_Line">The string to search in</param>
        /// <param name="_Initializer">Index to start the search at</param>
        /// <param name="_Forward">"True" = left to right | "False" = right to left</param>
        /// <param name="_Character">"True" = Forward for-loop | "False" = Reverse for-loop</param>
        /// <param name="_Index">Start/End-index of the searched character</param>
        /// <param name="_SearchWord">The word that is searched for in this Line</param>
        /// <returns>Returns an object with info about special character in this Method-call</returns>
        private static CharacterCheck CheckCharacter(string _Line, int _Initializer, bool _Forward, CharacterCheck _Character, out int _Index, string _SearchWord = "")
        {
            var _stringQuoteOpen = false;
            var _charQuoteOpen = false;
            _Character.openingBracketCount = 0;
            _Character.closingBracketCount = 0;
            var _openingCommentCount = 0;
            var _closingCommentCount = 0;
            var _count = 0;
            
            _Index = 0;

            // Check if Extension-Method
            if (!_Forward)
            {
                var _tmpLine = StringBuilderUtility.Substring(_Line, 0, _Initializer + _SearchWord.Length);

                // Match the Method as an Extension Method, when a "." is before it, except when the Method is called like: "SimpleDebugFormatting.D.METHOD()" or "D.METHOD()"
                if ( Regex.IsMatch(_tmpLine, StringBuilderUtility.Append(@"\w+\s*[.]\s*\b", _SearchWord, @"\b"))                                      &&
                    !Regex.IsMatch(_tmpLine, StringBuilderUtility.Append(@"\bD\b\s*[.]\s*\b", _SearchWord, @"\b"))                                    && // Mustn't be "D.METHOD()"
                    !Regex.IsMatch(_tmpLine, StringBuilderUtility.Append(@"\bSimpleDebugFormatting\b\s*[.]\s*\bD\b\s*[.]\s*\b", _SearchWord, @"\b")))    // Mustn't be "SimpleDebugFormatting.D.METHOD()"
                    _Character.isExtension = true;
            }
            
            // Check each character
            for (var i = _Initializer; _Forward ? i < _Line.Length : i >= 0; i = _Forward ? i + 1 : i - 1)
            {
                switch (_Line[i])
                {
                    // Quotes will be "false" on every second occurence
                    case '"':
                        _stringQuoteOpen = !_stringQuoteOpen;
                        break;
                    case '\'':
                        _charQuoteOpen = !_charQuoteOpen;
                        break;
                    // Brackets will only be counted when both quotes are "false", so they won't be part of a "string" or "char"
                    case '(':
                        if (!_stringQuoteOpen && !_charQuoteOpen)
                            _Character.openingBracketCount++;
                        break;
                    case ')':
                        if (!_stringQuoteOpen && !_charQuoteOpen)
                            _Character.closingBracketCount++;
                        if (_Forward && _Character.openingBracketCount == _Character.closingBracketCount)
                            _Character.lastClosingIndex = i;
                        break;
                    // Comments
                    case '/' when !_stringQuoteOpen && !_charQuoteOpen:
                        if (i + 1 < _Line.Length && _Line[i + 1] == '*')
                            _openingCommentCount++;
                        else if (i - 1 >= 0 && _Line[i - 1] == '*')
                            _closingCommentCount++;
                        break;
                    // Check if Extension-Method
                    case '.' when _Character.isExtension:
                        _Index = i;
                        return _Character;
                }

                _count = _count + 1 < _Line.Length ? _count + 1 : _Line.Length - 1 ;

                // Quotes must be "false" (so the character is not part of a "string"/"char") and the brackets and comments must have the same amount, to make sure the last ClosingBracket/Comment was reached
                if (Regex.IsMatch(_Line[i].ToString(), @"(\w|\s)") || _stringQuoteOpen || _charQuoteOpen || _Character.openingBracketCount != _Character.closingBracketCount || _openingCommentCount != _closingCommentCount) continue;

                    if (!_Character.isExtension)
                        _Index = _Forward ? _count : _Initializer - _count;
                    else
                        _Index = _Character.lastClosingIndex;
                    
                    break;
            }
            
            return _Character;
        }
    }
}