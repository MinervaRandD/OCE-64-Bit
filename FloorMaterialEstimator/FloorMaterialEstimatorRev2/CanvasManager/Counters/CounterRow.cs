using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Supporting_Forms
{
    public partial class CounterRow : UserControl
    {
        public int ControlIndex;

        private wnfCounters baseForm;

        private Counter counter;

        
        public CounterRow(wnfCounters baseForm, Counter counter)
        {
            this.baseForm = baseForm;
            this.counter = counter;

            InitializeComponent();

            this.lblCount.Text = counter.Count.ToString();
            this.lblTag.Text = counter.Tag.ToString();
            this.txbDescription.Text = counter.Description;
            this.ckbShow.Checked = counter.Show;

            this.Click += CounterRow_Click;
            this.lblCount.Click += CounterRow_Click;
            this.lblTag.Click += CounterRow_Click;
            this.txbDescription.Click += CounterRow_Click;
            this.txbDescription.TextChanged += TxbDescription_TextChanged;

            if (this.counter.Count <= 0)
            {
                this.ckbShow.Checked = false;
            }

            this.ckbShow.CheckedChanged += CkbShow_CheckedChanged;

            this.btnColor.BackColor = this.counter.Color;
        }

        private void TxbDescription_TextChanged(object sender, EventArgs e)
        {
            baseForm.btnUpdate_Click(null, null);
        }

        private void BtnColor_Paint(object sender, PaintEventArgs e)
        {
            this.btnColor.BackColor = this.counter.Color;
        }

        private void CounterRow_Click(object sender, EventArgs e)
        {
            baseForm.SelectRow(this.ControlIndex);
        }

        public void SelectRow()
        {
            this.BackColor = Color.Orange;

            this.lblCount.BackColor = Color.Orange;
            this.lblTag.BackColor = Color.Orange;
            this.txbDescription.BackColor = Color.Orange;
            this.ckbShow.BackColor = Color.Orange;
        }

        public void DeselectRow()
        {
            this.BackColor = SystemColors.Control;

            this.lblCount.BackColor = SystemColors.ControlLightLight;
            this.lblTag.BackColor = SystemColors.ControlLightLight;
            this.txbDescription.BackColor = SystemColors.ControlLightLight;
            this.ckbShow.BackColor = SystemColors.ControlLightLight;
        }

        internal void ShowIfCount()
        {
            if (this.counter.Count > 0)
            {
                this.ckbShow.Checked = true;
            }
        }

        internal void Unshow()
        {
            this.ckbShow.Checked = false;
        }

        internal void UpdateCount()
        {
            this.lblCount.Text = counter.Count.ToString();

            if (counter.Count > 0)
            {
                if (!this.ckbShow.Checked)
                {
                    this.ckbShow.Checked = true;
                }
            }

            else
            {
                if (this.ckbShow.Checked)
                {
                    this.ckbShow.Checked = false;
                }
            }
        }

        private void CkbShow_CheckedChanged(object sender, EventArgs e)
        {
            baseForm.SetCounterVisibility(this.ControlIndex, this.ckbShow.Checked);
        }

        internal void UpdateDescription()
        {
            counter.Description = this.txbDescription.Text.Trim();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            DialogResult dialogResult = colorDialog.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            Color color = colorDialog.Color;

            counter.A = color.A;
            counter.R = color.R;
            counter.G = color.G;
            counter.B = color.B;

            this.btnColor.BackColor = counter.Color;

            baseForm.btnUpdate_Click(null, null);
        }
    }
}
