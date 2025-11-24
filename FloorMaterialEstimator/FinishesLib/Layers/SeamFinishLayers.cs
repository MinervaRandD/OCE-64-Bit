namespace FinishesLib
{
    using Graphics;

    public class SeamFinishLayers
    {

        private GraphicsWindow window;

        private GraphicsPage page;

        private string seamFinishName;

        public SeamFinishLayers(GraphicsWindow window, GraphicsPage page, string seamFinishName)
        {
            this.window = window;

            this.page = page;

            this.seamFinishName = seamFinishName;

            AreaDesignStateLayer = new GraphicsLayerBase(this.window, page, "[SeamMode:AreaDesignStateLayer]" + seamFinishName, GraphicsLayerType.SeamMode_AreaDesignStateLayer, GraphicsLayerStyle.Dynamic);

            LineDesignStateLayer = new GraphicsLayerBase(this.window, page, "[SeamMode:LineDesignStateLayer]" + seamFinishName, GraphicsLayerType.SeamMode_LineDesignStateLayer, GraphicsLayerStyle.Dynamic);

            SeamDesignStateLayer = new GraphicsLayerBase(this.window, page, "[SeamMode:SeamDesignStateLayer]" + seamFinishName, GraphicsLayerType.SeamMode_SeamDesignStateLayer, GraphicsLayerStyle.Dynamic);
        }

        public GraphicsLayerBase AreaDesignStateLayer { get; private set; }

        public GraphicsLayerBase LineDesignStateLayer { get; private set; }

        public GraphicsLayerBase SeamDesignStateLayer { get; private set; }
    }
}