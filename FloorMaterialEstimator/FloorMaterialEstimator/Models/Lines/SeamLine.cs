//-------------------------------------------------------------------------------//
// <copyright file="SeamLine.cs" company="Bruun Estimating, LLC">                // 
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

    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Seams_And_Cuts;
    
    public class SeamLine: GraphicsLine
    {
        public Seam Seam { get; set; }

        public SeamLine(Seam seam, UCLine ucLine, CanvasManager canvasManager) : base(seam.Coord1, seam.Coord2, ucLine, canvasManager)
        {
            this.LineType = LineType.SeamLine;

            this.Seam = seam;
        }
    }
}
