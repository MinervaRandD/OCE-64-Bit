//-------------------------------------------------------------------------------//
// <copyright file="TapeMeasureManager.cs" company="Bruun Estimating, LLC">      // 
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
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

    public partial class CanvasManager
    {
        private GraphicsLine tapeMeasure { get; set; }

        /// <summary>
        /// Initializes the drawing of the tape measure
        /// </summary>
        /// <param name="x">The x location of the initiating mouse click</param>
        /// <param name="y">The y location of the initiating mouse click</param>
        private void InitializeTapeMeasureDraw(double x, double y)
        {
            this.tapeMeasure = new GraphicsLine(CurrentPage.DrawLine(x, y, x, y));

            this.ShapeDict.Add(this.tapeMeasure.NameID, this.tapeMeasure);

            this.DrawingShape = true;
        }

        /// <summary>
        /// Completes the drawing of the tape measure line
        /// </summary>
        /// <param name="x">The x location of the initiating mouse click</param>
        /// <param name="y">The y location of the initiating mouse click</param>
        private void CompleteTapeMeasureDraw(double x, double y)
        {
            Debug.Assert(this.tapeMeasure != null, "Invalid null tape measure in CompleteTapeMeasureDraw");
           
            tapeMeasure.SetLineEndPoint(x, y);

            if (this.ShapeDict.ContainsKey(this.tapeMeasure.NameID))
            {
                this.ShapeDict.Remove(this.tapeMeasure.NameID);
            }

            this.VsoWindow.DeselectAll();

            this.DrawingShape = false;

            double tapeMeasureLength = tapeMeasure.Length * CurrentPage.DrawingScale;

            int feet = (int)Math.Floor(tapeMeasureLength / 12.0);

            double inch = tapeMeasureLength - (12.0 * (double)feet);

            string StrLineText = ' ' + feet.ToString() + "' " + inch.ToString("0.0") + "\" ";

            Graphics.SetLineText(tapeMeasure, StrLineText);

            baseForm.DrawingMode = DrawingMode.Default;
        }

        private void RemoveTapeMeasure()
        {
            if (this.ShapeDict.ContainsKey(this.tapeMeasure.NameID))
            {
                this.ShapeDict.Remove(this.tapeMeasure.NameID);
            }

            tapeMeasure.VisioShape.Delete();

            tapeMeasure = null;
        }
    }
}
