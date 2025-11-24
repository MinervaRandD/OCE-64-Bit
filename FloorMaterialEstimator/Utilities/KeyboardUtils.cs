using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Utilities
{
    public static class KeyboardUtils
    {
        public static bool MKeyPressed => Keyboard.IsKeyDown(Key.M);

        public static bool EscKeyPressed => Keyboard.IsKeyDown(Key.Escape);

        public static bool CntlKeyPressed => Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
        
        public static bool AltKeyPressed => Keyboard.IsKeyDown(Key.LeftAlt) || Keyboard.IsKeyDown(Key.RightAlt);

        public static bool ShiftKeyPressed => Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

        public static bool AKeyPressed => Keyboard.IsKeyDown(Key.A);

        public static bool BKeyPressed => Keyboard.IsKeyDown(Key.B);

        public static bool DKeyPressed => Keyboard.IsKeyDown(Key.D);

        public static bool HKeyPressed => Keyboard.IsKeyDown(Key.H);

        public static bool KKeyPressed => Keyboard.IsKeyDown(Key.K);

        public static bool LKeyPressed => Keyboard.IsKeyDown(Key.L);

        public static bool VKeyPressed => Keyboard.IsKeyDown(Key.V);

        public static bool QKeyPressed => Keyboard.IsKeyDown(Key.Q);

        public static bool SKeyPressed => Keyboard.IsKeyDown(Key.S);

        public static bool TKeyPressed => Keyboard.IsKeyDown(Key.T);

        public static bool ZKeyPressed => Keyboard.IsKeyDown(Key.Z);

        public static bool HomeKeyPressed => Keyboard.IsKeyDown(Key.Home);

        public static bool F1KeyPressed => Keyboard.IsKeyDown(Key.F1);

        public static bool F2KeyPressed => Keyboard.IsKeyDown(Key.F2);

        public static bool TabKeyPressed => Keyboard.IsKeyDown(Key.Tab);

        public static bool InsertKeyPressed => Keyboard.IsKeyDown(Key.Insert);

        public static bool AddKeyPressed => Keyboard.IsKeyDown(Key.Add);

        public static bool SubKeyPressed => Keyboard.IsKeyDown(Key.Subtract);

        public static bool FwdSlashKeyPressed => Keyboard.IsKeyDown((Key)89);

        public static bool SpaceKeyPressed => Keyboard.IsKeyDown(Key.Space);

        public static bool DeleteKeyPressed => Keyboard.IsKeyDown(Key.Delete);

        public static bool RightBracketKeyPressed => Keyboard.IsKeyDown(Key.OemCloseBrackets);

        public static bool LeftBracketKeyPressed => Keyboard.IsKeyDown(Key.OemOpenBrackets);

        public static bool IsKeyPressed(int key) => Keyboard.IsKeyDown((Key)key);

        public static bool NumericKeypad0Pressed => Keyboard.IsKeyDown(Key.NumPad0);

        public static bool NumericKeypad0Toggled => Keyboard.IsKeyToggled(Key.NumPad0);

        public static bool Keyboard0Pressed => Keyboard.IsKeyDown(Key.D0);

        public static object NumericKeyPressed { get; set; }

        public static int? GetKeyPressed()
        {
            for (int i = 1; i < 256; i++)
            {
                if (Keyboard.GetKeyStates((Key) i) == KeyStates.Down)
                {
                    return i;
                }
            }

            return null;
        }

        public static int? GetNumericKeyPressed()
        {
            int? numberPressed = GetKeyboardNumericKeyPressed();

            if (numberPressed != null)
            {
                return numberPressed;
            }

            numberPressed = GetKeypadNumericKeyPressed();

            return numberPressed;
        }

        public static int? GetKeyboardNumericKeyPressed()
        {
            for (int i = 34; i <= 43; i++)
            {
                if (Keyboard.IsKeyDown((Key) i))
                {
                    return i - 34;
                }
            }

            return null;
        }

        public static int? GetKeypadNumericKeyPressed()
        {
            for (int i = 74; i <= 83; i++)
            {
                if (Keyboard.IsKeyDown((Key)i))
                {
                    return i - 74;
                }
            }

            return null;
        }

        private static char[] numericShiftKeyboardMap = new char[]
        {
            ')', '!', '@', '#', '$', '%', '^', '&', '*', '('
        };

        private static Dictionary<int, char> keyValToCharDict = new Dictionary<int, char>()
        {
            { 32, ' ' },
            { 32 + 256, ' ' },
            { 186, ';' },
            { 186 + 256, ':' },
            { 187, '=' },
            { 187 + 256, '+' },
            { 188, ',' },
            { 188 + 256, '<' },
            { 189, '-' },
            { 189 + 256, '_' },
            { 190, '.' },
            { 190 + 256, '>' },
            { 191, '/' },
            { 191 + 256, '?' },
            { 192, '`'},
            { 192 + 256, '~' },
            { 219, '[' },
            { 219 + 256, '{' },
            { 220, '\\' },
            { 220 + 256, '|' },
            { 221, ']' },
            { 221 + 256, '}' },
            { 222, '\'' },
            { 222 + 256, '"' }
        };

        public static char KeyValToChar(int keyVal, bool shiftKeyPressed)
        {
            if (keyVal >= 65 && keyVal <= 90)
            {
                if (shiftKeyPressed)
                {
                    return (char)keyVal;
                }

                else
                {
                    return (char)(keyVal + 32);
                }
            }

            if (keyVal >= 48 && keyVal <= 57)
            {
                if (shiftKeyPressed)
                {
                    return numericShiftKeyboardMap[keyVal - 48];
                }

                else
                {
                    return (char)keyVal;
                }
            }

            if (shiftKeyPressed)
            {
                keyVal += 256;
            }

            char rtrnChar = (char)0;

           keyValToCharDict.TryGetValue(keyVal, out rtrnChar);

            return rtrnChar;
            
        }
    }
}
