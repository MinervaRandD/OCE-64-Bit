//-------------------------------------------------------------------------------//
// <copyright file="CanvasManager.cs" company="Bruun Estimating, LLC">           // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.CanvasManager
{
    using AxMicrosoft.Office.Interop.VisOcx;
    using CanvasLib.Borders;
    using CanvasLib.Counters;
    using CanvasLib.LockIcon;
    using CanvasLib.Legend;
    using CanvasLib.Markers_and_Guides;
    using CanvasLib.MeasuringStick;
    using CanvasLib.Pan_And_Zoom;
    using CanvasLib.Scale_Line;
    using CanvasLib.Snap_To_Grid;
    using CanvasLib.Tape_Measure;
    using FinishesLib;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Supporting_Forms;
    using Geometry;
    using Globals;
    using Graphics;
    using Graphics;
    using MaterialsLayout;
    using PaletteLib;
    using SettingsLib;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using TracerLib;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;

    /// <summary>
    /// Canvas manager implements all functionality related to the visio graphics engine
    /// </summary>
    public partial class CanvasManager
    {
        public PanAndZoomController PanAndZoomController;

        public SnapToGridController SnapToGridController { get; set; }

        public ScaleRuleController ScaleRuleController { get; set; }

        public TapeMeasureController TapeMeasureController { get; set; }

        //public MeasuringStickController MeasuringStickController { get; set; }

        public CounterController CounterController { get; set; }

        public LegendController LegendController { get; set; }

        public LockIcon LockIcon { get; set; }

        public LabelManager LabelManager { get; set; }

        public FieldGuideController FieldGuideController
        {
            get
            {
                return FieldGuideGlobals.FieldGuideController;
            }

            set
            {
                FieldGuideGlobals.FieldGuideController = value;
            }
        }

        public CounterList CountersList
        {
            get { return CounterController.CounterList; }
            set { CounterController.UpdateCountersList(value); }
        }

        private const double clickResolution = 0.05;

        public const double CompletedShapeLineWidthInPts = 0.25;

        //public const string ZeroLineStyleFormula = "9";

        public const string AreaModePerimeterLineStyleFormula = "1";

        public static Color AreaModePerimeterLineColor = Color.Gray;

        public delegate void AreaSelectionChangedEventHandler(object sender, List<CanvasLayoutArea> e);

        public event AreaSelectionChangedEventHandler AreaSelectionChangedEvent;

        /// <summary>
        /// The current visio Page. This changes when the user selects a different Page
        /// </summary>
        //public Visio.Page CurrentPage { get; set; }

        public CanvasPage CurrentPage { get; set; }

        // Bad design, but as a quick and dirty...

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page => CurrentPage;

        //public VisioTestAndDebug Vtb { get; set; }

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Application VsoApplication { get; set; }

        public Visio.ApplicationSettings VsoAppSettings { get; set; }
        public double GridScale { get; set; } = 12.0;

        private bool showGrid = false;

        public ExtendedCrosshairs ExtendedCrosshairs { get; set; }

        public BorderManager BorderManager { get; set; } = null;

        // public Dictionary<string, CanvasDirectedLine> GuidDirectedLineDict => CurrentPage.DirectedLineDict;

        public bool ShowGrid
        {
            get
            {
                return showGrid;
            }

            set
            {
                showGrid = value;

                VsoWindow.ShowGrid = (short) (showGrid ? 1 : 0);
            }
        }

        private bool showRulers = false;

        public bool ShowRulers
        {
            get
            {
                return showRulers;
            }

            set
            {
                showRulers = value;

                VsoWindow.ShowRulers = (short)(showRulers ? 1 : 0);
            }
        }

        private bool showPanAndZoom = false;

        public bool ShowPanAndZoom
        {
            get
            {
                return showPanAndZoom;
            }

            set
            {
                showPanAndZoom = value;

                VisioInterop.ShowPanAndZoomBox(Window, value);
            }
        }

        public double GridOffset
        {
            get
            {
                return GlobalSettings.GridlineOffsetSetting;
            }
        } 

        //public DrawingMode DrawingMode
        //{
        //    get
        //    {
        //        return BaseForm.DrawingMode;
        //    }

        //    set
        //    {
        //        BaseForm.DrawingMode = value;
        //    }
        //}

        //public FloorMaterialEstimatorBaseForm BaseForm;

        private AxDrawingControl axDrawingControl;

        public UCAreaFinishPalette AreaFinishPalette => PalettesGlobal.AreaFinishPalette;

        public UCLineFinishPalette LineFinishPalette => PalettesGlobal.LineFinishPalette;

        public UCAreaFinishPaletteElement selectedFinishType => AreaFinishPalette.SelectedFinish;

        public UCLineFinishPaletteElement SelectedLineType => LineFinishPalette.SelectedLine;

        public AreaFinishManagerList AreaFinishManagerList => FinishManagerGlobals.AreaFinishManagerList;

        public LineFinishManagerList LineFinishManagerList => FinishManagerGlobals.LineFinishManagerList;

        public SeamFinishManagerList SeamFinishManagerList => FinishManagerGlobals.SeamFinishManagerList;

        public AreaFinishManager SelectedAreaFinishManager => FinishManagerGlobals.SelectedAreaFinishManager;

        public LineFinishManager SelectedLineFinishManager => FinishManagerGlobals.SelectedLineFinishManager;

        public SeamFinishBaseList FinishSeamBaseList => FinishGlobals.SeamFinishBaseList;
        
        //private SortedDictionary<int, AreaShape> areaShapeDict = new SortedDictionary<int, AreaShape>();

        private MeasuringStick _measuringStick = null;

        public MeasuringStick MeasuringStick
        {
            get
            {
                if (_measuringStick is null)
                {
                    _measuringStick = new MeasuringStick(Window, Page);
                }

                return _measuringStick;
            }
        }

        public FloorMaterialEstimatorBaseForm BaseForm { get; set; }

        public CanvasManager(FloorMaterialEstimatorBaseForm baseForm, AxDrawingControl axDrawingControl)
        {
            this.BaseForm = baseForm;
            this.axDrawingControl = axDrawingControl;

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;
            VsoApplication = VsoDocument.Application;

            VsoAppSettings = VsoApplication.Settings;

            //VsoAppSettings.SetRasterExportResolution(Visio.VisRasterExportResolution.visRasterUsePrinterResolution, 100, 100, 0);
       
            VsoApplication.BuiltInToolbars[0].UpdateUI();

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Array x;

            this.VsoDocument.GetThemeNames(Visio.VisThemeTypes.visThemeTypeColor, out x);

            Visio.Pages pages = this.VsoDocument.Pages;

            Window = new GraphicsWindow(this.VsoWindow);

            this.CurrentPage = new CanvasPage(this, Window, VsoDocument, pages[1]);

            this.CurrentPage.Name = "Drawing-1";

            VsoApplication.EventsEnabled = -1; // This shouldn't be necessary

            VsoWindow.ShowGrid = 1;
            VsoWindow.ShowRulers = 0;
            VsoWindow.ShowScrollBars = (short)Visio.VisScrollbarStates.visScrollBarBoth;
            VsoWindow.ShowPageBreaks = 0;
            VsoWindow.ShowPageTabs = false;

            VsoDocument.SnapEnabled = true;
            VsoDocument.SnapSettings = ((Visio.VisSnapSettings)VsoDocument.SnapSettings) | (Visio.VisSnapSettings)2;

            // The following is necessary so that shapes can be locked down from selection.
            VsoDocument.Protection = Visio.VisProtection.visProtectShapes;

            this.VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.VsoWindow.MouseUp += VsoWindow_MouseUp;
            this.VsoWindow.MouseMove += VsoWindow_MouseMove;
            
            VsoWindow.ZoomBehavior = Visio.VisZoomBehavior.visZoomNone;

            //this.VsoWindow.KeyUp += VsoWindow_KeyUp;

            VsoWindow.Page = pages[1];

            this.VsoWindow.ShowRulers = 0;

            this.VsoWindow.SelectionChanged += VsoWindow_SelectionChanged;

            this.CurrentPage.PageHeight = GlobalSettings.DefaultNewDrawingHeightInInches;
            this.CurrentPage.PageWidth = GlobalSettings.DefaultNewDrawingWidthInInches;

            this.CurrentPage.CreateCutsOversUndrsLayers();
            
            VisioInterop.Init(VsoApplication, this.Window, this.CurrentPage);
           // VisioGeometryEngine.Init(this.Window, this.CurrentPage);

            this.CurrentPage.SetPageSizeToCurrentSize();

            this.VsoWindow.ViewFit = 1;
           
            VisioInterop.SetPageGrid1(CurrentPage, GlobalSettings.GridSpacingInInches, GlobalSettings.GridlineOffsetSetting);

            VisioInterop.SetSizeBoxWindowVisibility(Window, false);

            SystemState.DrawingShape = false;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;

            PanAndZoomController = new PanAndZoomController(
                Window
                , Page
                ,this.Page.PageWidth
                ,this.Page.PageHeight
                , SystemState.BtnZoomIn
                , SystemState.BtnZoomOut
                , SystemState.DdbZoomPercent
                , SystemState.BtnFitCanvas);

            // PanAndZoomController.LockOnUnderSize = GlobalSettings.LockScrollWhenDrawingSmallerThanCanvas;

            #region Field guide setup

            FieldGuideController = new FieldGuideController(this.axDrawingControl, Window, Page, SystemGlobals.BaseForm.BtnShowFieldGuides);

            FieldGuideController.Init(GlobalSettings.FieldGuideColor, GlobalSettings.FieldGuideOpacity, GlobalSettings.FieldGuideWidthInPts, GlobalSettings.FieldGuideStyle);

            #endregion

            // Need to figure out a better way to do this...


            SnapToGridController = new SnapToGridController(this.axDrawingControl, this.VsoWindow, FieldGuideController);

            ScaleRuleController = new ScaleRuleController(Window, Page, ClearAllSeams, ReseamAllAreas, RaiseAreaSelectionChangedEvent);

            ScaleRuleController.SetScaleCompleted += ScaleRuleController_SetScaleCompleted;

            TapeMeasureController = new TapeMeasureController(Window, Page);

            TapeMeasureController.GetScaleCompleted += TapeMeasureController_GetScaleCompleted;

            string countersFilePath = string.Empty;

            if (Program.AppConfig.ContainsKey("countersfilepath"))
            {
                countersFilePath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["countersfilepath"]);
            }

            BorderManager = new BorderManager(Window, Page);

            CounterController = new CounterController(
                BaseForm
                , Window
                , Page
                , (CounterControl) SystemState.CccAreaMode
                , (CounterControl) SystemState.CccLineMode
                , SystemState.BtnCounters
                , countersFilePath);

            CounterController.DebugUpdateRequired += CounterController_DebugUpdateRequired;

            LegendController = new LegendController(baseForm, Window, Page, SystemState.BtnShowLegendForm, CounterController);

            LockIcon = new LockIcon(Window, Page);

            axDrawingControl.WindowActivated += AxDrawingControl_WindowActivated;
            //ActivateFirstPage();


            GraphicsLayer[] layers = new GraphicsLayer[] { /* this.CurrentPage.CutsIndexLayer */ }; // not sure how to handle this (Marc diamond)

            SystemGlobals.UpdateAreaSeamsUndrsOversDataDisplay = UpdateAreaSeamsUndrsOversDataDisplay;

            SystemGlobals.CompletePolylineDraw = CompletePolylineDraw;

            this.CurrentPage.VisioPage.ShapeAdded += VisioPage_ShapeAdded;

            SystemState.DrawingShapeChanged += SystemState_DrawingShapeChanged;
        }

        private void AxDrawingControl_WindowActivated(object sender, EVisOcx_WindowActivatedEvent e)
        {
            ActivateFirstPage();
        }

        private void ActivateFirstPage()
        {
            if (BaseForm.InvokeRequired) { BaseForm.Invoke((Action)ActivateFirstPage); return; }

            // Ensure the control is actually ready
            if (!axDrawingControl.IsHandleCreated || axDrawingControl.Document == null)
                return; // call again later (e.g., from Shown/DocumentOpened)

            var doc = axDrawingControl.Document;
            if (doc.Pages.Count == 0) return;

            // Prefer a foreground (non-background) page
            Visio.Page firstPage = null;
            foreach (Visio.Page p in doc.Pages)
                if (p.Background == 0) { firstPage = p; break; }
            firstPage = doc.Pages[1];

            // Use the control's drawing window, not Application.ActiveWindow
            var win = axDrawingControl.Window;

            // Give the control focus first; this often avoids the COM “invalid operation”
            axDrawingControl.Focus();

            // Now set the page and activate
            if (!ReferenceEquals(win.Page, firstPage))
                win.Page = firstPage;    // <-- this is the safe place to set Page

            win.Activate();
        }


        private void VsoApplication_SelectionChanged(Visio.Window Window)
        {
            throw new NotImplementedException();
        }

        private void SystemState_DrawingShapeChanged(bool drawingShape)
        {

            if (drawingShape == false)
            {
                CurrentPage.LastPointDrawn = Coordinate.NullCoordinate;
                SystemState.TssDrawoutLength.Text = string.Empty;
            }
#if DEBUG
            SystemState.TlsDrawingShape.Text = "Drawing shape: " + drawingShape.ToString();
#endif
        }

        //private void TxbRemnantWidthInches_TextChanged(object sender, EventArgs e)
        //{
        //    Utilities.SetTextFormatForValidInt(BaseForm.txbRemnantWidthInches);
        //}

        //private void TxbRemnantWidthFeet_TextChanged(object sender, EventArgs e)
        //{
        //    Utilities.SetTextFormatForValidInt(BaseForm.txbRemnantWidthFeet);
        //}

        private void CounterController_DebugUpdateRequired()
        {

        }

        private void VisioPage_ShapeAdded(Visio.Shape Shape)
        {
//#if DEBUG
//            BaseForm.UpdateDebugForm();
//#endif
        }

        public DrawingMode PrevDrawingMode { get; set; }

        public bool PrevDrawingShape { get; set; }

        private void ScaleRuleController_SetScaleCompleted(bool cancelled)
        {
            SystemState.BtnSetCustomScale.Checked = false;
            
            SystemState.DrawingShape = PrevDrawingShape;

            SystemState.DrawingMode = PrevDrawingMode;

            if (cancelled)
            {
                return;
            }

            FinishManagerGlobals.AreaFinishManagerList.UpdateFinishStats();
            FinishManagerGlobals.LineFinishManagerList.UpdateFinishStats();
           // BaseForm.SeamFinishManagerList.UpdateFinishStats();

            double widthInLocalInches = FixedWidthScaleInLocalInches();

            bool updateRequired = FinishManagerGlobals.AreaFinishManagerList.CanvasAreasAreSeamed();

            if (updateRequired)
            {
                
                //baseAreaFinishPalette.SystemState.BtnRedoSeamsAndCuts.Checked = true;
                SystemState.BtnRedoSeamsAndCuts.Image = SystemGlobals.RedoSeamsOnImage;
            }

            BorderManager.ResetWidth(widthInLocalInches);

            MeasuringStick.UpdateScale();

            SetMeasuringStickState(true);
            UpdateSetScaleTooltip(true);

            //if (Utilities.IsNotNull(BaseForm.SummaryReportForm))
            //{
            //   // BaseForm.SummaryReportForm.ReSetupReportRows();
            //}
        }

        public double FixedWidthScaleInInches()
        {
            return (12.0 * (double)SystemState.NudFixedWidthFeet.Value) + (double)SystemState.NudFixedWidthInches.Value;
        }

        public double FixedWidthScaleInLocalInches()
        {
            return FixedWidthScaleInInches() / Page.DrawingScaleInInches;
        }

        public void SetMeasuringStickState(bool enabled)
        {
            SystemState.BtnMeasuringStick.Enabled = enabled;
        }

        public void UpdateSetScaleTooltip(bool scaleSet)
        {
            string toolTipText = scaleSet ? String.Format(Properties.Resources.BF_SETSCALE_TIP_SET, Page.DrawingScaleInInches) : Properties.Resources.BF_SETSCALE_TIP_NOT_SET;
            SystemState.BtnSetCustomScale.ToolTipText = toolTipText;
        }

        private void TapeMeasureController_GetScaleCompleted()
        {
            //SystemState.DrawingShape = false;
            SystemState.DrawingMode = DrawingMode.Default;

            SystemState.BtnSetCustomScale.Checked = false;
        }
        private void SystemState_DesignStateChanged(DesignState prevDesignState, DesignState currDesignState)
        {
           
            if (prevDesignState == currDesignState)
            {
                return;
            }

            ExitingDesignState(prevDesignState);
            EnteringDesignState(currDesignState);
        }

        internal void ExitingDesignState(DesignState prevDesignState)
        {
            if (prevDesignState == DesignState.Line)
            {
                LineHistoryList.Clear();
                LineModeBuildingLine = null;

                CurrentPage.RemoveLineModeStartMarker();

            }
        }

        internal void EnteringDesignState(DesignState currDesignState, AreaShapeBuildStatus buildStatus = AreaShapeBuildStatus.Completed)
        {
            foreach (CanvasLayoutArea layoutArea in CurrentPage.LayoutAreas)
            {
                layoutArea.SetLineGraphics(currDesignState, AreaShapeBuildStatus.Completed);
            }

            if (currDesignState == DesignState.Line)
            {
                GraphicsLayerBase layer = Page.TakeoutLayer;

                if (layer != null)
                {
                    if (SystemState.BtnDoorTakeoutShow.BackColor == Color.Orange)
                    {
                        layer.SetLayerVisibility(false);
                    }

                    else
                    {
                        layer.SetLayerVisibility(true);
                    }
                }

                return;
            }

            if (currDesignState == DesignState.Area)
            {
                foreach (LineFinishManager lineFinishManager in FinishManagerGlobals.LineFinishManagerList)
                {
                    lineFinishManager.SetLayerVisibility(DesignState.Area);
                }


                //this.SetZeroLineActive();

            }

            else
            {
                GraphicsLayerBase layer = Page.TakeoutLayer;

                if (layer != null)
                {
                    layer.SetLayerVisibility(false);
                }
            }
        }

        private void VsoWindow_SelectionChanged(Visio.Window Window)
        {
            
        }

        /// <summary>
        /// Load the base drawing.
        /// </summary>
        public bool LoadDrawing(string drawingFileFullPath, bool eraseCurrentContents = true)
        {
            DrawingImporter drawingImporter = new DrawingImporter(this.Window, this.CurrentPage);

            string drawingFilePath = string.Empty;

            GraphicShape drawing = drawingImporter.ImportDrawing(drawingFileFullPath, eraseCurrentContents, VsoApplication);

            if (drawing is null)
            {
                return true;
            }

            drawing.ParentObject = drawing;

            SetupDrawing(drawing, drawingImporter.drawingName); 

            return false;
         }

        public void SetupDrawing(GraphicShape drawing, string drawingName)
        {
            ShapeSize size = VisioInterop.GetShapeDimensions(drawing);

            Page.AddToPageShapeDict(drawing);

            Page.Drawing = drawing;

            //GraphicsLayer drawingLayer = CurrentPage.DrawingLayer.GetBaseLayer();

            Page.DrawingLayer.AddShape(drawing, 1);

            Page.DrawingLayer.SetLayerVisibility(true);

            VisioInterop.SendToBack(drawing);

            CurrentPage.DrawingLayer.Lock();

            CurrentPage.DrawingLayer.SetLayerOpacity(0);

            VisioInterop.SetPageSize(this.CurrentPage, size);

            PanAndZoomController.SetPageSize(size.Width, size.Height);

            this.CurrentPage.PageWidth = size.Width;
            this.CurrentPage.PageHeight = size.Height;

            VisioInterop.SetPageGrid1(this.CurrentPage, GlobalSettings.GridSpacingInInches, 0.1);

            VisioInterop.SetShapeLocation(drawing);

            //VisioInterop.ResizeToFitContents(this.CurrentPage);

            //VisioInterop.SetWindowRect(Window, -0.25, size.Height + 0.25, size.Width + 0.5, size.Height + 0.5);

           // SetZoom(1.25, true);
            //VisioInterop.SetPageScale(Page, 1.25);

            VsoWindow.ShowPageBreaks = 0;

            //DrawingShape = false;

            Window?.DeselectAll();

            CurrentPage.Name = drawingName;

            //VsoWindow.Zoom = -1;
        }

        public void SetDrawoutLength(double x, double y)
        {
            if (!CurrentPage.ScaleHasBeenSet)
            {
                SystemState.TssDrawoutLength.Text = "";
            }

            else if ((SystemState.DesignState == DesignState.Area || SystemState.DesignState == DesignState.Line) &&
               !Coordinate.IsNullCoordinate(CurrentPage.LastPointDrawn))
            {

                double drawnOutDistance = MathUtils.H2Distance(CurrentPage.LastPointDrawn.X, CurrentPage.LastPointDrawn.Y, x, y);

                drawnOutDistance *= CurrentPage.DrawingScaleInInches;

                string drawoutText = FormatUtils.FormatInchesToFeetAndInches(drawnOutDistance, 2);

                SystemState.TssDrawoutLength.Text = drawoutText.PadLeft(9);

                if (GlobalSettings.LineDrawoutModeSetting == LineDrawoutMode.ShowNormalDrawout)
                {
                    BaseForm.lblRollOutLength.Visible = true;

                    BaseForm.lblRollOutLength.BringToFront();

                    Point p = BaseForm.PointToClient(Cursor.Position);

                    BaseForm.lblRollOutLength.Location = new Point(p.X - 20, p.Y + 20);

                    BaseForm.lblRollOutLength.Text = drawoutText;
                }
                
            }

            else
            {
                if (!string.IsNullOrEmpty(SystemState.TssDrawoutLength.Text))
                {
                    BaseForm.lblRollOutLength.Visible = false;
                    SystemState.TssDrawoutLength.Text = string.Empty;
                }
            }
        }

        internal void ShowDrawing(bool bShowDrawing)
        {
            GraphicsLayerBase layer = CurrentPage.DrawingLayer;

            if (layer == null)
            {
                return;
            }

            layer.SetLayerVisibility(bShowDrawing);
        }

        List<Coordinate> coordinateList = new List<Coordinate>();
       
        /// <summary>
        /// Remove layout area removes the layout area and all elements from the system and the visio canvas.
        /// </summary>
        /// <param name="canvasLayoutArea">The canvas layout area to remove</param>
        internal void RemoveLayoutArea(CanvasLayoutArea canvasLayoutArea)
        
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { canvasLayoutArea });
#endif

            //UCAreaFinishPaletteElement ucAreaFinish = canvasLayoutArea.UCAreaFinish;
        
            this.CurrentPage.RemoveLayoutArea(canvasLayoutArea);

            if (canvasLayoutArea == LastLayoutArea)
            {
                LastLayoutArea = null;
            }

            if (canvasLayoutArea.ExternalArea != null)
            {
                foreach (CanvasDirectedLine line in canvasLayoutArea.ExternalArea)
                {
                    LineFinishManager lineFinishManager = line.LineFinishManager;

                    if (lineFinishManager != null)
                    {
                        lineFinishManager.RemoveLine(line);
                    }

                }

                AreaFinishManager areaFinishManager = canvasLayoutArea.AreaFinishManager;

                if (areaFinishManager != null)
                {
                    GraphicsLayerBase layer = areaFinishManager.AreaPerimeterLayer;

                    if (layer != null)
                    {
                        foreach (CanvasDirectedLine line in canvasLayoutArea.ExternalArea)
                        {
                            layer.RemoveShapeFromLayer(line, 1);
                        }
                    }

                }
            }

            foreach (CanvasDirectedPolygon internalArea in canvasLayoutArea.InternalAreas)
            {
                foreach (CanvasDirectedLine line in internalArea)
                {
                    LineFinishManager lineFinishManager = line.LineFinishManager;

                    if (lineFinishManager != null)
                    {
                        lineFinishManager.RemoveLine(line);
                    }
                }
            }

            canvasLayoutArea
                .RemoveFromCanvas(true); // Remove the layout area (including all elements) from the visio canvas

            Page.RemoveFromPageShapeDict(canvasLayoutArea.Shape);

            if (canvasLayoutArea.AreaFinishManager != null)
            {
                canvasLayoutArea.AreaFinishManager.RemoveLayoutArea(canvasLayoutArea);
            }
            

            canvasLayoutArea.Delete();
        }

        internal void RemoveLineShape(CanvasDirectedLine line)
        {
            if (line is null)
            {
                return;
            }

            string guid = line.Guid;

            if (string.IsNullOrEmpty(guid))
            {
                return;
            }

            if (this.CurrentPage.DirectedLineDictContains(line))
            {
                this.CurrentPage.RemoveFromDirectedLineDict(line);
            }

            CurrentPage.RemoveFromDirectedLineDict(line);

            line.LineFinishManager.RemoveLineFull(line);

            line.Delete();
        }

        public bool NewSequenceStarted { get; set; } = false;

        public bool DrawingInProgress()
        {
            if (SystemState.DesignState == DesignState.Area)
            {
                if (SystemState.DrawingShape)
                {
                    return true;
                }
            }

            else if (SystemState.DesignState == DesignState.Line)
            {
                if (SystemState.DrawingMode == DrawingMode.Line2X && SystemState.DrawingShape)
                {
                    return true;
                }

                else if (SystemState.DrawingMode == DrawingMode.Line1X)
                {
                    if (NewSequenceStarted)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void RedoSeamsAndCuts()
        {
            List<CanvasLayoutArea> redoList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea layoutArea in CurrentPage.LayoutAreas)
            {
                if (layoutArea.HasSeamsOrRollouts)
                {
                    redoList.Add(layoutArea);
                    layoutArea.RemoveSeamsAndRollouts();
                }
            }

            foreach (CanvasLayoutArea layoutArea in redoList)
            {
                layoutArea.RegenerateSeamsAndCuts();

                layoutArea.AreaFinishManager.UpdateFinishStats();

                layoutArea.DrawSeams();
            }

            if (redoList.Count > 0)
            {
                UpdateAreaSeamsUndrsOversDataDisplay();

                if (FinishManagerGlobals.SelectedAreaFinishManager.MaterialsType == MaterialsType.Rolls)
                {
                    SystemGlobals.OversUndersFormUpdate(true);
                }
            }
        }

        //public System.Drawing.Rectangle GetScreen()
        //{
        //    return Screen.FromControl(BaseForm).Bounds;
        //}

        public void SetZoom(double zoom, bool centerDrawing) =>PanAndZoomController.SetZoom(zoom, centerDrawing);

        public void SetZoom() => PanAndZoomController.SetZoom();

       // public void UpdateDebugForm() => BaseForm.UpdateDebugForm();

        public string LayerVisibilityConsistencyCheck(GraphicsLayer layer) => FloorMaterialEstimator.DebugSupportRoutines.LayerVisibilityConsistencyCheck(layer);

        public string LayerVisibilityConsistencyCheck(GraphicsLayerBase layer) => FloorMaterialEstimator.DebugSupportRoutines.LayerVisibilityConsistencyCheck(layer);

        //public void ResetCopyAndPasteMode()
        //{
        //    BaseForm.btnCopyAndPasteShapes.BackColor = SystemColors.ControlLightLight;

        //    if (Utilities.IsNotNull(CopySelectedLayoutArea))
        //    {
        //        if (Utilities.IsNotNull(CopySelectedLayoutArea.CopyMarker))
        //        {
        //            CopySelectedLayoutArea.CopyMarker.Delete();
        //        }

        //        CopySelectedLayoutArea = null;
        //    }
        //}

        public void SetupMarkerAndGuides(Coordinate coord)
        {
            bool deselectRequired = false;

            if (GlobalSettings.ShowMarker)
            {
                CurrentPage.RemoveMarker();
                CurrentPage.PlaceMarker(coord, GlobalSettings.MarkerWidth);

                deselectRequired = true;
            }

            if (GlobalSettings.ShowGuides)
            {
                CurrentPage.RemoveGuides();
                CurrentPage.PlaceGuides(coord);

                deselectRequired = true;
            }

            if (deselectRequired)
            {
                Window?.DeselectAll();
            }
//#if DEBUG
//            BaseForm.UpdateDebugForm();
//#endif
        }

        public void RemoveMarkerAndGuides()
        {
            CurrentPage.RemoveMarker();
            CurrentPage.RemoveAllGuides();
        }

        private void MoveShapeByIncrement(GraphicShape shape, double dx, double dy)
        {
            getIncrement(ref dx, ref dy);

            VisioInterop.MoveShape(Window, shape, dx, dy);
        }

        private void MoveSelectedShapeByIncrement(GraphicShape shape, double dx, double dy)
        {
            getIncrement(ref dx, ref dy);

            VisioInterop.MoveSelectedShape(Window, shape, dx, dy);
        }

        private void getIncrement(ref double dx, ref double dy)
        {
            double baseScale = (double)GlobalSettings.ArrowMoveIncrement / 100.0;

            int? numericKey = KeyboardUtils.GetNumericKeyPressed();
            
            if (numericKey.HasValue)
            {
                if (numericKey.Value != 0 && numericKey.Value != 5)
                {
                    if (numericKey.Value > 5)
                    {
                        baseScale *= 10.0 * (double) (numericKey.Value - 5) / 4.0; 
                    }

                    else
                    {
                        baseScale *= .1 * (double)(5 - numericKey.Value);
                    }
                }
            }
            dx *= baseScale;
            dy *= baseScale;
        }
    }
}
