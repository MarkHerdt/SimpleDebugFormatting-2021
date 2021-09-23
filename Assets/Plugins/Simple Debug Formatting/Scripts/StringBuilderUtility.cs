using System.Linq;
using System.Text;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Utility class for "StringBuilder"-Methods
    /// </summary>
    public static class StringBuilderUtility
    {
        #region Privates
            /// <summary>
            /// ObjectPool of StringBuilder
            /// </summary>
            private static readonly StringBuilder[] STRING_BUILDER = { new StringBuilder() };
        #endregion

        /// <summary>
        /// Returns the "StringBuilder" from the pool, if it's not null, otherwise creates a new "StringBuilder" 
        /// </summary>
        private static StringBuilder GetStringBuilder()
        {
            if (STRING_BUILDER.First() == null) return new StringBuilder();
            
                var _tmp = STRING_BUILDER.First();
                STRING_BUILDER[0] = null;
                return _tmp;
        }

        /// <summary>
        /// Returns a "StringBuilder" back to the ObjectPool and clears it
        /// </summary>
        /// <param name="_StringBuilder">The "StringBuilder to return to the pool"</param>
        private static void ReturnStringBuilder(StringBuilder _StringBuilder)
        {
            _StringBuilder.Length = 0;

            STRING_BUILDER[0] = _StringBuilder;
        }

        /// <summary>
        /// Returns the "StringBuilder" back to the ObjectPool and returns the "StringBuilders" string
        /// </summary>
        /// <param name="_StringBuilder">StringBuilder object</param>
        private static string CustomToString(this StringBuilder _StringBuilder)
        {
            var _string = _StringBuilder.ToString();
            ReturnStringBuilder(_StringBuilder);
            return _string;
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <param name="_String4">Appended to the third string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3, string _String4)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);
            _stringBuilder.Append(_String4);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <param name="_String4">Appended to the third string</param>
        /// <param name="_String5">Appended to the fourth string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3, string _String4, string _String5)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);
            _stringBuilder.Append(_String4);
            _stringBuilder.Append(_String5);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <param name="_String4">Appended to the third string</param>
        /// <param name="_String5">Appended to the fourth string</param>
        /// <param name="_String6">Appended to the fifth string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3, string _String4, string _String5, string _String6)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);
            _stringBuilder.Append(_String4);
            _stringBuilder.Append(_String5);
            _stringBuilder.Append(_String6);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <param name="_String4">Appended to the third string</param>
        /// <param name="_String5">Appended to the fourth string</param>
        /// <param name="_String6">Appended to the fifth string</param>
        /// <param name="_String7">Appended to the sixth string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3, string _String4, string _String5, string _String6, string _String7)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);
            _stringBuilder.Append(_String4);
            _stringBuilder.Append(_String5);
            _stringBuilder.Append(_String6);
            _stringBuilder.Append(_String7);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <param name="_String4">Appended to the third string</param>
        /// <param name="_String5">Appended to the fourth string</param>
        /// <param name="_String6">Appended to the fifth string</param>
        /// <param name="_String7">Appended to the sixth string</param>
        /// <param name="_String8">Appended to the seventh string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3, string _String4, string _String5, string _String6, string _String7, string _String8)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);
            _stringBuilder.Append(_String4);
            _stringBuilder.Append(_String5);
            _stringBuilder.Append(_String6);
            _stringBuilder.Append(_String7);
            _stringBuilder.Append(_String8);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in the order they're passed into this Method via the "StringBuilder"
        /// </summary>
        /// <param name="_String1">First string to append the others to</param>
        /// <param name="_String2">Appended to the first string</param>
        /// <param name="_String3">Appended to the second string</param>
        /// <param name="_String4">Appended to the third string</param>
        /// <param name="_String5">Appended to the fourth string</param>
        /// <param name="_String6">Appended to the fifth string</param>
        /// <param name="_String7">Appended to the sixth string</param>
        /// <param name="_String8">Appended to the seventh string</param>
        /// <param name="_String9">Appended to the eight string</param>
        /// <param name="_String10">Appended to the ninth string</param>
        /// <param name="_String11">Appended to the tenth string</param>
        /// <param name="_String12">Appended to the eleventh string</param>
        /// <param name="_String13">Appended to the twelfth string</param>
        /// <param name="_String14">Appended to the thirteenth string</param>
        /// <param name="_String15">Appended to the fourteenth string</param>
        /// <param name="_String16">Appended to the fifteenth string</param>
        /// <param name="_String17">Appended to the sixteenth string</param>
        /// <returns>Returns the appended strings</returns>
        public static string Append(string _String1, string _String2, string _String3, string _String4, string _String5, string _String6, string _String7, string _String8, string _String9, string _String10, string _String11, string _String12, string _String13, string _String14, string _String15, string _String16, string _String17)
        {
            var _stringBuilder = GetStringBuilder();

            _stringBuilder.Append(_String1);
            _stringBuilder.Append(_String2);
            _stringBuilder.Append(_String3);
            _stringBuilder.Append(_String4);
            _stringBuilder.Append(_String5);
            _stringBuilder.Append(_String6);
            _stringBuilder.Append(_String7);
            _stringBuilder.Append(_String8);
            _stringBuilder.Append(_String9);
            _stringBuilder.Append(_String10);
            _stringBuilder.Append(_String11);
            _stringBuilder.Append(_String12);
            _stringBuilder.Append(_String13);
            _stringBuilder.Append(_String14);
            _stringBuilder.Append(_String15);
            _stringBuilder.Append(_String16);
            _stringBuilder.Append(_String17);

            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Returns a substring that is created with the "StringBuilder"'s "Append()"-Method
        /// </summary>
        /// <param name="_String">The string to create a substring of</param>
        /// <param name="_StartIndex">Index to start the substring at</param>
        public static string Substring(string _String, int _StartIndex)
        {
            return GetStringBuilder().Append(_String, _StartIndex, _String.Length - _StartIndex).CustomToString();
        }
        
        /// <summary>
        /// Returns a substring that is created with the "StringBuilder"'s "Append()"-Method
        /// </summary>
        /// <param name="_String">The string to create a substring of</param>
        /// <param name="_StartIndex">Index to start the substring at</param>
        /// <param name="_Length">Length of the substring</param>
        public static string Substring(string _String, int _StartIndex, int _Length)
        {
            if (_StartIndex < 0)
                _StartIndex = 0;
            if (_StartIndex > _String.Length)
                _StartIndex = _String.Length - 1;
            if (_Length < 0)
                _Length = 0;
            if (_StartIndex > _String.Length - _Length)
                _StartIndex = _String.Length - _Length;
            if (_Length == 0)
                return string.Empty;
            return _StartIndex == 0 && _Length == _String.Length ? _String : GetStringBuilder().Append(_String, _StartIndex, _Length).CustomToString();
        }

        /// <summary>
        /// Appends all chars in "_Values", separated by "_Separator", with the "StringBuilder"'s "Append()"-Method
        /// </summary>
        /// <param name="_Values">Array of chars to join</param>
        /// <param name="_Separator">Separates each string in "_Values"</param>
        /// <returns>Returns all chars from the Array joined into one string</returns>
        public static string Join(char[] _Values, string _Separator = "")
        {
            var _stringBuilder = GetStringBuilder();

            for (var i = 0; i < _Values.Length; i++)
            {
                _stringBuilder.Append(_Values[i]);
                if (i + 1 != _Values.Length)
                    _stringBuilder.Append(_Separator);
            }
            
            return _stringBuilder.CustomToString();
        }
        
        /// <summary>
        /// Appends all strings in "_Values", separated by "_Separator", with the "StringBuilder"'s "Append()"-Method
        /// </summary>
        /// <param name="_Values">Array of strings to join</param>
        /// <param name="_Separator">Separates each string in "_Values"</param>
        /// <returns>Returns all strings from the Array joined into one string</returns>
        public static string Join(string[] _Values, string _Separator = "")
        {
            var _stringBuilder = GetStringBuilder();

            for (var i = 0; i < _Values.Length; i++)
            {
                _stringBuilder.Append(_Values[i]);
                if (i + 1 != _Values.Length)
                    _stringBuilder.Append(_Separator);
            }
            
            return _stringBuilder.CustomToString();
        }
    }
}