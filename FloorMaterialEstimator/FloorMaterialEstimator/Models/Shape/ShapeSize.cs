//-------------------------------------------------------------------------------//
// <copyright file="Size.cs" company="Bruun Estimating, LLC">                    // 
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
    public struct ShapeSize
    {
        public double Width;
        public double Height;

        public ShapeSize(double Width, double Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }
}
