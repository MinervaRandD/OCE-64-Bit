namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.CanvasManager;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;
    using Graphics;
    using TracerLib;
    using System;

    public class CanvasEraser
    {
        FloorMaterialEstimatorBaseForm baseForm;
        CanvasManager.CanvasManager canvasManager;
        CanvasPage currentPage;

        GraphicsWindow window => canvasManager.Window;

        GraphicsPage page => canvasManager.CurrentPage;

        public CanvasEraser(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;
            canvasManager = baseForm.CanvasManager;
            currentPage = canvasManager.CurrentPage;
        }

        /// <summary>
        /// Remove all elements of the current project
        /// </summary>
        public void ClearCurrentCanvas()
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif
            removeBuildingLine();

            // MDD Reset

            List<GraphicsLayer> layerList = currentPage.GraphicsLayers.ToList();

            foreach (GraphicsLayer graphicsLayer in layerList)
            {
                if (graphicsLayer.GraphicsLayerStyle == GraphicsLayerStyle.Static)
                {
                    continue;
                }

                graphicsLayer.RemoveAllShapes();

                if (graphicsLayer.GraphicsLayerStyle == GraphicsLayerStyle.Dynamic)
                {
                    currentPage.RemoveFromGraphicsLayerDict(graphicsLayer);

                    graphicsLayer.Delete();
                }
            }

            removeDrawing();

            removeLayoutAreas();

            //-------------------------------------------------------------------------//
            // Clear global layers -- they will be dynamically reconstructed as needed //
            //-------------------------------------------------------------------------//

            page.AreaModeGlobalLayer.Delete();
            page.LineModeGlobalLayer.Delete();
            page.SeamModeGlobalLayer.Delete();
            page.AreaLegendLayer.Delete();
            page.DrawingLayer.Delete();
            page.TakeoutLayer.Delete();
            page.AreaLegendLayer.Delete();
            page.LineLegendLayer.Delete();

            // -------------------------------------------------------------------------//
            // Nuclear option, remove everything not already removed. Once cleaned up   //
            // this should not be necessary.
            // -------------------------------------------------------------------------//

            List<IGraphicsShape> shapeList = currentPage.GetShapeList();

            foreach (IGraphicsShape iShape in shapeList)
            {
                if (iShape.ShapeType == ShapeType.LockIcon
                    || iShape.ShapeType == ShapeType.MeasuringStick
                    || iShape.ShapeType == ShapeType.SeamingTool
                    || iShape.ShapeType == ShapeType.ExtendedCrosshairs)
                {
                    continue;
                }

                page.RemoveFromPageShapeDict((GraphicShape)iShape);

                if ((GraphicShape)iShape is null)
                {
                    Tracer.TraceGen.TraceError("Attempt to delete a null shape in CanvasEraser:ClearCurrentCanvas.", 1, true);

                    continue;
                }

                ((GraphicShape)iShape).Delete();
            }

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                areaFinishManager.Delete();
            }

            foreach (LineFinishManager lineFinishManager in FinishManagerGlobals.LineFinishManagerList)
            {
                lineFinishManager.Delete();
            }

            foreach (SeamFinishManager seamFinishManager in  FinishManagerGlobals.SeamFinishManagerList)
            {
                seamFinishManager.Delete();
            }
            // The following is the nuclear option and shouldn't be needed
            // because by design, every shape should be on a layer.


            Visio.Page visioPage = page.VisioPage;

            try
            {
                foreach (Visio.Layer visioLayer in visioPage.Layers)
                {
                    VisioInterop.UnlockLayer(visioLayer);
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Attempt to unlock layers in CanvasEraser:ClearCurrentCanvas throws an exception.", ex, 1, true);
            }

            try
            {
                List<Visio.Shape> remainingShapes = new List<Visio.Shape>();

                foreach (Visio.Shape visioShape in visioPage.Shapes)
                {
                    remainingShapes.Add(visioShape);
                }

                foreach (Visio.Shape visioShape in remainingShapes)
                {
                    if (visioShape.Data1 == "[LockIcon]"
                       || visioShape.Data1 == "[MeasuringStick]"
                       || visioShape.Data1 == "[SeamingTool]"
                       || visioShape.Data1.StartsWith("[ExtendedCrosshairs]"))
                    {
                        continue;
                    }

                    VisioInterop.UnlockShape(visioShape);

                    visioShape.Delete();
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("Attempt to delete shapes in CanvasEraser:ClearCurrentCanvas throws an exception.", ex, 1, true);
            }

            canvasManager.LegendController.AreaModeLegend.Delete();
            canvasManager.LegendController.LineModeLegend.Delete();

            removeCounters();

            clearCurrentPage();

            GuidMaintenance.ClearGuids();
        }
        
        bool IsToolGraphicsLayer(GraphicsLayer graphicsLayer) 
        {
            return graphicsLayer.Guid == canvasManager.MeasuringStick.Guid;
        }

        bool IsToolShape(GraphicShape shape) 
        {
            return shape.Guid == canvasManager.MeasuringStick.Guid;
        }

        bool IsToolVisioShape(Visio.Shape shape) 
        {
            return shape.Data3 == canvasManager.MeasuringStick.Guid;
        }

        //private string ValidateCleanProject()
        //{
        //    foreach (UCAreaFinishPaletteElement areaPaletteElement in PalettesGlobal.AreaFinishPalette)
        //    {
        //        if (areaPaletteElement.CanvasLayoutAreaDict.Count > 0)
        //        {
        //            return "Palette element layout area dictionary is not empty.";
        //        }
                
        //        if (currentPage.LayoutAreaCount > 0)
        //        {
        //            return "Current Page guid layout area dictionary is not empty.";
        //        }

        //        //if (!(areaPaletteElement.AreaDesignStateLayer is null))
        //        //{
        //        //    return "Area mode area design state layer is not null.";
        //        //}

        //        //if (!(areaPaletteElement.SeamDesignStateLayer is null))
        //        //{
        //        //    return "Area mode seam design state layer is not null.";
        //        //}
        //    }

        //    foreach (UCLineFinishPaletteElement linePaletteElement in baseForm.linePalette)
        //    {
        //        if (linePaletteElement.LineFinishManager.LineDictCount > 0)
        //        {
        //            return "Palette element line dictionary is not empty.";
        //        }

        //        if (currentPage.DirectedLineCount > 0)
        //        {
        //            return "Current Page line dictionary is not empty.";
        //        }
                
        //        if (!(linePaletteElement.AreaDesignStateLayer is null))
        //        {
        //            return "Line mode area design state layer is not null.";
        //        }

        //        if (!(linePaletteElement.LineDesignStateLayer is null))
        //        {
        //            return "Line mode line design state layer is not null.";
        //        }
        //    }

        //    return string.Empty;
        //}

        private void removeDrawing()
        {

            if (Utilities.IsNotNull(currentPage.DrawingInBytes))
            {
                currentPage.DrawingInBytes = null;
            }

            if (Utilities.IsNotNull(currentPage.Drawing))
            {
                page.RemoveFromPageShapeDict(currentPage.Drawing);

                currentPage.Drawing.Delete();

                currentPage.Drawing = null;
            }

        }

        private void removeBuildingLine()
        {
            if (!(canvasManager.BuildingPolyline is null))
            {
                canvasManager.BuildingPolyline.Delete();
                canvasManager.BuildingPolyline = null;
            }
        }

        private void removeLayoutAreas()
        {
            foreach (CanvasLayoutArea layoutArea in currentPage.LayoutAreas.ToList())
            {
                //----------------------------------------------------//
                // The following is removed because removal of layout //
                // area occurs in layoutArea.Delete()                 //
                //----------------------------------------------------//

                // currentPage.RemoveLayoutArea(layoutArea); // Removed. Redundant. See comment above.

                layoutArea.Delete();
            }

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                areaFinishManager.CanvasLayoutAreaDict.Clear();
            }

            //currentPage.LayoutAreaDict.Clear();
            currentPage.ClearLayoutAreaDict();
        }

        private void removeDirectedLines()
        {
            foreach (CanvasDirectedLine directedLine in currentPage.DirectedLines.ToList())
            {
                if (!(directedLine.ucLine is null))
                {
                    directedLine.LineFinishManager.RemoveLineFull(directedLine);
                }

                currentPage.RemoveFromDirectedLineDict(directedLine);

                VisioInterop.DeleteShape(directedLine.Shape);
            }

            foreach (LineFinishManager lineFinishManager in FinishManagerGlobals.LineFinishManagerList)
            {
                lineFinishManager.ClearLineDict();

                lineFinishManager.AreaDesignStateLayer.Delete();
                lineFinishManager.LineDesignStateLayer.Delete();
                lineFinishManager.SeamDesignStateLayer.Delete();
                lineFinishManager.RemnantSeamDesignStateLayer.Delete();

            }

            //currentPage.DirectedLineDict.Clear();
            currentPage.ClearDirectedLineDict();
        }

        private void removeCounters()
        {
            canvasManager.CounterController.RemoveCounters();
        }

        private void clearCurrentPage()
        {
            // Clear everything except for measuring stick and seaming tool.

            List<IGraphicsShape> shapesToRemove = new List<IGraphicsShape>(this.currentPage.PageShapeDictValues);

            foreach(IGraphicsShape iShape in shapesToRemove)
            {
                if (iShape.ShapeType != ShapeType.MeasuringStick && iShape.ShapeType != ShapeType.SeamingTool && iShape.ShapeType != ShapeType.ExtendedCrosshairs)
                {
                    this.currentPage.RemoveFromPageShapeDict(iShape.Guid);
                }
            }

           // this.currentPage.ShapeDict.Clear();
           // this.currentPage.GuidShapeDict.Clear();

            canvasManager.RemoveMarkerAndGuides();

            this.currentPage.ClearLayoutAreaDict();

            this.currentPage.ClearDirectedLineDict();

            this.currentPage.SelectedLineDict.Clear();

           // this.currentPage.GraphicsTakeoutAreaDict.Clear();

            this.currentPage.CurrentGuideList.Clear();

        }
    }
}
