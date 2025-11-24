

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

    public partial class UCSeamListElement : UserControl, IDisposable
    {
        //public UCSeam ucSeam;

        public SeamFinishBase SeamFinishBase;

        public string Guid => SeamFinishBase is null ? string.Empty : SeamFinishBase.Guid;

        public string SeamName
        {
            get
            {
                if (SeamFinishBase == null)
                {
                    return string.Empty;
                }

                return SeamFinishBase.SeamName;
            }

            set
            {
                if (SeamFinishBase == null)
                {
                    return;
                }

                SeamFinishBase.SeamName = value;
                this.lblSeamName.Text = value;
            }
        }

        public double LineWidthInPts
        {
            get
            {
                if (SeamFinishBase == null)
                {
                    return 0;
                }

                return SeamFinishBase.SeamWidthInPts;
            }

            set
            {
                if (SeamFinishBase == null)
                {
                    return;
                }

                SeamFinishBase.SeamWidthInPts = value;
            }
        }

        public int VisioDashType
        {
            get
            {
                if (SeamFinishBase == null)
                {
                    return 1;
                }

                return SeamFinishBase.VisioDashType;
            }

            set
            {
                if (SeamFinishBase == null)
                {
                    return;
                }

                SeamFinishBase.VisioDashType = value ;
            }
        }

        public string Product
        {
            get
            {
                if (SeamFinishBase == null)
                {
                    return string.Empty;
                }

                return SeamFinishBase.Product;
            }

            set
            {
                if (SeamFinishBase == null)
                {
                    return;
                }

                SeamFinishBase.Product = value;
            }
        }

        public string Notes
        {
            get
            {
                if (SeamFinishBase == null)
                {
                    return string.Empty;
                }

                return SeamFinishBase.Notes;
            }

            set
            {
                if (SeamFinishBase == null)
                {
                    return;
                }

                SeamFinishBase.Notes = value;
            }
        }

        public int lineListIndex = 0;

        public UCSeamListElement(SeamFinishBase finishSeamBase, int lineListIndex)
        {
            InitializeComponent();

            this.SeamFinishBase = finishSeamBase;

            this.lineListIndex = lineListIndex;

            this.SizeChanged += UCLineListElement_SizeChanged;
            this.lblSeamName.Click += LblLineName_Click;
            this.Paint += UCLineListElement_Paint;

            finishSeamBase.SeamGraphicsChanged += FinishSeamBase_SeamGraphicsChanged;
        }

        private void FinishSeamBase_SeamGraphicsChanged(SeamFinishBase SeamFinishBase)
        {
            this.Invalidate();
        }

        private void UCLineListElement_SizeChanged(object sender, EventArgs e)
        {

        }
        
        private void LblLineName_Click(object sender, EventArgs e)
        {
            OnClick(null);
        }

        private void UCLineListElement_Paint(object sender, PaintEventArgs e)
        {
            if (SeamFinishBase == null)
            {
                return;
            }

            this.lblSeamName.Text = SeamFinishBase.SeamName;

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(SeamFinishBase.SeamColor);

            // Set the width
            pen.Width = (float)SeamFinishBase.SeamWidthInPts;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[SeamFinishBase.VisioDashType];

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[SeamFinishBase.VisioDashType];

            e.Graphics.DrawLine(pen, 12.0F, 40.0F, this.Width - 24, 40.0F);

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public void Select()
        {
            this.lblSeamName.Font = new Font(this.lblSeamName.Font, FontStyle.Bold);
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void Deselect()
        {
            this.lblSeamName.Font = new Font(this.lblSeamName.Font, FontStyle.Regular);
            this.BorderStyle = BorderStyle.None;
        }

        public void Dispose()
        {
            SeamFinishBase.SeamGraphicsChanged -= FinishSeamBase_SeamGraphicsChanged;
        }
    }
}
