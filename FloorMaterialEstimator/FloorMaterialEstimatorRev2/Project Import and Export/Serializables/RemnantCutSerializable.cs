using Geometry;
using Graphics;
using MaterialsLayout;
using MaterialsLayout.MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class RemnantCutSerializable
    {
        public string Guid { get; set; }

        public string CutGuid { get; set; }
        public CoordinateSerializable UpperRght { get; set; }

        public CoordinateSerializable LowerRght { get; set; }

        public CoordinateSerializable UpperLeft { get; set; }

        public CoordinateSerializable LowerLeft { get; set; }

        public RemnantCutSerializable() { }

        public RemnantCutSerializable(GraphicsRemnantCut graphicsRemnantCut)
        {
            Guid = graphicsRemnantCut.Guid;

            CutGuid = graphicsRemnantCut.GraphicsCut.Guid;

            UpperRght = new CoordinateSerializable(graphicsRemnantCut.UpperRght);

            LowerRght = new CoordinateSerializable(graphicsRemnantCut.LowerRght);

            UpperLeft = new CoordinateSerializable(graphicsRemnantCut.UpperLeft);

            LowerLeft = new CoordinateSerializable(graphicsRemnantCut.LowerLeft);
        }

        public GraphicsRemnantCut Deserialize(GraphicsWindow window, GraphicsPage page, GraphicsCut graphicsCut)
        {
            Coordinate upperRght = this.UpperRght.Deserialize();
            Coordinate lowerRght = this.LowerRght.Deserialize();
            Coordinate upperLeft = this.UpperLeft.Deserialize();
            Coordinate lowerLeft = this.LowerLeft.Deserialize();

            RemnantCut remnantCut = new RemnantCut(upperRght, lowerRght, upperLeft, lowerLeft);

            GraphicsRemnantCut graphicsRemnantCut = new GraphicsRemnantCut(window, page, remnantCut);

            return graphicsRemnantCut;
        }

    }
}
