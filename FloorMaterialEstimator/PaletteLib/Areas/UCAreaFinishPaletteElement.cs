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

namespace PaletteLib
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

        public Size ExpandedSizeWithNoRollout;

        public Size ExpandedSizeWithRollout;

        public  Size CompressedSize;

        private void setupTallyDisplay(bool scaleHasBeenSet)
        {

            // We only populate the running tallys if the scale has been set.

            if (scaleHasBeenSet)
            {
                double? grossAreaInSqrFeet = null;

                if (AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    grossAreaInSqrFeet =
                        AreaFinishBase.GrossAreaInSqrInches.HasValue ?
                        AreaFinishBase.GrossAreaInSqrInches / 144.0 : null;
                }
                
                else
                {
                      grossAreaInSqrFeet = AreaFinishBase.TileAreaGrossAreaInSqrInches / 144.0 ;
                }
                
                
                double netAreaInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
                double perimeterInFeet = AreaFinishBase.PerimeterInInches / 12.0;
                
                double? wasteRatio = AreaFinishBase.WastePct;

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

                    this.lblWasteValue.Text = Math.Round(wastePct,1).ToString("0.0") + '%';
                }

                else
                {
                    this.lblWasteValue.Text = string.Empty;
                }

                double usedAreaInSqrFeet = 0;

                //if (AreaFinishBase.MaterialsType == MaterialsType.Tiles)
                //{
                //    if (grossAreaInSqrFeet.HasValue)
                //    {
                //        usedAreaInSqrFeet = grossAreaInSqrFeet.Value;
                //    }
                    
                //}

                //else
                //{
                //    usedAreaInSqrFeet = netAreaInSqrFeet;
                //}

                usedAreaInSqrFeet = netAreaInSqrFeet;

                if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
                {
                    if (grossAreaInSqrFeet.HasValue)
                    {
                        this.lblGrossAreaValue.Text = grossAreaInSqrFeet.Value.ToString("#,##0.0") + " s.f.";
                    }

                    else
                    {
                        this.lblGrossAreaValue.Text = string.Empty;
                    }

                    lblNetAreaValue.Text = usedAreaInSqrFeet.ToString("#,##0.0") + " s.f.";

                    //lblPerimValue.Text = perimeterInFeet.ToString("#,##0.0") + " f. ";
                }

                else
                {
                    if (grossAreaInSqrFeet.HasValue)
                    {
                        this.lblGrossAreaValue.Text = (grossAreaInSqrFeet.Value / 9.0).ToString("#,##0.0") + " s.f.";
                    }

                    else
                    {
                        this.lblGrossAreaValue.Text = string.Empty;
                    }

                    lblNetAreaValue.Text = (usedAreaInSqrFeet / 9.0).ToString("#,##0.0") + " s.y.";

                    //lblPerimValue.Text = (perimeterInFeet / 3.0).ToString("#,##0.0") + " y. ";
                }

                if (AreaFinishBase.MaterialsType == MaterialsType.Rolls && grossAreaInSqrFeet.HasValue)
                {

                    double effectiveWidthInFeet = (this.AreaFinishBase.RollWidthInInches - this.AreaFinishBase.OverlapInInches) / 12.0;

                    double effectiveLengthInInches = (144.0 * grossAreaInSqrFeet.Value) / this.AreaFinishBase.RollWidthInInches;

                    this.lblRollSqrYrds.Text =
                        '(' + effectiveWidthInFeet.ToString("0.00") + ")  "
                        + Utilities.GetFeet(AreaFinishBase.RollWidthInInches).ToString() + "'-"
                        + Utilities.GetInch(AreaFinishBase.RollWidthInInches) + "\" x "
                        + Utilities.GetFeet(effectiveLengthInInches).ToString("#,##0") + "'-"
                        + Utilities.GetInch(effectiveLengthInInches).ToString("0") + '"';

                    this.Size = ExpandedSizeWithRollout;

                    this.Invalidate();
                }

                else
                {
                    this.lblRollSqrYrds.Text = string.Empty;

                    this.Size = ExpandedSizeWithNoRollout;

                    this.Invalidate();
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
            this.lblGrossAreaValue.Text = string.Empty;
            this.lblNetAreaValue.Text = string.Empty; // 0.ToString("#,##0.0"); 
            //this.lblPerimValue.Text = string.Empty; // 0.ToString("#,##0.0");
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

                if (value > 8)
                {
                    this.lblShortCut.Text = string.Empty;
                    this.lblShortCut.Visible = false;
                }

                else
                {
                    this.lblShortCut.Text = (value + 1).ToString();
                    this.lblShortCut.Visible = true;
                }
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

            ExpandedSizeWithNoRollout = new Size(this.Width, (int) (this.Height / 1.45));

            ExpandedSizeWithRollout = new Size(this.Width, (int)(this.Height / 1.4));

            CompressedSize = new Size(this.Width, (int) Math.Round((double) this.Height / 1.85));

            if (SystemState.AreaPaletteState == AreaPaletteState.Compressed)
            {
                this.Size = CompressedSize;
            }

            else
            {
                this.Size = ExpandedSizeWithNoRollout;
            }

            this.baseAreaFinishPalette = baseFinishPalette;
            this.AreaFinishBase = areaFinishBase;

            this.pnlColor.BackColor = this.AreaFinishBase.Color;
            this.lblName.Text = this.AreaFinishBase.AreaName;
            this.trbTransparency.Value = (int) Math.Min(100, Math.Max(0, 100.0 * AreaFinishBase.Transparency));

            lblTileTrimFactor.Text = this.AreaFinishBase.TrimInInches.ToString() + '"';

            //this.pnlRollSqrYrds.Location = this.pnlTileTrimFactor.Location;
            this.pnlRollSqrYrds.Size = this.pnlTileTrimFactor.Size;

            this.lblRollSqrYrds.Size = new Size(254, 28);
            this.lblTrimFactor.Size = new Size(254, 28);

            if (AreaFinishBase.MaterialsType == MaterialsType.Tiles)
            {
                this.pnlRollSqrYrds.Visible = false;
                this.pnlTileTrimFactor.Visible = true;
                this.pnlTileTrimFactor.BringToFront();
            }

            else
            {
                this.pnlRollSqrYrds.Visible = true;
                this.pnlTileTrimFactor.Visible = false;
                this.pnlRollSqrYrds.BringToFront();
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

            this.trbTransparency.ValueChanged += TrbTransparency_ValueChanged;

            this.Dock = DockStyle.None;

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
            AreaFinishBase.PatternChanged += AreaFinishBase_PatternChanged;
            this.toolTip1.SetToolTip(trbTransparency, trbTransparency.Value.ToString()+ '%');
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

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_FilteredChanged(AreaFinishBase finishBase, bool filtered)
        {
            SetFiltered(filtered);

            UpdateAreaPaletteState(SystemState.AreaPaletteState);
        }

        private void AreaFinishBase_OpacityChanged(AreaFinishBase finishBase, double opacity)
        {
            this.pnlColor.Invalidate();
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

        private void AreaFinishBase_PatternChanged(AreaFinishBase finishBase, int pattern)
        {
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

                double? wasteRatio = finishBase.CalculateTileWasteFactor(SystemState.ScaleHasBeenSet);

                if (wasteRatio is null)
                {
                    this.lblWasteValue.Text = string.Empty;
                }

                else
                {
                    if (wasteRatio.Value< 0)
                    {
                        this.lblWasteValue.ForeColor= Color.Red;
                    }

                    else
                    {
                        this.lblWasteValue.ForeColor = Color.Black;
                    }

                    this.lblWasteValue.Text = Math.Round(wasteRatio.Value * 100.0,1).ToString("0.0");
                }

                this.lblWasteValue.BringToFront();

            }

            else
            {
                this.pnlTileTrimFactor.Visible = false;
                this.pnlRollSqrYrds.Visible = true;
                this.pnlRollSqrYrds.BringToFront();

                this.lblRollSqrYrds.BringToFront();
                
            }

        }

        PaintEventArgs e1 = null;

        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
            e1 = e;

            switch (AreaFinishBase.Pattern)
            {
                case 1:

                    HashedPanelPainter.PaintHorizontalHashedPanel(this.pnlColor, 6, AreaFinishBase.Color, e);

                    this.pnlColor.BackColor = Color.FromArgb(0, 255, 255, 255);

                    break;

                case 2:

                    HashedPanelPainter.PaintVerticalHashedPanel(this.pnlColor, 8, AreaFinishBase.Color, e);

                    this.pnlColor.BackColor = Color.FromArgb(0, 255, 255, 255);

                    break;

                case 3:

                    HashedPanelPainter.PaintCrossHashedPanel(this.pnlColor, 6, 8, AreaFinishBase.Color, e);

                    this.pnlColor.BackColor = Color.FromArgb(0, 255, 255, 255);

                    break;

                case 0:
                default:

                    this.pnlColor.ForeColor = AreaFinishBase.Color;
                    this.pnlColor.BackColor = AreaFinishBase.Color;

                    break;
            }

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
            double transparency = Math.Min(1.0, Math.Max(0, ((double)this.trbTransparency.Value) / 100.0));

            AreaFinishBase.Transparency = transparency;

            this.toolTip1.SetToolTip(trbTransparency, trbTransparency.Value.ToString() + '%');

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

        public void SetFiltered()
        {
            SetFiltered(AreaFinishBase.Filtered);
        }

        public void SetFiltered(bool filtered)
        {
            if (filtered)
            {
                this.BackColor = Color.LightGray;
            }

            else
            {
                this.BackColor = Color.White;
            }
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

            lblGrossAreaTitle.Font = new Font(lblGrossAreaTitle.Font, fs);
            lblNetAreaTitle.Font = new Font(lblNetAreaTitle.Font, fs);
            //lblPerimTitle.Font = new Font(lblPerimTitle.Font, fs);
            lblWasteTitle.Font = new Font(lblWasteTitle.Font, fs);

            lblCountTitle.Font = new Font(lblCountTitle.Font, fs);

            lblGrossAreaValue.Font = new Font(lblGrossAreaValue.Font, fs);
            lblNetAreaValue.Font = new Font(lblNetAreaValue.Font, fs);
            //lblPerimValue.Font = new Font(lblPerimValue.Font, fs);
            lblWasteValue.Font = new Font(lblWasteValue.Font, fs);
            
            lblCountValue.Font = new Font(lblCountValue.Font, fs);

            lblShortCut.Font = new Font(lblShortCut.Font, fs);

            lblName.Font = new Font(lblName.Font, fs);

            this.Invalidate();

            this.Refresh();
        }

        internal void UpdateAreaPaletteState(AreaPaletteState areaPaletteState)
        {
            if (areaPaletteState == AreaPaletteState.Expanded)
            {
                if (string.IsNullOrEmpty(this.lblRollSqrYrds.Text))
                {
                    this.Size = ExpandedSizeWithNoRollout;
                }
                
                else
                {
                    this.Size = ExpandedSizeWithRollout;
                }

                this.Invalidate();

                return;
            }

            if (areaPaletteState == AreaPaletteState.Compressed)
            {
                if (AreaFinishBase.Filtered)
                {
                    this.Height = 0;
                }

                else
                {
                    this.Size = CompressedSize;
                }

                return;
            }
        }

        public void UpdateSeam()
        {
            this.pnlColor.Invalidate();
        }

        internal void Delete()
        {
            this.Click  -= UCFinish_Click;
            this.pnlColor.Click  -= PnlColor_Click;

            this.pnlColor.Paint  -= PnlColor_Paint;

            this.trbTransparency.ValueChanged  -= TrbTransparency_ValueChanged;

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
