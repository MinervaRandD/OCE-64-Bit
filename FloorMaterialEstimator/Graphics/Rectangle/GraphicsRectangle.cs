#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsRectangle.cs. Project: Graphics. Created: 6/10/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using Geometry;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class GraphicsRectangle: Rectangle
    {
        public GraphicsWindow Window { get; set; } = null;

        public GraphicsPage Page { get; set; } = null;

        public GraphicShape Shape { get; set; } = null;

        public GraphicsRectangle(GraphicsWindow window, GraphicsPage page, Coordinate upperLeft, Coordinate lowerRght): base(upperLeft, lowerRght)
        {
            Window = window;

            Page = page;
        }

        public GraphicsRectangle(GraphicsWindow window, GraphicsPage page, Rectangle rectangle)
        {
            Window = window;

            Page = page;

            base.UpperLeft = rectangle.UpperLeft;
            base.LowerRght = rectangle.LowerRght;
        }
    }
}
