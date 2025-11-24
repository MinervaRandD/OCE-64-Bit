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

using OversUnders;

namespace PaletteLib
{
    
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using FinishesLib;
    using Globals;
    
    using Utilities;
    
    public partial class UCSeamAreaFinishPaletteElement : UserControl, IDisposable
    {
       // AreaFinishBaseList areaFinishBaseList => canvasManager.BaseForm.AreaFinishBaseList;

        //public List<MaterialArea> oversMaterialAreaList = new List<MaterialArea>();
        //public List<MaterialArea> undrsMaterialAreaList = new List<MaterialArea>();

        public const int CntlSizeY = 160;

        public AreaFinishBase AreaFinishBase;
        
        public string Guid => AreaFinishBase.Guid;

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
        
        private double effectiveWidth => (this.AreaFinishBase.RollWidthInInches - this.AreaFinishBase.OverlapInInches) / 12.0;


        private bool opacityChanged(Color currColor, Color updtColor)
        {
            return currColor.A != updtColor.A;
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

        private bool filtered = false;

        public SeamFinishBase SeamFinishBase
        {
            get
            {
                return AreaFinishBase.SeamFinishBase;
            }

            set
            {
                AreaFinishBase.SeamFinishBase = value;
            }
        }

        public UCSeamFinishPalette baseSeamFinishPalette;

        //public CanvasManager canvasManager
        //{
        //    get
        //    {
        //        return baseSeamFinishPalette.CanvasManager;
        //    }
        //}

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

        public int OverlapInInches
        {
            get
            {
                return AreaFinishBase.OverlapInInches;
            }

            set
            {
                AreaFinishBase.OverlapInInches = value;
            }
        }

        public double RollRepeatWidthInInches
        {
            get
            {
                return AreaFinishBase.RollRepeatWidthInInches;
            }

            set
            {
                AreaFinishBase.RollRepeatWidthInInches = value;
            }
        }

        public double RollRepeatLengthInInches
        {
            get
            {
                return AreaFinishBase.RollRepeatLengthInInches;
            }

            set
            {
                AreaFinishBase.RollRepeatLengthInInches = value;
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

        public MaterialsType MaterialsType
        {
            get
            {
                return AreaFinishBase.MaterialsType;
            }

            set
            {
                AreaFinishBase.MaterialsType = value;
            }
        }

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

                this.lblShortCut.Text = (value + 1).ToString();
            }
        }

        #region Constructors

        public UCSeamAreaFinishPaletteElement(UCSeamFinishPalette baseFinishPalette, AreaFinishBase areaFinishBase)
        {
            InitializeComponent();

            this.AreaFinishBase = areaFinishBase;
           
            this.baseSeamFinishPalette = baseFinishPalette;

            this.pnlColor.BackColor = this.AreaFinishBase.Color;
            this.lblName.Text = this.AreaFinishBase.AreaName;
 
            //this.pnlRollSqrYrds.Location = new Point(16, 68);
            //this.pnlRollSqrYrds.Size = new Size(256, 31);

            this.lblRollSqrYrds.Size = new Size(254, 28);

            this.pnlRollSqrYrds.Visible = true;
            this.pnlRollSqrYrds.BringToFront();

            this.lblSeamValue.Text = areaFinishBase.SeamFinishBase != null ? areaFinishBase.SeamFinishBase.SeamName : string.Empty;

            //lblGuid.Text = Guid;

            setupTallyDisplay(SystemState.ScaleHasBeenSet);

            this.Click += UCFinish_Click;
            this.pnlColor.Click += PnlColor_Click;

            this.pnlColor.Paint += PnlColor_Paint;

            AreaFinishBase.AreaNameChanged += AreaFinishBase_AreaNameChanged;
            AreaFinishBase.ColorChanged += AreaFinishBase_ColorChanged;
            AreaFinishBase.RollWidthChanged += AreaFinishBase_RollWidthChanged;
            //areaFinishBase.AreaChanged += AreaFinishBase_AreaChanged;
            //areaFinishBase.PerimeterChanged += AreaFinishBase_PerimeterChanged;
            //areaFinishBase.WastePercentChanged += AreaFinishBase_WastePercentChanged;
            AreaFinishBase.RolloutLengthChanged += AreaFinishBase_RolloutLengthChanged;
            AreaFinishBase.MaterialsTypeChanged += AreaFinishBase_MaterialsTypeChanged;
            AreaFinishBase.FinishStatsUpdated += AreaFinishBase_FinishStatsUpdated;
            AreaFinishBase.AreaDisplayUnitsChanged += AreaFinishBase_AreaDisplayUnitsChanged;
            AreaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;
            AreaFinishBase.FinishSeamGraphicsChanged += AreaFinishBase_FinishSeamGraphicsChanged;
            AreaFinishBase.SeamAreaLockedChanged += AreaFinishBase_SeamAreaLockedChanged;
            SetSeamAreaLockStatus(AreaFinishBase.SeamAreaLocked);
        }

        private void AreaFinishBase_SeamAreaLockedChanged(AreaFinishBase finishBase, bool seamAreaLocked)
        {
            this.SetSeamAreaLockStatus(seamAreaLocked);
        }

        private void AreaFinishBase_AreaDisplayUnitsChanged(AreaFinishBase finishBase, AreaDisplayUnits areaDisplayUnits)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_FinishStatsUpdated(AreaFinishBase finishBase, int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
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

        private void AreaFinishBase_AreaChanged(AreaFinishBase finishBase, double area)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_PerimeterChanged(AreaFinishBase finishBase, double perimeter)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);        }

        private void AreaFinishBase_FinishSeamChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.lblSeamValue.Text = AreaFinishBase.SeamFinishBase != null ? AreaFinishBase.SeamFinishBase.SeamName : string.Empty;

            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_FinishSeamGraphicsChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_MaterialsTypeChanged(AreaFinishBase finishBase, MaterialsType materialsType)
        {
            this.baseSeamFinishPalette.UpdateSeamAreaPalette();
        }

        private void AreaFinishBase_RolloutLengthChanged(AreaFinishBase finishBase, double rollOutLengthInInches)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void AreaFinishBase_RollWidthChanged(AreaFinishBase finishBase, double rollWidthInInches)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet);
        }

        private void setupTallyDisplay(bool scaleHasBeenSet)
        {
           
            // We only populate the running tallys if the scale has been set.

            if (scaleHasBeenSet)
            {
                double? grossAreaInSqrFeet = null;

                if (AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
                {
                    grossAreaInSqrFeet = AreaFinishBase.GrossAreaInSqrInches / 144.0;
                    double netAreaInSqrFeet = AreaFinishBase.NetAreaInSqrInches / 144.0;
                    double perimeterInFeet = AreaFinishBase.PerimeterInInches / 12.0;

                    if (grossAreaInSqrFeet.HasValue)
                    {
                        lblGrossAreaValue.Text = grossAreaInSqrFeet.Value.ToString("#,##0.0") + " s.f.";
                    }

                    else
                    {
                        lblGrossAreaValue.Text = string.Empty;
                    }

                    lblNetAreaValue.Text = netAreaInSqrFeet.ToString("#,##0.0") + " s.f.";
                    lblPerimValue.Text = perimeterInFeet.ToString("#,##0.0") + " f.";
                }

                else
                {
                    grossAreaInSqrFeet = (AreaFinishBase.GrossAreaInSqrInches / 9) / 144.0; // Use net to start
                    
                    double netAreaInSqrYds = (AreaFinishBase.NetAreaInSqrInches / 9) / 144.0;
                    double perimeterInYds = (AreaFinishBase.PerimeterInInches / 3) / 12.0;

                    if (grossAreaInSqrFeet.HasValue)
                    {
                        lblGrossAreaValue.Text = grossAreaInSqrFeet.Value.ToString("#,##0.0") + " s.y.";
                    }

                    else
                    {
                        lblGrossAreaValue.Text = string.Empty;
                    }

                    lblNetAreaValue.Text = netAreaInSqrYds.ToString("#,##0.0") + " s.y.";
                    lblPerimValue.Text = perimeterInYds.ToString("#,##0.0") + " y.";
                    //double? wasteRatio = AreaFinishBase.WasteRatio;
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
                }

                else
                {
                    this.lblRollSqrYrds.Text = string.Empty;
                }
            }

            else
            {
                lblGrossAreaValue.Text = string.Empty;
                lblNetAreaValue.Text = string.Empty;
                lblPerimValue.Text = string.Empty;
            }


            this.pnlRollSqrYrds.Invalidate();
           
        }

        internal void ClearTallyDisplays()
        {
            lblNetAreaValue.Text = string.Empty;
            lblPerimValue.Text = string.Empty;
            this.lblRollSqrYrds.Text = string.Empty;
        }




        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
          
            switch (AreaFinishBase.Pattern)
            {
                case 6:

                    HashedPanelPainter.PaintHorizontalHashedPanel(this.pnlColor, 6, AreaFinishBase.Color, e);

                    this.pnlColor.BackColor = Color.FromArgb(0, 255, 255, 255);

                    break;

                case 7:

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

            if (this.SeamFinishBase == null)
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

            pen = new Pen(this.SeamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.SeamFinishBase.VisioDashType];

            double sizeX = this.pnlColor.Width;

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, 0.1F, (float)yOffset, (float)sizeX - 0.2F, (float)yOffset);

                yOffset += yIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }

        #endregion

        #region Events

        //public delegate void FinishChangedHandler(UCAreaFinishPaletteElement ucAreaFinish);

        //public event FinishChangedHandler FinishChanged;

        public delegate void ControlClickedHandler(UCSeamAreaFinishPaletteElement sender);

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

            lblGrossAreaTitle.Font = new Font(lblGrossAreaTitle.Font, fs);
            lblNetAreaTitle.Font = new Font(lblNetAreaTitle.Font, fs);
            lblPerimText.Font = new Font(lblPerimText.Font, fs);

            lblGrossAreaValue.Font = new Font(lblGrossAreaValue.Font, fs);
            lblNetAreaValue.Font = new Font(lblNetAreaValue.Font, fs);
            lblPerimValue.Font = new Font(lblPerimValue.Font, fs);

            lblShortCut.Font = new Font(lblShortCut.Font, fs);

            lblName.Font = new Font(lblName.Font, fs);

            this.Invalidate();

            this.Refresh();
        }

        internal void SetSeam(SeamFinishBase finishSeamBase)
        {
            int indx = this.PositionOnPalette;

            string guid = this.Guid;

            this.SeamFinishBase = finishSeamBase;

            this.pnlColor.Invalidate();

            //this.Invalidate();
        }


        internal void UpdateSeam()
        {
            this.pnlColor.Invalidate();
        }

        internal void Delete()
        {
           
            AreaFinishBase.AreaNameChanged -= AreaFinishBase_AreaNameChanged;
            AreaFinishBase.ColorChanged -= AreaFinishBase_ColorChanged;
            AreaFinishBase.RolloutLengthChanged -= AreaFinishBase_RolloutLengthChanged;
            AreaFinishBase.RollWidthChanged -= AreaFinishBase_RollWidthChanged;
            AreaFinishBase.MaterialsTypeChanged -= AreaFinishBase_MaterialsTypeChanged;
            AreaFinishBase.MaterialsTypeChanged -= AreaFinishBase_MaterialsTypeChanged;
            AreaFinishBase.FinishSeamChanged -= AreaFinishBase_FinishSeamChanged;
            AreaFinishBase.FinishSeamGraphicsChanged -= AreaFinishBase_FinishSeamGraphicsChanged;
            AreaFinishBase.FinishStatsUpdated -= AreaFinishBase_FinishStatsUpdated;
        }

        private void UCSeamAreaFinishPaletteElement_Load(object sender, EventArgs e)
        {

        }

        private Image _seamAreaLockedPicture = null;

        private Image SeamAreaLockedPicture
        {
            get
            {
                if (_seamAreaLockedPicture == null)
                {
                    _seamAreaLockedPicture = Utilities.ByteArrayToImage(PaletteLib.Properties.Resources.SeamAreaLockedPicture);
                }

                return _seamAreaLockedPicture;
            }
        }

        private Image _seamAreaUnlockedPicture = null;

        private Image SeamAreaUnlockedPicture
        {
            get
            {
                if (_seamAreaUnlockedPicture == null)
                {
                    _seamAreaUnlockedPicture =
                        Utilities.ByteArrayToImage(PaletteLib.Properties.Resources.SeamAreaUnlockedPicture);
                }

                return _seamAreaUnlockedPicture;
            }
        }
        private void pbxSeamAreaLockedStatus_Click(object sender, EventArgs e)
        {
            if (this.AreaFinishBase.SeamAreaLocked)
            {
                this.AreaFinishBase.SeamAreaLocked = false;
            }

            else
            {
                this.AreaFinishBase.SeamAreaLocked = true;
            }

            SetSeamAreaLockStatus(this.AreaFinishBase.SeamAreaLocked);
        }

        public void SetSeamAreaLockStatus(bool seamAreaLocked)
        {
            if (seamAreaLocked)
            {
                this.pbxSeamAreaLockedStatus.Image = this.SeamAreaLockedPicture;
            }

            else
            {
                this.pbxSeamAreaLockedStatus.Image = this.SeamAreaUnlockedPicture;
            }
        }
    }
}
