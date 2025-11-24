
namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Graphics;
    using Geometry;

    public class InternalDirectedPolygonSerializable: List<BaseLineSerializable>
    {
       
        public InternalDirectedPolygonSerializable() { }

        public InternalDirectedPolygonSerializable(GraphicsDirectedPolygon polygon)
        {
            polygon.ForEach(l => Add(new BaseLineSerializable(l.Coord1, l.Coord2)));
        }

        public DirectedPolygon Deserialize()
        {
            List<DirectedLine> lineList = new List<DirectedLine>();

            this.ForEach(l => lineList.Add(new DirectedLine(l.Coord1, l.Coord2)));

            return new DirectedPolygon(lineList);
        }


    }
}
