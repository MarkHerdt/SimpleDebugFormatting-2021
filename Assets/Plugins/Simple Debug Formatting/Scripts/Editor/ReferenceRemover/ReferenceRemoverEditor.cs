using System;
using UnityEditor;
using UnityEngine;

namespace SimpleDebugFormatting.ReferenceRemover
{
    /// <summary>
    /// Custom Inspector-layout for the "ReferenceRemover"-class
    /// </summary>
    [CustomEditor(typeof(ReferenceRemover))]
    internal class ReferenceRemoverEditor : Editor
    {
        #region Privates
            /// <summary>
            /// Reference to the "ReferenceRemover"-Script
            /// </summary>
            private ReferenceRemover referenceRemover;
            /// <summary>
            /// Number of C#-Files in this Unity Project
            /// </summary>
            private GUIObject filesInProject = new GUIObject("<b>C# Files in Project:</b>\n", 0, 0, GUIObject.GUI.Label);
            /// <summary>
            /// C#-Files that have been read through
            /// </summary>
            private GUIObject readFiles = new GUIObject("\n<b>Reading Files:</b>", 0, 0, GUIObject.GUI.ProgressBar);
            /// <summary>
            /// C#-Files that have a reference to this Plugin
            /// </summary>
            private GUIObject filesWithReferences = new GUIObject("\n<b>Files with References:</b>\n", 0, 0, GUIObject.GUI.Label);
            /// <summary>
            /// C#-Files with references to this Plugin that have been read through
            /// </summary>
            private GUIObject readFilesWithReferences = new GUIObject("\n<b>Getting all References:</b>", 0, 0, GUIObject.GUI.ProgressBar);
            /// <summary>
            /// Count of all references to this Plugin
            /// </summary>
            private GUIObject totalReferences = new GUIObject("\n<b>Total References:</b>\n", 0, 0, GUIObject.GUI.Label);
            /// <summary>
            /// Count of how many lines with references in them have been checked
            /// </summary>
            private GUIObject linesChecked = new GUIObject("\n<b>Getting line indices:</b>", 0, 0, GUIObject.GUI.ProgressBar);
        #endregion

        private void OnEnable()
        {
            referenceRemover = (ReferenceRemover)target;

            EditorApplication.update += Update;
            UpdateValues();
        }
        
        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }
        
        public override void OnInspectorGUI()
        {
            Box(new [] { filesInProject, readFiles, filesWithReferences, readFilesWithReferences, totalReferences, linesChecked }, () =>
            {
                GUI.enabled = !ReferenceRemover.isSearching;
                if (GUILayout.Button("Search Plugin References"))
                    referenceRemover.SearchPluginReferences();
                GUI.enabled = true;
                if (GUILayout.Button("Cancel/Reset"))
                    referenceRemover.Cancel();
            });
        }

        /// <summary>
        /// Updates the Inspector
        /// </summary>
        private void Update()
        {
            if (ReferenceRemover.isSearching)
                UpdateValues();
        }
        
        /// <summary>
        /// Updates the values shown in the Inspector
        /// </summary>
        private void UpdateValues()
        {
            filesInProject.value = ReferenceRemover.filesInProject;
            
            readFiles.value = ReferenceRemover.readFiles;
            readFiles.maxValue = filesInProject.value;

            filesWithReferences.value = ReferenceRemover.filesWithReferences;

            readFilesWithReferences.value = ReferenceRemover.readFilesWithReferences;
            readFilesWithReferences.maxValue = filesWithReferences.value;

            totalReferences.value = ReferenceRemover.totalReferences;

            linesChecked.value = ReferenceRemover.linesChecked;
            linesChecked.maxValue = totalReferences.value;
            
            Repaint();
        }
        
        /// <summary>
        /// Creates a "HelpBox" in the Inspector
        /// </summary>
        /// <param name="_GUIObjects">The Objects to display</param>
        /// <param name="_Action">Action to perform in this Box</param>
        private void Box(GUIObject[] _GUIObjects, Action _Action)
        {
            GUILayout.BeginVertical("HelpBox");
                GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                        GUILayout.BeginVertical("HelpBox");

                            foreach (var _object in _GUIObjects)
                            {
                                if (_object.Gui == GUIObject.GUI.Label)
                                    _object.DrawLabel();
                                else
                                    _object.DrawProgressBar();
                            }
                            
                        GUILayout.EndVertical();

                        _Action.Invoke();
                        
                    GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        
        /// <summary>
        /// The GUIObject to display in the Inspector
        /// </summary>
        private struct GUIObject
        {
            /// <summary>
            /// What GUIObject to display in the Inspector
            /// </summary>
            internal enum GUI
            {
                Label,
                ProgressBar
            }
            
            #region Internals
                /// <summary>
                /// The value to display
                /// </summary>
                internal int value;
                /// <summary>
                /// For the ProgressBar percentage
                /// </summary>
                internal int maxValue;
            #endregion

            #region Privates
                /// <summary>
                /// Color for the InspectorText
                /// </summary>
                private string textColor;
            #endregion
            
            #region Properties
                /// <summary>
                /// Is displayed before the value
                /// </summary>
                private string Description { get; }
                /// <summary>
                /// What GUIObject to display in the Inspector
                /// </summary>
                internal GUI Gui { get; }
            #endregion
            
            /// <param name="_Description">Is displayed before the value</param>
            /// <param name="_Value">The value to display</param>
            /// <param name="_MaxValue">For the ProgressBar percentage</param>
            /// <param name="_GuiAction">The object to display in the Inspector</param>
            internal GUIObject(string _Description, int _Value, int _MaxValue, GUI _Gui)
            {
                Description = _Description;
                value = _Value;
                maxValue = _MaxValue;
                Gui = _Gui;
                
                textColor = string.Empty;
            }

            /// <summary>
            /// Draws a Label in the Inspector
            /// </summary>
            internal void DrawLabel()
            {
                textColor = EditorGUIUtility.isProSkin ? ColorCodes.SILVER_HEX : ColorCodes.BLACK_HEX;
                GUILayout.Label(string.Concat(textColor, Description, "<i>", value.ToString(), "</i>", "</color>"), new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 12, wordWrap = true });
            }

            /// <summary>
            /// Draws a ProgressBar in the Inspector
            /// </summary>
            internal void DrawProgressBar()
            {
                textColor = EditorGUIUtility.isProSkin ? ColorCodes.SILVER_HEX : ColorCodes.BLACK_HEX;
                GUILayout.Label(string.Concat(textColor, Description, "</color>"), new GUIStyle { richText = true, alignment = TextAnchor.MiddleCenter, fontSize = 12, wordWrap = true });
                
                var _width = GUILayoutUtility.GetLastRect().width > 375 ? 375 : GUILayoutUtility.GetLastRect().width;
                var _rect = new Rect(EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight)) { position = new Vector2((Screen.width * .5f) - (_width * .5f) + 6.25f, GUILayoutUtility.GetLastRect().position.y), width = _width};
                EditorGUI.ProgressBar(_rect, (float)value / (float)maxValue, value.ToString());
            }
        }
    }
}