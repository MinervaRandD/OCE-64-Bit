//-------------------------------------------------------------------------------//
// <copyright file="UCLine.cs" company="Bruun Estimating, LLC">                  // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Finish_Controls
{
 
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using FinishesLib;
    using Graphics;
    using Globals;
    //using FloorMaterialEstimator.CanvasManager;
    

    public partial class UCLineFinishPaletteElement : UserControl
    {

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        private DesignState designState => baseLineFinishPalette.DesignState;

        private SeamMode seamMode => baseLineFinishPalette.SeamMode;

        public string Guid => LineFinishBase.Guid;
       
        public LineFinishBase LineFinishBase;

        public string LineName => LineFinishBase.LineName;

        public short VisioDashType => (short) LineFinishBase.VisioLineType;
     
        public double LineWidthInPts => LineFinishBase.LineWidthInPts;
       
        public Color LineColor => LineFinishBase.LineColor;

        //public IEnumerable<DoorTakeout> DoorTakeoutList => LineFinishManager.DoorTakeoutList;

        //public DesignState LineAreaMode => baseLineFinishPalette.LineAreaMode;
        
        public bool Selected
        {
            get
            {
                return LineFinishBase.Selected;
            }

            set
            {
                LineFinishBase.Selected = value;

                updateSelectedFormat();

            }
        }
        
        #region Line Color

        private bool baseLineColorChanged(Color currColor, Color updtColor)
        {
            return currColor.R != updtColor.R ||
                    currColor.G != updtColor.G ||
                    currColor.B != updtColor.B;
        }

        //private void updateBaseLineColor(Color updtColor)
        //{
        //    visioLineColorFormula =
        //        string.Format("THEMEGUARD(RGB({0},{1},{2}))", updtColor.R, updtColor.G, updtColor.B);

        //    foreach (CanvasDirectedLine l in LineFinishManager.CanvasDirectedLines)
        //    {
        //        l.SetBaseLineColor(visioLineColorFormula);
        //    }
        //}

        private string _visioLineColorFormula = "THEMEGUARD(RGB(0,0,0))";

        public string visioLineColorFormula
        {
            get
            {
                return _visioLineColorFormula;
            }

            set
            {
                _visioLineColorFormula = value;
            }
        }

        #endregion

        #region Line Width

       
        private string product = string.Empty;

        public string Product
        {
            get
            {
                return product;
            }

            set
            {
                product = value;
            }
        }

        private string notes = string.Empty;

        public string Notes
        {
            get
            {
                return notes;
            }

            set
            {
                notes = value;
            }
        }
        //private void updateBaseLineWidth(double updtWidthInPts)
        //{       
        //    foreach (CanvasDirectedLine l in LineFinishManager.CanvasDirectedLines)
        //    {
        //        l.SetBaseLineWidth((double)updtWidthInPts);
        //    }
        //}

        private string _visioLineWidthFormula = "4 pt";

        public string visioLineWidthFormula
        {
            get
            {
                return _visioLineWidthFormula;
            }

            set
            {
                _visioLineWidthFormula = value;
            }
        }

        #endregion

        #region Line Style
       

        //private bool baseLineStyleChanged(short currStyle, short updtStyle)
        //{
        //    return currStyle != updtStyle;
        //}

        //private void updateBaseLineStyle(short visioLineStyle)
        //{
        //    //VisioDashType = visioLineStyle;
            
        //    foreach (CanvasDirectedLine l in LineFinishManager.CanvasDirectedLines)
        //    {
        //        l.SetBaseLineStyle(visioLineStyleFormula);
        //    }
        //}

        //private string _visioLineStyleFormula = "1";

        public string visioLineStyleFormula
        {
            get
            {
                return VisioDashType.ToString();
            }

            //set
            //{
            //    _visioLineStyleFormula = value;
            //}
        }

        #endregion

        //public LineFinishManager LineFinishManager { get; set; } = null;

        // Control of the display of lines, depending on design state is done by placing lines on layers and then
        // making those layers visible or invisibile depending on the current design state.

        //public GraphicsLayerBase AreaDesignStateLayer => LineFinishManager.AreaDesignStateLayer;
       
        //public GraphicsLayerBase LineDesignStateLayer => LineFinishManager.LineDesignStateLayer;
        
        //public GraphicsLayerBase SeamDesignStateLayer => LineFinishManager.SeamDesignStateLayer;
        
        //public GraphicsLayerBase RemnantSeamDesignStateLayer => LineFinishManager.RemnantSeamDesignStateLayer;
        
        public bool Filtered => LineFinishBase.Filtered;
       
        public UCLineFinishPalette baseLineFinishPalette;

        //public CanvasManager canvasManager
        //{
        //    get
        //    {
        //        return baseLineFinishPalette.CanvasManager;
        //    }
        //}

        private int positionOnPalette = -1;

        public int PositionOnPalette
        {
            get
            {
                return positionOnPalette;
            }

            set
            {
                positionOnPalette = value;

                this.lblShortCutNumber.Text = (value+1).ToString();
            }
        }

        public UCLineFinishPaletteElement(GraphicsWindow window, GraphicsPage page, UCLineFinishPalette baseLinePalette, LineFinishBase lineLineBase)
        {
            InitializeComponent();

            Window = window;

            Page = page;

            this.baseLineFinishPalette = baseLinePalette;

            this.LineFinishBase = lineLineBase;

            this.lblLineName.Text = this.LineFinishBase.LineName;

            //this.LineFinishManager = new LineFinishManager(Window, Page, baseLinePalette.BaseForm.LineFinishBaseList, lineLineBase);

            //this.lblLength.Visible = false;

            //updateBaseLineColor(this.LineFinishBase.LineColor);
            //updateBaseLineStyle((short) this.LineFinishBase.VisioLineType);
            //updateBaseLineWidth(this.LineFinishBase.LineWidthInPts);

            this.Click += UCLineFinishPaletteElement_Click;

            this.lblLineName.Click += LblLineName_Click;
            this.lblShortCutNumber.Click += LblShortCutNumber_Click;
            this.lblPerimDecimal.Click += LblPerimDecimal_Click;
            this.lblSelected.Click += LblSelected_Click;
            this.Paint += UCLine_Paint;

            this.LineFinishBase.LineNameChanged += LineFinishBase_LineNameChanged;
            this.LineFinishBase.LineColorChanged += LineFinishBase_LineColorChanged;
            this.LineFinishBase.LineTypeChanged += LineFinishBase_LineTypeChanged;
            this.LineFinishBase.LengthChanged += LineFinishBase_LengthChanged;
            this.LineFinishBase.LineWidthChanged += LineFinishBase_LineWidthChanged;
            this.LineFinishBase.FilteredChanged += LineFinishBase_FilteredChanged;
        }

        public void Delete()
        {

            this.Click -= UCLineFinishPaletteElement_Click;

            this.lblLineName.Click -= LblLineName_Click;
            this.lblShortCutNumber.Click -= LblShortCutNumber_Click;
            this.lblPerimDecimal.Click -= LblPerimDecimal_Click;
            this.lblSelected.Click -= LblSelected_Click;
            this.Paint -= UCLine_Paint;

            this.LineFinishBase.LineNameChanged -= LineFinishBase_LineNameChanged;
            this.LineFinishBase.LineColorChanged -= LineFinishBase_LineColorChanged;
            this.LineFinishBase.LineTypeChanged -= LineFinishBase_LineTypeChanged;
            this.LineFinishBase.LengthChanged -= LineFinishBase_LengthChanged;
            this.LineFinishBase.LineWidthChanged -= LineFinishBase_LineWidthChanged;
            this.LineFinishBase.FilteredChanged -= LineFinishBase_FilteredChanged;

           // this.LineFinishManager.Delete();
        }


        private void LineFinishBase_FilteredChanged(LineFinishBase LineFinishBase, bool filtered)
        {
            //LineFinishManager.SetLineState(designState, seamMode, this.Selected);

            if (filtered)
            {
                this.BackColor = Color.LightGray;
            }

            else
            {
                this.BackColor = Color.White;
            }
        }

        private void LineFinishBase_LineWidthChanged(LineFinishBase LineFinishBase, double lineWidthInPts)
        {
            //LineFinishManager.SetLineState(designState, seamMode, Selected);
            Invalidate();
        }

        private void LineFinishBase_LengthChanged(LineFinishBase LineFinishBase, double lengthInInches)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet, lengthInInches);
        }

        private void setupTallyDisplay(bool scaleHasBeenSet, double lengthInInches)
        {
            if (scaleHasBeenSet)
            {
                this.lblPerimDecimal.Text = (lengthInInches / 12.0).ToString("#,##0.0") + "   l.f.";
                //this.lblLength.Visible = true;
            }

            else
            {
                this.lblPerimDecimal.Text = string.Empty;
                //this.lblLength.Visible = false;
            }
        }

        private void LineFinishBase_LineNameChanged(LineFinishBase lineFinishBase, string lineName)
        {
            this.lblLineName.Text = lineName;
        }

        private void LineFinishBase_LineColorChanged(LineFinishBase LineFinishBase, Color lineColor)
        {
            //LineFinishManager.SetLineState(designState, seamMode, Selected);
            Invalidate();
        }

        private void LineFinishBase_LineTypeChanged(LineFinishBase lineFinishBase, int lineType)
        {
            //LineFinishManager.SetLineState(designState, seamMode, Selected);
            Invalidate();
        }


        #region Events

        public delegate void ControlClickedHandler(UCLineFinishPaletteElement sender);

        public event ControlClickedHandler ControlClicked;

        private void UCFinish_Click(object sender, EventArgs e)
        {
            if (ControlClicked != null)
            {
                ControlClicked.Invoke(this);
            }
        }

        private void UCLineFinishPaletteElement_Click(object sender, EventArgs e)
        {
            UCFinish_Click(sender, e);
        }

        private void LblSelected_Click(object sender, EventArgs e)
        {
            UCFinish_Click(sender, e);
        }

        private void LblPerimDecimal_Click(object sender, EventArgs e)
        {
            UCFinish_Click(sender, e);
        }

        private void LblShortCutNumber_Click(object sender, EventArgs e)
        {
            UCFinish_Click(sender, e);
        }

        private void LblLineName_Click(object sender, EventArgs e)
        {
            UCFinish_Click(sender, e);
        }

        #endregion


        private void updateSelectedFormat()
        {
            FontStyle fs = (Selected) ? FontStyle.Bold : FontStyle.Regular;

            lblShortCutNumber.Font = new Font(lblShortCutNumber.Font, fs);
            lblPerimDecimal.Font = new Font(lblPerimDecimal.Font, fs);
            lblLineName.Font = new Font(lblLineName.Font, fs);

            if (Selected)
            {
                lblSelected.Visible = true;
            }

            else
            {
                lblSelected.Visible = false;
            }
        }

        private void UCLine_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        //public delegate void FinishStatsUpdatedHandler(double totalLenthInInches);

        //public event FinishStatsUpdatedHandler FinishStatsUpdated;

        //public double LengthInInches => LineFinishBase.LengthInInches;

        //internal void UpdateFinishStats()
        //{
        //    double lengthInInches = 0.0;

        //    double scale = canvasManager.CurrentPage.DrawingScaleInInches;

        //    foreach (CanvasDirectedLine canvasDirectedLine in LineFinishManager.CanvasDirectedLines)
        //    {

        //        if (canvasDirectedLine.IsZeroLine)
        //        {
        //            continue;
        //        }

        //        if (canvasDirectedLine.LineRole == LineRole.NullPerimeter || canvasDirectedLine.LineRole == LineRole.ExternalPerimeter)
        //        {
        //            continue;
        //        }

        //        if (canvasDirectedLine.LineCompoundType == LineCompoundType.Single)
        //        {
        //            lengthInInches += canvasDirectedLine.GetScaledLineLength(scale);
        //        }

        //        else if (canvasDirectedLine.LineCompoundType == LineCompoundType.Double)
        //        {
        //            lengthInInches += 2.0 * canvasDirectedLine.GetScaledLineLength(scale);
        //        }

        //    }

        //    foreach (DoorTakeout takeout in DoorTakeoutList)
        //    {
        //        lengthInInches -= Utilities.FeetToInches(takeout.TakeoutAmount);
        //    }

        //    LineFinishBase.LengthInInches = lengthInInches;

        //}

        internal void ClearTallyDisplays()
        {
            lblPerimDecimal.Text = string.Empty;
            //lblLength.Visible = false;
        }

        private void Draw(PaintEventArgs e)
        {
            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(LineColor);

            // Set the width
            pen.Width = (float)LineWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.VisioDashType];

            e.Graphics.DrawLine(pen, 12.0F, 38.0F, 182.0F, 38.0F);
            
            // Dispose of the custom pen.
            pen.Dispose();
        }

        //internal void SetLayerVisibility(DesignState lineArea)
        //{
        //    LineFinishManager.SetLineState(designState, seamMode, false);
        //}


        //internal void AddLineFull(CanvasDirectedLine line, bool updateColor = true)
        //{
        //    //this.LineDict.Add(line.NameID, line);

        //    LineFinishManager.AddToLineDict(line);

        //    line.ucLine = this;

        //    if (updateColor)
        //    {
        //        line.SetBaseLineColor(this.visioLineColorFormula);
        //    }

        //    line.SetBaseLineStyle(this.visioLineStyleFormula);

        //    if (line.LineCompoundType == LineCompoundType.Double)
        //    {
        //        line.SetBaseLineWidth(2 * this.LineWidthInPts);
        //    }

        //    else
        //    {
        //        line.SetBaseLineWidth(this.LineWidthInPts);
        //    }


        //    Visio.Shape visioShape = line.Shape.VisioShape;

        //    if (line.OriginatingDesignState == DesignState.Area || line.OriginatingDesignState == DesignState.Seam)
        //    {
        //        AreaDesignStateLayer.AddShapeToLayer(line.Shape, 1);
        //        SeamDesignStateLayer.AddShapeToLayer(line.Shape, 1);

        //        if (line.IsZeroLine)
        //        {
        //            line.SetBaseLineStyle(CanvasManager.ZeroLineStyleFormula);
        //        }
        //    }

        //    else
        //    {


        //        //var visible = LineModeLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU;
        //    }

        //    LineDesignStateLayer.AddShapeToLayer(line.Shape, 1);

        //    UpdateFinishStats();
        //}

        //public void AddLine(CanvasDirectedLine canvasDirectedLine)
        //{

        //    LineFinishManager.AddToLineDict(canvasDirectedLine);

        //    canvasDirectedLine.ucLine = this;
        //}

        //internal void RemoveLineFull(CanvasDirectedLine line)
        //{
        //    AreaDesignStateLayer.RemoveShapeFromLayer(line, 1);
        //    LineDesignStateLayer.RemoveShapeFromLayer(line, 1);
        //    SeamDesignStateLayer.RemoveShapeFromLayer(line, 1);
        //    RemnantSeamDesignStateLayer.RemoveShapeFromLayer(line, 1);

        //    //--------------------------------------------------------//
        //    // The following check to see if the line is in the       //
        //    // dictionary should not be necessary, as it should       //
        //    // alwasy be at this point. But this is defensive until   //
        //    // the code gets cleaned up.                              //
        //    //--------------------------------------------------------//
        //    if (LineFinishManager.LineDictContains(line.Guid)) // MDD Reset
        //    {
        //        LineFinishManager.RemoveFromLineDict(line);
        //    }

        //    VisioInterop.DeleteShape(line.Shape);

        //    UpdateFinishStats();

        //}

        //internal void RemoveLine(CanvasDirectedLine line)
        //{
        //    AreaDesignStateLayer.RemoveShapeFromLayer(line, 1);
        //    LineDesignStateLayer.RemoveShapeFromLayer(line, 1);
        //    SeamDesignStateLayer.RemoveShapeFromLayer(line, 1);
        //    RemnantSeamDesignStateLayer.RemoveShapeFromLayer(line, 1);

        //    //--------------------------------------------------------//
        //    // The following check to see if the line is in the       //
        //    // dictionary should not be necessary, as it should       //
        //    // alwasy be at this point. But this is defensive until   //
        //    // the code gets cleaned up.                              //
        //    //--------------------------------------------------------//
        //    if (LineFinishManager.LineDictContains(line.Guid)) // MDD Reset
        //    {
        //        LineFinishManager.RemoveFromLineDict(line);
        //    }

        //    UpdateFinishStats();


        //}

        //public void AddLineToLayer(string guid, DesignState designState, SeamMode seamMode)
        //{
        //    if (string.IsNullOrEmpty(guid))
        //    {
        //        return;
        //    }

        //    if (!LineFinishManager.LineDictContains(guid))
        //    {
        //        return;
        //    }

        //    CanvasDirectedLine canvasDirectedLine = LineFinishManager.GetLineFromLineDict(guid);

        //    switch (designState)
        //    {
        //        case DesignState.Area: AreaDesignStateLayer.AddShapeToLayer(canvasDirectedLine, 1); return;
        //        case DesignState.Line: LineDesignStateLayer.AddShapeToLayer(canvasDirectedLine, 1); return;
        //        case DesignState.Seam:

        //            if (seamMode == SeamMode.Remnant)
        //            {
        //                RemnantSeamDesignStateLayer.AddShapeToLayer(canvasDirectedLine.Shape, 1); return;
        //            }

        //            else
        //            {
        //                SeamDesignStateLayer.AddShapeToLayer(canvasDirectedLine.Shape, 1); return;
        //            }

        //        default: return;
        //    }
        //}

        public override string ToString()
        {
            return $"{LineName}:{LineColor}";
        }

    }
}
