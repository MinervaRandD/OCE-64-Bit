//-------------------------------------------------------------------------------//
// <copyright file="RectangleManager.cs" company="Bruun Estimating, LLC">        // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//


namespace CanvasShapes
{
    using System;
    using Globals;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class CanvasManager
    {

        #region Rectangle Draw

        private double rectangleStartX { get; set; }
        private double rectangleStartY { get; set; }

        /// <summary>
        /// Initializes the rectangle draw process
        /// </summary>
        /// <param name="x">The x location of the initiating mouse click</param>
        /// <param name="y">The y location of the initiating mouse click</param>
        private void InitializeRectangleDraw(double x, double y)
        {
            rectangleStartX = x;
            rectangleStartY = y;

            SystemState.DrawingShape = true;
        }

        private void CompleteRectangleDraw(double lastX, double lastY)
        {
            throw new NotImplementedException();

            //Visio.Shape visioRectangle = CurrentPage.DrawRectangle(rectangleStartX, rectangleStartY, lastX, lastY);

            //Rectangle rectangle = new Rectangle(this, visioRectangle);

            //rectangle.CreatePerimeter(this);

            //this.selectedFinishType.AddShape(rectangle);

            ////this.areaShapeDict.Add(rectangle.ID, rectangle);
            //this.ShapeDict.Add(visioRectangle.NameID, rectangle);

            //foreach (GraphicsLine line in rectangle.Perimeter.PerimeterLines)
            //{
            //    this.lineShapeDict.Add(line.ID, line);
            //    this.visioShapeDict.Add(line.NameID, line.VisioShape);
            //    this.SelectedLineType.AddLine(line);
            //}

            //buildingRectangle = null;

            //DrawingShape = false;
        }

        #endregion

    }
}
