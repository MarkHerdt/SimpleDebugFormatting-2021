using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Formats the Messages for the "DebugLog"-Class
    /// </summary>
    internal static class DebugFormat
    {
        #region Privates
            /// <summary>
            /// Reference to the Type of the "P(Presets)"-Enum
            /// </summary>
            private static Type presetsType;
            /// <summary>
            /// Name of the Assembly, the Plugin is in
            /// </summary>
            private const string ASSEMBLY_PLUGIN = "SimpleDebugFormattingAssembly";
            /// <summary>
            /// Name of the Assembly, the Plugin is in (For older Unity versions without AssemblyDefinitionFiles)
            /// </summary>
            private const string ASSEMBLY_CSHARP = "Assembly-CSharp-firstpass";
            /// <summary>
            /// Name of the "P(Presets)"-enum
            /// </summary>
            private const string PRESETS_ENUM = "SimpleDebugFormatting.P";
        #endregion

        #region Properties
            /// <summary>
            /// Returns a reference to the Type of the "P(Presets)"-Enum
            /// </summary>
            internal static Type PresetsType
            {
                get
                {
                    if (presetsType != null) return presetsType;
                    
                        return presetsType = GetClass(ASSEMBLY_PLUGIN, PRESETS_ENUM) ?? GetClass(ASSEMBLY_CSHARP, PRESETS_ENUM);
                }
            }
        #endregion
        
        /// <summary>
        /// Returns a reference to the given Class in the given Assembly
        /// </summary>
        /// <param name="_Assembly">The Assembly to search the Class in</param>
        /// <param name="_Class">The Class to search for</param>
        private static Type GetClass(string _Assembly, string _Class)
        {
            // Get Assembly
            foreach (var _assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (_assembly.GetName().Name != _Assembly) continue;
                
                    // Get Class
                    foreach (var _type in _assembly.GetTypes())
                        if (_type.ToString() == _Class)
                            return _type;
                                            
                    break;
            }

            return null;
        }
        
        /// <summary>
        /// Concatenates the formatted Messages or separates them into different Debug Logs <br/>
        /// <b>Is called via Reflection, don't change the MethodName!</b>
        /// </summary>
        /// <param name="_Messages">Array of Messages</param>
        /// <returns>Returns a List of strings, where each element is a different Message for the DebugLog</returns>
        internal static List<string> ConcatMessages(object[] _Messages)
        {
            // Skips the formatting if the setting is checked in the settings
            if (Application.isEditor && Settings.DisableFormattingInEditor || !Application.isEditor && Settings.DisableFormattingInBuild)
                return _Messages != null ? new List<string> { string.Join(" ", _Messages.Select(_Message => _Message.ToString())) } : new List<string> { "null" };
            
            // Checks if any object is null, if the object is a Collection, it only checks the Collection, not its content, the content could still be null!
            var _nullCheck = _Messages != null ? _Messages.Select(_Message => _Message ?? "null").ToList() : new List<object> { "null" };
            var _messages = GetMessages(_nullCheck);

            // Concatenates all Messages
            var _formattedMessages = new List<string> { string.Empty };
            var _previousMessageIndex = 0;
            var _lastMessageIndex = _messages.IndexOf(_messages.LastOrDefault(_Message => !_Message.isFormatParameter));
            var _logId = 0;
  
            for (var i = 0; i < _messages.Count; i++)
            {
                if (_lastMessageIndex == -1) SetFormattedMessages(_formattedMessages, _messages[i], false, AdjustNewLines(_messages[i])); // When there were only "Format-Parameter" passed to the Log Method
                if (_messages[i].isFormatParameter) continue;
                if (_messages[i].groupId != _previousMessageIndex) _previousMessageIndex = i;
                
                // Displays the Message in a separate Log
                if (_messages[i].newLog)
                {
                    // If the Message is a Collection, displays every Message in this Log in a separate line
                    if (_messages[i].newLine)
                    {
                        // Creates a new Log
                        if (_logId != _messages[i].groupId)
                        {
                            // When the current Log is empty
                            if (_formattedMessages[_formattedMessages.Count - 1] == string.Empty)
                            {
                                var _newLine = string.Empty;
                                
                                if (Settings.NewLineAfterCollection)
                                    // Adds an empty line, when the next Message is from another collection
                                    if (i + 1 < _messages.Count && _messages[i].collectionID != _messages[i + 1].collectionID)
                                        _newLine = "\n";
                                
                                SetFormattedMessages(_formattedMessages, _messages[i], false,  AdjustNewLines(_messages[i]), _newLine);
                            }
                            // When there's already a Message inside the current Log
                            else
                                SetFormattedMessages(_formattedMessages, _messages[i], true, AdjustNewLines(_messages[i]));
                        }
                        // If the Message is a Collection and the first Log has been created already, display the next Messages in separate lines
                        else
                        {
                            var _newLine = string.Empty;
                            
                            if (Settings.NewLineAfterCollection)
                                // Adds an empty line, when the next Message is from another collection
                                if (i + 1 < _messages.Count && _messages[i].collectionID != _messages[i + 1].collectionID)
                                    _newLine = "\n";
                            
                            SetFormattedMessages(_formattedMessages, _messages[i], false,  "\n", AdjustNewLines(_messages[i]), _newLine);
                        }
                        
                        _logId = _messages[i].groupId;

                        // Adds a new Log when this Logs Messages have ended and there are still more Messages left 
                        if (i + 1 < _messages.Count && _messages[i + 1].isFormatParameter && i != _lastMessageIndex)
                            _formattedMessages.Add("");
                    }
                    else
                    {
                        // When the current Log is empty
                        if (_formattedMessages[_formattedMessages.Count - 1] == string.Empty)
                            SetFormattedMessages(_formattedMessages, _messages[i], false,  AdjustNewLines(_messages[i]));
                        // When there's already a Message inside the current Log
                        else
                            SetFormattedMessages(_formattedMessages, _messages[i], true, AdjustNewLines(_messages[i]));
                    
                        // Adds a new Log when this Logs Messages have ended and there are still more Messages left 
                        if (i + 1 < _messages.Count && _messages[i + 1].isFormatParameter && i != _lastMessageIndex)
                            _formattedMessages.Add(""); 
                    }
                }
                // Displays the Message in a separate line
                else if (_messages[i].newLine)
                {
                    // Adds an empty line between this and the previous Message, when the previous Message doesn't belong to this Collection (doesn't add one if the previous Message has the "newLine"-Parameter enabled)
                    if (i - 1 > -1 && _messages[i - 1].groupId != _messages[i].groupId && !_messages[_previousMessageIndex].newLine)
                        SetFormattedMessages(_formattedMessages, _messages[i], false,  "\n");
                    
                    // Messages from the same collection                                         // Is this Message the first of this Log?
                    SetFormattedMessages(_formattedMessages, _messages[i], false,  _formattedMessages.Last() == string.Empty ? "" : "\n", AdjustNewLines(_messages[i]));
                    
                    var _nextGroupIDIndex = _messages.IndexOf(_messages.FirstOrDefault(_Message => _Message.groupId > _messages[i].groupId));

                    if (Settings.NewLineAfterCollection)
                        // Adds an empty line when the next Message is a FormatParameter or from another collection
                        if (i + 1 < _messages.Count && (_messages[i + 1].isFormatParameter || _messages[i].collectionID != _messages[i + 1].collectionID))
                            SetFormattedMessages(_formattedMessages, _messages[i], false,  "\n");
                    // Adds another empty line when this Message group has ended and there are still more Messages left (doesn't add one if the next Message has the "newLine"-Parameter enabled)
                    if (i + 1 < _messages.Count && _messages[i + 1].isFormatParameter && i != _lastMessageIndex && _nextGroupIDIndex != -1 && !_messages[_nextGroupIDIndex].newLine)
                        SetFormattedMessages(_formattedMessages, _messages[i], false, "\n");
                }
                // Displays the Message next to the previous Message
                else
                    SetFormattedMessages(_formattedMessages, _messages[i], false,  AdjustNewLines(_messages[i]));
            }

            // Adds an empty line at the end of each log, so there is space between user log and Unity's stacktrace
            for (var i = 0; i < _formattedMessages.Count; i++)
                if (_formattedMessages[i].LastOrDefault() != '\n')
                    _formattedMessages[i] = StringBuilderUtility.Append(_formattedMessages[i], "\n");
            
            Message.MAX_COLLECTION_INDEX.Clear();
            Message.MAX_KEY_LENGTH.Clear();

            return _formattedMessages;
        }
        
        /// <summary>
        /// Sorts the Message Arguments into their respective Types
        /// </summary>
        /// <param name="_Messages">List of Messages</param>
        /// <returns>Returns a List of Message-Structs, where each List element is a differently formatted Message</returns>
        private static List<Message> GetMessages(IEnumerable<object> _Messages)
        {
            var _messages = GroupMessages(_Messages, new List<Message>(), false);
            var _index = _messages.IndexOf(_messages.FirstOrDefault(_Message => !_Message.isFormatParameter));
            
            // When only Format parameter have been passed in the DebugLog()-Methods
            if (_index == -1)
            {
                for (var i = 0; i < _messages.Count; i++)
                    SetObject(_messages, i, _messages[i].message);
            }
            else
            {
                // Removes all Format parameter that came before the first string or object and applies them to all Messages
                if (_index > 0)
                {
                    var _formatParameter = new List<Message>(_messages.Take(_index));
                    _messages.RemoveRange(0, _index);

                    foreach (var _parameter in _formatParameter)
                        SetMessage(_messages, _parameter.message, _messages.Count - 1, true);
                }
                
                for (var i = 0; i < _messages.Count; i++)
                {
                    // Skips the "if", when the Message comes from a Collection or a KeyValuePair
                    if (_messages[i].internalIndex == -1 && _messages[i].key == string.Empty)
                        SetMessage(_messages, _messages[i].message, i, false);
                    else
                        SetObject(_messages, i, _messages[i].message);
                }
            }
            
            return _messages;
        }

        /// <summary>
        /// Assigns every Message its ID, Messages that are passed as a Collection will share the same ID
        /// </summary>
        /// <param name="_messages">The arguments that are passed to the DebugLog-Method</param>
        /// <param name="_GroupedMessages">Grouped Messages will be saved to this List</param>
        /// <param name="_IsRecursiveCall">Set to "true", if this Method is called recursively</param>
        /// <param name="_Key">The key from this Messages KeyValuePair (if it comes from one) as a string</param>
        /// <returns>Returns the passed List with the grouped Messages</returns>
        private static List<Message> GroupMessages(IEnumerable<object> _Messages, List<Message> _GroupedMessages, bool _IsRecursiveCall, string _Key = "")
        {
            var _collectionID = 0;
            if (_IsRecursiveCall) _collectionID = ++Message.collectionCount;
            
            var _messages = _Messages.ToList();
            for (var i = 0; i < _messages.Count; i++)
            {
                // When the message is a Format argument and this Method call is not called recursively
                if (!_IsRecursiveCall && (_messages[i] is C || _messages[i] is Color || _messages[i] is Color32 || _messages[i] is F || _messages[i].GetType() == PresetsType || 
                                          _messages[i] is IEnumerable && _messages[i].GetType().GetElementType() == typeof(F) || _messages[i].GetType().IsGenericType && _messages[i].GetType().GetGenericArguments()[0] == typeof(F) || 
                                          _messages[i] is int || _messages[i] is uint || _messages[i] is long || _messages[i] is ulong || _messages[i] is byte || _messages[i] is sbyte || _messages[i] is short || _messages[i] is ushort || 
                                          _messages[i] is double || _messages[i] is float || _messages[i] is decimal))
                {
                    if (_messages[i] is IEnumerable<F>)
                        _GroupedMessages.AddRange(from object _message in (ICollection)_messages[i] select new Message(_message, Message.idCount, true));
                    else
                        _GroupedMessages.Add(new Message(_messages[i], Message.idCount, true));
                }
                // Message that will be shown in the Console
                else
                {
                    // Recursive call = object was a collection and all objects in the same collection must have the same ID (CollectionID will be different for each collection)
                    if (!_IsRecursiveCall) 
                        Message.idCount++;
                    
                    CheckType(_messages[i], i, _GroupedMessages, _IsRecursiveCall, _collectionID, false, _Key);
                }
            }
            
            return _GroupedMessages;
        }
        
        /// <summary>
        /// Checks what Type the passed Message is
        /// </summary>
        /// <param name="_Message">The Message object to check the Type of</param>
        /// <param name="_Index">Index of the Message inside the list</param>
        /// <param name="_GroupedMessages">List to save the grouped Messages in</param>
        /// <param name="_IsRecursiveCall">Was the "GroupMessages"-Method called recursively?</param>
        /// <param name="_CollectionID">ID of this Messages Collection</param>
        /// <param name="_IsKeyValuePair">Is the object to check a KeyValuePair? (Only true when this Method is called recursively)</param>
        /// <param name="_Key">The key object of this Messages KeyValuePair</param>
        /// <returns>Returns the key object of this Messages KeyValuePair, if this Message comes from a KeyValuePair otherwise returns null</returns>
        private static object CheckType(object _Message, int _Index, List<Message> _GroupedMessages, bool _IsRecursiveCall, int _CollectionID, bool _IsKeyValuePair = false, object _Key = null)
        {
            string _keyString;
            
            if (_IsKeyValuePair)
            {
                // When this Message is the Key
                if (_Index == -1)
                    return _Message;
                
                // Null in this case means, the Key object is null
                _keyString = _Key == null ? "null" : _Key.ToString();
            }
            else
            {
                // Null in this case means, there is no Key object
                _keyString = _Key == null ? string.Empty : _Key.ToString();
            }

            switch (_Message)
            {
                // Null
                case null:
                    _GroupedMessages.Add(new Message("null", Message.idCount, !_IsRecursiveCall ? 0 : _CollectionID, !_IsRecursiveCall ? -1 : _Index, _keyString));
                    return null;
                // Strings
                case string _:
                    _GroupedMessages.Add(new Message(_Message, Message.idCount, !_IsRecursiveCall ? 0 : _CollectionID, !_IsRecursiveCall ? -1 : _Index, _keyString));
                    return null;
            }
            
            var _interfaces = _Message.GetType().GetInterfaces();

            // Dictionaries
            if (_interfaces.Contains(typeof(IDictionary)) || _interfaces.Contains(typeof(IDictionary<,>)))
            {
                var _keys = (ICollection)_Message.GetType().GetProperty("Keys").GetValue(_Message, null);
                var _values = (ICollection)_Message.GetType().GetProperty("Values").GetValue(_Message, null);
                    
                var _keyArray = new object[_keys.Count];
                var _valueArray = new object[_values.Count];
                _keys.CopyTo(_keyArray, 0);
                _values.CopyTo(_valueArray, 0);
                    
                var _kvpList = new List<object>();
                for (var i = 0; i < _keyArray.Length; i++)
                    _kvpList.Add(new KeyValuePair<object, object>(_keyArray[i], _valueArray[i]));
                    
                AddCollection(_Message, _Index, _GroupedMessages, _IsRecursiveCall, _CollectionID, _keyString, _kvpList.Count);
                if (_kvpList.Count > 0)
                    GroupMessages(_kvpList, _GroupedMessages, true, _keyString);
            }
            // KeyValuePairs
            else if (_Message.GetType().IsGenericType && _Message.GetType().GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var _key = CheckType(_Message.GetType().GetProperty("Key").GetValue(_Message, null), -1, _GroupedMessages, _IsRecursiveCall, _CollectionID, true);
                CheckType(_Message.GetType().GetProperty("Value").GetValue(_Message, null), _Index, _GroupedMessages, _IsRecursiveCall, _CollectionID, true, _key);
            }
            // Every other Collection
            else if (_interfaces.Contains(typeof(IEnumerable)))
            {
                var _enumerator = ((IEnumerable)_Message).GetEnumerator();
                var _messages = new List<object>();

                while (_enumerator.MoveNext())
                    _messages.Add(_enumerator.Current);
                
                AddCollection(_Message, _Index, _GroupedMessages, _IsRecursiveCall, _CollectionID, _keyString, _messages.Count);
                if (_messages.Count > 0)
                    GroupMessages(_messages, _GroupedMessages, true, _keyString);   
            }
            // Everything else
            else
                _GroupedMessages.Add(new Message(_Message, Message.idCount, !_IsRecursiveCall ? 0 : _CollectionID, !_IsRecursiveCall ? -1 : _Index, _keyString));

            return null;
        }
        
        /// <summary>
        /// Adds the Collection to the GroupedMessage list
        /// </summary>
        /// <param name="_Message">The Message object to check the Type of</param>
        /// <param name="_Index">Index of the Message inside the list</param>
        /// <param name="_GroupedMessages">List to save the grouped Messages in</param>
        /// <param name="_IsRecursiveCall">Was the "GroupMessages"-Method called recursively?</param>
        /// <param name="_CollectionID">ID of this Messages Collection</param>
        /// <param name="_KeyString">The key object of this Messages KeyValuePair</param>
        /// <param name="_CollectionSize">Number of entries inside this Collection</param>
        private static void AddCollection(object _Message, int _Index, ICollection<Message> _GroupedMessages, bool _IsRecursiveCall, int _CollectionID, string _KeyString, int _CollectionSize)
        {
            var _niceTypeName = StringBuilderUtility.Append("<b><i>", _Message.GetType().GetNiceName(), "</i></b> ");
            var _collectionSize = StringBuilderUtility.Append("{ <i>", _CollectionSize == 0 ? "EMPTY" : _CollectionSize.ToString(), "</i> }");
            var _entryCollectionID = _CollectionSize > 0 ? Message.collectionCount + 1 : 0;
            
                                                  // IsRecursiveCall == The Collection will also be a part of another Collection when this is a recursive call, so it will also have an index
            _GroupedMessages.Add(_IsRecursiveCall ? new Message(StringBuilderUtility.Append(_niceTypeName, _collectionSize), Message.idCount, _CollectionID, _Index, _CollectionSize, _entryCollectionID, _KeyString)
                                                  // !IsRecursiveCall == The Collection isn't part of any Collection, so no index needs to be displayed
                                                  : new Message(StringBuilderUtility.Append(_niceTypeName, _collectionSize), Message.idCount, _CollectionID, -1, _CollectionSize, _entryCollectionID, _KeyString));
        }
        
        /// <summary>
        /// Sets the values for the Messages
        /// </summary>
        /// <param name="_Messages">The Message to apply the formatting to</param>
        /// <param name="_MessageType">The object that was passed into the DebugLog()-Method</param>
        /// <param name="_ListIndex">List index to start at</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetMessage(IList<Message> _Messages, object _MessageType, int _ListIndex, bool _ApplyToAll)
        {
            if (_MessageType is C)
                SetColor(_Messages, _ListIndex, (C)_MessageType, _ApplyToAll);
            else if (_MessageType is Color)
                SetColor(_Messages, _ListIndex, (Color)_MessageType, _ApplyToAll);
            else if (_MessageType is Color32)
                SetColor(_Messages, _ListIndex, (Color32)_MessageType, _ApplyToAll);
            else if (_MessageType is F)
                SetFormat(_Messages, _ListIndex, (F)_MessageType, _ApplyToAll);
            else if (_MessageType.GetType() == PresetsType)
                SetPreset(_Messages, _ListIndex, _MessageType.ToString(), _ApplyToAll);
            else if (_MessageType is int)
                SetSize(_Messages, _ListIndex, ((int)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is uint)
                SetSize(_Messages, _ListIndex, ((uint)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is long)
                SetSize(_Messages, _ListIndex, ((long)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is ulong)
                SetSize(_Messages, _ListIndex, ((ulong)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is byte)
                SetSize(_Messages, _ListIndex, ((byte)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is sbyte)
                SetSize(_Messages, _ListIndex, ((sbyte)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is short)
                SetSize(_Messages, _ListIndex, ((short)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is ushort)
                SetSize(_Messages, _ListIndex, ((ushort)_MessageType).ToString(), _ApplyToAll);
            else if (_MessageType is double)
                SetSize(_Messages, _ListIndex, ((double)_MessageType).ToString(CultureInfo.InvariantCulture), _ApplyToAll);
            else if (_MessageType is float)
                SetSize(_Messages, _ListIndex, ((float)_MessageType).ToString(CultureInfo.InvariantCulture), _ApplyToAll);
            else if (_MessageType is decimal)
                SetSize(_Messages, _ListIndex, ((decimal)_MessageType).ToString(CultureInfo.InvariantCulture), _ApplyToAll);
            else
                SetObject(_Messages, _ListIndex, _MessageType);
        }
        
        /// <summary>
        /// Applies the formatting values of a Preset to the Message
        /// </summary>
        /// <param name="_Messages">The Message to apply the formatting to</param>
        /// <param name="_ListIndex">Index of the List, the Preset is at</param>
        /// <param name="_PresetName">The Preset to get the formatting values from</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetPreset(IList<Message> _Messages, int _ListIndex, string _PresetName, bool _ApplyToAll)
        {
            var _preset = FormatPresets.Presets.FirstOrDefault(_Preset => _Preset.Name == _PresetName);
            
            if (_preset.Name == null) return;
                
                SetColor(_Messages, _ListIndex, _preset.Color, _ApplyToAll);
                SetFormat(_Messages, _ListIndex, _preset.Format, _ApplyToAll);
                SetSize(_Messages, _ListIndex, _preset.Size.ToString(), _ApplyToAll);
        }

        /// <summary>
        /// Formats the Color of the Message
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_ListIndex">Index of the List, the Color is at</param>
        /// <param name="_Color">The Color to apply to the Message</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetColor(IList<Message> _Messages, int _ListIndex, C _Color, bool _ApplyToAll)
        {
            // Moves from the last Message to the first until the IDs don't match anymore
            for (var i = _ListIndex; i >= 0; i--)
            {
                // Continues to the next Message, if this one is a format parameter 
                if (_Messages[i].isFormatParameter) continue;
                // Breaks, when the Message has a different groupID then this FormatSetting
                if (!_ApplyToAll && _Messages[i].groupId != _Messages[_ListIndex].groupId) break;
                
                    _Messages[i] = new Message(_Messages[i])
                    {
                        colorStart = "<color=",
                        color = StringBuilderUtility.Append(_Color.ToString(), ">"),
                        colorEnd = "</color>"
                    };
            }
        }
        
        /// <summary>
        /// Formats the Color of the Message
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_ListIndex">Index of the List, the Color is at</param>
        /// <param name="_Color">The Color to apply to the Message</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetColor(IList<Message> _Messages, int _ListIndex, Color _Color, bool _ApplyToAll)
        {
            // Moves from the last Message to the first until the IDs don't match anymore
            for (var i = _ListIndex; i >= 0; i--)
            {
                // Continues to the next Message, if this one is a format parameter 
                if (_Messages[i].isFormatParameter) continue;
                // Breaks, when the Message has a different groupID then this FormatSetting
                if (!_ApplyToAll && _Messages[i].groupId != _Messages[_ListIndex].groupId) break;
                
                _Messages[i] = new Message(_Messages[i])
                {
                    colorStart = "<color=",
                    color = StringBuilderUtility.Append("#", ColorUtility.ToHtmlStringRGBA(new Color(_Color.r > 1 ? _Color.r / 255 : _Color.r,
                                                                                                     _Color.g > 1 ? _Color.g / 255 : _Color.g,
                                                                                                     _Color.b > 1 ? _Color.b / 255 : _Color.b,
                                                                                                     _Color.a > 1 ? _Color.a / 255 : _Color.a)), ">"),
                    colorEnd = "</color>"
                };
            }
        }
        
        /// <summary>
        /// Formats the Color of the Message
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_ListIndex">Index of the List, the Color is at</param>
        /// <param name="_Color">The Color to apply to the Message</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetColor(IList<Message> _Messages, int _ListIndex, Color32 _Color, bool _ApplyToAll)
        {
            // Moves from the last Message to the first until the IDs don't match anymore
            for (var i = _ListIndex; i >= 0; i--)
            {
                // Continues to the next Message, if this one is a format parameter 
                if (_Messages[i].isFormatParameter) continue;
                // Breaks, when the Message has a different groupID then this FormatSetting
                if (!_ApplyToAll && _Messages[i].groupId != _Messages[_ListIndex].groupId) break;
                
                _Messages[i] = new Message(_Messages[i])
                {
                    colorStart = "<color=",
                    color = StringBuilderUtility.Append("#", ColorUtility.ToHtmlStringRGBA(new Color32(_Color.r, _Color.g, _Color.b, _Color.a)), ">"),
                    colorEnd = "</color>"
                };
            }
        }
        
        /// <summary>
        /// Formats the Text style of the Message
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_ListIndex">Index of the List, the FormatSetting is at</param>
        /// <param name="_Format">The Format to apply to the Message</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetFormat(IList<Message> _Messages, int _ListIndex, F _Format, bool _ApplyToAll)
        {
            // Moves from the last Message to the first until the IDs don't match anymore
            for (var i = _ListIndex; i >= 0; i--)
            {
                // Continues to the next Message, if this one is a format parameter 
                if (_Messages[i].isFormatParameter) continue;
                // Breaks, when the Message has a different groupID then this FormatSetting
                if (!_ApplyToAll && _Messages[i].groupId != _Messages[_ListIndex].groupId) break;
                
                    switch (_Format)
                    {
                        case F.B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                boldStart = "<b>",
                                boldEnd = "</b>" 
                            };
                            break;
                        case F.I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.NL:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true
                            };
                            break;
                        case F.NL_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                boldStart = "<b>",
                                boldEnd = "</b>" 
                            };
                            break;
                        case F.NL_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.NL_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.LOG:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true
                            };
                            break;
                        case F.LOG_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>" 
                            };
                            break;
                        case F.LOG_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.LOG_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.LOG_NL:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true
                            };
                            break;
                        case F.LOG_NL_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>" 
                            };
                            break;
                        case F.LOG_NL_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.LOG_NL_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>"
                            };
                            break;
                        case F.INDEX:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_NL:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_NL_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_NL_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_NL_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_LOG:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_LOG_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_LOG_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_LOG_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i)
                            };
                            break;
                        case F.INDEX_LOG_NL:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_LOG_NL_B:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_LOG_NL_I:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                        case F.INDEX_LOG_NL_BI:
                            _Messages[i] = new Message(_Messages[i])
                            {
                                newLine = true,
                                newLog = true,
                                boldStart = "<b>",
                                boldEnd = "</b>",
                                italicStart = "<i>",
                                italicEnd = "</i>",
                                index = GetIndexSpacing(_Messages, i, true)
                            };
                            break;
                    }
            }
        }

        /// <summary>
        /// Returns the formatted index-string, so the spacing between the index and the value will be equal on each Collection entry
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_Index">Index of the Message entry</param>
        /// <param name="_NewLine">Will this Message be displayed in a separate line?</param>
        private static string GetIndexSpacing(IList<Message> _Messages, int _Index, bool _NewLine = false)
        {
            var _index = _Messages[_Index].internalIndex;

            // Index will not be displayed
            if (_index == -1) return string.Empty;
            
                // Messages will not be displayed in a new line
                if (!_NewLine) return StringBuilderUtility.Append("<b>[</b>", "<i>", _index.ToString(), "</i>", "<b>]</b>");
                
                    var _thisIndex = _Messages[_Index].internalIndex.ToString().Length;
                    var _maxIndex = Message.MAX_COLLECTION_INDEX[_Messages[_Index].collectionID].ToString().Length;
                    var _space = _thisIndex < _maxIndex ? _maxIndex + (_maxIndex - _thisIndex) : 2;
                            
                    return StringBuilderUtility.Append("<b>[</b>", "<i>", _index.ToString(), "</i>", "<b>]</b>", " ".PadRight(_space));
        }
        
        /// <summary>
        /// Formats the size of the Message
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_ListIndex">Index of the List, the Message is at</param>
        /// <param name="_Size">The size to set the Message to</param>
        /// <param name="_ApplyToAll">Should this setting be applied to all Messages in the DebugLog?</param>
        private static void SetSize(IList<Message> _Messages, int _ListIndex, string _Size, bool _ApplyToAll)
        {
            // Moves from the last Message to the first until the IDs don't match anymore
            for (var i = _ListIndex; i >= 0; i--)
            {
                // Continues to the next Message, if this one is a format parameter 
                if (_Messages[i].isFormatParameter) continue;
                // Breaks, when the Message has a different groupID then this FormatSetting
                if (!_ApplyToAll && _Messages[i].groupId != _Messages[_ListIndex].groupId) break;
                
                    _Messages[i] = new Message(_Messages[i])
                    {
                        sizeStart = "<size=",
                        size = StringBuilderUtility.Append(_Size, ">"),
                        sizeEnd = "</size>"
                    };
            }
        }
        
        /// <summary>
        /// Concatenates an Object to the Message via the ToString()-Method 
        /// </summary>
        /// <param name="_Messages">List of all Messages in this DebugLog call</param>
        /// <param name="_ListIndex">Index of the List, the Message is at</param>
        /// <param name="_Object">The Object to concatenate to the Message</param>
        private static void SetObject(IList<Message> _Messages, int _ListIndex, object _Object)
        {
            string _text;
            
            try
            {
                // If the Object has an .ToString()-override, use the .ToString()-Method, otherwise use the .GetNiceName()-Method
                var _method = _Object.GetType().GetMethod("ToString", BindingFlags.Instance | BindingFlags.Public);
                _text = _method != null ? StringBuilderUtility.Append(_method.DeclaringType != typeof(object) ? _Object.ToString() : _Object.GetType().GetNiceName(), " ") : StringBuilderUtility.Append(_Object.ToString(), " ");
            }
            catch
            {
                _text = _Object.ToString();
            }

            _Messages[_ListIndex] = new Message(_Messages[_ListIndex])
            {
                Text = _text
            };
        }
        
        /// <summary>
        /// Moves the NewLine character outside the Rich-Text Tags, if there are any
        /// </summary>
        /// <param name="_Message">The Message to format</param>
        /// <returns>Returns the formatted Message as a string</returns>
        private static string AdjustNewLines(Message _Message)
        {
            var _newLineIndices = new List<int>();

            // Checks if the Message has NewLine character in it, and at what index they are if yes
            for (var i = 0; i < _Message.Text.Length; i++)
            {
                if (_Message.Text[i] == '\n')
                    _newLineIndices.Add(i);
            }

            // When there are no NewLine character in the Message
            if (_newLineIndices.Count == 0)
                return _Message.formattedMessage = StringBuilderUtility.Append(_Message.StartTags, _Message.Text, _Message.ClosingTags);
            
            var _tmpMessage = string.Empty;
            var _currentIndex = 0;
            
            // Rearranges the string, so all NewLine character are at the end (outside) of the RichText-formatting
            foreach (var _index in _newLineIndices)
            {
                var _startIndex = _currentIndex;
                var _length = _index - _startIndex;
                
                _tmpMessage = StringBuilderUtility.Append(_tmpMessage, _Message.StartTags, StringBuilderUtility.Substring(_Message.Text, _startIndex, _length), _Message.ClosingTags, "\n");
                
                _currentIndex = _index + 1;
            }

            // If there are still character after the last NewLine, add them to the Message
            var _lastIndex = int.Parse(_newLineIndices.Last().ToString());
            if (_Message.Text.Length > _lastIndex)
                _tmpMessage = StringBuilderUtility.Append(_tmpMessage, _Message.StartTags, StringBuilderUtility.Substring(_Message.Text, _lastIndex + 1), _Message.ClosingTags);

            return _Message.formattedMessage = _tmpMessage;
        }
        
        /// <summary>
        /// Adds all formatted Messages to the List
        /// </summary>
        /// <param name="_FormattedMessages">List for the formatted Messages</param>
        /// <param name="_Message">The Message object that contains the Messages settings</param>
        /// <param name="_Add">Whether to add the new Text to the list or concatenate it to the previous Message</param>
        /// <param name="_Text">The Message to display in the Console</param>
        private static void SetFormattedMessages(IList<string> _FormattedMessages, Message _Message, bool _Add, string _Text)
        {
            CheckLogLength(_FormattedMessages, _Message, _Add, _Text, 16299);
        }
        
        /// <summary>
        /// Adds all formatted Messages to the List
        /// </summary>
        /// <param name="_FormattedMessages">List for the formatted Messages</param>
        /// <param name="_Message">The Message object that contains the Messages settings</param>
        /// <param name="_Add">Whether to add the new Text to the list or concatenate it to the previous Message</param>
        /// <param name="_Text1">The Message to display in the Console (_Text2 will be appended)</param>
        /// <param name="_Text2">The Message to display in the Console (Will be appended to _Text1)</param>
        private static void SetFormattedMessages(IList<string> _FormattedMessages, Message _Message, bool _Add, string _Text1, string _Text2)
        {
            CheckLogLength(_FormattedMessages, _Message, _Add, StringBuilderUtility.Append(_Text1, _Text2), 16299);
        }
        
        /// <summary>
        /// Adds all formatted Messages to the List
        /// </summary>
        /// <param name="_FormattedMessages">List for the formatted Messages</param>
        /// <param name="_Message">The Message object that contains the Messages settings</param>
        /// <param name="_Add">Whether to add the new Text to the list or concatenate it to the previous Message</param>
        /// <param name="_Text1">The Message to display in the Console (_Text2 and _Text3 will be appended)</param>
        /// <param name="_Text2">The Message to display in the Console (Will be appended to _Text1)</param>
        /// <param name="_Text3">The Message to display in the Console (Will be appended to _Text2)</param>
        private static void SetFormattedMessages(IList<string> _FormattedMessages, Message _Message, bool _Add, string _Text1, string _Text2, string _Text3)
        {
            CheckLogLength(_FormattedMessages, _Message, _Add, StringBuilderUtility.Append(_Text1, _Text2, _Text3), 16299);
        }
        
        /// <summary>
        /// Splits the Message into multiple DebugLogs, when it's to long
        /// </summary>
        /// <param name="_FormattedMessages">List for the formatted Messages</param>
        /// <param name="_Message">The Message object that contains the Messages settings</param>
        /// <param name="_Add">Whether to add the new Text to the list or concatenate it to the previous Message</param>
        /// <param name="_Text">The Message to display in the Console</param>
        /// <param name="_MaxCharCount">The maximum amount of characters allowed in one DebugLog</param>
        private static void CheckLogLength(IList<string> _FormattedMessages, Message _Message, bool _Add,  string _Text, int _MaxCharCount)
        {
            if (_FormattedMessages.Last().Length + _Text.Length <= _MaxCharCount)
            {
                if (_Add)
                    _FormattedMessages.Add(_Text);
                else
                    _FormattedMessages[_FormattedMessages.Count - 1] = StringBuilderUtility.Append(_FormattedMessages.Last(), _Text);

                return;
            }

            // When the Message belongs to a Collection (each element is checked separately)
            if (_Message.collectionID != 0)
            {
                var _text = _Text.TrimStart();
                
                if (_Text.Length > _MaxCharCount)
                {
                    var _index = StringBuilderUtility.Substring(_Text, 0, _MaxCharCount - _Message.ClosingTags.Length).Length;
                    var _truncate = StringBuilderUtility.Substring(_Text, _index);
                    
                    if (_FormattedMessages.Last() == string.Empty)
                        _FormattedMessages[_FormattedMessages.Count - 1] = StringBuilderUtility.Append(StringBuilderUtility.Substring(_Text, 0, _index), _Message.ClosingTags);
                    else
                        _FormattedMessages.Add(StringBuilderUtility.Append(StringBuilderUtility.Substring(_Text, 0, _index), _Message.ClosingTags));
                    
                    _text = StringBuilderUtility.Append(_Message.StartTags, _truncate);
                }
                
                _FormattedMessages.Add("");
                CheckLogLength(_FormattedMessages, _Message, false, _text, _MaxCharCount);  
            }
            else
            {
                // For NewLine character, when the Message is already full
                if (_Text.IsNullOrEmptyOrWhitespace()) return;
                    
                    var _index = StringBuilderUtility.Substring(_Text, 0, _MaxCharCount - _FormattedMessages.Last().Length - _Message.ClosingTags.Length).Length;
                    var _truncate = StringBuilderUtility.Substring(_Text, _index);
                    
                    _FormattedMessages[_FormattedMessages.Count - 1] = StringBuilderUtility.Append(_FormattedMessages.Last(), StringBuilderUtility.Substring(_Text, 0, _index), _Message.ClosingTags);
                    
                    // When the Message doesn't contain anything except RichText-Formatting
                    if (StringBuilderUtility.Substring(_truncate, 0, _truncate.Length - _Message.ClosingTags.Length).IsNullOrEmptyOrWhitespace()) return;

                        _FormattedMessages.Add("");
                        CheckLogLength(_FormattedMessages, _Message, false, StringBuilderUtility.Append(_Message.StartTags, _truncate), _MaxCharCount);   
            }
        }
    }
}