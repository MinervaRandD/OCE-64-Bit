#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicCircleTag.cs. Project: Graphics. Created: 11/29/2024         */
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

using Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class GraphicCircleTag : GraphicCircle, IGraphicsShape
    {
        public new object ParentObject { get; set; } = null;
        protected uint tagIndexInt { get; set; } = 0;

        #region Constructors and Cloners
        public GraphicCircleTag()
         
        {
         
        }

        public GraphicCircleTag(
            object parentObject
            , GraphicsWindow window
            , GraphicsPage page
            , GraphicsLayerBase graphicsLayer
            , string guid
            , Coordinate center
            , double radius
            , Color color):
            base(window, page, graphicsLayer, guid, center, radius, color)
        {
            this.ParentObject = parentObject;

            base.ParentObject = this;

            ShapeType |= ShapeType.CircleTag;

            // Not sure why this necessary, but it solves some problems...

            Shape.ParentObject = this;
        }

        #endregion

        public void DrawCircleTag()
        {
            Page.DrawCircle(Center, 0.2, LineColor, Shape);

            Shape.SetShapeData("[GraphicsCircleTag]", "Circle Tag", Guid);

            this.Window?.DeselectAll();
        }

        public static explicit operator GraphicCircleTag(GraphicShape graphicShape)
        {
            string data1 = graphicShape.Data1;

            if (graphicShape == null)
            {
                return null;
            }

            object parentObject = graphicShape.ParentObject;

            if (parentObject is null)
            {
                return null;
            }

            if (!(parentObject is GraphicCircleTag))
            {
                return null;
            }

            return (GraphicCircleTag)parentObject;
        }
    }
}

