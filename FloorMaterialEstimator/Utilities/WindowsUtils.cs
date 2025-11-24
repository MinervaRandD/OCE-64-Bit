using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    using System;
    using System.Runtime.InteropServices;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class WindowUtils
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        private static readonly IntPtr HWND_TOP = IntPtr.Zero;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_SHOWWINDOW = 0x0040;

        public static void ResizeAndMoveVisioWindow(IntPtr visioHwnd, int x, int y, int width, int height)
        {
            SetWindowPos(visioHwnd, HWND_TOP, x, y, width, height, SWP_NOZORDER | SWP_SHOWWINDOW);
        }

        public static bool IsFormOpen<T>() where T : Form
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is T)
                    return true;
            }
            return false;
        }

    }

}
