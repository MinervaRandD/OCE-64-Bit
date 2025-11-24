

namespace FloorMaterialEstimator
{
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using Geometry;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using Utilities;
    using Graphics;
    using FinishesLib;

    public class RolloutSerializable
    {
        public string RolloutGuid { get; set; }

        public string LayoutAreaGuid { get; set; }

        public Rectangle RolloutRectangle { get; set; }

        public double SeamWidth { get; set; }

        public double MaterialWidth { get; set; }

        public double MaterialOverlap { get; set; }

        public List<CutSerializable> CutList { get; set; }

        public List<UndrageSerializable> UndrageList { get; set; }

        public double RolloutAngle { get; set; }

        public RolloutSerializable() { }

        public RolloutSerializable(GraphicsRollout graphicsRollout)
        {
            Debug.Assert(Utilities.IsNotNull(graphicsRollout));
            Debug.Assert(Utilities.IsNotNull(graphicsRollout.ParentGraphicsLayoutArea));

            this.RolloutGuid = graphicsRollout.Guid;

            this.LayoutAreaGuid = graphicsRollout.ParentGraphicsLayoutArea.Guid;

            RolloutRectangle = graphicsRollout.RolloutRectangle;

            SeamWidth = graphicsRollout.SeamWidth;

            MaterialWidth = graphicsRollout.MaterialWidth;

            MaterialOverlap = graphicsRollout.MaterialOverlap;

            CutList = graphicsRollout.GraphicsCutList.ConvertAll(c=>new CutSerializable(c));

            UndrageList = graphicsRollout.GraphicsUndrageList.ConvertAll(u=>new UndrageSerializable(u));

            RolloutAngle = graphicsRollout.RolloutAngle;
        }

        public GraphicsRollout Deserialize(
            GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , GraphicsLayerBase graphicsCutIndexLayer
            , GraphicsLayerBase overageLayer
            , GraphicsLayerBase overageIndexLayer)
        {

            GraphicsRollout graphicsRollout = new GraphicsRollout(null, window, page)
            {
                RolloutRectangle = this.RolloutRectangle
                ,SeamWidth = this.SeamWidth
                , MaterialWidth = this.MaterialWidth
                , MaterialOverlap = this.MaterialOverlap
                , Guid = this.RolloutGuid
                , RolloutAngle = this.RolloutAngle
                ,LayoutAreaGuid = this.LayoutAreaGuid
                , FinishesLibElements = finishesLibElements
            };

            graphicsRollout.GraphicsRollout = graphicsRollout;

            List<GraphicsCut> graphicsCutList = 
                this.CutList.ConvertAll(c => c.Deserialize(window, page, finishesLibElements, graphicsCutIndexLayer, overageLayer, overageIndexLayer, graphicsRollout));

            List<GraphicsUndrage> graphicsUndrageList = this.UndrageList.ConvertAll(u => u.Deserialize(window, page, graphicsRollout, finishesLibElements));

            graphicsCutList.ForEach(c => graphicsRollout.Add(c));

            graphicsUndrageList.ForEach(u => graphicsRollout.Add(u));

            return graphicsRollout;
        }

    }
}
