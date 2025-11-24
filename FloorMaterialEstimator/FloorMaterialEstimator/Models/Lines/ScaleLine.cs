//-------------------------------------------------------------------------------//
// <copyright file="ScaleLine.cs" company="Bruun Estimating, LLC">               // 
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

    using Visio = Microsoft.Office.Interop.Visio;

    public class ScaleLine: GraphicsLine
    {
        public ScaleLine(Visio.Shape visioShape): base(visioShape)
        {
            LineType = LineType.ScaleLine;
        }
    }
}
