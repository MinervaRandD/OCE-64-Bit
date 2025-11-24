//-------------------------------------------------------------------------------//
// <copyright file="MathUtils.cs" company="Bruun Estimating, LLC">               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using System.Windows.Media;

namespace Utilities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Mathematical methods, algos and functions
    /// </summary>
    public static class MathUtils
    {
        public const double C_2PI = 2.0 * Math.PI;

        public const double C_180_over_pi = 57.2957795130823; // In degrees

        public const double C_Sqrt2 = 1.414213562373095;

        public const double C_PI_over_4_Radians = 0.785398163397448; // In radians

        public static double H0Dist(double x1, double y1, double x2, double y2)
        {
            return System.Math.Max(System.Math.Abs(x1 - x2), System.Math.Abs(y1 - y2));
        }

        public static double H2Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2.0) + Math.Pow(y1 - y2, 2.0));
        }

        public static double NormalizedAtan(double x1, double y1, double x2, double y2)
        {
            double maxX = Math.Max(x1, x2);
            double minX = Math.Min(x1, x2);

            double maxY = Math.Max(y1, y2);
            double minY = Math.Min(y1, y2);

            double deltaY = maxY - minY;
            double deltaX = maxX - minX;

            return Math.Atan2(deltaY, deltaX);
        }

        public static double Atan(double x1, double y1, double x2, double y2)
        {
            double deltaY = y2 - y1;
            double deltaX = x2 - x1;

            return Math.Atan2(deltaY, deltaX);
        }

        public static double InteriorAngle(double x1, double y1, double x2, double y2)
        {
            double len1 = Math.Sqrt(x1 * x1 + y1 * y1);
            double len2 = Math.Sqrt(x2 * x2 + y2 * y2);

            double innerProd = x1 * x2 + y1 * y2;

            double cosTheta = innerProd / (len1 * len2);

            return Math.Acos(cosTheta);
        }

        public static bool IsBetween(double x1, double y1, double x2, double y2, double x3, double y3, double precision)
        {

            double crossProd = (y3 - y1) * (x2 - x1) - (x3 - x1) * (y2 - y1);

            if (Math.Abs(crossProd) > precision)
            {
                return false;
            }

            double dotProd = (x3 - x1) * (x2 - x1) + (y3 - y1) * (y2 - y1);

            if (dotProd < 0 - precision)
            {
                return false;
            }

            if (dotProd > Math.Pow(x2 - x1, 2.0) + Math.Pow(y2-y1, 2.0))
            {
                return false;
            }

            return true;
        }

        public static PrimaryOrientation LinePrimaryOrientation(double x1, double y1, double x2, double y2)
        {
            double atan = Atan(x1, y1, x2, y2);

            if (atan <= C_PI_over_4_Radians || atan >= 7 * C_PI_over_4_Radians || (atan >= 3 * C_PI_over_4_Radians && atan <= 5 * C_PI_over_4_Radians))
            {
                return PrimaryOrientation.Horizontal;
            }

            else
            {
                return PrimaryOrientation.Vertical;
            }
        }

        public static bool FallsBetween(double x, double x1, double x2)
        {
            if (x1 <= x2)
            {
                return x1 <= x && x <= x2;
            }

            else
            {
                return x2 <= x && x <= x1;
            }
        }

        public static bool ProperFallsBetween(double x, double x1, double x2)
        {
            if (x1 <= x2)
            {
                return x1 < x && x < x2;
            }

            else
            {
                return x2 < x && x < x1;
            }
        }

        /// <summary>
        /// Determines the orientation clockwise or counterclockwise of a polygon.
        /// 
        /// Note: Must be a completed polygon for this algo to work.
        /// 
        /// </summary>
        /// <param name="coordinateList">The list of coordinates that define the polygon</param>
        /// <returns></returns>
        public static double CurveOrientation(List<Tuple<double, double>> coordinateList)
        {
            if (coordinateList.Count <= 1)
            {
                return 0;
            }

            double sum = 0;

            double x1 = coordinateList[0].Item1;
            double y1 = coordinateList[0].Item2;

            double x2 = 0;
            double y2 = 0;

            for (int i = 1; i < coordinateList.Count; i++)
            {
                x2 = coordinateList[i].Item1;
                y2 = coordinateList[i].Item2;

                sum += (x2 - x1) * (y2 + y1);

                x1 = x2;
                y1 = y2;
            }

            return sum;
        }

        public static double? AddToNullableDouble(double? currValu, double? addValu)
        {
            if (currValu == null)
            {
                if (addValu == null)
                {
                    return null;
                }

                return addValu.Value;
            }

            if (addValu == null)
            {
                return currValu.Value;
            }

            return currValu.Value + addValu.Value;
        }
    }

    public enum PrimaryOrientation
    {
        Unknown = 0,
        Horizontal = 1,
        Vertical = 2
    }
}
