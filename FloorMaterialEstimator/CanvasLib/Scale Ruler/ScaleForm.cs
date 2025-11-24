
namespace CanvasLib.Scale_Line
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Utilities;

    public partial class ScaleForm : Form, ICursorManagementForm
    {
        public new DialogResult DialogResult;

        private ScaleLineState scaleLineState => scaleRuleController.ScaleLineState;

        private ScaleRuleController scaleRuleController;

        public ScaleForm(ScaleRuleController scaleRuleController)
        {
            this.scaleRuleController = scaleRuleController;

            InitializeComponent();

            KeyPreview = true;

            //AddToCursorManagementList();
            this.FormClosed += FinishesEditForm_FormClosed;

            this.MouseEnter += ScaleForm_MouseEnter;
            this.MouseLeave += ScaleForm_MouseLeave;

            this.btnCancel.Enabled = true;

            this.txbFeet.TextChanged += TxbFeet_TextChanged;
            this.txbInches.TextChanged += TxbInches_TextChanged;
            this.txbLengthInFeet.TextChanged += TxbLengthInFeet_TextChanged ;

            this.txbFeet.BackColor = SystemColors.ControlLightLight;
            this.txbInches.BackColor = SystemColors.ControlLightLight;
            this.txbLengthInFeet.BackColor = SystemColors.ControlLightLight;

            SetFormForScaleLineState();

            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.DialogResult = DialogResult.OK;

            this.txbLengthInFeet.GotFocus += TxbLengthInFeet_GotFocus;
            this.txbInches.GotFocus += TxbInches_GotFocus;
            this.txbFeet.GotFocus += TxbFeet_GotFocus;

            this.CkbRemoveAllSeams.CheckedChanged += CkbRemoveAllSeams_CheckedChanged;

            this.CkbReseamAllAreas.CheckedChanged += CkbReseamAllAreas_CheckedChanged;
            if (scaleRuleController.SeamedAreasExist)
            {
                this.Size = new Size(440, 470);
            }

            else
            {
                this.Size = new Size(440, 360);
            }

        }

        private void CkbRemoveAllSeams_CheckedChanged(object sender, EventArgs e)
        {
            if (CkbRemoveAllSeams.Checked)
            {
                CkbReseamAllAreas.Checked = false;
            }
        }

        private void CkbReseamAllAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (CkbReseamAllAreas.Checked)
            {
                CkbRemoveAllSeams.Checked = false;
            }
        }

        private void TxbFeet_GotFocus(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txbFeet.Text))
            {
                this.txbFeet.SelectAll();
            }
        }

        private void TxbInches_GotFocus(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(this.txbInches.Text))
            //{
            //    this.txbInches.Text = "0";
            //}
            this.txbInches.SelectAll();

           // this.txbLengthInFeet.Text = string.Empty;
        }

        private void TxbLengthInFeet_GotFocus(object sender, EventArgs e)
        {
        //    this.txbFeet.Text = string.Empty;
        //    this.txbInches.Text = string.Empty;
        }

        private void ScaleForm_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void ScaleForm_MouseLeave(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }

        public void SetFormForScaleLineState()
        {
          
            switch (scaleLineState)
            {
                case ScaleLineState.SetScaleInitiated:
                    setScaleLineInitiatedState();
                    return;

                case ScaleLineState.FrstPointSelected:
                    setScaleLineFrstPointSelectedState();
                    return;

                case ScaleLineState.ScndPointSelected:
                    setScaleLineScndPointSelectedState();
                    return;
            }
        }

        public void ClearMeasureBoxes()
        {
            this.txbFeet.Text = string.Empty;
            this.txbInches.Text = string.Empty;
        }

        private void setScaleLineInitiatedState()
        {
            this.gbxSetListEndpoints.Enabled = true;

            this.gbxSetLineLength.Enabled = false;
            this.txbFeet.Enabled = false;
            this.txbInches.Enabled = false;

            this.btnGoBack.Enabled = false;

            this.btnOK.Enabled = false;
            this.btnReset.Enabled = false;

            this.lblClickFirstPoint.Enabled = true;
            this.lblClickSecondPoint.Enabled = false;


        }

        private void setScaleLineFrstPointSelectedState()
        {
            this.lblClickFirstPoint.Enabled = false;
            this.lblClickSecondPoint.Enabled = true;

            this.gbxSetLineLength.Enabled = false;
            this.txbFeet.Enabled = false;
            this.txbInches.Enabled = false;

            this.btnGoBack.Enabled = true;

            this.btnReset.Enabled = true;
        }

        private void setScaleLineScndPointSelectedState()
        {
            this.lblClickFirstPoint.Enabled = false;
            this.lblClickSecondPoint.Enabled = false;

            this.btnGoBack.Enabled = true;

            this.gbxSetLineLength.Enabled = true;
            this.txbFeet.Enabled = true;
            this.txbInches.Enabled = true;

            setOKButtonStatus();

            Point pFocus = new Point(txbFeet.Width / 2, txbFeet.Height / 2);
            Point p = txbFeet.PointToScreen(pFocus);
            Cursor.Position = p;
        }

        private void TxbFeet_TextChanged(object sender, EventArgs e)
        {
            doTxbFeetTextChanged();
            setOKButtonStatus();
        }

        private void doTxbFeetTextChanged()
        {
            if (string.IsNullOrWhiteSpace(this.txbFeet.Text))
            {
                this.txbFeet.Text = string.Empty;
                this.txbFeet.BackColor = SystemColors.ControlLightLight;

                return;
            }

            this.txbLengthInFeet.Text = string.Empty;

            string txbText = this.txbFeet.Text.Trim();

            Utilities.SetTextFormatForValidPositiveDouble(this.txbFeet);

            //if (!Utilities.IsValidPosDbl(txbText))
            //{
            //    this.txbFeet.BackColor = Color.Pink;
            //    return;
            //}

            //else
            //{
            //    this.txbFeet.BackColor = SystemColors.ControlLightLight;
            //}

            if (Utilities.HasFractionalPart(txbText))
            {
                this.txbInches.Text = string.Empty;
                this.txbInches.Enabled = false;
            }

            else
            {
                this.txbInches.Enabled = true;
            }

        }

        private void TxbInches_TextChanged(object sender, EventArgs e)
        {
            doTxbInchesTextChanged();
            setOKButtonStatus();
        }

        private void doTxbInchesTextChanged()
        {
            if (string.IsNullOrWhiteSpace(this.txbInches.Text))
            {
                this.txbInches.Text = string.Empty;
                this.txbInches.BackColor = SystemColors.ControlLightLight;

                return;
            }

            this.txbLengthInFeet.Text = string.Empty;

            string txbText = this.txbInches.Text.Trim();

            if (!Utilities.IsValidPosDbl(txbText))
            {
                this.txbInches.BackColor = Color.Pink;
                return;
            }

            else
            {
                this.txbInches.BackColor = SystemColors.ControlLightLight;
            }

            double inches = double.Parse(txbText);

            if (inches >= 12)
            {
                this.txbInches.BackColor = Color.Pink;
            }

            else
            {
                this.txbInches.BackColor = SystemColors.ControlLightLight;
            }

            setOKButtonStatus();
        }

        private void TxbLengthInFeet_TextChanged(object sender, EventArgs e)
        {
            doLengthInFeetTextChanged();
            setOKButtonStatus();
        }

        private void doLengthInFeetTextChanged()
        {
            if (string.IsNullOrWhiteSpace(this.txbLengthInFeet.Text))
            {
                this.txbLengthInFeet.Text = string.Empty;
                this.txbLengthInFeet.BackColor = SystemColors.ControlLightLight;

                return;
            }

            this.txbInches.Text = string.Empty;
            this.txbFeet.Text = string.Empty;

            string txbText = this.txbLengthInFeet.Text.Trim();

            if (!Utilities.IsValidPosDbl(txbText))
            {
                this.txbLengthInFeet.BackColor = Color.Pink;
                return;
            }

            else
            {
                this.txbLengthInFeet.BackColor = SystemColors.ControlLightLight;
            }

            setOKButtonStatus();
        }

        private void setOKButtonStatus()
        {
            if (this.scaleLineState != ScaleLineState.ScndPointSelected)
            {
                this.btnOK.Enabled = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txbFeet.Text) && string.IsNullOrWhiteSpace(this.txbInches.Text) && string.IsNullOrWhiteSpace(this.txbLengthInFeet.Text))
            {
                this.btnOK.Enabled = false;
                return;
            }

            if (this.txbFeet.BackColor == Color.Pink || this.txbInches.BackColor == Color.Pink || this.txbLengthInFeet.BackColor == Color.Pink)
            {
                this.btnOK.Enabled = false;
                return;
            }

            var total = TotalInches();

            if (total.HasValue && total > 0.0)
            {
                this.btnOK.Enabled = true;
            }
        }

        public double? TotalInches()
        {
          
            if (this.txbFeet.BackColor == Color.Pink || this.txbInches.BackColor == Color.Pink || this.txbLengthInFeet.BackColor == Color.Pink)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(this.txbFeet.Text) && string.IsNullOrWhiteSpace(this.txbInches.Text) && string.IsNullOrWhiteSpace(this.txbLengthInFeet.Text))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(this.txbFeet.Text) && string.IsNullOrWhiteSpace(this.txbInches.Text) && !string.IsNullOrWhiteSpace(this.txbLengthInFeet.Text))
            {
                if (!Utilities.IsValidPosDbl(this.txbLengthInFeet.Text.Trim()))
                {
                    return null;
                }

                return double.Parse(this.txbLengthInFeet.Text.Trim()) * 12.0;
            }

            double rtrnValu = 0.0;

           if (!string.IsNullOrWhiteSpace(this.txbFeet.Text))
            {
                rtrnValu += double.Parse(this.txbFeet.Text.Trim()) * 12.0;
            }

            if (!string.IsNullOrWhiteSpace(this.txbInches.Text))
            {
                rtrnValu += double.Parse(this.txbInches.Text.Trim());
            }

            return rtrnValu;
        }

        #region Cursor Management

        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            
        }

        private void txbFeet_MouseEnter(object sender, EventArgs e)
        {
            this.txbFeet.Focus();
        }
    }
}
