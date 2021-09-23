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
    internal class ReferenceRemover_Old : ScriptableObject
    {
        #region Internals
            /// <summary>
            /// Is the reference search currently running?
            /// </summary>
            internal static bool isSearching;
            /// <summary>
            /// Number of C#-Files in this Unity Project
            /// </summary>
            internal static int fileInProject;
            /// <summary>
            /// Number of C#-Files that have been read through
            /// </summary>
            internal static int readFiles;
            /// <summary>
            /// Total Files to search in for references 
            /// </summary>
            internal static int filesToSearchIn;
            /// <summary>
            /// Number of C#-Files that have been read through
            /// </summary>
            internal static int findReferences;
            /// <summary>
            /// Number of C#-Files that have references to this Plugin
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
            /// Key = MethodName, Value = List of ClassNames, the MethodName is declared in (true = static Method, false = non static Method)
            /// </summary>
            private static Dictionary<string, Dictionary<bool, List<string>>> excludedTypeNames;
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
                var _scriptPaths = Directory.EnumerateFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories).ToArray(); // Application.dataPath // @"Assets\Scripts\Development\Test" 
                //var _scriptPaths = new[] { @"Assets\Plugins\Mirror\Editor\Weaver\Weaver.cs" }; // To test single files
                fileInProject = _scriptPaths.Length;
                
                excludedTypeNames = new Dictionary<string , Dictionary<bool, List<string>>> 
                { 
                    { "Log",       new Dictionary<bool, List<string>> { { true, new List<string> { "Debug", "Math" } }, { false, new List<string>() } } },
                    { "Warning",   new Dictionary<bool, List<string>> { { true, new List<string>()                   }, { false, new List<string>() } } }, 
                    { "Error",     new Dictionary<bool, List<string>> { { true, new List<string>()                   }, { false, new List<string>() } } }, 
                    { "Exception", new Dictionary<bool, List<string>> { { true, new List<string> { "System" }        }, { false, new List<string>() } } }
                };
                
                var _scripts = new List<Script>();
                var _scriptContents = new Dictionary<string, string[]>();
                
                var _read = Task.Run(async () =>
                {
                    // TODO: Get Methods in all Assemblies and write them in the Dictionary manually
                    // TODO: Find Namespace usages
                    // TODO: Check for "using static" in each script with references
                    
                    var _tasks = new List<Task>();

                    // Read each Script in the Project
                    foreach (var _path in _scriptPaths)
                    {
                        if (referenceToken.IsCancellationRequested)
                            break;
                        
                        // Won't search in files from this Plugin
                        if (!_path.Contains(_pluginPath))
                        {
                            KeyValuePair<string, string[]> _scriptContent;
                            var _tmpScriptContents = _scriptContents;
                            
                            _tasks.Add(Task.Run(() =>
                            {
                                if (referenceToken.IsCancellationRequested)
                                    return;
                                
                                _scriptContent = new KeyValuePair<string, string[]>(_path, File.ReadLines(_path).ToArray());
                                GetExcludedTypeNames(_scriptContent.Value);
                                
                            }, referenceSource.Token).ContinueWith(delegate {

                                if (!referenceToken.IsCancellationRequested)
                                {
                                    try
                                    {
                                        _tmpScriptContents.Add(_scriptContent.Key, _scriptContent.Value); 
                                        readFiles++;   
                                    }
                                    catch { /* Ignored */ }
                                }

                            }, TaskContinuationOptions.RunContinuationsAsynchronously));
                        }
                        else
                            readFiles++;
                    }
                    
                    await Task.WhenAll(_tasks);
                    
                }, referenceSource.Token);
                    
                await _read;

                var _check = Task.Run(() =>
                {
                    // Removes all Scripts that don't contain a reference to this Plugin
                    for (var i = 0; i < _scriptContents.Count; i++)
                    {
                        if (referenceToken.IsCancellationRequested)
                            break;

                        try
                        {
                            if (_scriptContents.ElementAt(i).Value.All(_Line => !GetNamespaceReference(_Line)))
                            {
                                _scriptContents.Remove(_scriptContents.ElementAt(i--).Key);
                                filesToSearchIn = _scriptContents.Count;
                            }
                            else
                            {
                                _scripts.Add(new Script(_scriptContents.ElementAt(i).Key, _scriptContents.ElementAt(i).Value));
                                findReferences = i + 1;
                                filesWithReferences = _scripts.Count;
                            }
                        }
                        catch { /* Ignored */ }
                    }
                    
                    _scriptPaths = null;
                    _scriptContents = null;

                    // Search all references
                    for (var i = 0; i < _scripts.Count; i++)
                    {
                        if (referenceToken.IsCancellationRequested)
                            break;
                        
                        // Check each line inside the Script
                        for (var j = 0; j < _scripts[i].content.Length; j++)
                        {
                            if (referenceToken.IsCancellationRequested)
                                break;
                            
                            if (GetReferences(_scripts[i].content[j], out var _match, out var _warning))
                            {
                                //_scripts[i].lines.Add(new Line(_scripts[i].content[j], j, _warning), _match);
                                totalReferences++;
                            }
                            // Finds all "using equals" usages in the Script (using NAME = NAMESPACE.CLASS)
                            if (!Regex.IsMatch(_scripts[i].content[j], @"using\s+\w+\s*=\s*\bSimpleDebugFormatting\b\s*[.].*\s*;?") &&
                                 Regex.IsMatch(_scripts[i].content[j], @"using\s+\w+\s*=\s*\S+\s*;?"))
                            {
                                var _usingIndex = _scripts[i].content[j].IndexOf("using", StringComparison.Ordinal) + "using".Length;
                                var _afterUsing = StringBuilderUtility.Substring(_scripts[i].content[j], _usingIndex);
                                var _nameIndex = _afterUsing.TakeWhile(_Char => _Char == ' ').Count();
                                var _name = StringBuilderUtility.Substring(_afterUsing, _nameIndex).TakeWhile(_Char => _Char != ' ' && _Char != '=').ToArray();
                                //_scripts[i].usingEquals.Add(StringBuilderUtility.Join(_name));
                            }
                        }
                        
                        readFilesWithReferences = i + 1;
                    }

                    // Get the line indices of all references
                    foreach (var _script in _scripts)
                    {
                        if (referenceToken.IsCancellationRequested)
                            break;
                        
                        // Each line with a reference in this Script
                        for (var i = 0; i < _script.lines.Count; i++)
                        {
                            if (referenceToken.IsCancellationRequested)
                                break;
                            
                            // Gets the indices of the first match
                            //_script.lines.ElementAt(i).Key.Add(GetIndices(_script, _script.lines.ElementAt(i).Key, _script.lines.ElementAt(i).Value));
                            
                            // Check if there are still matches in that line
                            RegexMatch _match;
                            do
                            {
                                if (referenceToken.IsCancellationRequested)
                                    break;

                                // Last index of the previous match
                                //var _index = _script.lines.ElementAt(i).Key.indices.LastOrDefault().endIndex + 1;
                                // New string to check, one index after the previous match
                                //var _substring = _script.content[_script.lines.ElementAt(i).Key.lineIndex].TrySubstring(_index);

                                // Repeat until there are no more matches in that line
                                //if (GetReferences(_substring, out _match, out var _warning))
                                //_script.lines.ElementAt(i).Key.Add(GetIndices(_script, _script.lines.ElementAt(i).Key, _match, _index));

                            } while (true);//_match != RegexMatch.None);
                    
                            linesChecked++;
                        }
                    }

                    var _scriptLines = new List<ScriptLines>();
                    
                    foreach (var _script in _scripts)
                    {
                        var _tmpScript = new ScriptLines(_script.path);
                        
                        foreach (var _line in _script.lines)
                        {
                            _tmpScript.AddLine(_line);
                        }
                        _scriptLines.Add(_tmpScript);
                    }
                    
                    
                    D.Log_SDF(_scriptLines, F.NL);
                    
                    //D.Log(excludedTypeNames, F.NL);
                    //D.Log(_scripts.SelectMany(_Script => _Script.usingEquals.Select(_Name => _Name)), F.NL);
                    //D.Log(_scripts.SelectMany(_Script => _Script.lines.Select(_Line => _Script.content[_Line - 1])), F.NL);
                    
                }, referenceSource.Token);


                try { await _check; }
                catch {  /* Ignored */ }
                
                D.Log_SDF("Finished");

                excludedTypeNames = null;
                referenceSource.Dispose();
                isSearching = false;
        }

        /// <summary>
        /// Resets the values shown in the Inspector
        /// </summary>
        private static void ResetValues()
        { 
            fileInProject = 0; 
            readFiles = 0;
            filesToSearchIn = 0;
            findReferences = 0;
            filesWithReferences = 0;
            readFilesWithReferences = 0;
            totalReferences = 0;
            linesChecked = 0;
        }
        
        /// <summary>
        /// Searches Members with the names "Log", "Warning", "Error" and "Exception" in all other Scripts in this Project 
        /// </summary>
        /// <param name="_Script">The File to search in</param>
        private static void GetExcludedTypeNames(IReadOnlyList<string> _Script)
        {
            for (var i = 0; i < _Script.Count; i++)
            {
                // Check for a "Log,Warning,Error,Exception"-Member in this Script
                if (CheckForType(_Script[i]) || !CheckForMember(_Script[i], out var _static, out var _memberName)) continue;
                    
                    var _lineIndices = new List<int>();
                        
                    // "j" can't be greater than "i", because then the "class" or "struct" would be declared after the Method
                    for (var j = 0; j <= i; j++)
                        // Checks for all "class" or "struct" declarations in the Script
                        if (CheckForType(_Script[j]))
                            _lineIndices.Add(j);

                    var _lineIndex = -1;
                        
                    // Saves the line index that is closest to the line where the Method was found
                    foreach (var _index in _lineIndices)
                        _lineIndex = _index > _lineIndex ? _index : _lineIndex;
                        
                    // Get the "class"/"struct" name that declares the Method
                    var _searchedKeyword = new[] { "class", "struct" };
                    foreach (var _keyword in _searchedKeyword)
                    {
                        var _index = _Script[_lineIndex].IndexOf(_keyword, StringComparison.Ordinal);
                        if (_index == -1) continue;
                                
                        var _afterKeyword = StringBuilderUtility.Substring(_Script[_lineIndex], _index + _keyword.Length);
                        var _whitespaceCount = _afterKeyword.TakeWhile(_Char => _Char == ' ').Count();
                        var _nameIndexCount = StringBuilderUtility.Substring(_afterKeyword, _whitespaceCount).TakeWhile(_Char => _Char != ' ' && _Char != '<').Count();
                        var _typeName = StringBuilderUtility.Substring(_Script[_lineIndex], _index + _keyword.Length + _whitespaceCount, _nameIndexCount);
                                    
                        if (_static)
                            if (!excludedTypeNames[_memberName][true].Contains(_typeName)) excludedTypeNames[_memberName][true].Add(_typeName);
                            else
                            if (!excludedTypeNames[_memberName][false].Contains(_typeName)) excludedTypeNames[_memberName][false].Add(_typeName);
                        break;
                    }
            }
        }

        /// <summary>
        /// Checks if the line contains a "class"/"struct" declaration
        /// </summary>
        /// <param name="_Line">The line to check</param>
        /// <returns>Returns "true" when the line contains a "class"/"struct" declaration</returns>
        private static bool CheckForType(string _Line)
        {
            return Regex.IsMatch(_Line, @"((private|protected|internal|public)\s+)?(static\s+)?((sealed|abstract)\s+)?(readonly\s+)?(class|struct){1}\s+[\w_]+(\s*<.+>)?\s*[:]?");
        }

        /// <summary>
        /// Checks if the line contains a Member with the same name as the Methods in this Plugin
        /// </summary>
        /// <param name="_Line">The line to check for the Member</param>
        /// <param name="_Static">Is this a static Member?</param>
        /// <param name="_MemberName">The MemberName that was found in the line</param>
        /// <returns>Returns "true" if the line contains a Member with the same name</returns>
        private static bool CheckForMember(string _Line, out bool _Static, out string _MemberName)
        {
            // STATIC: ((private|protected|internal|public)\s+)?(static\s+)(ref\s+)?(\w+(\s*<.+>\s*|\s+)\bKEYWORD\b){1}(\s*<.+>\s*)?((\s*([;]|[=].*[;]|[{](.*[}])?|=>|[(]{1}.*[)]*))|\W)
            // NORMAL: ((private|protected|internal|public)\s+)?((override|sealed override|override sealed)\s+)?(ref\s+)?((abstract|virtual)\s+)?(\w+(\s*<.+>\s*|\s+)\bKEYWORD\b){1}(\s*<.+>\s*)?((\s*([;]|[=].*[;]|[{](.*[}])?|=>|[(]{1}.*[)]*))|\W)
            
            const string _STATIC_PATTERN1 = @"((private|protected|internal|public)\s+)?(static\s+)(ref\s+)?(\w+(\s*<.+>\s*|\s+)\b";
            const string _STATIC_PATTERN2 = @"\b){1}(\s*<.+>\s*)?((\s*([;]|[=].*[;]|[{](.*[}])?|=>|[(]{1}.*[)]*))|\W)";
            const string _NORMAL_PATTERN1 = @"((private|protected|internal|public)\s+)?((override|sealed override|override sealed)\s+)?(ref\s+)?((abstract|virtual)\s+)?(\w+(\s*<.+>\s*|\s+)\b";
            const string _NORMAL_PATTERN2 = @"\b){1}(\s*<.+>\s*)?((\s*([;]|[=].*[;]|[{](.*[}])?|=>|[(]{1}.*[)]*))|\W)";
            
            if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_NORMAL_PATTERN1, "Log", _NORMAL_PATTERN2)))
            {
                if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_STATIC_PATTERN1, "Log", _STATIC_PATTERN2)))
                {
                    _MemberName = "Log";
                    _Static = true;
                    return true;
                }
                else
                {
                    _MemberName = "Log";
                    _Static = false;
                    return true;   
                }
            }
            if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_NORMAL_PATTERN1, "Warning", _NORMAL_PATTERN2)))
            {
                if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_STATIC_PATTERN1, "Warning", _STATIC_PATTERN2)))
                {
                    _MemberName = "Warning";
                    _Static = true;
                    return true;
                }
                else
                {
                    _MemberName = "Warning";
                    _Static = false;
                    return true;   
                }
            }
            if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_NORMAL_PATTERN1, "Error", _NORMAL_PATTERN2)))
            {
                if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_STATIC_PATTERN1, "Error", _STATIC_PATTERN2)))
                {
                    _MemberName = "Error";
                    _Static = true;
                    return true;
                }
                else
                {
                    _MemberName = "Error";
                    _Static = false;
                    return true;   
                }
            }
            if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_NORMAL_PATTERN1, "Exception", _NORMAL_PATTERN2)))
            {
                if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_STATIC_PATTERN1, "Exception", _STATIC_PATTERN2)))
                {
                    _MemberName = "Exception";
                    _Static = true;
                    return true;
                }
                else
                {
                    _MemberName = "Exception";
                    _Static = false;
                    return true;   
                }
            }
            
            _MemberName = string.Empty;
            _Static = false;
            return false;
        }
        
        /// <param name="_Line">The line to check for the Namespace reference</param>
        /// <returns>Returns "true" if a reference to this Plugin's Namespace is found</returns>
        private static bool GetNamespaceReference(string _Line)
        {
            return Regex.IsMatch(_Line, @"using\s+\bSimpleDebugFormatting\b\s*;?")                        || // Using directive
                   Regex.IsMatch(_Line, @"using\s+static\s+\bSimpleDebugFormatting\b\s*[.]\s*[A-Z]\s*;?") || // Using static
                   Regex.IsMatch(_Line, @"\bSimpleDebugFormatting\b\s*[.]\s*[A-Z]\S*\s*;*");                 // Inline Namespace call
        }
        
        /// <summary>
        /// Checks if the script contains any references to this Plugin
        /// </summary>
        /// <param name="_Line">The line to check for a reference</param>
        /// <param name="_Match">What the Regex-Pattern has matched with</param>
        /// <param name="_Warning">Does this line contain possible errors?</param>
        /// <returns>Returns true when a reference has been found</returns>
        private static bool GetReferences(string _Line, out RegexMatch _Match, out bool _Warning)
        {
            _Warning = false;
            
            // Using directive
            if (Regex.IsMatch(_Line, @"using\s+SimpleDebugFormatting\s*;?")  )
            {
                _Match = RegexMatch.Using;
                return true;
            }
            // Using static
            if (Regex.IsMatch(_Line, @"using\s+static\s+SimpleDebugFormatting\s*[.]\s*[A-Z]\s*;?"))
            {
                //_Match = RegexMatch.UsingStatic;
                //return true;
            }
            // Inline namespace call
            if (Regex.IsMatch(_Line, @"SimpleDebugFormatting\s*[.]\s*[A-Z]\S*\s*;*"))
            {
                //_Match = RegexMatch.InlineNamespace;
                //return true;
            }
            // D.Log()-Method
            if (Regex.IsMatch(_Line, @"(\w+\s*[.]\s*|(^|\W+))\bLog\b\s*[(].*[)]\s*;*"))
            {
                if (ExcludedTypeNames(_Line, excludedTypeNames["Log"][true], @"\s*[.]\s*\bLog\b\s*[(].*[)]\s*;*"))// TODO: Check if script contains declaration for non static Member
                {
                    if (MultipleSemicolons(_Line, out _Match, out _Warning))
                    {
                        return true;   
                    }
                    else
                    {
                        _Match = RegexMatch.Error;
                        return false;   
                    }
                }
                else
                {
                    _Match = RegexMatch.Log;
                    return true;   
                }
            }
            // D.Warning()-Method
            if (Regex.IsMatch(_Line, @"(\w+\s*[.]\s*|(^|\W+))\bWarning\b\s*[(].*[)]\s*;*")) 
            {
                if (ExcludedTypeNames(_Line, excludedTypeNames["Warning"][true], @"\s*[.]\s*\bWarning\b\s*[(].*[)]\s*;*"))// TODO: Check if script contains declaration for non static Member
                {
                    if (MultipleSemicolons(_Line, out _Match, out _Warning))
                    {
                        return true;   
                    }
                    else
                    {
                        _Match = RegexMatch.Error;
                        return false;   
                    }
                }
                else
                {
                    _Match = RegexMatch.Warning;
                    return true;   
                }
            }
            // D.Error()-Method
            if (Regex.IsMatch(_Line, @"(\w+\s*[.]\s*|(^|\W+))\bError\b\s*[(].*[)]\s*;*")) 
            {
                if (ExcludedTypeNames(_Line, excludedTypeNames["Error"][true], @"\s*[.]\s*\bError\b\s*[(].*[)]\s*;*"))// TODO: Check if script contains declaration for non static Member
                {
                    if (MultipleSemicolons(_Line, out _Match, out _Warning))
                    {
                        return true;   
                    }
                    else
                    {
                        _Match = RegexMatch.Error;
                        return false;   
                    }
                }
                else
                {
                    _Match = RegexMatch.Error;
                    return true;   
                }
            }
            // D.Exception()-Method
            if (Regex.IsMatch(_Line, @"(throw\s+(\w+[.]\s*)?|\w+[.]\s*|(^|\W+))\bException\b\s*[(].*[)]\s*;*")) 
            {
                if (ExcludedTypeNames(_Line, excludedTypeNames["Exception"][true], "", @"(throw\s+(", @"[.]\s*)?|", @"[.]\s*|new\s+", @"\s*[.]\s*|new\s+)\bException\b\s*[(].*[)]\s*;*"))// TODO: Check if script contains declaration for non static Member 
                {
                    if (MultipleSemicolons(_Line, out _Match, out _Warning))
                    {
                        return true;   
                    }
                    else
                    {
                        _Match = RegexMatch.Error;
                        return false;   
                    }
                }
                else
                {
                    _Match = RegexMatch.Exception;
                    return true;   
                }
            }

            _Match = RegexMatch.Error;
            return false;
        }

        /// <summary>
        /// Checks if the Method belongs to a class/struct not in this Plugin
        /// </summary>
        /// <param name="_Line">The line to check</param>
        /// <param name="_ExcludedTypeNames">List of class/struct-names that have Method declaration with the same names</param>
        /// <param name="_Pattern">Pattern for "Log"/Warning"/"Error"-Methods</param>
        /// <param name="_Patterns">Pattern for "Exception"-Methods</param>
        /// <returns>Returns "true" when the Method belongs to a class/struct outside of this Plugin</returns>
        private static bool ExcludedTypeNames(string _Line, List<string> _ExcludedTypeNames, string _Pattern = "", params string[] _Patterns)
        {
            foreach (var _typeName in _ExcludedTypeNames)
            {
                if (_Patterns.Length > 0)
                {
                    var _pattern = string.Empty;
                    for (var i = 0; i < _Patterns.Length; i++)
                        _pattern = StringBuilderUtility.Append(_pattern, _Patterns[i], i + 1 != _Patterns.Length ? _typeName : string.Empty);

                    if (Regex.IsMatch(_Line, _pattern))
                        return true;
                }
                else
                {
                    if (Regex.IsMatch(_Line, StringBuilderUtility.Append(_typeName, _Pattern)))
                        return true;   
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if there are multiple semicolons in the line, that are not inside strings ot chars
        /// </summary>
        /// <param name="_Line">The line to check</param>
        /// <param name="_Match">What the Regex-Pattern has matched with</param>
        /// <returns>Returns true when the line contains a reference to this Plugin</returns>
        private static bool MultipleSemicolons(string _Line, out RegexMatch _Match, out bool _Warning)
        {
            _Warning = false;
            
            // Multiple semicolons = maybe multiple Method calls
            if (_Line.Count(_Char => _Char == ';') > 1)
            {
                // Remove all string/char semicolons
                if (Regex.IsMatch(_Line, "(\".*;.*\")*(\';\')*"))
                {
                    // Removes all semicolons inside strings ot chars
                    var _tmpLine = Regex.Replace(_Line, "(\".*;.*\")*(\';\')*", string.Empty);
                    if (_tmpLine.Count(_Char => _Char == ';') > 1)
                    {
                        var _semicolons = _tmpLine.Split(';').Where(_String => !_String.IsNullOrEmptyOrWhitespace()).ToArray();

                        //var _match = RegexMatch.None;
                        // if (_semicolons.Any(_Semicolon => GetReferences(_Semicolon, out _match, out var _warning)))
                        // {
                        //     _Match = _match;
                        //     _Warning = true;
                        //     return true;
                        // }
                        // else
                        // {
                        //     _Match = _match;
                        //     return false;
                        // }
                    }
                }
            }

            _Match = RegexMatch.Error;
            return false;
        }
        
        /// <summary>
        /// Gets the start and end indices of the reference in the line
        /// </summary>
        /// <param name="_Script">The C#-File</param> 
        /// <param name="_Line">The line in the Script to check</param>
        /// <param name="_Match">What the Regex-Pattern has matched with</param>
        /// <param name="_StartIndex">Index in the line, from where to start the search</param>
        /// <returns>Returns an object with the start and end indices of the reference in the line</returns>
        private static Indices GetIndices(Script _Script, Line _Line, RegexMatch _Match, int _StartIndex = 0)
        {
            switch (_Match)
            {
                case RegexMatch.Using:
                    return GetNamespace(_Script, _Line, "using ", _StartIndex);
                // case RegexMatch.UsingStatic:
                //     return GetNamespace(_Script, _Line, "using static ", _StartIndex);
                // case RegexMatch.InlineNamespace:
                //     return GetMethod(_Script, _Line, "SimpleDebugFormatting", RegexMatch.InlineNamespace, _StartIndex);
                // case RegexMatch.Log:
                //     return GetMethod(_Script, _Line, "Log", RegexMatch.Log, _StartIndex);
                // case RegexMatch.Warning:
                //     return GetMethod(_Script, _Line, "Warning", RegexMatch.Warning, _StartIndex);
                // case RegexMatch.Error:
                //     return GetMethod(_Script, _Line, "Error", RegexMatch.Error, _StartIndex);
                // case RegexMatch.Exception:
                //     return GetMethod(_Script, _Line, "Exception", RegexMatch.Exception, _StartIndex);
                default:
                    return new Indices(-1, -1);
            }
        }

        /// <summary>
        /// Gets the start and end Indices of a "using statement", in a line
        /// </summary>
        /// <param name="_Script">The C#-File</param> 
        /// <param name="_Line">The line in the Script to check</param>
        /// <param name="_SearchedString">The string to search for in the line</param>
        /// <param name="_StartIndex">Index in the line, from where to start the search</param>
        /// <returns>Returns an object with the start and end indices of the reference in the line</returns>
        private static Indices GetNamespace(Script _Script, Line _Line, string _SearchedString, int _StartIndex = 0)
        {
            var _startIndex = _Script.content[_Line.lineIndex].IndexOf(_SearchedString, _StartIndex, StringComparison.Ordinal);
            if (_startIndex == -1) return new Indices(-1, -1);
            var _endIndex = StringBuilderUtility.Substring(_Script.content[_Line.lineIndex], _startIndex + _SearchedString.Length).TakeWhile(_Char => _Char != ';').Count();

            return new Indices(_startIndex, _startIndex + _SearchedString.Length + _endIndex);
        }

        /// <summary>
        /// Gets the start and end Indices of a Method reference, in a line
        /// </summary>
        /// <param name="_Script">The C#-File</param> 
        /// <param name="_Line">The line in the Script to check</param>
        /// <param name="_SearchedString">The string to search for in the line</param>
        /// <param name="_Match">What the Regex-Pattern has matched with</param>
        /// <param name="_StartIndex">Index in the line, from where to start the search</param>
        /// <returns>Returns an object with the start and end indices of the reference in the line</returns>
        private static Indices GetMethod(Script _Script, Line _Line, string _SearchedString, RegexMatch _Match, int _StartIndex = 0)
        {
            var _startIndex = _Script.content[_Line.lineIndex].IndexOf(_SearchedString, _StartIndex, StringComparison.Ordinal);

            if (_startIndex == -1)
                return new Indices(-1, -1);;
            
            var _substring = StringBuilderUtility.Substring(_Script.content[_Line.lineIndex], _startIndex);
            // Quotes will be "false" on every second occurence
            var _stringQuoteOpen = false;
            var _charQuoteOpen = false;
            // Brackets will only be counted when both quotes are "false", so they won't be part of a "string" or "char"
            var _openingBracketCount = 0;
            var _closingBracketCount = 0;
            var _endIndex = (_Script.content[_Line.lineIndex].Length - 1) + _startIndex;
            
            // Check each character
            for (var i = 0; i < _substring.Length; i++)
            {
                switch (_substring[i])
                {
                    case '"':
                        _stringQuoteOpen = !_stringQuoteOpen;
                        break;
                    case '\'':
                        _charQuoteOpen = !_charQuoteOpen;
                        break;
                    case '(':
                        if (!_stringQuoteOpen && !_charQuoteOpen)
                            _openingBracketCount++;
                        break;
                    case ')':
                        if (!_stringQuoteOpen && !_charQuoteOpen)
                            _closingBracketCount++;
                        break;
                }


                // Quotes must be "false" (so the ";" is not part of a "string"/"char") and the brackets must have the same amount, to make sure the last closing bracket was reached
                if (_substring[i] != ';' || _stringQuoteOpen || _charQuoteOpen || _openingBracketCount != _closingBracketCount) continue;
                    
                    _endIndex = i + _startIndex;
                    break;
            }

            // TODO: Not working correctly

            // // No ";" found in this line
            // if (_endIndex == 0)
            // {
            //     _endIndex = (_Script.content[_Line.lineIndex].Length - 1) + _startIndex;
            //     
            //     var _currentLine = _Line.lineIndex;
            //     var _keepSearching = true;
            //
            //     do
            //     {
            //         var _line = new Line(++_currentLine);
            //         var _end = _Script.content[_currentLine].Length - 1;
            //
            //         for (var i = 0; i < _Script.content[_currentLine].Length; i++)
            //             if (_Script.content[_currentLine][i] == ';')
            //             {
            //                 _end = i;
            //                 _keepSearching = false;
            //                 break;
            //             }
            //         
            //         _line.indices.Add(new Indices(0, _end));
            //         _Script.lines.Add(_line, _Match);
            //
            //     } while (_keepSearching);
            // }
            
            var _firstPeriod = false;
            var _secondPeriod = false;
            var _name = string.Empty;
            var _count = string.Empty;
            
            // Get class/object-name before the Method (right to left) 
            for (var i = _startIndex - 1; i >= 0; i--)
            {
                // First word before the Method
                if (!_firstPeriod)
                {
                    if (_Script.content[_Line.lineIndex][i] == '.')
                    {
                        _firstPeriod = true;
                        _count = StringBuilderUtility.Append(_count, _Script.content[_Line.lineIndex][i].ToString());
                    }
                    else if (_Script.content[_Line.lineIndex][i] == ' ')
                    {
                        _count = StringBuilderUtility.Append(_count, _Script.content[_Line.lineIndex][i].ToString());
                    }
                    else
                    {
                        _count = string.Empty;
                        break;
                    }
                }
                else
                {
                    if (Regex.IsMatch(_Script.content[_Line.lineIndex][i].ToString(), @"(\w|[_])"))
                    {
                        if (!_secondPeriod)
                            _name = StringBuilderUtility.Append(_name, _Script.content[_Line.lineIndex][i].ToString());

                        _count = StringBuilderUtility.Append(_count, _Script.content[_Line.lineIndex][i].ToString());
                    }
                    else if (Regex.IsMatch(_Script.content[_Line.lineIndex][i].ToString(), @"[.]"))
                    {
                        _secondPeriod = true;
                        _count = StringBuilderUtility.Append(_count, _Script.content[_Line.lineIndex][i].ToString());
                    }
                    else if (Regex.IsMatch(_Script.content[_Line.lineIndex][i].ToString(), @"\s"))
                    {
                        _count = StringBuilderUtility.Append(_count, _Script.content[_Line.lineIndex][i].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            if (_count.Length > 0)
                _startIndex -= _count.TrimEnd().Length;

            // if (_Script.usingEquals.Contains(_name)) _Line.SetWarning();
            return new Indices(_startIndex, _endIndex);
        }
    }
}