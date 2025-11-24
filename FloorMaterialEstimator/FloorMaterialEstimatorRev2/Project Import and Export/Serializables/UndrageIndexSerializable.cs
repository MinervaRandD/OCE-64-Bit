namespace FloorMaterialEstimator
{
    using Geometry;
    using Graphics;
    using MaterialsLayout;

    public class UndrageIndexSerializable
    {
        public string Guid { get; set; }

        public uint UndrageIndex { get; set; }

        public CoordinateSerializable Location { get; set; }

        public UndrageIndexSerializable() { }

        public UndrageIndexSerializable(GraphicsUndrageIndex graphicsUndrageIndex)
        {
            UndrageIndex = graphicsUndrageIndex.UndrageIndex;

            Location = new CoordinateSerializable(graphicsUndrageIndex.Location);

            Guid = graphicsUndrageIndex.Guid;
        }

        internal GraphicsUndrageIndex Deserialize(GraphicsUndrage parentUndrage, GraphicsWindow window, GraphicsPage page)
        {
            Coordinate center = Coordinate.NullCoordinate;

            if (parentUndrage.BoundingRectangle != null)
            {
                center = parentUndrage.BoundingRectangle.Center;
            }

            GraphicsLayerBase graphicsLayer = parentUndrage.FinishesLibElements.AreaFinishLayers.UndrsIndexLayer;

            GraphicsUndrageIndex graphicsUndrageIndex = new GraphicsUndrageIndex(parentUndrage, window, page, graphicsLayer, null, center, 0.2);

            
            graphicsUndrageIndex.UndrageIndex = UndrageIndex;

            graphicsUndrageIndex.Location = Location.Deserialize();

            return graphicsUndrageIndex;
        }
    }
}
