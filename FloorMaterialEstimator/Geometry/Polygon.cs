//-------------------------------------------------------------------------------//
// <copyright file="Polygon.cs" company="Bruun Estimating, LLC">                 // 
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

    public class Polygon: List<Line>
    {
        public Polygon() { }

        public Polygon(List<Line> lineList)
        {
            base.AddRange(lineList);
        }
    }
}
