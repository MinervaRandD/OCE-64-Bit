//-------------------------------------------------------------------------------//
// <copyright file="Coordinate.cs" company="Bruun Estimating, LLC">              // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct Coordinate: IComparable<Coordinate>
    {
        public Coordinate(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public double X;
        public double Y;

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

        internal void Translate(Coordinate xlateCoord)
        {
            X += xlateCoord.X;
            Y += xlateCoord.Y;
        }

        internal void Rotate(double[,] rotationMatrix)
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        internal void NumericalCondition(double v)
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

        internal Coordinate Clone()
        {
            Coordinate newCoordinate = new Coordinate(this.X, this.Y);

            return newCoordinate;
        }

        public override string ToString()
        {
            return "(" + X.ToString("0.0000") + ", " + Y.ToString("0.0000") + ")";
        }
    }
}
