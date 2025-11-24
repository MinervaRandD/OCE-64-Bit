//-------------------------------------------------------------------------------//
// <copyright file="Rectangle.cs" company="Bruun Estimating, LLC">               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Models
{
    using FloorMaterialEstimator.CanvasManager;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FloorMaterialEstimator.Finish_Controls;

    using Visio = Microsoft.Office.Interop.Visio;

    public class Rectangle : AreaShape
    {
        public Rectangle(CanvasManager canvasManager, Visio.Shape visioShape) : base(canvasManager, ShapeType.Rectangle, new Shape(visioShape, ShapeType.Rectangle))
        {
            visioShape.Data2 = "Rectangle";

            Perimeter = new Perimeter(this);

            InternalAreaShape = new Shape(visioShape, ShapeType.Rectangle);
        }

        internal void CreatePerimeter(CanvasManager canvasManager)
        {
            throw new NotImplementedException();

            //Visio.Path path = InternalAreaShape.VisioShape.Paths[1];

            //Array xyArray;

            //path.Points(0.001, out xyArray);

            //double[] points = (double[])xyArray;

            //for (int i = 0; i < xyArray.Length - 2; i += 2)
            //{
            //    Visio.Shape visioShape = canvasManager.CurrentPage.DrawLine(points[i], points[i + 1], points[i + 2], points[i + 3]);

            //    PerimeterLine line = new PerimeterLine(visioShape);

            //    Perimeter.AddLine(line);
            //}
        }

    }
}
