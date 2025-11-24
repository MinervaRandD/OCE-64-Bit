//-------------------------------------------------------------------------------//
// <copyright file="DirectedLine.cs" company="Bruun Estimating, LLC">            // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//


namespace Geometry
{
    using System;
    using System.Collections.Generic;
    using Utilities;
    using VectorLib;

    [Serializable]
    public class DirectedLine
    {
        public Coordinate Coord1;

        public Coordinate Coord2;

        public bool IsSeamable { get; set; } = false;

        public DirectedLine() { }

        public DirectedLine(Coordinate coord1, Coordinate coord2)
        {
            this.Coord1 = coord1;
            this.Coord2 = coord2;
        }

        public DirectedLine(Line line)
        {
            this.Coord1 = line.Coord1;
            this.Coord2 = line.Coord2;
        }

        public DirectedLine(DirectedLine line)
        {
            this.Coord1 = line.Coord1;
            this.Coord2 = line.Coord2;
        }

        public DirectedLine(double x1, double y1, double x2, double y2)
        {
            Coord1 = new Coordinate(x1, y1);
            Coord2 = new Coordinate(x2, y2);
        }

        public double Slope
        {
            get
            {
                if (IsHorizontal())
                {
                    return 0;
                }

                return (Coord2.Y - Coord1.Y) / (Coord2.X - Coord1.X);
            }
        }

        public double Intercept
        {
            get
            {
                if (IsHorizontal())
                {
                    if (Coord1.Y == 0)
                    {
                        return 0;
                    }

                    else
                    {
                        return double.NaN;
                    }
                }

                return Coord1.Y - Slope * Coord1.X;
            }
        }

        internal void NumericallyCondition(int precision)
        {
            Coord1.X = Math.Round(Coord1.X, precision);
            Coord1.Y = Math.Round(Coord1.Y, precision);

            Coord2.X = Math.Round(Coord2.X, precision);
            Coord2.Y = Math.Round(Coord2.Y, precision);
        }

        public Tuple<Coordinate, Coordinate> Key
        {
            get
            {
                return new Tuple<Coordinate, Coordinate>(Coord1, Coord2);
            }
        }

        public double MinX
        {
            get
            {
                return Math.Min(Coord1.X, Coord2.X);
            }
        }

        public double MaxX
        {
            get
            {
                return Math.Max(Coord1.X, Coord2.X);
            }
        }

        public double MinY
        {
            get
            {
                return Math.Min(Coord1.Y, Coord2.Y);
            }
        }

        public double MaxY
        {
            get
            {
                return Math.Max(Coord1.Y, Coord2.Y);
            }
        }

        public double Length
        {
            get
            {
                return GeometryUtils.H2Dist(Coord1, Coord2);
            }
        }

        public Tuple<Coordinate, Coordinate> NormalizedKey
        {
            get
            {
                if (Coord1 <= Coord2)
                {
                    return new Tuple<Coordinate, Coordinate>(Coord1, Coord2);
                }

                else
                {
                    return new Tuple<Coordinate, Coordinate>(Coord2, Coord1);
                }
            }
        }

        public bool Intersects(DirectedLine directedLine)
        {
            Coordinate coord = this.Intersect(directedLine, true);

            return !Coordinate.IsNullCoordinate(coord);
        }


        public bool IntersectsOrOverlaps(DirectedLine directedLine)
        {
            Coordinate coord = this.Intersect(directedLine, true);

            if (Coordinate.IsNullCoordinate(coord))
            {
                return this.Overlaps(directedLine);
            }

            return true;
        }

        public bool IntersectsInterior(DirectedLine directedLine)
        {
            if (directedLine.Coord1 == Coord1)
            {
                directedLine.ExtendLine(1.0e-4);
            }
            Coordinate coord = this.Intersect(directedLine, true);
            return !Coordinate.IsNullCoordinate(coord);
        }

        public bool IntersectsLevel(double y)
        {
            if (Coord1.Y <= y && Coord2.Y >= y)
            {
                return true;
            }

            if (Coord1.Y >= y && Coord2.Y <= y)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Gets the point on a line segment that is closes to the input coordinate. Refer to wiki page:
        /// https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
        /// </summary>
        /// <param name="coord">The coordinate used to find the nearest point</param>
        /// <returns>Returns the point on a line segment that is closes to the input coordinate</returns>
        public Coordinate GetNearestPointOnLineToCoord(Coordinate coord)
        {
           if (this.IsVertical(1e-5))
            {
                double minY = this.MinY;
                double maxY = this.MaxY;

                if (coord.Y <= minY)
                {
                    if (Coord1.Y == minY)
                    {
                        return Coord1;
                    }

                    else
                    {
                        return Coord2;
                    }
                }

                else if (coord.Y >= maxY)
                {
                    if (Coord1.Y == maxY)
                    {
                        return Coord1;
                    }

                    else
                    {
                        return Coord2;
                    }
                }

                else
                {
                    double aveX = 0.5 * (Coord1.X + Coord2.X);

                    // Average is a surrogate for getting the actual x value when the slope is effectively infinite.
                    // May result in numeric precision issues.

                    return new Coordinate(aveX, coord.Y);
                }
            }

            double minX = this.MinX;
            double maxX = this.MaxX;

            double a = this.Slope;
            double c = Coord1.Y - a * Coord1.X;

            double x0 = coord.X;
            double y0 = coord.Y;

            double d = 1.0 + a * a;

            double xi = (x0 + a * y0 - a * c) / d;
            double yi = (a * (x0 + a * y0) + c) / d;

            if (xi <= minX)
            {
                if (Coord1.X == minX)
                {
                    return Coord1;
                }

                else
                {
                    return Coord2;
                }
            }

            else if (xi >= maxX)
            {
                if (Coord1.X == maxX)
                {
                    return Coord1;
                }

                else
                {
                    return Coord2;
                }
            }

            else
            {
                return new Coordinate(xi, yi);
            }
        }

        /// <summary>
        /// Returns the center point of the line
        /// </summary>
        /// <returns></returns>
        public Coordinate Center()
        {
            return new Coordinate(0.5 * (Coord1.X + Coord2.X), 0.5 * (Coord1.Y + Coord2.Y));
        }

        /// <summary>
        /// Extends both ends of a line slightly to cover numerical imprecssions to guarantee intersections.
       
        /// </summary>
        /// <param name="extensionLength">The length or increment of the extension</param>
        /// <returns>Returns a new directed line slightly extended at the start point</returns>
        public DirectedLine ExtendLine(double extensionLength)
        {
            // Make line horizontal

 
            DirectedLine rotatedLine = this.Clone();

            double atan = rotatedLine.Atan();

            rotatedLine.Rotate(-atan);

            Coordinate coord1 = new Coordinate(rotatedLine.Coord1.X - extensionLength, rotatedLine.Coord1.Y);
            Coordinate coord2 = new Coordinate(rotatedLine.Coord2.X + extensionLength, rotatedLine.Coord2.Y);

            DirectedLine extendedLine = new DirectedLine(coord1, coord2);

            extendedLine.Rotate(atan);

            return extendedLine;
        }

        /// <summary>
        /// ExtendStart extends the line slightly at the start point. This is used to make sure
        /// that lines will intersect (in sub-division) when one line T's with another. Slight
        /// numerical precision errors may otherwise make the lines not intersect.
        /// </summary>
        /// <param name="extensionLength">The length or increment of the extension</param>
        /// <returns>Returns a new directed line slightly extended at the start point</returns>
        public DirectedLine ExtendStart(double extensionLength)
        {
            double DX = Coord2.X - Coord1.X;
            double DY = Coord2.Y - Coord1.Y;

            Coordinate coord1;

            if (Math.Abs(DX) >= Math.Abs(DY))
            {
                double s = DY / DX;

                double dx = extensionLength / Math.Sqrt(s * s + 1.0);
                double dy = s * dx;

                if (Coord1.X < Coord2.X)
                {
                    coord1 = new Coordinate(Coord1.X - dx, Coord1.Y - dy);
                }

                else
                {
                    coord1 = new Coordinate(Coord1.X + dx, Coord1.Y + dy);
                }
            }

            else
            {
                double si = DX / DY;

                double dy = extensionLength / Math.Sqrt(si * si + 1.0);
                double dx = si * dy;

                if (Coord1.Y < Coord2.Y)
                {
                    coord1 = new Coordinate(Coord1.X - dx, Coord1.Y - dy);
                }

                else
                {
                    coord1 = new Coordinate(Coord1.X + dx, Coord1.Y + dy);
                }
            }

            return new DirectedLine(coord1, Coord2);
        }

        /// <summary>
        /// ExtendEnd extends the line slightly at the end point. This is used to make sure
        /// that lines will intersect (in sub-division) when one line T's with another. Slight
        /// numerical precision errors may otherwise make the lines not intersect.
        /// </summary>
        /// <param name="extensionLength">The length or increment of the extension</param>
        /// <returns>Returns a new directed line slightly extended at the end point</returns>
        public DirectedLine ExtendEnd(double extensionLength)
        {
            double DX = Coord2.X - Coord1.X;
            double DY = Coord2.Y - Coord1.Y;

            Coordinate coord2;

            if (Math.Abs(DX) >= Math.Abs(DY))
            {
                double s = DY / DX;

                double dx = extensionLength / Math.Sqrt(s * s + 1.0);
                double dy = s * dx;

                if (Coord1.X < Coord2.X)
                {
                    coord2 = new Coordinate(Coord2.X + dx, Coord2.Y + dy);
                }

                else
                {
                    coord2 = new Coordinate(Coord2.X - dx, Coord2.Y - dy);
                }
            }

            else
            {
                double si = DX / DY;

                double dy = extensionLength / Math.Sqrt(si * si + 1.0);
                double dx = si * dy;

                if (Coord1.Y < Coord2.Y)
                {
                    coord2 = new Coordinate(Coord2.X + dx, Coord2.Y + dy);
                }

                else
                {
                    coord2 = new Coordinate(Coord2.X - dx, Coord2.Y - dy);
                }
            }

            return new DirectedLine(Coord1, coord2);
        }

        /// <summary>
        /// Determines whether a coordinate falls on the line segment.
        /// </summary>
        /// <param name="coord">The coordinate to test</param>
        /// <returns></returns>
        public bool Contains(Coordinate coord, double tolerance = 1e-2)
        {
            double x = coord.X;

            double x1 = Coord1.X;
            double x2 = Coord2.X;

            double y = coord.Y;

            double y1 = Coord1.Y;
            double y2 = Coord2.Y;

            if (IsHorizontal(1e-4))
            {
                double yAve = (y1 + y2) * 0.5;

                if (Math.Abs(yAve-y) >= tolerance)
                {
                    return false;
                }

                return MathUtils.FallsBetween(x, x1, x2);
            }

            if (IsVertical(1e-4))
            {
                double xAve = (x1 + x2) * 0.5;

                if (Math.Abs(xAve - x) >= tolerance)
                {
                    return false;
                }

                return MathUtils.FallsBetween(y, y1, y2);
            }

            double denomX = x2 - x1;

            double denomY = y2 - y1;

            double deltaX = x / denomX;

            double deltaY = y / denomY;

            if (Math.Abs(deltaX - deltaY) > tolerance)
            {
                return false;
            }

            return MathUtils.FallsBetween(x, x1, x2);
        }

        /// <summary>
        /// Determine whether a coordinate falls on a line segment but not on the endpoints
        /// </summary>
        /// <param name="coord">The coordinate to test</param>
        /// <returns></returns>
        public bool ProperContains(Coordinate coord, double tolerance = 1e-2)
        {
            double x = coord.X;

            double x1 = Coord1.X;
            double x2 = Coord2.X;

            double y = coord.Y;

            double y1 = Coord1.Y;
            double y2 = Coord2.Y;

            if (IsHorizontal(1e-4))
            {
                double yAve = (y1 + y2) * 0.5;

                if (Math.Abs(yAve - y) >= tolerance)
                {
                    return false;
                }

                return MathUtils.ProperFallsBetween(x, x1, x2);
            }

            if (IsVertical(1e-4))
            {
                double xAve = (x1 + x2) * 0.5;

                if (Math.Abs(xAve - x) >= tolerance)
                {
                    return false;
                }

                return MathUtils.ProperFallsBetween(y, y1, y2);
            }

            double denomX = x2 - x1;

            double denomY = y2 - y1;

            double deltaX = x / denomX;

            double deltaY = y / denomY;

            if (Math.Abs(deltaX - deltaY) > tolerance)
            {
                return false;
            }

            return MathUtils.ProperFallsBetween(x, x1, x2);
        }

        public bool ContainsByXDimension(DirectedLine line2)
        {
            DirectedLine line1 = this;

            if (this.MinX <= line2.MinX && this.MaxX >= line2.MaxX)
            {
                return true;
            }

            return false;
        }

        public double XInterceptForY(double y)
        {
            double x1 = Coord1.X;
            double x2 = Coord2.X;

            double y1 = Coord1.Y;
            double y2 = Coord2.Y;

            double deltaX = x2 - x1;
            double deltaY = y2 - y1;

            if (Math.Abs(deltaX) > Math.Abs(deltaY) && Math.Abs(deltaX) > 1.0e-100)
            {
                double slope = deltaY / deltaX;

                if (slope == 0)
                {
                    return (0.5 * (x1 + x2));
                }

                double intercept = y1 - slope * x1;

                return (y - intercept) / slope;
            }

            else if (Math.Abs(deltaY) > 1.0e-100)
            {
                double slope = deltaX / deltaY;

                double intercept = x1 - slope * y1;

                return slope * y + intercept;
            }

            return 0.5 * (x1 + x2);
        }

        public bool IsHorizontal(double tolerance = 0.0)
        {
            return Math.Abs(Coord1.Y - Coord2.Y) <= tolerance;
        }

        public bool IsVertical(double tolerance = 0.0)
        {
            return Math.Abs(Coord1.X - Coord2.X) <= tolerance;
        }

        public double NormalizedAtan()
        {
            double deltaY = MaxY - MinY;
            double deltaX = MaxX - MinX;

            return Math.Atan2(deltaY, deltaX);
        }

        public void Translate(Coordinate xlateCoord)
        {
            Coord1.Translate(xlateCoord);
            Coord2.Translate(xlateCoord);
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
            Coord1.Rotate(rotationMatrix);
            Coord2.Rotate(rotationMatrix);
        }

        public bool HasPointsBelow(double y)
        {
            return Coord1.Y < y || Coord2.Y < y;
        }

        public bool HasPointsAbove(double y)
        {
            return Coord1.Y > y || Coord2.Y > y;
        }

        public double Atan()
        {
            double deltaY = Coord2.Y - Coord1.Y;
            double deltaX = Coord2.X - Coord1.X;

            return Math.Atan2(deltaY, deltaX);
        }

        public DirectedLine Clone()
        {
            return new DirectedLine(this.Coord1.Clone(), this.Coord2.Clone());
        }

        public static DirectedLine operator *(double s, DirectedLine l)
        {
            return new DirectedLine(s * l.Coord1, s * l.Coord2);
        }

        public static bool operator ==(DirectedLine l1, DirectedLine l2)
        {
            if (l1 is null && l2 is null)
            {
                return true;
            }

            if (l1 is null || l2 is null)
            {
                return false;
            }

            return l1.Coord1 == l2.Coord1 && l1.Coord2 == l2.Coord2;
        }

        public static bool operator !=(DirectedLine l1, DirectedLine l2)
        {
            if (l1 is null && l2 is null)
            {
                return false;
            }

            if (l1 is null || l2 is null)
            {
                return true;
            }

            return l1.Coord1 != l2.Coord1 || l1.Coord2 != l2.Coord2;
        }

        public override bool Equals(object obj)
        {
            DirectedLine l1 = this;
            DirectedLine l2 = (DirectedLine)obj;

            if (l2 is null)
            {
                return false;
            }

            return l1 == l2;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    
        public Coordinate Intersect(DirectedLine line2, bool considerColinearOverlapAsIntersect = false)
        {
            Vector result = new Vector();

            if (!VectorLib.Vector.LineSegementsIntersect(
                (Vector)Coord1
                , (Vector)Coord2
                , (Vector)line2.Coord1
                , (Vector)line2.Coord2
                , out result
                , considerColinearOverlapAsIntersect))
            {
                return Coordinate.NullCoordinate;
            }

            Coordinate coord = new Coordinate(result.X, result.Y);

            return coord;
        }

        // TODO: Come up with a better implementation of this.
        public bool Overlaps(DirectedLine line2, double tolerance = 1e-8)
        {
            return VectorLib.Vector.LineSegmentsOverlap(
                (Vector)Coord1
                , (Vector)Coord2
                , (Vector)line2.Coord1
                , (Vector)line2.Coord2);
        }

        public bool ProperOverlaps(DirectedLine line2, double tolerance = 1e-8)
        {
           
            double theta = -this.Atan();

            double[,] rotationMatrix = new double[2, 2];

            rotationMatrix[0, 0] = Math.Cos(theta);
            rotationMatrix[0, 1] = -Math.Sin(theta);
            rotationMatrix[1, 0] = Math.Sin(theta);
            rotationMatrix[1, 1] = Math.Cos(theta);

            Coordinate coord1 = this.Coord1.Clone();
            Coordinate coord2 = this.Coord2.Clone();

            Coordinate coord3 = line2.Coord1;
            Coordinate coord4 = line2.Coord2;

            coord1.Translate(-Coord1);
            coord2.Translate(-Coord1);

            coord3.Translate(-Coord1);
            coord4.Translate(-Coord1);

            coord1.Rotate(rotationMatrix);
            coord2.Rotate(rotationMatrix);
            coord3.Rotate(rotationMatrix);
            coord4.Rotate(rotationMatrix);

            if (Math.Abs(coord3.Y) > tolerance || Math.Abs(coord4.Y) > tolerance)
            {
                return false;
            }

            double x1 = coord1.X;
            double x2 = coord2.X;
            double x3 = coord3.X;
            double x4 = coord4.X;

            if (x1 > x2)
            {
                Utilities.Swap(ref x1, ref x2);
            }

            if (x3 > x4)
            {
                Utilities.Swap(ref x3, ref x4);
            }

            if (x2 <= x3)
            {
                return false;
            }

            if (x4 <= x1)
            {
                return false;
            }

            return true;


            //return VectorLib.Vector.LineSegementsProperOverlap(
            //    (Vector)Coord1
            //    , (Vector)Coord2
            //    , (Vector)line2.Coord1
            //    , (Vector)line2.Coord2);
        }

        public bool Contains(DirectedLine line)
        {
            if (!MathUtils.IsBetween(Coord1.X, Coord1.Y, Coord2.X, Coord2.Y, line.Coord1.X, line.Coord1.Y, 0.0001))
            {
                return false;
            }

            if (!MathUtils.IsBetween(Coord1.X, Coord1.Y, Coord2.X, Coord2.Y, line.Coord2.X, line.Coord2.Y, 0.0001))
            {
                return false;
            }

            return true;
        }

        public string ToNormalizedString()
        {
            Tuple<Coordinate, Coordinate> normalizedKey = this.NormalizedKey;

            return "[" + normalizedKey.Item1.ToString() + " - " + normalizedKey.Item2.ToString() + "]";

        }


        public bool IsInList(List<DirectedLine> list)
        {
            foreach (DirectedLine line in list)
            {
                if (line.Equals(this)) { return true; }
            }

            return false;
        }

        public string ToString(string delimeter)
        {
            return Coord1.ToString() + delimeter + Coord2.ToString();
        }
    }
}
