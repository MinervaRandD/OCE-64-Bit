
namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FinishesLib;
    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using Utilities;

    public class UndrageSerializable
    {
        public UndrageIndexSerializable UndrageIndex { get; set; }

        public Rectangle BoundingRectangle { get; set; }
       
        public string Guid { get; set; }

        public DoubleTupleSerializable EffectiveDimensions { get; set; } = null;

        public DoubleTupleSerializable OverrideEffectiveDimensions { get; set; } = null;

        public bool Deleted { get; set; }

        public string ParentRolloutGuid { get; set; }

        public double MaterialWidthInInches { get; set; }

        public List<EmbeddedCutSerializable> EmbeddedCutList { get; set; }

        public UndrageSerializable() { }

        public UndrageSerializable(GraphicsUndrage undrage)
        {
            UndrageIndex = new UndrageIndexSerializable(undrage.GraphicsUndrageIndex);

            this.BoundingRectangle = undrage.BoundingRectangle;

            this.Guid = undrage.Guid;

            this.ParentRolloutGuid = undrage.GraphicsRolloutGuid;

            this.Deleted = undrage.Deleted;


            if (!(undrage.EffectiveDimensions is null))
            {
                this.EffectiveDimensions = new DoubleTupleSerializable(undrage.EffectiveDimensions);
            }

            if (!(undrage.OverrideEffectiveDimensions is null))
            {
                this.OverrideEffectiveDimensions = new DoubleTupleSerializable(undrage.OverrideEffectiveDimensions);
            }
        }

        public GraphicsUndrage Deserialize(
            GraphicsWindow window
            , GraphicsPage page
            , GraphicsRollout graphicsRollout
            , FinishesLibElements finishesLibElements)
        {
            GraphicsUndrage graphicsUndrage = new GraphicsUndrage(
                graphicsRollout
                , window
                , page
                , finishesLibElements
                , this.BoundingRectangle)
            {
                Deleted = this.Deleted
            };

            graphicsUndrage.ParentGraphicsRollout = graphicsRollout;

            graphicsUndrage.GraphicsUndrageIndex = null;

            if (Utilities.IsNotNull(this.UndrageIndex))
            {
                graphicsUndrage.GraphicsUndrageIndex = this.UndrageIndex.Deserialize(graphicsUndrage, window, page);
            }

            
            graphicsUndrage.EffectiveDimensions = null;
            graphicsUndrage.OverrideEffectiveDimensions = null;

            graphicsUndrage.MaterialWidth = graphicsUndrage.ParentGraphicsRollout.MaterialWidth;

            if (!(this.EffectiveDimensions is null))
            {
                graphicsUndrage.EffectiveDimensions = this.EffectiveDimensions.Deserialize();
            }

            if (!(this.OverrideEffectiveDimensions is null))
            {
                graphicsUndrage.OverrideEffectiveDimensions = this.OverrideEffectiveDimensions.Deserialize();
            }


            graphicsUndrage.EmbeddedCutList = new List<EmbeddedCut>();

            foreach (EmbeddedCutSerializable embeddedCutSerializable in this.EmbeddedCutList)
            {
                graphicsUndrage.EmbeddedCutList.Add(embeddedCutSerializable.Deserialize());
            }

            Undrage.AddIndex(graphicsUndrage.UndrageIndex);

            return graphicsUndrage;
        }
    }
}
