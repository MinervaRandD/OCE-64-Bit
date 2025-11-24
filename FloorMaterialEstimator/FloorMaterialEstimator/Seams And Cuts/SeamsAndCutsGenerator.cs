//-------------------------------------------------------------------------------//
// <copyright file="SeamsAndCutsGenerator.cs" company="Bruun Estimating, LLC">   // 
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
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Test_and_Debug;
    using FloorMaterialEstimator.Utilities;

    public partial class SeamsAndCutsGenerator
    {
        private int baseLineIndex;
        private PerimeterLine baseLine;
        private double rollWidthInInches;
        private Perimeter perimeter;

        private bool shapedTransformed = false;

        private List<Seam> transformedSeamList;
        private List<Line> transformedPerimeter;

        private double[,] rotationMatrix;
        private double[,] inverseRotationMatrix;

        private Coordinate translationCoord;
        private Coordinate inverseTranslationCoord;

        public SeamsAndCutsGenerator(Perimeter perimeter, int baseLineIndex, double rollWidthInInches)
        {
            this.perimeter = perimeter;
            this.baseLineIndex = baseLineIndex;
            this.baseLine = perimeter[baseLineIndex];
            this.rollWidthInInches = rollWidthInInches;

            perimeter.ValidateConsistentPerimeter();

            shapedTransformed = false;
        }

        private void GenerateTransformedElements()
        {
            Coordinate coord1 = baseLine.GetLineStartPoint();
            Coordinate coord2 = baseLine.GetLineEndpoint();

            Line line = new Line(coord1, coord2);

            this.translationCoord = -coord1;
            this.inverseTranslationCoord = coord1;

            double deltaX = coord2.X - coord1.X;
            double deltaY = coord2.Y - coord1.Y;

            double theta = Math.Atan2(deltaY, deltaX);

            rotationMatrix = new double[2, 2];

            rotationMatrix[0, 0] = Math.Cos(theta);
            rotationMatrix[0, 1] = Math.Sin(theta);
            rotationMatrix[1, 0] = -Math.Sin(theta);
            rotationMatrix[1, 1] = Math.Cos(theta);

            inverseRotationMatrix = new double[2, 2];

            inverseRotationMatrix[0, 0] = Math.Cos(-theta);
            inverseRotationMatrix[0, 1] = Math.Sin(-theta);
            inverseRotationMatrix[1, 0] = -Math.Sin(-theta);
            inverseRotationMatrix[1, 1] = Math.Cos(-theta);

            GenerateTransformedPerimeter();
        }

        private void GenerateTransformedPerimeter()
        {
            transformedPerimeter = new List<Line>();

            perimeter.PerimeterLines.ForEach(
                l => transformedPerimeter.Add(new Line(l.GetLineStartPoint(), l.GetLineEndpoint()))
                );

            transformedPerimeter.ForEach(l => l.Translate(this.translationCoord));
            transformedPerimeter.ForEach(l => l.Rotate(this.rotationMatrix));

            int count = transformedPerimeter.Count;

            Line line0 = transformedPerimeter[baseLineIndex];

            line0.Coord1.Y = 0.0;
            line0.Coord2.Y = 0.0;

            int prevIndex = (baseLineIndex + count - 1) % count;
            int nextIndex = (baseLineIndex + 1) % count;

            Line linePrev = transformedPerimeter[prevIndex];
            Line lineNext = transformedPerimeter[nextIndex];

            linePrev.Coord2.Y = 0.0;
            lineNext.Coord1.Y = 0.0;
        }
    }
}
