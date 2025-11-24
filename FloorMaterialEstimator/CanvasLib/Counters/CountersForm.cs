namespace CanvasLib.Counters
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;

    using Utilities;

    public partial class wnfCounters : Form, ICursorManagementForm
    {
        CounterList Counters;

        public CounterRow[] CounterRowList = new CounterRow[CounterList.CountersSize];

        private bool toggleActivation = true;

        private CounterController counterController;

        public wnfCounters(CounterController counterController, CounterList counters)
        {
            InitializeComponent();

            AddToCursorManagementList();
            this.FormClosed += FinishesEditForm_FormClosed;

            this.counterController = counterController;
            this.Counters = counters;

            int cntlLocnX = 12;
            int cntlLocnY = 12;

            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                CounterRow counterRow = new CounterRow(this, Counters[i]);

                CounterRowList[i] = counterRow;
                
                this.pnlCounters.Controls.Add(counterRow);

                counterRow.Location = new Point(cntlLocnX, cntlLocnY);

                counterRow.ControlIndex = i;

                cntlLocnY += counterRow.Height;

                //counterRow.CounterColorChanged += CounterRow_CounterColorChanged;
            }

            //SelectRow(0);

            //this.MouseEnter += WnfCounters_MouseEnter;
            //this.MouseLeave += WnfCounters_MouseLeave;

            setupFormSize();

            this.SizeChanged += WnfCounters_SizeChanged;
            this.FormClosed += WnfCounters_FormClosed;
        }

        internal void SelectRow(int controlIndex)
        {
            for (int i = 0 ; i < this.CounterRowList.Length; i++)
            {
                if (i == controlIndex)
                {
                    CounterRowList[i].SelectRow();
                }

                else
                {
                    CounterRowList[i].DeselectRow();
                }
            }

            counterController.SelectRow(controlIndex);

            this.BringToFront();
        }

        public void Close(bool toggleActivation = true)
        {
            this.toggleActivation = toggleActivation;

            base.Close();
        }

        private void WnfCounters_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (toggleActivation)
            {
                counterController.ToggleCountersActivation();
            }
        }

        internal void UpdateCount(int counterIndex)
        {
            CounterRowList[counterIndex].UpdateCount();
        }

        private void WnfCounters_SizeChanged(object sender, EventArgs e)
        {
            setupFormSize();
        }

        private void setupFormSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int grbxSizeX = this.grbButtons.Width;
            int grbxSizeY = this.grbButtons.Height;

            int panlLocnX = this.pnlCounters.Location.X;
            int panlLocnY = this.pnlCounters.Location.Y;

            int panlSizeX = this.pnlCounters.Size.Width;
            int panlSizeY = formSizeY - panlLocnY - grbxSizeY - 80;

            this.pnlCounters.Size = new Size(panlSizeX, panlSizeY);

            int grbxLocnX = this.grbButtons.Location.X;
            int grbxLocnY = panlLocnY + panlSizeY + 24;

            this.grbButtons.Location = new Point(grbxLocnX, grbxLocnY);
        }

        internal void SetCount(int counterIndex, int count)
        {
            Counters[counterIndex].Count = 0;

            UpdateCount(counterIndex);
        }

        internal void SetCounterVisibility(int counterIndex, bool visible)
        {
            counterController.SetCounterVisibility(counterIndex, visible);
        }

        internal void UpdateSelectedCounterDescription()
        {
            int counterIndex = counterController.SelectedCounterIndex;

            CounterRowList[counterIndex].txbDescription.Text = Counters[counterIndex].Description;
        }

        internal void CounterSizeChanged()
        {
            this.counterController.CounterSizeChanged();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            foreach (CounterRow counterRow in CounterRowList)
            {
                counterRow.ShowIfCount();
            }
        }

        private void btnShowNone_Click(object sender, EventArgs e)
        {
            foreach (CounterRow counterRow in CounterRowList)
            {
                counterRow.Unshow();
            }
        }

        private void btnClearSelectedCount_Click(object sender, EventArgs e)
        {
            counterController.RemoveCounter(counterController.SelectedCounterIndex);
        }

        private void btnClearAllCounts_Click(object sender, EventArgs e)
        {
            DialogResult dr = ManagedMessageBox.Show("This will clear all counts for all counters. Do you want to proceed?", "Clear Counters", MessageBoxButtons.YesNo);

            if (dr != DialogResult.Yes)
            {
                return;
            }

            counterController.RemoveAllCounters();

        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                CounterRowList[i].UpdateDescription();
            }

            //counterController.UpdateSelectedCounterDescription();

            counterController.UpdatedCounterColors();

           // MessageBoxAdv.Show("Counters have been updated.", "Counters Updated", MessageBoxAdv.Buttons.OK);

            MessageBox.Show("Counters Have Been Updated.");
        }

        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(counterController.CountersFilePath))
            {
                Counters.Save(counterController.CountersFilePath);

                ManagedMessageBox.Show("Global defaults for counters have been saved.");
            }
        }

        internal void ResetCounters()
        {
            for (int i = 0; i < CounterList.CountersSize; i++)
            {
                CounterRowList[i].lblCount.Text = "0";
            }
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
    }
}
