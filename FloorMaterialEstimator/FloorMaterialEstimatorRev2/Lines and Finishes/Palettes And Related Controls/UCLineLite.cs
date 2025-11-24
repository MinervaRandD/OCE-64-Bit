

namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using PaletteLib;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using FinishesLib;

    public partial class UCLineLite : UserControl
    {
        
        private UCLineFinishPalette ucLinePallet;

        public UCLineLite()
        {
            InitializeComponent();

            this.Paint += UCLineLite_Paint;
        }

        private void UCLineLite_Paint(object sender, PaintEventArgs e)
        {
            if (ucLinePallet == null)
            {
                return;
            }

            UCLineFinishPaletteElement selectedLine = ucLinePallet.SelectedLine;

            if (selectedLine == null)
            {
                return;
            }

            this.lblLineName.Text = selectedLine.LineName;

            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(selectedLine.LineColor);

            // Set the width
            pen.Width = (float) selectedLine.LineWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            int visioDashType = selectedLine.VisioDashType;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[visioDashType];

            e.Graphics.DrawLine(pen, 12.0F, 40.0F, 182.0F, 40.0F);
            

            // Dispose of the custom pen.
            pen.Dispose();
        }

        public void Init(UCLineFinishPalette ucLinePallet)
        {
            this.ucLinePallet = ucLinePallet;
        }
    }
}
