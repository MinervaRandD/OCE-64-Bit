//-------------------------------------------------------------------------------//
// <copyright file="OverageLineSegment.cs" company="Bruun Estimating, LLC">      // 
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
    using FloorMaterialEstimator.Utilities;

    public class OverageLineSegment: Line
    {
        public OverageLineSegment(Coordinate coord1, Coordinate coord2): base(coord1, coord2)
        {
            if (coord1 > coord2)
            {
                Utilities.Swap(ref coord1, ref coord2);
            }
        }

        public double Length { get; internal set; }
    }
}
