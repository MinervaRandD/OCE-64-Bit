
//-------------------------------------------------------------------------------//
// <copyright file="BorderGenerationMarker.cs"                                   //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace CanvasManager.Borders
{
    using Graphics;
    using Geometry;

    using System.Drawing;

    /// <summary>
    /// This class implements the shape that defines the marker for fixed width (border) generation.
    /// 
    /// The shape entails an out square and an inner circle.
    /// </summary>
    public class BorderGenerationMarker
    {
        /// <summary>
        /// The two shape elements of the marker
        /// </summary>
        Shape[] shapeElements;

        /// <summary>
        /// The x coordinate of the marker
        /// </summary>
        double x;

        /// <summary>
        /// The y coordinate of the marker
        /// </summary>
        double y;

        /// <summary>
        /// The total width of the marker
        /// </summary>
        double width;

        /// <summary>
        /// Border generation marker class.
        /// </summary>
        /// <param name="x">The x coordinate of the border marker</param>
        /// <param name="y">The y coordinate of the border marker</param>
        /// <param name="width">The width of the border marker</param>
        public BorderGenerationMarker(double x, double y, double width)
        {
            this.x = x;
            this.y = y;

            this.width = width;
        }

        /// <summary>
        /// Draws the border marker at the specified location.
        /// </summary>
        /// <param name="graphicsPage">The graphics page on which to put the marker</param>
        internal void Draw(GraphicsPage graphicsPage)
        {
            shapeElements = new Shape[2];

            shapeElements[0] = DrawOuterSquare(graphicsPage);

            shapeElements[1] = DrawInnerCircle(graphicsPage);
        }

        /// <summary>
        /// Draw the outer square of the marker
        /// </summary>
        /// <param name="graphicsPage">The graphics page on which to put the marker</param>
        /// <returns>Returns the outer square shape</returns>
        private Shape DrawOuterSquare(GraphicsPage graphicsPage)
        {
            double x1 = x - 0.5 * width;
            double x2 = x + 0.5 * width;

            double y1 = y - 0.5 * width;
            double y2 = y + 0.5 * width;

            return graphicsPage.DrawRectangle(x1, y1, x2, y2);
        }

        /// <summary>
        /// Draw the inner circle of the marker
        /// </summary>
        /// <param name="graphicsPage"></param>
        /// <returns>Returns the inner circle shape</returns>
        private Shape DrawInnerCircle(GraphicsPage graphicsPage)
        {
            double x1 = x - 0.25 * width;
            double x2 = x + 0.25 * width;

            double y1 = y - 0.25 * width;
            double y2 = y + 0.25 * width;

            return graphicsPage.DrawCircle(new Coordinate(x, y), width / 4.0, Color.Black);
        }

        /// <summary>
        /// Delete the border marker from the canvas.
        /// </summary>
        internal void Delete()
        {
            if (shapeElements == null)
            {
                return;
            }

            for (int i = 0; i < shapeElements.Length; i++)
            {
                Shape shape = shapeElements[i];

                if (shape != null)
                {
                    VisioInterop.DeleteShape(shape);
                }
            }

            shapeElements = null;
        }
    }
}
