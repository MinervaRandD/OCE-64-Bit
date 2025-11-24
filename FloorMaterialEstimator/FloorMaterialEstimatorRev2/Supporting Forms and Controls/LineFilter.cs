//-------------------------------------------------------------------------------//
// <copyright file="LineFilter.cs" company="Bruun Estimating, LLC">              // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Supporting_Forms
{
    using FloorMaterialEstimator;
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Utilities;

    public partial class LineFilter : Form//, IMessageFilter
    {
        FloorMaterialEstimatorBaseForm baseForm = null;

        private int checkedItemCount = 0;

        public LineFilter(FloorMaterialEstimatorBaseForm baseForm)
        {
            InitializeComponent();

            this.baseForm = baseForm;

            this.clbLineFinishes.Items.Clear();

            foreach (LineFinishManager lineFinishManager in FinishManagerGlobals.LineFinishManagerList)
            {
                clbLineFinishes.Items.Add(lineFinishManager.LineName, !lineFinishManager.Filtered);

                checkedItemCount += lineFinishManager.Filtered ? 0 : 1;
            }

            setupForm();

            //this.MouseEnter += LineFilter_MouseEnter;
            //this.MouseLeave += LineFilter_MouseLeave;

            clbLineFinishes.SelectedIndexChanged += ClbAreaFinishes_SelectedIndexChanged;

            clbLineFinishes.ItemCheck += ClbLineFinishes_ItemCheck;

            setFilteredLinesToolStatus();
        }

        //private void LineFilter_MouseEnter(object sender, EventArgs e)
        //{
        //    baseForm.Cursor = Cursors.Arrow;
        //}

        //private void LineFilter_MouseLeave(object sender, EventArgs e)
        //{
        //    baseForm.SetCursorForCurrentLocation();
        //}

        private void setupForm()
        {
            int countForSize = clbLineFinishes.Items.Count;

            if (countForSize < 6)
            {
                countForSize = 6;
            }

            else if (countForSize > 20)
            {
                countForSize = 20;
            }

            this.clbLineFinishes.Size = new Size(this.clbLineFinishes.Width, 22 * countForSize);

            this.pnlButtonPanel.Location = new Point(this.pnlButtonPanel.Location.X,
                this.clbLineFinishes.Location.Y + this.clbLineFinishes.Height + 16);

            this.Size = new Size(this.Width,
                this.pnlButtonPanel.Location.Y + this.pnlButtonPanel.Height + 48);

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void ClbAreaFinishes_SelectedIndexChanged(object sender, EventArgs e)
        {
            clbLineFinishes.ClearSelected();
        }

        private void ClbLineFinishes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //string lineName = clbLineFinishes.Items[e.Index].ToString();

            baseForm.linePalette[e.Index].LineFinishBase.Filtered = e.NewValue != CheckState.Checked;

            if (e.NewValue != e.CurrentValue)
            {
                checkedItemCount += e.NewValue == CheckState.Checked ? 1 : -1;
            }

            setFilteredLinesToolStatus();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.clbLineFinishes.BeginUpdate();

            for (int i = 0; i < clbLineFinishes.Items.Count; i++)
            {
                //string lineName = this.clbLineFinishes.Items[i].ToString();

                baseForm.linePalette[i].LineFinishBase.Filtered = false;

                clbLineFinishes.SetItemChecked(i, true);
            }

            this.checkedItemCount = clbLineFinishes.Items.Count;

            this.clbLineFinishes.EndUpdate();

            this.clbLineFinishes.Refresh();

            setFilteredLinesToolStatus(false);
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            this.clbLineFinishes.BeginUpdate();

            for (int i = 0; i < clbLineFinishes.Items.Count; i++)
            {
                //string lineName = this.clbLineFinishes.Items[i].ToString();

                baseForm.linePalette[i].LineFinishBase.Filtered = true;

                clbLineFinishes.SetItemChecked(i, false);
            }

            this.checkedItemCount = 0;

            this.clbLineFinishes.EndUpdate();

            this.clbLineFinishes.Refresh();

            setFilteredLinesToolStatus(true);
        }

        private void setFilteredLinesToolStatus()
        {
            baseForm.SetFilteredLineToolsStatus(checkedItemCount != this.clbLineFinishes.Items.Count);
        }

        private void setFilteredLinesToolStatus(bool checkedState)
        {
            baseForm.SetFilteredLineToolsStatus(checkedState);
        }

//        protected override void OnActivated(EventArgs e)
//        {
//            base.OnActivated(e);
//            // install message filter when form activates
//            Application.AddMessageFilter(this);
//        }

//        protected override void OnDeactivate(EventArgs e)
//        {
//            base.OnDeactivate(e);
//            // remove message filter when form deactivates
//            Application.RemoveMessageFilter(this);
//        }

//        public bool PreFilterMessage(ref Message m)
//        {
//            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
//            {
//                baseForm.SetCursorForCurrentLocation();
//#if DEBUG
//                baseForm.UpdateMousePositionDisplay();
//#endif
//            }

//            return false;
//        }
    }
}
