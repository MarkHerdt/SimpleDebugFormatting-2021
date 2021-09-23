using System.Collections.Generic;
using System.Linq;

namespace SimpleDebugFormatting.ReferenceRemover
{
    /// <summary>
    /// What the Regex-Pattern has matched with
    /// </summary>
    internal enum RegexMatch
    {
        /// <summary>
        /// Using directive
        /// </summary>
        Using = 1,
        /// <summary>
        /// D.Log_SDF()-Method
        /// </summary>
        Log = 2,
        /// <summary>
        /// D.Warning_SDF()-Method
        /// </summary>
        Warning = 3,
        /// <summary>
        /// D.Error_SDF()-Method
        /// </summary>
        Error = 4,
        /// <summary>
        /// D.Exception_SDF()-Method
        /// </summary>
        Exception = 5
    }
    
    /// <summary>
    /// Stores data of a C# file in the Project
    /// </summary>
    internal readonly struct Script
    {
        #region Internals
            /// <summary>
            /// The path to the script
            /// </summary>
            internal readonly string path;
            /// <summary>
            /// The content of the script
            /// </summary>
            internal readonly string[] content;
            /// <summary>
            /// List of Lines in this Script that contain references to this Plugin
            /// </summary>
            internal readonly List<Line> lines;
        #endregion
        
        /// </summary>
        /// <param name="_Path">The path to the script</param>
        /// <param name="_Content">The content of the script</param>
        internal Script(string _Path, string[] _Content)
        {
            path = _Path;
            content = _Content;

            lines = new List<Line>();
        }
    }
    
    /// <summary>
    /// Holds the line and Plugin reference indices
    /// </summary>
    internal struct Line
    {
        #region Internals
            /// <summary>
            /// The content of this line
            /// </summary>
            internal readonly string lineContent;
            /// <summary>
            /// The line index in the Script
            /// </summary>
            internal readonly int lineIndex;
            /// <summary>
            /// Key = Match that was found in this Line | Value = How often the match was found
            /// </summary>
            internal readonly Dictionary<RegexMatch, int> matchCount;
            /// <summary>
            /// From-To indices where the Plugin references in the line are
            /// </summary>
            internal readonly List<Indices> indices;
        #endregion

        #region Property
            /// <summary>
            /// Whether this line has potential errors
            /// </summary>
            internal bool Warning { get; private set; }
        #endregion
        
        /// <param name="_LineContent">The content of this line</param>
        /// <param name="_LineIndex">The line index in the Script</param>
        internal Line(string _LineContent, int _LineIndex)
        {
            lineContent = _LineContent;
            lineIndex = _LineIndex;

            matchCount = new Dictionary<RegexMatch, int>();
            indices = new List<Indices>();
            Warning = false;
        }

        /// <summary>
        /// Adds the passed Indices to this objects Indices-List when both of them are positive
        /// </summary>
        /// <param name="_Indices">The Indices to add to this Line's List</param>
        internal void AddIndices(Indices _Indices)
        {
            if (_Indices)
                indices.Add(_Indices);
        }

        /// <summary>
        /// Mark this line as a potential error
        /// </summary>
        internal void SetWarning()
        {
            Warning = true;
        }
    }
    
    /// <summary>
    /// Start and end indices for the Plugin references
    /// </summary>
    internal readonly struct Indices
    {
        #region Internals
            /// <summary>
            /// Index in the string where the reference starts
            /// </summary>
            internal readonly int startIndex;
            /// <summary>
            /// Index in the string where the reference ends
            /// </summary>
            internal readonly int endIndex;
        #endregion
        
        /// <param name="_StartIndex">Index in the string where the reference starts</param>
        /// <param name="_EndIndex">Index in the string where the reference ends</param>
        internal Indices(int _StartIndex, int _EndIndex)
        {
            startIndex = _StartIndex;
            endIndex = _EndIndex;
        }
        
        /// <param name="_Indices">The Indices object</param>
        /// <returns>Returns false when any field holds a negative value</returns>
        public static implicit operator bool(Indices _Indices)
        {
            return _Indices.startIndex >= 0 && _Indices.endIndex >= 0;
        }
        
        /// <returns>Returns the values of "startIndex" and "endIndex" separated by a whitespace</returns>
        public override string ToString()
        {
            return StringBuilderUtility.Append(startIndex.ToString(), "-", endIndex.ToString());
        }
    }

    /// <summary>
    /// Data-struct to check for special character in a Method call
    /// </summary>
    internal struct CharacterCheck
    {
        #region Internals
            /// <summary>
            /// Whether the Method call is an Extension Method
            /// </summary>
            internal bool isExtension;
            /// <summary>
            /// Number of '('-character used after the MethodName
            /// </summary>
            internal int openingBracketCount;
            /// <summary>
            /// Number of ')'-character used after the MethodName
            /// </summary>
            internal int closingBracketCount;
            /// <summary>
            /// Last index of a ClosingBracket, when both Bracket counts were equal
            /// </summary>
            internal int lastClosingIndex;
        #endregion
    }
    
    /// <summary>
    /// For displaying the lines inside the Scripts that are about to be removed, in the console
    /// </summary>
    internal struct ScriptLines
    {
        #region Privates
            /// <summary>
            /// Unique global index for each line 
            /// </summary>
            private static int lineCount;
            /// <summary>
            /// The absolute path to the Script
            /// </summary>
            private readonly string path;
            /// <summary>
            /// Key = global Line index, Value = LineContent
            /// </summary>
            private readonly Dictionary<int, KeyValuePair<int, string>> lines;
            /// <summary>
            /// Length of the longest Line in this Script
            /// </summary>
            private int lineContentLength;
        #endregion
        
        /// <param name="_Path">The absolute Path to the Script</param>
        internal ScriptLines(string _Path)
        {
            path = string.Empty;
            lines = new Dictionary<int, KeyValuePair<int, string>>();
            lineContentLength = 0;
            
            path = FormatPath(_Path);
        }

        /// <summary>
        /// Formats the Path to the Script that is displayed in the console
        /// </summary>
        /// <param name="_Path">The absolute Path to the Script</param>
        /// <returns>Returns the formatted Path</returns>
        private static string FormatPath(string _Path)
        {
            var _folderIndex = _Path.LastIndexOf('\\') + 1;
            var _fileIndex = _Path.LastIndexOf('.');
            var _folderPath = StringBuilderUtility.Substring(_Path, 0, _folderIndex);
            var _fileEnding = StringBuilderUtility.Substring(_Path, _fileIndex);
            var _scriptName = StringBuilderUtility.Substring(_Path, _folderIndex, _fileIndex - _folderIndex);
            
            return StringBuilderUtility.Append("<size=11><i>", _folderPath, StringBuilderUtility.Append("<b>", _scriptName, "</b>"), _fileEnding, "</i></size>");
        }

        /// <summary>
        /// Formats all entries of the "lines"-Dictionary into one string
        /// </summary>
        /// <returns>Returns the formatted string</returns>
        private string FormatLines()
        {
            var _string = string.Empty;
            
            foreach (var _line in lines)
                _string = StringBuilderUtility.Append(_string, "<b>[", "<i>", _line.Key.ToString(), "</i>", "]</b> ", 
                          StringBuilderUtility.Append(_line.Value.Value, "".PadRight((lineContentLength - _line.Value.Value.Length) * 2), "<b> | <i>Line: ", _line.Value.Key.ToString(), "</i></b>"), "\n");
            
            return _string;
        }
        
        /// <summary>
        /// Adds a new Line to display, under this Script
        /// </summary>
        /// <param name="_Line">The Line to add</param>
        internal void AddLine(Line _Line)
        {
            var _newLineContent = new List<string>();

            if (_Line.indices[0].startIndex - 1 >= 0)
                _newLineContent.Add(StringBuilderUtility.Substring(_Line.lineContent, 0, _Line.indices[0].startIndex - 1));
                        
            for (var i = 0; i < _Line.indices.Count; i++)
            {
                _newLineContent.Add(StringBuilderUtility.Append("<color=Red>", 
                                                                    StringBuilderUtility.Substring(_Line.lineContent, _Line.indices[i].startIndex, _Line.indices[i].endIndex - _Line.indices[i].startIndex + 1), 
                                                                    "</color>"));

                if (i + 1 != _Line.indices.Count)
                        _newLineContent.Add(StringBuilderUtility.Substring(_Line.lineContent, _Line.indices[i].endIndex, (_Line.indices[i + 1].startIndex - 1) - _Line.indices[i].endIndex));
            }
                            
            if (_Line.indices.Last().endIndex < _Line.lineContent.Length - 1)
                _newLineContent.Add(StringBuilderUtility.Substring(_Line.lineContent, _Line.indices.Last().endIndex + 1));

            var _formattedLineContent = StringBuilderUtility.Join(_newLineContent.ToArray());
            lineContentLength = _formattedLineContent.Length > lineContentLength ? _formattedLineContent.Length : lineContentLength;
            lines.Add(++lineCount, new KeyValuePair<int, string>(_Line.lineIndex, _formattedLineContent));
            // StringBuilderUtility.Append(StringBuilderUtility.Join(_newLineContent.ToArray()), "<b> | <i>Line: ", _Line.lineIndex.ToString(), "</i></b>")
        }

        public override string ToString()
        {
            return StringBuilderUtility.Append(path, "\n", FormatLines());
        }
    }
}