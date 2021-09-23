using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Creates Files on import if the don't exist already
    /// </summary>
    public class PackageImporter : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] _ImportedAssets, string[] _DeletedAssets, string[] _MovedAssets, string[] _MovedFromAssetPaths)
        {
            AssetDatabase.importPackageCompleted += CreateFiles;
        }

        /// <summary>
        /// Creates the Files if they doesn't already exist
        /// </summary>
        /// <param name="_PackageName">The name of the imported Package</param>
        private static void CreateFiles(string _PackageName)
        {
            // Is the imported Asset this Plugin?
            if (_PackageName == "Simple Debug Formatting")
            {
                // Check if the files already exist
                var _assets = AssetDatabase.FindAssets("Presets");
                var _presetsScript = _assets.FirstOrDefault(_Guid => AssetDatabase.GUIDToAssetPath(_Guid).Contains("/Simple Debug Formatting/Scripts/Presets.cs"));
                var _settingsAsset = AssetDatabase.FindAssets(StringBuilderUtility.Append("t:", typeof(Settings).FullName)).Length;
                var _presetsAsset = AssetDatabase.FindAssets(StringBuilderUtility.Append("t:", typeof(FormatPresets).FullName)).Length;

                if (_presetsScript != null && _settingsAsset != 0 && _presetsAsset != 0) return;
                {
                    var _scripts = AssetDatabase.FindAssets(typeof(PackageImporter).Name);
                    var _scriptPath = AssetDatabase.GUIDToAssetPath(_scripts.FirstOrDefault(_Guid => AssetDatabase.GUIDToAssetPath(_Guid).Contains("/Simple Debug Formatting/Scripts/Editor/")));
                    var _rootPath = _scriptPath.Remove(_scriptPath.LastIndexOf("Scripts/Editor/", StringComparison.Ordinal));
                    var _folderPath = StringBuilderUtility.Append(_rootPath, "Resources/");
                    
                    if (_presetsScript == null)
                        ScriptableObject.CreateInstance<CSharpFile>().Action(0, StringBuilderUtility.Append(_rootPath, "Scripts/Presets.cs"),string.Empty);
                    
                    if (!Directory.Exists(_folderPath)) 
                        Directory.CreateDirectory(_folderPath);
                    
                    // Create the ScriptableObjects
                    // "AssetDatabase.SaveAssets();" needs to be called separately, otherwise the "Settings_SDF_SOB.asset" won't be created when there are errors with the Presets!
                    if (_settingsAsset == 0)
                    {
                        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Settings>(), StringBuilderUtility.Append(_folderPath, "Settings_SDF_SOB.asset"));
                        AssetDatabase.SaveAssets();
                    }
                    if (_presetsAsset == 0)
                    {
                        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FormatPresets>(), StringBuilderUtility.Append(_folderPath, "Presets_SDF_SOB.asset"));
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            else
            {
                AssetDatabase.importPackageCompleted -= CreateFiles;
            }
        }

        /// <summary>
        /// Imports the newly created .cs file and writes its contents
        /// </summary>
        private class CSharpFile : EndNameEditAction
        {
            public override void Action(int _InstanceId, string _UnityPath, string _ResourceFile)
            {
                File.WriteAllText(Path.GetFullPath(_UnityPath), "namespace SimpleDebugFormatting\n{\n    /// <summary>\n    /// Enum of all Presets\n    /// </summary>\n    public enum P\n    {\n    }\n}");
                AssetDatabase.ImportAsset(_UnityPath);
                CleanUp();
            }
        }
    }
}