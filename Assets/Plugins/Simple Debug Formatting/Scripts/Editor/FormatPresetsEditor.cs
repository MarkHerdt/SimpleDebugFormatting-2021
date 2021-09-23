using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Custom Editor for the "FormatPreset"-Class
    /// </summary>
    [CustomEditor(typeof(FormatPresets))]
    internal class FormatPresetsEditor : Editor
    {
        #region Privates
            /// <summary>
            /// Reference to the "FormatPresets"-Class
            /// </summary>
            private FormatPresets formatPresets;
            /// <summary>
            /// List of all templates in the "FormatPresets"-Class
            /// </summary>
            private SerializedProperty presets;
            /// <summary>
            /// List that is shown in the Inspector
            /// </summary>
            private ReorderableList list;
            /// <summary>
            /// ID of the last focused control
            /// </summary>
            private int lastFocusID;
            /// <summary>
            /// Style-settings for the "GUILayout.Box()"
            /// </summary>
            private GUIStyle infoBoxStyle;
            /// <summary>
            /// Style-settings for the PresetName-Label
            /// </summary>
            private GUIStyle labelStyle;
            /// <summary>
            /// Content will be displayed in the InfoBox in the Inspector
            /// </summary>
            private string infoBoxText;
        #endregion

        private void OnEnable()
        {
            formatPresets = (FormatPresets)target;
            presets = serializedObject.FindProperty("presets");
                
            // Initialises the ReorderableList      
            list = new ReorderableList(serializedObject, presets, true, true, true, true)
            {
                drawHeaderCallback = delegate(Rect _Rect) { EditorGUI.LabelField(_Rect, "Presets"); }, drawElementCallback = DrawListItems, onAddCallback = ElementAdded, onRemoveCallback = ElementRemoved, onReorderCallback = ElementReordered
            };
                
            formatPresets.errorText = new List<string> { "" };
            
            formatPresets.MatchWithEnum();
            formatPresets.CheckNames();
            infoBoxText = formatPresets.DisplayMessages();
        }
        
        public override void OnInspectorGUI()
        {
            Initialize();
            
            GeneratePresetsButton();

            // MessageBox
            GUILayout.Label(infoBoxText, infoBoxStyle);
            
            // List
            serializedObject.Update();
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
        
        /// <summary>
        /// Initializes values for objects
        /// </summary>
        private void Initialize()
        {
            infoBoxStyle = new GUIStyle(EditorStyles.helpBox) {  richText = true, alignment = TextAnchor.MiddleCenter };
            labelStyle = new GUIStyle { richText = true };
        }

        /// <summary>
        /// Writes the Preset names to the "Presets"-Enum
        /// </summary>
        private void GeneratePresetsButton()
        {
            if (!GUILayout.Button("Generate Presets")) return;
            
                formatPresets.GeneratePresets(out infoBoxText);
        }
        
        /// <summary>
        /// Draws the entries for the List Element
        /// </summary>
        /// <param name="_Rect">Position of Element</param>
        /// <param name="_Index">At what index the Element is inside the List</param>
        /// <param name="_IsActive">Whether this Element is currently active</param>
        /// <param name="_IsFocused">Whether this Element is currently focused</param>
        private void DrawListItems(Rect _Rect, int _Index, bool _IsActive, bool _IsFocused)
        {
                // The element in the list
                var _element = list.serializedProperty.GetArrayElementAtIndex(_Index);

                // When any focused Field changes in the Inspector
                if (lastFocusID != GUIUtility.keyboardControl)
                {
                    formatPresets.MatchWithEnum();
                    formatPresets.CheckNames();
                    infoBoxText = formatPresets.DisplayMessages();
                    Repaint();
                }
                
                lastFocusID = GUIUtility.keyboardControl;
                
                // Name
                EditorGUI.LabelField(new Rect(_Rect.x, _Rect.y, 50, EditorGUIUtility.singleLineHeight),
                    new GUIContent(formatPresets.errors[_Index].AnyErrors ? StringBuilderUtility.Append(ColorCodes.ERROR_HEX, "Name</color>") : EditorGUIUtility.isProSkin ? StringBuilderUtility.Append(ColorCodes.SILVER_HEX, "Name</color>") : StringBuilderUtility.Append(ColorCodes.BLACK_HEX, "Name</color>"), "Choose a unique name with which to call this Preset from the Code"), labelStyle);
                EditorGUI.PropertyField(
                    position: new Rect(_Rect.x + 40, _Rect.y, 100, EditorGUIUtility.singleLineHeight),
                    property: _element.FindPropertyRelative("name"),
                    label:    GUIContent.none
                );

                // Color
                EditorGUI.LabelField(new Rect(_Rect.x + 150, _Rect.y, 50, EditorGUIUtility.singleLineHeight),
                    new GUIContent
                    {
                        text = "Color",
                        tooltip = "The color, the Console text which is formatted with this Preset, will have"
                    });
                EditorGUI.PropertyField(
                    position: new Rect(_Rect.x + 190, _Rect.y, 100, EditorGUIUtility.singleLineHeight),
                    property: _element.FindPropertyRelative("color"),
                    label:    GUIContent.none
                );

                // Format
                EditorGUI.LabelField(new Rect(_Rect.x + 300, _Rect.y, 50, EditorGUIUtility.singleLineHeight),
                    new GUIContent
                    {
                        text = "Format",
                        tooltip = "The formatting style, the Console text which is formatted with this Preset, will have\nN=No formatting\nB=Bolt\nI=Italic\nBI=Bold/Italic\nLOG=Will be displayed as a new DebugLog, in the Console"
                    });
                EditorGUI.PropertyField(
                    position: new Rect(_Rect.x + 350, _Rect.y, 125, EditorGUIUtility.singleLineHeight),
                    property: _element.FindPropertyRelative("format"),
                    label:    GUIContent.none
                );

                // Size
                EditorGUI.LabelField(new Rect(_Rect.x + 485, _Rect.y, 50, EditorGUIUtility.singleLineHeight),
                    new GUIContent
                    {
                        text = "Size",
                        tooltip = "The size in Pixel, the Console text which is formatted with this Preset, will have (12 = default)"
                    });
                EditorGUI.PropertyField(
                    position: new Rect(_Rect.x + 525, _Rect.y, 25, EditorGUIUtility.singleLineHeight),
                    property: _element.FindPropertyRelative("size"),
                    label:    GUIContent.none
                );
        }
        
        /// <summary>
        /// Is called when a new Element is added to the List
        /// </summary>
        /// <param name="_List">The List that the Element was added to</param>
        private void ElementAdded(ReorderableList _List)
        {
            presets.arraySize++;
            
            var _element = presets.GetArrayElementAtIndex(presets.arraySize - 1);
            
            _element.FindPropertyRelative("name").stringValue = StringBuilderUtility.Append("Preset", presets.arraySize.ToString());
            _element.FindPropertyRelative("color").colorValue = EditorGUIUtility.isProSkin ? ColorCodes.WHITE_32 : ColorCodes.BLACK_32;
            _element.FindPropertyRelative("format").intValue = 0;
            _element.FindPropertyRelative("size").intValue = 12;
            
            serializedObject.ApplyModifiedProperties();
            
            formatPresets.MatchWithEnum();
            formatPresets.CheckNames();
            infoBoxText = formatPresets.DisplayMessages();
        }

        /// <summary>
        /// Is called when an Element is removed from the List
        /// </summary>
        /// <param name="_List">The List that the Element was removed from</param>
        private void ElementRemoved(ReorderableList _List)
        {
            if (list.HasKeyboardControl())
            {
                var _focusedElement = list.index;
                
                presets.DeleteArrayElementAtIndex(_focusedElement);
                serializedObject.ApplyModifiedProperties();

                // Sets the new focus
                if (presets.arraySize > 0)
                {
                    // Focus stays the same
                    if (_focusedElement < presets.arraySize)
                        list.index = _focusedElement;
                    // Last Element was deleted
                    else if (_focusedElement == presets.arraySize)
                        list.index = _focusedElement - 1; 
                }
            }
            else
            {
                presets.arraySize--;
            }

            formatPresets.MatchWithEnum();
            formatPresets.CheckNames();
            infoBoxText = formatPresets.DisplayMessages();
        }

        /// <summary>
        /// Is called after an Element has been dragged to another index
        /// </summary>
        /// <param name="_List">The List that the Element has been dragged in</param>
        private void ElementReordered(ReorderableList _List)
        {
            formatPresets.MatchWithEnum();
            formatPresets.CheckNames();
            infoBoxText = formatPresets.DisplayMessages();
        }
    }
}