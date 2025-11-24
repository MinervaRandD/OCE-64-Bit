
namespace FloorMaterialEstimator
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using Globals;
    using FloorMaterialEstimator.Finish_Controls;
    using Graphics;
    using MaterialsLayout;
    using FloorMaterialEstimator.CanvasManager;

    public partial class FloorMaterialEstimatorBaseForm
    {
        public bool SeamingToolVisibleInSelectionMode { get; set; } = false;

        public SeamMode SeamMode
        {
            get
            {
                return SystemState.SeamMode;
            }

            set
            {
                if (value == SystemState.SeamMode)
                {
                    return;
                }

                if (SystemState.SeamMode == SeamMode.Subdivision && SystemState.DrawingShape)
                {
                    btnCancelSubdivision_Click(null, null);
                }

                SystemState.SeamMode = value;
#if DEBUG
                Debug.Assert(DesignState == DesignState.Seam);

                string designStateText = "Design State: Seam(" + value.ToString() + ")";

                this.tlsDesignState.Text = designStateText;


#endif
                foreach (CanvasLayoutArea canvasLayoutArea in currentPage.LayoutAreas)
                {
                    if (canvasLayoutArea.SeamDesignStateSubdivisionModeSelected)
                    {
                        canvasLayoutArea.DeselectForSubdivision();
                    }

                    CanvasManager.LayoutAreaForSubdivision = null;

                    //else if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                    //{
                    //    currentPage.UpdateSeamDesignStateAreaSelectionStatus(canvasLayoutArea, canvasLayoutArea.Guid, 0, 0);
                    //}
                }
                
                if (SystemState.SeamMode == SeamMode.Selection)
                {
                    this.btnCompleteSubdivision.Enabled = false;
                    this.btnCancelSubdivision.Enabled = false;

                    this.btnSeamDesignStateSelectionMode.BackColor = Color.Orange;
                    this.btnSeamDesignStateSubdivisionMode.BackColor = SystemColors.ControlLightLight;

                    this.grbAreaSubdivisionControls.Enabled = false;

                    // Show seaming tool when switching from subdivision mode if it was last visible when you were in subdividion mode.

                    if (SeamingToolVisibleInSelectionMode && !CanvasManager.SeamingTool.IsVisible)
                    {
                        this.CanvasManager.BtnShowSeamingTool_Click(null, null);
                    }


                    CanvasManager.ProcessSeamDesignStateStateChange();

                    this.grbViewSelection.Visible = true;
                    this.grbViewSelection.BringToFront();

                    this.grbRemnantSeamWidth.Visible = false;
                    this.grbRemnantSeamWidth.SendToBack();

                    this.ucRemnantsView.SendToBack();
                    this.ucRemnantsView.Visible = false;

                }

                if (SystemState.SeamMode == SeamMode.Subdivision)
                {
                    this.btnCompleteSubdivision.Enabled = true;
                    this.btnCancelSubdivision.Enabled = true;

                    this.btnSeamDesignStateSelectionMode.BackColor = SystemColors.ControlLightLight;
                    this.btnSeamDesignStateSubdivisionMode.BackColor = Color.Orange;

                    this.grbAreaSubdivisionControls.Enabled = true;

                    // Hide seaming tool when in subdivision mode and save whether it was showing when you switched from
                    // area mode.

                    if (SeamingToolVisibleInSelectionMode = CanvasManager.SeamingTool.IsVisible)
                    {
                        this.CanvasManager.BtnShowSeamingTool_Click(null, null);
                    }

                    
                    CanvasManager.ProcessSeamDesignStateStateChange();

                    this.grbViewSelection.Visible = true;
                    this.grbViewSelection.BringToFront();

                    this.grbRemnantSeamWidth.Visible = false;
                    this.grbRemnantSeamWidth.SendToBack();

                    this.ucRemnantsView.SendToBack();
                    this.ucRemnantsView.Visible = false;

                }

                if (SystemState.SeamMode == SeamMode.Remnant)
                {
                    this.btnCompleteSubdivision.Enabled = false;
                    this.btnCancelSubdivision.Enabled = false;

                    this.btnSeamDesignStateSelectionMode.BackColor = SystemColors.ControlLightLight;
                    this.btnSeamDesignStateSubdivisionMode.BackColor = SystemColors.ControlLightLight;

                    this.grbAreaSubdivisionControls.Enabled = false;

                    // Hide seaming tool when in subdivision mode and save whether it was showing when you switched from
                    // area mode.

                    if (SeamingToolVisibleInSelectionMode = CanvasManager.SeamingTool.IsVisible)
                    {
                        this.CanvasManager.BtnShowSeamingTool_Click(null, null);
                    }

                    CanvasManager.ProcessSeamDesignStateStateChange();

                    //this.ucAreasView.SendToBack();
                    //this.ucAreasView.Visible = false;

                    //this.ucSeamsView.SendToBack();
                    //this.ucSeamsView.Visible = false;

                    this.grbViewSelection.Visible = false;
                    this.grbViewSelection.SendToBack();

                    this.grbRemnantSeamWidth.Visible = true;
                    this.grbRemnantSeamWidth.BringToFront();

                    this.ucRemnantsView.BringToFront();
                    this.ucRemnantsView.Visible = true;
                }
            }
        }

        public void BtnSeamDesignStateSelectionMode_Click(object sender, EventArgs e)
        {
           SeamMode = SeamMode.Selection;
        }

        public void BtnSeamDesignStateSubdivisionMode_Click(object sender, EventArgs e)
        {
            CanvasManager.ResetSelectedLine();
            SeamMode = SeamMode.Subdivision;
        }

        public void btnSeamDesignStateRemnantMode_Click(object sender, EventArgs e)
        {
            SeamMode = SeamMode.Remnant;
        }

        public void btnCompleteSubdivision_Click(object sender, EventArgs e)
        {
            Debug.Assert(SeamMode == SeamMode.Subdivision);

            CanvasManager.ProcessSubdivisionPolylineCompleteShape(true);

        }

        private void btnCancelSubdivision_Click(object sender, EventArgs e)
        {
           // Debug.Assert(SeamMode == SeamMode.Subdivision);

            CanvasManager.ProcessSeamModeCancelShapeInProgress();

            if (CanvasManager.SeamingTool.IsVisible)
            {
                CanvasManager.SeamingTool.Select();
            }
        }

        private void btnRemoveSubdivision_Click(object sender, EventArgs e)
        {
           // Debug.Assert(SeamState == SeamState.SelectArea);
        }

        private void rbnViewAreas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnViewAreas.Checked)
            {
                this.ucAreasView.BringToFront();
                this.ucAreasView.Visible = true;
                this.ucSeamsView.SendToBack();
                this.ucSeamsView.Visible = false;
            }
        }

        private void rbnViewSeams_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnViewSeams.Checked)
            {
                this.ucSeamsView.BringToFront();
                this.ucSeamsView.Visible = true;
                this.ucAreasView.SendToBack();
                this.ucAreasView.Visible = false;
            }
        }

        public void SetupSeamModeDesignState(AreaFinishManager selectedAreaFinishManager)
        {

            foreach (AreaFinishManager areaFinishManager in this.AreaFinishManagerList)
            {
                areaFinishManager.SetupSeamModeDesignState(selectedAreaFinishManager.Guid);
            }

            this.ucAreasView.lblFinish.Text = selectedAreaFinishManager.AreaName;
            
            CanvasManager.UpdateAreaSeamsUndrsOversDataDisplay();
        }

        private void setupSeamTab()
        {
            ucAreasView.Init();
            ucSeamsView.Init();
            ucRemnantsView.Init();

            //DataGridView dgvAreas = ucAreasView.dgvAreas;

            //dgvAreas.Columns.Add("#", "#");
            //dgvAreas.Columns.Add("Area", "Area");

            //dgvAreas.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //dgvAreas.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            //dgvAreas.Columns[0].Width = 32;
            //dgvAreas.Columns[1].Width = dgvAreas.Width - 36;

            //dgvAreas.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvAreas.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //dgvAreas.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //dgvAreas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgvAreas.BringToFront();

            //this.dgvSeams.Columns.Add("Seam Index", "Seam Number");
            //this.dgvSeams.Columns.Add("Length", "Length");

            //this.dgvSeams.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //this.dgvSeams.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            //this.dgvSeams.Columns[0].Width = 32;
            //this.dgvSeams.Columns[1].Width = this.dgvSeams.Width - 36;

            //this.dgvSeams.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dgvSeams.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //this.dgvSeams.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //this.dgvSeams.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //this.dgvSeams.AllowUserToAddRows = false;


            //this.dgvOversUnders.Columns.Add("#", "#");
            //this.dgvOversUnders.Columns.Add("Tag", "Tag");
            //this.dgvOversUnders.Columns.Add("Area", "Area");

            //this.dgvOversUnders.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //this.dgvOversUnders.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //this.dgvOversUnders.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);

            //this.dgvOversUnders.Columns[0].Width = 32;
            //this.dgvOversUnders.Columns[1].Width = 32;
            //this.dgvOversUnders.Columns[2].Width = this.dgvOversUnders.Width - 70;

            //this.dgvOversUnders.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dgvOversUnders.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dgvOversUnders.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //this.dgvOversUnders.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, FontStyle.Regular);
            //this.dgvOversUnders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //this.dgvOversUnders.AllowUserToAddRows = false;


            //this.dgvSeams.SendToBack();
        }

        private void ToggleSeamDesignStateMode()
        {
            if (SeamMode == SeamMode.Selection)
            {
                this.BtnSeamDesignStateSubdivisionMode_Click(null, null);

                return;
            }

            if (SeamMode == SeamMode.Subdivision)
            {
                this.BtnSeamDesignStateSelectionMode_Click(null, null);

                return;
            }
        }
    }
}
