//-------------------------------------------------------------------------------//
// <copyright file="ScaleRulerManager.cs" company="Bruun Estimating, LLC">       // 
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
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Supporting_Forms;
    using FloorMaterialEstimator.Utilities;

    public partial class CanvasManager
    {

        /// <summary>
        /// Initializes the drawing of the scale line
        /// </summary>
        /// <param name="x">The x location of the initiating mouse click</param>
        /// <param name="y">The y location of the initiating mouse click</param>
        private void InitializeScaleLineDraw(double x, double y)
        {
            this.scaleLine = new ScaleLine(CurrentPage.DrawLine(x, y, x, y));

            this.scaleLine.SetEndpointArrows(5);

            this.ShapeDict.Add(this.scaleLine.NameID, this.scaleLine);

            this.DrawingShape = true;
        }

        /// <summary>
        /// Completes the drawing of the scale line
        /// </summary>
        /// <param name="x">The x location of the initiating mouse click</param>
        /// <param name="y">The y location of the initiating mouse click</param>
        private void CompleteScaleLineDraw(double x, double y)
        {
            if (scaleLine == null)
            {
                return;
            }

            scaleLine.SetLineEndPoint(x, y);

            if (this.ShapeDict.ContainsKey(this.scaleLine.NameID))
            {
                this.ShapeDict.Remove(this.scaleLine.NameID);
            }

            this.VsoWindow.DeselectAll();

            this.DrawingShape = false;

            ScaleForm scaleForm = new ScaleForm();

            DialogResult dialogResult = scaleForm.ShowDialog();

            if (dialogResult != DialogResult.OK)
            {
                scaleLine.VisioShape.Delete();

                return;
            }

            double requestedLength = scaleForm.TotalInches;

            if (requestedLength <= 0.0)
            {
                MessageBox.Show("The requested scale is invalided.");

                scaleLine.VisioShape.Delete();

                return;
            }

            double scaleLength = scaleLine.Length;

            if (scaleLength <= 0.0)
            {
                MessageBox.Show("The requested scale is invalided.");

                scaleLine.VisioShape.Delete();

                return;
            }

            double scale = requestedLength / scaleLength;

            CurrentPage.DrawingScale = scale;

            scaleLine.VisioShape.Delete();

            scaleLine = null;

            return;
        }
    }
}
