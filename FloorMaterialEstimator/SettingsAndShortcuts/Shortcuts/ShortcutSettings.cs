//-------------------------------------------------------------------------------//
// <copyright file="ShortcutSettings.cs" company="Bruun Estimating, LLC">        // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace SettingsLib
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Utilities;

    public class ShortcutSettings
    {
        static Dictionary<string, string> appConfig;

        public static UserType UserType;

        public static int[] AreaStateKeyToActionMapRght = new int[256];
        public static int[] AreaStateKeyToActionMapLeft = new int[256];

        public static int[] AreaStateKeyToActionMap => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? AreaStateKeyToActionMapRght : AreaStateKeyToActionMapLeft;

        public static int AreaStateKeyToActionMapValue(int keyAscii)
        {
           if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded)
            {
                return AreaStateKeyToActionMapRght[keyAscii];
            }

            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.LeftHanded)
            {
                return AreaStateKeyToActionMapLeft[keyAscii];
            }

            return -1;
        }

        public static int[] LineStateKeyToActionMapRght = new int[256];
        public static int[] LineStateKeyToActionMapLeft = new int[256];

        public static int[] LineStateKeyToActionMap => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? LineStateKeyToActionMapRght : LineStateKeyToActionMapLeft;

        public static int LineStateKeyToActionMapValue(int keyAscii)
        {
            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded)
            {
                return LineStateKeyToActionMapRght[keyAscii];
            }

            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.LeftHanded)
            {
                return LineStateKeyToActionMapLeft[keyAscii];
            }

            return -1;
        }

        public static int[] SeamStateKeyToActionMapRght = new int[256];
        public static int[] SeamStateKeyToActionMapLeft = new int[256];

        public static int[] SeamStateKeyToActionMap => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? SeamStateKeyToActionMapRght : SeamStateKeyToActionMapLeft;

        public static int SeamStateKeyToActionMapValue(int keyAscii)
        {
            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded)
            {
                return SeamStateKeyToActionMapRght[keyAscii];
            }

            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.LeftHanded)
            {
                return SeamStateKeyToActionMapRght[keyAscii];
            }

            return -1;
        }

        public static int[] MenuStateKeyToActionMapRght = new int[256];
        public static int[] MenuStateKeyToActionMapLeft = new int[256];

        public static int[] MenuStateKeyToActionMap => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? MenuStateKeyToActionMapRght : MenuStateKeyToActionMapLeft;

        public static int MenuStateKeyToActionMapValue(int keyAscii)
        {
            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded)
            {
                return MenuStateKeyToActionMapRght[keyAscii];
            }

            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.LeftHanded)
            {
                return MenuStateKeyToActionMapRght[keyAscii];
            }

            return -1;
        }

        public static int[] MiscStateKeyToActionMapRght = new int[256];
        public static int[] MiscStateKeyToActionMapLeft = new int[256];

        public static int[] MiscStateKeyToActionMap => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? MiscStateKeyToActionMapRght : MiscStateKeyToActionMapLeft;

        public static int MiscStateKeyToActionMapValue(int keyAscii)
        {
            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded)
            {
                return MiscStateKeyToActionMapRght[keyAscii];
            }

            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.LeftHanded)
            {
                return MiscStateKeyToActionMapLeft[keyAscii];
            }

            return -1;
        }

        public static Dictionary<string, int> StringToAsciiTable = new Dictionary<string, int>()
        {
            #region String to ascii values
            //["a"] = 97,
            //["b"] = 98,
            //["c"] = 99,
            //["d"] = 100,
            //["e"] = 101,
            //["f"] = 102,
            //["g"] = 103,
            //["h"] = 104,
            //["i"] = 105,
            //["j"] = 106,
            //["k"] = 107,
            //["l"] = 108,
            //["m"] = 109,
            //["n"] = 110,
            //["o"] = 111,
            //["p"] = 112,
            //["q"] = 113,
            //["r"] = 114,
            //["s"] = 115,
            //["t"] = 116,
            //["u"] = 117,
            //["v"] = 118,
            //["w"] = 119,
            //["x"] = 120,
            //["y"] = 121,
            //["z"] = 122,
            ["A"] = 65,
            ["B"] = 66,
            ["C"] = 67,
            ["D"] = 68,
            ["E"] = 69,
            ["F"] = 70,
            ["G"] = 71,
            ["H"] = 72,
            ["I"] = 73,
            ["J"] = 74,
            ["K"] = 75,
            ["L"] = 76,
            ["M"] = 77,
            ["N"] = 78,
            ["O"] = 79,
            ["P"] = 80,
            ["Q"] = 81,
            ["R"] = 82,
            ["S"] = 83,
            ["T"] = 84,
            ["U"] = 85,
            ["V"] = 86,
            ["W"] = 87,
            ["X"] = 88,
            ["Y"] = 89,
            ["Z"] = 90,
            ["F1*"] = 112, // + 128,
            ["F2*"] = 113, // + 128,
            ["F3*"] = 114, // + 128,
            ["F4*"] = 115, // + 128,
            ["F5"] = 116, // + 128,
            ["F6"] = 117, // + 128,
            ["F7"] = 118, // + 128,
            ["F8"] = 119, // + 128,
            ["F9"] = 120, // + 128,
            ["F10"] = 121, // + 128,
            ["F11"] = 122, // + 128,
            ["F12"] = 123, // + 128,
            ["00*"] = 123, // keypad '00' programmed to F12
            ["0*"] = 96, // + 128,
            ["1*"] = 97, // + 128,
            ["2*"] = 98, // + 128,
            ["3*"] = 99, // + 128,
            ["4*"] = 100, // + 128,
            ["5*"] = 101, // + 128,
            ["6*"] = 102, // + 128,
            ["7*"] = 103, // + 128,
            ["8*"] = 104, // + 128,
            ["9*"] = 105, // + 128,
            ["LEFTARROW"] = 37,
            ["UPARROW"] = 38,
            ["RIGHTARROW"] = 39,
            ["DOWNARROW"] = 40,
            ["ESC"] = 27,
            ["TAB"] = 9,
            ["ENTER*"] = 13,
            ["END"] = 35,
            ["HOME"] = 36,
            ["INSERT"] = 45,
            ["BS*"] = 8,
            ["DEL*"] = 110,
            ["`"] = 192,
            ["'"] = 222,
            [";"] = 186,
            ["."] = 190,
            [","] = 188,
            ["/"] = 191,
            ["\\"] = 220,
            ["="] = 187,
            ["["] = 219,
            ["]"] = 221,
            ["|"] = 124,
            [" "] = 32,
            ["**"] = 106,
            ["+*"] = 107,
            ["-*"] = 109,
            ["SPACEBAR"] = 32
            #endregion
        };

        public static string[] AsciiToStringTable;

        public static bool IsValidShortcut(string shortcut)
        {
            return StringToAsciiTable.ContainsKey(shortcut.Trim());
        }

        public static string[] AreaStateShortcutNames = new string[]
        {
            "Complete shape"
            ,"Complete shape by intersection"
            ,"Complete shape by maximum area"
            ,"Complete shape by minimum area"
            ,"Complete rotated shape"
            ,"Erase last line"
            ,"Cancel shape in progress"
            ,"Delete selected Shape"
            ,"Toggle zero line mode"
            ,"Area takeout"
            ,"Takeout and fill"
            ,"Embed shape"
            ,"Copy and paste"
            ,"Toggle fixed width"
            ,"Process fixed width jump"
            ,"Set selected areas to current material"
        };

        public static string[] AreaStateShortcutKeys = new string[]
        {
            "AreaStateCompleteShape"
            ,"AreaStateCompleteShapeByIntersection"
            ,"AreaStateCompleteShapeByMax"
            ,"AreaStateCompleteShapeByMin"
            ,"AreaStateCompleteRotatedShape"
            ,"AreaStateEraseLastLine"
            ,"AreaStateCancelShapeInProgress"
            ,"AreaStateDeleteSelectedShape"
            ,"AreaStateToggleZeroLineMode"
            ,"AreaStateTakeout"
            ,"AreaStateTakeoutAndFill"
            ,"AreaStateEmbedShape"
            ,"AreaStateCopyAndPaste"
            ,"AreaStateToggleFixedWidth"
            ,"AreaStateProcessFixedWidthJump"
            ,"AreaStateD2XLineHGuides"
            ,"AreaStateD2XLineVGuides"
            ,"AreaStateSetSelectedAreas"
        };

        public enum AreaShortcutKeys  {
                CompleteShape = 0
                , CompleteShapeByIntersection
                , CompleteShapeByMax
                , CompleteShapeByMin
                , CompleteRotatedShape
                , EraseLastLine
                , CancelShapeInProgress
                , DeleteSelectedShape
                , ToggleZeroLineMode
                , Takeout
                , TakeoutAndFill
                , EmbedShape
                , CopyAndPaste
                , ToggleFixedWidth
                , ProcessFixedWidthJump
                , D2XLineHGuides
                , D2XLineVGuides
                , SetSelectedAreas
        };


        public static string[] LineStateShortcutNames = new string[]
        {
            "Jump"
            ,"1X Line mode"
            ,"2X Line mode"
            ,"Duplicate line"
            ,"Toggle door takeout"
            ,"Erase Last Line"
            ,"Delete selected Line"
            ,"2x lines between H Guides"
            ,"2x lines between V Guides"
            ,"Set selected lines"
        };

        public static string[] LineStateShortcutKeys = new string[]
        {
            "LineStateJump"
            ,"LineStateSingleLine"
            ,"LineStateDoubleLine"
            ,"LineStateDuplicateLine"
            ,"LineStateToggleDoorTakeout"
            ,"LineStateEraseLastLine"
            ,"LineStateDeleteSelectedLine"
            ,"LineStateD2XLineHGuides"
            ,"LineStateD2XLineVGuides"
            ,"LineStateSetSelectedLines"
        };

        public enum LineShortcutKeys
        {
                Jump = 0
                , SingleLine
                , DoubleLine
                , DuplicateLine
                , ToggleDoorTakeout
                , EraseLastLine
                , DeleteSelectedLine
                , D2XLineHGuides
                , D2XLineVGuides
                , SetSelectedLines
        }

        public static string[] SeamStateShortcutNames = new string[]
        {
            "Erase Last Line"
            ,"Complete Shape"
            ,"Cancel Shape In Progress"
        };

        public static string[] SeamStateShortcutKeys = new string[]
        {
            "SeamStateEraseLastLine"
            ,"SeamStateCompleteShape"
            ,"SeamStateCancelShapeInProgress"
        };

        public static string[] MenuStateShortcutNames = new string[]
        {
            "Save project"
            ,"Zoom in"
            ,"Zoom out"
            ,"Fit to canvas"
            ,"Pan mode"
            ,"Draw mode"
            ,"Area mode"
            ,"Line mode"
            ,"Seam mode"
            ,"Show field guides"
            ,"Hide field guides"
            ,"Delete field guides"
            ,"Toggle highlight seam areas"
            ,"Toggle Align to grid"
            ,"Reverse Align to grid action"
            ,"Line measuring tool"
            ,"Overs/unders"
            ,"Full screen mode"
        };

        public static string[] MenuStateShortcutKeys = new string[]
        {
            "MenuStateSaveProject"
            ,"MenuStateZoomIn"
            ,"MenuStateZoomOut"
            ,"MenuStateFitToCanvas"
            ,"MenuStatePanMode"
            ,"MenuStateDrawMode"
            ,"MenuStateAreaMode"
            ,"MenuStateLineMode"
            ,"MenuStateSeamMode"
            ,"MenuStateShowFieldGuides"
            ,"MenuStateHideFieldGuides"
            ,"MenuStateDeleteFieldGuides"
            ,"MenuStateToggleHighlightSeamAreas"
            ,"MenuStateAlignToGrid"
            ,"MenusStateReverseAlignToGridShiftKey"
            ,"MenuStateLineMeasuringTool"
            ,"MenuStateOversUnders"
            ,"MenuStateFullScreenMode"
        };

        public static string[] MiscStateShortcutKeys = new string[]
        {
            "MiscStateZoomIn"
            ,"MiscStateZoomOut"
            ,"MiscStateToggleShowMeasuringStick"
            ,"MiscShowExtendedCrosshairs"
            ,"MiscShowUnseamedAreas"
            ,"MiscShowZeroAreas"
        };

        public static string[] MiscStateShortcutNames = new string[]
        {
            "Zoom In"
            ,"Zoom Out"
            ,"Toggle Show Measuring Stick"
            ,"Show Extended Crosshairs"
            ,"Show Unseamed Areas"
            ,"Show Zero Areas"
        };



        public static string[] AreaStateShortcutsRght;
        public static string[] AreaStateShortcutsLeft;

        public static string[] AreaStateShortcutDescriptionsRght;
        public static string[] AreaStateShortcutDescriptionsLeft;

        public static string[] AreaStateShortcuts => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? AreaStateShortcutsRght : AreaStateShortcutsLeft;
        public static string[] AreaStateShortcutDescriptions => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? AreaStateShortcutDescriptionsRght : AreaStateShortcutDescriptionsLeft;

        public static string[] LineStateShortcutsRght;
        public static string[] LineStateShortcutsLeft;

        public static string[] LineStateShortcutDescriptionsRght;
        public static string[] LineStateShortcutDescriptionsLeft;

        public static string[] LineStateShortcuts => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? LineStateShortcutsRght : LineStateShortcutsLeft;
        public static string[] LineStateShortcutDescriptions => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? LineStateShortcutDescriptionsRght : LineStateShortcutDescriptionsLeft;

        public static string[] SeamStateShortcutsRght;
        public static string[] SeamStateShortcutsLeft;

        public static string[] SeamStateShortcutDescriptionsLeft;
        public static string[] SeamStateShortcutDescriptionsRght;

        public static string[] SeamStateShortcuts => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? SeamStateShortcutsRght : SeamStateShortcutsLeft;
        public static string[] SeamStateShortcutDescriptions => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? SeamStateShortcutDescriptionsRght : SeamStateShortcutDescriptionsLeft;

        public static string[] MenuStateShortcutsRght;
        public static string[] MenuStateShortcutsLeft;

        public static string[] MenuStateShortcutDescriptionsLeft;
        public static string[] MenuStateShortcutDescriptionsRght;

        public static string[] MenuStateShortcuts => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? MenuStateShortcutsRght : MenuStateShortcutsLeft;
        public static string[] MenuStateShortcutDescriptions => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? MenuStateShortcutDescriptionsRght : MenuStateShortcutDescriptionsLeft;

        public static string[] MiscStateShortcutsRght;
        public static string[] MiscStateShortcutsLeft;

        public static string[] MiscStateShortcutDescriptionsRght;
        public static string[] MiscStateShortcutDescriptionsLeft;

        public static string[] MiscStateShortcuts => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? MiscStateShortcutsRght : MiscStateShortcutsLeft;
        public static string[] MiscStateShortcutDescriptions => GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? MiscStateShortcutDescriptionsRght : MiscStateShortcutDescriptionsLeft;

        public static int HorizontalLineFromGuides { get; set; } = 44;
        public static int VerticalLineFromGuides { get; set; } = 62;

        private static bool test = false;
        public static void Initialize(Dictionary<string, string> appConfig, UserType userType)
        {
            ShortcutSettings.UserType = userType;

            ShortcutSettings.appConfig = appConfig;

            AsciiToStringTable = new string[256];

            for (int i = 0; i < 256; i++)
            {
                AsciiToStringTable[i] = string.Empty;
            }

            foreach (KeyValuePair<string, int> kvp in StringToAsciiTable)
            {
                AsciiToStringTable[kvp.Value] = kvp.Key;
            }

            Initialize(AreaStateShortcutKeys, AreaStateKeyToActionMapRght, AreaStateKeyToActionMapLeft, ref AreaStateShortcutsRght, ref AreaStateShortcutsLeft, ref AreaStateShortcutDescriptionsRght, ref AreaStateShortcutDescriptionsLeft);
            Initialize(LineStateShortcutKeys, LineStateKeyToActionMapRght, LineStateKeyToActionMapLeft, ref LineStateShortcutsRght, ref LineStateShortcutsLeft, ref LineStateShortcutDescriptionsRght, ref LineStateShortcutDescriptionsLeft);
            Initialize(SeamStateShortcutKeys, SeamStateKeyToActionMapRght, SeamStateKeyToActionMapLeft, ref SeamStateShortcutsRght, ref SeamStateShortcutsLeft, ref SeamStateShortcutDescriptionsRght, ref SeamStateShortcutDescriptionsLeft);
            Initialize(MenuStateShortcutKeys, MenuStateKeyToActionMapRght, MenuStateKeyToActionMapLeft, ref MenuStateShortcutsRght, ref MenuStateShortcutsLeft, ref MenuStateShortcutDescriptionsRght, ref MenuStateShortcutDescriptionsLeft);

            test = true;

            Initialize(MiscStateShortcutKeys, MiscStateKeyToActionMapRght, MiscStateKeyToActionMapLeft, ref MiscStateShortcutsRght, ref MiscStateShortcutsLeft, ref MiscStateShortcutDescriptionsRght, ref MiscStateShortcutDescriptionsLeft);


            // Hardcoded. Eventually implement multiple shortcuts

            // Keyboard delete key is now same as hitting 'D'

            AreaStateKeyToActionMapRght[46] = 6;
            AreaStateKeyToActionMapLeft[46] = 6;
            LineStateKeyToActionMapRght[46] = 6;
            LineStateKeyToActionMapLeft[46] = 6;
        }

        private static void Initialize(
            string[] shortcutKey
            , int[] keyToActionMapRght
            , int[] keyToActionMapLeft
            , ref string[] shortcutsRght
            , ref string[] shortcutsLeft
            , ref string[] shortcutDescriptionsRght
            , ref string[] shortcutDescriptionsLeft)
        {
            int shortcutCount = shortcutKey.Length;

            shortcutsRght = new string[shortcutCount];
            shortcutsLeft = new string[shortcutCount];
            shortcutDescriptionsRght = new string[shortcutCount];
            shortcutDescriptionsLeft = new string[shortcutCount];

            for (int i = 0; i < 256; i++)
            {
                keyToActionMapRght[i] = -1;
                keyToActionMapLeft[i] = -1;
            }

            for (int i = 0; i < shortcutCount; i++)
            {
                string shortcutKeyRght = shortcutKey[i] + "[Rght]";
                string shortcutKeyLeft = shortcutKey[i] + "[Left]";
                string shortcutDescriptionKeyVal = shortcutKey[i] + "[Desc]";

                string shortcutStringRght = RegistryUtils.GetRegistryStringValue(shortcutKeyRght, string.Empty).Trim();
                string shortcutStringLeft = RegistryUtils.GetRegistryStringValue(shortcutKeyLeft, string.Empty).Trim();
                string shortcutDescriptionString = RegistryUtils.GetRegistryStringValue(shortcutDescriptionKeyVal, string.Empty).Trim();

                if (string.IsNullOrEmpty(shortcutDescriptionString))
                {
                    shortcutDescriptionKeyVal = shortcutDescriptionKeyVal.ToLower();

                    if (appConfig.ContainsKey(shortcutDescriptionKeyVal))
                    {
                        shortcutDescriptionString = appConfig[shortcutDescriptionKeyVal];
                    }
                }

                shortcutDescriptionsRght[i] = shortcutDescriptionString;
                shortcutDescriptionsLeft[i] = shortcutDescriptionString;

                if (!string.IsNullOrEmpty(shortcutStringRght))
                {
                    addKeyToActionMap(shortcutStringRght, keyToActionMapRght, shortcutsRght, i);
                }

                else
                {
                    shortcutKeyRght = shortcutKeyRght.ToLower();
   
                    if (appConfig.ContainsKey(shortcutKeyRght))
                    {
                        shortcutStringRght = appConfig[shortcutKeyRght];
                    }

                    if (!string.IsNullOrEmpty(shortcutStringRght))
                    {
                        addKeyToActionMap(shortcutStringRght, keyToActionMapRght, shortcutsRght, i);
                    }
                }

                if (!string.IsNullOrEmpty(shortcutStringLeft))
                {
                    addKeyToActionMap(shortcutStringLeft, keyToActionMapLeft, shortcutsLeft, i);
                }

                else
                {
                    shortcutKeyLeft = shortcutKeyLeft.ToLower();
                  
                    if (appConfig.ContainsKey(shortcutKeyLeft))
                    {
                        shortcutStringLeft = appConfig[shortcutKeyLeft];
                    }

                    if (!string.IsNullOrEmpty(shortcutStringLeft))
                    {
                        addKeyToActionMap(shortcutStringLeft, keyToActionMapLeft, shortcutsLeft, i);
                    }
                }
            }

        }

        private static void addKeyToActionMap(
            string key
            , int[] keyToActionMap
            , string[] shortcuts
            , int shortcutIndex)
        {
            key = key.Trim().ToUpper();

            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (!StringToAsciiTable.ContainsKey(key))
            {
                return;
            }

            int keyAscii = StringToAsciiTable[key];

            //if (keyAscii >= 97 && keyAscii <= 122)
            //{
            //    keyToActionMap[keyAscii] = shortcutIndex;
            //    keyToActionMap[keyAscii - 32] = shortcutIndex;

            //    shortcuts[shortcutIndex] = AsciiToStringTable[keyAscii - 32];

            //    return;
            //}

            //if (keyAscii >= 65 && keyAscii <= 90)
            //{
            //    keyToActionMap[keyAscii] = shortcutIndex;
            //    keyToActionMap[keyAscii + 32] = shortcutIndex;

            //    shortcuts[shortcutIndex] = AsciiToStringTable[keyAscii];

            //    return;
            //}

            keyToActionMap[keyAscii] = shortcutIndex;

            shortcuts[shortcutIndex] = AsciiToStringTable[keyAscii];
        }

        public static string GetAreaShortCutKey(AreaShortcutKeys key)
        {
            return AreaStateShortcuts[(int)key];
        }

        public static string GetLineShortCutKey(LineShortcutKeys key)
        {
            return LineStateShortcuts[(int)key];
        }

        public static string GetShortCutKey(string keyDefintion)
        {
            string result = "";

            if (!string.IsNullOrEmpty(keyDefintion))
            {
                var split = keyDefintion.Split('.');
                if (split.Length != 2)
                    return result;
                switch (split[0])
                {
                    case "AreaShortcutKeys":
                        {
                            AreaShortcutKeys key = (AreaShortcutKeys)Enum.Parse(typeof(AreaShortcutKeys), split[1]);
                            result = GetAreaShortCutKey(key);
                        }
                        break;

                    case "LineShortcutKeys":
                        {
                            LineShortcutKeys key = (LineShortcutKeys)Enum.Parse(typeof(LineShortcutKeys), split[1]);
                            result = GetLineShortCutKey(key);
                        }
                        break;

                    default:
                        return result;
                }
            }

            return result;
        }


    }

    public enum ShortCutOrientation
    {
        Unknown = 0,
        RightHanded = 1,
        LeftHanded = 2
    }
}
