using System;
using UnityEditor;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Custom Inspector-layout for the "Examples"-class
    /// </summary>
    [CustomEditor(typeof(Examples))]
    internal class ExamplesEditor : Editor
    {
        #region Inspector Fields
            /// <summary>
            /// Displays the text in a DebugLog
            /// </summary>
            private SerializedProperty log;
            /// <summary>
            /// Displays the text in a DebugLogWarning
            /// </summary>
            private SerializedProperty warning;
            /// <summary>
            /// Displays the text in a DebugLogError
            /// </summary>
            private SerializedProperty error;
            /// <summary>
            /// Displays the text in a DebugLogException
            /// </summary>
            private SerializedProperty exception;
            /// <summary>
            /// Displays the text in bold
            /// </summary>
            private SerializedProperty bold;
            /// <summary>
            /// Displays the text in italic
            /// </summary>
            private SerializedProperty italic;
            /// <summary>
            /// Plugin color
            /// </summary>
            private SerializedProperty colors;
            /// <summary>
            /// Unity's color
            /// </summary>
            private SerializedProperty color;
            /// <summary>
            /// Unity's Color32
            /// </summary>
            private SerializedProperty color32;
            /// <summary>
            /// Value of the currently selected color
            /// </summary>
            private SerializedProperty colorValue;
            /// <summary>
            /// Size to display the text in
            /// </summary>
            private SerializedProperty size;
            /// <summary>
            /// Displays a List in the console
            /// </summary>
            private SerializedProperty list;
            /// <summary>
            /// Displays a Dictionary in the console
            /// </summary>
            private SerializedProperty dictionary;
            /// <summary>
            /// Displays a JaggedArray in the console
            /// </summary>
            private SerializedProperty jaggedArray;
            /// <summary>
            /// Displays a MultidimensionalArray in the console
            /// </summary>
            private SerializedProperty multiDArray;
            /// <summary>
            /// Displays every Message in a new line
            /// </summary>
            private SerializedProperty newLine;
            /// <summary>
            /// Displays every Message in a new log
            /// </summary>
            private SerializedProperty newLog;
            /// <summary>
            /// Displays the index of the Message
            /// </summary>
            private SerializedProperty index;
        #endregion
        
        #region Privates
            /// <summary>
            /// Reference to the "Examples"-Class
            /// </summary>
            private Examples examples;
            /// <summary>
            /// Width of a ToggleButton
            /// </summary>
            private const float BUTTON_WIDTH = 100;
            /// <summary>
            /// The currently active LogType
            /// </summary>
            private string logType;
            /// <summary>
            /// Description text for the TextStyle
            /// </summary>
            private const string TEXT_STYLE = "<size=13>Text style</size>\n\n" + "Displays the text in <i>bold</i>, <i>italic</i> or <i>both</i>";
            /// <summary>
            /// The currently set TextStyle
            /// </summary>
            private string textStyle = ");";
            /// <summary>
            /// Description text for the TextColor
            /// </summary>
            private const string TEXT_COLOR = "<size=13>Color</size>\n\n" + "Displays the text in a different <i>color</i>";
            /// <summary>
            /// The currently set TextColor
            /// </summary>
            private string textColor = ");";
            /// <summary>
            /// Description text for the TextSize
            /// </summary>
            private const string TEXT_SIZE = "<size=13>Size</size>\n\n" + "Displays the text in a different <i>size</i>";
            /// <summary>
            /// The currently set TextSize
            /// </summary>
            private string textSize = ");";
            /// <summary>
            /// Description text for Collections 
            /// </summary>
            private const string COLLECTIONS = "<size=13>Collections</size>\n\n" + "Displays all elements of a <i>collection</i>";
            /// <summary>
            /// The collections to display
            /// </summary>
            private string collections = ");";
        #endregion
        
        private void OnEnable()
        {
            examples = (Examples)target;

            log = serializedObject.FindProperty("log");
            warning = serializedObject.FindProperty("logWarning");
            error = serializedObject.FindProperty("logError");
            exception = serializedObject.FindProperty("logException");
            bold = serializedObject.FindProperty("bold");
            italic = serializedObject.FindProperty("italic");
            colors = serializedObject.FindProperty("colors");
            color = serializedObject.FindProperty("color");
            color32 = serializedObject.FindProperty("color32");
            size = serializedObject.FindProperty("size");
            list = serializedObject.FindProperty("list");
            dictionary = serializedObject.FindProperty("dictionary");
            jaggedArray = serializedObject.FindProperty("jaggedArray");
            multiDArray = serializedObject.FindProperty("multiDArray");
            newLine = serializedObject.FindProperty("newLine");
            newLog = serializedObject.FindProperty("newLog");
            index = serializedObject.FindProperty("index");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // DebugLogs
            ToggleButtons(true, new ToggleButton(log, "Log"), new ToggleButton(warning, "Warning"), new ToggleButton(error, "Error"), new ToggleButton(exception, "Exception"));
            logType = GetLogType();
            
            // Text style Box
            Box(StringBuilderUtility.Append(TEXT_STYLE, "\n", logType, textStyle), () =>
            {
                ToggleButtons(false, new ToggleButton(bold, "Bold"), new ToggleButton(italic, "Italic"));
                textStyle = GetTextStyle();
                DebugButton("Print Text", examples.TextStyle); 
            });

            // Color Box
            Box(StringBuilderUtility.Append(TEXT_COLOR, "\n", logType, textColor), () =>
            {
                ToggleButtons(true, new ToggleButton(colors, "Colors"), new ToggleButton(color, "Color"), new ToggleButton(color32, "Color32"));

                if (colors.boolValue)
                {
                    colorValue = serializedObject.FindProperty("colorsValue");
                    EditorGUILayout.PropertyField(colorValue, new GUIContent("Plugin Color Enum"));
                }
                else if (color.boolValue)
                {
                    colorValue = serializedObject.FindProperty("colorValue");
                    EditorGUILayout.PropertyField(colorValue, new GUIContent("Unity Color"));
                }
                else if (color32.boolValue)
                {
                    colorValue = serializedObject.FindProperty("color32Value");
                    EditorGUILayout.PropertyField(colorValue, new GUIContent("Unity Color32"));
                }

                textColor = GetTextColor();
                DebugButton("Print Text", examples.TextColor);
            });
            
            // Size Box
            Box(StringBuilderUtility.Append(TEXT_SIZE, "\n", logType, textSize), () =>
            {
                EditorGUILayout.PropertyField(size, new GUIContent("Size"));
                if (size.intValue < 0) size.intValue = 0;
                if (size.longValue < 0) size.intValue = 0;
                textSize = GetTextSize();
                DebugButton("Print Text", examples.TextSize);
            });
            
            // Collections
            Box(StringBuilderUtility.Append(COLLECTIONS, "\n", logType, collections), () =>
            {
                ToggleButtons(true, new ToggleButton(list, "List"), new ToggleButton(dictionary, "Dictionary"), new ToggleButton(jaggedArray , "Jagged Array"), new ToggleButton(multiDArray, "Multi-D Array"));
                ToggleButtons(false, new ToggleButton(newLine, "New Line"), new ToggleButton(newLog, "New Log"), new ToggleButton(index, "Index"));
                collections = GetCollection();
                DebugButton("Print Text", examples.Collections);
            });
            
            serializedObject.ApplyModifiedProperties();
        }
        
        /// <summary>
        /// Creates a toggle button for each SerializedProperty
        /// </summary>
        /// <param name="_OneActive">If true, only one button can be active at a time</param>
        /// <param name="_Buttons">Array of button to create</param>
        private static void ToggleButtons(bool _OneActive, params ToggleButton[] _Buttons)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(Screen.width / 2f - (BUTTON_WIDTH * (_Buttons.Length / 2f)) - 12.5f);
            for (var i = 0; i < _Buttons.Length; i++)
            {
                _Buttons[i].serializedProperty.boolValue = GUILayout.Toggle(_Buttons[i].serializedProperty.boolValue, _Buttons[i].name, new GUIStyle(EditorStyles.miniButtonMid) { richText = true, alignment = TextAnchor.MiddleCenter,  fixedWidth = BUTTON_WIDTH });
                // Disables all other Buttons in the Array
                if (_OneActive && _Buttons[i].serializedProperty.boolValue)
                    foreach (var _button in _Buttons)
                        _button.serializedProperty.boolValue = _button.serializedProperty == _Buttons[i].serializedProperty;
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Creates a "HelpBox" in the Inspector
        /// </summary>
        /// <param name="_LabelText">Description text for this Box</param>
        /// <param name="_Action">Action to perform in this Box</param>
        private static void Box(string _LabelText, Action _Action)
        {
            GUILayout.BeginVertical("HelpBox");
                GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                        GUILayout.Label(StringBuilderUtility.Append("\n<b>", _LabelText, "</b>\n"), new GUIStyle(EditorStyles.helpBox) {  richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 11});
                        
                        _Action.Invoke();
                        
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        
        /// <summary>
        /// Creates a button that prints an example text with the style settings of the box it's in to the console
        /// </summary>
        /// <param name="_ButtonName">The name of the button</param>
        /// <param name="_Method">The Method that is invoked when the button is pressed</param>
        private void DebugButton(string _ButtonName, Action _Method)
        {
            if (!GUILayout.Button(_ButtonName)) return;

                if (!log.boolValue && !warning.boolValue && !error.boolValue && !exception.boolValue)
                {
                    log.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                }

                _Method.Invoke();
        }

        /// <summary>
        /// Returns the currently active LogType for the description text
        /// </summary>
        private string GetLogType()
        {
            const string _CLASS = ColorCodes.CLASS_HEX + "D</color>.";
            string _logType;
            const string _METHOD = "(";

            if (log.boolValue)
                _logType = "Log";
            else if (warning.boolValue)
                _logType = "Warning";
            else if (error.boolValue)
                _logType = "Error";
            else if (exception.boolValue)
                _logType = "Exception";
            else
                _logType = "Log";

            return StringBuilderUtility.Append(_CLASS, ColorCodes.METHOD_HEX, _logType, "</color>", _METHOD);
        }

        /// <summary>
        /// Returns the currently set TextStyle for the description text
        /// </summary>
        private string GetTextStyle()
        {
            const string _ENUM = ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>, " + ColorCodes.ENUM_HEX + "F</color>.";

            if (bold.boolValue && !italic.boolValue)
                return StringBuilderUtility.Append(_ENUM, "B);");
            if (italic.boolValue && !bold.boolValue)
                return StringBuilderUtility.Append(_ENUM, "I);");
            if (bold.boolValue && italic.boolValue)
                return StringBuilderUtility.Append(_ENUM, "BI);");
            
            return ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>);";
        }

        /// <summary>
        /// Returns the currently set TextColor for the description text
        /// </summary>
        private string GetTextColor()
        {
            const string _ENUM = ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>, " + ColorCodes.ENUM_HEX + "C</color>.";
            const string _COLOR = ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>, " + ColorCodes.KEYWORD_HEX + "new</color> " + ColorCodes.STRUCT_HEX + "Color</color>(";
            const string _COLOR_32 = ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>, " + ColorCodes.KEYWORD_HEX + "new</color> " + ColorCodes.STRUCT_HEX + "Color32</color>(";

            if (colors.boolValue && !color.boolValue && !color32.boolValue)
                return StringBuilderUtility.Append(_ENUM, ((C)colorValue.enumValueIndex).ToString(), ");");
            if (color.boolValue && !colors.boolValue && !color32.boolValue)
                return StringBuilderUtility.Append(_COLOR, ColorCodes.NUMBER_HEX, colorValue.colorValue.r.ToString("0.##"), "f</color>", ", ", 
                                                           ColorCodes.NUMBER_HEX, colorValue.colorValue.g.ToString("0.##"), "f</color>", ", ", 
                                                           ColorCodes.NUMBER_HEX, colorValue.colorValue.b.ToString("0.##"), "f</color>", ", ", 
                                                           ColorCodes.NUMBER_HEX, colorValue.colorValue.a.ToString("0.##"), "f</color>", "));");
            if (color32.boolValue && !colors.boolValue && !color.boolValue)
                return StringBuilderUtility.Append(_COLOR_32, ColorCodes.NUMBER_HEX, (colorValue.colorValue.r * 255).ToString("0"), "</color>", ", ", 
                                                              ColorCodes.NUMBER_HEX, (colorValue.colorValue.g * 255).ToString("0"), "</color>", ", ", 
                                                              ColorCodes.NUMBER_HEX, (colorValue.colorValue.b * 255).ToString("0"), "</color>", ", ", 
                                                              ColorCodes.NUMBER_HEX, (colorValue.colorValue.a * 255).ToString("0"), "</color>", "));");
            
            return ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>);";
        }

        /// <summary>
        /// Returns the currently set TextSize for the description text
        /// </summary>
        private string GetTextSize()
        {
            return StringBuilderUtility.Append(ColorCodes.STRING_HEX + "\"Lorem ipsum\"</color>, ", ColorCodes.NUMBER_HEX, size.longValue.ToString(), "</color>", ");");
        }

        /// <summary>
        /// Returns the currently selected collection and its format settings
        /// </summary>
        private string GetCollection()
        {
            var _collection = string.Empty;
            const string _FORMAT = ColorCodes.ENUM_HEX + "F</color>.";
            var _format = string.Empty;

            if (list.boolValue && !dictionary.boolValue && !jaggedArray.boolValue && !multiDArray.boolValue)
                _collection = "list";
            if (dictionary.boolValue && !list.boolValue && !jaggedArray.boolValue && !multiDArray.boolValue)
                _collection = "dictionary";
            if (jaggedArray.boolValue && !list.boolValue && !dictionary.boolValue && !multiDArray.boolValue)
                _collection = "jaggedArray";
            if (multiDArray.boolValue && !list.boolValue && !dictionary.boolValue && !jaggedArray.boolValue)
                _collection = "multiDArray";
            if (!list.boolValue && !dictionary.boolValue && !jaggedArray.boolValue && !multiDArray.boolValue)
                _collection = ColorCodes.KEYWORD_HEX +  "null</color>";

            if (newLine.boolValue && !newLog.boolValue && !index.boolValue)
                _format = StringBuilderUtility.Append(", ", _FORMAT, "NL");
            if (newLog.boolValue && !newLine.boolValue && !index.boolValue)
                _format = StringBuilderUtility.Append(", ", _FORMAT, "LOG");
            if (newLine.boolValue && newLog.boolValue && !index.boolValue)
                _format = StringBuilderUtility.Append(", ", _FORMAT, "LOG_NL");
            if (index.boolValue && !newLine.boolValue && !newLog.boolValue)
                _format = StringBuilderUtility.Append(_format, ", ", _FORMAT, "INDEX");
            if (index.boolValue && newLine.boolValue && !newLog.boolValue)
                _format = StringBuilderUtility.Append(_format, ", ", _FORMAT, "INDEX_NL");
            if (index.boolValue && !newLine.boolValue && newLog.boolValue)
                _format = StringBuilderUtility.Append(_format, ", ", _FORMAT, "INDEX_LOG");
            if (index.boolValue && newLine.boolValue && newLog.boolValue)
                _format = StringBuilderUtility.Append(_format, ", ", _FORMAT, "INDEX_LOG_NL");
            
            return StringBuilderUtility.Append(_collection, _format, ");");
        }
        
        /// <summary>
        /// Holds the toggle state of the button and its name
        /// </summary>
        private struct ToggleButton
        {
            #region Internals
                /// <summary>
                /// Holds the toggle state of the button
                /// </summary>
                internal readonly SerializedProperty serializedProperty;
                /// <summary>
                /// Name of the button
                /// </summary>
                internal readonly string name;
            #endregion

            /// <summary>
            /// Creates a new button
            /// </summary>
            /// <param name="_SerializedProperty">Holds the toggle state of the button</param>
            /// <param name="_Name">Name of the button</param>
            internal ToggleButton(SerializedProperty _SerializedProperty, string _Name)
            {
                serializedProperty = _SerializedProperty;
                name = _Name;
            }
        }
    }
}