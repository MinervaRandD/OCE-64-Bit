using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Supporting_Forms
{
    public partial class ExamplePolygon : UserControl
    {
        private System.Drawing.Graphics graphics;

        private PointF[] baseShapeDefinition = new PointF[]
        {
            new PointF(0.000f, 0.000f),
            new PointF(0.000f, 0.625f),
            new PointF(0.375f, 1.000f),
            new PointF(1.000f, 1.000f),
            new PointF(1.000f, 0.875f),
            new PointF(0.750f, 0.625f),
            new PointF(0.750f, 0.375f),
            new PointF(1.000f, 0.125f),
            new PointF(1.000f, 0.000f),
            new PointF(0.000f, 0.000f)
        };

        private PointF[] shapeDefinition;

        public Color BaseColor;

        private Color DefaultColor = SystemColors.ControlLightLight;

            //Color.FromArgb(0, SystemColors.ControlLightLight.R, SystemColors.ControlLightLight.G, SystemColors.ControlLightLight.B);

        public ExamplePolygon(Color baseColor)
        {
            InitializeComponent();

            this.BaseColor = baseColor;

            graphics = this.CreateGraphics();

            float scaleY = (float)this.Height * 0.8f;
            float scaleX = (float)this.Width * 0.8f;

            shapeDefinition = new PointF[baseShapeDefinition.Length];

            for (int i = 0; i < shapeDefinition.Length; i++)
            {
                shapeDefinition[i] = new PointF(baseShapeDefinition[i].X * scaleX+10, (1.0f - baseShapeDefinition[i].Y) * scaleY+ 10);
            }

            
            this.Paint += ExamplePolygon_Paint;

            this.SizeChanged += ExamplePolygon_SizeChanged;

            graphics.Clear(DefaultColor);

            this.BackColor = Color.FromArgb(0, SystemColors.ControlLightLight.R, SystemColors.ControlLightLight.G, SystemColors.ControlLightLight.B);
            this.ForeColor = this.BackColor;
        }

        private void ExamplePolygon_SizeChanged(object sender, EventArgs e)
        {

            float scaleY = (float)this.Height * 0.8f;
            float scaleX = (float)this.Width * 0.8f;

            shapeDefinition = new PointF[baseShapeDefinition.Length];

            for (int i = 0; i < shapeDefinition.Length; i++)
            {
                shapeDefinition[i] = new PointF(baseShapeDefinition[i].X * scaleX+10, (1.0f - baseShapeDefinition[i].Y) * scaleY+ 10);
            }
        }

        private void ExamplePolygon_Paint(object sender, PaintEventArgs e)
        {

            graphics.Clear(DefaultColor);

            Pen pen = new Pen(BaseColor);

            e.Graphics.DrawPolygon(pen, shapeDefinition);

            Brush brush = new SolidBrush(BaseColor);

            e.Graphics.FillPolygon(brush, shapeDefinition);
        }

        internal void SetColor(Color selectedColor)
        {
            graphics.Clear(DefaultColor);

            this.BaseColor = selectedColor;

            Pen pen = new Pen(BaseColor);

            graphics.DrawPolygon(pen, shapeDefinition);

            Brush brush = new SolidBrush(BaseColor);

            graphics.FillPolygon(brush, shapeDefinition);
        }

        internal void SetColor(Color fillColor, Color borderColor)
        {
            graphics.Clear(DefaultColor);

            this.BaseColor = fillColor;

            Pen pen = new Pen(borderColor,8);

            graphics.DrawPolygon(pen, shapeDefinition);

            Brush brush = new SolidBrush(fillColor);

            graphics.FillPolygon(brush, shapeDefinition);
        }
    }
}
