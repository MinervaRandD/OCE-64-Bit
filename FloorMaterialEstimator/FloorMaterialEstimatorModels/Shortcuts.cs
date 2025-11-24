//-------------------------------------------------------------------------------//
// <copyright file="Shortcuts.cs" company="Bruun Estimating, LLC">               // 
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

    //Kiet Nguyen:[1500] list shortcuts
    [Serializable]
    public class Shortcuts
    {
        public string Area { set; get; }
        public char Keystroke { set; get; }
        public string Action { set; get; }

        public string DisplayText { set; get; }
    }
}
