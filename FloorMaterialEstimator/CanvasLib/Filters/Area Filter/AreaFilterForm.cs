

using Globals;

namespace CanvasLib.Filters.Area_Filter
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using FinishesLib;

    using Utilities.Supporting_Controls;

    using Utilities;

    using System.Windows.Forms;

    public partial class AreaFilterForm : Form, ICursorManagementForm
    {
        private AreaFinishBaseList areaFinishBaseList;

        private List<AreaFilterRow> AreaFilterRowList;

        private AreaFilterTotalRow areaFilterTotalRow;

        private ScaleNotSetWarningRow scaleNotSetWarningRow;

        private ToolStripButton btnFilterAreas;

        public AreaFilterForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;

            this.FormClosed += AreaFilterForm_FormClosed;

            AddToCursorManagementList();

            this.FormClosed += FinishesEditForm_FormClosed;

            //this.btnAll.BackColor = Color.Orange;
        }


        public void Init(
            ToolStripButton btnFilterAreas
            ,AreaFinishBaseList areaFinishBaseList)
        {
            this.areaFinishBaseList = areaFinishBaseList;

            this.btnFilterAreas = btnFilterAreas;

            AreaFilterRowList = new List<AreaFilterRow>();

            int xPos = 1;
            int yPos = 1;

            AreaFilterTitleRow areaFilterTitleRow = new AreaFilterTitleRow();

            this.pnlAreaFilterRows.Controls.Add(areaFilterTitleRow);

            areaFilterTitleRow.Location = new Point(xPos, yPos);

            yPos += areaFilterTitleRow.Height;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                AreaFilterRow areaFilterRow = new AreaFilterRow();

                AreaFilterRowList.Add(areaFilterRow);

                areaFilterRow.Init(this, areaFinishBase);

                this.pnlAreaFilterRows.Controls.Add(areaFilterRow);

                areaFilterRow.Location = new Point(xPos, yPos);

                areaFinishBase.FilteredChanged += AreaFinishBase_FilteredChanged;

                yPos += areaFilterRow.Height;
            }

            areaFilterTotalRow = new AreaFilterTotalRow();

            this.pnlAreaFilterRows.Controls.Add(areaFilterTotalRow);

            areaFilterTotalRow.Location = new Point(xPos, yPos);

            scaleNotSetWarningRow = new ScaleNotSetWarningRow();

            this.pnlAreaFilterRows.Controls.Add(scaleNotSetWarningRow);

            yPos += areaFilterTotalRow.Height;

            scaleNotSetWarningRow.Location = new Point(xPos, yPos);

            areaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;

            UpdateTotals();
        }

        public void UpdateTotals()
        {
            if (!SystemState.ScaleHasBeenSet)
            {
                this.scaleNotSetWarningRow.Visible = true;
            }

            else
            {
                this.scaleNotSetWarningRow.Visible = false;
            }

            areaFilterTotalRow.Update(areaFinishBaseList);
        }

        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            areaFilterTotalRow.Update(areaFinishBaseList);
        }

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase areaFinishBase)
        {
            int xPos = 1;
            int yPos = this.areaFilterTotalRow.Location.Y;

            AreaFilterRow areaFilterRow = new AreaFilterRow();

            areaFilterRow.Init(this, areaFinishBase);

            AreaFilterRowList.Add(areaFilterRow);

            this.pnlAreaFilterRows.Controls.Add(areaFilterRow);

            areaFilterRow.Location = new Point(xPos, yPos);

            areaFinishBase.FilteredChanged += AreaFinishBase_FilteredChanged;

            this.areaFilterTotalRow.Location = new Point(xPos, yPos + areaFilterRow.Height);

            UpdateTotals();
        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase areaFinishBase, int position)
        {
            AreaFilterRow areaFilterRow = new AreaFilterRow();

            areaFilterRow.Init(this, areaFinishBase);

            AreaFilterRowList.Insert(position, areaFilterRow);

            this.pnlAreaFilterRows.Controls.Add(areaFilterRow);

            int xPos = 1;
            int yPos = this.areaFilterTotalRow.Location.Y + areaFilterRow.Height;

            this.areaFilterTotalRow.Location = new Point(xPos, yPos);

            yPos -= this.areaFilterTotalRow.Height;

            for (int i = AreaFilterRowList.Count - 1; i >= position; i--)
            {
                AreaFilterRowList[i].Location = new Point(xPos, yPos);

                yPos -= AreaFilterRowList[i].Height;
            }

            UpdateTotals();
        }

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            AreaFilterRow filterRow1 = AreaFilterRowList[position1];
            AreaFilterRow filterRow2 = AreaFilterRowList[position2];

            Point point1 = filterRow1.Location;
            Point point2 = filterRow2.Location;

            filterRow1.Location = point2;
            filterRow2.Location = point1;

            AreaFilterRowList[position1] = filterRow2;
            AreaFilterRowList[position2] = filterRow1;
        }

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            AreaFilterRow areaFilterRow = AreaFilterRowList[position];

            int xPos = 1;
            int yPos = areaFilterRow.Location.Y;

            this.pnlAreaFilterRows.Controls.Remove(areaFilterRow);

            for (int i = position + 1; i < AreaFilterRowList.Count; i++)
            {
                AreaFilterRowList[i].Location = new Point(xPos, yPos);

                yPos += AreaFilterRowList[i].Height;
            }

            this.areaFilterTotalRow.Location = new Point(xPos, yPos);

            AreaFilterRowList.RemoveAt(position);

            UpdateTotals();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (AreaFilterRow areaFilterRow in AreaFilterRowList)
            {
                areaFilterRow.SetFiltered(FilteredState.ShowAll);
            }

            //this.btnAll.BackColor = Color.Orange;
            //this.btnNone.BackColor = SystemColors.ControlLight;
            //this.BtnGTZero.BackColor = SystemColors.ControlLight;
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (AreaFilterRow areaFilterRow in AreaFilterRowList)
            {
                areaFilterRow.SetFiltered(FilteredState.ShowNone);
            }

            //this.btnAll.BackColor = SystemColors.ControlLight;
            ////this.btnNone.BackColor = Color.Orange;
            this.BtnGTZero.BackColor = SystemColors.ControlLight;
        }

        private void btnGTZero_Click(object sender, EventArgs e)
        {
            foreach (AreaFilterRow areaFilterRow in AreaFilterRowList)
            {
                areaFilterRow.SetFiltered(FilteredState.ShowGT0);
            }

            //this.btnAll.BackColor = SystemColors.ControlLight;
            //this.btnNone.BackColor = SystemColors.ControlLight;
            //this.BtnGTZero.BackColor = Color.Orange;
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

        private void AreaFilterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnFilterAreas.Checked = areaFinishBaseList.ItemsFiltered();

            areaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            areaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            areaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            areaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                areaFinishBase.FilteredChanged -= AreaFinishBase_FilteredChanged;
            }
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
