using System;
using UnityEditor;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Custom Inspector-layout for the "Settings"-class
    /// </summary>
    [CustomEditor(typeof(Settings))]
    internal class SettingsEditor : Editor
    {
        #region Inspector Fields
            /// <summary>
            /// True = Also displays the Parents Type name, when displaying the Collection Type | False = Only displays the Type of the Object
            /// </summary>
            private SerializedProperty showParentType;
            /// <summary>
            /// Whether to add an empty line between a Collection and its entries
            /// </summary>
            private SerializedProperty newLineAfterCollection;
            /// <summary>
            /// Won't format the Messages and only display them with the default Debug.Logs() (Editor only)
            /// </summary>
            private SerializedProperty disableFormattingInEditor;
            /// <summary>
            /// Won't format the Messages and only display them with the default Debug.Logs() (Build only)
            /// </summary>
            private SerializedProperty disableFormattingInBuild;
            /// <summary>
            /// Disables all D.Logs() called with this Plugin (Editor only)
            /// </summary>
            private SerializedProperty disableLogsInEditor;
            /// <summary>
            /// Disables all D.Logs() called with this Plugin (Build only)
            /// </summary>
            private SerializedProperty disableLogsInBuild;
        #endregion

        #region Privates
            /// <summary>
            /// Color for the InspectorText
            /// </summary>
            private static string textColor;
            /// <summary>
            /// Width of a ToggleButton
            /// </summary>
            private const float BUTTON_WIDTH = 100;
            /// <summary>
            /// Image for ParentType
            /// </summary>
            private Texture parentType;
            /// <summary>
            /// Image for ObjectType
            /// </summary>
            private Texture objectType;
            /// <summary>
            /// Image that is shown above the "showParentClass" "SerializedProperty"
            /// </summary>
            private Texture typeNameImage;
            /// <summary>
            /// Image for NewLine
            /// </summary>
            private Texture newLine;
            /// <summary>
            /// Image for NoNewLine
            /// </summary>
            private Texture noNewLine;
            /// <summary>
            /// Image that is shown above the "newLineImage" "SerializedProperty"
            /// </summary>
            private Texture newLineImage;
        #endregion
        
        private void OnEnable()
        {
            textColor = EditorGUIUtility.isProSkin ? ColorCodes.SILVER_HEX : ColorCodes.BLACK_HEX;
            
            parentType = EditorGUIUtility.isProSkin ? (Texture)Resources.Load("ParentType_Dark_SDF_IMG", typeof(Texture)) : (Texture)Resources.Load("ParentType_Light_SDF_IMG", typeof(Texture));
            objectType = EditorGUIUtility.isProSkin ? (Texture)Resources.Load("ObjectType_Dark_SDF_IMG", typeof(Texture)) : (Texture)Resources.Load("ObjectType_Light_SDF_IMG", typeof(Texture));
            newLine = EditorGUIUtility.isProSkin ? (Texture)Resources.Load("NewLine_Dark_SDF_IMG", typeof(Texture)) : (Texture)Resources.Load("NewLine_Light_SDF_IMG", typeof(Texture));
            noNewLine = EditorGUIUtility.isProSkin ? (Texture)Resources.Load("NoNewLine_Dark_SDF_IMG", typeof(Texture)) : (Texture)Resources.Load("NoNewLine_Light_SDF_IMG", typeof(Texture));
            
            showParentType = serializedObject.FindProperty("showParentType");
            newLineAfterCollection = serializedObject.FindProperty("newLineAfterCollection");
            disableFormattingInEditor = serializedObject.FindProperty("disableFormattingInEditor");
            disableFormattingInBuild = serializedObject.FindProperty("disableFormattingInBuild");
            disableLogsInEditor = serializedObject.FindProperty("disableLogsInEditor");
            disableLogsInBuild = serializedObject.FindProperty("disableLogsInBuild");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Collection Types
            typeNameImage = showParentType.boolValue ? parentType : objectType;
            Box("How Collection Types and Types without an .ToString()-overload are displayed in the Console", typeNameImage, () =>
            {
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(showParentType);
                if (EditorGUI.EndChangeCheck())
                    TypeExtension.ClearCachedNames();    
            });
            
            // Collection NewLine
            newLineImage = newLineAfterCollection.boolValue ? newLine : noNewLine;
            Box("Whether to add an empty line between a Collection and its entries", newLineImage, () =>
            {
                EditorGUILayout.PropertyField(newLineAfterCollection); 
            });
            
            // Disable Formatting
            Box("If selected, the Plugin won't format the Messages anymore and only display them with Unity's default Debug.Log()-Methods", () =>
            {
                EditorGUILayout.LabelField(StringBuilderUtility.Append("<b>", textColor, "Disable formatting", "</color></b>"), new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 11, wordWrap = true });
                ToggleButtons(false, new ToggleButton(disableFormattingInEditor, "Editor"), new ToggleButton(disableFormattingInBuild, "Build"));
            });
            
            // Disable Logs
            Box("If selected, disables all D.Logs(), called with this Plugin.\n<size=10>(Has no effect on Debug.Logs() that are called with Unity's Debug-class)\n(Exceptions will still be thrown!)</size>", () =>
            {
                EditorGUILayout.LabelField(StringBuilderUtility.Append("<b>", textColor, "Disable Logs", "</color></b>"), new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 11, wordWrap = true });
                ToggleButtons(false, new ToggleButton(disableLogsInEditor, "Editor"), new ToggleButton(disableLogsInBuild, "Build"));
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
                        GUILayout.BeginVertical("HelpBox");
                            GUILayout.Label(StringBuilderUtility.Append("\n<b>", textColor, _LabelText, "</color></b>\n"), new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 11, wordWrap = true });
                        GUILayout.EndVertical();

                        _Action.Invoke();
                        
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        
        /// <summary>
        /// Creates a "HelpBox" in the Inspector
        /// </summary>
        /// <param name="_LabelText">Description text for this Box</param>
        /// <param name="_Image">The image to show</param>
        /// <param name="_Action">Action to perform in this Box</param>
        private static void Box(string _LabelText, Texture _Image, Action _Action)
        {
            GUILayout.BeginVertical("HelpBox");
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.BeginVertical("HelpBox");
            GUILayout.Label(StringBuilderUtility.Append("\n<b>", textColor, _LabelText, "</color></b>\n"), new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 11, wordWrap = true });
            GUILayout.Label(new GUIContent { image = _Image }, new GUIStyle { alignment = TextAnchor.MiddleCenter });
            GUILayout.Label("");
            GUILayout.EndVertical();

            _Action.Invoke();
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
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