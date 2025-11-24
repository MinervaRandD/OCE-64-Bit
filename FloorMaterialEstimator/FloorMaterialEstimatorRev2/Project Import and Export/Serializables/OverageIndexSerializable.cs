

namespace FloorMaterialEstimator
{
    using Graphics;
    using MaterialsLayout;

    public class OverageIndexSerializable
    {
        public string Guid { get; set; }

        public uint OverageIndex { get; set; }

        public double Radius { get; set; } = 0.2;

        public CoordinateSerializable Location { get; set; }

        public OverageIndexSerializable() { }

        public OverageIndexSerializable(GraphicsOverageIndex graphicsOverageIndex)
        {
            OverageIndex = graphicsOverageIndex.OverageIndex;

            Location = new CoordinateSerializable(graphicsOverageIndex.Location);

            Radius = graphicsOverageIndex.Radius;

            Guid = graphicsOverageIndex.Guid;
        }

        internal GraphicsOverageIndex Deserialize(GraphicsOverage graphicsOverage, GraphicsWindow window, GraphicsPage page, GraphicsLayerBase overIndexLayer)
        {
            GraphicsOverageIndex graphicsOverageIndex = new GraphicsOverageIndex(
                graphicsOverage
                , window
                , page
                , overIndexLayer
                , Guid
                , Location.Deserialize()
                , Radius);

            graphicsOverageIndex.OverageIndex = OverageIndex;

            return graphicsOverageIndex;
        }
    }
}
