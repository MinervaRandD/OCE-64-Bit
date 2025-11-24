using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator
{
    public partial class KeyboardAndMouseActionsForm : Form
    {
        private List<Tuple<string, string>> fixedShortcutsGlobalInit = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("[Keyboard]+", "Zoom in"),
            new Tuple<string, string>("[Keyboard]-", "Zoom out"),
            new Tuple<string, string>("Alt-A, F1", "Select area design state"),
            new Tuple<string, string>("Atl-L, F2", "Select line design state"),
            new Tuple<string, string>("Alt-S, F3", "Select seam design state"),
            new Tuple<string, string>("Alt-T, F4", "Toggle design state sub-mode"),
            new Tuple<string, string>("F5", "Show field guides"),
            new Tuple<string, string>("F6", "Hide field guides"),
            new Tuple<string, string>("F7", "Delete all field guides"),
            new Tuple<string, string>("Tab-Click, [Numeric]+-Click", "Add bi-directional guide"),
            new Tuple<string, string>("[Numeric]--Click", "Add horizontal guide"),
            new Tuple<string, string>("[Numeric]/-Click", "Add vertical guide"),
            new Tuple<string, string>("Ctl key pressed", "Snap to nearest vertical or horizontal guide"),
        };

        private List<Tuple<string, string>> fixedShortcutsAreaDesignStateInit = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("1, 2, ..., 9", "Select corresponding area finish"),
            new Tuple<string, string>("Alt key pressed", "Draw line along field guide (note 1)"),
            new Tuple<string, string>("0-Click", "Fills the region bounded by guides with the current area finish"),
            new Tuple<string, string>("[Keyboard][1-9]-Click", "Fills the region bounded by guides with the corresponding finish")
        };

        private List<Tuple<string, string>> fixedShortcutsLineDesignStateInit = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("1, 2, ..., 9", "Select corresponding line finish"),
            new Tuple<string, string>("Alt key pressed", "Draw line along field guide (note 1)"),
            new Tuple<string, string>("H-Click, [Keypad]7-Click", "Draws a horizontal line between two adjacent guides (note 2"),
            new Tuple<string, string>("V-Click, [Keypad]8-Click", "Draws a vertical line between two adjacent guides (note 2")
        };

        public KeyboardAndMouseActionsForm()
        {
            InitializeComponent();

            InitializeShortcutPanel(this.pnlFixedShortcutsGlobal, fixedShortcutsGlobalInit);
            InitializeShortcutPanel(this.pnlFixedShortcutsAreaDesignState, fixedShortcutsAreaDesignStateInit);
            InitializeShortcutPanel(this.pnlFixedShortcutsLineDesignState, fixedShortcutsLineDesignStateInit);

        }

        private void InitializeShortcutPanel(Panel panel, List<Tuple<string, string>> shortcutList)
        {
            int cntlLocnX = 4;
            int cntlLocnY = 4;

            foreach (Tuple<string, string> shortcut in shortcutList)
            {
                UCKeyboardAndMouseActionsElement elem = new UCKeyboardAndMouseActionsElement();
                elem.Init(shortcut.Item1, shortcut.Item2);

                panel.Controls.Add(elem);

                elem.Location = new Point(cntlLocnX, cntlLocnY);

                cntlLocnY += elem.Height;
            }
        }
    }
}
