
namespace FloorMaterialEstimator
{

    using Visio = Microsoft.Office.Interop.Visio;
    using Graphics;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using FinishesLib;
    using System;
    using FloorMaterialEstimator.CanvasManager;

    // This class is a garbage pail of specific checks needed from time to time.
    public static class DebugSupportRoutines
    {
        public static void CheckForUnlinkedPolyline(GraphicsPage page, int checkPoint)
        {

            foreach (Visio.Shape visioShape in page.VisioPage.Shapes)
            {
                if (string.IsNullOrEmpty(visioShape.Data1) && visioShape.Data2 == "Polyline")
                {
                    MessageBox.Show("Unlinked polyline found at point " + checkPoint);

                    return;
                }
            }
        }

        public static void CheckForUnlinkedImage(GraphicsPage page, int checkPoint)
        {

            foreach (Visio.Shape visioShape in page.VisioPage.Shapes)
            {
                if (string.IsNullOrEmpty(visioShape.Data1) && visioShape.Data2 == "Image")
                {
                    MessageBox.Show("Unlinked image found at point " + checkPoint);

                    return;
                }
            }
        }

        public static void CheckForNullInPageShapeDict(GraphicsPage page, int checkPoint)
        {
            return;

            //foreach (IGraphicsShape iShape in page.PageShapeDict.Values)
            //{
            //    if (iShape.Shape is null)
            //    {
            //        MessageBox.Show("Null shape in shape dict at point " + checkPoint);
            //    }
            //}
        }

        public static void FinishBaseListEvents(AreaFinishBaseList areaFinishBaseList)
        {

            List<Delegate> itemAddedEvenHandlerList = areaFinishBaseList.ItemAddedEventHandlerList();
        }

        public static string LayerVisibilityConsistencyCheck(GraphicsLayerBase baseLayer)
        {
            if (baseLayer is null)
            {
                return "Base layer is null";
            }

            GraphicsLayer graphicsLayer = baseLayer.GetBaseLayer();

            if (graphicsLayer is null)
            {
                return "Graphics layer is null";
            }

            return LayerVisibilityConsistencyCheck(graphicsLayer);
        }

        public static string LayerVisibilityConsistencyCheck(GraphicsLayer graphicLayer)
        {
            if (graphicLayer is null)
            {
                return "Layer is null.";
            }

            Visio.Layer visioLayer = graphicLayer.visioLayer;

            if (visioLayer is null)
            {
                return "Visio layer is null.";
            }

            bool visioLayerVisibility = VisioInterop.GetLayerVisibility(visioLayer);

            if (graphicLayer.Visibility != visioLayerVisibility)
            {
                return "Layers are inconsistent: graphics layer visibility = " + graphicLayer.Visibility.ToString() + ", visio layer visibility = " + visioLayerVisibility.ToString() + ".";
            }

            return "Layer visibility is consistent.";
        }

        public static string ExternalAreaGuidCheck(CanvasLayoutArea canvasLayoutArea)
        {
            if (canvasLayoutArea.ExternalArea.Guid == canvasLayoutArea.Guid)
            {
                return "Canvas layout area guid == external area guid.";
            }

            else
            {
                return "Canvvas layout area guid != external area guid.";
            }
        }
    }
}
