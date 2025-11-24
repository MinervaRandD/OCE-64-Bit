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

namespace CanvasLib.Design_States_and_Modes
{
    /// <summary>
    /// Enumeration of various drawing modes.
    /// </summary>
    public enum DrawingMode
    {
        Default = 0,
        Line1X = 1,
        Line2X = 2,
        LineDuplicate = 3,
        Rectangle = 4,
        Polyline = 5,
        ScaleLine = 6,
        TapeMeasure = 7,
        BorderGeneration = 8,
        VertexEditing = 9
    }
}
