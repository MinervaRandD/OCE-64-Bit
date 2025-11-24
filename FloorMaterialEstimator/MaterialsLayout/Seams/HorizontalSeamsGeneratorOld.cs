//-------------------------------------------------------------------------------//
// <copyright file="HorizontalSeamsGenerator.cs"                                 //
//                  company="Bruun Estimating, LLC">                             // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace MaterialsLayout
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;
    using Geometry;
    using Utilities;

    public class HorizontalSeamsGeneratorOld
    {
        public List<Seam> SeamList;

        private List<DirectedLine> perimeter;

        private double minX;
        private double maxX;
        private double minY;
        private double maxY;

        private double seamWidth;

        public HorizontalSeamsGeneratorOld(LayoutArea layoutArea)
        {
            perimeter = new List<DirectedLine>();

            //foreach (DirectedLine directedLine in layoutArea)
            //{
            //    perimeter.Add(directedLine);
            //}
        }

        public HorizontalSeamsGeneratorOld(List<DirectedLine> lineList)
        {
            perimeter = new List<DirectedLine>();

            foreach (DirectedLine line in lineList)
            {
                perimeter.Add(new DirectedLine(line));
            }
        }

        public List<Seam> GenerateHorizontalSeamList(double seamWidth, double baseLineY = 0.0, bool includeBoundaries = false)
        {
            this.seamWidth = seamWidth;

            List<Seam> returnSeamList = new List<Seam>();

            minX = perimeter.Min(l => Math.Min(l.Coord1.X, l.Coord2.X));
            maxX = perimeter.Max(l => Math.Max(l.Coord1.X, l.Coord2.X));

            minY = perimeter.Min(l => Math.Min(l.Coord1.Y, l.Coord2.Y));
            maxY = perimeter.Max(l => Math.Max(l.Coord1.Y, l.Coord2.Y));

            double y = baseLineY;

            while (y <= maxY)
            {
                List<Seam> ySeamList = generateIntersectingSeamLines(y, includeBoundaries);

                returnSeamList.AddRange(ySeamList);

                y += seamWidth;
            }

            y = baseLineY - seamWidth;

            while (y >= minY)
            {
                List<Seam> ySeamList = generateIntersectingSeamLines(y, includeBoundaries);

                returnSeamList.AddRange(ySeamList);

                y -= seamWidth;
            }

            int count = perimeter.Count;

            this.SeamList = returnSeamList;

            return returnSeamList;
        }

        private List<Seam> generateIntersectingSeamLines(double y, bool includeBoundaries = false)
        {
            List<Seam> returnList = new List<Seam>();

            SortedList<Tuple<double, double>, int> intersectionList = generateIntersectingPoints(perimeter, y, includeBoundaries);

            for (int i = 0; i < intersectionList.Count; i += 2)
            {
                double x1 = intersectionList.Keys[i].Item1;
                double x2 = intersectionList.Keys[i + 1].Item2;

                int line1Index = intersectionList.Values[i];
                int line2Index = intersectionList.Values[i + 1];


                if (x1 > x2)
                {
                    Utilities.Swap(ref x1, ref x2);
                    Utilities.Swap(ref line1Index, ref line2Index);
                }

                Coordinate coord1 = new Coordinate(x1, y);
                Coordinate coord2 = new Coordinate(x2, y);

                Seam seam = new Seam(new DirectedLine(coord1, coord2), line1Index, line2Index);

                returnList.Add(seam);
            }

            return returnList;
        }

        private SortedList<Tuple<double, double>, int> generateIntersectingPoints(List<DirectedLine> transformedPerimeter, double y, bool includeBoundaries = false)
        {
            SortedList<Tuple<double, double>, int> intersectionList = new SortedList<Tuple<double, double>, int>();

            int count = transformedPerimeter.Count;

            for (int i = 0; i < count; i++)
            {
                DirectedLine line1 = transformedPerimeter[i];

                double x1 = line1.Coord1.X;
                double x2 = line1.Coord2.X;

                double y1 = line1.Coord1.Y;
                double y2 = line1.Coord2.Y;

                DirectedLine line2 = null;

                if (line1.IsHorizontal())
                {
                    if (!includeBoundaries)
                    {
                        continue;
                    }

                    if (line1.Coord1.Y != y)
                    {
                        continue;
                    }

                    line2 = transformedPerimeter[(i + 1) % count];

                    intersectionList.Add(new Tuple<double, double>(x2, line2.Coord1.X), i);
                   
                    continue;
                }

                // The big challenge here is going to be managing the numerical
                // precision issues.

                double yMin = Math.Min(y1, y2);
                double yMax = Math.Max(y1, y2);

                if (yMin > y || yMax < y)
                {
                    // This line does not intersect the current seam line level

                    // Keep in mind that the y coords are going negative.

                    continue;
                }

                if (yMin < y && yMax > y)
                {
                    // Intersection occurs in the middle of this line.
                    // Add the intersection point and move on. This is the easiest case.

                    //double xIntercept = (y - y1) * (x2 - x1) / (y2 - y1) + x1;
                    double xIntercept = line1.XInterceptForY(y);

                    intersectionList.Add(new Tuple<double, double>(xIntercept, xIntercept), i);

                    continue;
                }

                Debug.Assert(y1 == y || y2 == y, "Invalid state.");

                // At this point, either y1 or y2 lies on the base seam line.

                if (y == y1)
                {
                    // We handle whether or not to add an intersection line when y == y2

                    continue;
                }

                Debug.Assert(y2 == y, "Invalid state.");

                // Add this point, the second point lies on the seam line.

                Debug.Assert(y1 != y2, "Invalid state.");
                 
                Debug.Assert(!line1.IsHorizontal(), "Invalid state.");

                if (!includeBoundaries)
                {
                    line2 = GetNextNoneZeroSlopeLineInSequence(transformedPerimeter, i);
                }

                else
                {
                    line2 = transformedPerimeter[(i + 1) % count];
                }

                if ((line1.Coord1.Y > y && line2.Coord2.Y > y)
                    || (line1.Coord1.Y < y && line2.Coord2.Y < y))
                {
                    // This represents an apex either up or down, like /\ or \/, or a boundary.

                    if (!includeBoundaries)
                    {
                        continue;
                    }

                }

                // so now, we add the intersection point

                intersectionList.Add(new Tuple<double, double>(x2, line2.Coord1.X), i);
            }

            return intersectionList;
        }
        
        private DirectedLine GetNextNoneZeroSlopeLineInSequence(List<DirectedLine> transformedPerimeter, int i)
        {
            int iStart = i;
            int count = transformedPerimeter.Count;

            for (int iNext = (i + 1) % count; iNext != iStart; iNext = (iNext + 1) % count)
            {
                DirectedLine line = transformedPerimeter[iNext];

                if (!line.IsHorizontal())
                {
                    return line;
                }
            }

            return null;
        }
    }
}
