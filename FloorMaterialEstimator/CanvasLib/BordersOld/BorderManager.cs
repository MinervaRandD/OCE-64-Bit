

namespace CanvasLib.Borders
{
    using Geometry;
    using Graphics;
    using Utilities;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Rectangle = Geometry.Rectangle;

    public class BorderManager
    {
        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public Coordinate BorderFrstCoord { get; set; } = Coordinate.NullCoordinate;

        public Coordinate BorderScndCoord { get; set; } = Coordinate.NullCoordinate;

        public BorderGenerationState BorderGenerationState { get; set; } = BorderGenerationState.Unknown;

        public BorderGenerationMarker BorderFrstMarker { get; set; }  = null;
        
        public BorderGenerationMarker BorderScndMarker { get; set; }  = null;

        private Shape borderGuideLine = null;

        private double widthInLocalInches = 0.0;

        public BorderManager(GraphicsWindow window, GraphicsPage page)
        {
            Window = window;

            Page = page;

        }

        public void Init(double widthInLocalInches)
        {
            BorderGenerationState = BorderGenerationState.Initial;

            this.widthInLocalInches = widthInLocalInches;
        }

        public void Exit()
        {
            BorderGenerationState = BorderGenerationState.None;

            BorderFrstCoord = Coordinate.NullCoordinate;

            BorderScndCoord = Coordinate.NullCoordinate;

            if (Utilities.IsNotNull(BorderFrstMarker))
            {
                BorderFrstMarker.Delete();

                BorderFrstMarker = null;
            }

            if (Utilities.IsNotNull(BorderScndMarker))
            {
                BorderScndMarker.Delete();

                BorderScndMarker = null;
            }

            if (Utilities.IsNotNull(borderGuideLine))
            {
                VisioInterop.DeleteShape(borderGuideLine);

                borderGuideLine = null;
            }
        }

        public void BorderDrawingModeClick(int button, double x, double y, ref bool drawingShape)
        {
            if (button != 2)
            {
                return;
            }

            switch (BorderGenerationState)
            {
                case BorderGenerationState.Initial:
                    BorderFrstPointClicked(x, y, ref drawingShape);
                    return;

                case BorderGenerationState.FrstPointSelected:
                    BorderScndPointClicked(x, y, ref drawingShape);
                    return;

                case BorderGenerationState.ScndPointSelected:
                    BorderDrctPointClicked(x, y, ref drawingShape);
                    return;
            }
        }

        private void BorderFrstPointClicked(double x, double y, ref bool drawingShape)
        {
            this.BorderGenerationState = BorderGenerationState.FrstPointSelected;

            this.BorderFrstMarker = new BorderGenerationMarker(x, y, 0.2);

            BorderFrstMarker.Draw(Page);

            BorderFrstCoord = new Coordinate(x, y);

            VisioInterop.DeselectAll(Window);

            drawingShape = true;
        }

        private void BorderScndPointClicked(double x, double y, ref bool drawingShape)
        {
            this.BorderGenerationState = BorderGenerationState.ScndPointSelected;

            this.BorderScndMarker = new BorderGenerationMarker(x, y, 0.2);

            BorderScndMarker.Draw(Page);

            BorderScndCoord = new Coordinate(x, y);

            drawBorderGuideLine();

            VisioInterop.DeselectAll(Window);

            drawingShape = true;
        }

        private void drawBorderGuideLine()
        {
            borderGuideLine = Page.DrawLine(BorderFrstCoord.X, BorderFrstCoord.Y, BorderScndCoord.X, BorderScndCoord.Y, string.Empty);

            VisioInterop.SetBaseLineColor(borderGuideLine, Color.Black);
            VisioInterop.SetBaseLineStyle(borderGuideLine, VisioLineStyle.HalfDot);
            VisioInterop.SetLineWidth(borderGuideLine, 2);
        }

        private void BorderDrctPointClicked(double x, double y, ref bool drawingShape)
        {
            double x1 = BorderFrstCoord.X;
            double x2 = BorderScndCoord.X;

            double y1 = BorderFrstCoord.Y;
            double y2 = BorderScndCoord.Y;

            double atan = MathUtils.Atan(x1, y1, x2, y2);

            Coordinate coord = new Coordinate(x, y);

            coord.Rotate(atan);

            Rectangle rectangle;

            if (y < 0.0)
            {
                Coordinate upperLeft = new Coordinate(x1, y2 + widthInLocalInches);
                Coordinate lowerRght = new Coordinate(x2, y2);

                rectangle = new Rectangle(upperLeft, lowerRght);
            }

            else
            {
                Coordinate upperLeft = new Coordinate(x1, y2 - widthInLocalInches);
                Coordinate lowerRght = new Coordinate(x2, y2);

                rectangle = new Rectangle(upperLeft, lowerRght);
            }

            BorderFrstMarker.Delete();

            BorderFrstCoord = BorderScndCoord;

            BorderFrstMarker = BorderScndMarker;

            BorderScndCoord = Coordinate.NullCoordinate;

            this.BorderGenerationState = BorderGenerationState.FrstPointSelected;
        }

    }

}
