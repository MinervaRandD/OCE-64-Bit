using Geometry;
using Graphics;
using MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboLib
{
    public class GraphicsComboElem
    {
        public string Guid { get; set; }

        public uint Index { get; set; }

        public GraphicShape Shape => GraphicsDirectedPolygon.Shape;

        public GraphicsDirectedPolygon GraphicsDirectedPolygon { get; set; }

        public GraphicsCut GraphicsCut { get; set; }

        public Color FinishColor { get; set; }
        
        public bool IsRotated { get; set; }

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

        public GraphicsComboElem(uint index, GraphicsCut graphicsCut, GraphicsDirectedPolygon graphicsDirectedPolygon, Color finishColor)
        {
            Index = index;

            GraphicsCut = graphicsCut;

            double theta = graphicsCut.CutAngle;

            Coordinate translateCoord = graphicsCut.CutOffset;

            GraphicsDirectedPolygon = graphicsDirectedPolygon;

            GraphicsDirectedPolygon.Rotate(theta);

            FinishColor = finishColor;
        }

        public GraphicShape Draw(GraphicsWindow window, GraphicsPage page, Color perimeterColor, Color fillColor, double lineWidthInPts)
        {
            GraphicsDirectedPolygon.Rotate(-CutAngle);

            GraphicsDirectedPolygon.Draw(window, page, perimeterColor, fillColor);

            return Shape;
        }

        public GraphicsComboElem Clone()
        {
            GraphicsDirectedPolygon clonedPolygon = this.GraphicsDirectedPolygon.Clone();

            GraphicsComboElem clonedGraphicsComboElem = new GraphicsComboElem(this.Index, this.GraphicsCut,  clonedPolygon, this.FinishColor);

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
