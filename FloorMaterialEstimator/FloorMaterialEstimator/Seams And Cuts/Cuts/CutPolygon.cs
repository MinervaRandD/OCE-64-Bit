
namespace FloorMaterialEstimator.Seams_And_Cuts
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public class CutPolygon
    {
        public Visio.Shape visioShape = null;

        public Coordinate UpperLeftCorner;
        public Coordinate UpperRghtCorner;
        public Coordinate LowerRghtCorner;
        public Coordinate LowerLeftCorner;

        public List<CutLineSegment> PolygonPerimeter = new List<CutLineSegment>();

        public Coordinate UpperLeftBoundaryPoint(double y)
        {
            double minX = PolygonPerimeter.Min(s => Math.Min(s.Coord1.X, s.Coord2.X));

            return new Coordinate(minX, y);
        }

        public Coordinate LowerRightBoundaryPoint(double y)
        {
            double maxX = PolygonPerimeter.Max(s => Math.Max(s.Coord1.X, s.Coord2.X));
            
            return new Coordinate(maxX, y);
        }

       
        public CutPolygon() { }

        public CutPolygon(List<CutLineSegment> cutLineSegmentList)
        {
            PolygonPerimeter = cutLineSegmentList;
        }

        public void Add(CutLineSegment cutLineSegment)
        {
            if (PolygonPerimeter.Count > 0)
            {
                Debug.Assert(PolygonPerimeter[PolygonPerimeter.Count - 1].Coord2 == cutLineSegment.Coord1);
            }

            PolygonPerimeter.Add(cutLineSegment);
        }

        public CutLineSegment GetFirstLine()
        {
            if (PolygonPerimeter.Count <= 0)
            {
                return null;
            }

            return PolygonPerimeter[0];
        }

        public void Translate(Coordinate translateCoordinate)
        {
            PolygonPerimeter.ForEach(p => p.Translate(translateCoordinate));

            UpperLeftCorner.Translate(translateCoordinate);
            UpperRghtCorner.Translate(translateCoordinate);
            LowerRghtCorner.Translate(translateCoordinate);
            LowerLeftCorner.Translate(translateCoordinate);
        }

        internal void DeleteBoundary()
        {
            if (this.visioShape == null)
            {
                return;
            }

            this.visioShape.Delete();
        }

        internal void DrawBoundary(Page page)
        {
            visioShape = page.DrawPolyline(GetCoordinates(), 1);
            Graphics.FormatCutBox(visioShape);
        }

        internal void Rotate(double[,] rotationMatrix)
        {
            PolygonPerimeter.ForEach(p => p.Rotate(rotationMatrix));

            UpperLeftCorner.Rotate(rotationMatrix);
            UpperRghtCorner.Rotate(rotationMatrix);
            LowerRghtCorner.Rotate(rotationMatrix);
            LowerLeftCorner.Rotate(rotationMatrix);
        }
        
        internal void GenerateBoundaries(double y1, double y2)
        {
            Coordinate upperLeftCoord = UpperLeftBoundaryPoint(y1);
            Coordinate lowerRightCoord = LowerRightBoundaryPoint(y2);

            UpperLeftCorner = upperLeftCoord;
            UpperRghtCorner = new Coordinate(lowerRightCoord.X, upperLeftCoord.Y);
            LowerRghtCorner = lowerRightCoord;
            LowerLeftCorner = new Coordinate(upperLeftCoord.X, lowerRightCoord.Y);
        }

        internal double[] GetCoordinates()
        {
            double[] coordinates = new double[10];

            coordinates[0] = UpperLeftCorner.X;
            coordinates[1] = UpperLeftCorner.Y;

            coordinates[2] = UpperRghtCorner.X;
            coordinates[3] = UpperRghtCorner.Y;

            coordinates[4] = LowerRghtCorner.X;
            coordinates[5] = LowerRghtCorner.Y;

            coordinates[6] = LowerLeftCorner.X;
            coordinates[7] = LowerLeftCorner.Y;

            coordinates[8] = UpperLeftCorner.X;
            coordinates[9] = UpperLeftCorner.Y;

            return coordinates;
        }

        internal CutPolygon Clone()
        {
            List<CutLineSegment> clonedLineSegmentList = new List<CutLineSegment>();

            this.PolygonPerimeter.ForEach(p => clonedLineSegmentList.Add(p.Clone()));

            CutPolygon clonedCutPolygon = new CutPolygon(clonedLineSegmentList);

            clonedCutPolygon.UpperRghtCorner = this.UpperRghtCorner.Clone();
            clonedCutPolygon.UpperLeftCorner = this.UpperLeftCorner.Clone();
            clonedCutPolygon.LowerRghtCorner = this.LowerRghtCorner.Clone();
            clonedCutPolygon.LowerLeftCorner = this.LowerLeftCorner.Clone();

            return clonedCutPolygon;
        }
   
    }

}
