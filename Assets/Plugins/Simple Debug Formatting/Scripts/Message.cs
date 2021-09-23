using System.Collections.Generic;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Colors to display the Debug Message in
    /// </summary>
    public enum C
    {
        Aqua,
        Black,
        Blue,
        Brown,
        Cyan,
        DarkBlue,
        Fuchsia,
        Green,
        Grey,
        LightBlue,
        Lime,
        Magenta,
        Maroon,
        Navy,
        Olive,
        Orange,
        Purple,
        Red,
        Silver,
        Teal,
        White,
        Yellow,
    }
    
    /// <summary>
    /// Text Formats for the Debug Messages
    /// </summary>
    public enum F
    {
        /// <summary>
        /// "None" | Default setting, no need to apply this
        /// </summary>
        N = 0,
        /// <summary>
        /// Displays this Message in [BOLD]
        /// </summary>
        B = 1,
        /// <summary>
        /// Displays this Message in [ITALIC]
        /// </summary>
        I = 2,
        /// <summary>
        /// Displays this Message in [BOLD] and [ITALIC]
        /// </summary>
        BI = 3,
        /// <summary>
        /// Displays this Message in a separate Line (Useful for when a Message is passed as a collection)
        /// </summary>
        NL = 4,
        /// <summary>
        /// Displays this Message in a separate Line in [BOLD] (Useful for when a Message is passed as a collection)
        /// </summary>
        NL_B = 5,
        /// <summary>
        /// Displays this Message in a separate Line in [ITALIC] (Useful for when a Message is passed as a collection)
        /// </summary>
        NL_I = 6,
        /// <summary>
        /// Displays this Message in a separate Line in [BOLD] and [ITALIC] (Useful for when a Message is passed as a collection)
        /// </summary>
        NL_BI = 7,
        /// <summary>
        /// Displays this Message in a separate Log (Useful for when a Message is passed as a collection)
        /// </summary>
        LOG = 8,
        /// <summary>
        /// Displays this Message in a separate Log in [BOLD] (Useful for when a Message is passed as a collection)
        /// </summary>
        LOG_B = 9,
        /// <summary>
        /// Displays this Message in a separate Log in [ITALIC] (Useful for when a Message is passed as a collection)
        /// </summary>
        LOG_I = 10,
        /// <summary>
        /// Displays this Message in a separate Log in [BOLD] and [ITALIC] (Useful for when a Message is passed as a collection)
        /// </summary>
        LOG_BI = 11,
        /// <summary>
        /// Displays this Message in a separate Log and all its entries (if its a collection) in separate lines 
        /// </summary>
        LOG_NL = 12,
        /// <summary>
        /// Displays this Message in a separate Log and all its entries (if its a collection) in separate lines in [BOLD]
        /// </summary>
        LOG_NL_B = 13,
        /// <summary>
        /// Displays this Message in a separate Log and all its entries (if its a collection) in separate lines in [ITALIC]
        /// </summary>
        LOG_NL_I = 14,
        /// <summary>
        /// Displays this Message in a separate Log and all its entries (if its a collection) in separate lines in [BOLD] and [ITALIC]
        /// </summary>
        LOG_NL_BI = 15,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection)
        /// </summary>
        INDEX = 16,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in [BOLD]
        /// </summary>
        INDEX_B = 17,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in [ITALIC]
        /// </summary>
        INDEX_I = 18,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in [BOLD] and [ITALIC]
        /// </summary>
        INDEX_BI = 19,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Line
        /// </summary>
        INDEX_NL = 20,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Line in [BOLD]
        /// </summary>
        INDEX_NL_B = 21,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Line in [ITALIC]
        /// </summary>
        INDEX_NL_I = 22,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Line in [BOLD] and [ITALIC]
        /// </summary>
        INDEX_NL_BI = 23,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log
        /// </summary>
        INDEX_LOG = 24,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log in [BOLD]
        /// </summary>
        INDEX_LOG_B = 25,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log in [ITALIC]
        /// </summary>
        INDEX_LOG_I = 26,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log in [BOLD] and [ITALIC]
        /// </summary>
        INDEX_LOG_BI = 27,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log and all its entries in separate lines 
        /// </summary>
        INDEX_LOG_NL = 28,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log and all its entries in separate lines in [BOLD]
        /// </summary>
        INDEX_LOG_NL_B = 29,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log and all its entries in separate lines in [ITALIC]
        /// </summary>
        INDEX_LOG_NL_I = 30,
        /// <summary>
        /// Displays the Messages Index next to the Message (Only works if it comes from a collection) <br/>
        /// Displays this Message in a separate Log and all its entries in separate lines in [BOLD] and [ITALIC]
        /// </summary>
        INDEX_LOG_NL_BI = 31
    }

    /// <summary>
    /// Holds the Message to display and its Format settings
    /// </summary>
    internal struct Message
    {
        #region Internal
            /// <summary>
            /// Global ID counter (Is incremented whenever an ID is assigned)
            /// </summary>
            internal static int idCount;
            /// <summary>
            /// Global ID for the collections (Is incremented whenever a Message object is a collection)
            /// </summary>
            internal static int collectionCount;
            /// <summary>
            /// Saves the highest index for each Collection
            /// </summary>
            internal static readonly Dictionary<int, int> MAX_COLLECTION_INDEX = new Dictionary<int, int>();
            /// <summary>
            /// Saves the longest key for each Dictionary
            /// </summary>
            internal static readonly Dictionary<int, int> MAX_KEY_LENGTH = new Dictionary<int, int>();
            /// <summary>
            /// The object passed to the DebugLog-Method
            /// </summary>
            internal readonly object message;
            /// <summary>
            /// This messages ID (Messages that were passed as a collection share the same ID)
            /// </summary>
            internal readonly int groupId;
            /// <summary>
            /// Whether this parameter is to format the Message or not
            /// </summary>
            internal readonly bool isFormatParameter;
            /// <summary>
            /// How many entries are in this Messages Collection?
            /// </summary>
            internal readonly int collectionSize;
            /// <summary>
            /// Collection ID, the entries of this Collection have 
            /// </summary>
            internal readonly int entryCollectionID;
            /// <summary>
            /// Only assigned when the Message object comes from a Collection
            /// </summary>
            internal readonly int collectionID;
            /// <summary>
            /// Index the Message has in the collection it comes from
            /// </summary>
            internal readonly int internalIndex;
            /// <summary>
            /// Displays the Messages index (if it comes from a collection) next to it
            /// </summary>
            internal string index;
            /// <summary>
            /// The Key of this Value (Only set when this Message comes from a KeyValuePair)
            /// </summary>
            internal readonly string key;
            /// <summary>
            /// The Message to display in the Console
            /// </summary>
            private string text;
            /// <summary>
            /// Opening Tag for the Color format
            /// </summary>
            internal string colorStart;
            /// <summary>
            /// Color value to display the Message in
            /// </summary>
            internal string color;
            /// <summary>
            /// Closing Tag for the Color format
            /// </summary>
            internal string colorEnd;
            /// <summary>
            /// Opening Tag for the size format
            /// </summary>
            internal string sizeStart;
            /// <summary>
            /// Size value to display the Message in
            /// </summary>
            internal string size;
            /// <summary>
            /// Closing Tag for the size format
            /// </summary>
            internal string sizeEnd;
            /// <summary>
            /// Opening Tag for the Bold format
            /// </summary>
            internal string boldStart;
            /// <summary>
            /// Closing Tag for the Bold format
            /// </summary>
            internal string boldEnd;
            /// <summary>
            /// Opening Tag for the Italic format
            /// </summary>
            internal string italicStart;
            /// <summary>
            /// Closing Tag for the Italic format
            /// </summary>
            internal string italicEnd;
            /// <summary>
            /// Whether this Message will be displayed in a separate Line
            /// </summary>
            internal bool newLine;
            /// <summary>
            /// Whether this Message will be displayed in a separate Log
            /// </summary>
            internal bool newLog;
            /// <summary>
            /// The formatted Message to display in the Console
            /// </summary>
            internal string formattedMessage;
        #endregion

        #region Properties
            /// <summary>
            /// Start Tags for the Rich Text formatting
            /// </summary>
            internal string StartTags { get { return StringBuilderUtility.Append(boldStart, italicStart, sizeStart, size, colorStart, color); } }
            /// <summary>
            /// The Message to display in the Console
            /// </summary>
            internal string Text
            {
                get
                {
                    // Message comes from a KeyValuePair
                    if (key != string.Empty)
                        return StringBuilderUtility.Append(index, "<b>[ </b>", GetKeySpacing(), " <b><i>,</i></b> ", text, "<b> ]</b> ");
                    // Message comes from a Collection
                    if (index != string.Empty)
                        return StringBuilderUtility.Append(index, text, " ");

                    return StringBuilderUtility.Append(text, " ");
                }
                set { text = value; }
            }

            /// <summary>
            /// Closing Tags for the Rich Text formatting
            /// </summary>
            internal string ClosingTags { get { return StringBuilderUtility.Append(colorEnd, sizeEnd, italicEnd, boldEnd); } }
        #endregion

        /// <summary>
        /// Returns the formatted key, so the spacing between each key and value is the same in the whole Dictionary
        /// </summary>
        private string GetKeySpacing()
        {
            var _space = 1;
            var _key = collectionSize > 0 ? entryCollectionID : collectionID;
            if (newLine)
                _space = key.Length < MAX_KEY_LENGTH[_key] ? (MAX_KEY_LENGTH[_key] - key.Length) * 2 : 1;
            return StringBuilderUtility.Append(key, " ".PadRight(_space));
        }

        /// <summary>
        /// Constructor for FormatParameter
        /// </summary>
        /// <param name="_Message">The argument that was passed to the DebugLog-Method</param>
        /// <param name="_GroupId">The group ID of this Message object</param>
        /// <param name="_IsFormatParameter">Whether this parameter is to format the Message or not</param>
        internal Message(object _Message, int _GroupId, bool _IsFormatParameter)
        {
            message = _Message;
            groupId = _GroupId;
            isFormatParameter = _IsFormatParameter;
            
            collectionSize = 0;
            entryCollectionID = 0;
            collectionID = 0;
            internalIndex = -1;
            key = string.Empty;
            index = string.Empty;
            text = string.Empty;
            colorStart = string.Empty;
            color = string.Empty;
            colorEnd = string.Empty;
            sizeStart = string.Empty;
            size = string.Empty;
            sizeEnd = string.Empty;
            boldStart = string.Empty;
            boldEnd = string.Empty;
            italicStart = string.Empty;
            italicEnd = string.Empty;
            newLine = false;
            newLog = false;
            formattedMessage = string.Empty;
        }
        
        /// <summary>
        /// Constructor for Single objects
        /// </summary>
        /// <param name="_Message">The argument that was passed to the DebugLog-Method</param>
        /// <param name="_GroupId">The group ID of this Message object</param>
        /// <param name="_CollectionID">ID of this collection inside this group</param>
        /// <param name="_InternalIndex">Index the Message has in the collection it comes from</param>
        /// <param name="_Key">The Key of this Value (Only set when this Message comes from a KeyValuePair)</param>
        internal Message(object _Message, int _GroupId, int _CollectionID, int _InternalIndex, string _Key)
        {
            message = _Message;
            groupId = _GroupId;
            collectionID = _CollectionID;
            internalIndex = _InternalIndex;
            key = _Key;
            
            isFormatParameter = false;
            collectionSize = 0;
            entryCollectionID = 0;
            index = string.Empty;
            text = string.Empty;
            colorStart = string.Empty;
            color = string.Empty;
            colorEnd = string.Empty;
            sizeStart = string.Empty;
            size = string.Empty;
            sizeEnd = string.Empty;
            boldStart = string.Empty;
            boldEnd = string.Empty;
            italicStart = string.Empty;
            italicEnd = string.Empty;
            newLine = false;
            newLog = false;
            formattedMessage = string.Empty;
            
            CheckForCollection(_CollectionID, _InternalIndex);
            CheckForKey(_Key, _CollectionID);
        }
        
        /// <summary>
        /// Constructor for Collections
        /// </summary>
        /// <param name="_Message">The argument that was passed to the DebugLog-Method</param>
        /// <param name="_GroupId">The group ID of this Message object</param>
        /// <param name="_CollectionID">ID of this collection inside this group</param>
        /// <param name="_InternalIndex">Index the Message has in the collection it comes from</param>
        /// <param name="_CollectionSize">How many entries are in this Messages Collection</param>
        /// <param name="_EntryCollectionID">Collection ID, the entries of this Collection have </param>
        /// <param name="_Key">The Key of this Value (Only set when this Message comes from a KeyValuePair)</param>
        internal Message(object _Message, int _GroupId, int _CollectionID, int _InternalIndex, int _CollectionSize, int _EntryCollectionID, string _Key)
        {
            message = _Message;
            groupId = _GroupId;
            collectionID = _CollectionID;
            internalIndex = _InternalIndex;
            collectionSize = _CollectionSize;
            entryCollectionID = _EntryCollectionID;
            key = _Key;
            
            isFormatParameter = false;
            index = string.Empty;
            text = string.Empty;
            colorStart = string.Empty;
            color = string.Empty;
            colorEnd = string.Empty;
            sizeStart = string.Empty;
            size = string.Empty;
            sizeEnd = string.Empty;
            boldStart = string.Empty;
            boldEnd = string.Empty;
            italicStart = string.Empty;
            italicEnd = string.Empty;
            newLine = false;
            newLog = false;
            formattedMessage = string.Empty;

            CheckForCollection(_CollectionID, _InternalIndex);
            CheckForKey(_Key, _CollectionID);
        }
        
        /// <summary>
        /// Applies the values of an already existing Message object to a newly created one
        /// </summary>
        /// <param name="_Message">The Message object to copy the values from</param>
        internal Message(Message _Message)
        {
            message = _Message.message;
            isFormatParameter = _Message.isFormatParameter;
            groupId = _Message.groupId;
            collectionID = _Message.collectionID;
            internalIndex = _Message.internalIndex;
            collectionSize = _Message.collectionSize;
            entryCollectionID = _Message.entryCollectionID;
            key = _Message.key;
            index = _Message.index;
            text = _Message.text;
            colorStart = _Message.colorStart;
            color = _Message.color;
            colorEnd = _Message.colorEnd;
            sizeStart = _Message.sizeStart;
            size = _Message.size;
            sizeEnd = _Message.sizeEnd;
            boldStart = _Message.boldStart;
            boldEnd = _Message.boldEnd;
            italicStart = _Message.italicStart;
            italicEnd = _Message.italicEnd;
            newLine = _Message.newLine;
            newLog = _Message.newLog;
            formattedMessage = _Message.formattedMessage;
        }

        /// <summary>
        /// Checks if this Message belongs to a Collection
        /// </summary>
        /// <param name="_CollectionID">The ID of the Collection, this Message belongs to</param>
        /// <param name="_InternalIndex">The index this Message has in its Collection</param>
        private static void CheckForCollection(int _CollectionID, int _InternalIndex)
        {
            if (_CollectionID <= 0) return;
            
                // Key doesn't exist
                if (!MAX_COLLECTION_INDEX.ContainsKey(_CollectionID))
                    MAX_COLLECTION_INDEX.Add(_CollectionID, _InternalIndex);
                // Check if this Messages index is greater than the one saved for this Collection
                else
                    MAX_COLLECTION_INDEX[_CollectionID] = _InternalIndex > MAX_COLLECTION_INDEX[_CollectionID] ? _InternalIndex : MAX_COLLECTION_INDEX[_CollectionID];
        }

        /// <summary>
        /// Checks if this Message belongs to a KeyValuePair
        /// </summary>
        /// <param name="_Key">The Key of this Value</param>
        /// <param name="_CollectionID">The ID of the Collection, this Message belongs to</param>
        private static void CheckForKey(string _Key, int _CollectionID)
        {
            if (_Key == string.Empty) return;
            
                // Key doesn't exist
                if (!MAX_KEY_LENGTH.ContainsKey(_CollectionID))
                    MAX_KEY_LENGTH.Add(_CollectionID, _Key.Length);
                // Check if this Messages key has more character than the one saved for this Collection
                else
                    MAX_KEY_LENGTH[_CollectionID] = _Key.Length > MAX_KEY_LENGTH[_CollectionID] ? _Key.Length : MAX_KEY_LENGTH[_CollectionID];
        }
    }
}