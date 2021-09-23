using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Stores presets for the DebugFormatting
    /// </summary>
    //[CreateAssetMenu(menuName = "Simple Debug Formatting/FormatPresets", fileName = "Presets_SDF_SOB")] // Commented out so it won't show up in the creation Menu
    public class FormatPresets : ScriptableObject
    {
        #region Inspector Fields
            [Tooltip("List of all Presets")]
            [SerializeField] private List<Preset> presets = new List<Preset>();
        #endregion

        #region Privates
            /// <summary>
            /// Singleton of "FormatPresets"
            /// </summary>
            private static FormatPresets instance;
            #if UNITY_EDITOR
                /// <summary>
                /// Is "true" when the "Presets"-Enum has been edited
                /// </summary>
                public bool waitingForRecompile;
            #endif
            /// <summary>
            /// List of all possible error in each Preset-element
            /// </summary>
            public List<Error> errors = new List<Error>();
            /// <summary>
            /// ALl currently active errors
            /// </summary>
            public List<string> errorText = new List<string> { "" };
            /// <summary>
            /// Is shown when the Presets-List doesn't match the Enum
            /// </summary>
            private const string CHANGES_TEXT = ColorCodes.WARNING_HEX + "<b>Changes detected! Press the \"Generate Presets\"-Button, to apply them!</b></color>";
            /// <summary>
            /// Is shown when a name Field is empty
            /// </summary>
            private const string EMPTY_TEXT = ColorCodes.ERROR_HEX + "Empty names are not allowed!</color>";
            /// <summary>
            /// Is shown when a name Field starts with a digit
            /// </summary>
            private const string DIGIT_TEXT = ColorCodes.ERROR_HEX + "First character must not be a digit!</color>";
            /// <summary>
            /// Is shown when a name Field contains a special character
            /// </summary>
            private const string SPECIAL_TEXT = ColorCodes.ERROR_HEX + "Whitespaces and special characters (except \"_\") are not allowed!</color>";
            /// <summary>
            /// Is shown when there are duplicate names
            /// </summary>
            private const string DUPLICATE_TEXT = ColorCodes.ERROR_HEX + "Duplicate names are not allowed!</color>";
        #endregion
        
        #region Properties
            /// <summary>
            /// List of all Presets
            /// </summary>
            internal static IEnumerable<Preset> Presets
            {
                get
                {
                    #if UNITY_EDITOR
                        return LoadScriptableObjectInEditor().presets;
                    #else
                        return LoadScriptableObjectInBuild().presets;
                    #endif
                }
            }
        #endregion
        

        #if UNITY_EDITOR
            /// <summary>
            /// Is called when the Script has finished recompiling
            /// </summary>
            [UnityEditor.Callbacks.DidReloadScripts]
            private static void OnRecompiled()
            {
                LoadScriptableObjectInEditor();            
                PresetsGenerated();
            }
            
            /// <summary>
            /// Tries to load the ScriptableObject (Editor only!)
            /// </summary>
            /// <returns>Returns the instance of the first ScriptableObject.asset file found, or null</returns>
            public static FormatPresets LoadScriptableObjectInEditor()
            {
                if (instance != null) return instance;
                
                    return instance = (FormatPresets)UnityEditor.AssetDatabase.LoadAssetAtPath(UnityEditor.AssetDatabase.GUIDToAssetPath(UnityEditor.AssetDatabase.FindAssets(StringBuilderUtility.Append("t:", typeof(FormatPresets).FullName)).FirstOrDefault()), typeof(FormatPresets));
            }
            
            /// <summary>
            /// Message for when new Presets have been generated
            /// </summary>
            private static void PresetsGenerated()
            {
                if (instance == null || !instance.waitingForRecompile) return;
                
                    D.Log_SDF("Finished recompiling!", ColorCodes.SUCCESS_32, 20);
                    instance.waitingForRecompile = false;
            }
        #endif

        /// <summary>
        /// Tries to load the ScriptableObject from the "Resources"-Folder in Build
        /// </summary>
        /// <returns>Returns the instance of the first ScriptableObject.asset file found, or null</returns>
        private static FormatPresets LoadScriptableObjectInBuild()
        {
            if (instance != null) return instance;

                return instance = (FormatPresets)Resources.Load("Presets_SDF_SOB", typeof(FormatPresets));
        }
        
        /// <summary>
        /// Checks if the Enum matches the Presets-List
        /// </summary>
        public void MatchWithEnum()
        {
            var _differentCount = false;
            var _differentEntries = false;
            var _enumNames = (from object _preset in Enum.GetValues(DebugFormat.PresetsType) select _preset.ToString()).ToList();

            // Checks if the Preset-List and the Enum have the same number of entries
            if (_enumNames.Count != presets.Count)
            {
                _differentCount = true;
            }

            // Skips this check if the count is already different (one mismatch is enough to trigger the warning)
            if (!_differentCount)
            {
                // Checks if the Preset and Enum names match 
                for (var i = 0; i < presets.Count; i++)
                {
                    if (_enumNames[i] == presets[i].Name) continue;
                    
                        _differentEntries = true;
                        break;
                }
            }
            
            // Message
            if (_differentCount || _differentEntries)
            {
                if (!errorText.Contains(CHANGES_TEXT)) errorText[0] = CHANGES_TEXT;
            }
            else
            {
                errorText[0] = errorText[0].Replace(CHANGES_TEXT, "");
            }
        }
        
        /// <summary>
        /// Checks if the Preset names are in the right format
        /// </summary>
        public void CheckNames()
        {
            if (presets.Count == 0)
            {
                // Removes all error Messages when the List is empty (Starts at index 1, so "CHANGES_TEXT" won't be removed)
                if (errorText.Count > 1)
                    errorText.RemoveRange(1, errorText.Count - 1);

                return;
            }
            
            errors = new List<Error>();
            foreach (var _preset in presets) { errors.Add(new Error(false, false, false, false)); }

            for (var i = 0; i < presets.Count; i++)
            {
                var _nameI = presets[i].Name;
                
                // Name is empty
                if (_nameI == string.Empty)
                {
                    if (!errorText.Contains(EMPTY_TEXT)) errorText.Add(EMPTY_TEXT);
                    errors[i] = new Error(true, errors[i].isFirstDigit, errors[i].specialCharacter, errors[i].duplicate);
                }
                else
                {
                    if (errors.All(_Error => _Error.isEmpty == false)) errorText.Remove(EMPTY_TEXT);
                }
                // First character is digit
                if (Regex.IsMatch(_nameI, @"^[0-9]"))
                {
                    if (!errorText.Contains(DIGIT_TEXT)) errorText.Add(DIGIT_TEXT);
                    errors[i] = new Error(errors[i].isEmpty, true, errors[i].specialCharacter, errors[i].duplicate);
                }
                else
                {
                    if (errors.All(_Error => _Error.isFirstDigit == false)) errorText.Remove(DIGIT_TEXT);
                }
                // Contains special characters
                if (Regex.IsMatch(_nameI, @"[^a-zA-Z0-9_]+"))
                {
                    if (!errorText.Contains(SPECIAL_TEXT)) errorText.Add(SPECIAL_TEXT);
                    errors[i] = new Error(errors[i].isEmpty, errors[i].isFirstDigit, true, errors[i].duplicate);
                }
                else
                {
                    if (errors.All(_Error => _Error.specialCharacter == false)) errorText.Remove(SPECIAL_TEXT);
                }

                // If there's only one Preset in the List
                if (presets.Count < 2) break;
                
                    for (var j = i + 1; j < presets.Count; j++)
                    {
                        var _nameJ = presets[j].Name;
                                
                        // Contains duplicates
                        if (_nameI != _nameJ) continue;
                        
                            if (!errorText.Contains(DUPLICATE_TEXT)) errorText.Add(DUPLICATE_TEXT);
                            errors[i] = new Error(errors[i].isEmpty, errors[i].isFirstDigit, errors[i].specialCharacter, true);
                            errors[j] = new Error(errors[j].isEmpty, errors[j].isFirstDigit, errors[j].specialCharacter, true);
                    }
            }

            if (errors.All(_Error => _Error.duplicate == false)) errorText.Remove(DUPLICATE_TEXT);
        }
        
        /// <summary>
        /// Checks what Messages to display in the InfoBox
        /// </summary>
        /// <returns>Returns the Messages for the InfoBox</returns>
        public string DisplayMessages()
        {
            string _infoBoxText;
            
            if (errorText.Count > 0)
            {
                _infoBoxText = string.Empty;

                var _messages = errorText.Where(_String => _String != string.Empty).ToList();

                for (var i = 0; i < _messages.Count; i++)
                {
                    _infoBoxText = StringBuilderUtility.Append(_infoBoxText, _messages[i]);
                    if (i + 1 < _messages.Count) _infoBoxText = StringBuilderUtility.Append(_infoBoxText, "\n");
                }
            }
            else
            {
                _infoBoxText = string.Empty;
            }

            return _infoBoxText;
        }

        #if UNITY_EDITOR
            /// <summary>
            /// Writes the Preset names to the "Presets"-Enum
            /// </summary>
            /// <param name="_Message">Messages for the InfoBox</param>
            public void GeneratePresets(out string _Message)
            {
                MatchWithEnum();
                CheckNames();
                _Message = DisplayMessages();

                // No new changes to apply
                if (errorText[0] == string.Empty)
                {
                    D.Warning_SDF("Presets are up to date.", ColorCodes.WARNING_32);
                    return;
                }
                // Names are not formatted correctly
                if (errors.Any(_Error => _Error.AnyErrors))
                {
                    D.Error_SDF("Couldn't generate Presets! Check the \"<b>Presets_SDF_SOB</b>\"-ScriptableObject!", ColorCodes.ERROR_32);
                    return;
                }
                
                // Find File
                var _formatPresetsPath = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.MonoScript.FromScriptableObject(this));
                var _assets = UnityEditor.AssetDatabase.FindAssets("Presets", new [] { StringBuilderUtility.Substring(_formatPresetsPath, 0, _formatPresetsPath.LastIndexOf("/", StringComparison.Ordinal)) });
                var _presetsPath = UnityEditor.AssetDatabase.GUIDToAssetPath(_assets.FirstOrDefault(_Guid => UnityEditor.AssetDatabase.GUIDToAssetPath(_Guid).Contains("/Presets.cs")));

                // Read File
                var _fileContent = File.ReadAllLines(_presetsPath);
                var _newFileContent = string.Empty;
                var _presetNames = presets.Select(_Preset => _Preset.Name).ToList();

                var _enumStart = -1;
                var _openBracket = -1;
                var _closeBracket = -1;
                
                // Enum start
                for (var i = 0; i < _fileContent.Length; i++)
                {
                    if (!Regex.IsMatch(_fileContent[i], StringBuilderUtility.Append(@"(public)\s{1,}(enum)\s{1,}", "(", DebugFormat.PresetsType.Name, ")"))) continue;
                    
                        _enumStart = i;
                        break;
                }
                // Opening bracket
                for (var i = (_enumStart + 1); i < _fileContent.Length; i++)
                {
                    if (!Regex.IsMatch(_fileContent[i], "{")) continue;
                    
                        _openBracket = i;
                        break;
                }
                // Closing bracket
                for (var i = (_openBracket + 1); i < _fileContent.Length; i++)
                {
                    if (!Regex.IsMatch(_fileContent[i], "}")) continue;
                    
                        _closeBracket = i;
                        break;
                }
                
                // Create new File
                
                // File start to Enum
                for (var i = 0; i <= _openBracket; i++)
                    _newFileContent = StringBuilderUtility.Append(_newFileContent, _fileContent[i], "\n");
                // Enum entries
                for (var i = 0; i < _presetNames.Count; i++)
                    _newFileContent = StringBuilderUtility.Append(_newFileContent, "".PadLeft(_fileContent[_closeBracket].TakeWhile(_Char => _Char == ' ').Count() + 4), _presetNames[i], ",\n");
                // Enum end to File end
                for (var i = _closeBracket; i < _fileContent.Length; i++)
                    _newFileContent = StringBuilderUtility.Append(_newFileContent, _fileContent[i], i + 1 != _fileContent.Length ? "\n" : string.Empty);

                // Write to File and recompile
                
                // So the Message won't be spammed when there are errors
                if (!waitingForRecompile)
                    D.Warning_SDF("Writing the Presets, please wait for Unity to finish recompiling!", ColorCodes.WARNING_32, 20);
                waitingForRecompile = true;
                
                File.WriteAllText(_presetsPath, _newFileContent);
                UnityEditor.AssetDatabase.ImportAsset(_presetsPath);
            }
        #endif
        
        /// <summary>
        /// Stores the formatting values
        /// </summary>
        [Serializable]
        internal struct Preset
        {
            #region Inspector Fields
                [Tooltip("The name of this preset")]
                [SerializeField] private string name;
                [Tooltip("Text color of this preset")]
                [SerializeField] private Color32 color;
                [Tooltip("Format style of this preset")]
                [SerializeField] private F format;
                [Tooltip("Text size of this preset")]
                [SerializeField] private int size;
            #endregion

            #region Properties
                /// <summary>
                /// The name of this preset
                /// </summary>
                internal string Name { get { return name; } }
                /// <summary>
                /// Text color of this preset
                /// </summary>
                internal Color32 Color { get { return color; } }
                /// <summary>
                /// Format style of this preset
                /// </summary>
                internal F Format { get { return format; } }
                /// <summary>
                /// Text size of this preset
                /// </summary>
                internal int Size { get { return size; } }
            #endregion
        }
        
        /// <summary>
        /// Stores all errors for the Preset names
        /// </summary>
        public struct Error
        {
            #region Privates
                /// <summary>
                /// Is the name field empty?
                /// </summary>
                internal readonly bool isEmpty;
                /// <summary>
                /// Is the first character a digit?
                /// </summary>
                internal readonly bool isFirstDigit;
                /// <summary>
                /// Are there special character in the name?
                /// </summary>
                internal readonly bool specialCharacter;
                /// <summary>
                /// Are there duplicates of this name inside the List?
                /// </summary>
                internal readonly bool duplicate;
            #endregion

            #region Properties
                /// <summary>
                /// Has this Preset name any format errors?
                /// </summary>
                public bool AnyErrors { get { return isEmpty || isFirstDigit || specialCharacter || duplicate; } }
            #endregion
            
            /// <param name="_IsEmpty">Is the name field empty?</param>
            /// <param name="_IsFirstDigit">Is the first character a digit?</param>
            /// <param name="_SpecialCharacter">Are there special character in the name?</param>
            /// <param name="_Duplicate">Are there duplicates of this name inside the List?</param>
            internal Error(bool _IsEmpty, bool _IsFirstDigit, bool _SpecialCharacter, bool _Duplicate)
            {
                isEmpty = _IsEmpty;
                isFirstDigit = _IsFirstDigit;
                specialCharacter = _SpecialCharacter;
                duplicate = _Duplicate;
            }
        }
    }
}
