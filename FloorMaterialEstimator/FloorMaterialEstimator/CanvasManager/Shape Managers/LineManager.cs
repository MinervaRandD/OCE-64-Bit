//-------------------------------------------------------------------------------//
// <copyright file="LineManager.cs" company="Bruun Estimating, LLC">             // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;

    public partial class CanvasManager
    {
        private void InitializeLineDraw(double x, double y)
        {
            this.buildingLine = new GraphicsLine(this.CurrentPage.DrawLine(x, y, x, y));

            this.ShapeDict.Add(this.buildingLine.VisioShape.NameID, buildingLine);

            this.DrawingShape = true;
        }

        /// <summary>
        /// Completes the line drawing process
        /// </summary>
        /// <param name="x">The x location of the initiating mouse click</param>
        /// <param name="y">The y location of the initiating mouse click</param>
        private void CompleteLineDraw(double x, double y)
        {
            this.lineShapeDict.Add(this.buildingLine.VisioShape.ID, this.buildingLine);

            this.SelectedLineType.AddLine(buildingLine);

            this.buildingLine = null;

            this.DrawingShape = false;
        }
    }
}
