using System.Collections.Generic;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Class to print different examples for the Simple Debug Formatting Plugin in the Console
    /// </summary>
    public class Examples : MonoBehaviour
    {
        #region Inspector Fields
            [Tooltip("Displays the text in a DebugLog")]
            [SerializeField] private bool log;
            [Tooltip("Displays the text in a DebugLogWarning")]
            [SerializeField] private bool logWarning;
            [Tooltip("Displays the text in a DebugLogError")]
            [SerializeField] private bool logError;
            [Tooltip("Displays the text in a DebugLogException")]
            [SerializeField] private bool logException;
            [Tooltip("Displays the text in bold")]
            [SerializeField] private bool bold;
            [Tooltip("Displays the text in italic")]
            [SerializeField] private bool italic;
            [Tooltip("Plugin color")]
            [SerializeField] private bool colors;
            [Tooltip("Plugin color value")]
            [SerializeField] private C colorsValue;
            [Tooltip("Unity's color")]
            [SerializeField] private bool color;
            [Tooltip("Unity's color value")]
            [SerializeField] private Color colorValue;
            [Tooltip("Unity's color32")]
            [SerializeField] private bool color32;
            [Tooltip("Unity's color32 value")]
            [SerializeField] private Color32 color32Value;
            [Tooltip("Size to display the text in")]
            [SerializeField] private ulong size;
            [Tooltip("Displays a List in the console")]
            [SerializeField] private bool list;
            [Tooltip("Displays a Dictionary in the console")]
            [SerializeField] private bool dictionary;
            [Tooltip("Displays a JaggedArray in the console")]
            [SerializeField] private bool jaggedArray;
            [Tooltip("Displays a MultidimensionalArray in the console")]
            [SerializeField] private bool multiDArray;
            [Tooltip("Displays every Message in a new line")]
            [SerializeField] private bool newLine;
            [Tooltip("Displays every Message in a new log")]
            [SerializeField] private bool newLog;
            [Tooltip("Displays the index of the Message")]
            [SerializeField] private bool index;
        #endregion

        #region Privates
            /// <summary>
            /// String to display in the Console
            /// </summary>
            private const string LOREM_IPSUM = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. " +
                                               "At vero eos et accusam et justo duo dolores et ea rebum. " +
                                               "Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
            /// <summary>
            /// List of string to display in the Console
            /// </summary>
            private static readonly List<string> LIST = new List<string> 
            { 
                "Lorem ", "ipsum ", "dolor ", "sit ", "amet, ", "consetetur ", "sadipscing ", "elitr, ", "sed ", "diam ", "nonumy ", "eirmod ", "tempor ", "invidunt ", "ut ", "labore ", "et ", "dolore ", "magna ", "aliquyam ", "erat, ", "sed ", "diam ", "voluptua. ",
                "At ", "vero ", "eos ", "et ", "accusam ", "et ", "justo ", "duo ", "dolores ", "et ", "ea ", "rebum. ", 
                "Stet ", "clita ", "kasd ", "gubergren, ", "no ", "sea ", "takimata ", "sanctus ", "est ", "Lorem ", "ipsum ", "dolor ", "sit ", "amet."
                                                                           
            };
            /// <summary>
            /// Dictionary to display in the Console
            /// </summary>
            private static readonly Dictionary<string, List<string>> DICTIONARY = new Dictionary<string, List<string>>
            {
                { "Key_1", new List<string> { "Lorem ", "ipsum ", "dolor ", "sit ", "amet, ", "consetetur ", "sadipscing ", "elitr, ", "sed ", "diam ", "nonumy ", "eirmod ", "tempor ", "invidunt ", "ut ", "labore ", "et ", "dolore ", "magna ", "aliquyam ", "erat, ", "sed ", "diam ", "voluptua. " } },
                { "Key_2", new List<string> { "At ", "vero ", "eos ", "et ", "accusam ", "et ", "justo ", "duo ", "dolores ", "et ", "ea ", "rebum. " } },
                { "Key_3", new List<string> { "Stet ", "clita ", "kasd ", "gubergren, ", "no ", "sea ", "takimata ", "sanctus ", "est ", "Lorem ", "ipsum ", "dolor ", "sit ", "amet." } }
            };
            /// <summary>
            /// JaggedArray to display in the console
            /// </summary>
            private static readonly string[][] JAGGED_ARRAY = 
            {
                new [] { "Lorem ", "ipsum ", "dolor ", "sit ", "amet, ", "consetetur ", "sadipscing ", "elitr, ", "sed ", "diam ", "nonumy ", "eirmod ", "tempor ", "invidunt ", "ut ", "labore ", "et ", "dolore ", "magna ", "aliquyam ", "erat, ", "sed ", "diam ", "voluptua. " },
                new [] { "At ", "vero ", "eos ", "et ", "accusam ", "et ", "justo ", "duo ", "dolores ", "et ", "ea ", "rebum. " },
                new [] { "Stet ", "clita ", "kasd ", "gubergren, ", "no ", "sea ", "takimata ", "sanctus ", "est ", "Lorem ", "ipsum ", "dolor ", "sit ", "amet." }
            };
            /// <summary>
            /// MultiDimensionalArray to display in the Console
            /// </summary>
            private static readonly string[,,] MULTI_D_ARRAY = 
            {
                {
                    { "Lorem ", "ipsum ", "dolor ", "sit ", "amet, ", "consetetur ", "sadipscing ", "elitr, ", "sed ", "diam ", "nonumy ", "eirmod ", "tempor ", "invidunt ", "ut ", "labore ", "et ", "dolore ", "magna ", "aliquyam ", "erat, ", "sed ", "diam ", "voluptua. " }
                },
                {
                    {  "", "At ", "", "vero ", "", "eos ", "", "et ", "", "accusam ", "", "et ", "justo ", "", "duo ", "", "dolores ", "", "et ", "", "ea ", "", "rebum. ", "" }
                },
                {
                    { "Stet ", "", "clita ", "", "kasd ", "", "gubergren, ", "", "no ", "", "sea ", "", "takimata ", "", "sanctus ", "", "est ", "", "Lorem ", "", "ipsum ", "dolor ", "sit ", "amet." }
                }
            };
        #endregion
        
        /// <summary>
        /// Prints the text to the Console
        /// </summary>
        /// <param name="_Message">The objects to display in the Console</param>
        private void PrintText(params object[] _Message)
        {
            if (log)
                D.Log_SDF(_Message);
            else if (logWarning)
                D.Warning_SDF(_Message);
            else if (logError)
                D.Error_SDF(_Message);
            else if (logException)
                throw D.Exception_SDF(_Message);
        }
        
        /// <summary>
        /// Displays the text in different text styles
        /// </summary>
        public void TextStyle()
        {
            var _tmpText = LOREM_IPSUM;
            
            if (bold)
                _tmpText = StringBuilderUtility.Append("<b>", _tmpText, "</b>");
            if (italic)
                _tmpText = StringBuilderUtility.Append("<i>", _tmpText, "</i>");
            
            PrintText(_tmpText);
        }

        /// <summary>
        /// Displays the text in a different color
        /// </summary>
        public void TextColor()
        {
            var _tmpText = LOREM_IPSUM;

            if (colors)
                _tmpText = StringBuilderUtility.Append("<color=", colorsValue.ToString(), ">", _tmpText, "</color>");
            else if (color)
                _tmpText = StringBuilderUtility.Append("<color=#", ColorUtility.ToHtmlStringRGBA(colorValue), ">", _tmpText, "</color>");
            else if (color32)
                _tmpText = StringBuilderUtility.Append("<color=#", ColorUtility.ToHtmlStringRGBA(color32Value), ">", _tmpText, "</color>");

            PrintText(_tmpText);
        }

        /// <summary>
        /// Displays the text in a different size
        /// </summary>
        public void TextSize()
        {
            var _tmpText = LOREM_IPSUM;

            _tmpText = StringBuilderUtility.Append("<size=", size.ToString(), ">", _tmpText, "</size>");
            
            PrintText(_tmpText);
        }

        /// <summary>
        /// Displays all elements of a Collection
        /// </summary>
        public void Collections()
        {
            object _collection = null;
            var _formats = new List<F>();

            if (list)
                _collection = LIST;
            else if (dictionary)
                _collection = DICTIONARY;
            else if (jaggedArray)
                _collection = JAGGED_ARRAY;
            else if (multiDArray)
                _collection = MULTI_D_ARRAY;

            if (newLine)
                _formats.Add(F.NL);
            if (newLog)
                _formats.Add(F.LOG);
            if (index)
                _formats.Add(F.INDEX);

            PrintText(_collection, _formats);
        }
    }
}