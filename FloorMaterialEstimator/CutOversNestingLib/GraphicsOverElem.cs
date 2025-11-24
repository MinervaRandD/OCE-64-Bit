using Geometry;
using Graphics;
using MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutOversNestingLib
{
    public class GraphicsOverElem
    {
        public string Guid { get; set; }

        public int Index { get; set; }

        public Shape Shape => GraphicsDirectedPolygon.Shape;

        public GraphicsDirectedPolygon GraphicsDirectedPolygon { get; set; }

        public GraphicsOverage GraphicsOver { get; set; }

        public Color FinishColor { get; set; }

        private GraphicsWindow window;

        private GraphicsPage page;

        public double CutAngle
        {
            get
            {
                return GraphicsOver.BoundingRectangle.Angle;
            }

            set
            {
                GraphicsOver.BoundingRectangle.Angle = value;
            }
        }

        public GraphicsOverElem(
            GraphicsWindow window
            , GraphicsPage page
            , int index
            , GraphicsOverage graphicsOver
            , Color finishColor)
        {
            this.window = window;

            this.page = page;

            Index = index;

            GraphicsOver = graphicsOver;

            GraphicsDirectedPolygon = new GraphicsDirectedPolygon(window, page, (DirectedPolygon) graphicsOver.BoundingRectangle);

            double theta = graphicsOver.BoundingRectangle.Angle;

            Coordinate translateCoord = graphicsOver.BoundingRectangle.Offset;

            GraphicsOver.BoundingRectangle.Rotate(theta);

            FinishColor = finishColor;
        }

        public Shape Draw(Color perimeterColor, Color fillColor, double lineWidthInPts)
        {
            GraphicsDirectedPolygon.Draw(window, page, perimeterColor, fillColor);

            return Shape;
        }

        public void Rotate(double theta)
        {
            this.GraphicsDirectedPolygon.Rotate(theta);
        }

        public void Translate(Coordinate coordinate)
        {
            this.GraphicsDirectedPolygon.Translate(coordinate);
        }

        public double Length => this.GraphicsDirectedPolygon.Length;
    }
}
