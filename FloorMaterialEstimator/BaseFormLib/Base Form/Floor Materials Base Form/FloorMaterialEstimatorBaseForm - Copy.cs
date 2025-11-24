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
    using CanvasLib.Design_States_and_Modes;

    using SettingsLib;

    using System;
    using System.IO;
    using System.Drawing;
    using System.Windows.Forms;

    using CanvasLib;
    using FinishesLib;
    using Utilities;
    using System.Reflection;
    using CanvasLib.Scale_Line;

    //using System.Windows.Input;

    public partial class FloorMaterialEstimatorBaseForm : Form, IMessageFilter
    {
        private int cntlPanesBaseLocY = 32;
        private int pageAreaLineWidth = 296;

        private int tlsBaseLocY;

        public string CurrentProjectName;
        public string CurrentProjectPath;

        public string CurrentProjectFullPath;
    
        public string CurrentDrawingName;
        public string CurrentDrawingPath;

        public bool CurrentProjectChanged;

        public const int tbcAreaModeIndex = 0;
        public const int tbcLineModeIndex = 1;
        public const int tbcSeamModeIndex = 2;

        private DrawingMode drawingMode = DrawingMode.Default;

        public CanvasPage CurrentPage => CanvasManager.CurrentPage;

        //public UCLegend UCLegend { get; set; } = new UCLegend();

        public DrawingMode DrawingMode
        {
            get
            {
                return drawingMode;
            }

            set
            {
#if DEBUG
                tlsDrawingMode.Text = "Drawing Mode: " + value.ToString();
#endif
                drawingMode = value;

                if (value == DrawingMode.ScaleLine)
                {
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = true;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.TapeMeasure)
                {
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = true;

                    return;
                }

                if (value == DrawingMode.Default)
                {
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.Line1X || value == DrawingMode.Line2X || value == DrawingMode.LineDuplicate)
                {
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.Rectangle)
                {
                    btnDrawRectangle.Checked = true;
                    btnDrawPolyline.Checked = false;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.Polyline)
                {
                    btnDrawRectangle.Checked = false;
                    btnDrawPolyline.Checked = true;
                    btnSetCustomScale.Checked = false;
                    btnTapeMeasure.Checked = false;

                    return;
                }

                if (value == DrawingMode.BorderGeneration)
                {
                    return;
                }
            }
        }

        public UCAreaFinishPallet areaPallet;

        public UCLineFinishPallet linePallet;

        public UCSeamFinishPallet seamPallet;

        public AreaFinishBaseList AreaFinishBaseList;

        public LineFinishBaseList LineFinishBaseList;

        public SeamFinishBaseList SeamFinishBaseList;

        public CanvasManager.CanvasManager CanvasManager;

        public Bitmap CustomScaleLineColoredImage;
        public Bitmap CustomScaleLineUncoloredImage;

        public Bitmap ExpandCanvasImage;
        public Bitmap ContractCanvasImage;

        public Bitmap RedoSeamsOffImage;
        public Bitmap RedoSeamsOnImage;

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



        internal void MoveLineToSelectedLineType(string nameID)
        {
            if (string.IsNullOrEmpty(nameID))
            {
                return;
            }

            linePallet.MoveLineToSelectedLineType(nameID);
        }


        internal void MoveLineToSelectedLineType(CanvasDirectedLine line)
        {
            if (line is null)
            {
                return;
            }

            linePallet.MoveLineToSelectedLineType(line);
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

                if (!(CanvasManager.LineModeStartMarker is null))
                {
                    CanvasManager.LineModeStartMarker.Delete();
                    CanvasManager.LineModeStartMarker = null;
                }

                if (value == LineDrawingMode.Mode1X)
                {
                    this.btnLayoutLine1XMode.BackColor = Color.Orange;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                    this.btnLayoutLineActivate.BackColor = SystemColors.Control;

                    this.btnLayoutLineActivate.Text = "Activate";

                    this.btnLayoutLineDuplicate.Enabled = false;

                    this.btnLayoutLineJump.Enabled = true;
                    this.btnLayoutLineJump.BackColor = Color.Orange;

                    layoutLineMode = LineDrawingMode.Mode1X;

                    return;
                }

                if (value == LineDrawingMode.Mode2X)
                {
                    this.btnLayoutLine1XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLine2XMode.BackColor = Color.Orange;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                    this.btnLayoutLineActivate.BackColor = SystemColors.Control;

                    this.btnLayoutLineActivate.Text = "Activate";

                    layoutLineMode = LineDrawingMode.Mode2X;

                    this.btnLayoutLineJump.Enabled = false;
                    this.btnLayoutLineJump.BackColor = SystemColors.Control;

                    return;
                }

                if (value == LineDrawingMode.Duplicate)
                {
                    this.btnLayoutLine1XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLineDuplicate.BackColor = Color.Orange;
                    this.btnLayoutLineActivate.BackColor = SystemColors.Control;

                    this.btnLayoutLineActivate.Text = "Activate";

                    this.btnLayoutLineJump.Enabled = false;
                    this.btnLayoutLineJump.BackColor = SystemColors.Control;

                    layoutLineMode = LineDrawingMode.Duplicate;

                    return;
                }

                if (value == LineDrawingMode.TakeoutArea)
                {
                    this.btnLayoutLine1XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                    this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                    this.btnLayoutLineActivate.BackColor = Color.Orange;

                    this.btnLayoutLineActivate.Text = "Deactivate";

                    this.btnLayoutLineJump.Enabled = false;
                    this.btnLayoutLineJump.BackColor = SystemColors.Control;

                    layoutLineMode = LineDrawingMode.TakeoutArea;

                    return;
                }

                this.btnLayoutLine1XMode.BackColor = SystemColors.Control;
                this.btnLayoutLine2XMode.BackColor = SystemColors.Control;
                this.btnLayoutLineDuplicate.BackColor = SystemColors.Control;
                this.btnLayoutLineActivate.BackColor = SystemColors.Control;
            }
        }

        public bool TakeoutAreaMode => this.btnLayoutAreaTakeout.BackColor == Color.Orange;

        public bool TakeoutAreaAndFillMode => this.btnLayoutAreaTakeOutAndFill.BackColor == Color.Orange;

        public bool AreaModeCounterMode => this.btnCounters.Checked;
       
        public bool FixedWidthMode => this.ckbFixedWidth.Checked;

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

        internal void UpdateAreaSelections(UCAreaFinishPalletElement ucFinish)
        {
            SetupEditAreas(ucFinish);

            if (this.DesignState == DesignState.Seam)
            {
                SetupSeamModeDesignState(ucFinish);
            }

            
            if (this.DesignState == DesignState.Area)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in CurrentPage.AreaDesignStateSelectedAreas())
                {
                    CanvasManager.ProcessEditAreaModeActionChangeShapeToFinish(canvasLayoutArea, ucFinish);
                    CurrentPage.SetAreaDesignStateAreaSelectionStatus(canvasLayoutArea, false);
                }
            }
        }

        internal void UpdateLineSelections(UCLineFinishPalletElement ucLine)
        {
            SetupEditLines(ucLine);

            this.uclSelectedLine.Invalidate();

            CanvasManager.LineFinishSelectionChanged(ucLine);
        }

        internal void UpdateSeamDefinition(SeamFinishBase finishSeamBase)
        {
            
        }

        public FloorMaterialEstimatorBaseForm()
        {
            try
            {
                runFloorMaterialEstimatorBaseForm();
            }

            catch (Exception ex)
            {
                Logger.LogError("Exception thrown:\n" + ex.Message);
                Logger.LogError("Stack trace:\n" + ex.StackTrace);

                ManagedMessageBox.Show("This application has thrown an exception:\n" + ex.Message + "\n\nCheck log file for more details.");
            }
        }

        private void runFloorMaterialEstimatorBaseForm()
        { 
            InitializeComponent();

            tlsBaseLocY = tlsMainToolStrip.Location.Y + tlsMainToolStrip.Height + 2;
            
            this.tbcPageAreaLine.Location = new Point(0, cntlPanesBaseLocY);

            GlobalSettings.Initialize(Program.AppConfig);
            ShortcutSettings.Initialize(Program.AppConfig);

            GuidMaintenance.ClearGuids();

            CanvasManager = new CanvasManager.CanvasManager(this, this.axDrawingControl);

            areaPallet = new UCAreaFinishPallet();
            linePallet = new UCLineFinishPallet();
            seamPallet = new UCSeamFinishPallet();

            loadFinishPalletsFromDefaults();

            this.tbpAreas.Controls.Add(areaPallet);
            this.tbpLines.Controls.Add(linePallet);
            this.tbpSeams.Controls.Add(seamPallet);

            SetupEditAreas(selectedAreaFinish);
            SetupEditLines(selectedLineFinish);

            this.btnAreaDesignState.Checked = true;
            this.btnLineDesignState.Checked = false;

            DrawingMode = DrawingMode.Default;

            this.btnFilterAreas.Click += BtnFilterAreas_Click;
            this.btnFilterLines.Click += BtnFilterLines_Click;
            this.btnCounters.Click += BtnCounters_Click;

            this.btnLineDesignStateEditMode.Click += BtnLineDesignStateEditMode_Click;
            this.btnLineDesignStateLayoutMode.Click += BtnLineDesignStateLayoutMode_Click;
            this.btnSummaryReport.Click += BtnSummaryReport_Click;
            this.btnShowFieldGuides.Click += BtnShowFieldGuides_Click;
            this.btnHideFieldGuides.Click += BtnHideFieldGuides_Click;
            this.btnDeleteFieldGuides.Click += BtnDeleteFieldGuides_Click;

            this.btnShowFieldGuides.Checked = true;
            this.btnHideFieldGuides.Checked = false;
                     
            ClearLineLengthStatusStripDisplay();

            this.cccAreaMode.Init();
            this.cccLineMode.Init();

            this.pnlAreaCommandPane.BringToFront();

            this.btnSettings.Click += BtnGlobalSettings_Click;

            tlsMainToolStrip.Renderer = new CustomToolbarButtonRenderer();


            this.Cursor = Cursors.Arrow;

            this.tbcPageAreaLine.SelectedIndexChanged += TbcPageAreaLine_SelectedIndexChanged;

            this.nudFixedWidthFeet.Init("{0}'");
            this.nudFixedWidthInches.Init("{0}\"");
            this.ckbFixedWidth.CheckedChanged += CkbFixedWidth_CheckedChanged;
           
            this.btnAbout.Click += BtnAbout_Click;
            setupSeamTab();

            this.tbcPageAreaLine.Selecting += TbcPageAreaLine_Selecting;

            KeyPress += FloorMaterialEstimatorBaseForm_KeyPress;
            KeyDown += FloorMaterialEstimatorBaseForm_KeyDown;
            KeyUp += FloorMaterialEstimatorBaseForm_KeyUp;
            uclSelectedLine.Init(this.linePallet);

            CanvasManager.ShowGrid = GlobalSettings.ShowGrid;
            CanvasManager.ShowRulers = GlobalSettings.ShowRulers;
            CanvasManager.ShowPanAndZoom = GlobalSettings.ShowPanAndZoom;

            CanvasManager.CurrentPage.VisioPage.AutoSize = false;

            this.btnShowSeamingTool.Click += new System.EventHandler(CanvasManager.BtnShowSeamingTool_Click);
            this.btnCenterSeamingToolInView.Click += btnCenterSeamingToolInView_Click;
            this.KeyPreview = true;

            this.FormClosing += FloorMaterialEstimatorBaseForm_FormClosing;


            //((Control)this.axDrawingControl).MouseEnter += FloorMaterialEstimatorBaseForm_MouseEnter;
            ((Control)this.axDrawingControl).MouseLeave += FloorMaterialEstimatorBaseForm_MouseLeave;

            //this.axDrawingControl.MouseMoveEvent += AxDrawingControl_MouseMoveEvent;

            this.clrEditAreaModeButtons();
            this.clrEditLineModeButtons();
            this.setLayoutLineModeButtons();

            //CanvasManager.NominalZoom = 1;
            CanvasManager.SetZoom(1);

            this.ddbZoomPercent.Text = "100%";

            //pnlAreaCommandPane.MouseEnter += PnlAreaCommandPane_MouseEnter;


            this.tkbHScroll.BringToFront();
            this.tkbVScroll.BringToFront();

            this.ucSeamsView1.Size = this.ucAreasView.Size;
            this.ucSeamsView1.Location = this.ucSeamsView1.Location;

            //this.Controls.Add(UCLegend);

            //UCLegend.Init(CanvasManager.Window, CanvasManager.CurrentPage, AreaFinishBaseList);

            FloorMaterialEstimatorBaseForm_SizeChanged(null, null);

            this.SizeChanged += FloorMaterialEstimatorBaseForm_SizeChanged;

            Assembly asm = Assembly.GetExecutingAssembly();

            CustomScaleLineColoredImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.CustomScaleColored.png"));
            CustomScaleLineUncoloredImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.CustomWarning.png"));

            ExpandCanvasImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.HidePanes.png"));
            ContractCanvasImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.ShowPanes.png"));

            RedoSeamsOffImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.RedoSeamsOff.png"));
            RedoSeamsOnImage = new Bitmap(asm.GetManifestResourceStream("FloorMaterialEstimator.Image_Resources.RedoSeamsOn.png"));

#if DEBUG
            // Set up design state on status bar

            string designStateText = "Design State: " + DesignState.ToString();

            if (DesignState == DesignState.Area)
            {
                designStateText += "(" + AreaMode.ToString() + ")";
            }

            else if (DesignState == DesignState.Line)
            {
                designStateText += "(" + LineMode.ToString() + ")";
            }

            this.tlsDesignState.Text = designStateText;

            this.tlsDrawingMode.Text = "Drawing Mode: " + DrawingMode.ToString();
            this.tlsDrawingShape.Text = "Drawing shape: " + CanvasManager.DrawingShape.ToString();

            currentPage.Legend = new Legend(CanvasManager.Window, currentPage, AreaFinishBaseList);

            currentPage.Legend.LegendShowLocation = LegendLocation.None;

            currentPage.SetDefaultScale(GlobalSettings.DefaultDrawingScaleInInches);

            //this.btnCompleteShapeByIntersection.MouseHover += BtnCompleteShapeByIntersection_MouseHover;

            this.Load += FloorMaterialEstimatorBaseForm_Load;
            this.Shown += FloorMaterialEstimatorBaseForm_Shown;
#if DEBUG
            UpdateMousePositionDisplay();
#endif

         
            this.btnDebug.Visible = true;

#endif
        }

        private void FloorMaterialEstimatorBaseForm_Shown(object sender, EventArgs e)
        {

            if (!CurrentPage.ScaleHasBeenSet && GlobalSettings.ShowSetScaleReminder)
            {
                SetScaleWarningForm setScaleWarningForm = new SetScaleWarningForm();

                setScaleWarningForm.ShowDialog();
            }
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
            this.SetCursorForCurrentLocation();

            Application.DoEvents();
        }

        private void FloorMaterialEstimatorBaseForm_MouseEnter(object sender, EventArgs e)
        {
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
        
        private void loadFinishPalletsFromDefaults()
        {
            string areaFinishInitPath = string.Empty;
            string lineFinishInitPath = string.Empty;
            string finishSeamFileInitPath = string.Empty;

            if (Program.AppConfig.ContainsKey("areafinishinitpath"))
            {
                areaFinishInitPath = Path.Combine(Program.BaseDataFolder, Program.AppConfig["areafinishinitpath"]);
            }

            if (Program.AppConfig.ContainsKey("linefinishinitpath"))
            {
                lineFinishInitPath = Path.Combine(Program.BaseDataFolder, Program.AppConfig["linefinishinitpath"]);
            }

            if (Program.AppConfig.ContainsKey("seamlinefinishinitpath"))
            {
                finishSeamFileInitPath = Path.Combine(Program.BaseDataFolder, Program.AppConfig["seamlinefinishinitpath"]);
            }

            SeamFinishBaseList = new SeamFinishBaseList();
            SeamFinishBaseList.Load(finishSeamFileInitPath);

            LineFinishBaseList = new LineFinishBaseList();
            LineFinishBaseList.Load(lineFinishInitPath);

            AreaFinishBaseList = new AreaFinishBaseList();
            AreaFinishBaseList.Load(areaFinishInitPath, SeamFinishBaseList);

            areaPallet.Init(this, CanvasManager, AreaFinishBaseList);
            linePallet.Init(this, CanvasManager, LineFinishBaseList);
            seamPallet.Init(this, CanvasManager, AreaFinishBaseList, SeamFinishBaseList);

            areaPallet.FinishChanged += AreaPallet_FinishChanged;

            this.AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
            this.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

            Size pageAreaSize = tbpLines.Size;
            linePallet.Size = new Size(linePallet.Width, 800);

            linePallet.Refresh();

        }

        public void AreaPallet_FinishChanged(UCAreaFinishPalletElement ucAreaFinish)
        {
            UpdateAreaSelections(ucAreaFinish);
        }

        public void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            UpdateAreaSelections(areaPallet[itemIndex]);
        }

        public void LineFinishBaseList_ItemSelected(int itemIndex)
        {
            UpdateLineSelections(linePallet[itemIndex]);
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

        public void SetupEditAreas(UCAreaFinishPalletElement ucFinish)
        {
            this.pnlFinishColor.BackColor = ucFinish.FinishColor;
            this.lblFinishName.Text = ucFinish.AreaName;
        }

        private void SetupEditLines(UCLineFinishPalletElement ucLine)
        {
            uclSelectedLine.Invalidate();
        }

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

            areaPallet.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 32));
            linePallet.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 32));
            seamPallet.SetSize(new System.Drawing.Size(this.tbcPageAreaLine.Width - 8, this.tbcPageAreaLine.Height - 32));
        }

        private void setCommandPanelSizes()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int panlSizeY = formSizeY - cntlPanesBaseLocY - 128;
            int panlSizeX = 274;

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

            this.pnlAreaCommandPane.Location = new Point(cntlLocnX, tlsBaseLocY);
            this.pnlAreaCommandPane.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
        }

        private void setCmdPaneLineSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeX = this.pnlLineCommandPane.Width;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;
            int cntlLocnX = formSizeX - this.pnlLineCommandPane.Width - 32;

            this.pnlLineCommandPane.Location = new Point(cntlLocnX, tlsBaseLocY);
            this.pnlLineCommandPane.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
        }

        private void setCmdPaneSeamSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeX = this.pnlSeamCommandPane.Width;

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 64;
            int cntlLocnX = formSizeX - this.pnlSeamCommandPane.Width - 32;

            this.pnlSeamCommandPane.Location = new Point(cntlLocnX, tlsBaseLocY);
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
                cntlSizeX = formSizeX  - 56;
            }

            else
            {
                cntlSizeX = formSizeX - cntlLocX - this.pnlAreaCommandPane.Width - 32 - 48;
            }

            int cntlSizeY = formSizeY - cntlPanesBaseLocY - 128 ;

            //this.axDrawingControl.Visible = false;

            this.axDrawingControl.Location = new Point(cntlLocX, tlsBaseLocY);
            this.axDrawingControl.Size = new System.Drawing.Size(cntlSizeX, cntlSizeY);
            

            //this.axDrawingControl.Visible = true;

            this.axDrawingControl.BringToFront();

            AxEffectiveAreaBounds = new Rectangle(this.axDrawingControl.Location, new Size(this.axDrawingControl.Size.Width, this.axDrawingControl.Height - 36));

            this.pnlVisioPageControlCover.Location = new Point(cntlLocX + 4, cntlLocY + cntlSizeY - pnlVisioPageControlCover.Height - 4);
            this.pnlVisioPageControlCover.BringToFront();

            this.tkbHScroll.Location = this.pnlVisioPageControlCover.Location;
            this.tkbVScroll.Location = new Point(cntlLocX + cntlSizeX + 3, cntlLocY + 2);

            this.tkbHScroll.BringToFront();
            this.tkbVScroll.BringToFront();

            this.tkbHScroll.Size = new Size(cntlSizeX - 12, 48);
            this.tkbVScroll.Size = new Size(48, cntlSizeY - 32);

            CanvasManager.SetZoom();
        }

        private void setStatusBarSize()
        {
            this.tssFiller.Width = this.Width - this.tbcPageAreaLine.Width - this.tssLineSizeDecimal.Width - this.tssLineSizeEnglish.Width - 400;

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
        //    this.areaPallet.SetSeamForSelectedFinish(this.SeamFinishBaseList[selectedElement]);
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

            if (m.Msg == (int) WindowsMessage.WM_MOUSEHOVER)
            {
                SetCursorForCurrentLocation();
#if DEBUG
                UpdateMousePositionDisplay();
#endif
                return rtrnCode;
            }

       
#endif
            if (m.Msg == (int)WindowsMessage.WM_KEYDOWN)
            {
                int keyVal = m.WParam.ToInt32();
                long keyValL = m.LParam.ToInt64();

                CanvasManager.ProcessKeyDown(keyVal);

                return true;

            }

            if (m.Msg == (int)WindowsMessage.WM_KEYUP)
            {
                int keyVal = m.WParam.ToInt32();
                long keyValL = m.LParam.ToInt64();

                CanvasManager.ProcessKeyUp(keyVal);

                return true;
            }

            return rtrnCode;
        }

        protected override void WndProc(ref Message m)
        {
#if false
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
#endif
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
            this.tlsMouseXY.Text = '(' + Cursor.Position.X.ToString("#,##0") + ',' + Cursor.Position.Y.ToString("#,##0") + ')';
        }

        private void nudFixedWidthInches_ValueChanged(object sender, EventArgs e)
        {

        }

        private void nudFixedWidthFeet_ValueChanged(object sender, EventArgs e)
        {

        }



#endif
    }
}
