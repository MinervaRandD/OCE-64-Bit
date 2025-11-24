//-------------------------------------------------------------------------------//
// <copyright file="DebugState.cs" company="Bruun Estimating, LLC">              // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Test_and_Debug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum DebugCond
    {
        None = 0,
        VisioMouseEvents = 1,
        VisioPolyLineDraw = 2,
        SelectionChanged = 3
    }
}
