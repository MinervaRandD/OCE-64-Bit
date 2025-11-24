
namespace Geometry
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;
    using System.Drawing;
    using TracerLib;

    public class DirectedPolygon: List<DirectedLine>
    {
        public delegate void PerimeterLineAddedHandler(DirectedLine directedLine);

        public event PerimeterLineAddedHandler PerimeterLineAdded;

        public void PerimeterAdd(DirectedLine line)
        {
            Add(line);

            if (Utilities.IsNotNull(PerimeterLineAdded))
            {
                PerimeterLineAdded.Invoke(line);
            }
        }

        public double MinY
        {
            get
            {
                return this.Min(l => l.MinY);
            }
        }

        public double MaxY
        {
            get
            {
                return this.Max(l => l.MaxY);
            }
        }

        public double MinX
        {
            get
            {
                return this.Min(l => l.MinX);
            }
        }

        public double MaxX
        {
            get
            {
                return this.Max(l => l.MaxX);
            }
        }

        public DirectedPolygon() { }

        public DirectedPolygon(List<DirectedLine> lineList, bool validate = true)
        {
            base.AddRange(lineList);

            if (validate)
            {
                Debug.Assert(HasValidPerimeter());
            }
        }

        public DirectedPolygon(DirectedPolyline directedPolyline)
        {
            base.AddRange(directedPolyline);

            Debug.Assert(HasValidPerimeter());
        }

        public DirectedPolygon(List<Coordinate> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DirectedLine line = new DirectedLine(list[i], list[(i + 1) % list.Count]);

                base.Add(line);
            }
        }

        public DirectedPolygon(double[] pointsList)
        {
            if (pointsList is null)
            {
                return;
            }

            if (pointsList.Length < 6)
            {
                return;
            }

            for (int i = 0; i < pointsList.Length; i += 2)
            {
                double x1 = pointsList[i];
                double y1 = pointsList[i + 1];
                double x2 = pointsList[(i + 2) % pointsList.Length];
                double y2 = pointsList[(i + 3) % pointsList.Length];

                DirectedLine line = new DirectedLine(x1, y1, x2, y2);

                base.Add(line);
            }
        }

        public void NumericallyCondition(int precision)
        {
            foreach (DirectedLine directedLine in this)
            {
                directedLine.NumericallyCondition(precision);
            }
        }

        public void Translate(Coordinate translateCoord)
        {
            base.ForEach(l => l.Translate(translateCoord));
        }


        public void Rotate(double theta)
        {
            double[,] rotationMatrix = new double[2, 2];

            rotationMatrix[0, 0] = Math.Cos(theta);
            rotationMatrix[0, 1] = -Math.Sin(theta);
            rotationMatrix[1, 0] = Math.Sin(theta);
            rotationMatrix[1, 1] = Math.Cos(theta);

            Rotate(rotationMatrix);
        }

        public void Rotate(double[,] rotationMatrix)
        {
            base.ForEach(l => l.Rotate(rotationMatrix));
        }

        public void Transform(Coordinate translateCoord, double[,] rotationMatrix)
        {
            Translate(translateCoord);
            Rotate(rotationMatrix);
        }

        public void GetMinMaxAtLevelY(double y, out double minX, out double maxX)
        {
            minX = double.MaxValue;
            maxX = double.MinValue;

            foreach (DirectedLine line in this)
            {
                if (line.IsHorizontal())
                {
                    if (line.Coord1.Y != y)
                    {
                        continue;
                    }

                    if (line.MinX < minX)
                    {
                        minX = line.MinX;
                    }

                    if (line.MaxX > maxX)
                    {
                        maxX = line.MaxX;
                    }

                    continue;
                }

                if (!line.IntersectsLevel(y))
                {
                    continue;
                }
                
                double x = line.XInterceptForY(y);

                if (x < minX)
                {
                    minX = x;
                }

                if (x > maxX)
                {
                    maxX = x;
                }
            }
        }

        public void GetMinMaxAtLevelY(double y1, double y2, out double? minXOut, out double? maxXOut)
        {
            minXOut = null;
            maxXOut = null;

            double minX = double.MaxValue;
            double maxX = double.MinValue;

            foreach (DirectedLine line in this)
            {
                if (line.IsHorizontal())
                {
                    if (line.Coord1.Y == y1 || line.Coord1.Y == y2)
                    {
                        if (line.MinX < minX)
                        {
                            minX = line.MinX;
                        }

                        if (line.MaxX > maxX)
                        {
                            maxX = line.MaxX;
                        }
                    }

                    continue;
                }

                if (line.IntersectsLevel(y1))
                {
                    double x = line.XInterceptForY(y1);

                    if (x < minX)
                    {
                        minX = x;
                    }

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                }

                if (line.IntersectsLevel(y2))
                {
                    double x = line.XInterceptForY(y2);

                    if (x < minX)
                    {
                        minX = x;
                    }

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                }

                if (line.Coord1.Y > y1 && line.Coord1.Y < y2)
                {
                    double x = line.Coord1.X;

                    if (x < minX)
                    {
                        minX = x;
                    }

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                }

                if (line.Coord2.Y > y1 && line.Coord2.Y < y2)
                {
                    double x = line.Coord2.X;

                    if (x < minX)
                    {
                        minX = x;
                    }

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                }
            }

            if (minX < double.MaxValue && maxX > double.MinValue)
            {
                minXOut = minX;
                maxXOut = maxX;
            }
        }

        public void InverseTransform(double inverseTheta, Coordinate inverseTranslateCoord)
        {
            Rotate(inverseTheta);
            Translate(inverseTranslateCoord);
        }

        public void InverseTransform(double[,] inverseRotationMatrix, Coordinate inverseTranslateCoord)
        {
            Rotate(inverseRotationMatrix);
            Translate(inverseTranslateCoord);
        }


        /// <summary>
        /// The following will provide meaningful results only if the lines in the directed polygon are
        /// either horizontal or vertical
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> ToHorizontalRectangleList(double tolerance = 0)
        {
            List<Rectangle> rtrnList = new List<Rectangle>();

            // This needs to be fixed up but if the polygon has no lines, return an empty list.

            if (this.Count <= 0)
            {
                return rtrnList;
            }

            double minY = this.MinY;
            double maxY = this.MaxY;

            HashSet<double> xVals = new HashSet<double>();

            foreach (DirectedLine directedLine in this)
            {
                double x1 = directedLine.Coord1.X;
                double x2 = directedLine.Coord2.X;

                Debug.Assert(directedLine.IsHorizontal(tolerance) || directedLine.IsVertical(tolerance));

                if (!xVals.Contains(x1))
                {
                    xVals.Add(x1);
                }

                if (!xVals.Contains(x2))
                {
                    xVals.Add(x2);
                }
            }

            List<double> xValList = new List<double>(xVals);

            xValList.Sort();

            for (int i = 0; i < xValList.Count - 1; i++)
            {
                double x1 = xValList[i];
                double x2 = xValList[i + 1];

                Coordinate coord1 = new Coordinate(x1, minY);
                Coordinate coord2 = new Coordinate(x1, maxY);
                Coordinate coord3 = new Coordinate(x2, maxY);
                Coordinate coord4 = new Coordinate(x2, minY);

                DirectedPolygon boundingPolygon = new DirectedPolygon(new List<Coordinate>() { coord1, coord2, coord3, coord4 });

                List<DirectedPolygon> intersectionList = this.Intersect(boundingPolygon);

                foreach (DirectedPolygon intersectionPolygon in intersectionList)
                {
                    Rectangle rectangle = polygonToRectangle(intersectionPolygon);

                    rtrnList.Add(rectangle);
                }
            }

            bool merged = true ;

            while (merged)
            {
                merged = mergeRectangles(rtrnList);
            }
            
            return rtrnList;
        }

        private bool mergeRectangles(List<Rectangle> rectangleList)
        {
            for (int i = 0; i < rectangleList.Count - 1; i++)
            {
                for (int j = i + 1; j < rectangleList.Count; j++)
                {
                    Rectangle mergedRectangle = mergeRectangles(rectangleList[i], rectangleList[j]);

                    if (!(mergedRectangle is null))
                    {
                        //rectangleList.Remove(rectangleList[j]);
                        //rectangleList.Remove(rectangleList[i]);

                        rectangleList.RemoveAt(j);
                        rectangleList.RemoveAt(i);


                        rectangleList.Add(mergedRectangle);

                        return true;
                    }
                }
            }

            return false;
        }

        private Rectangle mergeRectangles(Rectangle rectangle1, Rectangle rectangle2)
        {
            double maxY1 = rectangle1.UpperLeft.Y;
            double maxY2 = rectangle2.UpperLeft.Y;

            if (maxY1 != maxY2)
            {
                return null;
            }

            double minY1 = rectangle1.LowerLeft.Y;
            double minY2 = rectangle2.LowerLeft.Y;

            if (minY1 != minY2)
            {
                return null;
            }

            double maxX1 = rectangle1.LowerRght.X;
            double minX2 = rectangle2.LowerLeft.X;

            if (maxX1 == minX2)
            {
                return new Rectangle(rectangle1.UpperLeft, rectangle2.LowerRght);
            }

            double minX1 = rectangle1.LowerLeft.X;
            double maxX2 = rectangle2.LowerRght.X;

            if (minX1 == maxX2)
            {
                return new Rectangle(rectangle2.UpperLeft, rectangle1.LowerRght);
            }

            return null;
        }

        private Rectangle polygonToRectangle(DirectedPolygon polygon) =>
            new Rectangle(new Coordinate(polygon.MinX, polygon.MaxY), new Coordinate(polygon.MaxX, polygon.MinY));

        public DirectedPolygon Clone()
        {
            List<DirectedLine> clonedLineList = new List<DirectedLine>();

            base.ForEach(l => clonedLineList.Add(l.Clone()));

            return new DirectedPolygon(clonedLineList);
        }


        public DirectedLine LongestHorizontalLine()
        {
            double maxLen = double.MinValue;
            DirectedLine maxLine = null;

            foreach (DirectedLine directedLine in this)
            {
                if (!directedLine.IsHorizontal())
                {
                    continue;
                }

                double len = directedLine.Length;

                if (len > maxLen)
                {
                    maxLine = directedLine.Clone();
                }
            }

            return maxLine ;
        }

        public bool Equals(DirectedPolygon p)
        {
            if (p is null)
            {
                return false;
            }

            if (Count != p.Count)
            {
                return false;
            }

            List<Coordinate> coordList1 = CoordinateList();
            List<Coordinate> coordList2 = p.CoordinateList();

            //int indx1 = coordList2.IndexOf(coordList1[0]);

            int indx1 = Coordinate.IndexOf(coordList2, coordList1[0], 1.0e-5);

            if (indx1 < 0)
            {
                return false;
            }

            //int indx2 = coordList2.IndexOf(coordList1[1]);

            int indx2 = Coordinate.IndexOf(coordList2, coordList1[1], 1.0e-5);

            if (indx2 < 0)
            {
                return false;
            }

            if ((indx2 - indx1 + Count) % Count == 1)
            {
                for (int i = 2; i < Count; i++)
                {
                    Coordinate coord1 = coordList1[i];
                    Coordinate coord2 = coordList2[(i + indx1) % Count];

                    Coordinate coord = coord2 - coord1;

                    if (Math.Abs(coord.X) > 1.0e-5 || Math.Abs(coord.Y) > 1.0e-5)
                    {
                        return false;
                    }
                }

                return true;
            }

            else if ((indx1 - indx2 + Count) % Count == 1)
            {
                for (int i = 2; i < Count; i++)
                {
                    Coordinate coord1 = coordList1[i];
                    Coordinate coord2 = coordList2[(indx1 - i + Count) % Count];

                    Coordinate coord = coord2 - coord1;

                    if (Math.Abs(coord.X) > 1.0e-5 || Math.Abs(coord.Y) > 1.0e-5)
                    {
                        return false;
                    }
                }

                return true;
            }

            else
            {
                return false;
            }
        }

        public bool Contains(Coordinate coord)
        {
            DirectedLine testLine = new DirectedLine(new Coordinate(0, 0), coord);

            int intersectCount = 0;

            foreach (DirectedLine boundaryLine in this)
            {
                if (boundaryLine.Intersects(testLine))
                {
                    intersectCount++;
                }
            }

            return intersectCount % 2 == 1;
        }

        public bool Contains(DirectedPolygon p)
        {
            List<DirectedPolygon> results = this.Intersect(p);

            if (results.Count != 1)
            {
                return false;
            }

            return results[0] == p;
        }

        public bool ApproxContains(DirectedPolygon p, double precision)
        {
            List<DirectedPolygon> results = p.Subtract(this);

            if (results.Count <= 0)
            {
                return true;
            }

            double totlArea = results.Sum(dp => dp.AreaInSqrInches());

            return totlArea < precision;
        }

        public bool Contains(DirectedPolyline p)
        {
            foreach (DirectedLine line1 in this)
            {
                foreach (DirectedLine line2 in p)
                {
                    if (line1.Intersects(line2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool Intersects(DirectedLine intersectLine)
        {
            foreach (DirectedLine boundaryLine in this)
            {
                if (boundaryLine.Intersects(intersectLine))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Intersects(DirectedPolygon p)
        {
            return this.Intersect(p).Count > 0;
        }

        public bool ApproxIntersects(DirectedPolygon p, double precision)
        {
            return this.ApproxIntersect(p,precision).Count > 0;
        }

        public List<DirectedPolygon> Subtract(DirectedPolygon p)
        {
            return GeometryUtils.Subtract(this, p);
        }

        public List<DirectedPolygon> Subtract(List<DirectedPolygon> s)
        {
            return GeometryUtils.Difference(this, s);
        }

        public List<DirectedPolygon> Intersect(DirectedPolygon p)
        {
            return GeometryUtils.Intersection(this, p);
        }

        public List<DirectedPolygon> ApproxIntersect(DirectedPolygon p, double precision)
        {
            return GeometryUtils.ApproxIntersection(this, p, precision);
        }

        public static List<DirectedPolygon> Intersect(DirectedPolygon dp1, DirectedPolygon dp2)
        {
            return GeometryUtils.Intersection(dp1, dp2);
        }

        public static List<DirectedPolygon> Union(List<DirectedPolygon> dpl)
        {
            return GeometryUtils.UnionNew(dpl);
        }

        public static List<DirectedPolygon> Union1(List<DirectedPolygon> dpl)
        {
            return GeometryUtils.Union1(dpl);
        }

        public static List<DirectedPolygon> operator &(DirectedPolygon p1, DirectedPolygon p2)
        {
            return GeometryUtils.Intersection(p1, p2);
        }

        public Coordinate GetClosestVertex(double x, double y)
        {
            List<Coordinate> coordinateList = this.CoordinateList();

            Coordinate minCoord = Coordinate.NullCoordinate;
            double minDist = double.MaxValue;

            foreach (Coordinate coordinate in coordinateList)
            {
                double currDist = MathUtils.H2Distance(coordinate.X, coordinate.Y, x, y);

                if (currDist < minDist)
                {
                    minDist = currDist;
                    minCoord = coordinate;
                }
            }

            return minCoord;
        }

        public static bool operator ==(DirectedPolygon p1, DirectedPolygon p2)
        {
            if (p1 is null && p2 is null)
            {
                return true;
            }

            if (p1 is null || p2 is null)
            {
                return false;
            }

            return p1.Equals(p2);
        }

        public static bool operator !=(DirectedPolygon p1, DirectedPolygon p2)
        {
            if (p1 is null && p2 is null)
            {
                return false;
            }

            if (p1 is null || p2 is null)
            {
                return true;
            }

            return !p1.Equals(p2);
        }

        /// <summary>
        /// Uses 'shoelace algorithm. Wikipedia reference: https://en.wikipedia.org/wiki/Shoelace_formula
        /// </summary>
        /// <returns>Area of polygon</returns>

        public double AreaInSqrInches(double scaleFactor = 1.0)
        {
            Debug.Assert(ValidateConsistentPerimeter());

            double area = 0;

            int count = 0;

            for (int i = 0; i < Count; i++)
            {
                double x1 = this[i].Coord1.X;
                double x2 = this[(i + 1) % Count].Coord1.X;

                double y1 = this[i].Coord1.Y;
                double y2 = this[(i + 1) % Count].Coord1.Y;

                area += x1 * y2 - x2 * y1;

                count++;
            }

            return 0.5 * Math.Abs(area) * Math.Pow(scaleFactor, 2.0) ;
        }

        public Coordinate Centroid()
        {
            double[] coordinateArray = this.CoordinateArray();

            int n = coordinateArray.Length / 2 - 1;

            double a = 0;

            double cx = 0.0;
            double cy = 0.0;

            for (int i = 0; i < n;i++)
            {
                int indx = 2 * i;
                int indx_p1 = indx + 2;

                double x_i = coordinateArray[indx];
                double x_ip1 = coordinateArray[indx_p1];
                double y_i = coordinateArray[indx + 1];
                double y_ip1 = coordinateArray[indx_p1 + 1];

                double xy = (x_i * y_ip1 - x_ip1 * y_i);

                cx += (x_i + x_ip1) * xy;
                cy += (y_i + y_ip1) * xy;

                a += xy;
            }

            a /= 2.0;

            cx /= 6.0 * a;
            cy /= 6.0 * a;

            return new Coordinate(cx, cy);
        }
        public double[] CoordinateArray()
        {
            int count = this.Count + 1;

            double[] coordinateArray = new double[2 * count];

            for (int i = 0; i < count; i++)
            {
                int indx = i % this.Count;

                coordinateArray[2 * i] = this[indx].Coord1.X;
                coordinateArray[2 * i + 1] = this[indx].Coord1.Y;
            }

            return coordinateArray;
        }

        public DirectedPolygon RoundCoordinates(int precision)
        {
            List<Coordinate> inptList = CoordinateList();
            List<Coordinate> outpList = new List<Coordinate>();
            
            foreach (Coordinate coord in inptList)
            {
                outpList.Add(new Coordinate(Math.Round(coord.X, precision), Math.Round(coord.Y, precision)));
            }

            List<DirectedLine> lineList = new List<DirectedLine>();

            for (int i = 0; i < outpList.Count; i++)
            {
                lineList.Add(new DirectedLine(outpList[i], outpList[(i + 1) % outpList.Count]));
            }

            return new DirectedPolygon(lineList);
        }

        public List<Coordinate> CoordinateList()
        {
            List<Coordinate> returnList = new List<Coordinate>();

            for (int i = 0; i < Count; i++)
            {
                returnList.Add(this[i].Coord1);
            }

            return returnList;
        }


        public PointF[] GeneratePointFCoordinates()
        {
            int count = base.Count;

            PointF[] coordinates = new PointF[count + 1];

            for (int i = 0; i <= count; i++)
            {
                Coordinate coord = base[i % count].Coord1;

                coordinates[i] = new PointF((float)coord.X, (float)coord.Y);
            }

            return coordinates;
        }

        public Dictionary<Coordinate, DirectedLine> LinesIndexedByCoord1()
        {
            Dictionary<Coordinate, DirectedLine> returnDict = new Dictionary<Coordinate, DirectedLine>();

            this.ForEach(l => returnDict.Add(l.Coord1, l));

            return returnDict;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public double Orientation()
        {
            if (Count <= 1)
            {
                return 0;
            }

            List<Tuple<double, double>> coordinateList = new List<Tuple<double, double>>();

            coordinateList.Add(new Tuple<double, double>(this[0].Coord1.X, this[0].Coord1.Y));

            for (int i = 0; i < Count; i++)
            {
                coordinateList.Add(new Tuple<double, double>(this[i].Coord2.X, this[i].Coord2.Y));
            }

            return MathUtils.CurveOrientation(coordinateList);
        }


        public bool HasValidPerimeter()
        {
            HashSet<Tuple<double, double>> visitedCoordinates = new HashSet<Tuple<double, double>>();

            for (int i = 0; i < Count; i++)
            {
                Coordinate coord2 = base[i].Coord2;
                Coordinate coord1 = base[(i + 1) % Count].Coord1;
               
                if (coord2 != coord1)
                {
                    return false;
                }

                if (visitedCoordinates.Contains(coord2.Key))
                {
                    return false;
                }

                visitedCoordinates.Add(coord2.Key);
            }

            return true;
        }

        /// <summary>
        /// Not fully tested.
        /// </summary>
        /// <returns></returns>
        public bool HasIntersectingLines()
        {
            int count = this.Count;

            if (count <= 3)
            {
                return false;
            }

            for (int l1 = 0; l1 < count - 1; l1++)
            {
                for (int l2 = l1 + 1; l2 < count; l2++)
                {
                    DirectedLine line1 = this[l1];
                    DirectedLine line2 = this[l2];

                    Coordinate intersectionCoord = line1.Intersect(line2);

                    if (Coordinate.IsNullCoordinate(intersectionCoord))
                    {
                        continue;
                    }

                    if (intersectionCoord == line1.Coord2 && intersectionCoord == line2.Coord1)
                    {
                        continue;
                    }

                    if (intersectionCoord == line1.Coord1 && intersectionCoord == line2.Coord2)
                    {
                        continue;
                    }

                    return true;
                }
            }

            return false;
        }

        public bool ValidateCompleteL()
        {
            if (Count <= 3)
            {
                return false;
            }

            if (!HasValidPerimeter())
            {
                return false;
            }

            if (!ValidateCompleteL(base[Count - 2]))
            {
                return false;
            }


            if (!ValidateCompleteL(base[Count - 1]))
            {
                return false;
            }

            return true;
        }

        public bool ValidateCompleteL(DirectedLine cmprLine)
        {
            int count = this.Count;

            if (count < 3)
            {
                return true;
            }

            DirectedLine frstLine = base[0];
            DirectedLine lastLine = base[Count - 3];

            if (!validateCompleteLStartorEndPoint(frstLine, cmprLine))
            {
                return false;
            }

            if (!validateCompleteLStartorEndPoint(lastLine, cmprLine))
            {
                return false;
            }

            for (int i = 1; i < Count - 3; i++)
            {
                DirectedLine currLine = base[i];

                Coordinate intersectCoord = cmprLine.Intersect(currLine);

                if (!Coordinate.IsNullCoordinate(intersectCoord))
                {
                    return false;
                }
            }

            return true;
        }

        private bool validateCompleteLStartorEndPoint(
            DirectedLine polyLine
            , DirectedLine cmprLine
            , double precision = 1.0e-8)
        {
            Coordinate intersectCoord = cmprLine.Intersect(polyLine);

            if (Coordinate.IsNullCoordinate(intersectCoord))
            {
                return true;
            }

            // Numerical precision is an issue here in doing the comparison. If this is not done this way then the
            // following test will erroneously fail under certain circumstances

            if (Coordinate.Equals(intersectCoord, polyLine.Coord1, precision) || Coordinate.Equals(intersectCoord, polyLine.Coord2, precision) &&
                (Coordinate.Equals(intersectCoord, cmprLine.Coord1, precision) || Coordinate.Equals(intersectCoord, cmprLine.Coord2, precision)))
            {
                return !cmprLine.ProperOverlaps(polyLine);
            }

            // The followng is the original version which failed if values were off by very small amounts.
            //
            //if ((intersectCoord == polyLine.Coord1 || intersectCoord == polyLine.Coord2) &&
            //    (intersectCoord == cmprLine.Coord1 || intersectCoord == cmprLine.Coord2))
            //{
            //    return !cmprLine.ProperOverlaps(polyLine);
            //}

            return false;
        }

        #region Debug Support

        public bool ValidateConsistentPerimeter()
        {
            for (int i = 0; i < Count; i++)
            {
                Coordinate c1 = this[i].Coord2;
                Coordinate c2 = this[(i + 1) % Count].Coord1;

                if (c1 != c2)
                {
                    return false;
                }
            }

            return true;
        }

        public string ToString(string delimeter)
        {
            string rtrnValu = string.Empty;

            foreach (var x in this)
            {
                rtrnValu += x.ToString(delimeter) + delimeter;
            }

            return rtrnValu;
        }

        #endregion

    }
}
