

namespace Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Utilities;

    public class PolygonDistanceGeneratorOld
    {
        DirectedPolygon poly1;
        DirectedPolygon poly2;

        List<Coordinate> poly1RghtFacingCoords;
        List<Coordinate> poly2LeftFacingCoords;

        List<DirectedLine> poly1RghtFacingLines;
        List<DirectedLine> poly2LeftFacingLines;

        Dictionary<Coordinate, DirectedLine> poly1LinesIndexedByCoord1;
        Dictionary<Coordinate, DirectedLine> poly2LinesIndexedByCoord1;

        public PolygonDistanceGeneratorOld(DirectedPolygon poly1, DirectedPolygon poly2)
        {
            this.poly1 = poly1;
            this.poly2 = poly2;
        }

        public double GenPolyDistance()
        {
            poly1LinesIndexedByCoord1 = poly1.LinesIndexedByCoord1();
            poly2LinesIndexedByCoord1 = poly2.LinesIndexedByCoord1();

            getPoly1RghtFacingCoords();
            getPoly2LeftFacingCoords();

            double minRghtFacingDistance = getMinRghtFacingDistance();
            double minLeftFacingDistance = getMinLeftFacingDistance();

            return Math.Min(minRghtFacingDistance, minLeftFacingDistance);
        }

        private void getPoly1RghtFacingCoords()
        {
            double maxX = poly1.MaxX;
           
            double maxY = poly1.MaxY;
            double minY = poly1.MinY;

            poly1RghtFacingCoords = new List<Coordinate>();
            poly1RghtFacingLines = new List<DirectedLine>();

            Coordinate maxYCoord = Coordinate.NullCoordinate;
            DirectedLine maxYLine = null;

            Coordinate minYCoord = Coordinate.NullCoordinate;
            DirectedLine minYLine = null;

            List<DirectedLine> maxYLineList = poly1.Where(l => (l.MaxY == maxY && !l.IsHorizontal())).ToList();
            List<DirectedLine> minYLineList = poly1.Where(l => (l.MinY == minY && !l.IsHorizontal())).ToList();

            double maxYmaxX = maxYLineList.Max(l => l.MaxX);
            double minYmaxX = minYLineList.Max(l => l.MaxX);

            maxYLine = maxYLineList.Where(l => l.MaxX == maxYmaxX).First();
            minYLine = minYLineList.Where(l => l.MaxX == minYmaxX).First();
            
            if (maxYLine.Coord1.Y == maxY)
            {
                maxYCoord = maxYLine.Coord1;
            }

            else
            {
                maxYCoord = maxYLine.Coord2;
            }

            if (minYLine.Coord1.Y == minY)
            {
                minYCoord = minYLine.Coord1;
            }

            else
            {
                minYCoord = minYLine.Coord2;
            }

            if (minYLine.Coord1 == minYCoord)
            {
                // Walk from bottom to top of right face of polygon.

                poly1RghtFacingCoords.Add(minYCoord);

                for (DirectedLine line = minYLine; line.Coord1 != maxYCoord; line = poly1LinesIndexedByCoord1[line.Coord2])
                {
                    poly1RghtFacingLines.Add(line);
                    poly1RghtFacingCoords.Add(line.Coord2);
                }
            }

            else
            {
                // Walk from top to bottom of right face of polygon.

                poly1RghtFacingCoords.Add(maxYCoord);

                for (DirectedLine line = maxYLine; line.Coord1 != minYCoord; line = poly1LinesIndexedByCoord1[line.Coord2])
                {
                    poly1RghtFacingLines.Add(line);
                    poly1RghtFacingCoords.Add(line.Coord2);
                }
            }
        }

        private void getPoly2LeftFacingCoords()
        {
            double minX = poly2.MinX;

            double maxY = poly2.MaxY;
            double minY = poly2.MinY;

            poly2LeftFacingCoords = new List<Coordinate>();
            poly2LeftFacingLines = new List<DirectedLine>();

            Coordinate maxYCoord = Coordinate.NullCoordinate;
            DirectedLine maxYLine = null;

            Coordinate minYCoord = Coordinate.NullCoordinate;
            DirectedLine minYLine = null;

            List<DirectedLine> maxYLineList = poly2.Where(l => (l.MaxY == maxY && !l.IsHorizontal())).ToList();
            List<DirectedLine> minYLineList = poly2.Where(l => (l.MinY == minY && !l.IsHorizontal())).ToList();

            double maxYminX = maxYLineList.Min(l => l.MinX);
            double minYminX = minYLineList.Min(l => l.MinX);

            maxYLine = maxYLineList.Where(l => l.MinX == maxYminX).First();
            minYLine = minYLineList.Where(l => l.MinX == minYminX).First();

            if (maxYLine.Coord1.Y == maxY)
            {
                maxYCoord = maxYLine.Coord1;
            }

            else
            {
                maxYCoord = maxYLine.Coord2;
            }

            if (minYLine.Coord1.Y == minY)
            {
                minYCoord = minYLine.Coord1;
            }

            else
            {
                minYCoord = minYLine.Coord2;
            }

            if (minYLine.Coord1 == minYCoord)
            {
                // Walk from bottom to top of left face of polygon.

                poly2LeftFacingCoords.Add(minYCoord);

                for (DirectedLine line = minYLine; line.Coord1 != maxYCoord; line = poly2LinesIndexedByCoord1[line.Coord2])
                {
                    poly2LeftFacingLines.Add(line);
                    poly2LeftFacingCoords.Add(line.Coord2);
                }
            }

            else
            {
                // Walk from top to bottom of left face of polygon.

                poly2LeftFacingCoords.Add(maxYCoord);

                for (DirectedLine line = maxYLine; line.Coord1 != minYCoord; line = poly2LinesIndexedByCoord1[line.Coord2])
                {
                    poly2LeftFacingLines.Add(line);
                    poly2LeftFacingCoords.Add(line.Coord2);
                }
            }
        }

        private double getMinRghtFacingDistance()
        {
            double minRghtFacingDistance = double.MaxValue;

            double poly1MaxX = poly1.MaxX;
            double poly2MinX = poly2.MinX;

            foreach (DirectedLine line in poly2LeftFacingLines)
            {
                Coordinate coord1 = line.Coord1;
                Coordinate coord2 = line.Coord2;

                if (coord1.Y > coord2.Y)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                double inverseSlope = (coord2.X - coord1.X) / (coord2.Y - coord1.Y);

                foreach (Coordinate coord in poly1RghtFacingCoords)
                {
                    if (coord.Y < coord1.Y || coord.Y > coord2.Y)
                    {
                        continue;
                    }

                    double deltaX = inverseSlope * (coord.Y - coord1.Y);

                    double x = (coord1.X + deltaX - poly2MinX) + (poly1MaxX - coord.X);

                    if (x < minRghtFacingDistance)
                    {
                        minRghtFacingDistance = x;
                    }
                }
            }

            return minRghtFacingDistance;
        }

        private double getMinLeftFacingDistance()
        {
            double minLeftFacingDistance = double.MaxValue;

            double poly2MinX = poly2.MinX;
            double poly1MaxX = poly1.MaxX;

            foreach (DirectedLine line in poly1RghtFacingLines)
            {
                Coordinate coord1 = line.Coord1;
                Coordinate coord2 = line.Coord2;

                if (coord1.Y > coord2.Y)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                double inverseSlope = (coord2.X - coord1.X) / (coord2.Y - coord1.Y);

                foreach (Coordinate coord in poly2LeftFacingCoords)
                {
                    if (coord.Y < coord1.Y || coord.Y > coord2.Y)
                    {
                        continue;
                    }

                    double deltaX = inverseSlope * (coord.Y - coord1.Y);

                    double x = (poly1MaxX - coord1.X - deltaX) + (coord.X - poly2MinX);

                    if (x < minLeftFacingDistance)
                    {
                        minLeftFacingDistance = x;
                    }
                }
            }

            return minLeftFacingDistance;
        }
    }
}
