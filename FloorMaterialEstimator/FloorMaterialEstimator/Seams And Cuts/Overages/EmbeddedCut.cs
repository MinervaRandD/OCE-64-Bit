//-------------------------------------------------------------------------------//
// <copyright file="EmbeddedCut.cs" company="Bruun Estimating, LLC">             // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Seams_And_Cuts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;

    public class EmbeddedCut
    {
        // An embedded cut is always rectangular, hence only need two coordinates to describe.

        public Coordinate UpperLeftCorner;
        public Coordinate LowerRghtCorner;

        public EmbeddedCut(Coordinate upperLeftCorner, Coordinate lowerRghtCorner)
        {
            this.UpperLeftCorner = upperLeftCorner;
            this.LowerRghtCorner = lowerRghtCorner;
        }
    }
}
