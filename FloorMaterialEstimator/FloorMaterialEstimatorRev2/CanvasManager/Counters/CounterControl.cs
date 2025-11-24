namespace CanvasLib.Counters
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class CounterControl : UserControl
    {
        private Counter counter;

        public CounterControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Deactivate();

            this.btnActivate.Click += BtnActivate_Click;
        }

        private void BtnActivate_Click(object sender, EventArgs e)
        {
            baseForm.ToggleCountersActivation();
        }

        internal void Activate(int counterIndex)
        {
            this.lblCountersHeader.Enabled = true;
            this.lblCountersHeader.BackColor = Color.Orange;

            this.lblTag.Enabled = true;
            this.lblCount.Enabled = true;
            this.txbDescription.Enabled = true;
            this.btnChange.Enabled = true;

            SelectCounter(counterIndex);

            this.btnActivate.Text = "Deactivate";
        }

        internal void Deactivate()
        {
            this.lblCountersHeader.Enabled = false;
            this.lblCountersHeader.BackColor = SystemColors.Control;

            this.lblTag.Enabled = false;
            this.lblCount.Enabled = false;
            this.txbDescription.Enabled = false;
            this.btnChange.Enabled = false;

            this.lblTag.Text = string.Empty;
            this.lblCount.Text = string.Empty;
            this.txbDescription.Text = string.Empty;

            this.btnActivate.Text = "Activate";
        }

        internal void SelectCounter(int counterIndex)
        {
            counter = baseForm.CanvasManager.Counters[counterIndex];

            this.lblTag.Text = counter.Tag.ToString();
            this.lblCount.Text = counter.Count.ToString();
            this.txbDescription.Text = counter.Description;
            //this.btnChange.Enabled = true;
        }

        internal void UpdateCount(int counterIndex)
        {
            Counter counter1 = baseForm.CanvasManager.Counters[counterIndex];

            if (counter1.Tag != this.counter.Tag)
            {
                return;
            }

            this.lblCount.Text = counter.Count.ToString();
        }

        internal void SetCount(int counterIndex, int count)
        {
            baseForm.CanvasManager.Counters[counterIndex].Count = count;

            UpdateCount(counterIndex);
        }

        internal void UpdateSelectedCounterDescription()
        {
            this.txbDescription.Text = counter.Description;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            counter.Description = this.txbDescription.Text.Trim();

            baseForm.CanvasManager.UpdateSelectedCounterDescription();
        }
    }
}
