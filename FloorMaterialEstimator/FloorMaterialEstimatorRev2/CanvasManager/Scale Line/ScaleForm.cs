
namespace CanvasLib.Scale_Line
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using Utilities;

    public partial class ScaleForm : Form
    {
        public new DialogResult DialogResult;

        private ScaleLineState scaleLineState => scaleRuleController.ScaleLineState;

        private ScaleRuleController scaleRuleController;

        public ScaleForm(ScaleRuleController scaleRuleController)
        {
            this.scaleRuleController = scaleRuleController;

            InitializeComponent();

            this.MouseEnter += ScaleForm_MouseEnter;
            this.MouseLeave += ScaleForm_MouseLeave;

            this.btnCancel.Enabled = true;

            this.txbFeet.TextChanged += TxbFeet_TextChanged;
            this.txbInches.TextChanged += TxbInches_TextChanged;

            this.txbFeet.BackColor = SystemColors.ControlLightLight;
            this.txbInches.BackColor = SystemColors.ControlLightLight;

            SetFormForScaleLineState();

            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            this.DialogResult = DialogResult.OK;
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

            string txbText = this.txbFeet.Text.Trim();

            if (!Utilities.IsValidPosDbl(txbText))
            {
                this.txbFeet.BackColor = Color.Pink;
                return;
            }

            else
            {
                this.txbFeet.BackColor = SystemColors.ControlLightLight;
            }

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

            string txbText = this.txbInches.Text.Trim();

            if (!Utilities.IsValidPosInt(txbText))
            {
                this.txbInches.BackColor = Color.Pink;
                return;
            }

            else
            {
                this.txbInches.BackColor = SystemColors.ControlLightLight;
            }

            int inches = int.Parse(txbText);

            if (inches > 11)
            {
                this.txbInches.BackColor = Color.Pink;
            }

            else
            {
                this.txbInches.BackColor = SystemColors.ControlLightLight;
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

            if (this.txbFeet.BackColor == Color.Pink || this.txbInches.BackColor == Color.Pink)
            {
                this.btnOK.Enabled = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txbFeet.Text) && string.IsNullOrWhiteSpace(this.txbInches.Text))
            {
                this.btnOK.Enabled = false;
                return;
            }

            this.btnOK.Enabled = true;
        }

        public double? TotalInches()
        {
            double rtrnValu = 0.0;

            if (this.txbFeet.BackColor == Color.Pink || this.txbInches.BackColor == Color.Pink)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(this.txbFeet.Text) && string.IsNullOrWhiteSpace(this.txbInches.Text))
            {
                return null;
            }

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
    }
}
