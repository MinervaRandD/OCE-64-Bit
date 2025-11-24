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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using FinishesLib;
    using Graphics;

    using FloorMaterialEstimator;
    using FloorMaterialEstimator.CanvasManager;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class UCLineFinishPalletElement : UserControl
    {

        private DesignState designState
        {
            get
            {
                return baseLineFinishPallet.DesignState;
            }
        }

        public string Guid
        {
            get
            {
                return LineFinishBase.Guid;
            }

            set
            {
                LineFinishBase.Guid = value;
            }
        }


        public LineFinishBase LineFinishBase;

        public string LineName
        {
            get
            {
                return LineFinishBase.LineName;
            }

            set
            {
                LineFinishBase.LineName = value;
                this.lblLineName.Text = value;
            }
        }

        public short VisioDashType
        {
            get
            {
                return (short) LineFinishBase.VisioLineType;
            }

            set
            {
                LineFinishBase.VisioLineType = value;
                updateBaseLineStyle(value);

            }
        }
     
        public double LineWidthInPts
        {
            get
            {
                return LineFinishBase.LineWidthInPts;
            }

            set
            {
                LineFinishBase.LineWidthInPts = value;
                updateBaseLineWidth(value);
            }
        }

        public Color LineColor
        {
            get
            {
                return LineFinishBase.LineColor;
            }

            set
            {
                LineFinishBase.LineColor = value;
              
                updateBaseLineColor(value);
            }
        }

        public DesignState LineAreaMode
        {
            get
            {
                return baseLineFinishPallet.LineAreaMode;
            }
        }

        private bool selected = false;

        public bool Selected
        {
            get
            {
                return this.selected;
            }

            set
            {
                this.selected = value;

                updateSelectedFormat();

            }
        }


        private double? _dPerim = null;

        public double? dPerim
        {
            get
            {
                return _dPerim;
            }

            set
            {
                _dPerim = value;

                if (_dPerim == null)
                {
                    lblPerimDecimal.Text = string.Empty;
                }

                else
                {
                    lblPerimDecimal.Text = _dPerim.Value.ToString("#,##0.0");
                }
            }
        }


        private double? _dTrim = null;

        public double? dTrim
        {
            get
            {
                return _dTrim;
            }

            set
            {
                _dTrim = value;
            }
        }


        #region Line Color

        private bool baseLineColorChanged(Color currColor, Color updtColor)
        {
            return currColor.R != updtColor.R ||
                    currColor.G != updtColor.G ||
                    currColor.B != updtColor.B;
        }

        private void updateBaseLineColor(Color updtColor)
        {
            visioLineColorFormula =
                string.Format("THEMEGUARD(RGB({0},{1},{2}))", updtColor.R, updtColor.G, updtColor.B);

            foreach (CanvasDirectedLine l in CanvasDirectedLines)
            {
                l.SetBaseLineColor(visioLineColorFormula);
            }
        }

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
        private void updateBaseLineWidth(double updtWidthInPts)
        {       
            foreach (CanvasDirectedLine l in CanvasDirectedLines)
            {
                l.SetBaseLineWidth((double)updtWidthInPts);
            }
        }

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
       

        private bool baseLineStyleChanged(short currStyle, short updtStyle)
        {
            return currStyle != updtStyle;
        }

        private void updateBaseLineStyle(short visioLineStyle)
        {
            visioLineStyleFormula = visioLineStyle.ToString();
            
            foreach (CanvasDirectedLine l in CanvasDirectedLines)
            {
                l.SetBaseLineStyle(visioLineStyleFormula);
            }
        }

        private string _visioLineStyleFormula = "1";

        public string visioLineStyleFormula
        {
            get
            {
                return _visioLineStyleFormula;
            }

            set
            {
                _visioLineStyleFormula = value;
            }
        }

#endregion

        public Visio.Layer AreaModeLayer;
        public Visio.Layer LineModeLayer;

        private bool filtered = false;

        public bool Filtered
        {
            get
            {
                return filtered;
            }

            set
            {
                filtered = value;

                SetLineState(designState, this.Selected);

                if (filtered)
                {
                    this.Enabled = false;
                }

                else
                {
                    this.Enabled = true;
                }
            }
        }

        public UCLineFinishPallet baseLineFinishPallet;

        public CanvasManager canvasManager
        {
            get
            {
                return baseLineFinishPallet.CanvasManager;
            }
        }

        private int positionOnPallet = -1;

        public int PositionOnPallet
        {
            get
            {
                return positionOnPallet;
            }

            set
            {
                positionOnPallet = value;

                this.lblShortCutNumber.Text = (value+1).ToString();
            }
        }
        
        public Dictionary<string, CanvasDirectedLine> LineDict = new Dictionary<string, CanvasDirectedLine>();
        public Dictionary<string, CanvasDirectedLine> LineDictByGuid = new Dictionary<string, CanvasDirectedLine>();

        public IEnumerable<CanvasDirectedLine> CanvasDirectedLines => LineDict.Values;

        public UCLineFinishPalletElement(UCLineFinishPallet baseLinePallet, LineFinishBase finishLineBase)
        {
            InitializeComponent();

            this.baseLineFinishPallet = baseLinePallet;

            this.LineFinishBase = finishLineBase;

            this.LineName = this.LineFinishBase.LineName;

            this.lblGuid.Text = finishLineBase.Guid;

            updateBaseLineColor(this.LineFinishBase.LineColor);
            updateBaseLineStyle((short) this.LineFinishBase.VisioLineType);
            updateBaseLineWidth(this.LineFinishBase.LineWidthInPts);

            this.Click += UCLineFinishPalletElement_Click;

            this.lblLineName.Click += LblLineName_Click;
            this.lblShortCutNumber.Click += LblShortCutNumber_Click;
            this.lblPerimDecimal.Click += LblPerimDecimal_Click;
            this.lblSelected.Click += LblSelected_Click;
            this.Paint += UCLine_Paint;
        }

        #region Events

        public delegate void ControlClickedHandler(UCLineFinishPalletElement sender);

        public event ControlClickedHandler ControlClicked;

        //private void PnlColor_Click(object sender, EventArgs e)
        //{
        //    if (ControlClicked != null)
        //    {
        //        ControlClicked.Invoke(this);
        //    }
        //}

        private void UCFinish_Click(object sender, EventArgs e)
        {
            if (ControlClicked != null)
            {
                ControlClicked.Invoke(this);
            }
        }

        private void UCLineFinishPalletElement_Click(object sender, EventArgs e)
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

        internal void SetLineState(DesignState designState, bool selected)
        {
            if (Filtered)
            {
                VisioInterop.SetLayerVisibility(this.AreaModeLayer, false);
                VisioInterop.SetLayerVisibility(this.LineModeLayer, false);

                return;
            }


            if (designState == DesignState.Area)
            {
                VisioInterop.SetLayerVisibility(AreaModeLayer, true);
                VisioInterop.SetLayerVisibility(LineModeLayer, false);

                foreach (CanvasDirectedLine line in CanvasDirectedLines)
                {
                    line.SetLineGraphics(DesignState.Area, selected, AreaShapeBuildStatus.Completed);
                }
            }

            else if (designState == DesignState.Line)
            {
                VisioInterop.SetLayerVisibility(AreaModeLayer, true);
                VisioInterop.SetLayerVisibility(LineModeLayer, true);

                foreach (CanvasDirectedLine line in CanvasDirectedLines)
                {
                    line.SetLineGraphics(DesignState.Line, selected, AreaShapeBuildStatus.Completed);
                }
            }

            else if (designState == DesignState.Seam)
            {
                VisioInterop.SetLayerVisibility(AreaModeLayer, true);
                VisioInterop.SetLayerVisibility(LineModeLayer, false);

                foreach (CanvasDirectedLine line in CanvasDirectedLines)
                {
                    line.SetLineGraphics(DesignState.Seam, selected, AreaShapeBuildStatus.Completed);
                }
            }
        }

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

            e.Graphics.DrawLine(pen, 12.0F, 54.0F, 182.0F, 54.0F);
            
            // Dispose of the custom pen.
            pen.Dispose();
        }

        internal void SetLayerVisibility(DesignState lineArea)
        {
            if (lineArea == DesignState.Area)
            {
                VisioInterop.SetLayerVisibility(this.LineModeLayer, false);
            }

            else
            {
                if (!Filtered)
                {
                    VisioInterop.SetLayerVisibility(this.LineModeLayer, true);
                }
            }
        }

        //public void SetBase(Base l_b)
        //{
        //    bBase = l_b;
        //    //LineName = b.sName;
        //    //lblName.Text = LineName;
        //    //pnlBackColor = Color.FromArgb(255, b.R, b.G, b.B);
        //    //Alpha = (short)b.A;
        //    //this.Invalidate();
        //}

        internal void AddLine(CanvasDirectedLine line)
        {
            this.LineDict.Add(line.NameID, line);
            this.LineDictByGuid.Add(line.Guid, line);

            line.ucLine = this;

            line.Shape.VisioShape.Data1 = this.LineName;
            line.Shape.VisioShape.Data2 = "Line";

            line.SetBaseLineColor(this.visioLineColorFormula);
            line.SetBaseLineStyle(this.visioLineStyleFormula);

            if (line.LineCompoundType == LineCompoundType.Double)
            {
                line.SetBaseLineWidth(2 * this.LineWidthInPts);
            }

            else
            {
                line.SetBaseLineWidth(this.LineWidthInPts);
            }
           

            Visio.Shape visioShape = line.Shape.VisioShape;

            if (line.OriginatingDesignState == DesignState.Area || line.OriginatingDesignState == DesignState.Seam)
            {
                AreaModeLayer.Add(visioShape, 1);

                if (line.IsZeroLine)
                {
                    line.SetBaseLineStyle(CanvasManager.ZeroLineStyleFormula);
                }
            }

            else
            {
                LineModeLayer.Add(visioShape, 1);

                //var visible = LineModeLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU;
            }
        }

        internal void RemoveLine(CanvasDirectedLine line)
        {
            if (!LineDict.ContainsKey(line.NameID))
            {
                return;
            }

            this.LineDict.Remove(line.NameID);
            this.LineDictByGuid.Remove(line.Guid);

            line.Shape.VisioShape.Data1 = string.Empty;

            Visio.Shape visioShape = line.Shape.VisioShape;

            if (line.OriginatingDesignState == DesignState.Area || line.OriginatingDesignState == DesignState.Seam)
            {
                AreaModeLayer.Remove(visioShape, 1);
            }

            else
            {
                LineModeLayer.Remove(visioShape, 1);
            }
        }

        internal void Delete()
        {
            this.AreaModeLayer.Delete(1);
            this.LineModeLayer.Delete(1);
        }

    }
}
