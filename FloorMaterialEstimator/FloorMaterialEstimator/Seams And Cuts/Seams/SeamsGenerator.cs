//-------------------------------------------------------------------------------//
// <copyright file="SeamLine.cs" company="Bruun Estimating, LLC">                // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
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

    public partial class SeamsAndCutsGenerator
    {
        public List<Seam> SeamList;

        public List<Seam> GenerateSeamList()
        {
            if (!this.shapedTransformed)
            {
                GenerateTransformedElements();
            }

            HorizontalSeamsGenerator horizontalSeamsGenerator = new HorizontalSeamsGenerator(transformedPerimeter);

            this.transformedSeamList = horizontalSeamsGenerator.GenerateHorizontalSeamList(rollWidthInInches, 0.0);

            SeamList = InverseTransform(this.transformedSeamList);

            return SeamList;
        }

        private List<Seam> InverseTransform(List<Seam> seamList)
        {
            List<Seam> xformSeamList = new List<Seam>();

            seamList.ForEach(
                s => xformSeamList.Add(s.Clone()));

            xformSeamList.ForEach(s => s.Rotate(inverseRotationMatrix));

            xformSeamList.ForEach(s => s.Translate(inverseTranslationCoord));

            return xformSeamList;
        }
    }

}
