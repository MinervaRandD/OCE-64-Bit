
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

namespace CanvasLib.Borders
{
    using Graphics;
    using Geometry;

    //using System.Drawing;

    using Utilities;
    using System;

    using Visio = Microsoft.Office.Interop.Visio;
    /// <summary>
    /// This class implements the shape that defines the marker for fixed width (border) generation.
    /// 
    /// The shape entails an out square and an inner circle.
    /// </summary>
    public class BorderGenerationMarker
    {
        GraphicsWindow window;

        GraphicsPage page;

        /// <summary>
        /// The two shape elements of the marker
        /// </summary>
        Graphics.GraphicShape[] shapeElements;

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

        GraphicShape borderGenerationMarkerShape;

        private string guid;

        /// <summary>
        /// Border generation marker class.
        /// </summary>
        /// <param name="x">The x coordinate of the border marker</param>
        /// <param name="y">The y coordinate of the border marker</param>
        /// <param name="width">The width of the border marker</param>
        public BorderGenerationMarker(GraphicsWindow window, GraphicsPage page, double x, double y, double width)
        {
            this.window = window;

            this.page = page;

            this.x = x;
            this.y = y;

            this.width = width;

            this.guid = GuidMaintenance.CreateGuid(this);
        }

        internal void Draw()
        {
            shapeElements = new GraphicShape[2];

            shapeElements[0] = DrawOuterSquare(page);

            shapeElements[1] = DrawInnerCircle(page);

            borderGenerationMarkerShape = VisioInterop.GroupShapes(window, shapeElements);

            borderGenerationMarkerShape.Guid = guid;

            borderGenerationMarkerShape.VisioShape.Data3 = guid;
        
            window.VisioWindow.DeselectAll();
        }

        /// <summary>
        /// Draw the outer square of the marker
        /// </summary>
        /// <param name="graphicsPage">The graphics Page on which to put the marker</param>
        /// <returns>Returns the outer square shape</returns>
        private GraphicShape DrawOuterSquare(GraphicsPage graphicsPage)
        {
            double x1 = x - 0.5 * width;
            double x2 = x + 0.5 * width;

            double y1 = y - 0.5 * width;
            double y2 = y + 0.5 * width;

            GraphicShape rectangleShape = graphicsPage.DrawRectangle(this, x1, y1, x2, y2, guid);

            return rectangleShape;
        }

        /// <summary>
        /// Draw the inner circle of the marker
        /// </summary>
        /// <param name="graphicsPage"></param>
        /// <returns>Returns the inner circle shape</returns>
        private GraphicShape DrawInnerCircle(GraphicsPage graphicsPage)
        {
            double x1 = x - 0.25 * width;
            double x2 = x + 0.25 * width;

            double y1 = y - 0.25 * width;
            double y2 = y + 0.25 * width;

            GraphicShape circleShape = graphicsPage.DrawCircle(this, new Coordinate(x, y), width / 4.0, System.Drawing.Color.Black);

            return circleShape;
        }

        /// <summary>
        /// Delete the border marker from the canvas.
        /// </summary>
        internal void Delete()
        {
            if (borderGenerationMarkerShape is null)
            {
                return;
            }

            VisioInterop.UngroupShape(borderGenerationMarkerShape);

            shapeElements[0].Delete();
            shapeElements[1].Delete();

            shapeElements[0] = null;
            shapeElements[1] = null;

            //borderGenerationMarkerShape.Delete();

            borderGenerationMarkerShape = null;

            shapeElements = null;
        }

        internal Coordinate Coordinate() => new Coordinate(x, y);
        
    }
}
