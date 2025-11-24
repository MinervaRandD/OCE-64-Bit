namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class CursorManager
    {
        [DllImport("user32.dll")]
        static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32
        uiParam, String pvParam, UInt32 fWinIni);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr pcur);

      

        public const int
            IDC_NORMAL = 32512,
            IDC_ARROW = 32512,
            IDC_IBEAM = 32513,
            IDC_WAIT = 32514,
            IDC_CROSS = 32515,
            IDC_UPARROW = 32516,
            IDC_SIZE = 32640,
            IDC_ICON = 32641,
            IDC_SIZENWSE = 32642,
            IDC_SIZENESW = 32643,
            IDC_SIZEWE = 32644,
            IDC_SIZENS = 32645,
            IDC_SIZEALL = 32646,
            IDC_NO = 32648,
            IDC_HAND = 32649,
            IDC_APPSTARTING = 32650,
            IDC_HELP = 32651;

        public static uint CurrentCursor = IDC_ARROW;

        private static IntPtr CROSS = LoadCursor(IntPtr.Zero, (int)IDC_CROSS);
        private static IntPtr ARROW = LoadCursor(IntPtr.Zero, (int)IDC_ARROW);

        public static void SetCursorToCross()
        {
            if (CurrentCursor != IDC_CROSS)
            {
                SystemParametersInfo(0x0057, 0, null, 0);
                SetSystemCursor(CopyIcon(CROSS), IDC_NORMAL);

                CurrentCursor = IDC_CROSS;
            }
        }

        public static void SetCursorToArrow()
        {
            if (CurrentCursor != IDC_ARROW)
            {
                SystemParametersInfo(0x0057, 0, null, 0);
                SetSystemCursor(CopyIcon(ARROW), IDC_NORMAL);

                CurrentCursor = IDC_ARROW;
            }
        }

        public static bool Ignore { get; set; } = false;

        public static List<ICursorManagementForm> CursorManagerFormList = new List<ICursorManagementForm>();

        private static List<Form> formsUnderCursor = new List<Form>();
        public static void WndProc(Form form)
        {
            if (Ignore)
            {
                return;
            }

            if (!CursorManagerFormList.Contains((ICursorManagementForm) form))
            {
                return;
            }

            if (!form.Bounds.Contains(Cursor.Position))
            {
                return;
            }

            formsUnderCursor.Clear();

            foreach (ICursorManagementForm cForm in CursorManagerFormList)
            {
                Form form1 = (Form)cForm;

                if (form1.Bounds.Contains(Cursor.Position))
                {
                    CursorManager.SetCursorToArrow();

                    if (cForm.IsTopMost)
                    {
                        form1.Activate();

                        return;
                    }

                    formsUnderCursor.Add(form1);
                }
            }

            if (formsUnderCursor.Count <= 0)
            {
                return;
            }

            CursorManagerFormList.ForEach(f => f.IsTopMost = false);

            ((ICursorManagementForm)formsUnderCursor[0]).IsTopMost = true;

            formsUnderCursor[0].Activate();

        }

    }
}
