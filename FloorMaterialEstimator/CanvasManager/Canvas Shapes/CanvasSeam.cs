

namespace CanvasManager.CanvasShapes
{
    using Globals;
    using Graphics;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using FinishesLib;
    using Utilities;
    using System.Drawing;

    public class CanvasSeam
    {
        public CanvasLayoutArea layoutArea;

        public SeamFinishBase SeamFinishBase { get; set; }


        public AreaFinishLayers areaFinishLayers = null;

        public string Guid { get; private set; }

        public SeamType SeamType
        {
            get
            {
                if (GraphicsSeam is null)
                {
                    throw new NotImplementedException("Attempt to get seam type with graphics seam undefined");
                }

                return GraphicsSeam.SeamType;
            }

            set
            {
                if (GraphicsSeam is null)
                {
                    throw new NotImplementedException("Attempt to set seam type with graphics seam undefined");
                }

                GraphicsSeam.SeamType = value;
            }
        }

        public bool IsHideable
        {
            get
            {
                if (GraphicsSeam is null)
                {
                    throw new NotImplementedException("Attempt to get hideable with graphics seam undefined");
                }

                return GraphicsSeam.IsHideable;
            }

            set
            {
                if (GraphicsSeam is null)
                {
                    throw new NotImplementedException("Attempt to set hideable with graphics seam undefined");
                }

                GraphicsSeam.IsHideable = value;
            }
        }

        public GraphicsSeam GraphicsSeam { get; set; }
        
        public bool Selected
        {
            get
            {
                return GraphicsSeam.Selected;
            }

            set
            {
                GraphicsSeam.Selected = value;
            }
        }

        public CanvasSeam(GraphicsWindow window, GraphicsPage page, CanvasLayoutArea layoutArea, GraphicsSeam graphicsSeam)
        {
            this.layoutArea = layoutArea;
            this.SeamFinishBase = layoutArea.AreaFinishManager.AreaFinishBase.SeamFinishBase;

            GraphicsSeam = graphicsSeam;

            areaFinishLayers = layoutArea.AreaFinishManager.AreaFinishLayers;

            string guid = GuidMaintenance.CreateGuid(this);

            this.Guid = guid;
        }

        public CanvasSeam(GraphicsSeam graphicsSeam)
        {
            GraphicsSeam = graphicsSeam;

            string guid = GuidMaintenance.CreateGuid(this);

            this.Guid = guid;
        }

        public void SetSeamLineWidth(DesignState designState, bool selected)
        {
            double seamWidthInPts = GetSeamWidthInPts(designState, selected);

            GraphicsSeam.UpdateSeamGraphics(SeamFinishBase.SeamColor, SeamFinishBase.VisioDashType, seamWidthInPts);
        }

        public void UpdateSeamGraphics(SeamFinishBase seamFinishBase, bool selected)
        {
            this.SeamFinishBase = seamFinishBase;

            double seamWidthInPts = GetSeamWidthInPts(SystemState.DesignState, selected);

            GraphicsSeam.UpdateSeamGraphics(seamFinishBase.SeamColor, seamFinishBase.VisioDashType, seamWidthInPts);
        }


        public double GetSeamWidthInPts(DesignState designState, bool selected)
        {
            if (designState == DesignState.Area)
            {
                return SeamFinishBase.SeamWidthInPts;
            }

            if (designState == DesignState.Line)
            {
                return 0;
            }

            if (designState == DesignState.Seam)
            {
                if (selected)
                {
                    return SeamFinishBase.SeamWidthInPts;
                }

                else
                {
                    return 0.25;
                }
            }

            return 0;
        }

        public void AddToSeamLayer()
        {
            if (SeamType == SeamType.Basic)
            {
                if (!IsHideable)
                {
                    areaFinishLayers.NormalSeamsUnhideableLayer.AddShape(GraphicsSeam.Shape, 1);
                    areaFinishLayers.NormalSeamsLayer.AddShape(GraphicsSeam.Shape, 1);

                }

                else
                {
                    areaFinishLayers.NormalSeamsLayer.AddShape(GraphicsSeam.Shape, 1);
                }
            }

            else if (SeamType == SeamType.Manual)
            {
                areaFinishLayers.ManualSeamsAllLayer.AddShape(GraphicsSeam.Shape, 1);
            }
        }

        public void RemoveFromSeamLayer()
        {
            if (SeamType == SeamType.Basic)
            {
                if (!IsHideable)
                {
                    areaFinishLayers.NormalSeamsUnhideableLayer.RemoveShapeFromLayer(GraphicsSeam.Shape, 1);
                    areaFinishLayers.NormalSeamsLayer.RemoveShapeFromLayer(GraphicsSeam.Shape, 1);
                }

                else
                {
                    areaFinishLayers.NormalSeamsUnhideableLayer.RemoveShapeFromLayer(GraphicsSeam.Shape, 1);
                }
            }

            else if (SeamType == SeamType.Manual)
            {
                //if (IsHideable)
                //{
                //    layoutArea.ManualSeamsUnhideableLayer.RemoveShape(GraphicsSeam.Shape, 1);
                //}

                //else
                //{
                areaFinishLayers.ManualSeamsAllLayer.RemoveShapeFromLayer(GraphicsSeam.Shape, 1);
                //}
            }
        }

        public void Draw(Color seamColor, double lineWidthInPts, int visioDashType) => GraphicsSeam.Draw(seamColor, lineWidthInPts, visioDashType);
        
        public void Select()
        {
           Selected = true;

            GraphicsSeam.Shape.SetLineWidth(SeamFinishBase.SeamWidthInPts * 2.0);
        }

        public void Deselect()
        {
            Selected = false;

            GraphicsSeam.Shape.SetLineWidth(SeamFinishBase.SeamWidthInPts);
        }

        public void Delete()
        {
            GraphicsSeam.Delete();
        }

    }
}
