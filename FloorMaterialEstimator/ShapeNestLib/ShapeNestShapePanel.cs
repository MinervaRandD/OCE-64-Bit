using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SettingsLib;
using Geometry;
using VoronoiLib;

namespace ShapeNestLib
{
    internal class ShapeNestShapePanel: Panel
    {
    
        public ShapeNestShape shapeNestShape;

        Color lineColor;

        Color fillColor;

        public bool Selected;

        public ShapeNestShapePanel(ShapeNestShape shapeNestShape, Color lineColor, Color fillColor)
        {
            this.shapeNestShape = shapeNestShape;

            this.lineColor = lineColor;
            this.fillColor = fillColor;

            this.Selected = shapeNestShape.selectionNmbr.HasValue;

            this.Paint += ShapeNestShapePanel_Paint;

            this.Click += ShapeNestShapePanel_Click;

            this.DoubleClick += ShapeNestShapePanel_DoubleClick;
        }

        private void ShapeNestShapePanel_DoubleClick(object sender, EventArgs e)
        {
            changeCheckStatus();
        }

        private void ShapeNestShapePanel_Click(object sender, EventArgs e)
        {
            changeCheckStatus();
        }

        private void changeCheckStatus()
        {
            this.Selected = !this.Selected;

            this.shapeNestShape.Selected = this.Selected;

            this.Invalidate();
        }

        private void ShapeNestShapePanel_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        public void Draw(PaintEventArgs e)
        {
            List<PointF> shapePoints = shapeNestShape.points;

            float sizeX = this.Width;
            float sizeY = this.Height;

            //float maxX = shapeNestShape.MaxX;
            //float maxY = shapeNestShape.MaxY;

            float minX = shapeNestShape.MinX;
            float minY = shapeNestShape.MinY;

            float lengthX = shapeNestShape.Width;
            float lengthY = shapeNestShape.Height;

            float scaleX = sizeX / lengthX;
            float scaleY = sizeY / lengthY;

            float scale = 0.8F * Math.Min(scaleX, scaleY);

            float offsetX = 0.5F * (sizeX - (lengthX * scale));
            float offsetY = 0.5F * (sizeY - (lengthY * scale));

            var transformedPoints = shapePoints.Select(p => new PointF(offsetX + ((p.X - minX) * scale), sizeY - (offsetY + (p.Y - minY) * scale))).ToArray();

            System.Drawing.Graphics g = e.Graphics;

            Pen pen = new Pen(lineColor);

            for (int i = 0; i < transformedPoints.Length; i++)
            {
                g.DrawLine(pen, transformedPoints[i], transformedPoints[(i + 1) % transformedPoints.Length]);
            }

            Brush fillBrush = new SolidBrush(fillColor);

            g.FillPolygon(fillBrush, transformedPoints);

            List<DirectedLine> directedLineList = new List<DirectedLine>();

            for (int i = 0; i < transformedPoints.Length - 1; i++)
            {
                PointF transformedPoint1 = transformedPoints[i];
                PointF transformedPoint2 = transformedPoints[(i + 1) % transformedPoints.Length];

                Coordinate coord1 = new Coordinate(transformedPoint1.X, transformedPoint1.Y);
                Coordinate coord2 = new Coordinate(transformedPoint2.X, transformedPoint2.Y);

                DirectedLine directedLine = new DirectedLine(coord1, coord2);

                directedLineList.Add(directedLine);
            }

            VoronoiRunner vonoiRunner = new VoronoiRunner(directedLineList, 20);

            Coordinate centroid = vonoiRunner.RunVoroniAlgo();

            drawShapeNmbr(g, shapeNestShape.indxNmbr, centroid);


            if (Selected)
            {

                drawCheckBox(g);
            }

            g.Dispose();
        }

        private void drawShapeNmbr(System.Drawing.Graphics g, int shapeNmbr, Coordinate centroid)
        {
            Brush textBrush = new SolidBrush(GlobalSettings.AreaIndexFontColor);

            string sn = shapeNestShape.indxNmbr.ToString();

            var fontFamily = new FontFamily("Times New Roman");
            var font = new Font(fontFamily, 12);

            g.DrawString(sn, font, textBrush, (float)centroid.X - 7f, (float)centroid.Y - 8f);

        }

        private PointF[] checkPoints = new PointF[]
        {
            new PointF(6,7)
            , new PointF(8,13)
            , new PointF(13, 4)
        };

        private void drawCheckBox(System.Drawing.Graphics g)
        {
            Pen checkBoxRecPen = new Pen(Color.Black, 1f);

            g.DrawRectangle(checkBoxRecPen, 3.0f, 2.0f, 12f, 13f);

            Pen checkPen = new Pen(Color.Red, 1f);

            g.DrawLines(checkPen, checkPoints);
        }

        internal void SelectShape()
        {
            this.Selected = true;

            this.shapeNestShape.Selected = true;

            this.Invalidate();
        }

        internal void DeselectShape()
        {
            this.Selected = false;

            this.shapeNestShape.Selected = false;

            this.Invalidate();
        }

        internal void SelectIfNumbered()
        {
            if (this.Selected)
            {
                return;
            }

            if (!this.shapeNestShape.selectionNmbr.HasValue)
            {
                return;
            }

            SelectShape();
        }
    }
}
