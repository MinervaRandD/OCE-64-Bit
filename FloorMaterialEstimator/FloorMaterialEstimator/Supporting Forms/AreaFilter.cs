//-------------------------------------------------------------------------------//
// <copyright file="AreaFilter.cs" company="Bruun Estimating, LLC">              // 
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


    public partial class AreaFilter : Form
    {
        FloorMaterialEstimatorBaseForm baseForm = null;

        private int checkedItemCount = 0;

        private Dictionary<string, UCFinish> finishDict
        {
            get
            {
                return baseForm.finishPallet.finishDict;
            }
        }

        public AreaFilter(FloorMaterialEstimatorBaseForm baseForm)
        {
            InitializeComponent();

            this.baseForm = baseForm;

            this.clbAreaFinishes.Items.Clear();

            foreach (UCFinish ucFinish in finishDict.Values)
            {
                clbAreaFinishes.Items.Add(ucFinish.finishName, !ucFinish.bIsFiltered);

                checkedItemCount += ucFinish.bIsFiltered ? 0 : 1;
            }

            setupForm();

            clbAreaFinishes.SelectedIndexChanged += ClbAreaFinishes_SelectedIndexChanged;

            clbAreaFinishes.ItemCheck += ClbAreaFinishes_ItemCheck;

            setFilteredAreasToolStatus();
        }

        private void setupForm()
        {
            int countForSize = clbAreaFinishes.Items.Count;

            if (countForSize < 6)
            {
                countForSize = 6;
            }

            else if (countForSize > 20)
            {
                countForSize = 20;
            }

            this.clbAreaFinishes.Size = new Size(this.clbAreaFinishes.Width, 22 * countForSize);

            this.pnlButtonPanel.Location = new Point(this.pnlButtonPanel.Location.X,
                this.clbAreaFinishes.Location.Y + this.clbAreaFinishes.Height + 16);

            this.Size = new Size(this.Width,
                this.pnlButtonPanel.Location.Y + this.pnlButtonPanel.Height + 48);

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
        }

        private void ClbAreaFinishes_SelectedIndexChanged(object sender, EventArgs e)
        {
            clbAreaFinishes.ClearSelected();
        }

        private void ClbAreaFinishes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string finishName = clbAreaFinishes.Items[e.Index].ToString();

            finishDict[finishName].bIsFiltered = e.NewValue == CheckState.Unchecked;

            if (e.NewValue != e.CurrentValue)
            {
                checkedItemCount += e.NewValue == CheckState.Checked ? 1 : -1;
            }
           
            setFilteredAreasToolStatus();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.clbAreaFinishes.BeginUpdate();

            for (int i = 0; i < clbAreaFinishes.Items.Count; i++)
            {
                string finishName = this.clbAreaFinishes.Items[i].ToString();

                finishDict[finishName].bIsFiltered = false;

                clbAreaFinishes.SetItemChecked(i, true);
            }

            checkedItemCount = clbAreaFinishes.Items.Count;

            this.clbAreaFinishes.EndUpdate();

            this.clbAreaFinishes.Refresh();

            setFilteredAreasToolStatus(false);
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            this.clbAreaFinishes.BeginUpdate();

            for (int i = 0; i < clbAreaFinishes.Items.Count; i++)
            {
                string finishName = this.clbAreaFinishes.Items[i].ToString();

                clbAreaFinishes.SetItemChecked(i, false);

                finishDict[finishName].bIsFiltered = true;
            }

            checkedItemCount = 0;

            setFilteredAreasToolStatus(true);

            this.clbAreaFinishes.EndUpdate();

            this.clbAreaFinishes.Refresh();
        }

        private void setFilteredAreasToolStatus()
        {
            baseForm.SetFilteredAreaToolsStatus(checkedItemCount != this.clbAreaFinishes.Items.Count);
        }

        private void setFilteredAreasToolStatus(bool checkedState)
        {
            baseForm.SetFilteredAreaToolsStatus(checkedState);
        }
    }
}
