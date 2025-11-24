#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicSeamTag.cs. Project: Graphics. Created: 11/29/2024         */
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
using Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class GraphicSeamTag: GraphicCircleTag, IGraphicsShape
    {

        public GraphicSeamTag()
        {

        }

        public GraphicSeamTag(string guid)
        {
            Guid = guid;
        }

        public void Delete()
        {
            if (Shape is null)
            {
                return;
            }

            Page.RemoveFromPageShapeDict(Shape);

            Shape.Delete();

            Shape = null;
        }

        public static explicit operator GraphicSeamTag(GraphicShape shape)
        {
            if (shape == null)
            {
                return null;
            }

            var parentObject = shape.ParentObject;

            if (parentObject == null)
            {
                return null;
            }

            if (!(parentObject is GraphicSeamTag))
            {
                return null;
            }

            return (GraphicSeamTag)parentObject;
        }

        public static explicit operator GraphicShape(GraphicSeamTag graphicSeamTag)
        {
            return graphicSeamTag.Shape;
        }
    }
}
