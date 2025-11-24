

namespace Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Utilities;

    public class PolygonDistanceGenerator
    {
        DirectedPolygon poly1;
        DirectedPolygon poly2;

        List<Coordinate> poly1RghtFacingCoords;
        List<Coordinate> poly2LeftFacingCoords;

        List<DirectedLine> poly1RghtFacingLines;
        List<DirectedLine> poly2LeftFacingLines;

        Dictionary<Coordinate, DirectedLine> poly1LinesIndexedByCoord1;
        Dictionary<Coordinate, DirectedLine> poly2LinesIndexedByCoord1;

        double maxPoly1X;
        double minPoly1X;

        double maxPoly2X;
        double minPoly2X;
        
        double poly2Offset;

        public PolygonDistanceGenerator(DirectedPolygon poly1, DirectedPolygon poly2)
        {
            this.poly1 = poly1;
            this.poly2 = poly2;
        }

        public double GenPolyDistance()
        {
            minPoly1X = poly1.MinX;
            maxPoly1X = poly1.MaxX;

            minPoly2X = poly2.MinX;
            maxPoly2X = poly2.MaxX;

            poly2Offset = maxPoly1X - minPoly2X;
            
            getPoly1RghtFacingElements();
            getPoly2LeftFacingElements();

            double minRghtFacingDistance = getMinRghtFacingDistance();
            double minLeftFacingDistance = getMinLeftFacingDistance();

            return Math.Min(minRghtFacingDistance, minLeftFacingDistance);
        }

        private void getPoly1RghtFacingElements()
        {
            poly1RghtFacingCoords = new List<Coordinate>();
            poly1RghtFacingLines = new List<DirectedLine>();

            HashSet<Tuple<double, double>> excludedCoordSet = new HashSet<Tuple<double, double>>();
            HashSet<Tuple<double, double>> includedCoordSet = new HashSet<Tuple<double, double>>();

            foreach (DirectedLine line in poly1)
            {
                if (line.IsHorizontal())
                {
                    if (line.Coord1.X <= line.Coord2.X)
                    {
                        excludedCoordSet.Add(line.Coord1.Key);
                    }

                    else
                    {
                        excludedCoordSet.Add(line.Coord2.Key);
                    }
                }
            }

            foreach (DirectedLine line in poly1)
            {
                if (line.IsHorizontal())
                {
                    continue;
                }

                // Note: this does not eliminate all lines that will never contribute to the minimum distance.
                // The purpose here is to pare down the list.

                if (excludedCoordSet.Contains(line.Coord1.Key) || excludedCoordSet.Contains(line.Coord2.Key))
                {
                    continue;
                }

                Coordinate coord1 = new Coordinate(line.Coord1.X - minPoly1X, line.Coord1.Y);
                Coordinate coord2 = new Coordinate(line.Coord2.X - minPoly1X, line.Coord2.Y);

                if (coord1.Y > coord2.Y)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                DirectedLine line2 = new DirectedLine(coord1, coord2);

                poly1RghtFacingLines.Add(line2);

                if (!includedCoordSet.Contains(coord1.Key))
                {
                    includedCoordSet.Add(coord1.Key);
                }

                if (!includedCoordSet.Contains(coord2.Key))
                {
                    includedCoordSet.Add(coord2.Key);
                }
            }

            foreach (Tuple<double, double> key in includedCoordSet)
            {
                poly1RghtFacingCoords.Add(new Coordinate(key.Item1, key.Item2));
            }

            minPoly1X = poly1RghtFacingCoords.Min(c => c.X);
            maxPoly1X = poly1RghtFacingCoords.Max(c => c.X);
        }

        private void getPoly2LeftFacingElements()
        {
            poly2Offset = maxPoly1X - minPoly2X;

            poly2LeftFacingCoords = new List<Coordinate>();
            poly2LeftFacingLines = new List<DirectedLine>();

            HashSet<Tuple<double, double>> excludedCoordSet = new HashSet<Tuple<double, double>>();
            HashSet<Tuple<double, double>> includedCoordSet = new HashSet<Tuple<double, double>>();

            foreach (DirectedLine line in poly2)
            {
                if (line.IsHorizontal())
                {
                    if (line.Coord1.X >= line.Coord2.X)
                    {
                        excludedCoordSet.Add(line.Coord1.Key);
                    }

                    else
                    {
                        excludedCoordSet.Add(line.Coord2.Key);
                    }
                }
            }

            foreach (DirectedLine line in poly2)
            {
                if (line.IsHorizontal())
                {
                    continue;
                }

                // Note: this does not eliminate all lines that will never contribute to the minimum distance.
                // The purpose here is to pare down the list.

                if (excludedCoordSet.Contains(line.Coord1.Key) || excludedCoordSet.Contains(line.Coord2.Key))
                {
                    continue;
                }

                Coordinate coord1 = new Coordinate(line.Coord1.X + poly2Offset, line.Coord1.Y);
                Coordinate coord2 = new Coordinate(line.Coord2.X + poly2Offset, line.Coord2.Y);

                if (coord1.Y > coord2.Y)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                DirectedLine line2 = new DirectedLine(coord1, coord2);

                poly2LeftFacingLines.Add(line2);

                if (!includedCoordSet.Contains(coord1.Key))
                {
                    includedCoordSet.Add(coord1.Key);
                }

                if (!includedCoordSet.Contains(coord2.Key))
                {
                    includedCoordSet.Add(coord2.Key);
                }
            }

            foreach (Tuple<double, double> key in includedCoordSet)
            {
                poly2LeftFacingCoords.Add(new Coordinate(key.Item1, key.Item2));
            }

            minPoly2X = poly2LeftFacingCoords.Min(c => c.X);
            maxPoly2X = poly2LeftFacingCoords.Max(c => c.X);
        }

        private double getMinRghtFacingDistance()
        {
            double minRghtFacingDistance = double.MaxValue;

            double poly1MaxX = maxPoly1X;
            double poly2MinX = minPoly2X;

            foreach (DirectedLine line in poly2LeftFacingLines)
            {
                Coordinate coord1 = line.Coord1;
                Coordinate coord2 = line.Coord2;

                Debug.Assert(coord1.Y < coord2.Y);
                
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

            double poly2MinX = minPoly2X;
            double poly1MaxX = maxPoly1X;

            foreach (DirectedLine line in poly1RghtFacingLines)
            {
                Coordinate coord1 = line.Coord1;
                Coordinate coord2 = line.Coord2;

                Debug.Assert(coord1.Y < coord2.Y);

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
