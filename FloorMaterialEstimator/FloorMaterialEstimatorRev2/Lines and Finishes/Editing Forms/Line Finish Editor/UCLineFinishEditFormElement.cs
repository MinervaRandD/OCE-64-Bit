

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

    public partial class UCLineFinishEditFormElement : UserControl
    {
        public LineFinishBase LineFinishBase;

        public UCLineFinishEditFormElement(LineFinishBase lineFinishBase, int lineListIndex)
        {
            InitializeComponent();

            this.LineFinishBase = lineFinishBase;

            this.positionOnPalette = lineListIndex;

            this.txbLineName.Enabled = false;
            this.SizeChanged += UCLineListElement_SizeChanged;

            this.txbLineName.Click += LblLineName_Click;
            this.Click += UCLineFinishEditFormElement_Click;

            this.Paint += UCLineListElement_Paint;

            this.AutoSize = false;
        }

        public delegate void ControlClickedHandler(UCLineFinishEditFormElement sender);

        public event ControlClickedHandler ControlClicked;

        private void UCLineListElementClick(UCLineFinishEditFormElement sender)
        {
            if (ControlClicked != null)
            {
                ControlClicked.Invoke(sender);
            }

            
        }

        private void LblFinishName_Click(object sender, EventArgs e)
        {
            UCLineListElementClick(this);
        }

        private void UCLineFinishEditFormElement_Click(object sender, EventArgs e)
        {
            UCLineListElementClick(this);
        }

        public string LineName
        {
            get
            {
                if (LineFinishBase == null)
                {
                    return string.Empty;
                }

                return LineFinishBase.LineName;
            }

            set
            {
                if (LineFinishBase == null)
                {
                    return;
                }

                LineFinishBase.LineName = value;
                this.txbLineName.Text = value;
            }
        }

        public string Product
        {
            get
            {
                if (LineFinishBase == null)
                {
                    return string.Empty;
                }

                return LineFinishBase.Product;
            }

            set
            {
                if (LineFinishBase == null)
                {
                    return;
                }

                LineFinishBase.Product = value;
            }
        }

        public string Notes
        {
            get
            {
                if (LineFinishBase == null)
                {
                    return string.Empty;
                }

                return LineFinishBase.Notes;
            }

            set
            {
                if (LineFinishBase == null)
                {
                    return;
                }

                LineFinishBase.Notes = value;
            }
        }

        public int positionOnPalette = 0;

        private void UCLineListElement_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void LblLineName_Click(object sender, EventArgs e)
        {
            OnClick(null);
        }

        private void UCLineListElement_Paint(object sender, PaintEventArgs e)
        {
            if (LineFinishBase == null)
            {
                return;
            }

            this.txbLineName.Text = LineFinishBase.LineName;

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(LineFinishBase.LineColor);

            // Set the width
            pen.Width = (float) LineFinishBase.LineWidthInPts;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[LineFinishBase.VisioLineType];

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;
            
            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[LineFinishBase.VisioLineType];

            e.Graphics.DrawLine(pen, 12.0F, 26.0F, this.Width - 24, 26.0F);

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public new void Select()
        {
            this.txbLineName.Font = new Font(this.txbLineName.Font, FontStyle.Bold);
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        public void Deselect()
        {
            this.txbLineName.Font = new Font(this.txbLineName.Font, FontStyle.Regular);
            this.BorderStyle = BorderStyle.None;
        }
    }
}
