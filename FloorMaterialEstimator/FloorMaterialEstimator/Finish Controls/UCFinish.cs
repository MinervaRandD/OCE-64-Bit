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

namespace FloorMaterialEstimator.Finish_Controls
{
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

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

    public partial class UCFinish : UserControl
    {
        private bool SelectedBase = false;

        public bool Selected
        {
            get
            {
                return this.SelectedBase;
            }

            set
            {
                if (this.SelectedBase == value)
                {
                    return;
                }

                this.SelectedBase = value;

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

                lblAreaValue.Text = this._dArea.ToString("#,##0.0");
            }
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

                lblPerimValue.Text = _dPerim.ToString("#,##0.0");
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


        private string _finishName = string.Empty;

        public string finishName
        {
            get
            {
                return _finishName;
            }

            set
            {
                _finishName = value;
                this.lblName.Text = _finishName;
            }
        }

        private Color _finishColor = Color.Empty;

        public Color finishColor
        {
            get
            {
                return _finishColor;
            }

            set
            {
                Color prevColor = _finishColor;
                Color currColor = value;

                _finishColor = value;

                this.pnlColor.BackColor = _finishColor;
                this.pnlColor.ForeColor = _finishColor;

                if (baseColorChanged(prevColor, currColor))
                {
                    updateBaseColor(value);
                }

                if (opacityChanged(prevColor, currColor))
                {
                    updateOpacity(value);
                }
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

            areaShapeList.ForEach(s => s.SetFillColor(visioFillColorFormula));
        }

        private bool opacityChanged(Color currColor, Color updtColor)
        {
            return currColor.A != updtColor.A;
        }

        private void updateOpacity(Color updtColor)
        {
            double opacity = 100.0 - Math.Max(Math.Min(100.0 * ((double)_finishColor.A) / 255.0, 100.0), 0.0);

            visioFillTransparencyFormula = opacity.ToString("0.00000000") + "%";

            areaShapeList.ForEach(s => s.SetFillOpacity(visioFillTransparencyFormula));
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
                    FloorMaterialEstimator.Utilities.Graphics.SetLayerVisibility(layer, false);
                    this.Enabled = false;
                }

                else
                {
                    FloorMaterialEstimator.Utilities.Graphics.SetLayerVisibility(layer, true);
                    this.Enabled = true;
                }
            }
        }

        public UCFinishPallet baseFinishPallet;

        public CanvasManager canvasManager
        {
            get
            {
                return baseFinishPallet.canvasManager;
            }
        }

        public int fIndex = -1;

        public List<AreaShape> areaShapeList = new List<AreaShape>();

        #region Constructors

        public UCFinish(UCFinishPallet baseFinishPallet)
        {
            InitializeComponent();

            setupTrim();

            this.baseFinishPallet = baseFinishPallet;

            this.Click += UCFinish_Click;

            this.pnlColor.Click += PnlColor_Click;

            this.trbOpacity.ValueChanged += TrbTransparency_ValueChanged;

            this.cmbTrimFactor.SelectedIndexChanged += CmbTrimFactor_SelectedIndexChanged;
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
                return;
            }

            this.dTrim = dTrimInches / 12.0;

            UpdateFinishStats();
        }

        internal void UpdateFinishStats()
        {
            dArea = areaShapeList.Sum(a => a.InternalArea());
            dPerim = areaShapeList.Sum(p => p.PerimeterLength());
            dWaste = 100.0 * _dTrim * dPerim / dArea ;
        }

        private void TrbTransparency_ValueChanged(object sender, EventArgs e)
        {
            this.lblOpacityValue.Text = this.trbOpacity.Value.ToString("0") + "%";

            byte A = (byte) Math.Round(Math.Max(Math.Min(255.0 * ((double)this.trbOpacity.Value) / 100.0, 255), 0.0));

            Color updtColor = Color.FromArgb((int)A, finishColor);

            finishColor = updtColor;
        }

        #endregion

        #region Events

        private void UCFinish_Click(object sender, EventArgs e)
        {
            baseFinishPallet.selectedFinish = this;

        }

        private void PnlColor_Click(object sender, EventArgs e)
        {
            UCFinish_Click(sender, e);
        }


        #endregion

        private void updateSelectedFormat()
        {
            FontStyle fs = (Selected) ? FontStyle.Bold : FontStyle.Regular;

            lblAreaText.Font = new Font(lblAreaText.Font, fs);
            lblPerimText.Font = new Font(lblPerimText.Font, fs);
            lblWasteText.Font = new Font(lblWasteText.Font, fs);

            lblAreaValue.Font = new Font(lblAreaValue.Font, fs);
            lblPerimValue.Font = new Font(lblPerimValue.Font, fs);
            lblWasteValue.Font = new Font(lblWasteValue.Font, fs);

            lblName.Font = new Font(lblName.Font, fs);

            this.Refresh();
        }


        internal void AddShape(AreaShape areaShape)
        {
            this.areaShapeList.Add(areaShape);

            areaShape.InternalAreaShape.VisioShape.Data1 = this.finishName;

            areaShape.SetFillColor(this.visioFillColorFormula);
            areaShape.SetFillOpacity(this.visioFillTransparencyFormula);

            layer.Add(areaShape.InternalAreaShape.VisioShape, 1);

            areaShape.ucFinish = this;

            this.UpdateFinishStats();
        }
    }
}
