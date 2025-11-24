using Geometry;
using Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.CanvasManager
{
    public class CanvasBorderDirectionSelector
    {
        GraphicsWindow window;

        GraphicsPage page;

        GraphicsDirectedLine arrow1;
        GraphicsDirectedLine arrow2;

        public CanvasBorderDirectionHandle Handle1;
        public CanvasBorderDirectionHandle Handle2;

        Coordinate center;
        double slope;

        const double arrowLength = 0.1;
        const double circleRadius = 0.1;

        public CanvasBorderDirectionSelector(GraphicsWindow window, GraphicsPage page, Coordinate center, double slope)
        {
            this.page = page;

            this.center = center;
            this.slope = slope;

            double deltaX = 0;
            double deltaY = 0;

            if (double.IsInfinity(slope))
            {
                deltaX = 0;
                deltaY = arrowLength;
            }

            else
            {
                deltaX = Math.Sqrt(arrowLength / Math.Pow(slope, 2.0));
                deltaY = slope * deltaX;
            }

            Coordinate arrow1Coord2 = center + new Coordinate(deltaX, deltaY);
            Coordinate arrow2Coord2 = center - new Coordinate(deltaX, deltaY);

            if (double.IsInfinity(slope))
            {
                deltaX = 0;
                deltaY = arrowLength + circleRadius;
            }

            else
            {
                deltaX = Math.Sqrt((arrowLength + circleRadius) / Math.Pow(slope, 2.0));
                deltaY = slope * deltaX;
            }

            Coordinate circle1Center = center + new Coordinate(deltaX, deltaY);
            Coordinate circle2Center = center - new Coordinate(deltaX, deltaY);

            arrow1 = new GraphicsDirectedLine(window, page, new DirectedLine(center, arrow1Coord2), LineRole.SingleLine);
            arrow2 = new GraphicsDirectedLine(window, page, new DirectedLine(center, arrow2Coord2), LineRole.SingleLine);

            Handle1 = new CanvasBorderDirectionHandle(window, page, this, 'l', circle1Center, circleRadius);
            Handle2 = new CanvasBorderDirectionHandle(window, page, this, 'r', circle2Center, circleRadius);
        }

        public void Draw()
        {
            arrow1.Shape = page.DrawLine(arrow1.Coord1.X, arrow1.Coord1.Y, arrow1.Coord2.X, arrow1.Coord2.Y, string.Empty);
            arrow2.Shape = page.DrawLine(arrow2.Coord1.X, arrow2.Coord1.Y, arrow2.Coord2.X, arrow2.Coord2.Y, string.Empty);

            arrow1.SetBaseLineColor(Color.Green);
            arrow2.SetBaseLineColor(Color.Green);

            VisioInterop.SetLineEndShape(arrow1.Shape, 5, 3);
            VisioInterop.SetLineEndShape(arrow2.Shape, 5, 3);

            Handle1.Draw();
            Handle2.Draw();

            //page.BorderDirectionHandleDict.Add(Handle1.Guid, Handle1);
            //page.BorderDirectionHandleDict.Add(Handle2.Guid, Handle2);
        }

        public void Remove()
        {
            VisioInterop.DeleteShape(arrow1.Shape);
            VisioInterop.DeleteShape(arrow2.Shape);

            Handle1.Remove();
            Handle2.Remove();
        }
    }
}
