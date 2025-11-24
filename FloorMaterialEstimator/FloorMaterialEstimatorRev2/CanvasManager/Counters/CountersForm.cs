namespace CanvasLib.Counters
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;

    using FloorMaterialEstimator.CanvasManager;

    using Utilities;

    public partial class wnfCounters : Form, IMessageFilter
    {
        CounterList Counters;

        CounterRow[] CounterRowList = new CounterRow[26];

        private CanvasManager canvasManager;

        private FloorMaterialEstimatorBaseForm baseForm => canvasManager.BaseForm;

        private bool toggleActivation = true;

        public wnfCounters(CanvasManager canvasManager, CounterList counters)
        {
            InitializeComponent();

            this.canvasManager = canvasManager;
            this.Counters = counters;

            int cntlLocnX = 8;
            int cntlLocnY = 12;

            for (int i = 0; i < 26; i++)
            {
                CounterRow counterRow = new CounterRow(this, Counters[i]);

                CounterRowList[i] = counterRow;
                
                this.pnlCounters.Controls.Add(counterRow);

                counterRow.Location = new Point(cntlLocnX, cntlLocnY);

                counterRow.ControlIndex = i;

                cntlLocnY += counterRow.Height;
            }

            SelectRow(0);

            this.MouseEnter += WnfCounters_MouseEnter;
            this.MouseLeave += WnfCounters_MouseLeave;

            setupFormSize();

            this.SizeChanged += WnfCounters_SizeChanged;
            this.FormClosed += WnfCounters_FormClosed;
        }

        private void WnfCounters_MouseEnter(object sender, EventArgs e)
        {
            baseForm.Cursor = Cursors.Arrow;
        }

        private void WnfCounters_MouseLeave(object sender, EventArgs e)
        {
            baseForm.SetCursorForCurrentLocation();
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

            canvasManager.SelectRow(controlIndex);
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
                canvasManager.ToggleCountersActivation();
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
            canvasManager.SetCounterVisibility(counterIndex, visible);
        }

        internal void UpdateSelectedCounterDescription()
        {
            int counterIndex = canvasManager.SelectedCounterIndex;

            CounterRowList[counterIndex].txbDescription.Text = Counters[counterIndex].Description;
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
            canvasManager.RemoveCounter(canvasManager.SelectedCounterIndex);
        }

        private void btnClearAllCounts_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 26; i++)
            {
                canvasManager.RemoveCounter(i);
            }
        }

        public void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 26; i++)
            {
                CounterRowList[i].UpdateDescription();
            }

            canvasManager.UpdateSelectedCounterDescription();

            canvasManager.UpdatedCounterColors();
        }

        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            if (Program.AppConfig.ContainsKey("countersfilepath"))
            {
                string outpFilePath = Path.Combine(Program.BaseDataFolder, Program.AppConfig["countersfilepath"]);

                Counters.Save(outpFilePath);

                MessageBox.Show("Global defaults for counters have been saved.");
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
            {
                baseForm.SetCursorForCurrentLocation();
#if DEBUG
                baseForm.UpdateMousePositionDisplay();
#endif
            }

            return false;
        }
    }
}
