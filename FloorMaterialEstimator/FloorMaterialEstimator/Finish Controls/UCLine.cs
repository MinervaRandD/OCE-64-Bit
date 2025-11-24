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
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.CanvasManager;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class UCLine : UserControl
    {
        private bool _bSelected = false;

        public bool bSelected
        {
            get
            {
                return _bSelected;
            }

            set
            {
                if (_bSelected == value)
                {
                    return;
                }

                _bSelected = value;

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

        private string _lineName = string.Empty;

        public string lineName
        {
            get
            {
                return _lineName;
            }

            set
            {
                _lineName = value;
                this.lblLineName.Text = _lineName;
            }
        }

        #region Line Color

        private Color _lineColor = Color.Empty;

        public Color lineColor
        {
            get
            {
                return _lineColor;
            }

            set
            {
                Color prevColor = _lineColor;
                Color currColor = value;

                _lineColor = value;

                if (baseLineColorChanged(prevColor, currColor))
                {
                    updateBaseLineColor(value);
                }
            }
        }

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

            lineList.ForEach(l => l.SetBaseLineColor(visioLineColorFormula));
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

        private double lineWidthInPts = 4;

        public double LineWidthInPts
        {
            get
            {
                return lineWidthInPts;
            }

            set
            {
                double prevWidth = lineWidthInPts;
                double currWidth = value;

                lineWidthInPts = value;

                if (currWidth != prevWidth)
                {
                    updateBaseLineWidth(value);
                }
            }
        }

        private void updateBaseLineWidth(double updtWidthInPts)
        {             
            lineList.ForEach(l => l.SetBaseLineWidth((double)updtWidthInPts));
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

        private short _lineStyle = 0;

        public short lineStyle
        {
            get
            {
                return _lineStyle;
            }

            set
            {
                short prevStyle = _lineStyle;
                short currStyle = value;

                _lineStyle = value;

                if (baseLineStyleChanged(prevStyle, currStyle))
                {
                    updateBaseLineStyle(value);
                }

            }
        }

        private bool baseLineStyleChanged(short currStyle, short updtStyle)
        {
            return currStyle != updtStyle;
        }

        private void updateBaseLineStyle(short updtStyle)
        {
            switch (updtStyle)
            {
                case 0:
                    visioLineStyleFormula = "1"; // Solid as originally defined.
                    break;

                case 1:
                    visioLineStyleFormula = "10"; // Dot as originally defined.
                    break;

                case 2:
                    visioLineStyleFormula = "9"; // Dash as originally defined
                    break;

                case 3:
                    visioLineStyleFormula = "11"; // Dash dot as originally defined
                    break;

                default:
                    throw new NotImplementedException();
            }
         
            lineList.ForEach(l => l.SetBaseLineStyle(visioLineStyleFormula));
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
        public Visio.Layer layer;

        private bool _bIsFiltered = false;

        public bool bIsFiltered
        {
            get
            {
                return _bIsFiltered;
            }

            set
            {
                _bIsFiltered = value;

                if (_bIsFiltered)
                {
                    layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "0";
                }

                else
                {
                    layer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "1";
                }
            }
        }


        private Base _bBase;

        public Base bBase
        {
            get
            {
                return _bBase;
            }

            set
            {
                _bBase = value;
            }
        }
        public UCLinePallet baseLinePallet;

        public CanvasManager canvasManager
        {
            get
            {
                return baseLinePallet.canvasManager;
            }
        }

        private int _lIndex = -1;

        public int lIndex
        {
            get
            {
                return _lIndex;
            }

            set
            {
                _lIndex = value;

                this.lblShortCutNumber.Text = (value+1).ToString();
            }
        }

        public List<GraphicsLine> lineList = new List<GraphicsLine>();

        public UCLine(UCLinePallet baseLinePallet)
        {
            InitializeComponent();

            this.baseLinePallet = baseLinePallet;

            this.Click += UCLine_Click;

            this.Paint += UCLine_Paint;
        }

        private void UCLine_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                baseLinePallet.UCLineControlKeyClick(this);
            }

            else
            {
                baseLinePallet.UCLineSimpleClick(this);
            }
        }

        private void updateSelectedFormat()
        {
            FontStyle fs = (bSelected) ? FontStyle.Bold : FontStyle.Regular;

            lblShortCutNumber.Font = new Font(lblShortCutNumber.Font, fs);
            lblPerimDecimal.Font = new Font(lblPerimDecimal.Font, fs);
            lblLineName.Font = new Font(lblLineName.Font, fs);

            if (bSelected)
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
            ShowPensAndSmoothingMode(e);
        }

        private void ShowPensAndSmoothingMode(PaintEventArgs e)
        {

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(lineColor);

            // Set the width
            pen.Width = (float)bBase.Width;

            // Set the DashCap
            if (bBase.CapType == 0)
            {
                pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
            }
            else
            {
                pen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            }

            // Create a custom dash pattern.
            if (bBase.DashStyle == 0)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            }
            else if (bBase.DashStyle == 1)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            }
            else if (bBase.DashStyle == 2)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            }

            // Draw a line
            if (bBase.DashType == 0)
            {
                e.Graphics.DrawLine(pen, 12.0F, 54.0F, 182.0F, 54.0F);
            }
            else
            {
                e.Graphics.DrawLine(pen, 12.0F, 44.0F, 182.0F, 44.0F);
                e.Graphics.DrawLine(pen, 12.0F, 64.0F, 182.0F, 64.0F);
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public void SetBase(Base l_b)
        {
            bBase = l_b;
            //LineName = b.sName;
            //lblName.Text = LineName;
            //pnlBackColor = Color.FromArgb(255, b.R, b.G, b.B);
            //Alpha = (short)b.A;
            //this.Invalidate();
        }

        internal void AddLine(GraphicsLine line)
        {
            this.lineList.Add(line);

            line.ucLine = this;

            line.VisioShape.Data1 = this.lineName;
            line.VisioShape.Data2 = "Line";

            line.SetBaseLineColor(this.visioLineColorFormula);
            line.SetBaseLineStyle(this.visioLineStyleFormula);
            line.SetBaseLineWidth(this.lineWidthInPts);

            Visio.Shape visioShape = line.VisioShape;

            layer.Add(visioShape, 1);
        }

        internal void RemoveLine(GraphicsLine line)
        {
            this.lineList.Remove(line);
            line.VisioShape.Data1 = string.Empty;

            Visio.Shape visioShape = line.VisioShape;

            layer.Remove(visioShape, 1);
        }
    }
}
