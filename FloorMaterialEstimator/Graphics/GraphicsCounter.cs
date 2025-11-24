//-------------------------------------------------------------------------------//
// <copyright file="Page.cs" company="Bruun Estimating, LLC">                    // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

namespace Graphics
{
    using System.Drawing;
    using Geometry;

    public class GraphicsCounter : GraphicsCircle
    {
        public char CounterTag;
        public Color CounterColor;
        public int CounterIndex => (int) (CounterTag - 'A');

        public GraphicsCounter(GraphicsPage page, Coordinate center, double radius, Color counterColor, char counterTag) : base(page, center, radius)
        {
            CounterTag = counterTag;
            CounterColor = counterColor;
        }

        public GraphicsCounter(GraphicsPage page, Coordinate center, double radius, Color counterColor, char counterTag, string guid) : base(page, center, radius, guid)
        {
            CounterTag = counterTag;
            CounterColor = counterColor;
        }

        public void Draw(string fontSize = null)
        {
            Shape = GraphicsPage.DrawCircle(Guid, Center, Radius, CounterColor);

            base.Guid = Shape.Guid;

            VisioInterop.SetShapeText(Shape, CounterTag.ToString(), 12, CounterColor);
            VisioInterop.SetShapeFill(Shape, "0");
            VisioInterop.SetLineWidth(Shape, 1.5);
        }

        public void UpdateColor(Color color)
        {
            CounterColor = color;

            VisioInterop.SetShapeText(Shape, CounterTag.ToString(), 12, CounterColor);
            VisioInterop.SetBaseLineColor(Shape, CounterColor);
        }

        public void Delete()
        {
            VisioInterop.DeleteShape(this.Shape);
        }
    }
}