

namespace FloorMaterialEstimator
{
    using CanvasLib.Counters;
    using CanvasLib.DoorTakeouts;
    using CanvasLib.Legend;
    using CanvasLib.Markers_and_Guides;
    using FinishesLib;
    using FloorMaterialEstimator.CanvasManager;
    using Geometry;
    using Globals;
    using Graphics;
    using MaterialsLayout;
    using Microsoft.Office.Interop.Visio;
    using PaletteLib;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web.UI;
    using System.Windows.Forms;
    using Utilities;
    using static System.Windows.Forms.LinkLabel;

    public class ProjectImporter
    {
        FloorMaterialEstimatorBaseForm baseForm;

        private GraphicsWindow window => baseForm.CanvasManager.Window;

        private GraphicsPage page => baseForm.CanvasManager.Page;

        public ProjectImporter(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;
        }

        public Dictionary<string, CanvasDirectedLine> CanvasDirectedLineDict;

        public Dictionary<string, CanvasLayoutArea> CanvasLayoutAreaDict;

        public Dictionary<string, CanvasDirectedPolygon> CanvasDirectedPolygonDict = new Dictionary<string, CanvasDirectedPolygon>();

        public Dictionary<string, GraphicsRollout> GraphicsRolloutDict = new Dictionary<string, GraphicsRollout>();

        public List<GraphicsCounter> GraphicsCounterList = new List<GraphicsCounter>();

        public List<DoorTakeout> DoorTakeoutList = new List<DoorTakeout>();

        public List<GraphicsUndrage> GraphicsUndrageList = new List<GraphicsUndrage>();

        public delegate void ImportProgressEventHandler(string stepName, int stepWeight);

        public event ImportProgressEventHandler ImportProgressEvent;

        public delegate void ImportStatusEventHandler(string stepName);

        public event ImportStatusEventHandler ImportStatusEvent;

        private FloorMaterialEstimator.CanvasManager.CanvasManager canvasManager => baseForm.CanvasManager;

        private CanvasPage currentPage => canvasManager.CurrentPage;

        public string ImportProject(string projectFileName)
        {
            SystemState.LoadingExistingProject = true;

            VisioInterop.DisableScreenUpdating();

            string rtrnString = doImportProject(projectFileName);

            VisioInterop.EnableScreenUpdating();

            SystemState.LoadingExistingProject = false;

            window.DeselectAll();

            return rtrnString;
        }

        ProgressReporterForm progressReportForm = null;

        private string doImportProject(string projectFileName)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { projectFileName });
#endif
          
            if (string.IsNullOrEmpty(projectFileName))
            {
                return string.Empty;
            }

            Point location = baseForm.Location;

            if ((new FileInfo(projectFileName)).Length > 250 * 1024)
            {
                progressReportForm = new ProgressReporterForm();

                progressReportForm.Init(this);

                progressReportForm.Location = location;

                progressReportForm.Location = new System.Drawing.Point(baseForm.Location.X + 100, baseForm.Location.Y + 100);
                progressReportForm.Show();
            }
            
            updateStatus("Reading project file.");

            StreamReader serialReader = new StreamReader(projectFileName);

            ProjectSerializable project = ProjectSerializable.Deserialize(serialReader); updateProgress(1);

            if (project == null)
            {

                if (progressReportForm != null)
                {
                    progressReportForm.Close();
                }

                return string.Empty;

            }

            baseForm.CurrentProjectName = project.CurrentProjectName;

            SystemGlobals.OriginalImagePath = project.OriginalImagePath;

            if (project.Version != Program.Version)
            {
                DialogResult result = ManagedMessageBox.Show("The version of this project is not consistent with the current release. Do you want to continue?", "Inconsistent Version", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {

                    if (progressReportForm != null)
                    {
                        progressReportForm.Close();
                    }

                    return string.Empty;
                }
            }

            SystemState.XtraLgthFeet = project.XtraLgthFeet;
            SystemState.XtraLgthInch = project.XtraLgthInch;

            updateStatus("Parsing project file.");

            setupCurrentPage(project.Page); updateProgress(1);

            baseForm.tbcPageAreaLine.Hide();

            if (FinishManagerGlobals.AreaFinishManagerList != null)
            {
                FinishManagerGlobals.AreaFinishManagerList.Dispose();
            }

            // Kludge to assure all area finishes are represented in square feet

            project.AreaFinishBaseList.ForEach(a => a.AreaDisplayUnits = AreaDisplayUnits.SquareFeet);

            FinishGlobals.AreaFinishBaseList = project.AreaFinishBaseList;

            if (FinishManagerGlobals.AreaFinishManagerList != null)
            {
                FinishManagerGlobals.AreaFinishManagerList.Dispose();
            }

            FinishManagerGlobals.AreaFinishManagerList = new AreaFinishManagerList(window, page, FinishGlobals.AreaFinishBaseList);

            baseForm.LineFinishBaseList = project.LineFinishBaseList;

            if (FinishManagerGlobals.LineFinishManagerList != null)
            {
                FinishManagerGlobals.LineFinishManagerList.Dispose();
            }

            FinishManagerGlobals.LineFinishManagerList = new LineFinishManagerList(baseForm.CanvasManager, window, page, baseForm.LineFinishBaseList);

            baseForm.SeamFinishBaseList = project.SeamFinishBaseList;

            if (baseForm.SeamFinishManagerList != null)
            {
                baseForm.SeamFinishManagerList.Dispose();
            }

            if (project.ZeroLineBase != null)
            {
                baseForm.ZeroLineBase = project.ZeroLineBase.Clone();
            }

            baseForm.SeamFinishManagerList = new SeamFinishManagerList(baseForm.CanvasManager, window, page, baseForm.SeamFinishBaseList);

            baseForm.CanvasManager.CountersList = project.CountersList;

            baseForm.CanvasManager.LabelManager.SetlabelList(project.LabelList);

            //baseForm.AreaFilters.AreaFinishList = FinishGlobals.AreaFinishBaseList;

            updateStatus("Constructing palettes.");

            setupSeamFinishPalette(project.SeamFinishBaseList, project.AreaFinishBaseList); updateProgress(1);

            setupLineFinishPalette(project.LineFinishBaseList); updateProgress(1);

            setupAreaFinishPalette(FinishGlobals.AreaFinishBaseList); updateProgress(1);

            setupFieldGuides(project.fieldGuideController); updateProgress(1);

            baseForm.tbcPageAreaLine.Show();

            CanvasDirectedLineDict = setupDirectedLines(project.DirectedLineList);

            CanvasLayoutAreaDict = setupLayoutAreas(project.LayoutAreaList);

            buildDisplayCanvasSeamList();

            GraphicsCounterList = setupCounters(project.GraphicCounterList);

            DoorTakeoutList = setupTakeouts(project.TakeoutList);

            linkImportedElements();

            updateStatus("Adding lines to canvas.");

            addLinesToPalette();

            updateStatus("Adding areas to canvas.");

            addAreasToPalette(); updateProgress(1);

            updateStatus("Adding counters and takeouts to canvas.");

            addCountersToPalette(); updateProgress(1);

            addTakeoutsToPalette(); updateProgress(1);

            addVirtualOversUndrsToAreaElements(project.VirtualOverageList, project.VirtualUndrageList);

            canvasManager.SetMeasuringStickState(project.Page.ScaleHasBeenSet);

            canvasManager.ScaleRuleController.CancelScaleLine();

            canvasManager.TapeMeasureController.CancelTapeMeasure();

            SystemGlobals.ShowAreaLegendInAreaMode = project.ShowAreaLegendInAreaMode;

            SystemGlobals.ShowAreaLegendInLineMode = project.ShowAreaLegendInLineMode;

            SystemGlobals.ShowLineLegendInAreaMode = project.ShowLineLegendInAreaMode;

            SystemGlobals.ShowLineLegendInLineMode = project.ShowAreaLegendInLineMode;

            if (project.NormalSeamDisplayStatus == "ShowAll")
            {
                baseForm.RbnSeamModeAutoSeamsShowAll.Checked = true;
            }

            else
            {
                baseForm.RbnSeamModeAutoSeamsShowUnHideable.Checked = true;
            }

            if (project.ManaulSeamDisplayStatus == "ShowAll")
            {
                baseForm.RbnSeamModeManualSeamsShowAll.Checked = true;
            }

            else if (project.ManaulSeamDisplayStatus == "HideAll")
            {
                baseForm.RbnSeamModeManualSeamsHideAll.Checked = true;
            }

            else
            {
                baseForm.RbnSeamModeManualSeamsShowAll.Checked = true; // Default. Should not be needed but may be an issue because unhideable manual seams were removed along with the associated radio button.
            }

            if (canvasManager.selectedFinishType.MaterialsType == MaterialsType.Rolls)
            {
                baseForm.EnableOversUndersButton(true);
            }

            if (Utilities.IsNotNull(project.Options))
            {
                project.Options.DeSerialize(baseForm);
            }

            baseForm.nudFixedWidthFeet.Value = project.FixedWidthFeet;

            baseForm.nudFixedWidthInches.Value = project.FixedWidthInch;

            baseForm.Notes = project.Notes;

            baseForm.BtnAreaDesignState_Click(null, null);

            SystemState.DesignState = DesignState.Area;

            canvasManager.LegendController.Init(FinishGlobals.AreaFinishBaseList, FinishGlobals.LineFinishBaseList, canvasManager.CountersList);

            Coordinate areaModeLegendLocation = Coordinate.NullCoordinate;

            updateStatus("Setting up area legend.");

            if (project.AreaModeLegend != null)
            {
                

                areaModeLegendLocation = project.AreaModeLegend.Location.Deserialize();

                double areaModeSize = project.AreaModeLegend.Size;

                LegendLocation legandLocation = project.AreaModeLegend.LegendShowLocation;

                bool visible = project.AreaModeLegend.Visible;

                bool locateToClick = project.AreaModeLegend.LocateToClick;

                canvasManager.LegendController.AreaModeLegendNavigationForm.LocateToClick = locateToClick;


                if (areaModeLegendLocation != Coordinate.NullCoordinate)
                {
                    canvasManager.LegendController.AreaModeLegend.SetUp(areaModeLegendLocation.X, areaModeLegendLocation.Y, legandLocation, areaModeSize, visible);

                    canvasManager.LegendController.AreaModeLegend.ShowLegend(visible);
                }

                
                //canvasManager.LegendController.SetupFormForDesignState();
            }

            Coordinate lineModeLegendLocation = Coordinate.NullCoordinate;

            updateStatus("Setting up line legend.");

            if (project.LineModeLegend != null)
            {
                
                lineModeLegendLocation = project.LineModeLegend.Location.Deserialize();

                double lineModeSize = project.LineModeLegend.Size;

                bool locateToClick = project.LineModeLegend.LocateToClick;

                canvasManager.LegendController.LineModeLegendNavigationForm.LocateToClick = locateToClick;

                bool visible = project.LineModeLegend.Visible;

                if (lineModeLegendLocation != Coordinate.NullCoordinate)
                {
                    canvasManager.LegendController.LineModeLegend.ShowLegend(false);
                    canvasManager.LegendController.LineModeLegend.Visible = visible;
                    
                }
               // canvasManager.LegendController.SetupFormForDesignState();
            }

            canvasManager.LegendController.SetAreaModeLegendLayerVisibility(project.ShowAreaLegendInAreaMode);
            canvasManager.LegendController.SetLineModeLegendLayerVisibility(project.ShowLineLegendInAreaMode);

            //    canvasManager.LabelManager = new LabelManager(baseForm.AreaFinishManagerList, baseForm, canvasManager.Window, canvasManager.Page, baseForm.btnShowLabelEditor);

            PalettesGlobal.AreaFinishPalette.SetFiltered();
            PalettesGlobal.LineFinishPalette.SetFiltered();

            baseForm.BtnFilterAreas.Checked = FinishManagerGlobals.AreaFinishManagerList.ElementsFiltered();
            baseForm.BtnFilterLines.Checked = FinishManagerGlobals.LineFinishManagerList.ElementsFiltered();

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    if (canvasLayoutArea.FinishesLibElements == null)
                    {

                        FinishesLibElements finishesLibElements = new FinishesLibElements(
                            areaFinishManager.AreaFinishBase
                            , null
                            , null
                            , areaFinishManager.AreaFinishLayers
                            , null
                            , null);

                        canvasLayoutArea.FinishesLibElements = finishesLibElements;
                    }
                }

                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    GraphicsLayoutArea graphicsLayoutArea = (GraphicsLayoutArea)canvasLayoutArea;

                    graphicsLayoutArea.ParentArea = canvasLayoutArea;

                    foreach (GraphicsRollout graphicsRollout in graphicsLayoutArea.GraphicsRolloutList)
                    {
                        graphicsRollout.ParentGraphicsLayoutArea = graphicsLayoutArea;

                        foreach (GraphicsCut graphicsCut in graphicsLayoutArea.GraphicsCutList)
                        {
                            graphicsCut.ParentGraphicsRollout = graphicsRollout;

                            foreach (GraphicsOverage graphicsOverage in graphicsCut.GraphicsOverageList)
                            {
                                graphicsOverage.ParentGraphicsCut = graphicsCut;
                            }
                        }

                        foreach (GraphicsUndrage graphicsUndrage in graphicsRollout.GraphicsUndrageList)
                        {
                            graphicsUndrage.ParentGraphicsRollout = graphicsRollout;
                        }
                    }
                }
            }

            completeProgress();

            if (progressReportForm != null)
            {
                progressReportForm.Close();
            }

            foreach (GraphicShape graphicShape in page.PageShapeDictValues)
            {

            }
            //addUndragesToUndrageIndex(project);
            SystemGlobals.paletteSource = "Project";

            return projectFileName;
        }

        private void updateProgress(double stepWeight)
        {
            if (progressReportForm == null)
            {
                return;
            }

            progressReportForm.UpdateImportProgress(stepWeight);
        }

        private void updateStatus(string stepName)
        {
            if (progressReportForm == null)
            {
                return;
            }

            progressReportForm.UpdateImportStatus(stepName);

            //if (this.ImportStatusEvent != null)
            //{
            //    this.ImportStatusEvent.Invoke(stepName);
            //}
        }

        private void completeProgress()
        {
            if (progressReportForm == null)
            {
                return;
            }

            progressReportForm.CompleteProgress();
        }

        private void addUndragesToUndrageIndex(ProjectSerializable project)
        {
            bool duplicateWarning = false;

            foreach (CanvasLayoutArea layoutArea in CanvasLayoutAreaDict.Values)
            {
                foreach (var rollout in layoutArea.RolloutList)
                {
                    foreach (var undrage in  rollout.GraphicsUndrageList)
                    {
                        Undrage.AddIndex(undrage.UndrageIndex);
                    }
                }
            }
        }

        private void buildDisplayCanvasSeamList()
        {
            currentPage.DisplaySeamDict = new Dictionary<string, CanvasSeam>();

            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreaDict.Values)
            {
                foreach (CanvasSeam canvasSeam in canvasLayoutArea.DisplayCanvasSeamList)
                {
                    currentPage.DisplaySeamDict.Add(canvasSeam.GraphicsSeam.Guid, canvasSeam);
                }
            }
        }

        private void addVirtualOversUndrsToAreaElements(
            List<VirtualOverageSerializable> virtualOverageList
            , List<VirtualUndrageSerializable> virtualUndrageList)
        {
            foreach (VirtualOverageSerializable virtualOverSerializable in virtualOverageList)
            {
                string guid = virtualOverSerializable.AreaFinishGuid;

                VirtualOverage virtualOverage = virtualOverSerializable.Deserialize();

                AreaFinishManager areaFinishManager = FinishManagerGlobals.AreaFinishManagerList[guid];

                areaFinishManager.VirtualOverageList.Add(virtualOverage);
            }

            foreach (VirtualUndrageSerializable virtualUndrSerializable in virtualUndrageList)
            {
                string guid = virtualUndrSerializable.AreaFinishGuid;

                VirtualUndrage virtualUndrage = virtualUndrSerializable.Deserialize();

                AreaFinishManager areaFinishManager = FinishManagerGlobals.AreaFinishManagerList[guid];

                areaFinishManager.VirtualUndrageList.Add(virtualUndrage);
            }
        }

        private void addLinesToPalette()
        {
            double stepWeightIncrement = 10.0 / (double)CanvasDirectedLineDict.Count;

            foreach (CanvasDirectedLine canvasDirectedLine in CanvasDirectedLineDict.Values)
            {
                updateProgress(stepWeightIncrement);

                currentPage.AddToDirectedLineDict(canvasDirectedLine);

                CanvasLayoutArea layoutArea = canvasDirectedLine.ParentLayoutArea;

                updateProgress(stepWeightIncrement);

                GuidMaintenance.AddGuid(canvasDirectedLine.Guid, canvasDirectedLine);

                LineFinishManager lineFinishManager = canvasDirectedLine.LineFinishManager;

                updateProgress(stepWeightIncrement);

                canvasDirectedLine.Draw();

                // Set shape data for tracking.

                updateProgress(stepWeightIncrement);

                canvasDirectedLine.SetShapeData();

                updateProgress(stepWeightIncrement);

                lineFinishManager.AddLineFull(canvasDirectedLine);
            }
        }

        private void addAreasToPalette()
        {
            //currentPage.UsedSeamAreaIndices.Clear();

            double stepWeightIncrement = 20.0 / (double)CanvasLayoutAreaDict.Count;

            foreach (CanvasLayoutArea layoutArea in CanvasLayoutAreaDict.Values)
            {
                updateProgress(stepWeightIncrement);



                currentPage.AddLayoutArea(layoutArea);

                updateProgress(stepWeightIncrement);

                GuidMaintenance.AddGuid(layoutArea.Guid, layoutArea);

                // The last parameter stops the draw composite shape to redraw the perimeter lines. When the project is imported, the 
                // lines are drawn in the routine addLinesToPalette. Without this parameter the perimeter lines would be drawn twice.
                updateProgress(stepWeightIncrement);


                layoutArea.DrawCompositeShape(window, page, true);


                updateProgress(stepWeightIncrement);
                // In the following, There are three cases to consider:
                // 1. The layout area is a top level layout area
                // 2. The layout area is a leaf level layout area
                // 3. The layout area is an internal layout area

                //UCAreaFinishPaletteElement ucAreaFinish = layoutArea.AreaFinishManager.UCAreaPaletteElement;
                
                updateProgress(stepWeightIncrement);
                
                AreaFinishManager areaFinishManager = layoutArea.AreaFinishManager;

                layoutArea.Shape.GraphicsLayer = areaFinishManager.AreaPerimeterLayer;

                if (layoutArea.ParentArea is null)
                {
                    // Top level.
                    areaFinishManager.AddNormalLayoutArea(layoutArea, false, true, false);

                    // layoutArea.Lock();

                    if (layoutArea.SeamIndexTag != null)
                    {
                        if (layoutArea.SeamIndexTag.Shape == null)
                        {
                            layoutArea.SeamIndexTag.Draw();
                        }

                        var seamAreaIndexLayer = layoutArea.AreaFinishManager.AreaFinishLayers.SeamAreaIndexLayer;
                        var seamIndexTag = layoutArea.SeamIndexTag;

                        seamAreaIndexLayer.AddShape(seamIndexTag.Shape, 1);
                        seamIndexTag.Shape.LayerSet.Add(seamAreaIndexLayer);

                        if (layoutArea.IsSeamed())
                        {
                            layoutArea.SeamIndexTag.UnderlineTag();
                        }
                    }
                    continue;
                }

                if (layoutArea.ParentArea != null && layoutArea.OffspringAreas.Count <= 0)
                {
                    // This is a leaf layout area. Add only to seam level.

                    areaFinishManager.AddNormalLayoutAreaSeamStateOnly(layoutArea, true);

                    // layoutArea.Lock();

                    continue;
                }

                updateProgress(stepWeightIncrement);

            }

            // FinishManagerGlobals.AreaFinishManagerList.UpdateFinishStats();

            updateProgress(stepWeightIncrement);

            window?.DeselectAll();

            //baseForm.CanvasManager.VsoWindow.DeselectAll();
        }

        private void addCountersToPalette()
        {
            canvasManager.CounterController.AddCountersToPalette(GraphicsCounterList);

        }

        private void addTakeoutsToPalette()
        {
            GraphicsLayerBase layer = currentPage.TakeoutLayer;

            foreach (DoorTakeout graphicsTakeout in DoorTakeoutList)
            {
                GuidMaintenance.AddGuid(graphicsTakeout.Guid, graphicsTakeout);

                LineFinishManager lineFinishManager = FinishManagerGlobals.LineFinishManagerList[graphicsTakeout.LineFinishBaseGuid];

                LineFinishBase lineFinishBase = lineFinishManager.LineFinishBase;

                graphicsTakeout.Draw(lineFinishBase.LineColor, 10, lineFinishBase.VisioLineType);

                graphicsTakeout.Shape.SetShapeData("[Takeout]", "Circle", graphicsTakeout.Guid);

                window?.DeselectAll();

                lineFinishManager.AddToGraphicsTakeoutAreaDict(graphicsTakeout);

                //currentPage.GraphicsTakeoutAreaDict.Add(graphicsTakeout.Guid, graphicsTakeout);
                currentPage.AddToPageShapeDict(graphicsTakeout);

                layer.AddShape(graphicsTakeout.Shape, 1);
            }

            FinishManagerGlobals.LineFinishManagerList.UpdateFinishStats();
        }

        private void linkImportedElements()
        {
            foreach (CanvasLayoutArea layoutArea in CanvasLayoutAreaDict.Values)
            {
                if (layoutArea.ParentArea != null)
                {
                    layoutArea.ParentArea = CanvasLayoutAreaDict[layoutArea.ParentAreaGuid];
                }

                if (!(layoutArea.OffspringAreaGuidList is null))
                {
                    layoutArea.OffspringAreaGuidList.ForEach(g => layoutArea.AddToOffspringList(CanvasLayoutAreaDict[g]));
                }
            }

            foreach (GraphicsUndrage graphicsUndrage in GraphicsUndrageList)
            {
                graphicsUndrage.ParentGraphicsRollout = GraphicsRolloutDict[graphicsUndrage.GraphicsRolloutGuid];
            }

            foreach (CanvasDirectedLine directedLine in CanvasDirectedLineDict.Values)
            {
                directedLine.ParentPolygon = null;

                if (linePolygonDict.ContainsKey(directedLine.Guid))
                {
                    string parentPolygonGuid = linePolygonDict[directedLine.Guid];

                    if (!string.IsNullOrEmpty(parentPolygonGuid))
                    {
                        if (this.CanvasDirectedPolygonDict.ContainsKey(parentPolygonGuid))
                        {
                            directedLine.ParentPolygon = this.CanvasDirectedPolygonDict[parentPolygonGuid];
                        }
                    }

                }

                if (lineAssociatedLineDict.ContainsKey(directedLine.Guid))
                {
                    string associatedLineGuid = lineAssociatedLineDict[directedLine.Guid];

                    if (!string.IsNullOrEmpty(associatedLineGuid))
                    {
                        if (this.CanvasDirectedLineDict.ContainsKey(associatedLineGuid))
                        {
                            var associatedLine = this.CanvasDirectedLineDict[associatedLineGuid];

                            associatedLine.AssociatedDirectedLine = directedLine;
                            directedLine.AssociatedDirectedLine = associatedLine;
                        }
                    }
                }

                //string parentLayoutAreaGuid = lineLayoutAreaDict[directedLine.Guid];
                //directedLine.ParentLayoutArea = CanvasLayoutAreaDict[parentLayoutAreaGuid];      
            }
        }

        private void setupCurrentPage(PageSerializable page)
        {
            currentPage.DrawingInBytes = page.DrawingInBytes;

            DrawingImporter drawingImporter = new DrawingImporter(this.window, this.currentPage);

            if (page.DrawingInBytes != null)
            {
                currentPage.Drawing = drawingImporter.ImportDrawing(page.DrawingInBytes);

                if (!(currentPage.Drawing is null))
                {
                    currentPage.Drawing.SetShapeData("Architectural Drawing", "Image");

                    //---------------------------------------------//
                    // The following is not necessary because this //
                    // logic is in the call to canvasmanager.Setup //
                    // Drawing                                     //
                    //---------------------------------------------//

                    currentPage.Drawing.ParentObject = currentPage.Drawing;

                    canvasManager.SetupDrawing(currentPage.Drawing, drawingImporter.drawingName);
                }
            }

            if (page.ScaleHasBeenSet)
            {
                currentPage.DrawingScaleInInches = page.DrawingScaleInInches;
            }

            else
            {
                currentPage.SetDefaultScale(page.DrawingScaleInInches);
            }

            currentPage.PageHeight = page.PageHeight;
            currentPage.PageWidth = page.PageWidth;

            currentPage.SetPageSizeToCurrentSize();
        }

        private void setupAreaFinishPalette(
            AreaFinishBaseList areaFinishBaseList)
        {
            areaFinishBaseList.ForEach(
                a => { if (string.IsNullOrEmpty(a.Guid)) a.Guid = GuidMaintenance.CreateGuid(a); else GuidMaintenance.AddGuid(a.Guid, a); });

            UCAreaFinishPalette areaPalette = PalettesGlobal.AreaFinishPalette;

            areaPalette.SuspendLayout();


            FinishGlobals.AreaFinishBaseList.ItemSelected += baseForm.AreaFinishBaseList_ItemSelected;

            areaPalette.Init(canvasManager.Window, canvasManager.Page);

            areaPalette.Select(areaFinishBaseList.SelectedItemIndex);

            areaPalette.SetLineFinish(PalettesGlobal.LineFinishPalette.SelectedItemIndex);

            areaPalette.ResumeLayout();
        }

        private void setupLineFinishPalette(LineFinishBaseList lineFinishBaseList)
        {
            lineFinishBaseList.ForEach(
                l => { if (string.IsNullOrEmpty(l.Guid)) l.Guid = GuidMaintenance.CreateGuid(l); else GuidMaintenance.AddGuid(l.Guid, l); });

            UCLineFinishPalette linePalette = baseForm.linePalette;

            linePalette.SuspendLayout();

            baseForm.LineFinishBaseList.ItemSelected += baseForm.LineFinishBaseList_ItemSelected;

            linePalette.Init(lineFinishBaseList);

            linePalette.Select(lineFinishBaseList.SelectedItemIndex);

            linePalette.ResumeLayout();
        }

        private void setupSeamFinishPalette(SeamFinishBaseList seamFinishBaseList, AreaFinishBaseList areaFinishBaseList)
        {
            seamFinishBaseList.ForEach(
               s => { if (string.IsNullOrEmpty(s.Guid)) s.Guid = GuidMaintenance.CreateGuid(s); else GuidMaintenance.AddGuid(s.Guid, s); });

            UCSeamFinishPalette seamPalette = baseForm.seamPalette;

            seamPalette.SuspendLayout();

            seamPalette.Reinit(areaFinishBaseList, seamFinishBaseList);

            // The following sets up the subscription in the area finish elements to the changes in the seams so that the area seams 
            // react immediately to changes in the seam format.

            seamFinishBaseList.ForEach(s => { seamFinishBaseList.SeamFinishByGuid.Add(s.Guid, s); seamFinishBaseList.SeamFinishByName.Add(s.SeamName, s); });

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    if (areaFinishBase.SeamFinishBase is null)
                    {
                        continue;
                    }

                    string seamGuid = areaFinishBase.SeamFinishBase.Guid;

                    if (baseForm.SeamFinishBaseList.SeamFinishByGuid.ContainsKey(seamGuid))
                    {
                        areaFinishBase.SeamFinishBase = baseForm.SeamFinishBaseList.SeamFinishByGuid[seamGuid];

                        continue;
                    }

                    string seamName = areaFinishBase.SeamFinishBase.SeamName;

                    if (baseForm.SeamFinishBaseList.SeamFinishByName.ContainsKey(seamName))
                    {
                        areaFinishBase.SeamFinishBase = baseForm.SeamFinishBaseList.SeamFinishByName[seamName];

                        continue;
                    }

                    throw new Exception("Unable to link seam finish base into area finish base.");
                }

                else
                {
                    areaFinishBase.SeamFinishBase = null;
                }
            }

            seamPalette.ResumeLayout();
        }

        private void setupFieldGuides(FieldGuideControllerSerializable fieldGuideControllerSerializable)
        {
            FieldGuideController fieldGuideContoller = baseForm.CanvasManager.FieldGuideController;

            fieldGuideContoller.Init(
                fieldGuideControllerSerializable.FieldGuideLineColor,
                fieldGuideControllerSerializable.FieldGuideLineOpacity,
                fieldGuideControllerSerializable.FieldGuideLineWidthInPts,
                fieldGuideControllerSerializable.FieldGuideLineStyle
                );

            foreach (FieldGuideSerializable fieldGuideSerializable in fieldGuideControllerSerializable.FieldGuideList)
            {
                fieldGuideContoller.AddFieldGuide(fieldGuideSerializable.X, fieldGuideSerializable.Y, fieldGuideSerializable.Guid);
            }

            if (fieldGuideControllerSerializable.FieldGuidesShowing)
            {
                baseForm.BtnShowFieldGuides_Click(null, null);
            }

            else
            {
                baseForm.BtnHideFieldGuides_Click(null, null);
            }
        }

        private Dictionary<string, string> linePolygonDict = new Dictionary<string, string>();

        private Dictionary<string, string> lineLayoutAreaDict = new Dictionary<string, string>();

        private Dictionary<string, string> lineAssociatedLineDict = new Dictionary<string, string>();

        private Dictionary<string, CanvasDirectedLine> setupDirectedLines(List<DirectedLineSerializable> lineList)
        {
            Dictionary<string, CanvasDirectedLine> rtrnDict = new Dictionary<string, CanvasDirectedLine>();

            foreach (DirectedLineSerializable line in lineList)
            {
                CanvasDirectedLine canvasDirectedLine = new CanvasDirectedLine(canvasManager, line);

                linePolygonDict.Add(line.Guid, line.ParentPolygonGuid);

                lineLayoutAreaDict.Add(line.Guid, line.ParentAreaGuid);

                if (line.AssociatedLineGuid != null)
                {
                    lineAssociatedLineDict.Add(line.Guid, line.AssociatedLineGuid);
                }

                rtrnDict.Add(canvasDirectedLine.Guid, canvasDirectedLine);
            }

            return rtrnDict;
        }

        private Dictionary<string, CanvasLayoutArea> setupLayoutAreas(List<LayoutAreaSerializable> layoutAreaList)
        {
            Dictionary<string, CanvasLayoutArea> rtrnDict = new Dictionary<string, CanvasLayoutArea>();

            foreach (LayoutAreaSerializable layoutAreaSerializable in layoutAreaList)
            {
                // CanvasLayoutArea canvasLayoutArea = deserializeLayoutArea(Window, Page, layoutAreaSerializable);

                CanvasLayoutArea canvasLayoutArea = layoutAreaSerializable.Deserialize(baseForm, this, window, page);

                canvasLayoutArea.Shape = new GraphicShape(canvasLayoutArea, window, page, null);

                canvasLayoutArea.Shape.SetShapeData("[CanvasLayoutArea]", "Complex Object", canvasLayoutArea.Guid);

                canvasLayoutArea.Shape.Guid = canvasLayoutArea.Guid;

                rtrnDict.Add(canvasLayoutArea.Guid, canvasLayoutArea);
            }
            return rtrnDict;
        }

        private List<GraphicsCounter> setupCounters(List<CounterSerializable> counterList)
        {
            List<GraphicsCounter> rtrnList = new List<GraphicsCounter>();

            if (counterList is null)
            {
                return rtrnList;
            }

            foreach (CounterSerializable counterSerializable in counterList)
            {
                GraphicsCounter counter = counterSerializable.Deserialize(this.window, this.page, canvasManager.CounterController);

                rtrnList.Add(counter);
            }

            return rtrnList;
        }


        private List<DoorTakeout> setupTakeouts(List<TakeoutSerializable> takeoutList)
        {
            List<DoorTakeout> rtrnList = new List<DoorTakeout>();

            if (takeoutList is null)
            {
                return rtrnList;
            }

            foreach (TakeoutSerializable takeoutSerializable in takeoutList)
            {
                DoorTakeout takeout = takeoutSerializable.Deserialize(page);

                rtrnList.Add(takeout);
            }

            return rtrnList;
        }
    }
}
