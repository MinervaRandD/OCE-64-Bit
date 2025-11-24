//-------------------------------------------------------------------------------//
// <copyright file="Line.cs" company="Bruun Estimating, LLC">                    // 
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Utilities;

    public class Line
    {
        public Coordinate Coord1;
        
        public Coordinate Coord2;

        public Line(Coordinate coord1, Coordinate coord2)
        {
            this.Coord1 = coord1;
            this.Coord2 = coord2;

            if (this.Coord1 > this.Coord2)
            {
                Utilities.Swap(ref this.Coord1, ref this.Coord2);
            }
        }

        public Tuple<Coordinate, Coordinate> Key
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

        public int SlopeSign
        {
            get
            {
                if (Coord1.Y == Coord2.Y)
                {
                    return 0;
                }
                
                if (Coord2.Y > Coord1.Y)
                {
                    if (Coord1.X <= Coord2.X)
                    {
                        return 1;
                    }

                    else
                    {
                        return -1;
                    }
                }

                else
                {
                    if (Coord1.X <= Coord2.X)
                    {
                        return -1;
                    }

                    else
                    {
                        return 1;
                    }
                }
            }
        }

        public double Slope
        {
            get
            {
                double deltaX = this.Coord2.X - this.Coord1.X;
                double deltaY = this.Coord2.Y - this.Coord1.Y;

                if (Math.Abs(deltaX) < 1.0e-12)
                {
                    if (Math.Abs(deltaY) < 1.0e-12)
                    {
                        return 0.0;
                    }

                    else
                    {
                        if (this.Coord2.Y >= this.Coord1.Y)
                        {
                            return double.PositiveInfinity;
                        }

                        else
                        {
                            return double.NegativeInfinity;
                        }
                    }
                }

                else
                {
                    return deltaY / deltaX;
                }

                return double.NaN;
            }
        }

        internal bool IntersectsLevel(double y)
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

        public double XInterceptForY(double y)
        {
            double x1 = Coord1.X;
            double x2 = Coord2.X;

            double y1 = Coord1.Y;
            double y2 = Coord2.Y;

            if (y2 == y1)
            {
                throw new NotImplementedException();
            }

            return (y - y1) * (x2 - x1) / (y2 - y1) + x1;
        }

        public bool IsHorizontal()
        {
            return Coord1.Y == Coord2.Y;
        }

        public bool IsVertical()
        {
            return Coord1.X == Coord2.X;
        }


        public void Translate(Coordinate xlateCoord)
        {
            Coord1.Translate(xlateCoord);
            Coord2.Translate(xlateCoord);
        }

        public void Rotate(double[,] rotationMatrix)
        {
            Coord1.Rotate(rotationMatrix);
            Coord2.Rotate(rotationMatrix);
        }

        public bool FallsBetween(double y1, double y2)
        {
            return Coord1.Y > y2 && Coord1.Y < y1 && Coord2.Y > y2 && Coord2.Y < y1;
        }

        public bool HasPointsBelow(double y)
        {
            return Coord1.Y < y || Coord2.Y < y;
        }

        public bool HasPointsAbove(double y)
        {
            return Coord1.Y > y || Coord2.Y > y;
        }

        public double NormalizedAtan()
        {
            double deltaY = MaxY - MinY;
            double deltaX = MaxX - MinX;

            return Math.Atan2(deltaY, deltaX);
        }

        public Line Clone()
        {
            return new Line(this.Coord1.Clone(), this.Coord2.Clone());
        }

        public static bool operator ==(Line l1, Line l2)
        {
            if (l1.Coord1 == l2.Coord1)
            {
                return l1.Coord2 == l2.Coord2;
            }

            if (l1.Coord2 == l2.Coord1)
            {
                return l1.Coord1 == l2.Coord2;
            }

            return false;
        }

        public static bool operator !=(Line l1, Line l2)
        {
            if (l1.Coord1 == l2.Coord1)
            {
                return l1.Coord2 != l2.Coord2;
            }

            if (l1.Coord2 == l2.Coord1)
            {
                return l1.Coord1 != l2.Coord2;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            Line l1 = this;
            Line l2 = (Line)obj;

            if (l1.Coord1 == l2.Coord1)
            {
                return l1.Coord2 == l2.Coord2;
            }

            if (l1.Coord2 == l2.Coord1)
            {
                return l1.Coord1 == l2.Coord2;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
