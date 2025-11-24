using FinishesLib;
using FloorMaterialEstimator.Finish_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Lines_and_Finishes.Supporting_Controls
{
    public partial class UCCustomPatternPallet : UserControl
    {
        const double nLinesPerSquare = 12.0F;

        AreaFinishBaseList areaFinishBaseList;

        public UCCustomPatternPallet()
        {
            InitializeComponent();
        }

        public void Init(AreaFinishBaseList areaFinishBaseList, UCCustomColorPallet uCCustomColorPallet)
        {
            this.areaFinishBaseList = areaFinishBaseList;

            this.pnlNoPattern.Paint += PnlNoPattern_Paint;
            this.pnlHorizontalHashPattern.Paint += PnlHorizontalHashPattern_Paint;
            this.pnlVerticalHashPattern.Paint += PnlVerticalHashPattern_Paint;
            this.pnlCrossHashPattern.Paint += PnlCrossHashPattern_Paint;
            
           

            this.pnlNoPattern.Click += PnlNoPattern_Click;
            this.pnlNoPattern.MouseDown += PnlNoPattern_MouseDown;
            this.pnlNoPattern.MouseUp += PnlNoPattern_MouseUp;

            this.pnlHorizontalHashPattern.Click += PnlHorizontalHashPattern_Click;
            this.pnlHorizontalHashPattern.MouseDown += PnlHorizontalHashPattern_MouseDown;
            this.pnlHorizontalHashPattern.MouseUp += PnlHorizontalHashPattern_MouseUp;

            this.pnlVerticalHashPattern.Click += PnlVerticalHashPattern_Click;
            this.pnlVerticalHashPattern.MouseDown += PnlVerticalHashPattern_MouseDown;
            this.pnlVerticalHashPattern.MouseUp += PnlVerticalHashPattern_MouseUp;


            this.pnlCrossHashPattern.Click += PnlCrossHashPattern_Click;
            this.pnlCrossHashPattern.MouseDown += PnlCrossHashPattern_MouseDown;
            this.pnlCrossHashPattern.MouseUp += PnlCrossHashPattern_MouseUp;


            uCCustomColorPallet.ColorSelected += UCCustomColorPallet_ColorSelected;
            this.areaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
        }

        public void UCCustomColorPallet_ColorSelected(ColorSelectedEventArgs args)
        {
            this.pnlNoPattern.Invalidate();
            this.pnlHorizontalHashPattern.Invalidate();
            this.pnlVerticalHashPattern.Invalidate();
            this.pnlCrossHashPattern.Invalidate();
        }

        private void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            this.pnlNoPattern.Invalidate();
            this.pnlHorizontalHashPattern.Invalidate();
            this.pnlVerticalHashPattern.Invalidate();
            this.pnlCrossHashPattern.Invalidate();
        }

        private void PnlNoPattern_MouseDown(object sender, MouseEventArgs e)
        {
            pnlNoPattern.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PnlNoPattern_Click(object sender, EventArgs e)
        {
            areaFinishBaseList.SelectedItem.Pattern = 0;
        }


        private void PnlHorizontalHashPattern_MouseUp(object sender, MouseEventArgs e)
        {
            pnlHorizontalHashPattern.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PnlHorizontalHashPattern_MouseDown(object sender, MouseEventArgs e)
        {
            pnlHorizontalHashPattern.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PnlNoPattern_MouseUp(object sender, MouseEventArgs e)
        {
            pnlNoPattern.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PnlHorizontalHashPattern_Click(object sender, EventArgs e)
        {
            areaFinishBaseList.SelectedItem.Pattern = 1;
        }

        private void PnlVerticalHashPattern_MouseDown(object sender, MouseEventArgs e)
        {
            pnlVerticalHashPattern.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PnlVerticalHashPattern_MouseUp(object sender, MouseEventArgs e)
        {
            pnlVerticalHashPattern.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PnlVerticalHashPattern_Click(object sender, EventArgs e)
        {
            areaFinishBaseList.SelectedItem.Pattern = 2;
        }

        private void PnlCrossHashPattern_MouseDown(object sender, MouseEventArgs e)
        {
            pnlCrossHashPattern.BorderStyle = BorderStyle.Fixed3D;
        }

        private void PnlCrossHashPattern_MouseUp(object sender, MouseEventArgs e)
        {
            pnlCrossHashPattern.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PnlCrossHashPattern_Click(object sender, EventArgs e)
        {
            areaFinishBaseList.SelectedItem.Pattern = 3;
        }

        private void PnlNoPattern_Paint(object sender, PaintEventArgs e)
        {
            pnlNoPattern.BackColor = areaFinishBaseList.SelectedItem.Color;
        }

        private void PnlHorizontalHashPattern_Paint(object sender, PaintEventArgs e)
        {
            Color selectedColor = areaFinishBaseList.SelectedItem.Color;

            selectedColor = Color.FromArgb(255, selectedColor.R, selectedColor.G, selectedColor.B);

            Utilities.HashedPanelPainter.PaintHorizontalHashedPanel(this.pnlHorizontalHashPattern, (int)nLinesPerSquare, selectedColor, e);
        }

        private void PnlVerticalHashPattern_Paint(object sender, PaintEventArgs e)
        {
            Color selectedColor = areaFinishBaseList.SelectedItem.Color;

            selectedColor = Color.FromArgb(255, selectedColor.R, selectedColor.G, selectedColor.B);

            Utilities.HashedPanelPainter.PaintVerticalHashedPanel(this.pnlHorizontalHashPattern, (int)nLinesPerSquare, selectedColor, e);

        }

        private void PnlCrossHashPattern_Paint(object sender, PaintEventArgs e)
        {
            Color selectedColor = areaFinishBaseList.SelectedItem.Color;

            selectedColor = Color.FromArgb(255, selectedColor.R, selectedColor.G, selectedColor.B);

            Utilities.HashedPanelPainter.PaintCrossHashedPanel(this.pnlHorizontalHashPattern, (int)nLinesPerSquare, (int)nLinesPerSquare, selectedColor, e);
        }

    }
}
