using System;

using Geometry;
using Graphics;

namespace CanvasManager.Borders
{

    using Geometry;
    using Graphics;

    public class BorderGuideShape
    {
        private GraphicsWindow window;

        private GraphicsPage page;

        private Coordinate coord1;

        private Coordinate coord2;

        private double widthInInches;

        public BorderGuideShape(
            GraphicsWindow window
            , GraphicsPage page
            , Coordinate coord1
            , Coordinate coord2
            , double widthInInches)
        {
            this.window = window;

            this.page = page;

            this.coord1 = coord1;

            this.coord2 = coord2;

            this.widthInInches = widthInInches;
        }
    }
}
