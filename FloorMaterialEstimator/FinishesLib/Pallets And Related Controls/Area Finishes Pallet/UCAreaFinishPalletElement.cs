//-------------------------------------------------------------------------------//
// <copyright file="UCFinish.cs" company="Bruun Estimating, LLC">                //
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FinishesLib
{
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

    using Graphics;

    using Visio = Microsoft.Office.Interop.Visio;
    using System.Diagnostics;
    using FloorMaterialEstimator.ShortcutsAndSettings;

    public partial class UCAreaFinishPalletElement : UserControl
    {
        public const int CntlSizeY = 160;

        public AreaFinishBase AreaFinishBase;

        private DesignState designState => baseAreaFinishPallet.DesignState;
       
        private bool showAreas => baseAreaFinishPallet.ShowAreas;
       
        public string Guid => AreaFinishBase.Guid;


        public string AreaName
        {
            get
            {
                return AreaFinishBase.AreaName;
            }

            set
            {
                AreaFinishBase.AreaName = value;
                this.lblName.Text = value;
            }
        }

        public double Opacity
        {
            get
            {
                return AreaFinishBase.Opacity;
            }

            set
            {
                AreaFinishBase.Opacity = value;

                this.trbOpacity.Value = (int)Math.Max(0, Math.Min(100, Math.Round(100.0 * (double)value)));
            }
        }

        public Color FinishColor
        {
            get
            {
                return AreaFinishBase.Color;
            }

            set
            {
                AreaFinishBase.Color = value;

                this.pnlColor.BackColor = AreaFinishBase.Color;
                this.pnlColor.ForeColor = AreaFinishBase.Color;

                updateBaseColor(value);

                updateOpacity(value);

            }
        }

        public string Product
        {
            get
            {
                return AreaFinishBase.Product;
            }

            set
            {
                AreaFinishBase.Product = value;
            }
        }

        private string notes = string.Empty;

        public string Notes
        {
            get
            {
                return AreaFinishBase.Notes;
            }

            set
            {
                AreaFinishBase.Notes = value;
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

        private double _dArea = 0;

        public double dArea
        {
            get
            {
                return this._dArea;
            }

            set
            {
                this._dArea = value;

                lblAreaValue.Text = (this._dArea / 144.0).ToString("#,##0.0");
            }
        }

        #region Design State Management

        public void SetDesignStateFormat(DesignState designState, bool showAreas)
        {
            if (Filtered || !showAreas)
            {
                VisioInterop.SetLayerVisibility(this.areaDesignStateLayer, false);
                VisioInterop.SetLayerVisibility(this.seamDesignStateLayer, false);

                return;
            }

            if (designState == DesignState.Area)
            {
                VisioInterop.SetLayerVisibility(this.areaDesignStateLayer, true);
                VisioInterop.SetLayerVisibility(this.seamDesignStateLayer, false);
                
                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    layoutArea.SetAreaDesignStateFormat(baseAreaFinishPallet.BaseForm.AreaMode);
                }
            }

            else if (designState == DesignState.Line)
            {
                VisioInterop.SetLayerVisibility(this.areaDesignStateLayer, false);
                VisioInterop.SetLayerVisibility(this.seamDesignStateLayer, false);

                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    layoutArea.SetLineDesignStateFormat(baseAreaFinishPallet.BaseForm.LineMode);
                }
            }

            else if (designState == DesignState.Seam)
            {
                VisioInterop.SetLayerVisibility(this.areaDesignStateLayer, true);
                VisioInterop.SetLayerVisibility(this.seamDesignStateLayer, true);

                foreach (CanvasLayoutArea layoutArea in this.CanvasLayoutAreas)
                {
                    layoutArea.SetSeamDesignStateFormat(baseAreaFinishPallet.BaseForm.SeamMode);
                }
                
            }
        }

        #endregion

        internal void SetLayerInvisibility()
        {
            VisioInterop.SetLayerVisibility(this.areaDesignStateLayer, false);
            VisioInterop.SetLayerVisibility(this.seamDesignStateLayer, false);
        }

        private double _dPerim = 0;

        public double dPerim
        {
            get
            {
                return this._dPerim;
            }

            set
            {
                this._dPerim = value;

                lblPerimValue.Text = (_dPerim / 12.0).ToString("#,##0.0");
            }
        }

        private double _dWaste = 0;

        public double dWaste
        {
            get
            {
                return _dWaste;
            }

            set
            {
                _dWaste = value;

                lblWasteValue.Text = _dWaste.ToString("#,##0.0");
            }
        }

        private double _dTrim = 0;

        public double dTrim
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


        private bool baseColorChanged(Color currColor, Color updtColor)
        {
            return currColor.R != updtColor.R ||
                    currColor.G != updtColor.G ||
                    currColor.B != updtColor.B;
        }

        private void updateBaseColor(Color updtColor)
        {
            visioFillColorFormula =
                string.Format("THEMEGUARD(RGB({0},{1},{2}))", updtColor.R, updtColor.G, updtColor.B);

            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreas)
            {
                canvasLayoutArea.SetFillColor(visioFillColorFormula);
            }
        }

        private bool opacityChanged(Color currColor, Color updtColor)
        {
            return currColor.A != updtColor.A;
        }

        private void updateOpacity(Color updtColor)
        {
            double opacity = 100.0 - Math.Max(Math.Min(100.0 * ((double)FinishColor.A) / 255.0, 100.0), 0.0);

            visioFillTransparencyFormula = opacity.ToString("0.00000000") + "%";

            foreach (CanvasLayoutArea canvasLayoutArea in CanvasLayoutAreas)
            {
                canvasLayoutArea.SetFillTransparancy(visioFillTransparencyFormula);
            }
        }

        private string _visioFillColorFormula = "THEMEGUARD(RGB(255,255,255))";

        public string visioFillColorFormula
        {
            get
            {
                return _visioFillColorFormula;
            }

            set
            {
                _visioFillColorFormula = value;
            }
        }

        private string _visioFillTransparencyFormula = "100%";

        public string visioFillTransparencyFormula
        {
            get
            {
                return _visioFillTransparencyFormula;
            }

            set
            {
                _visioFillTransparencyFormula = value;
            }
        }

        public Visio.Layer areaDesignStateLayer;
        public Visio.Layer seamDesignStateLayer;

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

                SetDesignStateFormat(designState, showAreas);

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

        public SeamFinishBase FinishSeamBase
        {
            get
            {
                return AreaFinishBase.FinishSeamBase;
            }

            set
            {
                AreaFinishBase.FinishSeamBase = value;
            }
        }

        public UCAreaFinishPallet baseAreaFinishPallet;

        public CanvasManager canvasManager
        {
            get
            {
                return baseAreaFinishPallet.CanvasManager;
            }
        }

      

        public double TileWidthInInches
        {
            get
            {
                return AreaFinishBase.TileWidthInInches;
            }

            set
            {
                AreaFinishBase.TileWidthInInches = value;
            }
        }

        public double TileHeightInInches
        {
            get
            {
                return AreaFinishBase.TileHeightInInches;
            }

            set
            {
                AreaFinishBase.TileHeightInInches = value;
            }
        }

        public double RollWidthInInches
        {
            get
            {
                return AreaFinishBase.RollWidthInInches;
            }

            set
            {
                AreaFinishBase.RollWidthInInches = value;
            }
        }

        public double RollRepeat1InInches
        {
            get
            {
                return AreaFinishBase.RollRepeat1InInches;
            }

            set
            {
                AreaFinishBase.RollRepeat1InInches = value;
            }
        }

        public double RollRepeat2InInches
        {
            get
            {
                return AreaFinishBase.RollRepeat2InInches;
            }

            set
            {
                AreaFinishBase.RollRepeat2InInches = value;
            }
        }

        public bool Seamed
        {
            get
            {
                return AreaFinishBase.Seamed;
            }

            set
            {
                AreaFinishBase.Seamed = value;
            }
        }

        public bool Cuts
        {
            get
            {
                return AreaFinishBase.Cuts;
            }

            set
            {
                AreaFinishBase.Cuts = value;
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

                this.lblShortCut.Text = (value + 1).ToString();
            }
        }

        public Dictionary<string, CanvasLayoutArea> CanvasLayoutAreaDict = new Dictionary<string, CanvasLayoutArea>();

        public IEnumerable<CanvasLayoutArea> CanvasLayoutAreas => CanvasLayoutAreaDict.Values;

        #region Constructors

        public UCAreaFinishPalletElement(UCAreaFinishPallet baseFinishPallet, AreaFinishBase areaFinishBase)
        {
            InitializeComponent();

            this.AreaFinishBase = areaFinishBase;
            
            setupTrim();

            this.baseAreaFinishPallet = baseFinishPallet;


            this.pnlColor.BackColor = this.AreaFinishBase.Color;
            this.lblName.Text = this.AreaFinishBase.AreaName;
            this.trbOpacity.Value = (int) Math.Min(100, Math.Max(0, 100.0 * AreaFinishBase.Opacity));

            lblAreaValue.Text = "    0.0";
            lblPerimValue.Text = "    0.0";
            lblWasteValue.Text = "  0.0%";

            lblGuid.Text = Guid;

            updateBaseColor(this.AreaFinishBase.Color);

            updateOpacity(this.AreaFinishBase.Color);

            this.Click += UCFinish_Click;
            this.pnlColor.Click += PnlColor_Click;

            this.pnlColor.Paint += PnlColor_Paint;

            this.trbOpacity.ValueChanged += TrbTransparency_ValueChanged;

            this.cmbTrimFactor.SelectedIndexChanged += CmbTrimFactor_SelectedIndexChanged;
        }

        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
            if (this.FinishSeamBase == null)
            {
                return;
            }

            double height = this.pnlColor.Height;
            double yIncmnt = height / 3.0;

            double yOffset = yIncmnt / 2.0 - 0.333F;

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen;

            pen = new Pen(this.FinishSeamBase.SeamColor);

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.FinishSeamBase.VisioDashType];

            double sizeX = this.pnlColor.Width;

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, 0.1F, (float)yOffset, (float)sizeX - 0.2F, (float)yOffset);

                yOffset += yIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        private void setupTrim()
        {
            for (int i = 0; i <= 6; i++)
            {
                this.cmbTrimFactor.Items.Add(i.ToString() + '"');
            }

            this.cmbTrimFactor.SelectedIndex = 0;
        }

        private void CmbTrimFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            double dTrimInches = 0;

            string sTrim = cmbTrimFactor.SelectedItem.ToString().Replace("\"", "");

            if (!double.TryParse(sTrim, out dTrimInches))
            {
                this.lblTrimFactor.Select();

                return;
            }

            this.dTrim = dTrimInches / 12.0;

            UpdateFinishStats();

            this.lblTrimFactor.Select();
        }

        internal void UpdateFinishStats()
        {
            dArea = CanvasLayoutAreas.Where(a=>a.ParentArea == null).Sum(a => a.Area());
            dPerim = CanvasLayoutAreas.Where(a => a.ParentArea == null).Sum(p => p.PerimeterLength());
            dWaste = dArea > 0.0 ? 100.0 * _dTrim * dPerim / dArea : 0.0 ;
        }

        private void TrbTransparency_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = ((double)this.trbOpacity.Value) / 100.0;

            this.lblOpacityValue.Text = (100.0 * this.Opacity).ToString("0") + "%";

            byte A = (byte) Math.Round(Math.Max(Math.Min(255.0 * this.Opacity, 255), 0.0));

            Color updtColor = Color.FromArgb((int)A, FinishColor);

            FinishColor = updtColor;

            if (FinishChanged != null)
            {
                FinishChanged.Invoke(this);
            }
        }

        #endregion

        #region Events

        public delegate void FinishChangedHandler(UCAreaFinishPalletElement ucAreaFinish);

        public event FinishChangedHandler FinishChanged;

        public delegate void ControlClickedHandler(UCAreaFinishPalletElement sender);

        public event ControlClickedHandler ControlClicked;
        
        private void PnlColor_Click(object sender, EventArgs e)
        {
            if (ControlClicked != null)
            {
                ControlClicked.Invoke(this);
            }
        }

        private void UCFinish_Click(object sender, EventArgs e)
        {
            if (ControlClicked != null)
            {
                ControlClicked.Invoke(this);
            }
        }

        #endregion

        private void updateSelectedFormat()
        {
            FontStyle fs = (this.selected) ? FontStyle.Bold : FontStyle.Regular;

            lblAreaText.Font = new Font(lblAreaText.Font, fs);
            lblPerimText.Font = new Font(lblPerimText.Font, fs);
            lblWasteText.Font = new Font(lblWasteText.Font, fs);

            lblAreaValue.Font = new Font(lblAreaValue.Font, fs);
            lblPerimValue.Font = new Font(lblPerimValue.Font, fs);
            lblWasteValue.Font = new Font(lblWasteValue.Font, fs);

            lblShortCut.Font = new Font(lblShortCut.Font, fs);

            lblName.Font = new Font(lblName.Font, fs);

            this.Invalidate();

            this.Refresh();
        }

        internal void AddShape(CanvasLayoutArea canvasLayoutArea, bool recursive = true, bool updateColor = true)
        {
            this.CanvasLayoutAreaDict.Add(canvasLayoutArea.NameID, canvasLayoutArea);

            canvasLayoutArea.InternalArea.VisioShape.Data1 = this.AreaName;
             
            if (updateColor)
            {
                canvasLayoutArea.SetFillColor(this.visioFillColorFormula);
                canvasLayoutArea.SetFillTransparancy(this.visioFillTransparencyFormula);
            }

            seamDesignStateLayer.Add(canvasLayoutArea.InternalArea.VisioShape, 1);

            canvasLayoutArea.DrawSeams();

            if (canvasLayoutArea.OriginatingDesignState == DesignState.Area)
            {
                areaDesignStateLayer.Add(canvasLayoutArea.InternalArea.VisioShape, 1);
            }

            canvasLayoutArea.UCAreaFinish = this;

            this.UpdateFinishStats();

            if (!recursive)
            {
                return;
            }

            if (canvasLayoutArea.OffspringAreas is null)
            {
                return;
            }

            foreach (CanvasLayoutArea offspringArea in canvasLayoutArea.OffspringAreas)
            {
                AddShape(offspringArea, recursive, updateColor);
            }
        }

        internal void RemoveShape(CanvasLayoutArea canvasLayoutArea)
        {
            this.CanvasLayoutAreaDict.Remove(canvasLayoutArea.NameID);

            canvasLayoutArea.Shape.VisioShape.Data1 = string.Empty;

            Visio.Shape visioShape = canvasLayoutArea.Shape.VisioShape;

            if (canvasLayoutArea.OriginatingDesignState == DesignState.Area)
            {
                areaDesignStateLayer.Remove(visioShape, 1);
            }

            seamDesignStateLayer.Remove(visioShape, 1);

            canvasLayoutArea.UndrawSeams();

            this.UpdateFinishStats();

            if (canvasLayoutArea.OffspringAreas is null)
            {
                return;
            }

            foreach (CanvasLayoutArea offspringArea in canvasLayoutArea.OffspringAreas)
            {
                RemoveShape(offspringArea);
            }
        }

        internal void SetSeam(SeamFinishBase finishSeamBase)
        {
            int indx = this.PositionOnPallet;

            string guid = this.Guid;

            this.FinishSeamBase = finishSeamBase.Clone();

            this.pnlColor.Invalidate();

            //this.Invalidate();
        }


        internal void Delete()
        {
            areaDesignStateLayer.Delete(1);
            seamDesignStateLayer.Delete(1);
        }

    }
}
