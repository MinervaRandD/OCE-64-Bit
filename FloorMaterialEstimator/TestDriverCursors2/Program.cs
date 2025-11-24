

namespace TestDriverCursors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    static class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32
        uiParam, String pvParam, UInt32 fWinIni);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr pcur);

        public static uint CROSS = 32515;
        public static uint NORMAL = 32512;
        public static uint IBEAM = 32513;
        public static uint ARROW = 32512;

        public static uint[] Cursors = { NORMAL, ARROW };

        public static uint currentCursor = ARROW;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //for (int i = 0; i < Cursors.Length; i++)
            //    SetSystemCursor(CopyIcon(LoadCursor(IntPtr.Zero, (int)CROSS)), Cursors[i]);

            Application.Run(new CursorTestForm());

            SystemParametersInfo(0x0057, 0, null, 0);
        }

        public static void SetCursorToCross()
        {
            //if (currentCursor != CROSS)
            //{
                SystemParametersInfo(0x0057, 0, null, 0);
                SetSystemCursor(CopyIcon(LoadCursor(IntPtr.Zero, (int)CROSS)), NORMAL);

                currentCursor = CROSS;
            //}
        }

        public static void SetCursorToArrow()
        {
            //if (currentCursor != ARROW)
            //{
                SystemParametersInfo(0x0057, 0, null, 0);
                SetSystemCursor(CopyIcon(LoadCursor(IntPtr.Zero, (int)ARROW)), NORMAL);

                currentCursor = ARROW;
            //}
        }
    }
}
