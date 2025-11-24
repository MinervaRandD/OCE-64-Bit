//-------------------------------------------------------------------------------//
// <copyright file="Cut.cs" company="Bruun Estimating, LLC">                     // 
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

    using Visio = Microsoft.Office.Interop.Visio;

    public class Cut
    {
        public int CutIndex = 0;

        private static int CutIndexGenerator = 1;

        public CutPolygon CutPolygon = null;

        public double UpperOffset;
        public double LowerOffset;

        public Cut() { CutIndex = CutIndexGenerator++;  }
        
        public Cut(CutPolygon cutPolygon, double upperOffset, double lowerOffset)
        {
            this.CutPolygon = cutPolygon;

            this.UpperOffset = upperOffset;
            this.LowerOffset = lowerOffset;
        }

        public void Translate(Coordinate translateCoordinate)
        {
            CutPolygon.Translate(translateCoordinate);
        }

        public void Rotate(double[,] rotationMatrix)
        {
            CutPolygon.Rotate(rotationMatrix);
        }


        internal void DrawBoundary(Page currentPage)
        {
            CutPolygon.DrawBoundary(currentPage);
        }

        internal void DeleteBoundary()
        {
            CutPolygon.DeleteBoundary();
        }

        internal void GenerateBoundaries(double y1, double y2)
        {
            CutPolygon.GenerateBoundaries(y1, y2);
        }

        internal Cut Clone()
        {
            CutPolygon clonedCutPolygon = this.CutPolygon.Clone() ;

            Cut clonedCut = new Cut(clonedCutPolygon, UpperOffset, LowerOffset);

            return clonedCut;
        }
    }
}
