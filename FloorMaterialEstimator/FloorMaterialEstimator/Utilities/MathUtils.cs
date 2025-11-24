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

namespace FloorMaterialEstimator.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    /// <summary>
    /// Mathematical methods, algos and functions
    /// </summary>
    public static class MathUtils
    {
        internal static double c2PI = 2.0 * Math.PI;

        public static double H0Dist(Coordinate c1, Coordinate c2)
        {
            return System.Math.Max(System.Math.Abs(c1.X - c2.X), System.Math.Abs(c1.Y - c2.Y));
        }

        public static double H0Dist(double x1, double y1, double x2, double y2)
        {
            return System.Math.Max(System.Math.Abs(x1 - x2), System.Math.Abs(y1 - y2));
        }

        internal static double H2Length(Coordinate c1, Coordinate c2)
        {
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2.0) + Math.Pow(c1.Y - c2.Y, 2.0));
        }

        internal static double NormalizedAtan(Coordinate coord1, Coordinate coord2)
        {
            double maxX = Math.Max(coord1.X, coord2.X);
            double minX = Math.Min(coord1.X, coord2.X);

            double maxY = Math.Max(coord1.Y, coord2.Y);
            double minY = Math.Min(coord1.Y, coord2.Y);

            double deltaY = maxY - minY;
            double deltaX = maxX - minX;

            return Math.Atan2(deltaY, deltaX);

        }

        internal static double Atan(Coordinate coord1, Coordinate coord2)
        {

            double deltaY = coord2.Y - coord1.Y;
            double deltaX = coord2.X - coord1.X;

            return Math.Atan2(deltaY, deltaX);
        }
    }
}
