

namespace FloorMaterialEstimator
{
    using Graphics;
    using System.Collections.Generic;
    using System.Linq;
    using CanvasManager;
    using Utilities;
    using Visio = Microsoft.Office.Interop.Visio;
    using System;

    class ConsistencyChecker
    {
        private GraphicsWindow window;
        private GraphicsPage page;
        private CanvasPage canvasPage;

        public ConsistencyChecker(GraphicsWindow window, CanvasPage canvasPage, GraphicsPage page)
        {
            this.window = window;

            this.page = page;

            this.canvasPage = canvasPage;
        }

        public List<string> GenerateConsistencyErrors()
        {
            List<string> errorList = new List<string>();

            errorList.AddRange(generateShapeInconsistencies());
            errorList.AddRange(generateLayerInconsistencies());
            errorList.AddRange(generateShapeGuidInconsistencies());
            errorList.AddRange(CanvasLayoutInconsistencies());

            return errorList;
        }

        private List<string> generateShapeGuidInconsistencies()
        {
            List<string> errorList = new List<string>();

            foreach (IGraphicsShape iShape in page.PageShapeDictValues)
            {
                if (iShape.ShapeType == ShapeType.LayoutArea)
                {
                    continue; // Haven't figured this one out yet.
                }

                Shape shape = iShape.Shape;

                if (shape is null)
                {
                    errorList.Add("IShape '" + FormatUtils.FormatGuidAbbreviation(iShape.Guid) + "' has a null Shape.");
                }

                else
                {
                    if (shape.Guid != iShape.Guid)
                    {
                        errorList.Add("Shape guid mismatch: IShape guid '"
                            + FormatUtils.FormatGuidAbbreviation(iShape.Guid)
                            + "', Shape guid '" 
                            + FormatUtils.FormatGuidAbbreviation(shape.Guid) 
                            + "'.");
                    }

                    Visio.Shape visioShape = shape.VisioShape;

                    if (visioShape is null)
                    {
                        errorList.Add("Shape '" + FormatUtils.FormatGuidAbbreviation(shape.Guid) + "' has a null visio shape.");
                    }

                    else
                    {
                        if (iShape.Guid != shape.Guid || iShape.Guid != visioShape.Data3 || shape.Guid != visioShape.Data3)
                        {
                            errorList.Add("Shape guid mismatch: IShape guid '"
                                + FormatUtils.FormatGuidAbbreviation(iShape.Guid)
                                + "', Shape guid '"
                                + FormatUtils.FormatGuidAbbreviation(shape.Guid)
                                + "', visio shape guid '"
                                + FormatUtils.FormatGuidAbbreviation(visioShape.Data3)
                                + "'.");
                        }
                    }
                }
            }

            return errorList;

        }

        private List<string> generateShapeInconsistencies()
        {
            List<string> errorList = new List<string>();

            errorList.AddRange(compareVisioShapesToShapeDict());

            return errorList;
        }

        private List<string> generateLayerInconsistencies()
        {
            List<string> errorList = new List<string>();

            errorList.AddRange(compareVisioLayersToLayerDict());

            return errorList;
        }

        private List<string> compareVisioShapesToShapeDict()
        {
            List<string> errorList = new List<string>();

            HashSet<Visio.Shape> shapeDictSet = new HashSet<Visio.Shape>();

            foreach (IGraphicsShape iShape in page.PageShapeDictValues)
            {
                Shape shape = iShape.Shape;

                if (shape != null)
                {
                    Visio.Shape visioShape = shape.VisioShape;

                    if (visioShape != null)
                    {
                        shapeDictSet.Add(visioShape);
                    }
                }
            }

            HashSet<Visio.Shape> visioShapesSet = new HashSet<Visio.Shape>();

            foreach (Visio.Shape visioShape in page.VisioPage.Shapes)
            {
                visioShapesSet.Add(visioShape);
            }

            foreach (Visio.Shape visioShape in visioShapesSet)
            {
                if (!shapeDictSet.Contains(visioShape))
                {
                    errorList.Add("Shape " + VisioInterop.VisioShapeToString(visioShape) + " is in visio shapes but not ShapeDict");
                }
            }

            foreach (Visio.Shape visioShape in shapeDictSet)
            {
                if (!visioShapesSet.Contains(visioShape))
                {
                    errorList.Add("Shape " + VisioInterop.VisioShapeToString(visioShape) + " is in ShapeDict but not visio shapes");
                }
            }

            foreach (Visio.Shape visioShape in visioShapesSet)
            {
                if (!shapeDictSet.Contains(visioShape))
                {
                    errorList.Add("Shape " + VisioInterop.VisioShapeToString(visioShape) + " is in visio shapes but not in ShapeDict");
                }
            }

            return errorList;
        }

        private List<string> compareVisioLayersToLayerDict()
        {
            List<string> errorList = new List<string>();

            HashSet<Visio.Layer> graphicsLayerSet = new HashSet<Visio.Layer>();

            Dictionary<string, int> graphicsLayerCountDict = new Dictionary<string, int>();

            Dictionary<string, GraphicsLayer> graphicsLayerDict = new Dictionary<string, GraphicsLayer>();

            foreach (GraphicsLayer graphicsLayer in page.GraphicsLayers)
            {
                Visio.Layer visioLayer = graphicsLayer.visioLayer;

                if (visioLayer != null)
                {
                    graphicsLayerSet.Add(visioLayer);
                }

                graphicsLayerDict.Add(graphicsLayer.Guid, graphicsLayer);

                graphicsLayerCountDict.Add(graphicsLayer.LayerName, graphicsLayer.ShapeDictCount());
            }

            HashSet<Visio.Layer> visioLayerSet = new HashSet<Visio.Layer>();

            Dictionary<string, int> visioLayerCountDict = new Dictionary<string, int>();

            Dictionary<string, Visio.Layer> visioLayerDict = new Dictionary<string, Visio.Layer>();

            foreach (Visio.Layer visioLayer in page.VisioPage.Layers)
            {
                visioLayerSet.Add(visioLayer);

                visioLayerCountDict.Add(visioLayer.Name, 0);

                visioLayerDict.Add(visioLayer.Name, visioLayer);
            }

            foreach (Visio.Shape shape in this.page.VisioPage.Shapes)
            {
                for (short i = 1; i <= shape.LayerCount; i++)
                {
                    Visio.Layer layer = shape.Layer[i];

                    visioLayerCountDict[layer.Name]++;
                   
                }
            }

            foreach (Visio.Layer visioLayer in visioLayerSet)
            {
                if (!graphicsLayerSet.Contains(visioLayer))
                {
                    errorList.Add("Layer " + VisioInterop.VisioLayerToString(visioLayer) + " is in visio layers but not graphics layer dict.");
                }
            }

            foreach (Visio.Layer visioLayer in graphicsLayerSet)
            {
                if (!visioLayerSet.Contains(visioLayer))
                {
                    errorList.Add("Layer " + VisioInterop.VisioLayerToString(visioLayer) + " is in GuidLayerDict but not visio layers");
                }
            }

            foreach (Visio.Layer visioLayer in graphicsLayerSet.Intersect(visioLayerSet))
            {
                int graphicsLayerCount = graphicsLayerCountDict[visioLayer.Name];
                int visioLayerCount = visioLayerCountDict[visioLayer.Name];

                if (graphicsLayerCount != visioLayerCount)
                {
                    errorList.Add("Layer " + visioLayer.Name + " count mismatch: Graphics layer count =" + graphicsLayerCount + ", visioLayerCount=" + visioLayerCount + ".");
                }
            }

            foreach (GraphicsLayer graphicsLayer in page.GraphicsLayers)
            {
                Visio.Layer visioLayer = graphicsLayer.visioLayer;

                if (visioLayerSet.Contains(visioLayer))
                {
                    if (graphicsLayer.Visibility != VisioInterop.GetLayerVisibility(visioLayer))
                    {
                        errorList.Add(
                            "Layer "+
                            visioLayer.Name +
                            " visibility mismatch: Graphics layer visibility =" +
                            graphicsLayer.Visibility.ToString() +
                            ", visioLayerCount=" +
                             VisioInterop.GetLayerVisibility(visioLayer).ToString() +
                             ".");
                    }
                }
            }

            return errorList;
        }

        private List<string> CanvasLayoutInconsistencies()
        {
            List<string> errorList = new List<string>();

            foreach (CanvasLayoutArea canvasLayoutArea in this.canvasPage.LayoutAreas)
            {
                if (canvasLayoutArea.Shape is null)
                {
                    errorList.Add("Null shape in canvas layout area " + FormatUtils.FormatGuidAbbreviation(canvasLayoutArea.Guid) + ".");
                }

                //else
                //{
                //    if (canvasLayoutArea.Guid != canvasLayoutArea.Shape.Guid)
                //    {
                //        errorList.Add("Canvas layout area and canvas layout area shape guid inconsistency: "
                //            + "Canvas layout area guid = '" + FormatUtils.FormatGuidAbbreviation(canvasLayoutArea.Guid) + "', "
                //            + "Canvas layout area shape guid = '" + FormatUtils.FormatGuidAbbreviation(canvasLayoutArea.Shape.Guid) + "'.");
                //    }
                //}

                if (canvasLayoutArea.ExternalArea is null)
                {
                    errorList.Add("Null external area in canvas layout area " + FormatUtils.FormatGuidAbbreviation(canvasLayoutArea.Guid) + ".");
                }

                //else
                //{
                //    if (canvasLayoutArea.Guid == canvasLayoutArea.ExternalArea.Guid)
                //    {
                //        errorList.Add("Canvas layout area and external shape guid inconsistency, guids are the same: "
                //            + "Canvas layout area guid = '" + FormatUtils.FormatGuidAbbreviation(canvasLayoutArea.Guid) + ".");
                //    }
                //}

                foreach (CanvasDirectedPolygon internalArea in canvasLayoutArea.InternalAreas)
                {
                    if (canvasLayoutArea.Guid == internalArea.Guid)
                    {
                        errorList.Add("Canvas layout area and internal area guid inconsistency, guids are the same: "
                            + "Canvas layout area guid = '" + FormatUtils.FormatGuidAbbreviation(canvasLayoutArea.Guid) + ".");
                    }
                }
            }

            return errorList;
        }
    }
}
