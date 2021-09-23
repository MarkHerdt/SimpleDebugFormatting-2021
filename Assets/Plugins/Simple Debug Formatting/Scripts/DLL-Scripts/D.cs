// using System;
// using System.Collections.Generic;
// using System.Globalization;
// using System.Linq;
// using System.Reflection;
// using UnityEngine;
//
// namespace SimpleDebugFormatting
// {
//     /// <summary>
//     /// For displaying formatted Debug.Logs
//     /// </summary>
//     public static class D
//     {
//         #region Privates
//             /// <summary>
//             /// Reference to the "DebugFormat"-Class
//             /// </summary>
//             private static Type debugFormat;
//             /// <summary>
//             /// Reference to the "DisableLogs"-Property in the "Settings"-class
//             /// </summary>
//             private static PropertyInfo disableLogs;
//             /// <summary>
//             /// Reference to the "ConcatMessages()"-Method in the "DebugFormat"-class
//             /// </summary>
//             private static MethodInfo concatMessages;
//             /// <summary>
//             /// Name of the Assembly, the Plugin is in
//             /// </summary>
//             private const string ASSEMBLY_PLUGIN = "SimpleDebugFormattingAssembly";
//             /// <summary>
//             /// Name of the Assembly, the Plugin is in (For older Unity versions without AssemblyDefinitionFiles)
//             /// </summary>
//             private const string ASSEMBLY_CSHARP = "Assembly-CSharp-firstpass";
//             /// <summary>
//             /// Name of the "DebugFormat"-class
//             /// </summary>
//             private const string DEBUG_FORMAT_CLASS = "SimpleDebugFormatting.DebugFormat";
//             /// <summary>
//             /// Name of the "Settings"-class
//             /// </summary>
//             private const string SETTINGS_CLASS = "SimpleDebugFormatting.Settings";
//             /// <summary>
//             /// Property name to disable the Logs in the Editor
//             /// </summary>
//             private const string EDITOR_LOGS = "DisableLogsInEditor";
//             /// <summary>
//             /// Property name to disable the Logs in Build
//             /// </summary>
//             private const string BUILD_LOGS = "DisableLogsInBuild";
//             /// <summary>
//             /// Name of the Method to invoke
//             /// </summary>
//             private const string METHOD_NAME = "ConcatMessages";
//         #endregion
//
//         #region Properties
//             /// <summary>
//             /// Reference to the "DebugFormat"-Class
//             /// </summary>
//             private static Type DebugFormat
//             {
//                 get
//                 {
//                     if (debugFormat != null) return debugFormat;
//
//                         return debugFormat = GetClass(ASSEMBLY_PLUGIN, DEBUG_FORMAT_CLASS) ?? GetClass(ASSEMBLY_CSHARP, DEBUG_FORMAT_CLASS);
//                 }
//             }
//             /// <summary>
//             /// Disables the Logs if true
//             /// </summary>
//             internal static bool DisableLogs
//             {
//                 get
//                 {
//                     if (disableLogs != null) return (bool)disableLogs.GetValue(null, null);
//
//                         var _settings = GetClass(ASSEMBLY_PLUGIN, SETTINGS_CLASS) ?? GetClass(ASSEMBLY_CSHARP, SETTINGS_CLASS);
//
//                         if (_settings == null) return false;
//                         
//                             return (disableLogs = _settings.GetProperty(Application.isEditor ? EDITOR_LOGS : BUILD_LOGS, BindingFlags.Static | BindingFlags.NonPublic)) != null && (bool)disableLogs.GetValue(null, null);
//                 }
//             }
//             /// <summary>
//             /// Reference to the "ConcatMessages()"-Method in the "DebugFormat"-class
//             /// </summary>
//             private static MethodInfo ConcatMessages
//             {
//                 get
//                 {
//                     if (concatMessages != null) return concatMessages;
//                     
//                         return concatMessages = DebugFormat.GetMethod(METHOD_NAME, BindingFlags.Static | BindingFlags.NonPublic);
//                 }
//             }
//         #endregion
//
//         /// <summary>
//         /// Returns a reference to the given Class in the given Assembly
//         /// </summary>
//         /// <param name="_Assembly">The Assembly to search the Class in</param>
//         /// <param name="_Class">The Class to search for</param>
//         private static Type GetClass(string _Assembly, string _Class)
//         {
//             // Get Assembly
//             foreach (var _assembly in AppDomain.CurrentDomain.GetAssemblies())
//             {
//                 if (_assembly.GetName().Name != _Assembly) continue;
//                 
//                     // Get Class
//                     foreach (var _type in _assembly.GetTypes())
//                         if (_type.ToString() == _Class)
//                             return _type;
//                                         
//                     break;
//             }
//
//             return null;
//         }
//         
//         /// <summary>
//         /// Displays a "byte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         public static void Log_SDF(byte _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         public static void Log_SDF(sbyte _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         public static void Log_SDF(short _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         public static void Log_SDF(ushort _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         public static void Log_SDF(int _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         public static void Log_SDF(uint _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         public static void Log_SDF(long _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         public static void Log_SDF(ulong _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         public static void Log_SDF(decimal _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         public static void Log_SDF(float _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         public static void Log_SDF(double _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         public static void Log_SDF(char _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.Log(AddNewLine(_Message.ToString()));
//         }
//
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         public static void Log_SDF(object _Message, bool _DontFormat = false)
//         {
//             if (DisableLogs) return;
//             
//                 if (!_DontFormat)
//                     foreach (var _message in GetMessages(new [] { _Message }))
//                         Debug.Log(_message);
//                 else
//                     Debug.Log(_Message);
//         }
//         
//         /// <summary>
//         /// Displays multiple Objects with different formatting in the Console                                            <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Messages">Message and Format arguments</param>
//         public static void Log_SDF(params object[] _Messages)
//         {
//             if (DisableLogs) return;
//             
//                 foreach (var _message in GetMessages(_Messages))
//                     Debug.Log(_message);
//         }
//
//         /// <summary>
//         /// Displays a "byte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         public static void Warning_SDF(byte _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         public static void Warning_SDF(sbyte _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         public static void Warning_SDF(short _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         public static void Warning_SDF(ushort _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         public static void Warning_SDF(int _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         public static void Warning_SDF(uint _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         public static void Warning_SDF(long _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         public static void Warning_SDF(ulong _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         public static void Warning_SDF(decimal _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         public static void Warning_SDF(float _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         public static void Warning_SDF(double _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         public static void Warning_SDF(char _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogWarning(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         public static void Warning_SDF(object _Message, bool _DontFormat = false)
//         {
//             if (DisableLogs) return;
//             
//                 if (!_DontFormat)
//                     foreach (var _message in GetMessages(new [] { _Message }))
//                         Debug.LogWarning(_message);
//                 else
//                     Debug.LogWarning(_Message);
//         }
//         
//         /// <summary>
//         /// Displays multiple Objects with different formatting in the Console                                            <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Messages">Message and Format arguments</param>
//         public static void Warning_SDF(params object[] _Messages)
//         {
//             if (DisableLogs) return;
//             
//                 foreach (var _message in GetMessages(_Messages))
//                     Debug.LogWarning(_message);
//         }
//
//         /// <summary>
//         /// Displays a "byte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         public static void Error_SDF(byte _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         public static void Error_SDF(sbyte _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         public static void Error_SDF(short _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         public static void Error_SDF(ushort _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         public static void Error_SDF(int _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         public static void Error_SDF(uint _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         public static void Error_SDF(long _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         public static void Error_SDF(ulong _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         public static void Error_SDF(decimal _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         public static void Error_SDF(float _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         public static void Error_SDF(double _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         public static void Error_SDF(char _Message)
//         {
//             if (DisableLogs) return;
//             
//                 Debug.LogError(AddNewLine(_Message.ToString()));
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         public static void Error_SDF(object _Message, bool _DontFormat = false)
//         {
//             if (DisableLogs) return;
//             
//                 if (!_DontFormat)
//                     foreach (var _message in GetMessages(new [] { _Message }))
//                         Debug.LogError(_message);
//                 else
//                     Debug.LogError(_Message);
//         }
//         
//         /// <summary>
//         /// Displays multiple Objects with different formatting in the Console                                            <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Messages">Message and Format arguments</param>
//         public static void Error_SDF(params object[] _Messages)
//         {
//             if (DisableLogs) return;
//             
//                 foreach (var _message in GetMessages(_Messages))
//                     Debug.LogError(_message);
//         }
//
//         /// <summary>
//         /// Displays a "byte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         public static Exception Exception_SDF(byte _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         public static Exception Exception_SDF(sbyte _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         public static Exception Exception_SDF(short _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         public static Exception Exception_SDF(ushort _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         public static Exception Exception_SDF(int _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         public static Exception Exception_SDF(uint _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         public static Exception Exception_SDF(long _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         public static Exception Exception_SDF(ulong _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         public static Exception Exception_SDF(decimal _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString(CultureInfo.InvariantCulture)) : new Exception(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         public static Exception Exception_SDF(float _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString(CultureInfo.InvariantCulture)) : new Exception(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         public static Exception Exception_SDF(double _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString(CultureInfo.InvariantCulture)) : new Exception(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)).ToString());
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         public static Exception Exception_SDF(char _Message)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(AddNewLine(_Message.ToString()).ToString());
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         public static Exception Exception_SDF(object _Message, bool _DontFormat = false)
//         {
//             return DisableLogs ? null : !_DontFormat ? new Exception(string.Join("\n", GetMessages(new [] { _Message }).ToArray())) : new Exception(_Message.ToString());
//         }
//         
//         /// <summary>
//         /// Displays an "Exception" in the Console
//         /// </summary>
//         /// <param name="_Exception">The "Exception" to display</param>
//         /// <param name="_InnerException">The "InnerException" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         public static Exception Exception_SDF(Exception _Exception, Exception _InnerException = null, bool _DontFormat = false)
//         {
//             if (DisableLogs) return new Exception(_Exception.Message, _InnerException);
//             
//                 if (_DontFormat)
//                     return _InnerException == null ? _Exception : new Exception(_Exception.Message, _InnerException);
//                 if (_InnerException == null)
//                     return new Exception(string.Join("\n", GetMessages(new object[] { _Exception.Message }).ToArray()));
//                     
//                 return new Exception(string.Join("\n", GetMessages(new object[] { _Exception.Message }).ToArray()), 
//                        new Exception(string.Join("\n", GetMessages(new object[] { _InnerException.Message }).ToArray())));
//         }
//         
//         /// <summary>
//         /// Displays multiple Objects with different formatting in the Console                                            <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Messages">Message and Format arguments</param>
//         public static Exception Exception_SDF(params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(string.Join(" ", _Messages.Select(_Message => _Message.ToString()).ToArray())) 
//                                : new Exception(string.Join("\n", GetMessages(_Messages).ToArray()));
//         }
//
//         /// <summary>
//         /// Adds an empty line at the end of each log, so there is space between user log and Unity's stacktrace
//         /// </summary>
//         /// <param name="_Message">The Message to add a new Line to</param>
//         /// <returns>Returns the formatted Message</returns>
//         internal static object AddNewLine(object _Message)
//         {
//             return string.Concat(_Message, "\n");
//         }
//         
//         /// <summary>
//         /// Invokes the "ConcatMessages"-Method in the "DebugFormat"-Class via Reflection
//         /// </summary>
//         /// <param name="_Parameters">The parameters to pass to the "ConcatMessages"-Method</param>
//         /// <returns>Returns a List of strings, where each element is a different Message for the DebugLog</returns>
//         internal static List<string> GetMessages<T>(T[] _Parameters)
//         {
//             return (List<string>)ConcatMessages.Invoke(null, new object[] { _Parameters });
//         }
//     }
// }