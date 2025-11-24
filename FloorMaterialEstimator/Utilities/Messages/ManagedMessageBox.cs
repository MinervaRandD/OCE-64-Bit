using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
    public static class ManagedMessageBox
    {
        public static DialogResult Show(string msg)
        {
            DialogResult dr;

            CursorManager.SetCursorToArrow();

            CursorManager.Ignore = true;

            dr = MessageBox.Show(msg);

            CursorManager.Ignore = false;

            return dr;
        }

        public static DialogResult Show(string msg, string title, MessageBoxButtons btns)
        {
            DialogResult dr;

            CursorManager.SetCursorToArrow();

            CursorManager.Ignore = true;

            dr = MessageBox.Show(msg, title, btns);

            CursorManager.Ignore = false;

            return dr;
        }
    }
}
