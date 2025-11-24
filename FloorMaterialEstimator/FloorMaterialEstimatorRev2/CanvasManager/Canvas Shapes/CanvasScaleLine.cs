
namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Graphics;

    public class CanvasScaleLine: CanvasDirectedLine
    {
        public CanvasScaleLine(GraphicsPage page, GraphicsDirectedLine line) : base(page, line, DesignState.Area) { }
    }
}
