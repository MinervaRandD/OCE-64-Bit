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
        private static Dictionary<string, string> appConfig;

        public const int AreaStateShortcutCount = 4;
        public const int LineStateShortcutCount = 5;
        public const int SeamStateShortcutCount = 3;
        public const int GeneralShortcutCount = 3;

        public static HashSet<string>[] AreaStateShortcuts = new HashSet<string>[AreaStateShortcutCount];

        public static HashSet<string>[] LineStateShortcuts = new HashSet<string>[LineStateShortcutCount];

        public static HashSet<string>[] SeamStateShortcuts = new HashSet<string>[SeamStateShortcutCount];

        public static HashSet<string>[] GeneralShortcuts = new HashSet<string>[GeneralShortcutCount];

        public static int[] AreaStateKeyToActionMap = new int[256];
        public static int[] LineStateKeyToActionMap = new int[256];
        public static int[] SeamStateKeyToActionMap = new int[256];
        public static int[] GeneralKeyToActionMap = new int[256];

        public static HashSet<string> ValidShortcuts = new HashSet<string>()
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "0",
            "`", "'", ";", ".", "/", "+", "-", "=", "[", "]",
           /* "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", */
            "ESC", "TAB", "ENTER", "BACK", "HOME", "END", "INSERT"
        };

        public static Dictionary<string, int> ShortcutToCodeDict = new Dictionary<string, int>()
        {
            { "ESC", 27 },
            { "TAB", 9 },
            { "ENTER", 13 },
            { "BACK", 8 },
            { "HOME", 128 },
            { "END",  129 },
            { "INSERT", 130 }
        };

        public static bool IsValidShortcut(string shortcut)
        {
            return ValidShortcuts.Contains(shortcut);
        }

        public static string[] AreaStateShortcutNames = new string[]
        {
            "Erase Last Line",
            "Complete Shape",
            "Cancel Shape In Progress",
            "Toggle Zero Line Mode"
        };


        public static string[] LineStateShortcutNames = new string[]
        {
            "Erase Last Line",
            "Jump",
            "Single Line",
            "Double Line",
            "Duplicate Line"
        };

        public static string[] SeamStateShortcutNames = new string[]
        {
            "Erase Last Line",
            "Complete Shape",
            "Cancel Shape In Progress"
        };

        public static string[] GeneralShortcutNames = new string[]
        {
            "Zoom In",
            "Zoom Out",
            "Toggle Area-Line Mode"
        };

        public static string[] AreaStateShortcutKeys = new string[]
        {
            "AreaStateEraseLastLine",
            "AreaStateCompleteShape",
            "AreaStateCancelShapeInProgress",
            "AreaStateToggleZeroLineState"
        };

        public static string[] LineStateShortcutKeys = new string[]
        {
            "LineStateEraseLastLine",
            "LineStateJump",
            "LineStateSingleLine",
            "LineStateDoubleLine",
            "LineStateDuplicateLine"
        };

        public static string[] SeamStateShortcutKeys = new string[]
        {
            "SeamStateEraseLastLine",
            "SeamStateCompleteShape",
            "SeamStateCancelShapeInProgress"
        };

        public static string[] GeneralShortcutKeys = new string[]
        {
            "GeneralZoomIn",
            "GeneralZoomOut",
            "GeneralToggleAreaLineState"
        };

        public static void Update(
            HashSet<string>[] areaModeShortcuts,
            HashSet<string>[] lineModeShortcuts,
            HashSet<string>[] seamModeShortcuts,
            HashSet<string>[] generalShortcuts,
            bool setAsDefault = false)
        {
            AreaStateShortcuts = areaModeShortcuts;
            LineStateShortcuts = lineModeShortcuts;
            SeamStateShortcuts = seamModeShortcuts;
            GeneralShortcuts = generalShortcuts;

            update(AreaStateShortcuts, AreaStateShortcutKeys, AreaStateKeyToActionMap, AreaStateShortcutCount, setAsDefault);
            update(LineStateShortcuts, LineStateShortcutKeys, LineStateKeyToActionMap, LineStateShortcutCount, setAsDefault);
            update(SeamStateShortcuts, SeamStateShortcutKeys, SeamStateKeyToActionMap, SeamStateShortcutCount, setAsDefault);
            update(GeneralShortcuts, GeneralShortcutKeys, GeneralKeyToActionMap, GeneralShortcutCount, setAsDefault);
        }

        private static void update(HashSet<string>[] shortcuts, string[] shortcutKeys, int[] keyToActionMap, int shortcutCount, bool setAsDefault = false)
        {
            Debug.Assert(shortcuts != null);
            Debug.Assert(shortcutKeys != null);
            Debug.Assert(keyToActionMap != null);

            for (int i = 0; i < keyToActionMap.Length; i++)
            {
                keyToActionMap[i] = -1;
            }

            for (int i = 0; i < shortcutCount; i++)
            {
                addShortcutsToKeyToActionMap(i, shortcuts[i], keyToActionMap);
            }

            if (!setAsDefault)
            {
                return;
            }

            for (int i = 0; i < shortcutCount; i++)
            {
                string regStr = string.Empty;

                if (shortcuts[i] != null)
                {
                    List<string> shortcutList = shortcuts[i].ToList();

                    if (shortcutList != null)
                    {
                        if (shortcutList.Count > 0)
                        {
                            regStr = string.Join(",", shortcutList);
                        }
                    }
                }

                RegistryUtils.SetRegistryValue(shortcutKeys[i], regStr);
            }

        }

        public static void Initialize(Dictionary<string, string> appConfig)
        {
            ShortcutSettings.appConfig = appConfig;
            
            Initialize(AreaStateShortcuts, AreaStateShortcutKeys, AreaStateKeyToActionMap, AreaStateShortcutCount);
            Initialize(LineStateShortcuts, LineStateShortcutKeys, LineStateKeyToActionMap, LineStateShortcutCount);
            Initialize(SeamStateShortcuts, SeamStateShortcutKeys, SeamStateKeyToActionMap, SeamStateShortcutCount);
            Initialize(GeneralShortcuts, GeneralShortcutKeys, GeneralKeyToActionMap, GeneralShortcutCount);
        }

        private static void Initialize(HashSet<string>[] shortcuts, string[] shortcutKey, int[] keyToActionMap, int shortcutCount)
        {
            for (int i = 0; i < keyToActionMap.Length; i++)
            {
                keyToActionMap[i] = -1;
            }

            for (int i = 0; i < shortcutCount; i++)
            {
                string shortcutString = RegistryUtils.GetRegistryStringValue(shortcutKey[i], string.Empty);

                if (!string.IsNullOrEmpty(shortcutString))
                {
                    shortcuts[i] = parseShortcutsString(shortcutString);

                    addShortcutsToKeyToActionMap(i, shortcuts[i], keyToActionMap);

                    continue;
                }

                else
                {
                    string appConfigKey = shortcutKey[i].ToLower();

                    if (appConfig.ContainsKey(appConfigKey))
                    {
                        shortcuts[i] = parseShortcutsString(appConfig[appConfigKey]);

                        addShortcutsToKeyToActionMap(i, shortcuts[i], keyToActionMap);

                        continue;
                    }

                    else
                    {
                        shortcuts[i] = new HashSet<string>();
                    }
                }
            }
        }

        private static HashSet<string> parseShortcutsString(string shortcutString)
        {
            HashSet<string> returnList = new HashSet<string>();

            string[] elements = shortcutString.Split(',');

            foreach (string element in elements)
            {
                if (string.IsNullOrWhiteSpace(element))
                {
                    continue;
                }

                string element1 = element.Trim().ToUpper();
                
                if (!IsValidShortcut(element1))
                {
                    throw new Exception("Invalid shortcut found in initialization");
                }

                returnList.Add(element1);
            }

            return returnList;
        }

        private static void addShortcutsToKeyToActionMap(int i, HashSet<string> shortcuts, int[] keyToActionMap)
        {
            foreach (string shortcut in shortcuts)
            {
                if (shortcut.Length == 1)
                {
                    char cShortCut = shortcut[0];

                    if (cShortCut >= 'a' && cShortCut <= 'z')
                    {
                        keyToActionMap[cShortCut] = i;
                        keyToActionMap[cShortCut - 32] = i;
                    }

                    else if (cShortCut >= 'A' && cShortCut <= 'Z')
                    {
                        keyToActionMap[cShortCut] = i;
                        keyToActionMap[cShortCut + 32] = i;
                    }

                    else
                    {
                        keyToActionMap[cShortCut] = i;
                    }
                }

                else
                {
                    Debug.Assert(ShortcutToCodeDict.ContainsKey(shortcut));

                    keyToActionMap[ShortcutToCodeDict[shortcut]] = i;
                }
            }
        }
    }
}
