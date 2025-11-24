

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

    using Visio = Microsoft.Office.Interop.Visio;
    using Utilities;

    public partial class UCSeamPalletElement : UserControl
    {
        public string Guid;

        private UCSeamFinishPallet ucSeamPallet;

        public SeamFinishBase SeamFinish;

        public string SeamName
        {
            get
            {
                return SeamFinish.SeamName;
            }

            set
            {
                SeamFinish.SeamName = value;
            }
        }

        public int VisioDashType
        {
            get
            {
                return SeamFinish.VisioDashType;
            }

            set
            {
                SeamFinish.VisioDashType = value;
            }
        }

        public double LineWidthInPts
        {
            get
            {
                return SeamFinish.LineWidthInPts;
            }

            set
            {
                SeamFinish.LineWidthInPts = value;
            }
        }

        public Color SeamColor
        {
            get
            {
                return SeamFinish.SeamColor;
            }

            set
            {
                SeamFinish.A = value.A;
                SeamFinish.R = value.R;
                SeamFinish.G = value.G;
                SeamFinish.B = value.B;
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

        public Visio.Layer AreaModeLayer { get; set; }
        public Visio.Layer LineModeLayer { get; set; }

        public int sIndex = -1;

        internal bool bIsFiltered;

        public UCSeamPalletElement(UCSeamFinishPallet ucSeamPallet)
        {
            InitializeComponent();

            this.Guid = GuidMaintenance.CreateGuid(this);

            this.pnlColor.Paint += PnlColor_Paint;

            this.ucSeamPallet = ucSeamPallet;

            this.Click += UCSeam_Click;
            this.pnlColor.Click += PnlColor_Click;
        }

        private void PnlColor_Click(object sender, EventArgs e)
        {
            UCSeam_Click(this, e);
        }

        private void UCSeam_Click(object sender, EventArgs e)
        {
            ucSeamPallet.SelectedSeam = this;
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

            if (this.SeamFinish != null)
            {
                pen = new Pen(this.SeamFinish.SeamColor);
            }

            else
            {
                pen = new Pen(Color.Red);
            }

            // Set the width
            pen.Width = (float)1.5F; // this.FinishSeamLine.LineWeight;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            if (this.SeamFinish != null)
            {
                pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.SeamFinish.VisioDashType];
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

    }
}
