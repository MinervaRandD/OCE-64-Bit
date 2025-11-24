//-------------------------------------------------------------------------------//
// <copyright file="UCAreaFinishPaletteElement.cs"                                //
//           company="Bruun Estimating, LLC">                                    //
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//     Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019, 2020     //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Finish_Controls
{   
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using FinishesLib;
    using Graphics;
    using Utilities;
    using Globals;
    

    public partial class UCAreaFinishPaletteElement : UserControl, IDisposable
    {
        public const int CntlSizeY = 160;

        public AreaFinishBase AreaFinishBase;

        public GraphicsPage Page { get; set; }

        public GraphicsWindow Window { get; set; }

        public string Guid => AreaFinishBase.Guid;

        public string AreaName => AreaFinishBase.AreaName;

        public double Opacity => AreaFinishBase.Opacity;
       
        public Color FinishColor => AreaFinishBase.Color;

        private SeamFinishBase seamFinishBase => AreaFinishBase.SeamFinishBase;

        public MaterialsType MaterialsType => AreaFinishBase.MaterialsType;

        private void setupTallyDisplay(bool scaleHasBeenSet)
        {

           double effectiveWidth = (this.AreaFinishBase.RollWidthInInches - this.AreaFinishBase.OverlapInInches) / 12.0;
           
            this.lblRollSqrYrds.Text =
                '(' + effectiveWidth.ToString("0.00") + ")  "
                + Utilities.GetFeet(AreaFinishBase.RollWidthInInches).ToString() + "'-"
                + Utilities.GetInch(AreaFinishBase.RollWidthInInches) + "\" x "
                + Utilities.GetFeet(AreaFinishBase.RolloutLengthInInches).ToString("#,##0") + "'-"
                + Utilities.GetInch(AreaFinishBase.RolloutLengthInInches).ToString("0") + '"';

            // We only populate the running tallys if the scale has been set.

            if (scaleHasBeenSet)
            {
                double grossAreaInSqrFeet = AreaFinishBase.GrossAreaInSqrInches / 144.0;
                double netAreaInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
                double perimeterInFeet = AreaFinishBase.PerimeterInInches / 12.0;
                
                double? wasteRatio = AreaFinishBase.WasteRatio;

                double wastePct = 0.0;

                if (wasteRatio.HasValue)
                {
                    if (wasteRatio.Value < 0)
                    {
                        this.lblWasteValue.ForeColor = Color.Red;
                    }

                    else
                    {
                        this.lblWasteValue.ForeColor = Color.Black;
                    }

                    wastePct = wasteRatio.Value * 100.0;

                    //grossAreaInSqrFeet *= (1.0 + wasteRatio.Value);

                    this.lblWasteValue.Text = wastePct.ToString("0.00") + '%';
                }

                else
                {
                    this.lblWasteValue.Text = string.Empty;
                }

                double usedAreaInSqrFeet = 0;

                if (AreaFinishBase.MaterialsType == MaterialsType.Tiles)
                {
                    usedAreaInSqrFeet = grossAreaInSqrFeet;
                }

                else
                {
                    usedAreaInSqrFeet = netAreaInSqrFeet;
                }

                if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
                {
                    lblNetAreaValue.Text = usedAreaInSqrFeet.ToString("#,##0.0") + " s.f.";

                    lblPerimValue.Text = perimeterInFeet.ToString("#,##0.0") + " f. ";
                }

                else
                {
                    lblNetAreaValue.Text = (usedAreaInSqrFeet / 9.0).ToString("#,##0.0") + " s.y.";

                    lblPerimValue.Text = (perimeterInFeet / 3.0).ToString("#,##0.0") + " y. ";
                }

            }

            else
            {
                ClearTallyDisplays();
            }

            this.lblCountValue.Text = AreaFinishBase.Count.ToString();

            this.pnlRollSqrYrds.Invalidate();
            this.lblWasteValue.Invalidate();
        }

        public UCAreaFinishPalette baseAreaFinishPalette;

        internal void ClearTallyDisplays()
        {
            this.lblNetAreaValue.Text = string.Empty; // 0.ToString("#,##0.0"); 
            //this.lblGrossAreaValue.Text = string.Empty; //
            this.lblPerimValue.Text = string.Empty; // 0.ToString("#,##0.0");
            this.lblWasteValue.Text = string.Empty; //0.ToString("0.0") + '%'; ;
        }


        private int _positionOnPalette = -1;

        public int PositionOnPalette
        {
            get
            {
                return _positionOnPalette;
            }

            set
            {
                _positionOnPalette = value;

                this.lblShortCut.Text = (value + 1).ToString();
            }
        }

#region Constructors

        public static int IndexCounter = 1;

        public int Index;

        public UCAreaFinishPaletteElement()
        {
            Index = IndexCounter++;
        }

        public UCAreaFinishPaletteElement(
            UCAreaFinishPalette baseFinishPalette
            , GraphicsWindow window
            , GraphicsPage page
            , AreaFinishBase areaFinishBase
            )
        {
            InitializeComponent();

            this.Window = window;

            this.Page = page;

            Index = IndexCounter++;

            this.baseAreaFinishPalette = baseFinishPalette;
            this.AreaFinishBase = areaFinishBase;

            this.pnlColor.BackColor = this.AreaFinishBase.Color;
            this.lblName.Text = this.AreaFinishBase.AreaName;
            this.trbOpacity.Value = (int) Math.Min(100, Math.Max(0, 100.0 * AreaFinishBase.Opacity));

            lblTileTrimFactor.Text = this.AreaFinishBase.TrimInInches.ToString() + '"';

            //lblOpacityValue.Text = this.trbOpacity.Value.ToString() + '%';

            //this.pnlTileTrimFactor.Location = new Point(4, 68);
            //this.pnlTileTrimFactor.Size = new Size(256, 31);

            this.pnlRollSqrYrds.Location = this.pnlTileTrimFactor.Location;
            this.pnlRollSqrYrds.Size = this.pnlTileTrimFactor.Size;

            this.lblRollSqrYrds.Size = new Size(254, 28);
            this.lblTrimFactor.Size = new Size(254, 28);

            if (AreaFinishBase.MaterialsType == MaterialsType.Tiles)
            {
                this.pnlRollSqrYrds.Visible = false;
                this.pnlTileTrimFactor.Visible = true;
                this.pnlTileTrimFactor.BringToFront();
                this.lblNetAreaTitle.Text = "Gross Area:";
            }

            else
            {
                this.pnlRollSqrYrds.Visible = true;
                this.pnlTileTrimFactor.Visible = false;
                this.pnlRollSqrYrds.BringToFront();
                this.lblNetAreaTitle.Text = "Net Area:";
            }

            //lblGuid.Text = Guid;

            setupTallyDisplay(SystemState.ScaleHasBeenSet);

            this.lblRollSqrYrds.Invalidate();

            this.lblWasteValue.Invalidate();

            //AreaFinishManager.UpdateBaseColor(this.AreaFinishBase.Color);

            //AreaFinishManager.UpdateOpacity(this.AreaFinishBase.Color);

            this.Click += UCFinish_Click;
            this.pnlColor.Click += PnlColor_Click;

            this.pnlColor.Paint += PnlColor_Paint;

            this.trbOpacity.ValueChanged += TrbTransparency_ValueChanged;

            AreaFinishBase.OpacityChanged += AreaFinishBase_OpacityChanged;
            AreaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
            AreaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            AreaFinishBase.TileTrimChanged += AreaFinishBase_TileTrimChanged;
            AreaFinishBase.MaterialsTypeChanged += AreaFinishBase_MaterialsTypeChanged;
            AreaFinishBase.RollWidthChanged += AreaFinishBase_RollWidthChanged;
            AreaFinishBase.RolloutLengthChanged += AreaFinishBase_RolloutLengthChanged;
            AreaFinishBase.FilteredChanged += AreaFinishBase_FilteredChanged;
            AreaFinishBase.FinishStatsUpdated += AreaFinishBase_FinishStatsUpdated;
            AreaFinishBase.AreaDisplayUnitsChanged += AreaFinishBase_AreaDisplayUnitsChanged;
            AreaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;
            AreaFinishBase.FinishSeamGraphicsChanged += AreaFinishBase_FinishSeamGraphicsChanged;
            
        }

        private void AreaFinishBase_AreaDisplayUnitsChanged(AreaFinishBase finishBase, AreaDisplayUnits areaDisplayUnits)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_FinishSeamGraphicsChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_FinishSeamChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, int count, double netAreaInSqrInches, double grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            if (filtered)
            {
                this.BackColor = Color.LightGray;
            }

            else
            {
                this.BackColor = Color.White;
            }

            //baseAreaFinishPalette.AreaFinishManagerList.SetupSeamFilters();
        }

        private void AreaFinishBase_OpacityChanged(AreaFinishBase finishBase, double opacity)
        {
            //this.lblOpacityValue.Text = (100.0 * opacity).ToString("0") + "%";
        }

        private void AreaFinishBase_AreaNameChanged(AreaFinishBase finishBase, string areaName)
        {
            this.lblName.Text = areaName;
        }

        private void AreaFinishBase_ColorChanged(AreaFinishBase finishBase, Color color)
        {
            this.pnlColor.BackColor = color;
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_RolloutLengthChanged(AreaFinishBase finishBase, double rollOutLengthInInches)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_RollWidthChanged(AreaFinishBase finishBase, double rollWidthInInches)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_TileTrimChanged(AreaFinishBase finishBase, double tileTrimInInches)
        {
            this.lblTileTrimFactor.Text = tileTrimInInches.ToString() + '"';
        }

        private void AreaFinishBase_MaterialsTypeChanged(AreaFinishBase finishBase, MaterialsType materialsType)
        {
            if (materialsType == MaterialsType.Tiles)
            {
                this.pnlTileTrimFactor.Visible = true;
                this.pnlRollSqrYrds.Visible = false;
                this.pnlTileTrimFactor.BringToFront();
                this.lblNetAreaTitle.Text = "Gross Area:";
              
            }

            else
            {
                this.pnlTileTrimFactor.Visible = false;
                this.pnlRollSqrYrds.Visible = true;
                this.pnlRollSqrYrds.BringToFront();
                this.lblNetAreaTitle.Text = "Net Area:";

                //calculateRollWasteFactor(RollWidthInInches);
            }
        }

        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
            if (this.seamFinishBase == null)
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

            pen = new Pen(this.seamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.seamFinishBase.VisioDashType];

            double sizeX = this.pnlColor.Width;

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, 0.1F, (float)yOffset, (float)sizeX - 0.2F, (float)yOffset);

                yOffset += yIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        private void TrbTransparency_ValueChanged(object sender, EventArgs e)
        {
            double opacity = Math.Min(1.0, Math.Max(0, ((double)this.trbOpacity.Value) / 100.0));

            AreaFinishBase.Opacity = opacity;

            byte A = (byte) Math.Round(Math.Max(Math.Min(255.0 * this.Opacity, 255), 0.0));

            Color updtColor = Color.FromArgb((int)A, AreaFinishBase.Color);

            AreaFinishBase.Color = updtColor;

            //canvasManager.BaseForm.pnlFinishColor.BackColor = updtColor;
        }

#endregion

#region Events

        //public delegate void FinishChangedHandler(UCAreaFinishPaletteElement ucAreaFinish);

        //public event FinishChangedHandler FinishChanged;

        public delegate void ControlClickedHandler(UCAreaFinishPaletteElement sender);

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

        public void SetSelected(bool selected)
        {
            AreaFinishBase.Selected = selected;

            updateSelectedFormat();
        }

        //public void SetTrimInInches(double trimInInches)
        //{
        //    this.lblTileTrimFactor.Text = trimInInches.ToString() + '"';

        //    AreaFinishBase.TrimInInches = trimInInches;

        //    AreaFinishManager.UpdateFinishStats();
        //}

        private void updateSelectedFormat()
        {
            FontStyle fs = (AreaFinishBase.Selected) ? FontStyle.Bold : FontStyle.Regular;

            lblNetAreaTitle.Font = new Font(lblNetAreaTitle.Font, fs);
            lblPerimTitle.Font = new Font(lblPerimTitle.Font, fs);
            lblWasteTitle.Font = new Font(lblWasteTitle.Font, fs);
            //lblGrossAreaTitle.Font = new Font(lblGrossAreaTitle.Font, fs);
            lblCountTitle.Font = new Font(lblCountTitle.Font, fs);

            lblNetAreaValue.Font = new Font(lblNetAreaValue.Font, fs);
            lblPerimValue.Font = new Font(lblPerimValue.Font, fs);
            lblWasteValue.Font = new Font(lblWasteValue.Font, fs);
            //lblGrossAreaValue.Font = new Font(lblGrossAreaValue.Font, fs);
            lblCountValue.Font = new Font(lblCountValue.Font, fs);

            lblShortCut.Font = new Font(lblShortCut.Font, fs);

            lblName.Font = new Font(lblName.Font, fs);

            this.Invalidate();

            this.Refresh();
        }

      
        internal void UpdateSeam()
        {
            this.pnlColor.Invalidate();
        }

        internal void Delete()
        {
            this.Click  -= UCFinish_Click;
            this.pnlColor.Click  -= PnlColor_Click;

            this.pnlColor.Paint  -= PnlColor_Paint;

            this.trbOpacity.ValueChanged  -= TrbTransparency_ValueChanged;

            AreaFinishBase.OpacityChanged  -= AreaFinishBase_OpacityChanged;
            AreaFinishBase.AreaNameChanged  -= AreaFinishBase_AreaNameChanged;
            AreaFinishBase.ColorChanged  -= AreaFinishBase_ColorChanged;
            AreaFinishBase.TileTrimChanged  -= AreaFinishBase_TileTrimChanged;
            AreaFinishBase.MaterialsTypeChanged  -= AreaFinishBase_MaterialsTypeChanged;
            AreaFinishBase.RollWidthChanged  -= AreaFinishBase_RollWidthChanged;
            AreaFinishBase.RolloutLengthChanged  -= AreaFinishBase_RolloutLengthChanged;
            AreaFinishBase.FilteredChanged  -= AreaFinishBase_FilteredChanged;
            AreaFinishBase.FinishStatsUpdated  -= AreaFinishBase_FinishStatsUpdated;
            AreaFinishBase.AreaDisplayUnitsChanged -= AreaFinishBase_AreaDisplayUnitsChanged;
            AreaFinishBase.FinishSeamChanged  -= AreaFinishBase_FinishSeamChanged;
            AreaFinishBase.FinishSeamGraphicsChanged  -= AreaFinishBase_FinishSeamGraphicsChanged;
        }

    }
}
