

namespace FloorMaterialEstimator.Finish_Controls
{
    using System; 
    using System.Drawing;  
    using System.Windows.Forms;
    using FinishesLib;
    using Graphics;

    public partial class UCSeamPaletteElement : UserControl, IDisposable
    {
        public string Guid;

        private UCSeamFinishPalette ucSeamPalette;

        public SeamFinishBase SeamFinishBase;

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public string SeamName
        {
            get
            {
                return SeamFinishBase.SeamName;
            }

            set
            {
                SeamFinishBase.SeamName = value;
                this.lblSeamName.Text = value;
            }
        }

        public int VisioDashType
        {
            get
            {
                return SeamFinishBase.VisioDashType;
            }

            set
            {
                SeamFinishBase.VisioDashType = value;
            }
        }

        public double LineWidthInPts
        {
            get
            {
                return SeamFinishBase.SeamWidthInPts;
            }

            set
            {
                SeamFinishBase.SeamWidthInPts = value;
            }
        }

        public Color SeamColor
        {
            get
            {
                return SeamFinishBase.SeamColor;
            }

            set
            {
                SeamFinishBase.A = value.A;
                SeamFinishBase.R = value.R;
                SeamFinishBase.G = value.G;
                SeamFinishBase.B = value.B;
            }
        }

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

        public string Product { get; set; }
        public string Notes { get;  set; }

        //public SeamFinishManager SeamFinishManager { get; set; } = null;

        public int sIndex = -1;

        internal bool bIsFiltered;

        public UCSeamPaletteElement(GraphicsWindow window, GraphicsPage page, UCSeamFinishPalette ucSeamPalette)
        {
            InitializeComponent();

            this.ucSeamPalette = ucSeamPalette;

            this.SeamFinishBase = null;

            this.Window = window;

            this.Page = page;

            //this.SeamFinishManager = new SeamFinishManager(Window, Page, ucSeamPalette.BaseForm.SeamFinishBaseList, SeamFinishBase);

            this.pnlColor.Paint += PnlColor_Paint;

            this.Size = new Size(190, 74);

            this.Click += UCSeam_Click;
            this.pnlColor.Click += PnlColor_Click;
        }

        public UCSeamPaletteElement(GraphicsWindow window, GraphicsPage page, UCSeamFinishPalette ucSeamPalette, SeamFinishBase seamFinishBase, bool selected)
        {
            InitializeComponent();

            this.ucSeamPalette = ucSeamPalette;

            this.SeamFinishBase = seamFinishBase;

            this.Window = window;

            this.Page = page;

           // this.SeamFinishManager = new SeamFinishManager(Window, Page, ucSeamPalette.BaseForm.SeamFinishBaseList, SeamFinishBase);

            this.Selected = selected;

            this.pnlColor.Paint += PnlColor_Paint;


            this.Size = new Size(190-50, 74);

            this.Click += UCSeam_Click;
            this.pnlColor.Click += PnlColor_Click;

            this.SeamFinishBase.SeamGraphicsChanged += SeamFinishBase_SeamGraphicsChanged;
        }

        private void SeamFinishBase_SeamGraphicsChanged(SeamFinishBase SeamFinishBase)
        {
            this.pnlColor.Invalidate();
        }

        private void PnlColor_Click(object sender, EventArgs e)
        {
            UCSeam_Click(this, e);
        }

        private void UCSeam_Click(object sender, EventArgs e)
        {
            ucSeamPalette.SelectedSeam = this;
        }

        private void PnlColor_Paint(object sender, PaintEventArgs e)
        {
            double height = this.pnlColor.Height;
            double yIncmnt = height / 3.0;

            double yOffset = yIncmnt / 2.0 - 0.333F;

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen;

            if (this.SeamFinishBase != null)
            {
                pen = new Pen(this.SeamFinishBase.SeamColor);
            }

            else
            {
                pen = new Pen(Color.Red);
            }

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            if (this.SeamFinishBase != null)
            {
                pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.SeamFinishBase.VisioDashType];
            }

            else
            {
                pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[1];
            }
            
            double sizeX = this.pnlColor.Width;

            for (int i = 0; i < 3; i++)
            {
                e.Graphics.DrawLine(pen, 0.1F, (float) yOffset, (float) sizeX - 0.2F, (float) yOffset);

                yOffset += yIncmnt;
            }
     
            // Dispose of the custom pen.
            pen.Dispose();
        }

        private void updateSelectedFormat()
        {
            FontStyle fs = (Selected) ? FontStyle.Bold : FontStyle.Regular;

            lblWidthTitle.Font = new Font(lblWidthTitle.Font, fs);
            lblWidthValue.Font = new Font(lblWidthValue.Font, fs);

            lblLengthTitle.Font = new Font(lblLengthTitle.Font, fs);
            lblLengthValue.Font = new Font(lblLengthValue.Font, fs);

            lblSeamName.Font = new Font(lblSeamName.Font, fs);

            this.Refresh();
        }

        public void Delete()
        {
            this.pnlColor.Paint -= PnlColor_Paint;

            this.Click -= UCSeam_Click;
            this.pnlColor.Click -= PnlColor_Click;
        }
    }
}
