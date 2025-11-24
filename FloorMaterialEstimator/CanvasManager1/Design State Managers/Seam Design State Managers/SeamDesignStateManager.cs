namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
  
    using System.Windows.Forms;

    using Globals;
    using FinishesLib;
    using PaletteLib;
    using FloorMaterialEstimator.Finish_Controls;
    using Graphics;
    using Geometry;
    using OversUnders;
    using Utilities;
    using System.Drawing;

    public partial class CanvasManager
    {

        public void ProcessSeamDesignStateStateChange()
        {
            CanvasLayoutArea layoutAreaForSubdivision = null;

            layoutAreaForSubdivisionDict.TryGetValue(FinishGlobals.SelectedAreaFinish.Guid, out layoutAreaForSubdivision);

            if (BaseForm.SeamMode == SeamMode.Selection)
            {
                if (layoutAreaForSubdivision != null)
                {
                    layoutAreaForSubdivision.DeselectForSubdivision();

                    layoutAreaForSubdivision = null;
                }
            }

            bool visibility = BaseForm.SeamMode == SeamMode.Remnant;

            foreach (AreaFinishManager areaFinishManager in CanvasManagerGlobals.AreaFinishManagerList)
            {
                areaFinishManager.RemnantSeamDesignStateLayer.SetLayerVisibility(visibility);
            }

            foreach (LineFinishManager lineFinishManager in CanvasManagerGlobals.LineFinishManagerList)
            {
                lineFinishManager.RemnantSeamDesignStateLayer.SetLayerVisibility(visibility);
            }

            //CurrentPage.RmntEmbdLayer.SetLayerVisibility(visibility && BaseForm.ckbShowEmbeddedCuts.Checked);
        }

        public void ProcessSeamDesignStateClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            //if (CurrentPage.MouseOverSeamingTool(x, y, SeamingTool))
            //{
            //    SeamingTool.Show();

            //    cancelDefault = false;

            //    return;
            //}

            cancelDefault = true;

            if (BaseForm.CounterMode)
            {
                if (button == 1)
                {
                    MessageBox.Show("You are in counter mode. Right click to add a counter.");

                    return;
                }
                if (button == 2)
                {
                    CounterController.ProcessCounterModeClick(x, y);
                }

                return;
            }

            else if (BaseForm.SeamMode == SeamMode.Subdivision)
            {
                ProcessSeamDesignStateSubdivideAreaClick(button, keyButtonState, x, y, ref cancelDefault);

                return;
            }

            else if (BaseForm.SeamMode == SeamMode.Selection)
            {
                processSeamDesignStateSelectAreaClick(button, keyButtonState, x, y, ref cancelDefault);

                return;
            }

            else if (BaseForm.SeamMode == SeamMode.Remnant)
            {
                processSeamDesignStateRemnantAnalysisClick(button, keyButtonState, x, y, ref cancelDefault);

                return;
            }

            else if (BaseForm.AreaModeLabelMode)
            {
                LabelManager.ProcessLabelModeClick(x, y);

                return;
            }

            BaseForm.CurrentProjectChanged = true;
        }

        private void ProcessSeamDesignModeCompleteShape()
        {
            ProcessPolylineCompleteShape(true);
        }

        /// <summary>
        /// Remove the last line created during drawing. Currently only applies to polyline.
        /// </summary>
        private void ProcessSeamModeDeleteBuildingLine()
        {
            RemoveBuildingPolyLineBuildingLine();
        }

        public void ProcessSeamModeCompleteShape()
        {
            ProcessPolylineCompleteShape(true);

            RemoveMarkerAndGuides();
        }

        private void completeSeamDesignStatePolylineDraw(CanvasDirectedPolyline polyline)
        {
            if (BaseForm.SeamMode == SeamMode.Subdivision)
            {
                completeSeamDesignStateSubdivisionModePolylineDraw(polyline);
            }

            else if (BaseForm.SeamMode == SeamMode.Remnant)
            {
                completeSeamDesignStateRemnantModePolylineDraw(polyline);
            }
        }


        public void ProcessSeamModeCancelShapeInProgress()
        {
            DeleteBuildingPolyLine();

            RemoveMarkerAndGuides();
        }

        public void UpdateAreaAndSeamsStats()
        {
            string guid = FinishGlobals.SelectedAreaFinish.Guid;
        }

        public void UpdateAreaSeamsUndrsOversDataDisplay()
        {
            string guid = FinishGlobals.SelectedAreaFinish.Guid;

            updateAreasDataDisplay(guid);
            updateSeamsDataDisplay(guid);
            //updateUndrsOversDisplay(guid);
           
        }

        private void updateAreasDataDisplay(string guid)
        {
            UCAreaFinishPaletteElement e = PalettesGlobal.AreaFinishPalette[guid];

            double totalAreaForFinish = PalettesGlobal.AreaFinishPalette[guid].AreaFinishBase.NetAreaInSqrInches / 144.0;

            string areaName = FinishGlobals.SelectedAreaFinish.AreaName;
            
            BaseForm.ucAreasView.Setup(areaName, CurrentPage.SeamModeAreaData(guid), totalAreaForFinish);
        }

        private void updateSeamsDataDisplay(string guid)
        {
            //double totalLnth = 0;

            string seamName = string.Empty;

            AreaFinishBase selectedAreaFinish = FinishGlobals.SelectedAreaFinish;

            if (Utilities.IsNotNull(selectedAreaFinish))
            {
                if (Utilities.IsNotNull(selectedAreaFinish))
                {
                    SeamFinishBase seamFinishBase = selectedAreaFinish.SeamFinishBase;

                    if (Utilities.IsNotNull(seamFinishBase))
                    {
                        seamName = seamFinishBase.SeamName;
                    }
                }
            }

            BaseForm.ucSeamsView.Setup(seamName, CurrentPage.SeamModeSeamData(guid));
        }


        private SortedList<int, List<MaterialArea>> overageList = new SortedList<int, List<MaterialArea>>();
        private SortedList<int, List<MaterialArea>> undrageList = new SortedList<int, List<MaterialArea>>();

        //private void updateUndrsOversDisplay(string guid)
        //{
        //    generateOversUndrsLists();

        //     BaseForm.dgvOversUnders.Rows.Clear();

        //    int tag = 0;

        //    foreach (KeyValuePair<int, List<MaterialArea>> kvp in undrageList)
        //    {
        //        string indexStr = kvp.Key.ToString();

        //        foreach (MaterialArea underage in kvp.Value)
        //        {
        //            string tagStr = genTagStr(tag++);
        //            string underageStr = underage.ToString();

        //            BaseForm.dgvOversUnders.Rows.Add(new object[] { indexStr, tagStr, underageStr });
        //        }
        //    }

        //    foreach (KeyValuePair<int, List<MaterialArea>> kvp in overageList)
        //    {
        //        string indexStr = kvp.Key.ToString();

        //        foreach (MaterialArea overage in kvp.Value)
        //        {
        //            string tagStr = genTagStr(tag++);
        //            string overageStr = overage.ToString();

        //            BaseForm.dgvOversUnders.Rows.Add(new object[] { indexStr, tagStr, overageStr });
        //        }
        //    }

        //    BaseForm.dgvOversUnders.CurrentCell = null;

        //    BaseForm.dgvOversUnders.Refresh();
        //}

        //private void generateOversUndrsLists()
        //{
        //    overageList.Clear();
        //    undrageList.Clear();

        //    foreach (MaterialArea materialArea in FinishGlobals.SelectedAreaFinish.OversMaterialAreaList)
        //    {
        //        int index = materialArea.SeamAreaIndex;

        //        if (index <= 0)
        //        {
        //            continue;
        //        }

        //        List<MaterialArea> materialAreaList = null;

        //        if (overageList.ContainsKey(index))
        //        {
        //            materialAreaList = overageList[index];
        //        }

        //        else
        //        {
        //            materialAreaList = new List<MaterialArea>();
        //            overageList[index] = materialAreaList;
        //        }

        //        materialAreaList.Add(materialArea);
        //    }

        //    foreach (MaterialArea materialArea in FinishGlobals.SelectedAreaFinish.UndrsMaterialAreaList)
        //    {
        //        int index = materialArea.SeamAreaIndex;

        //        if (index <= 0)
        //        {
        //            continue;
        //        }

        //        List<MaterialArea> materialAreaList = null;

        //        if (undrageList.ContainsKey(index))
        //        {
        //            materialAreaList = undrageList[index];
        //        }

        //        else
        //        {
        //            materialAreaList = new List<MaterialArea>();
        //            undrageList[index] = materialAreaList;
        //        }

        //        materialAreaList.Add(materialArea);
        //    }
        //}

        string genTagStr(int tag)
        {
            string returnStr = string.Empty;

            while (true)
            {
                returnStr = returnStr + (char)('A' + tag % 26);

                tag /= 26;

                if (tag == 0)
                {
                    break;
                }
            }

            return returnStr;
        }

        internal void ProcessSeamModeFinishNumericShortCut(int key)
        {
            int position = key - 1;

            if (position < 0 || position >= FinishGlobals.AreaFinishBaseList.Count)
            {
                return;
            }

            if (FinishGlobals.AreaFinishBaseList[position].MaterialsType == MaterialsType.Rolls)
            {
                FinishGlobals.AreaFinishBaseList.SelectElem(position);
            }
        }

        public void SetupAllSeamStateSeamLayersForSelectedArea()
        {
            CanvasManagerGlobals.AreaFinishManagerList.SetupAllSeamLayers();
        }
    }
}
