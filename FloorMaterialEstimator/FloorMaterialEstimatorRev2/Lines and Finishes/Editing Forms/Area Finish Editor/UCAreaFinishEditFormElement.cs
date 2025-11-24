

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
    using FloorMaterialEstimator;
    using FinishesLib;
    using FloorMaterialEstimator.CanvasManager;

    public partial class UCAreaFinishEditFormElement : UserControl, IDisposable
    {
        private AreaFinishesEditForm baseForm;

        public AreaFinishManager AreaFinishManager => baseForm.AreaFinishManagerList.AreaFinishManagerDict[AreaFinishBase.Guid];

        public AreaFinishBase AreaFinishBase;

        public bool UseFullOpacity = false;

        public double OriginalSizeField = 0.0;

        public string OriginalAreaName = "";

        public UCAreaFinishEditFormElement(AreaFinishesEditForm baseForm, AreaFinishBase areaFinishBase, int areaListIndex)
        {
            InitializeComponent();
           
            this.baseForm= baseForm;

            
            this.AreaFinishBase = areaFinishBase;

            //this.AreaFinishManager = areaFinishBase.AreaFinishManager;

            this.positionOnPalette = areaListIndex;

            this.txbFinishName.Enabled = false;

            this.OriginalSizeField = areaFinishBase.RollRepeatLengthInInches;
            this.OriginalAreaName = areaFinishBase.AreaName;

            this.SizeChanged += UCAreaListElement_SizeChanged;

            this.Click += UCAreaListElement_Click;
            this.txbFinishName.Click += LblFinishName_Click;
            this.pnlColor.Click += PnlColor_Click;

            this.Paint += UCAreaListElement_Paint;
            this.pnlColor.Paint += PnlColor_Paint;

            this.AreaFinishBase.PatternChanged += AreaFinishBase_PatternChanged;
            this.AreaFinishBase.OpacityChanged += AreaFinishBase_OpacityChanged;
            this.AreaFinishBase.FinishSeamChanged += AreaFinishBase_FinishSeamChanged;
            this.AreaFinishBase.FinishSeamGraphicsChanged += AreaFinishBase_FinishSeamGraphicsChanged;
        }

        private void AreaFinishBase_PatternChanged(AreaFinishBase finishBase, int pattern)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_FinishSeamGraphicsChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_FinishSeamChanged(AreaFinishBase finishBase, SeamFinishBase seamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void AreaFinishBase_OpacityChanged(AreaFinishBase finishBase, double opacity)
        {
            UpdateColor();
        }

        public delegate void ControlClickedHandler(UCAreaFinishEditFormElement sender);

        public event ControlClickedHandler ControlClicked;

        private void UCAreaListElementClick(UCAreaFinishEditFormElement sender)
        {
            if (ControlClicked != null)
            {
                ControlClicked.Invoke(sender);
            }
        }

        private void UCAreaListElement_Click(object sender, EventArgs e)
        {
            UCAreaListElementClick(this);
        }

        private void LblFinishName_Click(object sender, EventArgs e)
        {
            UCAreaListElementClick(this);
        }

        private void PnlColor_Click(object sender, EventArgs e)
        {
            UCAreaListElementClick(this);
        }

        public Stack<Color> colorHistory = new Stack<Color>();


        public void UpdateColor()
        {
            this.pnlColor.Invalidate();
        }

        public string AreaName
        {
            get
            {
                if (AreaFinishBase == null)
                {
                    return string.Empty;
                }

                return AreaFinishBase.AreaName;
            }

            set
            {
                if (AreaFinishBase == null)
                {
                    return;
                }

                AreaFinishBase.AreaName = value;

                this.txbFinishName.Text = value;
            }
        }

        public string Product
        {
            get
            {
                if (AreaFinishBase == null)
                {
                    return string.Empty;
                }

                return AreaFinishBase.Product;
            }

            set
            {
                if (AreaFinishBase == null)
                {
                    return;
                }

                AreaFinishBase.Product = value;
            }
        }

        internal void UpdateSeam()
        {
            this.pnlColor.Invalidate();
        }

        public string Notes
        {
            get
            {
                if (AreaFinishBase == null)
                {
                    return string.Empty;
                }

                return AreaFinishBase.Notes;
            }

            set
            {
                if (AreaFinishBase == null)
                {
                    return;
                }

                AreaFinishBase.Notes = value;
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

        public int TileWidthFeet
        {
            get
            {
                return (int) Math.Floor(TileWidthInInches / 12.0);
            }
        }

        public double TileWidthInch
        {
            get
            {
                return TileWidthInInches - 12.0 * Math.Floor(TileWidthInInches / 12.0);
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

        public int TileHeightFeet
        {
            get
            {
                return (int)Math.Floor(TileHeightInInches / 12.0);
            }
        }

        public double TileHeightInch
        {
            get
            {
                return TileHeightInInches - 12.0 * Math.Floor(TileHeightInInches / 12.0);
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

        public int RollWidthFeet
        {
            get
            {
                return (int)Math.Floor(RollWidthInInches / 12.0);
            }
        }

        public double RollWidthInch
        {
            get
            {
                return RollWidthInInches - 12.0 * Math.Floor(RollWidthInInches / 12.0);
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
        public double RollRepeat1InInches
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

        public int RollRepeat1Feet
        {
            get
            {
                return (int)Math.Floor(RollRepeat1InInches / 12.0);
            }
        }

        public double RollRepeat1Inch
        {
            get
            {
                return RollRepeat1InInches - 12.0 * Math.Floor(RollRepeat1InInches / 12.0);
            }
        }

        public double RollRepeat2InInches
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

        public int RollRepeat2Feet
        {
            get
            {
                return (int)Math.Floor(RollRepeat2InInches / 12.0);
            }
        }

        public double RollRepeat2Inch
        {
            get
            {
                return RollRepeat2InInches - 12.0 * Math.Floor(RollRepeat2InInches / 12.0);
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

        public double? GrossAreaInSqrInches => AreaFinishBase.GrossAreaInSqrInches;

        public double NetAreaInSqrInches => AreaFinishBase.NetAreaInSqrInches;


        public double? Waste => AreaFinishBase.Waste;
       
        public int positionOnPalette = 0;

        private void UCAreaListElement_SizeChanged(object sender, EventArgs e)
        {
            
        }

      

        private void UCAreaListElement_Paint(object sender, PaintEventArgs e)
        {  
            this.txbFinishName.Text = AreaFinishBase.AreaName;
        }

        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
            switch (AreaFinishBase.Pattern)
            {
                case 6:

                    Utilities.HashedPanelPainter.PaintHorizontalHashedPanel(this.pnlColor, 10, AreaFinishBase.Color, e);

                    break;

                case 7:

                    Utilities.HashedPanelPainter.PaintVerticalHashedPanel(this.pnlColor, 10, AreaFinishBase.Color, e);

                    break;

                case 3:

                    Utilities.HashedPanelPainter.PaintCrossHashedPanel(this.pnlColor, 10, 10, AreaFinishBase.Color, e);

                    break;

                case 0:
                default:

                    if (UseFullOpacity)
                    {
                        this.pnlColor.BackColor = Color.FromArgb(AreaFinishBase.Color.R, AreaFinishBase.Color.G, AreaFinishBase.Color.B);
                    }

                    else
                    {
                        this.pnlColor.BackColor = AreaFinishBase.Color;
                    }

                    break;
            }
           

            if (this.AreaFinishBase.SeamFinishBase == null)
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

            pen = new Pen(this.AreaFinishBase.SeamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.AreaFinishBase.SeamFinishBase.VisioDashType];

            double sizeX = this.pnlColor.Width;

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, 0.1F, (float)yOffset, (float)sizeX - 0.2F, (float)yOffset);

                yOffset += yIncmnt;
            }

            // Dispose of the custom pen.
            pen.Dispose();
        }


        public void SelectElem()
        {
            this.txbFinishName.Font = new Font(this.txbFinishName.Font, FontStyle.Bold);
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void DeselectElem()
        {
            this.txbFinishName.Font = new Font(this.txbFinishName.Font, FontStyle.Regular);
            this.BorderStyle = BorderStyle.None;
        }

        public new void Dispose()
        {

            this.AreaFinishBase.OpacityChanged -= AreaFinishBase_OpacityChanged;
            this.AreaFinishBase.FinishSeamChanged -= AreaFinishBase_FinishSeamChanged;
            this.AreaFinishBase.FinishSeamGraphicsChanged -= AreaFinishBase_FinishSeamGraphicsChanged;
        }
    }
}
