namespace FinishesLib
{
    using Graphics;

    public class LineFinishLayers
    {
  
        private GraphicsWindow window;

        private GraphicsPage page;

        private string lineFinishName;

        public LineFinishLayers(GraphicsWindow window, GraphicsPage page, string lineFinishName)
        {
            this.window = window;

            this.page = page;

            this.lineFinishName = lineFinishName;

            AreaDesignStateLayer = new GraphicsLayerBase(this.window, page, "[LineMode:AreaDesignStateLayer]" + lineFinishName, GraphicsLayerType.LineMode_AreaDesignStateLayer, GraphicsLayerStyle.Dynamic, true);

            LineDesignStateLayer = new GraphicsLayerBase(this.window, page, "[LineMode:LineDesignStateLayer]" + lineFinishName, GraphicsLayerType.LineMode_LineDesignStateLayer, GraphicsLayerStyle.Dynamic, true);

            SeamDesignStateLayer = new GraphicsLayerBase(this.window, page, "[LineMode:SeamDesignStateLayer]" + lineFinishName, GraphicsLayerType.LineMode_SeamDesignStateLayer,GraphicsLayerStyle.Dynamic, true);

            //SeamDesignStateLayer.Lock();

            RemnantSeamDesignStateLayer = new GraphicsLayerBase(this.window, page, "[LineMode:RemnantSeamDesignStateLayer]" + lineFinishName, GraphicsLayerType.LineMode_RemnantSeamDesignStateLayer, GraphicsLayerStyle.Dynamic, true);

            LineDesignStateAssociatedLineLayer = new GraphicsLayerBase(this.window, page, "[LineMode:LineDesignStateAssociatedLineLayer]" + lineFinishName, GraphicsLayerType.LineMode_LineDesignStateLayer, GraphicsLayerStyle.Dynamic, true);
        }

        public GraphicsLayerBase AreaDesignStateLayer { get; private set; }

        public GraphicsLayerBase LineDesignStateLayer { get; private set; }

        public GraphicsLayerBase SeamDesignStateLayer { get; private set; }

        public GraphicsLayerBase RemnantSeamDesignStateLayer { get; private set; }

        public GraphicsLayerBase LineDesignStateAssociatedLineLayer { get; private set; }
    }

}
