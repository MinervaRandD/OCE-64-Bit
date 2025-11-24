
namespace CanvasLib.Finishes
{
    using Graphics;
    using FinishesLib;
    using CanvasLib.Design_States_and_Modes;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AreaFinishElement
    {
        public string Guid;

        public AreaFinishBase AreaFinishBase { get; set; }

        public GraphicsLayer AreaDesignStateLayer { get; set; }

        public GraphicsLayer SeamDesignStateLayer { get; set; }
      
        private GraphicsPage graphicsPage { get; set; }

        public Dictionary<string, GraphicsLayoutArea> GraphicsLayoutAreaDict = new Dictionary<string, GraphicsLayoutArea>();

        public IEnumerable<GraphicsLayoutArea> GraphicsLayoutAreas => GraphicsLayoutAreaDict.Values;

        public AreaFinishElement(AreaFinishBase areaFinishBase, GraphicsPage graphicsPage)
        {
            this.AreaFinishBase = areaFinishBase;

            this.Guid = AreaFinishBase.Guid;

            this.graphicsPage = graphicsPage;

            AreaDesignStateLayer = new GraphicsLayer(Guid, graphicsPage, "AreaDesignStateLayer_" + Guid);

            SeamDesignStateLayer = new GraphicsLayer(Guid, graphicsPage, "SeamDesignStateLayer_" + Guid);
        }

        public void SetDesignStateFormat(DesignState designState, bool filtered, bool showAreas)
        {
            if (filtered || !showAreas)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);
                
                return;
            }

            if (designState == DesignState.Area)
            {
                AreaDesignStateLayer.SetLayerVisibility(true);
                SeamDesignStateLayer.SetLayerVisibility(false);

                //foreach (GraphicsLayoutArea layoutArea in this.GraphicsLayoutAreas)
                //{
                //    layoutArea.SetAreaDesignStateFormat(baseAreaFinishPallet.BaseForm.AreaMode);
                //}
            }

            else if (designState == DesignState.Line)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);

                //foreach (GraphicsLayoutArea layoutArea in this.GraphicsLayoutAreas)
                //{
                //    layoutArea.SetLineDesignStateFormat(baseAreaFinishPallet.BaseForm.LineMode);
                //}
            }

            else if (designState == DesignState.Seam)
            {
                AreaDesignStateLayer.SetLayerVisibility(true);
                SeamDesignStateLayer.SetLayerVisibility(true);

                //foreach (GraphicsLayoutArea layoutArea in this.GraphicsLayoutAreas)
                //{
                //    layoutArea.SetSeamDesignStateFormat(baseAreaFinishPallet.BaseForm.SeamMode);
                //}
            }
        }

        public void SetupSeamModeDesignState(string selectedFinishGuid)
        {

            if (Guid != selectedFinishGuid)
            {
                AreaDesignStateLayer.SetLayerVisibility(false);
                SeamDesignStateLayer.SetLayerVisibility(false);
            }

            else
            {
                AreaDesignStateLayer.SetLayerVisibility(true);
                SeamDesignStateLayer.SetLayerVisibility(true);
            }
        }

        public void Delete()
        {
            
            AreaDesignStateLayer.Delete();
            SeamDesignStateLayer.Delete();

            AreaDesignStateLayer = null;
            SeamDesignStateLayer = null;

            Guid = string.Empty;

        }

    }
}
