

using Globals;

namespace CanvasLib.Filters.Line_Filter
{
    using FinishesLib;
    using Utilities;

    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using CanvasLib.Filters;

    public partial class LineFilterForm : Form, ICursorManagementForm
    {
        private LineFinishBaseList lineFinishBaseList;

        private SeamFinishBaseList seamFinishBaseList;

        private List<LineFilterRow> LineFilterRowList;

        private List<SeamFilterRow> SeamFilterRowList;

        private LineFilterTotalRow lineFilterTotalRow;

        private ScaleNotSetWarningRow lineScaleNotSetWarningRow;

        private SeamFilterTotalRow seamFilterTotalRow;

        private ScaleNotSetWarningRow seamScaleNotSetWarningRow;

        private ToolStripButton btnFilterLines;

        public LineFilterForm()
        {
            InitializeComponent();

            AddToCursorManagementList();
            this.FormClosed += FinishesEditForm_FormClosed;

            //btnLinesAll.BackColor = Color.Orange;
            //BtnLinesGT0.BackColor = SystemColors.ControlLight;
            //btnLinesNone.BackColor = SystemColors.ControlLight;

            //btnSeamsAll.BackColor = Color.Orange;
            //btnSeamsGT0.BackColor = SystemColors.ControlLight;
            //btnSeamsNone.BackColor = SystemColors.ControlLight;
        }

        public void Init(
            ToolStripButton btnFilterLines
            , LineFinishBaseList lineFinishBaseList
            , SeamFinishBaseList seamFinishBaseList)
        {
            this.btnFilterLines = btnFilterLines;

            Init(lineFinishBaseList);
            Init(seamFinishBaseList);

            this.FormClosed += LineFilterForm_FormClosed;
        }

        private void LineFilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnFilterLines.Checked = lineFinishBaseList.ItemsFiltered();
        }

        public void Init(LineFinishBaseList lineFinishBaseList)
        {
            this.lineFinishBaseList = lineFinishBaseList;
            
            LineFilterRowList = new List<LineFilterRow>();

            this.lineFinishBaseList = lineFinishBaseList;

            int xPos = 1;
            int yPos = 1;

            LineFilterTitleRow areaFilterTitleRow = new LineFilterTitleRow();

            lineFilterTotalRow = new LineFilterTotalRow();

            lineScaleNotSetWarningRow = new ScaleNotSetWarningRow();

            this.pnlLineFilterRows.Controls.Add(areaFilterTitleRow);

            areaFilterTitleRow.Location = new Point(xPos, yPos);

            yPos += areaFilterTitleRow.Height;

            foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
            {
                LineFilterRow lineFilterRow = new LineFilterRow();

                LineFilterRowList.Add(lineFilterRow);

                lineFilterRow.Init(this, lineFinishBase);

                this.pnlLineFilterRows.Controls.Add(lineFilterRow);

                lineFilterRow.Location = new Point(xPos, yPos);

                lineFinishBase.FilteredChanged += LineFinishBase_FilteredChanged;

                yPos += lineFilterRow.Height;
            }

            this.pnlLineFilterRows.Controls.Add(lineFilterTotalRow);

            lineFilterTotalRow.Location = new Point(xPos, yPos);

            this.pnlLineFilterRows.Controls.Add(lineScaleNotSetWarningRow);

            lineScaleNotSetWarningRow.Location = new Point(xPos, yPos + lineFilterTotalRow.Height);

            this.lineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;
            this.lineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;
            this.lineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;
            this.lineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;

            SystemState.ScaleHasBeenSetChanged += SystemState_ScaleHasBeenSetChanged;

            UpdateLineTotals();
        }

        private void SystemState_ScaleHasBeenSetChanged(bool scaleHasBeenSet)
        {
            UpdateLineTotals();
            UpdateSeamTotals();
        }

        public void Init(SeamFinishBaseList seamFinishBaseList)
        {
            this.seamFinishBaseList = seamFinishBaseList;

            SeamFilterRowList = new List<SeamFilterRow>();

            this.seamFinishBaseList = seamFinishBaseList;

            int xPos = 1;
            int yPos = 1;

            SeamFilterTitleRow seamFilterTitleRow = new SeamFilterTitleRow();

            seamScaleNotSetWarningRow = new ScaleNotSetWarningRow();

            this.pnlSeamFilterRows.Controls.Add(seamFilterTitleRow);

            seamFilterTitleRow.Location = new Point(xPos, yPos);

            seamFilterTotalRow = new SeamFilterTotalRow();

            yPos += seamFilterTitleRow.Height;

            foreach (SeamFinishBase seamFinishBase in seamFinishBaseList)
            {
                SeamFilterRow seamFilterRow = new SeamFilterRow();

                SeamFilterRowList.Add(seamFilterRow);

                seamFilterRow.Init(this, seamFinishBase);

                this.pnlSeamFilterRows.Controls.Add(seamFilterRow);

                seamFilterRow.Location = new Point(xPos, yPos);

                seamFinishBase.FilteredChanged += SeamFinishBase_FilteredChanged;

                yPos += seamFilterRow.Height;
            }


            this.pnlSeamFilterRows.Controls.Add(seamFilterTotalRow);

            seamFilterTotalRow.Location = new Point(xPos, yPos);

            this.pnlSeamFilterRows.Controls.Add(seamScaleNotSetWarningRow);

            seamScaleNotSetWarningRow.Location = new Point(xPos, yPos + seamFilterTotalRow.Height);


            this.seamFinishBaseList.ItemAdded += SeamFinishBaseList_ItemAdded;
            this.seamFinishBaseList.ItemInserted += SeamFinishBaseList_ItemInserted;
            this.seamFinishBaseList.ItemRemoved += SeamFinishBaseList_ItemRemoved;
            this.seamFinishBaseList.ItemsSwapped += SeamFinishBaseList_ItemsSwapped;

            UpdateSeamTotals();
        }

        public void UpdateLineTotals()
        {
            if (SystemState.ScaleHasBeenSet)
            {
                lineScaleNotSetWarningRow.Visible = false;
            }

            else
            {
                lineScaleNotSetWarningRow.Visible = true;
            }

            lineFilterTotalRow.Update(lineFinishBaseList);

            combinedTotalRow1.Update(lineFinishBaseList, seamFinishBaseList);
        }


        public void UpdateSeamTotals()
        {
            if (SystemState.ScaleHasBeenSet)
            {
                seamScaleNotSetWarningRow.Visible = false;
            }

            else
            {
                seamScaleNotSetWarningRow.Visible = true;
            }

            seamFilterTotalRow.Update(seamFinishBaseList);

            combinedTotalRow1.Update(lineFinishBaseList, seamFinishBaseList);
        }

        public void Reinit(LineFinishBaseList lineFinishBaseList, SeamFinishBaseList seamFinishBaseList)
        {
            Reinit(lineFinishBaseList);
            Reinit(seamFinishBaseList);
        }

        public void Reinit(LineFinishBaseList lineFinishBaseList)
        {
            lineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            lineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            lineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            lineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;
            SystemState.ScaleHasBeenSetChanged -= SystemState_ScaleHasBeenSetChanged;

            Init(lineFinishBaseList);
        }

        public void Reinit(SeamFinishBaseList seamFinishBaseList)
        {
            Init(seamFinishBaseList);
        }

        private void LineFinishBase_FilteredChanged(LineFinishBase finishBase, bool filtered)
        {
            lineFilterTotalRow.Update(lineFinishBaseList);
        }

        private void SeamFinishBase_FilteredChanged(SeamFinishBase finishBase, bool filtered)
        {
            seamFilterTotalRow.Update(seamFinishBaseList);
        }

        private void LineFinishBaseList_ItemAdded(LineFinishBase lineFinishBase)
        {
            int xPos = 1;
            int yPos = this.lineFilterTotalRow.Location.Y;

            LineFilterRow lineFilterRow = new LineFilterRow();

            lineFilterRow.Init(this, lineFinishBase);

            LineFilterRowList.Add(lineFilterRow);

            this.pnlLineFilterRows.Controls.Add(lineFilterRow);

            lineFilterRow.Location = new Point(xPos, yPos);

            lineFinishBase.FilteredChanged += LineFinishBase_FilteredChanged;

            this.lineFilterTotalRow.Location = new Point(xPos, yPos + lineFilterRow.Height);

            UpdateLineTotals();
        }

        private void SeamFinishBaseList_ItemAdded(SeamFinishBase seamFinishBase)
        {
            int xPos = 1;
            int yPos = this.lineFilterTotalRow.Location.Y;

            SeamFilterRow seamFilterRow = new SeamFilterRow();

            seamFilterRow.Init(this, seamFinishBase);

            SeamFilterRowList.Add(seamFilterRow);

            this.pnlSeamFilterRows.Controls.Add(seamFilterRow);

            seamFilterRow.Location = new Point(xPos, yPos);

            seamFinishBase.FilteredChanged += SeamFinishBase_FilteredChanged;

            seamFilterRow.Location = new Point(xPos, yPos + seamFilterRow.Height);

            UpdateSeamTotals();
        }

        private void LineFinishBaseList_ItemInserted(LineFinishBase lineFinishBase, int position)
        {
            LineFilterRow lineFilterRow = new LineFilterRow();

            lineFilterRow.Init(this, lineFinishBase);

            LineFilterRowList.Insert(position, lineFilterRow);

            this.pnlLineFilterRows.Controls.Add(lineFilterRow);

            int xPos = 1;
            int yPos = this.lineFilterTotalRow.Location.Y + lineFilterRow.Height;

            this.lineFilterTotalRow.Location = new Point(xPos, yPos);

            yPos -= this.lineFilterTotalRow.Height;

            for (int i = LineFilterRowList.Count - 1; i >= position; i--)
            {
                LineFilterRowList[i].Location = new Point(xPos, yPos);

                yPos -= LineFilterRowList[i].Height;
            }

            UpdateLineTotals();
        }

        private void SeamFinishBaseList_ItemInserted(SeamFinishBase seamFinishBase, int position)
        {
            SeamFilterRow seamFilterRow = new SeamFilterRow();

            seamFilterRow.Init(this, seamFinishBase);

            SeamFilterRowList.Insert(position, seamFilterRow);

            this.pnlSeamFilterRows.Controls.Add(seamFilterRow);

            int xPos = 1;
            int yPos = this.seamFilterTotalRow.Location.Y + seamFilterRow.Height;

            this.seamFilterTotalRow.Location = new Point(xPos, yPos);

            yPos -= this.seamFilterTotalRow.Height;

            for (int i = SeamFilterRowList.Count - 1; i >= position; i--)
            {
                SeamFilterRowList[i].Location = new Point(xPos, yPos);

                yPos -= SeamFilterRowList[i].Height;
            }

            UpdateSeamTotals();
        }

        private void LineFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            LineFilterRow filterRow1 = LineFilterRowList[position1];
            LineFilterRow filterRow2 = LineFilterRowList[position2];

            Point point1 = filterRow1.Location;
            Point point2 = filterRow2.Location;

            filterRow1.Location = point2;
            filterRow2.Location = point1;

            LineFilterRowList[position1] = filterRow2;
            LineFilterRowList[position2] = filterRow1;
        }

        private void SeamFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            SeamFilterRow filterRow1 = SeamFilterRowList[position1];
            SeamFilterRow filterRow2 = SeamFilterRowList[position2];

            Point point1 = filterRow1.Location;
            Point point2 = filterRow2.Location;

            filterRow1.Location = point2;
            filterRow2.Location = point1;

            SeamFilterRowList[position1] = filterRow2;
            SeamFilterRowList[position2] = filterRow1;
        }

        private void LineFinishBaseList_ItemRemoved(string guid, int position)
        {
            LineFilterRow areaFilterRow = LineFilterRowList[position];

            int xPos = 1;
            int yPos = areaFilterRow.Location.Y;

            this.pnlLineFilterRows.Controls.Remove(areaFilterRow);

            for (int i = position + 1; i < LineFilterRowList.Count; i++)
            {
                LineFilterRowList[i].Location = new Point(xPos, yPos);

                yPos += LineFilterRowList[i].Height;
            }

            this.lineFilterTotalRow.Location = new Point(xPos, yPos);

            LineFilterRowList.RemoveAt(position);

            UpdateLineTotals();
        }

        private void SeamFinishBaseList_ItemRemoved(string guid, int position)
        {
            SeamFilterRow areaFilterRow = SeamFilterRowList[position];

            int xPos = 1;
            int yPos = areaFilterRow.Location.Y;

            this.pnlSeamFilterRows.Controls.Remove(areaFilterRow);

            for (int i = position + 1; i < SeamFilterRowList.Count; i++)
            {
                SeamFilterRowList[i].Location = new Point(xPos, yPos);

                yPos += SeamFilterRowList[i].Height;
            }

            this.seamFilterTotalRow.Location = new Point(xPos, yPos);

            SeamFilterRowList.RemoveAt(position);

            UpdateSeamTotals();
        }

        private void btnLinesAll_Click(object sender, System.EventArgs e)
        {
            foreach (LineFilterRow lineFilterRow in LineFilterRowList)
            {
                lineFilterRow.Filter(FilteredState.ShowAll);
            }

            //btnLinesAll.BackColor = Color.Orange;
            //BtnLinesGT0.BackColor = SystemColors.ControlLight;
            //btnLinesNone.BackColor = SystemColors.ControlLight;
        }

        private void btnLinesNone_Click(object sender, System.EventArgs e)
        {
            foreach (LineFilterRow lineFilterRow in LineFilterRowList)
            {
                lineFilterRow.Filter(FilteredState.ShowNone);
            }

            //btnLinesAll.BackColor = SystemColors.ControlLight;
            //BtnLinesGT0.BackColor = SystemColors.ControlLight;
            //btnLinesNone.BackColor = Color.Orange;
        }

        private void btnLinesGT0_Click(object sender, System.EventArgs e)
        {
            foreach (LineFilterRow lineFilterRow in LineFilterRowList)
            {
                lineFilterRow.Filter(FilteredState.ShowGT0);
            }

            //btnLinesAll.BackColor = SystemColors.ControlLight;
            //BtnLinesGT0.BackColor = Color.Orange;
            //btnLinesNone.BackColor = SystemColors.ControlLight;
        }

        private void btnSeamsAll_Click(object sender, System.EventArgs e)
        {
            foreach (SeamFilterRow seamFilterRow in SeamFilterRowList)
            {
                seamFilterRow.Filter(FilteredState.ShowAll);
            }

            //btnSeamsAll.BackColor = Color.Orange;
            //btnSeamsGT0.BackColor = SystemColors.ControlLight;
            //btnSeamsNone.BackColor = SystemColors.ControlLight;
        }

        private void btnSeamsNone_Click(object sender, System.EventArgs e)
        {
            foreach (SeamFilterRow seamFilterRow in SeamFilterRowList)
            {
                seamFilterRow.Filter(FilteredState.ShowNone);
            }

            //btnSeamsAll.BackColor = SystemColors.ControlLight;
            //btnSeamsGT0.BackColor = SystemColors.ControlLight;
            //btnSeamsNone.BackColor = Color.Orange;
        }

        private void btnSeamsGT0_Click(object sender, System.EventArgs e)
        {
            foreach (SeamFilterRow seamFilterRow in SeamFilterRowList)
            {
                seamFilterRow.Filter(FilteredState.ShowGT0);
            }

            //btnSeamsAll.BackColor = SystemColors.ControlLight;
            //btnSeamsGT0.BackColor = Color.Orange;
            //btnSeamsNone.BackColor = SystemColors.ControlLight;
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

            foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
            {
                lineFinishBase.FilteredChanged -= LineFinishBase_FilteredChanged;
            }

            this.lineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            this.lineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            this.lineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            this.lineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;

            foreach (SeamFinishBase seamFinishBase in seamFinishBaseList)
            {
                seamFinishBase.FilteredChanged -= SeamFinishBase_FilteredChanged;
            }

            this.seamFinishBaseList.ItemAdded -= SeamFinishBaseList_ItemAdded;
            this.seamFinishBaseList.ItemInserted -= SeamFinishBaseList_ItemInserted;
            this.seamFinishBaseList.ItemRemoved -= SeamFinishBaseList_ItemRemoved;
            this.seamFinishBaseList.ItemsSwapped -= SeamFinishBaseList_ItemsSwapped;
            SystemState.ScaleHasBeenSetChanged -= SystemState_ScaleHasBeenSetChanged;
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
