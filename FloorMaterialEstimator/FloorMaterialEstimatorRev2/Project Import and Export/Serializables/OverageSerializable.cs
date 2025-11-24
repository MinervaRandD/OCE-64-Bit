
namespace FloorMaterialEstimator
{
    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using Utilities;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FinishesLib;

    public class OverageSerializable
    {
        public OverageIndexSerializable OverageIndex { get; set; }

        public bool IsEmbeddedOverage { get; set; }
        public Rectangle BoundingRectangle { get; set; }
        
        public List<EmbeddedCutSerializable> EmbeddedCutList { get; set; }

        public DoubleTupleSerializable EffectiveDimensions { get; set; } = null;

        public DoubleTupleSerializable OverrideEffectiveDimensions { get; set; } = null;

        public string ParentCutGuid { get; set; }

        public string Guid { get; set; }

        public bool Deleted { get; set; }

        public OverageSerializable() { }

        public OverageSerializable(GraphicsOverage graphicsOverage)
        {
            OverageIndex = new OverageIndexSerializable(graphicsOverage.GraphicsOverageIndex);

            IsEmbeddedOverage = graphicsOverage.IsEmbeddedOverage;

            BoundingRectangle = graphicsOverage.BoundingRectangle;

            Guid = graphicsOverage.Guid;

            Deleted = graphicsOverage.Deleted;

            ParentCutGuid = graphicsOverage.ParentGraphicsCut.Guid;

            if (!(graphicsOverage.EffectiveDimensions is null))
            {
                this.EffectiveDimensions = new DoubleTupleSerializable(graphicsOverage.EffectiveDimensions);
            }
          
            if (!(graphicsOverage.OverrideEffectiveDimensions is null))
            {
                this.OverrideEffectiveDimensions = new DoubleTupleSerializable(graphicsOverage.OverrideEffectiveDimensions);
            }
        }

        public GraphicsOverage Deserialize(
            GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , GraphicsLayerBase overageLayer
            , GraphicsLayerBase overageIndexLayer
            , GraphicsCut graphicsCut)
        {
            // The following is a mess. It would be better to have a graphicsOverage serializable distinct from graphics graphicsOverage serializable.

            Overage overage = new Overage()
            {
                BoundingRectangle = this.BoundingRectangle
            };

         
            GraphicsOverage graphicsOverage = new GraphicsOverage(graphicsCut, window, page, finishesLibElements, overage);

            graphicsOverage.GraphicsOverageIndex = null;

            if (Utilities.IsNotNull(this.OverageIndex))
            {
                graphicsOverage.GraphicsOverageIndex = this.OverageIndex.Deserialize(graphicsOverage, window, page, overageIndexLayer);
            }

            graphicsOverage.OverageIndex = graphicsOverage.GraphicsOverageIndex.OverageIndex;


            graphicsOverage.EffectiveDimensions = null;
            graphicsOverage.OverrideEffectiveDimensions = null;

            if (!(this.EffectiveDimensions is null))
            {
                graphicsOverage.EffectiveDimensions = this.EffectiveDimensions.Deserialize();
            }

            if (!(this.OverrideEffectiveDimensions is null))
            {
                graphicsOverage.OverrideEffectiveDimensions = this.OverrideEffectiveDimensions.Deserialize();
            }

            graphicsOverage.Deleted = this.Deleted;

            graphicsOverage.EmbeddedCutList = new List<EmbeddedCut>();

            foreach (EmbeddedCutSerializable embeddedCutSerializable in this.EmbeddedCutList)
            {
                graphicsOverage.EmbeddedCutList.Add(embeddedCutSerializable.Deserialize());
            }

            graphicsOverage.IsEmbeddedOverage = this.IsEmbeddedOverage;

            Overage.AddIndex(graphicsOverage.OverageIndex);

            return graphicsOverage;
        }

    }
}
