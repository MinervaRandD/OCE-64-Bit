using FloorMaterialEstimator.CanvasManager;
using Geometry;
using Graphics;
using Utilities;
using MaterialsLayout;
using MaterialsLayout.MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaletteLib;
using FinishesLib;

namespace FloorMaterialEstimator
{
    public class CutSerializable
    {
        public string ParentRolloutGuid { get; set; }

        public string Guid { get; set; }

        public uint CutIndex { get; set; }

        public CutIndexSerializable CutIndexSerializable { get; set; }

        public List<InternalDirectedPolygonSerializable> CutPolygonList { get; set; }

        public RectangleSerializable CutRectangle { get; set; }

        public RectangleSerializable OverrideCutRectangle { get; set; }

        public List<OverageSerializable> OverageList { get; set; } = new List<OverageSerializable>();

        public List<RemnantCutSerializable> RemnantCutList { get; set; } = new List<RemnantCutSerializable>();

        //public List<UnderageSerializable> UnderageList { get; set; }

        public double CutAngle { get; set; }

        public Coordinate CutOffset { get; set; }

        public bool IsRotated { get; set; }

        public double SeamWidth { get; set; }

        public double MaterialWidth { get; set; }

        public double MaterialOverlap { get; set; }

        public decimal PatternRepeats { get; set; }

        public CutSerializable() { }

        public CutSerializable(GraphicsCut graphicsCut)
        {
            this.Guid = graphicsCut.Guid;

            if (!(graphicsCut.ParentGraphicsRollout is null))
            {
                ParentRolloutGuid = graphicsCut.ParentGraphicsRollout.Guid;
            }

            CutAngle = graphicsCut.CutAngle;

            CutOffset = graphicsCut.CutOffset;

            CutIndex = graphicsCut.CutIndex;

            SeamWidth = graphicsCut.SeamWidth;

            MaterialWidth = graphicsCut.MaterialWidth;

            MaterialOverlap = graphicsCut.MaterialOverlap;

            PatternRepeats = graphicsCut.PatternRepeats;

            if (Utilities.Utilities.IsNotNull(graphicsCut.GraphicsCutIndex))
            {
                CutIndexSerializable = new CutIndexSerializable(graphicsCut.GraphicsCutIndex);
            }

            else
            {
                CutIndexSerializable = null;

                // Only normal layout areas have cut indices.
                //ProjectSerializable.ProjectSerializationSucceeded = false;
            }

            IsRotated = graphicsCut.IsRotated;

            CutRectangle = new RectangleSerializable(graphicsCut.CutRectangle);

            if (graphicsCut.OverrideCutRectangle is null)
            {
                OverrideCutRectangle = null;
            }

            else
            {
                OverrideCutRectangle = new RectangleSerializable(graphicsCut.OverrideCutRectangle);
            }

            CutPolygonList = new List<InternalDirectedPolygonSerializable>();

            graphicsCut.GraphicsCutPolygonList.ForEach(p => CutPolygonList.Add(new InternalDirectedPolygonSerializable(p)));

            foreach (GraphicsOverage graphicsOverage in graphicsCut.GraphicsOverageList)
            {
                OverageSerializable overageSerializable = new OverageSerializable(graphicsOverage);

                OverageList.Add(overageSerializable);
            }

            foreach (GraphicsRemnantCut graphicsRemnantCut in graphicsCut.GraphicsRemnantCutList)
            {
                RemnantCutSerializable remnantCutSerializable = new RemnantCutSerializable(graphicsRemnantCut);

                RemnantCutList.Add(remnantCutSerializable);
            }
        }

        public GraphicsCut Deserialize(
            GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , GraphicsLayerBase graphicsCutIndexLayer
            , GraphicsLayerBase overageLayer
            , GraphicsLayerBase overageIndexLayer
            , GraphicsRollout graphicsRollout)
        {
            GraphicsCut graphicsCut = new GraphicsCut(graphicsRollout, window, page, this.Guid, finishesLibElements);

            //----------------------------------------------------------------------------//
            // For non normal roll out areas, there is no index set up, so we filter here //
            //----------------------------------------------------------------------------//

            if (this.CutIndexSerializable != null)
            {
                graphicsCut.GraphicsCutIndex = this.CutIndexSerializable.Deserialize(window, page, graphicsCutIndexLayer);

                graphicsCut.CutIndex = this.CutIndex;

                graphicsCut.FinishesLibElements = finishesLibElements;

            }

            graphicsCut.IsRotated = this.IsRotated;

            graphicsCut.CutRectangle = this.CutRectangle.Deserialize();

            graphicsCut.SeamWidth = SeamWidth; 

            graphicsCut.MaterialWidth= MaterialWidth;

             graphicsCut.MaterialOverlap = MaterialOverlap;

            graphicsCut.PatternRepeats = this.PatternRepeats;

            if (this.OverrideCutRectangle is null)
            {
                graphicsCut.OverrideCutRectangle = null;
            }

            else
            {
                graphicsCut.OverrideCutRectangle = this.OverrideCutRectangle.Deserialize();
            }
            

            graphicsCut.CutPolygonList = this.CutPolygonList.ConvertAll(p => p.Deserialize());

            graphicsCut.CutAngle = this.CutAngle;

            graphicsCut.CutOffset = this.CutOffset;

            foreach (OverageSerializable overageSerializable in this.OverageList)
            {
                graphicsCut.Add(overageSerializable.Deserialize(window, page, finishesLibElements, overageLayer, overageIndexLayer, graphicsCut));
            }

            foreach (RemnantCutSerializable remnantCutSerializable in this.RemnantCutList)
            {
                graphicsCut.GraphicsRemnantCutList.Add(remnantCutSerializable.Deserialize(window, page, graphicsCut));
            }

            return graphicsCut;
        }
    }
}
