using FinishesLib;
using Graphics;
using MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class CutIndexSerializable
    {
        public string Guid { get; set; }

        public uint CutIndex { get; set; }
        public CoordinateSerializable Location { get; set; }
        public CutIndexSerializable() { }

        public CutIndexSerializable(GraphicsCutIndex graphicsCutIndex)
        {
            CutIndex = graphicsCutIndex.CutIndex;

            Location = new CoordinateSerializable(graphicsCutIndex.Location);

            Guid = graphicsCutIndex.Guid;
        }

        internal GraphicsCutIndex Deserialize(GraphicsWindow window, GraphicsPage page, GraphicsLayerBase graphicsCutIndexLayer)
        {
            GraphicsCutIndex graphicsCutIndex = new GraphicsCutIndex(window, page, graphicsCutIndexLayer);

            graphicsCutIndex.Location = Location.Deserialize();

            graphicsCutIndex.CutIndex = CutIndex;

            return graphicsCutIndex;
        }
    }
}
