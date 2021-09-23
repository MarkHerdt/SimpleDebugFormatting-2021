using System;
using System.Linq;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Settings for the formatting
    /// </summary>
    //[CreateAssetMenu(menuName = "Simple Debug Formatting/Settings", fileName = "Settings_SDF_SOB")] // Commented out so it won't show up in the creation Menu
    public class Settings : ScriptableObject
    {
        #region Inspector Fields
            [Tooltip("True = Also displays the Parents Type name, when displaying the Collection Type | False = Only displays the Type of the Object")]
            [SerializeField] private bool showParentType;
            [Tooltip("Whether to add an empty line between a Collection and its entries")]
            [SerializeField] private bool newLineAfterCollection = true;
            [Tooltip("Won't format the Messages and only display them with the default Debug.Logs() (Editor only)")]
            [SerializeField] private bool disableFormattingInEditor;
            [Tooltip("Won't format the Messages and only display them with the default Debug.Logs() (Build only)")]
            [SerializeField] private bool disableFormattingInBuild;
            [Tooltip("Disables all D.Logs() called with this Plugin (Editor only)")]
            [SerializeField] private bool disableLogsInEditor;
            [Tooltip("Disables all D.Logs() called with this Plugin (Build only)")]
            [SerializeField] private bool disableLogsInBuild;
        #endregion

        #region Privates
            /// <summary>
            /// Singleton of "Settings"
            /// </summary>
            private static Settings instance;
        #endregion
        
        #region Properties
            /// <summary>
            /// Returns the Instance of the "Settings"-ScriptableObject or creates a new one
            /// </summary>
            /// <exception cref="Exception">When no ScriptableObject of Type "Settings" could be found/exception>
            private static Settings Instance
            {
                get
                {
                    #if UNITY_EDITOR
                        return LoadScriptableObjectInEditor();
                    #else
                        return LoadScriptableObjectInBuild();
                    #endif
                }
            }
            /// <summary>
            /// True = Also displays the Parents Type name, when displaying the Collection Type | False = Only displays the Type of the Object
            /// </summary>
            internal static bool ShowParentTypes { get { return Instance.showParentType; } }
            /// <summary>
            /// Whether to add an empty line between a Collection and its entries
            /// </summary>
            internal static bool NewLineAfterCollection { get { return Instance.newLineAfterCollection; } }
            /// <summary>
            /// Won't format the Messages and only display them with the default Debug.Logs() (Editor only)
            /// </summary>
            internal static bool DisableFormattingInEditor { get { return Instance.disableFormattingInEditor; } }
            /// <summary>
            /// Won't format the Messages and only display them with the default Debug.Logs() (Build only)
            /// </summary>
            internal static bool DisableFormattingInBuild { get { return Instance.disableFormattingInBuild; } }
            /// <summary>
            /// Disables all D.Logs() called with this Plugin (Editor only)
            /// </summary>
            internal static bool DisableLogsInEditor { get { return Instance.disableLogsInEditor; } }
            /// <summary>
            /// Disables all D.Logs() called with this Plugin (Build only)
            /// </summary>
            internal static bool DisableLogsInBuild { get { return Instance.disableLogsInBuild; } }
        #endregion


        #if UNITY_EDITOR
            /// <summary>
            /// Tries to load the ScriptableObject (Editor only!)
            /// </summary>
            /// <returns>Returns the instance of the first ScriptableObject.asset file found, or null</returns>
            public static Settings LoadScriptableObjectInEditor()
            {
                if (instance != null) return instance;
                    
                    return instance = (Settings)UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets(StringBuilderUtility.Append("t:", typeof(Settings).FullName)).FirstOrDefault()), typeof(Settings));
            }
        #endif
        
        /// <summary>
        /// Tries to load the ScriptableObject from the "Resources"-Folder in Build
        /// </summary>
        /// <returns>Returns the instance of the first ScriptableObject.asset file found, or null</returns>
        private static Settings LoadScriptableObjectInBuild()
        {
            if (instance != null) return instance;

                return instance = (Settings)Resources.Load("Settings_SDF_SOB", typeof(Settings));
        }
    }
}