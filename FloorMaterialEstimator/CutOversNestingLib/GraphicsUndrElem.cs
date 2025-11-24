using Geometry;
using Graphics;
using MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CutOversNestingLib
{
    public class GraphicsUndrElem
    {
        public string Guid { get; set; }

        public uint Index { get; set; }

        public GraphicsLayoutArea GraphicsLayoutArea { get; set; }

        public GraphicShape Shape => GraphicsDirectedPolygon.Shape;

        public GraphicsDirectedPolygon GraphicsDirectedPolygon { get; set; }

        public GraphicsUndrage GraphicsUndr { get; set; }

        public Color FinishColor { get; set; }

        private GraphicsWindow window;

        private GraphicsPage page;

        public double CutAngle
        {
            get
            {
                return GraphicsUndr.BoundingRectangle.Angle;
            }

            set
            {
                GraphicsUndr.BoundingRectangle.Angle = value;
            }
        }

        public GraphicsUndrElem(
            GraphicsWindow window
            , GraphicsPage page
           // , ParentGraphicsLayoutArea graphicsLayoutArea
            , uint index
            , GraphicsUndrage graphicsUndr
            , Color finishColor)
        {
            this.window = window;

            this.page = page;

            Index = index;

            GraphicsUndr = graphicsUndr;

            DirectedPolygon directedPolygon = (DirectedPolygon)graphicsUndr.BoundingRectangle;

            GraphicsLayoutArea = GraphicsUndr.ParentGraphicsRollout.ParentGraphicsLayoutArea;

            GraphicsDirectedPolygon externalArea = GraphicsLayoutArea.ExternalArea;

            List<DirectedPolygon> directedPolygonList = directedPolygon.Intersect(externalArea);

            if (directedPolygonList.Count != 1)
            {
                throw new Exception("Unexpected list count.");
            }

            GraphicsDirectedPolygon = new GraphicsDirectedPolygon(window, page, directedPolygonList[0]);

            //GraphicsDirectedPolygon = new GraphicsDirectedPolygon(Window, Page, (DirectedPolygon) graphicsUndr.BoundingRectangle);

            double theta = graphicsUndr.BoundingRectangle.Angle;

            Coordinate translateCoord = graphicsUndr.BoundingRectangle.Offset;

            GraphicsUndr.BoundingRectangle.Rotate(theta);

            FinishColor = finishColor;
        }

        public GraphicShape Draw(
            GraphicsWindow window
            , GraphicsPage page
            , Color perimeterColor
            , Color fillColor
            , double lineWidthInPts)
        {
            GraphicShape shape = GraphicsDirectedPolygon.Draw(window, page, perimeterColor, fillColor);

            string indexText = Utilities.Utilities.IndexToLowerCaseString(Index);

            VisioInterop.SetShapeText(shape, indexText, Color.Black, 18);

            VisioInterop.SetLineWidth(shape, lineWidthInPts);

            return shape;
        }

        public GraphicShape Draw(
            GraphicsWindow window
            , GraphicsPage page
            , double x
            , double y
            , Color perimeterColor
            , Color fillColor
            , double lineWidthInPts)
        {
            GraphicShape shape = GraphicsDirectedPolygon.Draw(window, page, perimeterColor, fillColor);
            //Shape shape = this.GraphicsUndr.Draw(perimeterColor, fillColor);

            VisioInterop.SetLineWidth(shape, lineWidthInPts);

            string indexText = Utilities.Utilities.IndexToLowerCaseString(Index);

            VisioInterop.SetShapeText(shape, indexText, Color.Black, 18);

            VisioInterop.SetShapePinLocation(shape, x, y);

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

        public double Height => this.GraphicsDirectedPolygon.Height;

        public void Undraw()
        {
            if (Utilities.Utilities.IsNotNull(Shape))
            {
                Shape.Delete();
            }
        }

        public GraphicsUndrElem Clone(GraphicsWindow cloneWindow, GraphicsPage clonePage)
        {
            GraphicsUndrage clonedGraphicsUndrage = this.GraphicsUndr.Clone(null, cloneWindow, clonePage);

            GraphicsUndrElem clonedGraphicsUndrElem = new GraphicsUndrElem(cloneWindow, clonePage, this.Index, clonedGraphicsUndrage, Color.Green);

            return clonedGraphicsUndrElem;
        }
    }
}
