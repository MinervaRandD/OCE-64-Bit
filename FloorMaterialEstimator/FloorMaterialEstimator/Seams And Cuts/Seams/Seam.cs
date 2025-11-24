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

namespace FloorMaterialEstimator.Seams_And_Cuts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;

    public class Seam
    {
        public Coordinate Coord1;
        public Coordinate Coord2;

        public int Line1Index;
        public int Line2Index;

        public int SeamIndex;

        public static int SeamIndexCounter = -1;

        public Seam(Coordinate coord1, Coordinate coord2, int line1Index, int line2Index)
        {
            this.Coord1 = coord1;
            this.Coord2 = coord2;
            this.Line1Index = line1Index;
            this.Line2Index = line2Index;

            SeamIndex = SeamIndexCounter--;
        }

        public void Translate(Coordinate translationCoordinate)
        {
            Coord1.Translate(translationCoordinate);
            Coord2.Translate(translationCoordinate);
        }

        public void Rotate(double[,] rotationMatrix)
        {
            Coord1.Rotate(rotationMatrix);
            Coord2.Rotate(rotationMatrix);
        }

        internal Seam Clone()
        {
            return new Seam(this.Coord1, this.Coord2, this.Line1Index, this.Line2Index);
        }

        internal void Delete()
        {
            
        }
    }
}
