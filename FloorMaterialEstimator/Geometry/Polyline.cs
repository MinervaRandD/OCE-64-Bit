//-------------------------------------------------------------------------------//
// <copyright file="Polyline.cs" company="Bruun Estimating, LLC">                // 
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

    public class Polyline: List<Line>
    {
        public Polyline() { }
       
        public Polyline(List<Line> lineList)
        {
            base.AddRange(lineList);
        }

        public void Translate(Coordinate translateCoord)
        {
            base.ForEach(l => l.Translate(translateCoord));
        }

        public void Rotate(double[,] rotationMatrix)
        {
            base.ForEach(l => l.Rotate(rotationMatrix));
        }

        public void Transform(Coordinate translateCoord, double[,] rotationMatrix)
        {
            Translate(translateCoord);
            Rotate(rotationMatrix);
        }

        public void InverseTransform(double[,] inverseRotationMatrix, Coordinate inverseTranslateCoord)
        {
            Rotate(inverseRotationMatrix);
            Translate(inverseTranslateCoord);
        }

        public Polyline Clone()
        {
            List<Line> clonedLineList = new List<Line>();

            base.ForEach(l => clonedLineList.Add(l.Clone()));

            return new Polyline(clonedLineList);
        }

    }
}
