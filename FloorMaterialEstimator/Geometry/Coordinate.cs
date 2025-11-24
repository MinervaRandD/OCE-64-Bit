#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: Coordinate.cs. Project: Geometry. Created: 10/6/2024                                          */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion


namespace Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Utilities;
    using VectorLib;

    [Serializable]
    public struct Coordinate: IComparable<Coordinate>
    {
        public double X { get; set; }
        public double Y { get; set; }

        [XmlIgnore]
        public Tuple<double, double> Key => new Tuple<double, double>(X, Y);

        public static Coordinate NullCoordinate = new Coordinate(double.NaN, double.NaN);

        public Coordinate(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static bool operator <(Coordinate c1, Coordinate c2)
        {
            if (c1.X < c2.X)
            {
                return true;
            }

            if (c1.X == c2.X)
            {
                return c1.Y < c2.Y;
            }

            return false;
        }

        public static bool operator <=(Coordinate c1, Coordinate c2)
        {
            if (c1.X > c2.X)
            {
                return false;
            }

            if (c1.X < c2.X)
            {
                return true;
            }

            if (c1.X == c2.X)
            {
                return c1.Y <= c2.Y;
            }

            return false;
        }

        public static bool operator >=(Coordinate c1, Coordinate c2)
        {
            if (c1.X < c2.X)
            {
                return false;
            }

            if (c1.X > c2.X)
            {
                return true;
            }

            if (c1.X == c2.X)
            {
                return c1.Y >= c2.Y;
            }

            return false;
        }

        public static double H2Distance(Coordinate coord1, Coordinate coord2)
        {
            if (IsNullCoordinate(coord1) || IsNullCoordinate(coord2))
            {
                return double.NaN;
            }

            return MathUtils.H2Distance(coord1.X, coord1.Y, coord2.X, coord2.Y);
        }

        public static bool operator >(Coordinate c1, Coordinate c2)
        {
            if (c1.X > c2.X)
            {
                return true;
            }

            if (c1.X == c2.X)
            {
                return c1.Y > c2.Y;
            }

            return false;
        }

        public static Coordinate operator -(Coordinate c)
        {
            return new Coordinate(-c.X, -c.Y);
        }

        public static Coordinate operator -(Coordinate c1, Coordinate c2)
        {
            return new Coordinate(c1.X - c2.X, c1.Y - c2.Y);
        }

        public static Coordinate operator +(Coordinate c1, Coordinate c2)
        {
            return new Coordinate(c1.X + c2.X, c1.Y + c2.Y);
        }

        public static bool operator ==(Coordinate c1, Coordinate c2)
        {
            return c1.X == c2.X && c1.Y == c2.Y;
        }

        public static bool operator !=(Coordinate c1, Coordinate c2)
        {
            return c1.X != c2.X || c1.Y != c2.Y;
        }

        public void Translate(Coordinate xlateCoord)
        {
            X += xlateCoord.X;
            Y += xlateCoord.Y;
        }

        public void Rotate(double[,] rotationMatrix)
        {
            double x = X;
            double y = Y;

            X = rotationMatrix[0, 0] * x + rotationMatrix[0, 1] * y;
            Y = rotationMatrix[1, 0] * x + rotationMatrix[1, 1] * y;

            if (Math.Abs(X) < 1.0e-14)
            {
                X = 0.0;
            }

            if (Math.Abs(Y) < 1.0e-14)
            {
                Y = 0.0;
            }
        }

        public void Rotate(double theta)
        {
            double x = X;
            double y = Y;

            double cosTheta = Math.Cos(theta);
            double sinTheta = Math.Sin(theta);

            X = x * cosTheta - y * sinTheta;
            Y = x * sinTheta + y * cosTheta;

            if (Math.Abs(X) < 1.0e-14)
            {
                X = 0.0;
            }

            if (Math.Abs(Y) < 1.0e-14)
            {
                Y = 0.0;
            }
        }

        public void Transform(Coordinate translateCoord, double theta)
        {
            Translate(translateCoord);
            Rotate(theta);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static bool Equals(Coordinate coord1, Coordinate coord2, double precision)
        {
            if (Math.Abs(coord1.X - coord2.X) > precision)
            {
                return false;
            }

            if (Math.Abs(coord1.Y - coord2.Y) > precision)
            {
                return false;
            }

            return true;
        }

        public void NumericalCondition(double v)
        {
            if (Math.Abs(X) < v)
            {
                X = 0.0;
            }

            if (Math.Abs(Y) < v)
            {
                Y = 0.0;
            }
        }

        public override int GetHashCode()
       {
            return base.GetHashCode();
        }

        public int CompareTo(Coordinate c2)
        {
            if (X < c2.X)
            {
                return -1;
            }

            if (X > c2.X)
            {
                return 1;
            }

            if (Y < c2.Y)
            {
                return -1;
            }

            if (Y > c2.Y)
            {
                return 1;
            }

            return 0;
        }

        public static int Compare(Coordinate c1, Coordinate c2)
        {
            return c1.CompareTo(c2);
        }

        public Coordinate Clone()
        {
            return new Coordinate(this.X, this.Y);
        }

        public override string ToString()
        {
            return "(" + X.ToString("0.0000") + ", " + Y.ToString("0.0000") + ")";
        }

        internal static int IndexOf(List<Coordinate> coordList, Coordinate coord, double precision)
        {
            for (int i = 0; i < coordList.Count; i++)
            {
                if (Coordinate.Equals(coordList[i], coord, precision))
                {
                    return i;
                }
            }

            return -1;
        }

        public static bool IsNullCoordinate(Coordinate coord)
        {
            return double.IsNaN(coord.X) && double.IsNaN(coord.Y);
        }

        public static explicit operator Vector(Coordinate coord)
        {
            return new Vector(coord.X, coord.Y);
        }

        public static Coordinate operator *(double s, Coordinate coord)
        {
            return new Coordinate(s * coord.X, s * coord.Y);
        }

        public static bool IsNullCoordinate(object borderScndCoordinate)
        {
            throw new NotImplementedException();
        }

        internal double DistanceTo(Coordinate coord1)
        {
            return MathUtils.H2Distance(X, Y, coord1.X, coord1.Y);
        }

        public string ToString(string delimeter)
        {
            return X.ToString() + delimeter + Y.ToString();
        }
    }
}
