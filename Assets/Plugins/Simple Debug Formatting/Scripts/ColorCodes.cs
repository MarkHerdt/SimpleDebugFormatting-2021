using UnityEngine;

namespace SimpleDebugFormatting
{
    /// <summary>
    /// Holds color hex codes for the RichText formatting and Color32 values
    /// </summary>
    public struct ColorCodes
    {
        #region Hex Codes
            /// <summary>
            /// Black <br/>
            /// color=#FFFFFF
            /// </summary>
            public const string BLACK_HEX = "<color=#000000>";
            /// <summary>
            /// White <br/>
            /// color=#000000
            /// </summary>
            public const string WHITE_HEX = "<color=#FFFFFF>";
            /// <summary>
            /// Silver <br/>
            /// color=#BABABA
            /// </summary>
            public const string SILVER_HEX = "<color=#BABABA>";
            /// <summary>
            /// Green <br/>
            /// color=#4abf40
            /// </summary>
            public const string SUCCESS_HEX = "<color=#4abf40>";
            /// <summary>
            /// Orange <br/>
            /// color=#ff6a00
            /// </summary>
            public const string WARNING_HEX = "<color=#ff6a00>";
            /// <summary>
            /// Red <br/>
            /// color=#ec1313
            /// </summary>
            public const string ERROR_HEX = "<color=#ec1313>";
            /// <summary>
            /// Blue <br/>
            /// color=#6C95EB
            /// </summary>
            public const string KEYWORD_HEX = "<color=#6C95EB>";
            /// <summary>
            /// Turquoise <br/>
            /// color=#4EC9B0
            /// </summary>
            public const string CLASS_HEX = "<color=#4EC9B0>";
            /// <summary>
            /// Green <br/>
            /// color=#80B780
            /// </summary>
            public const string STRUCT_HEX = "<color=#80B780>";
            /// <summary>
            /// Green <br/>
            /// color=#B7D680
            /// </summary>
            public const string INTERFACE_HEX = "<color=#B7D680>";
            /// <summary>
            /// Green <br/>
            /// color=#9FCC75
            /// </summary>
            public const string ENUM_HEX = "<color=#9FCC75>";
            /// <summary>
            /// Yellow <br/>
            /// color=#FFFF80
            /// </summary>
            public const string METHOD_HEX = "<color=#FFFF80>";
            /// <summary>
            /// Blue <br/>
            /// color=#8FD2F8
            /// </summary>
            public const string PARAMETER_HEX = "<color=#8FD2F8>";
            /// <summary>
            /// Brown <br/>
            /// color=#D59C7C
            /// </summary>
            public const string STRING_HEX = "<color=#D59C7C>";
            /// <summary>
            /// Green <br/>
            /// color=#699878
            /// </summary>
            public const string NUMBER_HEX = "<color=#699878>";
            /// <summary>
            /// Blue <br/>
            /// color=#9CDCFE
            /// </summary>
            public const string ARGUMENT_HEX = "<color=#9CDCFE>";
            /// <summary>
            /// Green <br/>
            /// color=#608B4E
            /// </summary>
            public const string COMMENT_HEX = "<color=#608B4E>";
            /// <summary>
            /// Blue <br/>
            /// color=#7FD6FD
            /// </summary>
            public const string PREFAB_HEX = "<color=#7FD6FD>";
        #endregion

        #region Color32
            /// <summary>
            /// Black <br/>
            /// new Color32(0, 0, 0, 255);
            /// </summary>
            public static readonly Color32 BLACK_32 = new Color32(0, 0, 0, 255);
            /// <summary>
            /// White <br/>
            /// new Color32(255, 255, 255, 255);
            /// </summary>
            public static readonly Color32 WHITE_32 = new Color32(255, 255, 255, 255);
            /// <summary>
            /// Silver <br/>
            /// new Color32(186, 186, 186, 255);
            /// </summary>
            public static readonly Color32 SILVER_32 = new Color32(186, 186, 186, 255);
            /// <summary>
            /// Green <br/>
            /// new Color32(74, 191, 64, 255);
            /// </summary>
            public static readonly Color32 SUCCESS_32 = new Color32(74, 191, 64, 255);
            /// <summary>
            /// Orange <br/>
            /// new Color32(255, 106, 0, 255);
            /// </summary>
            public static readonly Color32 WARNING_32 = new Color32(255, 106, 0, 255);
            /// <summary>
            /// Red <br/>
            /// new Color32(236, 19, 19, 255);
            /// </summary>
            public static readonly Color32 ERROR_32 = new Color32(236, 19, 19, 255);
            /// <summary>
            /// Blue <br/>
            /// new Color32(108, 149, 235, 255);
            /// </summary>
            public static readonly Color32 KEYWORD_32 = new Color32(108, 149, 235, 255);
            /// <summary>
            /// Turquoise <br/>
            /// new Color32(78, 201, 176, 255);
            /// </summary>
            public static readonly Color32 CLASS_32 = new Color32(78, 201, 176, 255);
            /// <summary>
            /// Green <br/>
            /// new Color32(128, 183, 128, 255);
            /// </summary>
            public static readonly Color32 STRUCT_32 = new Color32(128, 183, 128, 255);
            /// <summary>
            /// Green <br/>
            /// new Color32(183, 214, 128, 255);
            /// </summary>
            public static readonly Color32 INTERFACE_32 = new Color32(183, 214, 128, 255);
            /// <summary>
            /// Green <br/>
            /// new Color32(159, 204, 163, 117);
            /// </summary>
            public static readonly Color32 ENUM_32 = new Color32(159, 204, 117, 255);
            /// <summary>
            /// Yellow <br/>
            /// new Color32(255, 255, 128, 255);
            /// </summary>
            public static readonly Color32 METHOD_32 = new Color32(255, 255, 128, 255);
            /// <summary>
            /// Blue <br/>
            /// new Color32(143, 210, 248, 255)
            /// </summary>
            public static readonly Color32 PARAMETER_32 = new Color32(143, 210, 248, 255);
            /// <summary>
            /// Brown <br/>
            /// new Color32(213, 156, 124, 255);
            /// </summary>
            public static readonly Color32 STRING_32 = new Color32(213, 156, 124, 255);
            /// <summary>
            /// Green <br/>
            /// new Color32(105, 152, 120, 255);
            /// </summary>
            public static readonly Color32 NUMBER_32 = new Color32(105, 152, 120, 255);
            /// <summary>
            /// Blue <br/>
            /// new Color32(156, 220, 254, 255);
            /// </summary>
            public static readonly Color32 ARGUMENT_32 = new Color32(156, 220, 254, 255);
            /// <summary>
            /// Green <br/>
            /// new Color32(96, 139, 78, 255);
            /// </summary>
            public static readonly Color32 COMMENT_32 = new Color32(96, 139, 78, 255);
            /// <summary>
            /// Blue <br/>
            /// new Color32(127, 214, 253, 255);
            /// </summary>
            public static readonly Color32 PREFAB_32 = new Color32(127, 214, 253, 255);
        #endregion
    }
}