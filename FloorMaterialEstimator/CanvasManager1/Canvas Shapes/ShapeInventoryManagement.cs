using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.CanvasManager
{
    using Graphics;

    public partial class CanvasManager
    {

        //public Dictionary<string, IGraphicsShape> ShapeDict
        //{
        //    get
        //    {
        //        return CurrentPage.PageShapeDict;
        //    }
        //}
        
        public List<CanvasLayoutArea> AreaDesignStateSelectedAreas => CurrentPage.AreaDesignStateSelectedAreas();

        //public Dictionary<string, GraphicsTakeout> GraphicsTakeoutAreaDict
        //{
        //    get
        //    {
        //        return CurrentPage.GraphicsTakeoutAreaDict;
        //    }
        //}

        //public Dictionary<string, GraphicsCounter> GraphicsCounterDict
        //{
        //    get
        //    {
        //        return CurrentPage.GraphicsCounterDict;
        //    }
        //}
    }
}
