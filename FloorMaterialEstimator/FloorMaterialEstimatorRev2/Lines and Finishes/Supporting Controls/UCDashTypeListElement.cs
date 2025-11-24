using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Finish_Controls
{
    public partial class UCDashTypeListElement : UserControl
    {
        public Color PenColor;
        public int PenWidth;
        public float[] PenDashType;
        public int VisioDashTypeIndex;

        public int ListElementIndex = 0;

        public UCDashTypeListElement(Color penColor, int penWidth, float[] penDashType, int visioDashTypeIndex, int listElementIndex)
        {
            InitializeComponent();

            ListElementIndex = listElementIndex;

            PenColor = penColor;
            PenWidth = penWidth;
            PenDashType = penDashType;
            VisioDashTypeIndex = visioDashTypeIndex;

            this.Paint += UCDashTypeListElement_Paint;

            this.BackColor = SystemColors.ControlLight;

            this.MouseDown += UCDashTypeListElement_MouseDown;
            this.MouseUp += UCDashTypeListElement_MouseUp;
        }


        private void UCDashTypeListElement_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackColor = SystemColors.ControlLight;
        }

        private void UCDashTypeListElement_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackColor = SystemColors.ControlDark;
        }

        private void UCDashTypeListElement_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;

            Pen pen = new Pen(PenColor, PenWidth);

            pen.DashPattern = PenDashType;

            int x1 = 2;
            int y1 = (this.Height - PenWidth) / 2;
            int x2 = this.Width - 2;
            int y2 = y1;

            g.DrawLine(pen, x1, y1, x2, y2);
        }
    }
}
