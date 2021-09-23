using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Extension Methods for Type-objects
    /// </summary>
    public static class TypeExtension
    {
        #region Privates
            /// <summary>
            /// Locks the "CACHED_NICE_NAMES"-Dictionary while it's being used
            /// </summary>
            private static readonly object LOCK = new object();
            /// <summary>
            /// Cached storage for already formatted TypeNames
            /// </summary>
            private static readonly Dictionary<Type, string> CACHED_NICE_NAMES = new Dictionary<Type, string>();
            /// <summary>
            /// Lookup table to convert TypeNames to a nicer looking variant
            /// </summary>
            private static readonly Dictionary<string, string> NICE_TYPE_NAMES = new Dictionary<string, string>
            {
                { "Boolean", "bool" },
                { "Byte", "byte" },
                { "SByte", "sbyte" },
                { "Int16", "short" },
                { "UInt16", "ushort" },
                { "Int32", "int" },
                { "UInt32", "uint" },
                { "Int64", "long" },
                { "UInt64", "ulong" },
                { "Decimal", "decimal" },
                { "Single", "float" },
                { "Double", "double" },
                { "Char", "char" },
                { "String", "string" },
                { "Boolean[]", "bool[]" },
                { "Byte[]", "byte[]" },
                { "SByte[]", "sbyte[]" },
                { "Int16[]", "short[]" },
                { "UInt16[]", "ushort[]" },
                { "Int32[]", "int[]" },
                { "UInt32[]", "uint[]" },
                { "Int64[]", "long[]" },
                { "UInt64[]", "ulong[]" },
                { "Decimal[]", "decimal[]" },
                { "Single[]", "float[]" },
                { "Double[]", "double[]" },
                { "Char[]", "char[]" },
                { "String[]", "string[]" }
            };
        #endregion

        /// <summary>
        /// Clears all cached TypeNames from the Dictionary
        /// </summary>
        public static void ClearCachedNames()
        {
            lock (LOCK)
                CACHED_NICE_NAMES.Clear();
        }
        
        /// <summary>
        /// Returns the TypeName in a nicer looking variant
        /// </summary>
        /// <param name="_Type">The Type to get the name of</param>
        internal static string GetNiceName(this Type _Type)
        {
            if (!Settings.ShowParentTypes)
                return GetCachedNiceName(_Type);

            return _Type.IsNested && !_Type.IsGenericParameter ? StringBuilderUtility.Append(GetNiceName(_Type.DeclaringType), ".", GetCachedNiceName(_Type)) : GetCachedNiceName(_Type);
        }
        
        /// <summary>
        /// Tries to get the TypeName variant from a cached Dictionary, if that Type has been converted before
        /// </summary>
        /// <param name="_Type">The Type to get the name of</param>
        /// <returns>Returns a nicer looking TypeName</returns>
        private static string GetCachedNiceName(Type _Type)
        {
            string _niceName;
            lock (LOCK)
            {
                if (CACHED_NICE_NAMES.TryGetValue(_Type, out _niceName)) return _niceName;
                    
                    _niceName = CreateNiceName(_Type);
                    CACHED_NICE_NAMES.Add(_Type, _niceName);
            }
            return _niceName;
        }
        
        /// <summary>
        /// Creates a nicer looking TypeName variant
        /// </summary>
        /// <param name="_Type">The Type to get the name of</param>
        /// <returns>Returns the formatted TypeName</returns>
        private static string CreateNiceName(Type _Type)
        {
            if (_Type.IsArray)
            {
                var _rank = _Type.GetArrayRank();
                return StringBuilderUtility.Append(GetNiceName(_Type.GetElementType()), _rank == 1 ? "[]" : "[,]");
            }
            if (_Type.InheritsFrom(typeof (Nullable<>)))
                return StringBuilderUtility.Append(GetNiceName(_Type.GetGenericArguments()[0]), "?");
            if (_Type.IsByRef)
                return StringBuilderUtility.Append("ref", GetNiceName(_Type.GetElementType()));
            if (_Type.IsGenericParameter || !_Type.IsGenericType)
                return _Type.GetNiceTypeName();
            
            var _stringBuilder = new StringBuilder();
            var _name = _Type.Name;
            var _length = _name.IndexOf("`", StringComparison.Ordinal);
            var _arguments = _Type.GetGenericArguments();
            
            _stringBuilder.Append(_length != -1 ? StringBuilderUtility.Substring(_name, 0, _length) : _name);
            _stringBuilder.Append('<');
            for (var i = 0; i < _arguments.Length; ++i)
            {
                var _argument = _arguments[i];
                if (i != 0)
                    _stringBuilder.Append(", ");
                _stringBuilder.Append(GetNiceName(_argument));
            }
            _stringBuilder.Append('>');
            
            return _stringBuilder.ToString();
        }
        
        /// <summary>
        /// Checks whether a type inherits or implements another Type
        /// </summary>
        /// <param name="_Type">The Type to check</param>
        /// <param name="_BaseType">The Type to check the inheritance from</param>
        private static bool InheritsFrom(this Type _Type, Type _BaseType)
        {
            if (_BaseType.IsAssignableFrom(_Type))
                return true;
            if (_Type.IsInterface && !_BaseType.IsInterface)
                return false;
            if (_BaseType.IsInterface)
                return _Type.GetInterfaces().Contains(_BaseType);
            for (var _type = _Type; _type != null; _type = _type.BaseType)
            {
                if (_type == _BaseType || _BaseType.IsGenericTypeDefinition && _type.IsGenericType && _type.GetGenericTypeDefinition() == _BaseType)
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// Replaces the TypeName with a nicer looking variant
        /// </summary>
        /// <param name="_Type">The type to get the name of</param>
        /// <returns>Returns the nicely converted TypeName</returns>
        private static string GetNiceTypeName(this Type _Type)
        {
            var _key = _Type.Name;
            string _empty;
            
            if (NICE_TYPE_NAMES.TryGetValue(_key, out _empty))
                _key = _empty;
            
            return _key;
        }
    }
    
    /// <summary>
    /// Extension Methods for String-objects
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Returns true if this string is null, empty, or contains only whitespace
        /// </summary>
        /// <param name="_String">The string to check</param>
        public static bool IsNullOrEmptyOrWhitespace(this string _String)
        {
            return string.IsNullOrEmpty(_String) || _String.All(char.IsWhiteSpace);
        }

        /// <summary>
        /// Same as "string.Substring" but returns "string.Empty" if the StartIndex is negative or >= this.string.length 
        /// </summary>
        /// <param name="_String">The string to create a substring og</param>
        /// <param name="_StartIndex">The index to start at</param>
        public static string TrySubstring(this string _String, int _StartIndex)
        {
            if (_StartIndex == -1 || _StartIndex >= _String.Length)
                return string.Empty;

            return StringBuilderUtility.Substring(_String, _StartIndex);
        }
    }
}