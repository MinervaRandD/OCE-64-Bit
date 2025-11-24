#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: GraphicsCircle.cs. Project: Graphics. Created: 6/10/2024         */
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



namespace Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Utilities;

    public class GraphicsCircle : Circle, IGraphicsShape
    {
        public GraphicShape Shape { get; set; }

        public GraphicsWindow Window {get; set;}

        public GraphicsPage Page { get; set; }

        public string Guid { get; set; }

        public ShapeType ShapeType
        {
            get
            {
                return ShapeType.Circle;
            }
        }

        public GraphicsLayerBase GraphicsLayer { get; set; } = null;

        public GraphicsCircle(GraphicsWindow window, GraphicsPage page, Coordinate center, double radius): base(center, radius)
        {
            Window = window;

            Page = page;

            Guid = GuidMaintenance.CreateGuid(this);
        }


        public GraphicsCircle(GraphicsPage page, Coordinate center, double radius, string guid) : base(center, radius)
        {
            Page = page;

            Guid = guid;
        }

        public void Delete()
        {
            VisioInterop.DeleteShape(Shape);
        }

        //public void Draw(Color lineColor, double lineWidthInPts)
        //{
        //    Shape = Page.DrawCircle(Center, Radius, lineColor);

        //    Shape.SetLineWidth(lineWidthInPts);
        //}

    }
}
