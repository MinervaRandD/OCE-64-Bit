//-------------------------------------------------------------------------------//
// <copyright file="DrawingMode.cs" company="Bruun Estimating, LLC">             // 
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
    /// <summary>
    /// Enumeration of various drawing modes.
    /// </summary>
    public enum DrawingMode
    {
        Default = 0,
        ScaleLine = 1,
        TapeMeasure = 2,
        Line = 3,
        Rectangle = 4,
        Polyline = 5 
    }
}
