
namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Graphics;

    public class CanvasTapeMeasureLine: CanvasDirectedLine
    {
        public CanvasTapeMeasureLine(GraphicsPage page, GraphicsDirectedLine line):base(page, line, DesignState.Area)
        {
           
        }
    }
}
