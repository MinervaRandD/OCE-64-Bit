using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialsLayout;
using Geometry;
using ComboLib;
using System.Diagnostics;

namespace FloorMaterialEstimator
{
    public partial class UCUnitPath : UserControl
    {
        private ComboPath path;

        public float scale = 64;

        public double PathWdth = 0;

        private double drawingScaleInFeet;

        private double[] offsetArray = null;

        private double[] comboElemLgthArray = null;

        public UCUnitPath(ComboPath path, double maxY, double drawingScaleInFeet)
        {
            InitializeComponent();

            this.path = path;

            offsetArray = new double[path.Length];

            comboElemLgthArray = new double[path.Length];

            scale = 64.0f / (float)maxY;

            this.drawingScaleInFeet = drawingScaleInFeet;

            setCntlWdth();

            this.SizeChanged += UCUnitPath_SizeChanged;

            this.pnlPath.Paint += PnlPath_Paint;
        }


        private void setCntlWdth()
        {
            if (path.Length <= 0)
            {
                return;
            }

            this.PathWdth = path.Width();

            this.lblPathLength.Text = (this.PathWdth * drawingScaleInFeet).ToString("#,##0.00");

            setSizeByWidth(this.PathWdth);
        }
    

        private void UCUnitPath_SizeChanged(object sender, EventArgs e)
        {
            int cntlSizeX = this.Width;
            int cntlSizeY = this.Height;

            this.pnlPath.Size = new Size(cntlSizeX, this.pnlPath.Height);
            this.lblPathLength.Size = new Size(cntlSizeX, this.lblPathLength.Height);
        }

        private void PnlPath_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics graphics = e.Graphics;

            double offset = 0;

            offsetArray[0] = 0;

            if (path.Length <= 0)
            {
                return;
            }

            ComboPathElement pathElement = path[0];

            drawCut(graphics, pathElement.GraphicsComboElem, 0.0);

            for (int i = 1; i < path.Length; i++)
            {
                //double comboElemLength = pathElement.Length;

                //comboElemLgthArray[i - 1] = comboElemLength;

                //pathElement = path[i];

                //double offsetIncrement = comboElemLength - pathElement.Offset;

                //offset += offsetIncrement;

                //// MDD Remove

                ////if (i == 2)
                ////{
                ////    offset = comboElemLgthArray[0];
                ////}

                //drawCut(graphics, pathElement.GraphicsComboElem, offset);

                pathElement = path[i];

                offset = pathElement.EndpointOffset - pathElement.Length;

                drawCut(graphics, pathElement.GraphicsComboElem, pathElement.EndpointOffset - pathElement.Length);
            }

        }

        private void setSizeByWidth(double width)
        {
            this.Width = (int) (Math.Ceiling(width * scale) + 4);
            this.pnlPath.Width = this.Width;
            this.lblPathLength.Width = this.Width;
        }

        private void drawCut(System.Drawing.Graphics graphics, GraphicsComboElem graphicsComboElem, double offset)
        {
            DirectedPolygon directedPolygon = graphicsComboElem.GraphicsDirectedPolygon;

            {
                PointF[] coordsF = directedPolygon.GeneratePointFCoordinates();

                int count = coordsF.Length;

                for (int i = 0; i < count; i++)
                {
                    coordsF[i] = new PointF((coordsF[i].X + (float)offset) * scale, 64.0f - coordsF[i].Y * scale);
                }

                Pen pen = new Pen(Color.Black, (float)3.0);

                graphics.DrawPolygon(pen, coordsF);

                Brush brush = new SolidBrush(Color.PaleGreen);

                graphics.FillPolygon(brush, coordsF);

                float minX = coordsF.Min(c => c.X);
                float maxX = coordsF.Max(c => c.X);
                float minY = coordsF.Min(c => c.Y);
                float maxY = coordsF.Max(c => c.Y);

                float x = 0.5f * (maxX + minX);
                float y = 56f - 0.5f * (maxY - minY);

                string label = graphicsComboElem.Index.ToString();

                Font textFont = new Font("Arial", 12, FontStyle.Bold);

                StringFormat textFormat = new StringFormat();

                SolidBrush textBrush = new SolidBrush(Color.Red);

                if (graphicsComboElem.IsRotated)
                {
                    Size textSize = TextRenderer.MeasureText(label, textFont);

                    float xlateX = textSize.Width + x;
                    float xlateY = textSize.Height + y;

                    graphics.TranslateTransform(xlateX, xlateY);

                    graphics.RotateTransform(180);


                    graphics.DrawString(label, textFont, textBrush, 0, 0, textFormat);

                    graphics.RotateTransform(-180);

                    graphics.TranslateTransform(-xlateX, -xlateY);
                }

                else
                {
                    graphics.DrawString(label, textFont, textBrush, x, y, textFormat);
                }
            }

        }

        //private double drawCut(PaintEventArgs e, ParentGraphicsCut cut, double offset)
        //{
        //    DirectedPolygon polygon = (DirectedPolygon)cut.CutPolygon;

        //    PointF[] coordsF = cut.CutPolygon.GeneratePointFCoordinates();

        //    int count = coordsF.Length;

        //    for (int i = 0; i < 5; i++)
        //    {
        //        coordsF[i] = new PointF((coordsF[i].X + (float) offset) * scale, 64.0f - coordsF[i].Y * scale);
        //    }

        //    Pen pen = new Pen(Color.Black, (float) 3.0);

        //    e.Graphics.DrawPolygon(pen, coordsF);

        //    Brush brush = new SolidBrush(Color.PaleGreen);

        //    e.Graphics.FillPolygon(brush, coordsF);

        //    Font textFont = new Font("Arial", 12, FontStyle.Bold);

        //    StringFormat textFormat = new StringFormat();

        //    SolidBrush textBrush = new SolidBrush(Color.Red);

        //    float minX = coordsF.Min(c => c.X);
        //    float maxX = coordsF.Max(c => c.X);
        //    float minY = coordsF.Min(c => c.Y);
        //    float maxY = coordsF.Max(c => c.Y);

        //    float x = 0.5f * (maxX - minX);
        //    float y = 56f - 0.5f * (maxY - minY);

        //    e.Graphics.DrawString(cut.Tag.ToString(), textFont, textBrush, x, y, textFormat);

        //    return cut.CutPolygon.MaxX - cut.CutPolygon.MinX;
        //}
    }
}
