

namespace CanvasLib.Counters
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class CounterControl : UserControl
    {
        private Counter counter;

        private CounterController counterController;

        public CounterControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.rbnSmall.CheckedChanged += RbnSmall_CheckedChanged;
            this.rbnMedium.CheckedChanged += RbnMedium_CheckedChanged;
            this.rbnLarge.CheckedChanged += RbnLarge_CheckedChanged;
        }

        private void RbnSmall_CheckedChanged(object sender, EventArgs e)
        {
            if (counterController is null)
            {
                return;
            }

            if (!this.rbnSmall.Checked)
            {
                return;
            }

            counterController.SetAllCountersSize(CounterDisplaySize.Small);

            //if (counter is null)
            //{
            //    return;
            //}

            //if (this.rbnSmall.Checked)
            //{
            //    counter.CounterDisplaySize = CounterDisplaySize.Small;
            //}
        }

        private void RbnMedium_CheckedChanged(object sender, EventArgs e)
        {
            if (counterController is null)
            {
                return;
            }

            if (!this.rbnMedium.Checked)
            {
                return;
            }

            counterController.SetAllCountersSize(CounterDisplaySize.Medium);

            //if (counter is null)
            //{
            //    return;
            //}

            //if (this.rbnMedium.Checked)
            //{
            //    counter.CounterDisplaySize = CounterDisplaySize.Medium;
            //}
        }

        private void RbnLarge_CheckedChanged(object sender, EventArgs e)
        {
            if (counterController is null)
            {
                return;
            }

            if (!this.rbnLarge.Checked)
            {
                return;
            }

            counterController.SetAllCountersSize(CounterDisplaySize.Large);
        }

        public void Init(CounterController counterController)
        {
            this.counterController = counterController;

            Deactivate();

            this.BtnActivate.Click += BtnActivate_Click;
        }

        public void BtnActivate_Click(object sender, EventArgs e)
        {
            counterController.ToggleCountersActivation();
        }

        internal void Activate(int counterIndex)
        {
            //if (counterIndex < 0 || counterIndex >= Counters.CounterList.CountersSize)
            //{
            //    return;
            //}

            this.lblCountersHeader.Enabled = true;
            this.lblCountersHeader.BackColor = Color.Orange;
            this.lblTotalHeader.Enabled = true;
            this.lblTotalHeader.BackColor = Color.Orange;

            this.lblTag.Enabled = true;
            this.lblCount.Enabled = true;
            this.lblTotal.Enabled = true;
            this.lblDescription.Enabled = true;
            this.btnChange.Enabled = true;

            this.BtnActivate.Text = "Deactivate";
            this.BtnActivate.BackColor = Color.Orange;

            SelectCounter(counterIndex);
        }

        public void Deactivate()
        {
            this.lblCountersHeader.Enabled = false;
            this.lblCountersHeader.BackColor = SystemColors.Control;
            this.lblTotalHeader.Enabled = false;
            this.lblTotalHeader.BackColor = SystemColors.Control;

            this.lblTag.Enabled = false;
            this.lblCount.Enabled = false;
            this.lblTotal.Enabled = false;
            this.lblDescription.Enabled = false;
            this.btnChange.Enabled = false;

            this.lblTag.Text = string.Empty;
            this.lblCount.Text = string.Empty;
            this.lblTotal.Text = string.Empty;
            this.lblDescription.Text = string.Empty;

            this.BtnActivate.Text = "Activate";
            this.BtnActivate.BackColor = SystemColors.Control;
        }

        internal void SelectCounter(int counterIndex)
        {
            if (counterIndex < 0 || counterIndex >= counterController.CounterList.Counters.Length)
            {
                return;
            }

            counter = counterController.CounterList[counterIndex];

            this.lblTag.Text = counter.Tag.ToString();
            this.lblDescription.Text = counter.Description;

            UpdateCountTotals();

            switch (counter.CounterDisplaySize)
            {
                case CounterDisplaySize.Small: this.rbnSmall.Checked = true; break;
                case CounterDisplaySize.Medium: this.rbnMedium.Checked = true; break;
                case CounterDisplaySize.Large: this.rbnLarge.Checked = true; break;
            }

            counter.Show = true;

            this.ckbHideCircles.Checked = !counter.ShowCircle;
        }

        internal void UpdateCountTotal(int counterIndex)
        {
            //if (this.counter is null)
            //{
            //    MessageBox.Show("Please select a counter.");
            //    return;
            //}

            Counter counter1 = counterController.CounterList[counterIndex];

            if (this.counter is null)
            {
                this.counter = counter1;
            }

            if (counter1.Tag != this.counter.Tag)
            {
                return;
            }

            UpdateCountTotals();
        }

        private void UpdateCountTotals()
        {
            this.lblCount.Text = counter.Count.ToString();
            double total = counter.Count * counter.Size;
            this.lblTotal.Text = total.ToString("N1");
        }

        internal void SetCount(int counterIndex, int count)
        {
            counterController.CounterList[counterIndex].Count = count;

            UpdateCountTotal(counterIndex);
        }

        internal void UpdateSelectedCounterDescription()
        {
            this.lblDescription.Text = counter.Description;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (counter is null)
            {
                return;
            }

            counter.Description = this.lblDescription.Text.Trim();

            counterController.UpdateSelectedCounterDescription();
        }

        private void ckbHideCircles_CheckedChanged(object sender, EventArgs e)
        {
            if (counter is null)
            {
                return;
            }

            counter.ShowCircle = !ckbHideCircles.Checked;
        }
    }
}
