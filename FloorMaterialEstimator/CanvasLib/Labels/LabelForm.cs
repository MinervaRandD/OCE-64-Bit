using SettingsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace CanvasLib.Labels
{
    public partial class LabelForm : Form, ICursorManagementForm
    {
        private ILabelManager labelManager;

        private static Color buttonActive = Color.Orange;
        private static Color buttonInActive = SystemColors.Control;
        private static LabelContainer cutsContainer = LabelContainer.Circle;

        private Label Label => labelManager.ActiveLabel;

        public LabelForm(ILabelManager manager)
        {
            InitializeComponent();

            AddToCursorManagementList();

            this.labelManager = manager;

            GetLabelInfo();
        }

        public void GetLabelInfo()
        {
            txtLabel.Text = Label.Text;

            this.btnColor.BackColor = Label.Color;

            AdjustContainerButtons();

            this.nmPointSize.Value = (decimal)Label.LabelSize;
        }

        private void AdjustContainerButtons()
        {
            switch (Label.Container)
            {
                case LabelContainer.None:
                    rdbNone.Checked = true;
                    break;

                case LabelContainer.Circle:
                    rdbCircle.Checked = true;
                    break;

                case LabelContainer.Rectangle:
                    rdbRectangle.Checked = true;
                    break;

                default:
                    rdbNone.Checked = true;
                    break;

            }
        }

        private void AdjustDefaultButtons ()
        {
            if (Label.LabelSize == GlobalSettings.CutIndexFontInPts
                && Label.Container == cutsContainer
                && Label.Color.A == GlobalSettings.CutIndexFontColor.A
                && Label.Color.R == GlobalSettings.CutIndexFontColor.R
                && Label.Color.G == GlobalSettings.CutIndexFontColor.G
                && Label.Color.B == GlobalSettings.CutIndexFontColor.B
                )
            {
                btnCutsIndex.BackColor = buttonActive;
                btnCounterLarge.BackColor = buttonInActive;
                btnCounterMedium.BackColor = buttonInActive;
                btnCounterSmall.BackColor = buttonInActive;
            }
            else if (Label.LabelSize == GlobalSettings.CounterLargeFontInPts)
            {
                btnCutsIndex.BackColor = buttonInActive;
                btnCounterLarge.BackColor = buttonActive;
                btnCounterMedium.BackColor = buttonInActive;
                btnCounterSmall.BackColor = buttonInActive;
            }
            else if (Label.LabelSize == GlobalSettings.CounterMediumFontInPts)
            {
                btnCutsIndex.BackColor = buttonInActive;
                btnCounterLarge.BackColor = buttonInActive;
                btnCounterMedium.BackColor = buttonActive;
                btnCounterSmall.BackColor = buttonInActive;
            }
            else if (Label.LabelSize == GlobalSettings.CounterSmallFontInPts)
            {
                btnCutsIndex.BackColor = buttonInActive;
                btnCounterLarge.BackColor = buttonInActive;
                btnCounterMedium.BackColor = buttonInActive;
                btnCounterSmall.BackColor = buttonActive;
            }
            else
            {
                btnCutsIndex.BackColor = buttonInActive;
                btnCounterLarge.BackColor = buttonInActive;
                btnCounterMedium.BackColor = buttonInActive;
                btnCounterSmall.BackColor = buttonInActive;
            }

        }

        #region Cursor Management
        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void LabelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();

            labelManager.DeActivateLabels();
        }

        public bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }

        public void AddToCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Add(this);
        }

        public void RemoveFromCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Remove(this);
        }
        public bool IsTopMost { get; set; } = false;

        #endregion

        private void txtLabel_TextChanged(object sender, EventArgs e)
        {
            Label.Text = txtLabel.Text;
        }

        private void rdbNone_CheckedChanged(object sender, EventArgs e)
        {
            Label.Container = LabelContainer.None;

            AdjustDefaultButtons();
        }

        private void rdbCircle_CheckedChanged(object sender, EventArgs e)
        {
            Label.Container = LabelContainer.Circle;

            AdjustDefaultButtons();
        }

        private void rdbRectangle_CheckedChanged(object sender, EventArgs e)
        {
            Label.Container = LabelContainer.Rectangle;

            AdjustDefaultButtons();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            int customColor = this.Label.ColorR + 256 * this.Label.ColorG + 256 * 256 * this.Label.ColorB;

            colorDialog.CustomColors = new int[] { customColor };

            colorDialog.Color = this.btnColor.BackColor;


            DialogResult dialogResult = colorDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            Color color = colorDialog.Color;

            Label.ColorA = color.A;
            Label.ColorR = color.R;
            Label.ColorG = color.G;
            Label.ColorB = color.B;

            this.btnColor.BackColor = color;

            AdjustDefaultButtons();
        }

        private void btnCutsIndex_Click(object sender, EventArgs e)
        {
            Color color = GlobalSettings.CutIndexFontColor;
            this.btnColor.BackColor = color;

            Label.ColorA = color.A;
            Label.ColorR = color.R;
            Label.ColorG = color.G;
            Label.ColorB = color.B;

            Label.Container = cutsContainer;

            this.nmPointSize.Value = (decimal)GlobalSettings.CutIndexFontInPts;

            AdjustContainerButtons();
        }

        private void btnCounterSmall_Click(object sender, EventArgs e)
        {
            this.nmPointSize.Value = (decimal)GlobalSettings.CounterSmallFontInPts;
        }

        private void btnCounterMedium_Click(object sender, EventArgs e)
        {
            this.nmPointSize.Value = (decimal)GlobalSettings.CounterMediumFontInPts;
        }

        private void btnCounterLarge_Click(object sender, EventArgs e)
        {
            this.nmPointSize.Value = (decimal)GlobalSettings.CounterLargeFontInPts;
        }

        private void nmPointSize_ValueChanged(object sender, EventArgs e)
        {
            double ptsSize = (double)this.nmPointSize.Value;
            Label.LabelSize = ptsSize;

            AdjustDefaultButtons();
        }
    }
}
