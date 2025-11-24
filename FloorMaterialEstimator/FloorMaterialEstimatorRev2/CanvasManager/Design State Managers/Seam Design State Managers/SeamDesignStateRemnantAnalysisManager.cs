using FinishesLib;

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using Utilities;
    using Globals;

    using FloorMaterialEstimator.Finish_Controls;

    using Visio = Microsoft.Office.Interop.Visio;
    using FloorMaterialEstimator.Supporting_Forms;
    using SettingsLib;
    using PaletteLib;
    using MaterialsLayout;
    using MaterialsLayout.Subdivision;
    using System.Drawing;
    using Microsoft.VisualBasic;
    using MaterialsLayout.MaterialsLayout;
    using CanvasLib.CanvasShapes;

    public partial class CanvasManager
    {
    
        public void processSeamDesignStateRemnantAnalysisClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            if (button == 1)
            {
                processSeamDesignStateRemnantAnalysisLeftClick(x, y, ref cancelDefault);
            }

            else if (button == 2)
            {
                if (KeyboardUtils.CntlKeyPressed)
                {
                    processSeamDesignStateRemnantAnalysisRghtCntlClick(x, y, ref cancelDefault);
                }

                else
                {
                    processSeamDesignStateRemnantAnalysisRghtBaseClick(x, y, ref cancelDefault);
                    
                }
            }
        }

        private void processSeamDesignStateRemnantAnalysisRghtBaseClick(double x, double y, ref bool cancelDefault)
        {
            processAreaDesignStateLayoutModeRghtClick(x, y);
        }

        private void processSeamDesignStateRemnantAnalysisRghtCntlClick(double x, double y, ref bool cancelDefault)
        {
            //if (processSeamDesignStateProcessSeamingToolClick(x, y))
            //{
            //    return;
            //}

            //RemnantSpecForm remnantSpecForm = new RemnantSpecForm();
            //remnantSpecForm.ShowDialog();
            //remnantSpecForm.BringToFront();

            //if (remnantSpecForm.DialogResult != DialogResult.OK)
            //{
            //    return;
            //}

            if (SystemState.DrawingShape)
            {
                MessageBox.Show("Complete the current drawing before attempting to create embedded cuts.");
                return;
            }

            CanvasLayoutArea selectedCanvasLayoutArea = CurrentPage.GetSelectedLayoutArea(x, y);

            if (selectedCanvasLayoutArea is null)
            {
                MessageBox.Show("You must be within a remnant layout area to create embedded cuts.");
                return;
            }

            if (selectedCanvasLayoutArea.LayoutAreaType != LayoutAreaType.Remnant)
            {
                MessageBox.Show("The layout area selected for remnant analysis must be a remnant area.");
                return;
            }

            string strFeet = BaseForm.txbRemnantWidthFeet.Text.Trim();
            string strInch = BaseForm.txbRemnantWidthInches.Text.Trim();

            if (string.IsNullOrEmpty(strFeet) && string.IsNullOrEmpty(strInch))
            {
                MessageBox.Show("A valid seam width must be provided.");
                return;
            }

            int iFeet = 0;
            int iInch = 0;

            if (!string.IsNullOrEmpty(strFeet))
            {
                if (!Utilities.IsAllDigits(strFeet))
                {
                    MessageBox.Show("A whole number must be provided for the seam width feet.");
                    return;
                }

                iFeet = int.Parse(strFeet);
            }

            if (!string.IsNullOrEmpty(strInch))
            {
                if (!Utilities.IsAllDigits(strInch))
                {
                    MessageBox.Show("A whole number must be provided for the seam width inches.");
                    return;
                }

                iInch = int.Parse(strInch);

                if (iInch > 11)
                {
                    MessageBox.Show("A whole number between 0 and 11 must be provided for the seam width inches.");
                    return;
                }
            }

            SystemState.RemnantSeamWidthInFeet = ((double)iFeet) + ((double)iInch) / 12.0;

            if (layoutAreaSelectedForRemnantAnalysis is null)
            {
                MessageBox.Show("A base layout area must be selected to provide the seam angle.");
                return;
            }

            if (!selectedCanvasLayoutArea.SeamDesignStateSelectionModeSelected)
            {
                MessageBox.Show("The layout area selected for remnant analysis must be selected.");
                return;
            }

            if (selectedCanvasLayoutArea.IsSeamed())
            {
                string seamAreaIndex = "R" + (-selectedCanvasLayoutArea.SeamAreaIndex).ToString();

                if (BaseForm.ucRemnantsView.ContainsRemnantKey(seamAreaIndex))
                {
                    BaseForm.ucRemnantsView.RemoveRemnantArea(seamAreaIndex);
                }
            }

            DirectedLine baseSeamLine = layoutAreaSelectedForRemnantAnalysis.BaseSeamLineWall;

            double deltaY = baseSeamLine.Coord2.Y - baseSeamLine.Coord1.Y;
            double deltaX = baseSeamLine.Coord2.X - baseSeamLine.Coord1.X;

            Coordinate coord1 = new Coordinate(x - deltaX, y - deltaY);
            Coordinate coord2 = new Coordinate(x + deltaX, y + deltaY);

            DirectedLine RemnantSeamBaseLine = new DirectedLine(coord1, coord2);

            selectedCanvasLayoutArea.GenerateSeamsAndRollouts(RemnantSeamBaseLine);
            
            double totalAreaInInches = selectedCanvasLayoutArea.NetAreaInSqrInches();
            double remnantAreaInInches = 0;

            foreach (GraphicsCut graphicsCut in selectedCanvasLayoutArea.GraphicsCutList)
            {
                foreach (GraphicsRemnantCut graphicsRemnantCut in graphicsCut.GraphicsRemnantCutList)
                {
                    remnantAreaInInches += graphicsRemnantCut.AreaInInches;
                }
            }

            BaseForm.ucRemnantsView.AddRemnantArea(
                "R" + -selectedCanvasLayoutArea.SeamIndexTag.SeamAreaIndex
                , selectedCanvasLayoutArea.GraphicsCutList
                , remnantAreaInInches
                , totalAreaInInches);

        }

        private void processSeamDesignStateRemnantAnalysisLeftClick(double x, double y, ref bool cancelDefault)
        {
            List<CanvasLayoutArea> selectedAreaList = CurrentPage.GetSelectedAreaShapeList(x, y, true);

            if (selectedAreaList == null)
            {
                return;
            }

            if (selectedAreaList.Count <= 0)
            {
                return;
            }

            CanvasLayoutArea selectedLayoutArea = null;

            foreach (CanvasLayoutArea layoutArea in selectedAreaList)
            {
                if (layoutArea.LayoutAreaType == LayoutAreaType.Normal)
                {
                    selectNormalLayoutAreaInRemnantMode(layoutArea, x, y);

                    return;
                }

                if (layoutArea.AreaFinishManager.Guid != FinishGlobals.SelectedAreaFinish.Guid)
                {
                    continue;
                }

                selectedLayoutArea = layoutArea;

                break;
            }

            if (selectedLayoutArea is null)
            {
                return;
            }

            if (selectedLayoutArea.AreaFinishBase.MaterialsType == FinishesLib.MaterialsType.Tiles)
            {
                ManagedMessageBox.Show("The finish type for this object is tiled and therefore cannot be selected for seaming.");
                return;
            }

            CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(selectedLayoutArea, FinishGlobals.SelectedAreaFinish.Guid);

            SeamingTool.Select();

            selectedLayoutArea.AreaFinishManager.UpdateFinishStats();
        }

        public CanvasLayoutArea layoutAreaSelectedForRemnantAnalysis { get; set; } = null;

        private void selectNormalLayoutAreaInRemnantMode(CanvasLayoutArea selectedLayoutArea, double x, double y)
        {
            if (!selectedLayoutArea.IsSeamed())
            {
                return;
            }

            if (selectedLayoutArea.SeamIndexTag is null)
            {
                return;
            }

            //if (MathUtils.H2Distance(x, y, selectedLayoutArea.SeamIndexTag.X, selectedLayoutArea.SeamIndexTag.Y) > 0.75)
            //{
            //    return;
            //}

            if (selectedLayoutArea.SelectedForRemnantAnalysis)
            {
                selectedLayoutArea.SelectedForRemnantAnalysis = false;

                layoutAreaSelectedForRemnantAnalysis = null;

                return;
            }

            foreach (CanvasLayoutArea canvasLayoutArea in selectedLayoutArea.AreaFinishManager.CanvasLayoutAreas)
            {
                if (canvasLayoutArea == selectedLayoutArea)
                {
                    continue;
                }

                canvasLayoutArea.SelectedForRemnantAnalysis = false;
            }

            selectedLayoutArea.SelectedForRemnantAnalysis = true;

            layoutAreaSelectedForRemnantAnalysis = selectedLayoutArea;

            return;
        }

        private void completeSeamDesignStateRemnantModePolylineDraw(CanvasDirectedPolyline polyline)
        {        
            CanvasDirectedLine lastLine = BuildingPolyline.LastLine;
            CanvasDirectedLine frstLine = BuildingPolyline.FrstLine;

            if (GlobalSettings.OperatingModeSetting == OperatingMode.Normal)
            {
                VisioInterop.SetLineBeginShape(frstLine.Shape, 0, 0);
            }

            RemoveMarkerAndGuides();

            if (!(buildingPolyline.StartMarker is null))
            {
                buildingPolyline.StartMarker.Delete();
                buildingPolyline.StartMarker = null;
            }

            frstLine.SetLineStartpoint(lastLine.GetLineEndpoint());

            //frstLine.SyncWithVisio();
            UCAreaFinishPaletteElement ucAreaFinish = null;
            AreaFinishManager areaFinishManager = null;

            //if (finishIndex > 0)
            //{
            //    ucAreaFinish = PalettesGlobal.AreaFinishPalette[finishIndex - 1];
            //}

            AddRemnantPolylineToCanvas(BuildingPolyline, areaFinishManager, ucAreaFinish);

            polyline.Delete();

            SystemState.DrawingShape = false;

            buildingPolyline = null;

            SystemState.DrawingMode = DrawingMode.Default;

            Window?.DeselectAll();
        }


       public void AddRemnantPolylineToCanvas(
           CanvasDirectedPolyline canvasDirectedPolyline
           , AreaFinishManager areaFinishManager = null
           , UCAreaFinishPaletteElement ucAreaFinish = null
           , UCLineFinishPaletteElement ucLineFinish = null)
        {
            if (areaFinishManager == null)
            {
                areaFinishManager = FinishManagerGlobals.SelectedAreaFinishManager;
            }

            if (ucAreaFinish == null)
            {
                ucAreaFinish = selectedFinishType;
            }

            if (ucLineFinish == null)
            {
                ucLineFinish = SelectedLineType;
            }
#if DEBUG
            canvasDirectedPolyline.ValidateConsistentPerimeter();
#endif

            canvasDirectedPolyline.BuildStatus = AreaShapeBuildStatus.Completed;

            CanvasDirectedPolygon canvasDirectedPolygon = new CanvasDirectedPolygon(this, Window, Page);

            foreach (CanvasDirectedLine canvasDirectedLine in canvasDirectedPolyline)
            {
                LineFinishManager lineFinishManager = canvasDirectedLine.LineFinishManager;

                UCLineFinishPaletteElement ucLine = canvasDirectedLine.ucLine;

                //canvasDirectedLine.Draw();

                GraphicsDirectedLine graphicsDirectedLine = new GraphicsDirectedLine(Window, Page, new DirectedLine(canvasDirectedLine.Coord1, canvasDirectedLine.Coord2), LineRole.SingleLine);

                //graphicsDirectedLine.Draw(Color.Black, 2);

                CanvasDirectedLine canvasDirectedLine1 = new CanvasDirectedLine(this, lineFinishManager, graphicsDirectedLine, DesignState.Seam);

                canvasDirectedLine1.CanvasLineType = CanvasLineType.Remnant;

                //canvasDirectedLine1.Shape = graphicsDirectedLine.Shape;

                canvasDirectedLine1.ucLine = ucLine;

                canvasDirectedLine1.Draw();

                lineFinishManager.AddLine(canvasDirectedLine1);

                lineFinishManager.AddLineToLayer(canvasDirectedLine1.Guid, DesignState.Seam, SeamMode.Remnant);

                canvasDirectedPolygon.PerimeterAdd(canvasDirectedLine1);

                canvasDirectedLine1.ParentPolygon = canvasDirectedPolygon;

                CurrentPage.AddToDirectedLineDict(canvasDirectedLine1);
            }

            UCLineFinishPaletteElement debugUCLine = BaseForm.selectedLineFinish;

            addRemnantOverlappingInternalAreaToCanvas(canvasDirectedPolygon, areaFinishManager, ucAreaFinish);

            canvasDirectedPolyline.Delete();

            //buildingPolyline.Delete();

            // foreach (CanvasDirectedLine canvasDirectedLine in canvasDirectedPolygon.Perimeter)
            // {
            //UCLineFinishPaletteElement ucLine = canvasDirectedLine.ucLine;

            //canvasDirectedLine.Draw();

            //ucLine.AddLine(canvasDirectedLine);

            //ucLine.RemnantSeamDesignStateLayer.Add(canvasDirectedLine.Shape, 1);

            //ucLine.AddLineToLayer(canvasDirectedLine.Guid, DesignState.Seam, SeamMode.Remnant);
            //}

            Window?.DeselectAll();


        }

        private void addRemnantOverlappingInternalAreaToCanvas(
            CanvasDirectedPolygon canvasDirectedPolygon
            , AreaFinishManager areaFinishManager
            , UCAreaFinishPaletteElement ucAreaFinish)
        {
            canvasDirectedPolygon.CreateInternalAreaShape();

#if DEBUG
            BaseForm.UpdateDebugForm();
#endif
            FinishesLibElements finishLibElements = new FinishesLibElements(
                areaFinishManager.AreaFinishBase
                , null
                , null
                , areaFinishManager.AreaFinishLayers
                , null
                , null);

            CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(
                this
                //, ucAreaFinish
                , areaFinishManager
                , finishLibElements
                , canvasDirectedPolygon
                , new List<CanvasDirectedPolygon>()
                , DesignState.Seam);

            canvasLayoutArea.LayoutAreaType = LayoutAreaType.Remnant;

            addRemnantAreaCanvasLayoutAreaToCanvas(canvasLayoutArea, ucAreaFinish);
        }

        private void addRemnantAreaCanvasLayoutAreaToCanvas(CanvasLayoutArea canvasLayoutArea, UCAreaFinishPaletteElement ucAreaFinish)
        {
            CurrentPage.SelectedLayoutArea = canvasLayoutArea;

            canvasLayoutArea.SetCompletedLineWidth();

            canvasLayoutArea.AreaFinishManager.AddRemnantLayoutArea(canvasLayoutArea);

            CurrentPage.AddLayoutArea(canvasLayoutArea);

            canvasLayoutArea.Lock();


            //this.AreaHistoryList.Add(canvasLayoutArea);
        }
    }
}