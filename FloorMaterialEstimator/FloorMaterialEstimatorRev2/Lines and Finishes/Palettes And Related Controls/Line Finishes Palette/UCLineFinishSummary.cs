//-------------------------------------------------------------------------------//
// <copyright file="UCLine.cs" company="Bruun Estimating, LLC">                  // 
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
    using System.Drawing;
    
    using System.Windows.Forms;

    using FinishesLib;

    using Graphics;

    using Globals;

    public partial class UCLineFinishSummary : UserControl
    {
        //public UCLineFinishPaletteElement lineFinishPalletElement;

        public LineFinishBase LineFinishBase = null; //=> (lineFinishPalletElement != null) ? lineFinishPalletElement.LineFinishBase : null;

        public string Guid => LineFinishBase.Guid;

        public string LineName => LineFinishBase.LineName;

        public short VisioDashType => (short)LineFinishBase.VisioLineType;
       
        public double LineWidthInPts => LineFinishBase.LineWidthInPts;
        
        public Color LineColor => LineFinishBase.LineColor;

        //public CanvasManager canvasManager { get; set; }

        private GraphicsWindow window;

        private GraphicsPage page;

        public UCLineFinishSummary(GraphicsWindow window, GraphicsPage page)
        {
            InitializeComponent();

            this.window = window;

            this.page = page;

            this.Paint += UCLine_Paint;
        }

        public void SetLineFinish(int selectedIndex)
        {
            if (LineFinishBase != null)
            {
                LineFinishBase.LengthChanged -= LineFinishBase_LengthChanged;
                LineFinishBase.DefinitionChanged -= LineFinishBase_DefinitionChanged;
            }

            LineFinishBase = StaticGlobals.LineFinishBaseList[selectedIndex];

            this.lblLineName.Text = this.LineFinishBase.LineName;

            double lengthInInches = StaticGlobals.LineFinishBaseList.SelectedItem.LengthInInches;

            setupTallyDisplay(SystemState.ScaleHasBeenSet, lengthInInches);

            LineFinishBase.DefinitionChanged += LineFinishBase_DefinitionChanged;
            LineFinishBase.LengthChanged += LineFinishBase_LengthChanged;

            this.Invalidate();
        }

        private void LineFinishBase_DefinitionChanged(LineFinishBase LineFinishBase)
        {
            this.lblLineName.Text = this.LineFinishBase.LineName;

            this.Invalidate();
        }

        private void LineFinishBase_LengthChanged(LineFinishBase LineFinishBase, double lengthInInches)
        {
            setupTallyDisplay(SystemState.ScaleHasBeenSet, lengthInInches);

            this.Invalidate();
        }

        private void setupTallyDisplay(bool scaleHasBeenSet, double lengthInInches)
        {
            if (scaleHasBeenSet)
            {
                this.lblPerimDecimal.Text = (lengthInInches / 12.0).ToString("#,##0.0") + " l.f.";
                this.lblLength.Visible = true;
            }

            else
            {
                this.lblPerimDecimal.Text = string.Empty;
                this.lblLength.Visible = false;
            }

            this.lblLength.Invalidate();
            this.lblPerimDecimal.Invalidate();
        }

        private void UCLine_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void Draw(PaintEventArgs e)
        {
            if (LineFinishBase == null)
            {
                return;
            }

            // Set the SmoothingMode property to smooth the line.
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Create a new Pen object.
            Pen pen = new Pen(this.LineFinishBase.LineColor);

            // Set the width
            pen.Width = (float)LineWidthInPts;

            pen.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

            pen.DashPattern = LineFinishBase.VisioToDrawingPatternDict[this.VisioDashType];

            e.Graphics.DrawLine(pen, 12.0F, 54.0F, 182.0F, 54.0F);

            // Dispose of the custom pen.
            pen.Dispose();
        }
    }
}
