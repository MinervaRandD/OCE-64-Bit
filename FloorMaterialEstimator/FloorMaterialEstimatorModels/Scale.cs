//-------------------------------------------------------------------------------//
// <copyright file="Scale.cs" company="Bruun Estimating, LLC">                   // 
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

    //Kiet Nguyen:[Sheet 400] list scale
    [Serializable]
    public class Scale
    {
        public string Value { set; get; }

        public string DisplayText { set; get; }
        public string Feet { set; get; }
        public string Inches { set; get; }
    }
}
