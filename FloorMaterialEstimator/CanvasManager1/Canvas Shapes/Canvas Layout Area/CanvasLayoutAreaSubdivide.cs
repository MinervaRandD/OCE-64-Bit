namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
 
    using MaterialsLayout;
    
    using SettingsLib;
    using Utilities;
    using Globals;
    using MaterialsLayout.MaterialsLayout;
    using CanvasLib.Markers_and_Guides;
    using DebugSupport;
    using TracerLib;

    public partial class CanvasLayoutArea : GraphicsLayoutArea, IAreaShape
    {
        internal void SelectForSubdivision()
        {
            //if (ParentArea != null)
            //{
            //    ParentArea.MakeDisappear();
            //}

            Color selectedAreaColor = GlobalSettings.SelectedAreaColor;

            this.SetFillColor(selectedAreaColor);

            double opacity = GlobalSettings.SelectedAreaOpacity;

            this.SetFillOpacity(opacity);

            this.SeamDesignStateSubdivisionModeSelected = true;
        }


        internal void DeselectForSubdivision()
        {
            this.SeamDesignStateSubdivisionModeSelected = false;

            this.SetFillColor(this.AreaFinishBase.Color);
            this.SetFillOpacity(this.AreaFinishBase.Opacity);
        }

#if false
        public bool SubdivideByLine(CanvasDirectedPolyline polyline)
        {
            // In this approach, the underlying Visio geometry engine is used to subdivide the shape.
            // This is done because it is much more reliable that building the corresponding logic from scratch.
            // However, the geometry engine has side effects, most notably that it removes the original shape from the
            // canvas and much of the following logic is needed to deal with this. The process is pretty tricky and 
            // may be hard to follow.

            ((GraphicsLayoutArea)this).Shape = this.Shape;

            // Need to remove start marker here because otherwise it gets repeated by the visio engine and leaves
            // a circle on the canvas.

            // This is where the subdivision actually occurs. What gets returned is a list of shapes of subdivided regions. These shapes
            // have already been placed on the canvas by the Visio geometry engine.

            //string layerConsistency = LayerVisibilityConsistencyCheck(layoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

            List<Shape> subdivideShapeList = this.Divide(Window, Page, polyline.GetCoordinateList());

            if (subdivideShapeList.Count <= 0)
            {
                return false;
            }

            List<CanvasLayoutArea> subdividedAreaList = new List<CanvasLayoutArea>();

            List<DirectedPolygon> internalBoundaryList = new List<DirectedPolygon>();

            //layerConsistency = LayerVisibilityConsistencyCheck(layoutAreaForSubdivision.AreaFinishManager.AreaDesignStateLayer);

            foreach (Shape shape in subdivideShapeList)
            {
                internalBoundaryList.Clear();

                DirectedPolygon externalBoundary = VisioInterop.GetShapeBoundaries(shape, internalBoundaryList);

                //internalBoundaryList.Clear();

                LayoutArea layoutArea = new LayoutArea(externalBoundary, internalBoundaryList);

                // The corresponding shapes are already on the canvas (Visio puts them there). Now we construct layout areas around these shapes for
                // internal maintenance. Normally, it would be the other way around: The layout area is constructed first and then drawn onto the canvas.

                CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(this.canvasManager, layoutArea, this.UCAreaFinish, DesignState.Seam);

                canvasLayoutArea.Guid = shape.Guid;

                VisioInterop.SetShapeData(shape, "Subdivison Layout Area", "Complex Shape[" + this.UCAreaFinish.AreaName + "]", shape.Guid);

                canvasLayoutArea.Shape = shape;

                subdividedAreaList.Add(canvasLayoutArea);
            }

            if (subdividedAreaList.Count <= 0)
            {
                return false;
            }
        }
#endif
    }
}
