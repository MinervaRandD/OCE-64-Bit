using System;

using Geometry;
using Graphics;

namespace CanvasLib.Borders
{
    using Geometry;
    using Graphics;
    using Utilities;
    using FinishesLib;
    using System.Drawing;
    using System.Collections.Generic;

    public class BorderGuideBoundary
    {
        private GraphicsWindow window;

        private GraphicsPage page;

        private Coordinate coord1;

        private Coordinate coord2;

        private double widthInInches;

        public GraphicShape BoxLeft { get; set; }

        public GraphicShape BoxRght { get; set; }
        public AreaFinishBase AreaFinishBase { get; internal set; }

        private string guid;

        double[] boxRghtCoords;

        double[] boxLeftCoords;

        public BorderGuideBoundary(
            GraphicsWindow window
            , GraphicsPage page
            , Coordinate coord1
            , Coordinate coord2
            , double widthInInches
            , AreaFinishBase areaFinishBase)
        {
            this.window = window;

            this.page = page;

            this.coord1 = coord1;

            this.coord2 = coord2;

            this.widthInInches = widthInInches;

            this.AreaFinishBase = areaFinishBase;
        }

        internal void UpdateBorderGuideColors(bool IsLeftSelected, bool IsRghtSelected)
        {
            Color color = Color.White;
            double opacity = 1.0;

            if (Utilities.IsNotNull(AreaFinishBase))
            {
                color = AreaFinishBase.Color;
                opacity = AreaFinishBase.Opacity;
            }

            if (IsLeftSelected)
            {
                VisioInterop.SetBaseFillColor(BoxLeft, color);
                VisioInterop.SetFillOpacity(BoxLeft, opacity);
            }

            else
            {
                VisioInterop.SetBaseFillColor(BoxLeft, Color.White);
                VisioInterop.SetFillOpacity(BoxLeft, 0);
            }

            if (IsRghtSelected)
            {
                VisioInterop.SetBaseFillColor(BoxRght, color);
                VisioInterop.SetFillOpacity(BoxRght, opacity);
            }

            else
            {
                VisioInterop.SetBaseFillColor(BoxRght, Color.White);
                VisioInterop.SetFillOpacity(BoxRght, 0);
            }
        }

        internal void UpdateBoundaryWidth(double widthInLocalInches)
        {
            this.widthInInches = widthInLocalInches;

            this.Delete();

            this.Draw();

            window?.DeselectAll();
        }

        internal void Draw()
        {
            double length = BoundaryGuideLength();

            boxLeftCoords = generateBoxCoords(coord1, coord2, length);

            BoxLeft = page.DrawPolygon(this, boxLeftCoords);

            BoxLeft.Guid = GuidMaintenance.CreateGuid(BoxLeft);

            BoxLeft.SetFillOpacity(0);

            BoxLeft.SetLineStyle(VisioLineStyle.Dot);

            BoxLeft.SetLineWidth(4);

            BoxLeft.VisioShape.Data2 = "BoxLeft";
           
            boxRghtCoords = generateBoxCoords(coord2, coord1, length);

            BoxRght = page.DrawPolygon(this, boxRghtCoords);

            BoxRght.Guid = GuidMaintenance.CreateGuid(BoxRght);

            BoxRght.SetFillOpacity(0);

            BoxRght.SetLineStyle(VisioLineStyle.Dot);

            BoxRght.SetLineWidth(4);

            BoxRght.VisioShape.Data2 = "BoxRght";

            page.AddToPageShapeDict((IGraphicsShape) BoxLeft);
            page.AddToPageShapeDict((IGraphicsShape) BoxRght);

            window?.DeselectAll();
        }

        private double[] generateBoxCoords(Coordinate coord1, Coordinate coord2, double length)
        {

            Coordinate boxCoord1 = new Coordinate(0, 0);
            Coordinate boxCoord2 = new Coordinate(0, widthInInches);
            Coordinate boxCoord3 = new Coordinate(length, widthInInches);
            Coordinate boxCoord4 = new Coordinate(length, 0);

            double angle = MathUtils.Atan(coord1.X, coord1.Y, coord2.X, coord2.Y);

            boxCoord1.Rotate(angle);
            boxCoord2.Rotate(angle);
            boxCoord3.Rotate(angle);
            boxCoord4.Rotate(angle);

            boxCoord1.Translate(coord1);
            boxCoord2.Translate(coord1);
            boxCoord3.Translate(coord1);
            boxCoord4.Translate(coord1);


            double[] boxCoords = new double[10];

            boxCoords[0] = boxCoord1.X;
            boxCoords[1] = boxCoord1.Y;
            boxCoords[2] = boxCoord2.X;
            boxCoords[3] = boxCoord2.Y;
            boxCoords[4] = boxCoord3.X;
            boxCoords[5] = boxCoord3.Y;
            boxCoords[6] = boxCoord4.X;
            boxCoords[7] = boxCoord4.Y;
            boxCoords[8] = boxCoord1.X;
            boxCoords[9] = boxCoord1.Y;

            return boxCoords;
        }


        internal void Delete()
        {
            page.RemoveFromPageShapeDict(BoxLeft.Guid);
            page.RemoveFromPageShapeDict(BoxRght.Guid);

            BoxLeft.Delete();
            BoxRght.Delete();
        }

        internal double BoundaryGuideLength()
        {
            return MathUtils.H2Distance(coord1.X, coord1.Y, coord2.X, coord2.Y);
        }

        internal List<Coordinate> GenerateCoordinateList(bool IsLeftSelected, bool IsRghtSelected)
        {
            List<Coordinate> coordinateList = new List<Coordinate>();

            if (!IsLeftSelected && !IsRghtSelected)
            {
                return null;
            }

            double[] boxCoords;

            if (IsLeftSelected)
            {
                boxCoords = boxLeftCoords;
            }

            else
            {
                boxCoords = boxRghtCoords;
            }

          
            for (int i = 0; i < 9; i += 2)
            {
                Coordinate coord = new Coordinate(boxCoords[i], boxCoords[(i + 1)]);

                coordinateList.Add(coord);
            }

            return coordinateList;
        }
    }
}
