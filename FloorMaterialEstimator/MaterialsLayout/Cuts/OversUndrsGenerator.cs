
namespace MaterialsLayout
{
    using Geometry;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OversUndrsGenerator
    {
        DirectedPolygon rolloutPolygon;
        DirectedPolygon cutPolygon;
        double tolerance;

        public OversUndrsGenerator(
            DirectedPolygon rolloutPolygon,
            DirectedPolygon cutPolygon,
            double tolerance)
        {
            this.rolloutPolygon = rolloutPolygon;
            this.cutPolygon = cutPolygon;
            
            this.tolerance = tolerance;
        }

        public void GenerateOversandUndrs(List<GraphicsOverage> overageList, List<GraphicsUndrage> undrageList)
        {
            
        }
    }
}
