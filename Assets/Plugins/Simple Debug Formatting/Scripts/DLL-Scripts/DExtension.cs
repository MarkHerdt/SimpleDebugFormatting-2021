// using System;
// using System.Globalization;
// using UnityEngine;
// using static SimpleDebugFormatting.D;
//
// namespace SimpleDebugFormatting
// {
//     /// <summary>
//     /// For displaying formatted Debug.Logs, as Extension-Methods
//     /// </summary>
//     public static class DExtension
//     {
//         /// <summary>
//         /// Displays a "byte"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "byte"-value</returns>
//         public static byte Log_SDF(this byte _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "sbyte"-value</returns>
//         public static sbyte Log_SDF(this sbyte _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//                 
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "short"-value</returns>
//         public static short Log_SDF(this short _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "ushort"-value</returns>
//         public static ushort Log_SDF(this ushort _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "int"-value</returns>
//         public static int Log_SDF(this int _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "uint"-value</returns>
//         public static uint Log_SDF(this uint _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "long"-value</returns>
//         public static long Log_SDF(this long _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "ulong"-value</returns>
//         public static ulong Log_SDF(this ulong _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "decimal"-value</returns>
//         public static decimal Log_SDF(this decimal _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "float"-value</returns>
//         public static float Log_SDF(this float _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "double"-value</returns>
//         public static double Log_SDF(this double _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "char"-value</returns>
//         public static char Log_SDF(this char _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//
//         /// <summary>
//         /// Displays a "Color"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "Color"</returns>
//         public static Color Log_SDF(this Color _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "Color32"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color32" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "Color32"</returns>
//         public static Color32 Log_SDF(this Color32 _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.Log(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.Log(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         /// <returns>Returns the "object"</returns>
//         public static T Log_SDF<T>(this T _Message, bool _DontFormat = false)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (!_DontFormat)
//                     foreach (var _message in GetMessages(new [] { _Message }))
//                         Debug.Log(_message);
//                 else
//                     Debug.Log(_Message);
//                 
//                 return _Message;
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
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "object"</returns>
//         public static T Log_SDF<T>(this T _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 foreach (var _message in GetMessages(InsertAtZero(_Message, _Messages)))
//                     Debug.Log(_message);
//                 
//                 return _Message;
//         }
//
//         /// <summary>
//         /// Displays a "byte"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "byte"-value</returns>
//         public static byte Warning_SDF(this byte _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "sbyte"-value</returns>
//         public static sbyte Warning_SDF(this sbyte _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "short"-value</returns>
//         public static short Warning_SDF(this short _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "ushort"-value</returns>
//         public static ushort Warning_SDF(this ushort _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "int"-value</returns>
//         public static int Warning_SDF(this int _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "uint"-value</returns>
//         public static uint Warning_SDF(this uint _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "long"-value</returns>
//         public static long Warning_SDF(this long _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "ulong"-value</returns>
//         public static ulong Warning_SDF(this ulong _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "decimal"-value</returns>
//         public static decimal Warning_SDF(this decimal _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "float"-value</returns>
//         public static float Warning_SDF(this float _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "double"-value</returns>
//         public static double Warning_SDF(this double _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "char"-value</returns>
//         public static char Warning_SDF(this char _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "Color"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "Color"</returns>
//         public static Color Warning_SDF(this Color _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "Color32"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color32" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "Color32"-value</returns>
//         public static Color32 Warning_SDF(this Color32 _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogWarning(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         /// <returns>Returns the "object"</returns>
//         public static T Warning_SDF<T>(this T _Message, bool _DontFormat = false)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (!_DontFormat)
//                     foreach (var _message in GetMessages(new [] { _Message }))
//                         Debug.LogWarning(_message);
//                 else
//                     Debug.LogWarning(_Message);
//                 
//                 return _Message;
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
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "object"</returns>
//         public static T Warning_SDF<T>(this T _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 foreach (var _message in GetMessages(InsertAtZero(_Message, _Messages)))
//                     Debug.LogWarning(_message);
//                 
//                 return _Message;
//         }
//
//         /// <summary>
//         /// Displays a "byte"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "byte"-value</returns>
//         public static byte Error_SDF(this byte _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "sbyte"-value</returns>
//         public static sbyte Error_SDF(this sbyte _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "short"-value</returns>
//         public static short Error_SDF(this short _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "ushort"-value</returns>
//         public static ushort Error_SDF(this ushort _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "int"-value</returns>
//         public static int Error_SDF(this int _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "uint"-value</returns>
//         public static uint Error_SDF(this uint _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "long"-value</returns>
//         public static long Error_SDF(this long _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "ulong"-value</returns>
//         public static ulong Error_SDF(this ulong _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "decinmal"-value</returns>
//         public static decimal Error_SDF(this decimal _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "float"-value</returns>
//         public static float Error_SDF(this float _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "double"-value</returns>
//         public static double Error_SDF(this double _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "char"-value</returns>
//         public static char Error_SDF(this char _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "Color"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "Color"</returns>
//         public static Color Error_SDF(this Color _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays a "Color32"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color32" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "Color32"</returns>
//         public static Color32 Error_SDF(this Color32 _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (_Messages == null)
//                     Debug.LogError(AddNewLine(_Message.ToString()));
//                 else
//                     foreach (var _message in GetMessages(InsertAtZero(_Message.ToString(), _Messages)))
//                         Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         /// <returns>Returns the "object"</returns>
//         public static T Error_SDF<T>(this T _Message, bool _DontFormat = false)
//         {
//             if (DisableLogs) return _Message;
//             
//                 if (!_DontFormat)
//                     foreach (var _message in GetMessages(new [] { _Message }))
//                         Debug.LogError(_message);
//                 else
//                     Debug.LogError(_Message);
//                 
//                 return _Message;
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
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns the "object"</returns>
//         public static T Error_SDF<T>(this T _Message, params object[] _Messages)
//         {
//             if (DisableLogs) return _Message;
//             
//                 foreach (var _message in GetMessages(InsertAtZero(_Message, _Messages)))
//                     Debug.LogError(_message);
//                 
//                 return _Message;
//         }
//
//         /// <summary>
//         /// Displays a "byte"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "byte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this byte _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays an "sbyte"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "sbyte" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this sbyte _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "short"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "short" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this short _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "ushort"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ushort" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this ushort _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays an "int"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "int" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this int _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "uint"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "uint" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this uint _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "long"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "long" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this long _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "ulong"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "ulong" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this ulong _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "decimal"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "decimal" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this decimal _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString(CultureInfo.InvariantCulture)) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "float"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "float" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this float _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString(CultureInfo.InvariantCulture)) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "double"-value in the Console                                                                      <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "double" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this double _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString(CultureInfo.InvariantCulture)) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString(CultureInfo.InvariantCulture)).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(CultureInfo.InvariantCulture), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "char"-value in the Console                                                                        <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "char" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this char _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "Color"-value in the Console                                                                       <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this Color _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays a "Color32"-value in the Console                                                                     <br/>
//         /// <b>Color</b>:      <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>Color32</b>:    <i>(Unity Struct) will format the messages color.</i>                                      <br/>
//         /// <b>C (Color)</b>:  <i>(Plugin Enum) will format the messages color.</i>                                       <br/>
//         /// <b>F (Format)</b>: <i>(Plugin Enum) will format the messages Text style.</i>                                  <br/>
//         /// <b>P (Preset)</b>: <i>(Plugin Enum) will format the message with the values set in the ScriptableObject.</i>  <br/>
//         /// <b>Number</b>:     <i>Numbers will format the messages size.</i>                                              <br/>
//         /// <b>object</b>:     <i>Objects will be concatenated together via ToString().</i>                               <br/>
//         /// </summary>
//         /// <param name="_Message">The "Color32" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this Color32 _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : _Messages == null ? new Exception(AddNewLine(_Message.ToString()).ToString()) 
//                                                    : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message.ToString(), _Messages)).ToArray()));
//         }
//         
//         /// <summary>
//         /// Displays an "object" in the Console
//         /// </summary>
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this object _Message, bool _DontFormat = false)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) 
//                                : !_DontFormat ? new Exception(string.Join("\n", GetMessages(new [] { _Message }).ToArray())) 
//                                               : new Exception(_Message.ToString());
//         }
//         
//         /// <summary>
//         /// Displays an "Exception" in the Console
//         /// </summary>
//         /// <param name="_Exception">The "Exception" to display</param>
//         /// <param name="_InnerException">The "InnerException" to display</param>
//         /// <param name="_DontFormat">True = Displays this Message without any formatting</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this Exception _Exception, Exception _InnerException = null, bool _DontFormat = false)
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
//         /// <param name="_Message">The "object" to display</param>
//         /// <param name="_Messages">Message and Format arguments</param>
//         /// <returns>Returns an "Exception"</returns>
//         public static Exception Exception_SDF(this object _Message, params object[] _Messages)
//         {
//             return DisableLogs ? new Exception(_Message.ToString()) : new Exception(string.Join("\n", GetMessages(InsertAtZero(_Message, _Messages)).ToArray()));
//         }
//
//         /// <summary>
//         /// Inserts the passed Object into the passed Array at index 0
//         /// </summary>
//         /// <param name="_Object">The object to insert into the Array</param>
//         /// <param name="_Array">The Array to insert the object to</param>
//         /// <returns>Returns the passed Array with the inserted object at index 0</returns>
//         private static object[] InsertAtZero(object _Object, object[] _Array)
//         {
//             var _array = new object[_Array.Length + 1]; _array[0] = _Object;
//             Array.Copy(_Array, 0, _array, 1, _Array.Length);
//             return _array;
//         }
//     }
// }