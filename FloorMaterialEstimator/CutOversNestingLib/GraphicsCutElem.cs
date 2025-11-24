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
    public class GraphicsCutElem
    {
        public string Guid { get; set; }

        public uint Index { get; set; }

        public GraphicShape Shape => GraphicsDirectedPolygon.Shape;

        public GraphicsDirectedPolygon GraphicsDirectedPolygon { get; set; }

        public GraphicsCut GraphicsCut { get; set; }

        public Color FinishColor { get; set; }

        public Color LineColor { get; set; }

        public double MaterialWidthInInches { get; set; }

        private GraphicsWindow window;

        private GraphicsPage page;

        public double CutAngle
        {
            get
            {
                return GraphicsCut.CutAngle;
            }

            set
            {
                GraphicsCut.CutAngle = value;
            }
        }

        public GraphicsCutElem(
            GraphicsWindow window
            , GraphicsPage page
            , uint index
            , GraphicsCut graphicsCut
            , GraphicsDirectedPolygon graphicsDirectedPolygon
            , double materialWidth
            , Color finishColor
            , Color lineColor)
        {
            this.window = window;

            this.page = page;

            Index = index;

            GraphicsCut = graphicsCut;

            double theta = graphicsCut.CutAngle;

            Coordinate translateCoord = graphicsCut.CutOffset;

            GraphicsDirectedPolygon = graphicsDirectedPolygon;

            GraphicsDirectedPolygon.Rotate(theta);

            FinishColor = finishColor;

            MaterialWidthInInches = materialWidth;
        }

        public GraphicShape Draw(GraphicsWindow window, GraphicsPage page, Color perimeterColor, Color fillColor, double lineWidthInPts)
        {
            GraphicsDirectedPolygon.Rotate(-CutAngle);

            GraphicsDirectedPolygon.Draw(window, page, perimeterColor, fillColor);

            VisioInterop.SetShapeText(Shape, this.Index.ToString(), Color.Black, 24);

            return Shape;
        }

        public GraphicsCutElem Clone()
        {
            GraphicsDirectedPolygon clonedPolygon = this.GraphicsDirectedPolygon.Clone();

            GraphicsCutElem clonedGraphicsComboElem 
                = new GraphicsCutElem(
                    window
                    , page
                    , this.Index
                    , this.GraphicsCut
                    ,  clonedPolygon
                    , this.MaterialWidthInInches
                    , this.FinishColor
                    , this.LineColor
                    );

            return clonedGraphicsComboElem;
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
