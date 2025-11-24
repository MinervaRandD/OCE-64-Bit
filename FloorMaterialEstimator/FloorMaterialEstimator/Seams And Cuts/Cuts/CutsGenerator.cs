//-------------------------------------------------------------------------------//
// <copyright file="CutsGenerator.cs" company="Bruun Estimating, LLC">           // 
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
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

    public partial class SeamsAndCutsGenerator
    {
        public List<Cut> CutList;

        public List<Cut> GenerateCutList()
        {
            if (transformedSeamList == null)
            {
                GenerateSeamList();
            }

            HorizontalCutGenerator horizontalCutGenerator = new HorizontalCutGenerator(this.transformedSeamList, this.transformedPerimeter);

            List<Cut> horizontalCutList = horizontalCutGenerator.GenerateHorizontalCuts(rollWidthInInches);

            CutList = new List<Cut>();

            foreach (Cut horizontalCut in horizontalCutList)
            {
                Cut cut = horizontalCut.Clone();

                cut.Rotate(this.inverseRotationMatrix);
                cut.Translate(this.inverseTranslationCoord);

                CutList.Add(cut);
            }

            return CutList;
        }
    }
}
