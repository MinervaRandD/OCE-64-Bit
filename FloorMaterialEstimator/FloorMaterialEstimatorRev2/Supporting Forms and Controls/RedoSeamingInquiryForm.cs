using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

namespace FloorMaterialEstimator
{
    using System.Windows.Forms;
    using FinishesLib;
    using CanvasManager;
    using FloorMaterialEstimator;

    public partial class RedoSeamingInquiryForm : Form
    {
        private FloorMaterialEstimatorBaseForm baseForm;

        private AreaFinishBaseList areaFinishList;

        public RedoSeamingInquiryForm(
            FloorMaterialEstimatorBaseForm baseForm
            ,AreaFinishBaseList areaFinishList)
        {
            InitializeComponent();

            this.baseForm = baseForm;

            this.areaFinishList = areaFinishList;

            FormBorderStyle = FormBorderStyle.FixedSingle;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRedoAllSeamsForCurrentFinish_Click(object sender, EventArgs e)
        {
            List<CanvasLayoutArea> canvasLayoutAreaList = new List<CanvasLayoutArea>();

            AreaFinishBase selectedAreaFinishBase = areaFinishList.SelectedItem;

            foreach (CanvasLayoutArea canvasLayoutArea in baseForm.CurrentPage.LayoutAreas)
            {
                if (canvasLayoutArea.AreaFinishBase == selectedAreaFinishBase)
                {
                    if (canvasLayoutArea.IsSeamed())
                    {
                        canvasLayoutAreaList.Add(canvasLayoutArea);

                        canvasLayoutArea.RemoveSeamsAndRollouts();
                        //canvasLayoutArea.RemoveSeamIndexTag();
                    }
                }
            }

            foreach (CanvasLayoutArea canvasLayoutArea in canvasLayoutAreaList)
            {
                canvasLayoutArea.RegenerateSeamsAndCuts();

                canvasLayoutArea.AreaFinishManager.UpdateFinishStats();

                canvasLayoutArea.DrawSeams();
            }

            if (canvasLayoutAreaList.Count > 0)
            {
                baseForm.CanvasManager.UpdateAreaSeamsUndrsOversDataDisplay();

                if (selectedAreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    baseForm.OversUndersFormUpdate();
                }
            }
        }

        private void btnClearAllSeamsForTheCurrentFinish_Click(object sender, EventArgs e)
        {
            List<CanvasLayoutArea> canvasLayoutAreaList = new List<CanvasLayoutArea>();

            AreaFinishBase selectedAreaFinishBase = areaFinishList.SelectedItem;

            foreach (CanvasLayoutArea canvasLayoutArea in baseForm.CurrentPage.LayoutAreas)
            {
                if (canvasLayoutArea.AreaFinishBase != selectedAreaFinishBase)
                {
                    continue;
                }

                canvasLayoutArea.SeamDesignStateSelectionModeSelected = false;

                if (!(canvasLayoutArea.SeamIndexTag is null))
                {
                    canvasLayoutArea.AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea.SeamIndexTag, 1);

                    canvasLayoutArea.SeamIndexTag.Delete();
                }

                if (canvasLayoutArea.IsSeamed())
                {
                        
                    canvasLayoutArea.RemoveSeamIndexTag();
                    canvasLayoutArea.RemoveSeamsAndRollouts(this.ckbIncludeManualSeams.Checked);
                    canvasLayoutArea.RemoveEmbeddedOvers();
                       

                    canvasLayoutArea.BaseSeamLineWall = null;

                    canvasLayoutAreaList.Add(canvasLayoutArea);
                }
            }

            foreach (CanvasLayoutArea canvasLayoutArea in canvasLayoutAreaList)
            {
                canvasLayoutArea.AreaFinishManager.UpdateFinishStats();
            }

            if (canvasLayoutAreaList.Count > 0)
            {
                baseForm.CanvasManager.UpdateAreaSeamsUndrsOversDataDisplay();

                if (selectedAreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    baseForm.OversUndersFormUpdate();
                }
            }
        }

        private void btnClearAllSeamsForAllFinishes_Click(object sender, EventArgs e)
        {
            List<CanvasLayoutArea> canvasLayoutAreaList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in baseForm.CurrentPage.LayoutAreas)
            {
                canvasLayoutArea.SeamDesignStateSelectionModeSelected = false;

                if (!(canvasLayoutArea.SeamIndexTag is null))
                {
                    canvasLayoutArea.AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(canvasLayoutArea.SeamIndexTag, 1);

                    canvasLayoutArea.SeamIndexTag.Delete();
                }

                if (canvasLayoutArea.IsSeamed())
                {

                    canvasLayoutArea.RemoveSeamIndexTag();
                    canvasLayoutArea.RemoveSeamsAndRollouts(this.ckbIncludeManualSeams.Checked);
                    canvasLayoutArea.RemoveEmbeddedOvers();


                    canvasLayoutArea.BaseSeamLineWall = null;

                    canvasLayoutAreaList.Add(canvasLayoutArea);
                }
            }

            foreach (CanvasLayoutArea canvasLayoutArea in canvasLayoutAreaList)
            {
                canvasLayoutArea.AreaFinishManager.UpdateFinishStats();
            }

            if (canvasLayoutAreaList.Count > 0)
            {
                baseForm.CanvasManager.UpdateAreaSeamsUndrsOversDataDisplay();

                baseForm.OversUndersFormUpdate();
            }
        }
    }
}
