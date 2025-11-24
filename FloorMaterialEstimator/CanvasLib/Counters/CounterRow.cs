namespace CanvasLib.Counters
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using Utilities;

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
            this.UpdateSize();
            this.UpdateTotal();
            this.ckbShow.Checked = counter.Show;

            //this.Click += CounterRow_Click;
            //this.lblCount.Click += CounterRow_Click;
            //this.lblTag.Click += CounterRow_Click;
            //this.txbDescription.Click += CounterRow_Click;
            this.txbDescription.TextChanged += TxbDescription_TextChanged;
            //this.txbSize.Click += CounterRow_Click;
            this.txbSize.TextChanged += TxbSize_TextChanged;


            this.ckbShow.CheckedChanged += CkbShow_CheckedChanged;
            this.counter.CounterFilteredChanged += Counter_CounterFilteredChanged;


            this.btnColor.BackColor = this.counter.Color;
        }

        private void Counter_CounterFilteredChanged(Counter counter, bool filtered)
        {
            this.ckbShow.Checked = !filtered;
        }

        private void TxbSize_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(this.txbSize);

            double result;

            if (double.TryParse(txbSize.Text, out result))
            {
                counter.Size = result;
                
                UpdateTotal();

                baseForm.CounterSizeChanged();
            }
        }

        private void TxbDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateDescription();

            //baseForm.btnUpdate_Click(null, null);
        }

        private void BtnColor_Paint(object sender, PaintEventArgs e)
        {
            this.btnColor.BackColor = this.counter.Color;
        }

        //private void CounterRow_Click(object sender, EventArgs e)
        //{
        //    baseForm.SelectRow(this.ControlIndex);
        //}

        public void SelectRow()
        {
            SetRowItemsColor(Color.LightGray);
        }

        public void DeselectRow()
        {
            SetRowItemsColor(SystemColors.ControlLightLight);
        }

        private void SetRowItemsColor(Color color)
        {
            this.BackColor = color;

            this.lblCount.BackColor = color;
            this.lblTag.BackColor = color;
            this.txbDescription.BackColor = color;
            this.ckbShow.BackColor = color;
            this.txbSize.BackColor = color;
            this.lblTotal.BackColor = color;
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
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            double total = counter.Count * counter.Size;
            if (total != 0.0)
            {
                this.lblTotal.Text = total.ToString("N1");
            } else
            {
                this.lblTotal.Text = "";
            }
        }

        private void UpdateSize()
        {
            if (counter.Size != 0.0) {
                this.txbSize.Text = counter.Size.ToString("####.###");
            } else
            {
                this.txbSize.Text = "";
            }
        }

        private void CkbShow_CheckedChanged(object sender, EventArgs e)
        {
            baseForm.SetCounterVisibility(this.ControlIndex, this.ckbShow.Checked);

            counter.Show = this.ckbShow.Checked;
        }

        internal void UpdateDescription()
        {
            counter.Description = this.txbDescription.Text;
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            int customColor = this.counter.R + 256 * this.counter.G + 256 * 256 * this.counter.B;

            colorDialog.CustomColors = new int[] { customColor };

            colorDialog.Color = this.btnColor.BackColor;


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

            counter.Color = color;

            //if (CounterColorChanged != null)
            //{
            //    CounterColorChanged.Invoke(this.counter);
            //}
            //baseForm.btnUpdate_Click(null, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            baseForm.SelectRow(this.ControlIndex);
        }
    }
}
