using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities.User_Controls
{
    public partial class SelectionHighlight : UserControl
    {
        const int penWidth = 5;

        public SelectionHighlight()
        {
            InitializeComponent();

            this.pnlHighlightBorder.BackColor = SystemColors.ControlLightLight;
        }


        bool initialized = false;

        public void Initialize()
        {

            this.pnlHighlightBorder.Size = new Size(this.Width - 2, this.Height - 2);
            this.pnlHighlightBorder.Location = new Point(1, 1);

            this.BorderStyle = BorderStyle.None;

            this.pnlHighlightBorder.Paint += pnlHighlightBorder_Paint;

            initialized = true;
        }

        bool highlighted = false;

        public void HighLight(bool highlighted)
        {
            if (!initialized)
            {
                Initialize();
            }

            if (highlighted == this.highlighted)
            {
                return;
            }

            this.highlighted = highlighted;

            this.pnlHighlightBorder.Invalidate();
            this.pnlHighlightBorder.Update();
        }

        private void pnlHighlightBorder_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (!highlighted)
            {
                g.Clear(SystemColors.ControlLightLight);
            }

            else
            {
                Pen p = new Pen(Color.Orange, penWidth);

                g.DrawRectangle(p, 2F, 2F, this.pnlHighlightBorder.Width - penWidth, this.pnlHighlightBorder.Height - penWidth);
            }
        }
    }
}
