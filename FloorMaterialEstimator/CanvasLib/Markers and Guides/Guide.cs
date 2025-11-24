//-------------------------------------------------------------------------------//
// <copyright file="Guide.cs"                                                    //
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

namespace CanvasLib.Markers_and_Guides
{
    using Graphics;
    using Utilities;

    /// <summary>
    /// Guide class implements the logic to place guides on the canvas.
    /// These are not field guides but rather guides associated with a specific point.
    /// The guides are a vertical and horizontal line set.
    /// </summary>
    public class Guide
    {
        GraphicShape[] shapeElements;

        public double X { get; set; }
        public double Y { get; set; }
        
        
        double pageWidth;
        double pageHeight;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">The x point to place the guides</param>
        /// <param name="y">The x point to place the guides</param>
        public Guide(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Places the guide(s) on the canvas
        /// </summary>
        /// <param name="page">The Page on which to place the guide</param>
        public void Draw(GraphicsPage page)
        {
            pageWidth = page.PageWidth;
            pageHeight = page.PageHeight;

            shapeElements = new GraphicShape[2];

            //shapeElements[0] = Page.DrawLine("Guide-L", 0, Y, X - 0.125, Y);
            //shapeElements[1] = Page.DrawLine("Guide-R", X + 0.125, Y, pageWidth, Y);
            //shapeElements[2] = Page.DrawLine("Guide-U", X, 0, X, Y - 0.125);
            //shapeElements[3] = Page.DrawLine("Guide-D", X, Y + 0.125, X, pageHeight);

            shapeElements[0] = page.DrawLine(this, "Guide-H", 0, Y, pageWidth, Y);
            shapeElements[1] = page.DrawLine(this, "Guide-V", X, 0, X, pageHeight);

            foreach (GraphicShape shape in shapeElements)
            {
                VisioInterop.FormatGuideLine(shape);
            }
        }

        /// <summary>
        /// Delete guides
        /// </summary>
        public void Delete()
        {
            if (shapeElements is null)
            {
                return;
            }

            for (int i = 0; i < shapeElements.Length; i++)
            {
                GraphicShape shape = shapeElements[i];

                if (Utilities.IsNotNull(shape))
                {
                    VisioInterop.DeleteShape(shape);
                }
            }

            shapeElements = null;
        }
    }
}
