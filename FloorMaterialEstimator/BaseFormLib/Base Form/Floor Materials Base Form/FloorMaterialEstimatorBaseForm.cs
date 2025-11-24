//-------------------------------------------------------------------------------//
// <copyright file="FloorMaterialEstimatorBaseForm.cs"                           //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;

    using CanvasLib.Legend;
    using Globals;
    using CanvasLib.Filters.Area_Filter;

    using SettingsLib;

    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;

    using CanvasLib;
    using FinishesLib;
    using PaletteLib;
    using Utilities;
    using System.Reflection;
    using CanvasLib.Scale_Line;
    using CanvasLib.Markers_and_Guides;
    using System.Collections.Generic;
    using Graphics;
    using MaterialsLayout;
    using FloorMaterialEstimator.Base_Form.Floor_Materials_Base_Form;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Threading;
    using TracerLib;

    //using System.Windows.Input;

    public partial class FloorMaterialEstimatorBaseForm : Form, IMessageFilter
    {
        private ToolTip toolTip = new ToolTip();

        private int cntlPanesBaseLocY = 32;
        private int pageAreaLineWidth = 296 - 20;

        private int tlsBaseLocY;

        public string CurrentProjectName;
        public string CurrentProjectPath;

        public string CurrentProjectFullPath;
        public string CurrentSetupFullPath;

        public string CurrentDrawingName;
        public string CurrentDrawingPath;

        public bool CurrentProjectChanged
        {
            get
            {
                return SystemState.CurrentProjectChanged;
            }

            set
            {
                SystemState.CurrentProjectChanged = value;
            }
        }

        public const int tbcAreaModeIndex = 0;
        public const int tbcLineModeIndex = 1;
        public const int tbcSeamModeIndex = 2;

        private const int STATUSBAR_ADJUSTMNENT_2 = 1000;

        public CanvasPage CurrentPage => CanvasManager.CurrentPage;

        //public UCLegend UCLegend { get; set; } = new UCLegend();

        public ToolTipEditManager ToolTipEditManager { get; set; }


        private void SystemState_DrawingModeChanged(DrawingMode drawingMode)
        {
#if DEBUG
            tlsDrawingMode.Text = "Drawing Mode: " + drawingMode.ToString();
#endif
            SystemState.DrawingMode = drawingMode;

            if (drawingMode == DrawingMode.ScaleLine)
            {
                btnSetCustomScale.Checked = true;
                btnTapeMeasure.Checked = false;

                return;
            }

            if (drawingMode == DrawingMode.TapeMeasure)
            {
                btnSetCustomScale.Checked = false;
                btnTapeMeasure.Checked = true;

                return;
            }

            if (drawingMode == DrawingMode.Default)
            {
                btnSetCustomScale.Checked = false;
                btnTapeMeasure.Checked = false;

                return;
            }

            if (drawingMode == DrawingMode.Line1X || drawingMode == DrawingMode.Line2X || drawingMode == DrawingMode.LineDuplicate)
            {
                btnSetCustomScale.Checked = false;
                btnTapeMeasure.Checked = false;

                return;
            }

            if (drawingMode == DrawingMode.Rectangle)
            {
                btnSetCustomScale.Checked = false;
                btnTapeMeasure.Checked = false;

                return;
            }

            if (drawingMode == DrawingMode.Polyline)
            {
                btnSetCustomScale.Checked = false;
                btnTapeMeasure.Checked = false;

                return;
            }
        }

//        public DrawingMode DrawingMode
//        {
//            get
//            {
//                return SystemState.DrawingMode;
//            }

//            set
//            {
//#if DEBUG
//                tlsDrawingMode.Text = "Drawing Mode: " + value.ToString();
//#endif
//                SystemState.DrawingMode = value;

//                if (value == DrawingMode.ScaleLine)
//                {
//                    btnSetCustomScale.Checked = true;
//                    btnTapeMeasure.Checked = false;

//                    return;
//                }

//                if (value == DrawingMode.TapeMeasure)
//                {
//                    btnSetCustomScale.Checked = false;
//                    btnTapeMeasure.Checked = true;

//                    return;
//                }

//                if (value == DrawingMode.Default)
//                {
//                    btnSetCustomScale.Checked = false;
//                    btnTapeMeasure.Checked = false;

//                    return;
//                }

//                if (value == DrawingMode.Line1X || value == DrawingMode.Line2X || value == DrawingMode.LineDuplicate)
//                {
//                    btnSetCustomScale.Checked = false;
//                    btnTapeMeasure.Checked = false;

//                    return;
//                }

//                if (value == DrawingMode.Rectangle)
//                {
//                    btnSetCustomScale.Checked = false;
//                    btnTapeMeasure.Checked = false;

//                    return;
//                }

//                if (value == DrawingMode.Polyline)
//                {
//                    btnSetCustomScale.Checked = false;
//                    btnTapeMeasure.Checked = false;

//                    return;
//                }

//                if (value == DrawingMode.BorderGeneration)
//                {
//                    return;
//                }
//            }
//        }

        public UCAreaFinishPalette areaPalette
        {
            get
            {
                return PalettesGlobal.AreaFinishPalette;
            }

            set
            {
                PalettesGlobal.AreaFinishPalette = value;
            }
        }
        public UCLineFinishPalette linePalette
        {
            get
            {
                return PalettesGlobal.LineFinishPalette;
            }

            set
            {
                PalettesGlobal.LineFinishPalette = value;
            }
        }


        public UCSeamFinishPalette seamPalette;

        public AreaFinishBaseList AreaFinishBaseList
        {
            get
            {
                return FinishGlobals.AreaFinishBaseList;
            }

            set
            {
                FinishGlobals.AreaFinishBaseList = value;
            }
        }

        public AreaFinishManagerList AreaFinishManagerList
        {
            get
            {
                return CanvasManagerGlobals.AreaFinishManagerList;
            }

            set
            {
                CanvasManagerGlobals.AreaFinishManagerList = value;
            }
        }

        public LineFinishBaseList LineFinishBaseList
        {
            get
            {
                return FinishGlobals.LineFinishBaseList;
            }

            set
            {
                FinishGlobals.LineFinishBaseList = value;
            }
        }

        public LineFinishManagerList LineFinishManagerList
        {
            get
            {
                return CanvasManagerGlobals.LineFinishManagerList;
            }

            set
            {
                CanvasManagerGlobals.LineFinishManagerList = value;
            }
        }


        public SeamFinishBaseList SeamFinishBaseList
        {
            get
            {
                return FinishGlobals.SeamFinishBaseList;
            }

            set
            {
                FinishGlobals.SeamFinishBaseList = value;
            }
        }

        public SeamFinishManagerList SeamFinishManagerList
        {
            get
            {
                return CanvasManagerGlobals.SeamFinishManagerList;
            }

            set
            {
                CanvasManagerGlobals.SeamFinishManagerList = value;
            }
        }

//public List<AreaFinishManager> AreaFinishElementList
//{
//    get
//    {
//        List<AreaFinishManager> rtrnList = new List<AreaFinishManager>();

//        foreach (UCAreaFinishPaletteElement finishPalletElement in areaPalette)
//        {
//            rtrnList.Add(finishPalletElement.AreaFinishManager);
//        }

//        return rtrnList;
//    }
//}
//public AreaFilters AreaFilters;

public String Notes { get; set; }

        public CanvasManager.CanvasManager CanvasManager;

        public Bitmap CustomScaleLineColoredImage;
        public Bitmap CustomScaleLineUncoloredImage;

        public Bitmap ExpandCanvasImage;
        public Bitmap ContractCanvasImage;

        public Bitmap RedoSeamsOffImage {  get { return StaticGlobals.RedoSeamsOffImage; } set { StaticGlobals.RedoSeamsOffImage = value ; } }
        public Bitmap RedoSeamsOnImage { get { return StaticGlobals.RedoSeamsOnImage; } set { StaticGlobals.RedoSeamsOnImage = value; } }

        public Bitmap ShowAreaImage;
        public Bitmap HideAreaImage;

        public Bitmap ShowDrawingImage;
        public Bitmap HideDrawingImage;

        public Bitmap MRghtButtonDownImage;
        public Bitmap MLeftButtonDownImage;

        public Bitmap MeasuringStickNotSelectedImage;
        public Bitmap MeasuringStickSelectedImage;

        public Bitmap MButtonUpImage;

        private CanvasPage currentPage
        {
            get
            {
                if (CanvasManager == null)
                {
                    return null;
                }

                return CanvasManager.CurrentPage;
            }
        }


        private DrawingMode prevAreaModeDrawingMode = DrawingMode.Default;

        private MSStencilHelper stencilHelper = new MSStencilHelper();

        public MeasuringStickStencil SelectedMeasuringStickStencil
        {
            get { return this.stencilHelper.SelectecdStencil; }
        }

        public List<MeasuringStickStencil> MeasuringStickStencils
        {
            get { return this.stencilHelper.Stencils; }
        }

        public List<Color> MeasuringStickStencilColors
        {
            get { return this.stencilHelper.StencilColors; }
        }

        //internal void MoveLineToSelectedLineType(string nameID)
        //{
        //    if (string.IsNullOrEmpty(nameID))
        //    {
        //        return;
        //    }

        //    linePalette.MoveLineToSelectedLineType(nameID);
        //}

        internal void MoveLineToSelectedLineType(CanvasDirectedLine line)
        {
            if (line is null)
            {
                return;
            }

            LineFinishManagerList.MoveLineToSelectedLineType(line);
        }

        internal void DeleteLineShape(CanvasDirectedLine line)
        {
            if (line is null)
            {
                return;
            }

            CanvasManager.RemoveLineShape(line);
        }

        private EditAreaMode editAreaMode { get; set; } = EditAreaMode.ChangeShapesToSelected;

        public EditAreaMode EditAreaMode
        {
            get
            {
                return editAreaMode;
            }

            set
            {
                editAreaMode = value;
            }
        }

        private EditLineMode editLineMode { get; set; } = EditLineMode.ChangeLinesToSelected;

        public EditLineMode EditLineMode
        {
            get
            {
                return editLineMode;
            }

            set
            {
                editLineMode = value;
            }
        }

        private LineDrawingMode layoutLineMode = LineDrawingMode.Mode2X;

        public LineDrawingMode LayoutLineMode
        {
            get
            {
                return layoutLineMode;
            }

            set
            {
                if (layoutLineMode == value)
                {
                    return;
                }

                // The following may not be needed, but added to cover all basis. The purpose is to
                // remove the start marker when there is an appropriate change in system state.

                CurrentPage.RemoveLineModeStartMarker();

                if (value == LineDrawingMode.Mode1X)
                {
                    this.LblNewSequence.BackColor = Color.Orange;
                    this.BtnLayoutLine1XMode.BackColor = Color.Orange;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                    this.btnLayoutLineActivate.BackColor = SystemColors.Control;

                    this.btnLayoutLineActivate.Text = "Activate";

                    this.btnLayoutLineDuplicate.Enabled = false;

                    layoutLineMode = LineDrawingMode.Mode1X;

                    SystemState.DrawingMode = DrawingMode.Line1X;

                    return;
                }

                if (value == LineDrawingMode.Mode2X)
                {
                    this.LblNewSequence.BackColor = SystemColors.ControlLightLight;
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLine2XMode.BackColor = Color.Orange;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                    this.btnLayoutLineActivate.BackColor = SystemColors.Control;

                    this.btnLayoutLineActivate.Text = "Activate";

                    layoutLineMode = LineDrawingMode.Mode2X;

                    SystemState.DrawingMode = DrawingMode.Line2X;

                    return;
                }

                if (value == LineDrawingMode.Duplicate)
                {
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLineDuplicate.BackColor = Color.Orange;
                    this.btnLayoutLineActivate.BackColor = SystemColors.Control;

                    this.btnLayoutLineActivate.Text = "Activate";

                    layoutLineMode = LineDrawingMode.Duplicate;


                    SystemState.DrawingMode = DrawingMode.LineDuplicate;

                    return;
                }

                if (value == LineDrawingMode.TakeoutArea)
                {
                    this.BtnLayoutLine1XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                    this.btnLayoutLineActivate.BackColor = Color.Orange;

                    this.btnLayoutLineActivate.Text = "Deactivate";

                    layoutLineMode = LineDrawingMode.TakeoutArea;

                    return;
                }

                this.BtnLayoutLine1XMode.BackColor = SystemColors.Control;
                this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                this.btnLayoutLineActivate.BackColor = SystemColors.Control;
            }
        }

        public bool CounterMode => this.BtnCounters.Checked;

        public bool AreaModeLabelMode => this.btnShowLabelEditor.Checked;

        public void SetDesignStateButton(DesignState designState)
        {
            if (designState == DesignState.Area)
            {
                Utilities.SetButtonState(btnAreaDesignState, true);
                Utilities.SetButtonState(btnLineDesignState, false);
                Utilities.SetButtonState(btnSeamDesignState, false);

                this.pnlAreaCommandPane.BringToFront();
            }

            else if (designState == DesignState.Line)
            {
                Utilities.SetButtonState(btnAreaDesignState, false);
                Utilities.SetButtonState(btnLineDesignState, true);
                Utilities.SetButtonState(btnSeamDesignState, false);

                this.pnlLineCommandPane.BringToFront();

            }

            else if (designState == DesignState.Seam)
            {
                Utilities.SetButtonState(btnAreaDesignState, false);
                Utilities.SetButtonState(btnLineDesignState, false);
                Utilities.SetButtonState(btnSeamDesignState, true);

                this.pnlSeamCommandPane.BringToFront();
            }
        }

        internal void UpdateAreaSelections(AreaFinishManager areaFinishManager)
        {
            //SetupEditAreas(areaFinishBase);

            if (this.DesignState == DesignState.Seam)
            {
                SetupSeamModeDesignState(areaFinishManager);
            }


            if (this.DesignState == DesignState.Area)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.AreaDesignStateSelectedAreas())
                {
                    CanvasManager.ProcessEditAreaModeActionChangeShapeToFinish(canvasLayoutArea, areaFinishManager);
                    CurrentPage.SetAreaDesignStateAreaSelectionStatus(canvasLayoutArea, false);
                }
            }
        }

        internal void UpdateLineSelections(UCLineFinishPaletteElement ucLine)
        {
            //SetupEditLines(ucLine);

            // this.uclSelectedLine.Invalidate();

            CanvasManager.LineFinishSelectionChanged(ucLine);
        }

        internal void UpdateSeamDefinition(SeamFinishBase finishSeamBase)
        {

        }

        public FloorMaterialEstimatorBaseForm()
        {
            InitializeComponent();


            KeyPreview = true;

            GlobalSettings.Initialize(Program.AppConfig);

            Tracer.TraceGen = new Tracer(GlobalSettings.TraceLevel, Program.TraceLogFilePath, true);

            this.lblProjectName.Text = "Version " + Program.Version;

            this.tslErrorOccured.Visible = false;

            Tracer.TraceGen.ErrorReported += Tracer_ErrorReported;

            Tracer.TraceGen.ExceptionReported += Tracer_ExceptionReported;
            Tracer.TraceGen.TraceInfo("Project " + this.lblProjectName.Text + " starts.");

            try
            {
                runFloorMaterialEstimatorBaseForm();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception at floor materials base form level:", ex, 1, true);
            }
        }

        private void Tracer_ExceptionReported(Exception ex)
        {
            this.tslErrorOccured.Visible = true;
        }

        private void Tracer_ErrorReported()
        {
            this.tslErrorOccured.Visible = true;
        }

        private void runFloorMaterialEstimatorBaseForm()
        {
            ShortcutSettings.Initialize(Program.AppConfig, Program.UserType);

            //---------------------------///
            // Initialize static globals //
            //---------------------------//
            SystemState.Init(
                RbnAreaModeAutoSeamsShowAll
                , RbnAreaModeAutoSeamsHideAll
                , RbnAreaModeAutoSeamsShowUnHideable
                , ckbShowAreaModeCutIndices
                , RbnSeamModeAutoSeamsHideAll
                , RbnSeamModeAutoSeamsShowUnHideable
                , RbnSeamModeManualSeamsShowAll
                , RbnSeamModeAutoSeamsShowAll
                , ckbShowSeamModeCuts
                , ckbShowSeamModeCutIndices
                , ckbShowSeamModeOvers
                , ckbShowSeamModeUndrs
                , ckbShowEmbeddedCuts
                , ckbShowEmbeddedOvers
                , btnAreaDesignStateZeroLine
                , this.btnRedoSeamsAndCuts
                , this.rbnFixedWidth
                , this.btnLayoutAreaTakeout
                , this.btnLayoutAreaTakeOutAndFill
                , this.btnTapeMeasure
                , tbcPageAreaLine
                , this.tkbHScroll
                , this.tkbVScroll
                , this.btnZoomIn
                , this.btnZoomOut
                , this.ddbZoomPercent
                , this.btnFitCanvas
                , this.cccAreaMode
                , this.cccLineMode
                , this.BtnCounters
                , this.btnShowLegendForm
                , this.BtnShowFieldGuides
                , this.tssDrawoutLength
                , this.tlsDrawingShape
                , this.btnSetCustomScale
                , this.nudFixedWidthFeet
                , this.nudFixedWidthInches
                , this.btnMeasuringStick
                , this.btnDoorTakeoutShow
                , this.rbnColorOnly
                , this.rbnOversGenerator
                , this.btnEmbeddLayoutAreas
                , this.nudOversGeneratorWidthFeet
                , this.nudOversGeneratorWidthInches);

            StaticGlobals.SetupAllSeamStateSeamLayersForSelectedArea = new Action(() => AreaFinishManagerList.ForEach(m => m.SetupAllSeamLayers()));

            StaticGlobals.ActivateTapeMeasure = new Action(() => BtnTapeMeasure_Click(null, null));

            StaticGlobals.OversUndersFormUpdate = this.OversUndersFormUpdate;

            StaticGlobals.SetCursorPosition = SetCursorPosition;

            StaticGlobals.GetCursorPosition = GetCursorPosition;

            SystemState.DrawingModeChanged += SystemState_DrawingModeChanged;
            //------------------------------------------------------------------//

            setupToolTips();

            EnableOversUndersButton(true);

            tlsBaseLocY = tlsMainToolStrip.Location.Y + tlsMainToolStrip.Height + 2;

            this.tbcPageAreaLine.Location = new Point(0, cntlPanesBaseLocY);

            GuidMaintenance.ClearGuids();

            this.stencilHelper.Init(this.axDrawingControl);
            SetupMeasuringStickMenu(this.stencilHelper);
            axDrawingControl.Document.ShapeAdded += new Microsoft.Office.Interop.Visio.EDocument_ShapeAddedEventHandler(ShapeAddedEventHandler);

            areaPalette = new UCAreaFinishPalette();
            linePalette = new UCLineFinishPalette();
            seamPalette = new UCSeamFinishPalette();

            CanvasManager = new CanvasManager.CanvasManager(this, axDrawingControl);
            if (this.stencilHelper.SelectecdStencil != null) CanvasManager.MeasuringStick.RulerStencil = this.stencilHelper.SelectecdStencil.Stencil;

            CanvasManager.AreaSelectionChangedEvent += AreaSelectionChangedEventHandler;

            loadFinishPalettesFromDefaults();

            CanvasManager.LabelManager = new LabelManager(AreaFinishManagerList, this, CanvasManager.Window, CanvasManager.Page, btnShowLabelEditor);

            this.tbpAreas.Controls.Add(areaPalette);
            this.tbpLines.Controls.Add(linePalette);
            this.tbpSeams.Controls.Add(seamPalette);

            //SetupEditAreas(selectedAreaFinish);
            //SetupEditLines(selectedLineFinish);

            this.btnAreaDesignState.Checked = true;
            this.btnLineDesignState.Checked = false;
            this.btnAutoSelect.Checked = false;

            SystemState.DrawingMode = DrawingMode.Default;

            this.LblNewSequence.BackColor = SystemColors.ControlLightLight;

            this.BtnFilterAreas.Click += BtnFilterAreas_Click;
            this.BtnFilterLines.Click += BtnFilterLines_Click;

            //this.btnLineDesignStateEditMode.Click += BtnLineDesignStateEditMode_Click;
            //this.btnLineDesignStateLayoutMode.Click += BtnLineDesignStateLayoutMode_Click;
            this.BtnSummaryReport.Click += BtnSummaryReport_Click;
            this.BtnShowFieldGuides.Click += BtnShowFieldGuides_Click;
            this.btnHideFieldGuides.Click += BtnHideFieldGuides_Click;
            this.btnDeleteFieldGuides.Click += BtnDeleteFieldGuides_Click;
            this.btnAutoSelect.Click += BtnAutoSelect_Click;
            this.BtnShowFieldGuides.Checked = true;
            this.btnHideFieldGuides.Checked = false;

            if (GlobalSettings.AllowEditingOfToolTips)
            {
                this.btnToolTipSettings.Visible = true;
            }

            else
            {
                this.btnToolTipSettings.Visible = false;
            }

            ClearLineLengthStatusStripDisplay();

            this.cccAreaMode.Init();
            this.cccLineMode.Init();

            this.pnlAreaCommandPane.BringToFront();

            this.btnGeneralSettings.Click += BtnGeneralSettings_Click;
            this.btnAreaEditSettings.Click += btnAreaEditSettings_Click;
            this.btnLineEditSettings.Click += btnLineEditSettings_Click;
            this.btnDebug.Click += BtnDebug_Click;
            tlsMainToolStrip.Renderer = new CustomToolbarButtonRenderer();


            this.Cursor = Cursors.Arrow;

            this.tbcPageAreaLine.SelectedIndexChanged += TbcPageAreaLine_SelectedIndexChanged;

            this.nudFixedWidthFeet.Init("{0}'");
            this.nudFixedWidthInches.Init("{0}\"");

            this.nudOversGeneratorWidthFeet.Init("{0}'");
            this.nudOversGeneratorWidthInches.Init("{0}\"");

            this.btnAbout.Click += BtnAbout_Click;
            setupSeamTab();

            this.tbcPageAreaLine.Selecting += TbcPageAreaLine_Selecting;

            KeyPress += FloorMaterialEstimatorBaseForm_KeyPress;
            KeyDown += FloorMaterialEstimatorBaseForm_KeyDown;
            KeyUp += FloorMaterialEstimatorBaseForm_KeyUp;
            //uclSelectedLine.Init(this.linePalette);

            CanvasManager.ShowGrid = GlobalSettings.ShowGrid;
            CanvasManager.ShowRulers = GlobalSettings.ShowRulers;
            CanvasManager.ShowPanAndZoom = GlobalSettings.ShowPanAndZoom;

            CanvasManager.CurrentPage.VisioPage.AutoSize = false;

            VisioInterop.HideDrawingExplorer(CanvasManager.Window);

            this.btnShowSeamingTool.MouseUp += new System.Windows.Forms.MouseEventHandler(CanvasManager.BtnShowSeamingTool_Click);
            this.btnCenterSeamingToolInView.Click += btnCenterSeamingToolInView_Click;
            this.KeyPreview = true;

            this.FormClosing += FloorMaterialEstimatorBaseForm_FormClosing;


            //((Control)this.axDrawingControl).MouseEnter += FloorMaterialEstimatorBaseForm_MouseEnter;
            ((Control)this.axDrawingControl).MouseLeave += FloorMaterialEstimatorBaseForm_MouseLeave;
            //this.MouseLeave += FloorMaterialEstimatorBaseForm_MouseEnter;

            //this.axDrawingControl.MouseMoveEvent += AxDrawingControl_MouseMoveEvent;

            this.clrEditAreaModeButtons();
            this.clrEditLineModeButtons();
            this.setLayoutLineModeButtons();

            this.ddbZoomPercent.Text = "100%";

            //((Control)this.axDrawingControl).MouseWheel += FloorMaterialEstimatorBaseForm_MouseWheel;
            //pnlAreaCommandPane.MouseEnter += PnlAreaCommandPane_MouseEnter;


            this.tkbHScroll.BringToFront();
            this.tkbVScroll.BringToFront();

            this.ucAreasView.Size = new Size(212, 250);

            this.ucSeamsView.Size = this.ucAreasView.Size;
            this.ucSeamsView.Location = this.ucAreasView.Location;

            this.ucRemnantsView.Size = this.ucAreasView.Size;
            this.ucRemnantsView.Location = this.ucAreasView.Location;

            this.ucAreasView.BringToFront();

            this.ucSeamsView.SendToBack();
            this.ucSeamsView.Visible = false;

            this.ucRemnantsView.SendToBack();
            this.ucRemnantsView.Visible = false;

            this.grbRemnantSeamWidth.Size = this.grbViewSelection.Size;
            this.grbRemnantSeamWidth.Location = this.grbViewSelection.Location;

            this.grbRemnantSeamWidth.Visible = false;
            this.grbRemnantSeamWidth.SendToBack();

            //this.Controls.Add(UCLegend);

            //UCLegend.Init(CanvasManager.Window, CanvasManager.CurrentPage, AreaFinishBaseList);

            FloorMaterialEstimatorBaseForm_SizeChanged(null, null);

            this.SizeChanged += FloorMaterialEstimatorBaseForm_SizeChanged;

            // CanvasManager.SetZoom(1, true); // Added


            Assembly asm = Assembly.GetExecutingAssembly();

            CustomScaleLineColoredImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.CustomScaleColored.png"));
            CustomScaleLineUncoloredImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.CustomWarning.png"));

            ExpandCanvasImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.HidePanes.png"));
            ContractCanvasImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.ShowPanes.png"));

            RedoSeamsOffImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.RedoSeamsOff.png"));
            RedoSeamsOnImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.RedoSeamsOn.png"));

            ShowAreaImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.ShowArea.png"));
            HideAreaImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.HideArea.png"));

            ShowDrawingImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.ShowDrawing.png"));
            HideDrawingImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.HideDrawing.png"));

            MeasuringStickNotSelectedImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.MeasuringStickNotSelected.png"));
            MeasuringStickSelectedImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.MeasuringStickSelected.png"));

            // string resources = Assembly.GetManifestResourceNames();

            MRghtButtonDownImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.MRghtButtonDown.png"));
            MLeftButtonDownImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.MLeftButtonDown.png"));

            MButtonUpImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.MButtonUp.png"));

            CanvasManager.ExtendedCrosshairs = new ExtendedCrosshairs(CanvasManager.Window, CanvasManager.Page);

            CanvasManager.Page.AddToPageShapeDict(CanvasManager.ExtendedCrosshairs.HorzLine);
            CanvasManager.Page.AddToPageShapeDict(CanvasManager.ExtendedCrosshairs.VertLine);

            CurrentPage.ExtendedCrosshairsLayer.AddShapeToLayer(CanvasManager.ExtendedCrosshairs.HorzLine, 1);
            CurrentPage.ExtendedCrosshairsLayer.AddShapeToLayer(CanvasManager.ExtendedCrosshairs.VertLine, 1);

            CurrentPage.ExtendedCrosshairsLayer.SetLayerVisibility(false);

#if DEBUG
            // Set up design state on status bar

            string designStateText = "Design State: " + DesignState.ToString();

            /* if (DesignState == DesignState.Area)
             {
                 designStateText += "(" + AreaMode.ToString() + ")";
             }

             elseif (DesignState == DesignState.Line)
             {
                 designStateText += "(" + LineMode.ToString() + ")";
             }
             */

            this.tlsDesignState.Text = designStateText;

            this.tlsDrawingMode.Text = "Drawing Mode: " + SystemState.DrawingMode.ToString();
            this.tlsDrawingShape.Text = "Drawing shape: " + SystemState.DrawingShape.ToString();


            currentPage.SetDefaultScale(GlobalSettings.DefaultDrawingScaleInInches);

            //this.btnCompleteShapeByIntersection.MouseHover += BtnCompleteShapeByIntersection_MouseHover;

            this.Shown += FloorMaterialEstimatorBaseForm_Shown;

#if DEBUG
            UpdateMousePositionDisplay();
#endif
            this.btnDebug.Visible = true;
#endif
            // For some unknown reason, the original request to set the zoom does not work. So we manually click the zoom to fit here.

            CanvasManager.SetZoom(1, true);

            CanvasManager.PanAndZoomController.ZoomToFit();
        }

        public void SelectMeasuringStickStencil(Color color)
        {
            this.stencilHelper.SelectMeasuringStickStencil(color);
            CanvasManager.MeasuringStick.RulerStencil = this.stencilHelper.SelectecdStencil.Stencil;
            SetupMeasuringStickMenu(this.stencilHelper);
        }

        private void SetupMeasuringStickMenu(MSStencilHelper stencilHelper)
        {
            this.btnMeasuringStick.DropDown = new ContextMenuStrip();

            for (int i = 0; i < stencilHelper.Stencils.Count; ++i)
            {
                this.btnMeasuringStick.DropDown.Items.Add(stencilHelper.Stencils[i].DisplayName);
            }

            SelectMeasuringStickMenuItem(stencilHelper);
        }

        private void SelectMeasuringStickMenuItem(MSStencilHelper stencilHelper, int prevousIndex = -1)
        {
            if (this.btnMeasuringStick.DropDownItems.Count > stencilHelper.SelectecdStencilIndex)
            {
                this.btnMeasuringStick.DefaultItem = this.btnMeasuringStick.DropDownItems[stencilHelper.SelectecdStencilIndex];

                this.btnMeasuringStick.DropDownItems[stencilHelper.SelectecdStencilIndex].Font = new Font(this.btnMeasuringStick.DropDownItems[stencilHelper.SelectecdStencilIndex].Font, FontStyle.Bold);
                if (prevousIndex >= 0) this.btnMeasuringStick.DropDownItems[prevousIndex].Font = new Font(this.btnMeasuringStick.DropDownItems[prevousIndex].Font, FontStyle.Regular);

                string toolTipText = String.Format(Properties.Resources.BF_MEASURING_STICK_TIP, this.stencilHelper.SelectecdStencil.DisplayName);
                this.btnMeasuringStick.ToolTipText = toolTipText;
            }
        }

        private void setupToolTips()
        {
            this.toolTip.ShowAlways = true;

            List<object> toolTippableObjectList = createToolTippableControlsList();

            ToolTipEditManager = new ToolTipEditManager(
                this.toolTip
                , toolTippableObjectList
                , Path.Combine(Program.OCEOperatingDataFolder, @"Defaults\ToolTipDefaults.xml"));
        }

        private List<object> createToolTippableControlsList()
        {
            List<object> toolTippableObjectList = new List<object>();

            foreach (object obj in tlsMainToolStrip.Items)
            {
                toolTippableObjectList.Add(obj);
            }

            foreach (object obj in new List<object>()
            #region Object List
            {
                btnDoorTakeoutShow,
                btnLayoutLineActivate,
                rbnDoorTakeoutOther,
                rbnDoorTakeout6Ft,
                rbnDoorTakeout3Ft,
                btnLayoutLineDuplicate,
                btnLayoutLine2XMode,
                //btnLayoutLineJump,
                BtnLayoutLine1XMode,
                btnCopyAndPasteShapes,
                btnFixedWidthJump,
                btnEmbeddLayoutAreas,
                btnLayoutAreaTakeOutAndFill,
                btnLayoutAreaTakeout,
                btnAreaDesignStateZeroLine,
                btnCompleteShapeByIntersection,
                btnAreaDesignStateCompleteDrawing,
                ckbShowSeamModeUndrs,
                ckbShowSeamModeOvers,
                ckbShowSeamModeCuts,
                btnCenterSeamingToolInView,
                //rbnManualSeamsShowUnhideable,
                RbnSeamModeManualSeamsHideAll,
                RbnSeamModeManualSeamsShowAll,
                RbnSeamModeAutoSeamsHideAll,
                RbnSeamModeAutoSeamsShowUnHideable,
                RbnSeamModeAutoSeamsShowAll,
                btnSubdivideRegion,
                btnSeamArea,
                btnSeamSingleLineToTool,
                btnAlignTool,
                btnShowSeamingTool,
                btnCancelSubdivision,
                btnCompleteSubdivision,
                btnSeamDesignStateSelectionMode,
                btnSeamDesignStateSubdivisionMode,
                rbnViewSeams,
                rbnViewAreas
            }
            #endregion
                )
            {
                toolTippableObjectList.Add(obj);
            }

            return toolTippableObjectList;
        }

        private void FloorMaterialEstimatorBaseForm_Shown(object sender, EventArgs e)
        {

            if (!CurrentPage.ScaleHasBeenSet && GlobalSettings.ShowSetScaleReminder)
            {
                SetScaleWarningForm setScaleWarningForm = new SetScaleWarningForm();

                setScaleWarningForm.ShowDialog();
            }

            if (GlobalSettings.AutosaveEnabled)
            {
                if (GlobalSettings.AutosaveIntervalInSeconds >= 10)
                {
                    Program.autosaveTimer = new System.Timers.Timer(1000 * GlobalSettings.AutosaveIntervalInSeconds);
                    Program.autosaveTimer.Enabled = true;

                    Program.autosaveTimer.Elapsed += doAutoSave;
                    // Synchronize the timer with the text box
                    Program.autosaveTimer.SynchronizingObject = this;
                    // Start the timer
                    Program.autosaveTimer.AutoReset = true;

                }
            }
        }

        //bool withinAutoSave = false;

        // Couldn't get semaphores to work. the following is a kludge that actually has
        // a potential race condition.

        //private static Mutex autosaveMutex = new Mutex(true,"Autosave");

        //private static Semaphore autosaveSemaphore = new Semaphore(0, 2);

        public static Object AutosaveLock = new Object();

        //private void getAutosaveSemaphore()
        //{
        //    lock (autosaveLock)
        //    {
        //        skipAutosave = true;
        //    }
        //}

        //private void relAutosaveSemaphore()
        //{
        //    lock (autosaveLock)
        //    {
        //        skipAutosave = false;
        //    }
        //}

        private static bool withinAutosave = false;

        private static bool withinProjectAccess = false;

        private void doAutoSave(Object source, System.Timers.ElapsedEventArgs e)
        {

            if (DesignState == DesignState.Seam)
            {
                return;
            }

            if (SystemState.DrawingShape)
            {
                return;
            }

            if (withinProjectAccess)
            {
                return;
            }

            if (withinAutosave)
            {
                return;
            }

            withinAutosave = true;

            //btnToolStripSave.Enabled = false;
            //btnToolStripSave.Checked = true;

            lock (AutosaveLock)
            {
                doAutoSaveRoutine(btnToolStripSave);

                //Thread autosaveThread = new Thread(() => doAutoSaveRoutine(btnToolStripSave));
            }

            //btnToolStripSave.Enabled = true;
            //btnToolStripSave.Checked = false;

            withinAutosave = false;
            // autosaveThread.Start();

        }

        private void doAutoSaveRoutine(
            ToolStripButton btn)
        {
            //lock (AutosaveLock)
            //{
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });


            try
            {

                btnToolStripSave.Enabled = false;
                btnToolStripSave.Checked = true;

                //Invoke(new Action(() =>
                //{

                //    btn.Enabled = false;
                //    btn.Checked = true;

                //    btn.Invalidate();

                //}));

                StreamWriter exportStream = new StreamWriter(Path.Combine(Program.AutosaveFolder, "Autosave.eproj"));

                ProjectSerializable project = new ProjectSerializable(this);

                if (!ProjectSerializable.ProjectSerializationSucceeded)
                {
                    return;
                }

                project.Serialize(exportStream);

                //System.Threading.Thread.Sleep(5000);


                btnToolStripSave.Enabled = true;
                btnToolStripSave.Checked = false;

                //Invoke(new Action(() =>
                //{

                //    btn.Enabled = true;
                //    btn.Checked = false;

                //    btn.Invalidate();

                //}));

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in FloorMaterialEstimatorBaseForm:doAutosaveRoutine", ex, 1, true);
            }



            //}

        }

        private void btnCenterSeamingToolInView_Click(object sender, EventArgs e)
        {
            CanvasManager.BtnCenterSeamingToolInView_Click(sender, e);
        }

        private void BtnCompleteShapeByIntersection_MouseHover(object sender, EventArgs e)
        {
            this.btnCompleteShapeByIntersection.Focus();
        }

        //private void PnlAreaCommandPane_MouseEnter(object sender, EventArgs e)
        //{
        //    this.pnlAreaCommandPane.Focus();
        //}

        private void FloorMaterialEstimatorBaseForm_MouseLeave(object sender, EventArgs e)
        {
            //Debug.WriteLine("MouseLeave");

            this.SetCursorForCurrentLocation();

            //this.CanvasManager.ResetSeamSelectedObjects();

            Application.DoEvents();
        }

        private void FloorMaterialEstimatorBaseForm_MouseEnter(object sender, EventArgs e)
        {
            //Debug.WriteLine("MouseEnter");
            this.SetCursorForCurrentLocation();
        }

        #region Mouse cursor control

        private void PnlSeamCommandPane_MouseEnter(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }

        private void TbcPageAreaLine_MouseEnter(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }

        private void TbcPageAreaLine_MouseHover(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }

        private void TlsMainToolStrip_MouseHover(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }

        private void TlsMainToolStrip_MouseEnter(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }
        #endregion

        private void AxDrawingControl_MouseMoveEvent(object sender, AxMicrosoft.Office.Interop.VisOcx.EVisOcx_MouseMoveEvent e)
        {
            //this.Cursor = Cursors.Cross;
        }

        private void loadFinishPalettesFromDefaults()
        {
            string areaFinishInitPath = string.Empty;
            string lineFinishInitPath = string.Empty;
            string finishSeamFileInitPath = string.Empty;

            if (Program.AppConfig.ContainsKey("areafinishinitpath"))
            {
                areaFinishInitPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["areafinishinitpath"]);
            }

            if (Program.AppConfig.ContainsKey("linefinishinitpath"))
            {
                lineFinishInitPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["linefinishinitpath"]);
            }

            if (Program.AppConfig.ContainsKey("seamlinefinishinitpath"))
            {
                finishSeamFileInitPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["seamlinefinishinitpath"]);
            }

            if (!File.Exists(finishSeamFileInitPath))
            {
                MessageBox.Show("The expected file containing the initial definition of seams '" + finishSeamFileInitPath + "' is missing. The program cannot run without it.");
                this.Close();
            }

            if (!File.Exists(lineFinishInitPath))
            {
                MessageBox.Show("The expected file containing the initial definition of line finishes '" + lineFinishInitPath + "' is missing. The program cannot run without it.");
                this.Close();
            }

            if (!File.Exists(areaFinishInitPath))
            {
                MessageBox.Show("The expected file containing the initial definition of area finishes '" + areaFinishInitPath + "' is missing. The program cannot run without it.");
                this.Close();
            }

            SeamFinishBaseList = new SeamFinishBaseList();
            SeamFinishBaseList.Load(finishSeamFileInitPath);

            LineFinishBaseList = new LineFinishBaseList();
            LineFinishBaseList.Load(lineFinishInitPath);

            AreaFinishBaseList = new AreaFinishBaseList();
            AreaFinishBaseList.Load(areaFinishInitPath, SeamFinishBaseList);

            if (this.SeamFinishManagerList != null)
            {
                this.SeamFinishManagerList.Dispose();
            }

            SeamFinishManagerList = new SeamFinishManagerList(CanvasManager, CanvasManager.Window, CanvasManager.Page, SeamFinishBaseList);

            if (this.AreaFinishManagerList != null)
            {
                this.AreaFinishManagerList.Dispose();
            }

            AreaFinishManagerList = new AreaFinishManagerList(CanvasManager.Window, CanvasManager.Page, AreaFinishBaseList);

            if (this.LineFinishManagerList != null)
            {
                this.LineFinishManagerList.Dispose();
            }

            LineFinishManagerList = new LineFinishManagerList(CanvasManager, CanvasManager.Window, CanvasManager.Page, LineFinishBaseList);

            //AreaFilters = new AreaFilters(AreaFinishBaseList);
            //this.AreaFilters.SeamFilterChanged += AreaFilters_SeamFilterChanged;

            // Following can be removed once testing has been completed.

            if (CanvasManager.Window is null || CanvasManager.Page is null)
            {
                throw new Exception("E00002: Window or page is null in the initialization of area pallet.");
            }


            areaPalette.Init(CanvasManager.Window, CanvasManager.Page);
            linePalette.Init(CanvasManager.Window, CanvasManager.Page, LineFinishBaseList);
            seamPalette.Init(CanvasManager.Window, CanvasManager.Page, AreaFinishBaseList, SeamFinishBaseList);

            //areaPalette.FinishChanged += AreaPallet_FinishChanged;

            this.AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
            this.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

            Size pageAreaSize = tbpLines.Size;
            linePalette.Size = new Size(linePalette.Width, 800);

            linePalette.Refresh();

            CanvasManager.LegendController.Init(AreaFinishBaseList, LineFinishBaseList, CanvasManager.CountersList);
        }

        //public void AreaPallet_FinishChanged(UCAreaFinishPaletteElement ucAreaFinish)
        //{
        //    UpdateAreaSelections(ucAreaFinish);

        //    CanvasManager.LastLayoutArea = null;
        //}

        public void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            //UCAreaFinishPaletteElement ucAreaFinishPalletElement = areaPalette[itemIndex];
            AreaFinishManager areaFinishManager = AreaFinishManagerList[itemIndex];//ucAreaFinishPalletElement.AreaFinishBase;

            UpdateAreaSelections(areaFinishManager);

            CanvasManager.BorderManager.AreaFinishBase = areaPalette[itemIndex].AreaFinishBase;

            if (CanvasManager.BorderManager.BorderGenerationState != CanvasLib.Borders.BorderGenerationState.OngoingBorderBuild)
            {
                int feet = areaFinishManager.AreaFinishBase.FixedWidthInches / 12;
                int inch = areaFinishManager.AreaFinishBase.FixedWidthInches % 12;

                this.nudFixedWidthFeet.Value = feet;
                this.nudFixedWidthInches.Value = inch;
            }

            if (areaFinishManager.MaterialsType == MaterialsType.Rolls)
            {
                OversUndersFormUpdate();
            }

            CanvasManager.LastLayoutArea = null;
        }

        public void LineFinishBaseList_ItemSelected(int itemIndex)
        {
            UpdateLineSelections(linePalette[itemIndex]);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        internal Point GetCursorPosition()
        {
            return Cursor.Position;
        }

        internal void SetCursorPosition(int x1, int y1)
        {
            Cursor.Position = new Point(x1, y1);
        }

        internal void SetCursorPosition(Point newPosition)
        {
            Cursor.Position = newPosition;
        }

        //public void SetupEditAreas(UCAreaFinishPaletteElement ucFinish)
        //{
        //    //this.pnlFinishColor.BackColor = ucFinish.FinishColor;
        //    //this.lblFinishName.Text = ucFinish.AreaName;
        //}

        //private void SetupEditLines(UCLineFinishPaletteElement ucLine)
        //{
        //    // uclSelectedLine.Invalidate();
        //}

        private void FloorMaterialEstimatorBaseForm_SizeChanged(object sender, EventArgs e)
        {
            setProjNameLabel();
            setTbcPageAreaSize();
            setCmdPaneAreaSize();
            setCmdPaneLineSize();
            setCmdPaneSeamSize();
            setAxAreaSize();
            setStatusBarSize();
            setToolbarSize();
            setCommandPanelSizes();
        }

        private void setProjNameLabel()
        {
            this.lblProjectName.Location = new Point(this.Width / 2, 2);
        }

        private void setToolbarSize()
        {

            this.lblSeparator.Width = Math.Max(this.Width - 1875, 32);
        }

        private void setTbcPageAreaSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;

            this.tbcPageAreaLine.Location = new Point(1, tlsBaseLocY);

            this.tbcPageAreaLine.Size = new System.Drawing.Size(pageAreaLineWidth, cntlSizeY);

            foreach (TabPage tp in this.tbcPageAreaLine.TabPages)
            {
                tp.Size = this.tbcPageAreaLine.Size;
            }

            areaPalette.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 64));
            linePalette.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 64));
            seamPalette.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 64));
        }

        private void setCommandPanelSizes()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int panlSizeY = formSizeY - cntlPanesBaseLocY - 128;
            int panlSizeX = 264 + 15;

            this.pnlAreaCommandPane.Size = new Size(panlSizeX, panlSizeY);
            this.pnlLineCommandPane.Size = new Size(panlSizeX, panlSizeY);
            this.pnlSeamCommandPane.Size = new Size(panlSizeX, panlSizeY);
        }

        private void setCmdPaneAreaSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeX = this.pnlAreaCommandPane.Width;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;
            int cntlLocnX = formSizeX - this.pnlAreaCommandPane.Width - 32;

            this.pnlAreaCommandPane.Location = new Point(cntlLocnX + 15, tlsBaseLocY);
            this.pnlAreaCommandPane.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
        }

        private void setCmdPaneLineSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeX = this.pnlLineCommandPane.Width;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;
            int cntlLocnX = formSizeX - this.pnlLineCommandPane.Width - 32;

            this.pnlLineCommandPane.Location = new Point(cntlLocnX + 15, tlsBaseLocY);
            this.pnlLineCommandPane.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
        }

        private void setCmdPaneSeamSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeX = this.pnlSeamCommandPane.Width;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;
            int cntlLocnX = formSizeX - this.pnlSeamCommandPane.Width - 32;

            this.pnlSeamCommandPane.Location = new Point(cntlLocnX + 15, tlsBaseLocY);
            this.pnlSeamCommandPane.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
        }

        Rectangle AxEffectiveAreaBounds;

        private void setAxAreaSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlLocX;

            if (btnFullScreen.Checked)
            {
                cntlLocX = 1;
            }

            else
            {
                cntlLocX = this.tbcPageAreaLine.Location.X + this.tbcPageAreaLine.Width + 1;
            }

            int cntlLocY = this.axDrawingControl.Location.Y;

            int cntlSizeX;

            if (btnFullScreen.Checked)
            {
                cntlSizeX = formSizeX - 56;
            }

            else
            {
                cntlSizeX = formSizeX - cntlLocX - this.pnlAreaCommandPane.Width - 32 - 48;
            }

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 128;

            //this.axDrawingControl.Visible = false;

            this.axDrawingControl.Location = new Point(cntlLocX + 15, tlsBaseLocY);
            this.axDrawingControl.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);


            //this.axDrawingControl.Visible = true;

            this.axDrawingControl.BringToFront();

            AxEffectiveAreaBounds = new Rectangle(this.axDrawingControl.Location, new Size(this.axDrawingControl.Size.Width, this.axDrawingControl.Height - 36));

            //this.pnlVisioPageControlCover.Location = new Point(cntlLocX + 4, cntlLocY + cntlSizeY - pnlVisioPageControlCover.Height - 4);
            //this.pnlVisioPageControlCover.BringToFront();

            this.tkbHScroll.Location = new Point(cntlLocX + 20, cntlLocY + cntlSizeY - 30);
            this.tkbVScroll.Location = new Point(cntlLocX + cntlSizeX + 3 + 15, cntlLocY + 2);

            this.tkbHScroll.BringToFront();
            this.tkbVScroll.BringToFront();

            this.tkbHScroll.Size = new Size(cntlSizeX - 12, 48);
            this.tkbVScroll.Size = new Size(48, cntlSizeY - 32);

            CanvasManager.SetZoom();
        }

        private void setStatusBarSize()
        {
            this.tssFiller1.Width = this.Width - this.tbcPageAreaLine.Width - this.tssLineSizeDecimal.Width - this.tssLineSizeEnglish.Width - 400;
            this.tssFiller2.Width = Math.Max(128, this.Width - this.tbcPageAreaLine.Width - this.tssLineSizeDecimal.Width - this.tssLineSizeEnglish.Width - tssFiller1.Width - STATUSBAR_ADJUSTMNENT_2);

            this.sssMainForm.BringToFront();
        }

        internal void SetLineLengthStatusStripDisplay(double length)
        {
            // Assume line length in inches.

            double feet = (int)Math.Floor(length / 12.0);

            double inch = length - (double)(feet * 12);

            this.tssLineSizeDecimal.Text = (length / 12.0).ToString("#,##0.00") + "'";
            this.tssLineSizeEnglish.Text = feet.ToString("#,##0") + "-" + inch.ToString("0.0") + '"';

            this.sssMainForm.BringToFront();
            this.sssMainForm.Refresh();
        }

        internal void ClearLineLengthStatusStripDisplay()
        {
            this.tssLineSizeDecimal.Text = string.Empty;
            this.tssLineSizeEnglish.Text = string.Empty;
        }

        //internal void SelectUCSeam(int selectedElement)
        //{
        //    this.areaPalette.SetSeamForSelectedFinish(this.SeamFinishBaseList[selectedElement]);
        //}

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == (int)WindowsMessage.WM_RBUTTONUP)
            {
                // Check to see if the measuring stick is selected

                if (Utilities.IsNotNull(this.CanvasManager.MeasuringStick))
                {
                    if (this.CanvasManager.MeasuringStick.IsVisible)
                    {
                        if (this.CanvasManager.MeasuringStick.IsSelected())
                        {
                            return true;
                        }
                    }
                }
            }

            if (this.txbRemnantWidthFeet.Focused || this.txbRemnantWidthInches.Focused)
            {
                return false;
            }

            bool rtrnCode = false;

#if true
            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
            {
                SetCursorForCurrentLocation();


#if DEBUG
                UpdateMousePositionDisplay();
#endif
                return rtrnCode;
            }

            if (m.Msg == (int)WindowsMessage.WM_MOUSEHOVER)
            {
                SetCursorForCurrentLocation();
#if DEBUG
                UpdateMousePositionDisplay();
#endif
                return rtrnCode;
            }


#endif

            if (m.Msg == (int)WindowsMessage.WM_MOUSEWHEEL)
            {
                if (!KeyboardUtils.CntlKeyPressed)
                {
                    return false;
                }

                Point cursorPosition = this.PointToClient(Cursor.Position);

                if (!AxEffectiveAreaBounds.Contains(cursorPosition))
                {
                    return false;
                }

                int wParam = (int)m.WParam;

                if (Utilities.IsNotNull(CanvasManager.PanAndZoomController))
                {
                    double incr = ((double)GlobalSettings.MouseWheelZoomInterval) * 0.01;

                    if (wParam < 0)
                    {
                        incr = -incr;
                    }
                    CanvasManager.PanAndZoomController.SetZoom(incr + SystemState.ZoomPercent);
                }

                return true;
            }

            if (m.Msg == (int)WindowsMessage.WM_KEYDOWN)
            {
                int keyVal = m.WParam.ToInt32();
                long keyValL = m.LParam.ToInt64();

                if (this.txbDoorTakeoutOther.Focused)
                {
                    return false;
                }

                CanvasManager.ProcessKeyDown(keyVal);

                return true;

            }

            if (m.Msg == (int)WindowsMessage.WM_KEYUP)
            {
                int keyVal = m.WParam.ToInt32();
                long keyValL = m.LParam.ToInt64();

                if (this.txbDoorTakeoutOther.Focused)
                {
                    return false;
                }

                CanvasManager.ProcessKeyUp(keyVal);

                return true;
            }

            return rtrnCode;
        }

        protected override void WndProc(ref Message m)
        {

            Point clientPos = PointToClient(Cursor.Position);

            //this.lblCursorLocation.Text = '(' + clientPos.X.ToString("#,##0.00") + ", " + clientPos.Y.ToString("#,##0.00") + ')';

            if (base.Bounds.Contains(Cursor.Position))
            {
                SetCursorForCurrentLocation();

                //ActivateForm();

                //this.lblWithinBounds.Text = "True";
            }

            else
            {

                //this.cursorTestForm.SetCursorForCurrentLocation();
                //this.lblWithinBounds.Text = "False";
            }

            base.WndProc(ref m);
        }

        public void SetCursorForCurrentLocation()
        {
            foreach (ICursorManagementForm form in CursorManager.CursorManagerFormList)
            {
                if (form.CursorWithinBounds())
                {
                    return;
                }
            }

            if (AxEffectiveAreaBounds.Contains(PointToClient(Cursor.Position)))
            {
                CursorManager.SetCursorToCross();
                this.lblCursorType.Text = "Cross";

            }

            else
            {
                CursorManager.SetCursorToArrow();
                this.lblCursorType.Text = "Arrow";
            }
        }

#if DEBUG
        public void UpdateMousePositionDisplay()
        {
            //this.tlsMouseXY.Text = '(' + Cursor.Position.X.ToString("#,##0") + ',' + Cursor.Position.Y.ToString("#,##0") + ')';
            this.tlsMouseXY.Text = "X : " + Cursor.Position.X.ToString("#,##0") + "     Y : " + Cursor.Position.Y.ToString("#,##0");
        }
#endif
        public void BtnMeasuringStick_ButtonClick(object sender, EventArgs e)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { sender, e });
#endif
            try
            {
                if (CanvasManager.MeasuringStick.IsVisible)
                {
                    CanvasManager.MeasuringStick.Hide();

                    this.btnMeasuringStick.Image = MeasuringStickNotSelectedImage;
                }
                else
                {
                    CanvasManager.MeasuringStick.Show();

                    this.btnMeasuringStick.Image = MeasuringStickSelectedImage;
                }
            }
            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in FloorMaterialEstimatorBaseForm:btnMeasuringStick_ButtonClick", ex, 1, true);

                //Logger.LogError("Exception thrown:\n" + ex.Message, MessageSeparator.None);
                //Logger.LogError("Stack trace:\n" + ex.StackTrace, MessageSeparator.DoubleLine);
            }
            finally
            {
                //this.btnMeasuringStick = CanvasManager.MeasuringStick.IsVisible;
            }
        }

        private void btnMeasuringStick_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { sender, e });

            try
            {
                int previousIndex = this.stencilHelper.SelectecdStencilIndex;

                this.stencilHelper.SelectMeasuringStickStencil((e.ClickedItem as ToolStripItem).Text);
                CanvasManager.MeasuringStick.RulerStencil = this.stencilHelper.SelectecdStencil.Stencil;

                SelectMeasuringStickMenuItem(this.stencilHelper, previousIndex);

                if (!CanvasManager.MeasuringStick.IsVisible)
                {
                    CanvasManager.MeasuringStick.Show();

                    this.btnMeasuringStick.Image = MeasuringStickSelectedImage;
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Exception thrown in FloorMaterialEstimatorBaseForm:btnMeasuringStick_DropDownItemClicked", ex, 1, true);

                //Logger.LogError("Exception thrown:\n" + ex.Message, MessageSeparator.None);
                //Logger.LogError("Stack trace:\n" + ex.StackTrace, MessageSeparator.DoubleLine);
            }
            finally
            {
                //this.btnMeasuringStick.Checked = CanvasManager.MeasuringStick.IsVisible;
            }
        }

        private void DestroyMeasuringStick()
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif

            try
            {
                CanvasManager.MeasuringStick.Destroy();
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("FloorMaterialEstimatorBaseForm:DestoryMeasuringStick throws an exception.", ex, 1, true);
            }
            finally
            {
                //this.btnMeasuringStick.Checked = CanvasManager.MeasuringStick.IsVisible;
            }
        }

        public void ShapeAddedEventHandler(Microsoft.Office.Interop.Visio.Shape shape)
        {
            if (CanvasLib.MeasuringStick.MeasuringStick.IsMeasuringStickShape(shape))
            {
                shape.Delete();
                axDrawingControl.Document.PurgeUndo();
            }
        }

        public void EnsureOversUndersForm()
        {
            if (OversUndersForm is null)
            {
                OversUndersForm = new FloorMaterialEstimator.OversUndersForm.OversUndersForm(this);

                OversUndersForm.FormClosed += UndersOversForm_FormClosed;
            }
        }

        public void EnableOversUndersButton(bool enable)
        {
            this.btnOversUnders.Enabled = enable;
        }

        private int ConvertAreaToSquareFeet(double area)
        {
            double areaSqFt = (area * Math.Pow(this.CurrentPage.DrawingScaleInInches, 2)) / 144;
            return (int)Math.Round(areaSqFt);
        }

        public void AreaSelectionChangedEventHandler(object sender, List<Shape> e)
        {
            if (this.CurrentPage.ScaleHasBeenSet)
            {
                double highlightedArea = 0.0;

                foreach (Shape shape in e)
                {
                    double shapeArea = VisioInterop.GetShapeArea(shape);
                    highlightedArea += shapeArea;
                }

                if (highlightedArea > 0.0)
                {
                    this.tssHighlightedArea.Text = string.Format("{0:N0} s.f.", ConvertAreaToSquareFeet(highlightedArea));
                }
                else
                {
                    this.tssHighlightedArea.Text = string.Empty;
                }
            }
        }

        public void ScaleStateChangeHandler(bool scaleHasBeenSet)
        {
            if (scaleHasBeenSet)
            {
                this.tssHighlightedArea.Text = string.Empty;
            }
            else
            {
                this.tssHighlightedArea.Text = "<No Scale Set>";
            }
        }

        private void btnLineModeDeleteLinesSelectAll_Click(object sender, EventArgs e)
        {
            this.ckbDeleteSingleLines.Checked = true;
            this.ckbDeleteDoubleLines.Checked = true;
            this.ckbDeleteAreaModeLines.Checked = true;
        }

        private void btnLineModeDeleteLInesSelectNone_Click(object sender, EventArgs e)
        {
            this.ckbDeleteSingleLines.Checked = false;
            this.ckbDeleteDoubleLines.Checked = false;
            this.ckbDeleteAreaModeLines.Checked = false;
        }

        private void btnLineModeDeleteLines_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Confirm deletion of selected lines.", "Confirm Deletion", MessageBoxButtons.OKCancel);

            if (dr != DialogResult.OK)
            {
                return;
            }

            CanvasManager.DeleteSelectedLines(this.ckbDeleteSingleLines.Checked, this.ckbDeleteDoubleLines.Checked, this.ckbDeleteAreaModeLines.Checked);

            BtnLayoutLine1XMode_Click(null, null); // Reset to 1X mode after deletion of lines.
        }

    }
}