//-------------------------------------------------------------------------------//
// <copyright file="BorderGenerationState.cs"                                    //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace CanvasManager.Borders
{
    /// <summary>
    /// Enumerator for the fixed width (border) state.
    /// </summary>
    public enum BorderGenerationState
    {
        Unknown = 0,            // Unknown state (should never occur)
        None = 1,               // No state, border generation is not active
        Initial = 2,            // In initial state
        FrstPointSelected = 3,  // First point is on the canvas
        OngoingBorderBuild = 4   // Second point is on the canvas
    }
}
