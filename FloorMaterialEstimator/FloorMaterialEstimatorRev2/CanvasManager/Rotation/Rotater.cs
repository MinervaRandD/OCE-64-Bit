using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

using Visio = Microsoft.Office.Interop.Visio;
using Microsoft.Office.Interop.Visio;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using Graphics;
using Geometry;
using MaterialsLayout;
using AxMicrosoft.Office.Interop.VisOcx;
using System.IO;

namespace FloorMaterialEstimator.CanvasManager
{
    public class Rotater
    {
        private GraphicsPage currentPage = null;

        List<GraphicShape> lineList = null;

        List<GraphicShape> lockedShapes = null;

        List<GraphicsLayer> layerList = null;

        List<GraphicsLayer> lockedLayers = null;

        List<GraphicShape> seamList = null;

        Dictionary<string, GraphicShape> graphicShapeDictFromCanvasLayouts = null;
        Dictionary<string, GraphicShape> graphicShapeDictFromLayers = null;
        Dictionary<string, GraphicShape> graphicShapeDictFromPage = null;


        Dictionary<string, GraphicShape> layerShapeDictionary = null;

        Dictionary<string, Visio.Shape> pageShapeDictionary = null;

        double degrees = 0;

        private List<string> rotatableLayers = new List<string>()
        {
            "[AreaMode:AreaFinishLabelLayer]",
            "[AreaMode:AreaPerimeterLayer]",
            "[AreaMode:RemnantSeamDesignStateLayer]",
            "[AreaMode:SeamDesignStateLayer]",
             "[LineMode:SeamDesignStateLayer]",
            "[AreaMode:SeamStateLockLayer]",
            "[OversIndexLayer]",
            "[UndrsIndexLayer]",
            "[CutsIndexLayer]",
            "[CutsLayer]",
            "[EmbdOverLayer]",
            "[LineMode:LineDesignStateLayer]",
            "[ManualSeamsAllLayer]",
            "[NormalSeamsLayer]",
            "[NormalSeamsUnhideableLayer]",
            "[OversLayer]",
            "[UndrsLayer]",
            "[AreaMode:AreaDesignStateLayer]",
            "[LineMode:AreaDesignStateLayer]",
            "[AreaMode:AreaPerimeterLayer]",
            "[AreaMode:SeamDesignStateLayer]",
            "[BoundaryLayer]",
            "[CounterController]",
            "[CutsIndexLayer]",
            "[CutsLayer]",
            "[NormalSeamsLayer]",
            "[NormalSeamsUnhideableLayer]",
            "[UndrsLayer]",
            "[Worklayer]"
        };

        private HashSet<ShapeType> rotatableShapes = new HashSet<ShapeType>()
        {
            ShapeType.Line,
            ShapeType.Circle,
            ShapeType.Rectangle,
            ShapeType.Polyline,
            ShapeType.Polygon,
            ShapeType.LayoutArea,
            ShapeType.TextBox,
            ShapeType.Image,
            ShapeType.CutIndex,
            ShapeType.OverageIndex,
            ShapeType.UndrageIndex,
            ShapeType.LayoutAreaShape,
            ShapeType.SeamTag
        };

        public Rotater(CanvasPage currentPage)
        {
            this.currentPage = currentPage;
        }

        public void DoRotate(double degrees)
        {
            if (degrees == 0)
            {
                return;
            }

            this.degrees = degrees;

            collectGraphicLayers();
            collectGraphicShapes();
            
            generateSelectionAndRotate();

            syncRotatedShapesWithCanvas();

            relockShapes();
            relockLayers();
        }

        private void collectGraphicLayers()
        {
            layerList = new List<GraphicsLayer>();

            lockedLayers = new List<GraphicsLayer>();

            foreach (GraphicsLayer layer in currentPage.GraphicsLayers)
            {
                if (layer.GraphicsLayerType == GraphicsLayerType.Unknown)
                {
                    Console.WriteLine("Unknown layer: " + layer.LayerName);
                    
                }
                if (!isRotatableLayer(layer.LayerName))
                {
                    continue;
                }

                layerList.Add(layer);

                if (layer.IsLocked)
                {
                    lockedLayers.Add(layer);

                    layer.Unlock();
                }

            }


            if (currentPage.DrawingLayer != null)
            {
                currentPage.DrawingLayer.UnLock();

            }
        }

        private bool isRotatableLayer(string layerName)
        {
            bool rtrnValue =  rotatableLayers.Any(s => layerName.StartsWith(s));

            return rtrnValue;
        }

        private bool isRotatableGraphicShape(GraphicShape shape) => rotatableShapes.Contains(shape.ShapeType);


        private void collectGraphicShapes()
        {

            lineList = new List<GraphicShape>();

            lockedShapes = new List<GraphicShape>();

            generateCompositeShapeDictionaries();

        }

        private void generateCompositeShapeDictionaries()
        {
            layerShapeDictionary = new Dictionary<string, GraphicShape>();

            foreach (GraphicsLayer layer in layerList)
            {
                foreach (IGraphicsShape iShape in layer.Shapes)
                {
                    GraphicShape shape = (GraphicShape)iShape.Shape;

                    if (shape is null)
                    {
                        continue;
                    }

                    if (layerShapeDictionary.ContainsKey(shape.Guid))
                    {
                        continue;
                    }

                    if (shape.ShapeType == ShapeType.Line)
                    {
                        try
                        {
                            lineList.Add(shape);           
                        }

                        catch
                        {
                            ;
                        }
                    }

                    else
                    {
                        Console.WriteLine("Nonline Shape: " + shape.ToString());
                    }

                    if (shape.IsLocked)
                    {
                        lockedShapes.Add(shape);
                        shape.Unlock();
                    }

                    layerShapeDictionary.Add(shape.Guid, shape);
                }
            }
#if false
            foreach (Visio.Shape shape in currentPage.VisioPage.Shapes)
            {
                if (shape.Data2.Contains("Legend"))
                {
                    continue;
                }

                if (shape.Data2.Contains("Rectangle"))
                {
                    continue;
                }

                if (shape.Data1.Contains("Crosshairs"))
                {
                    continue;
                }

                if (shape.Data1.Contains("Associated") || shape.Data1.Contains("Perimeter"))
                {
                    pageShapeDictionary.Add(shape.Data3, shape);
                }
            }

            Console.WriteLine("Layer Shapes:");

            foreach (KeyValuePair<string, Visio.Shape> keyValuePair in layerShapeDictionary)
            {
                string key = keyValuePair.Key;
                Visio.Shape shape = keyValuePair.Value;

                Console.WriteLine("    " + key + ": [" + shape.Data1 + ", " + shape.Data2 + ", " + shape.Data3 + "]");
            }

            Console.WriteLine("\nPage Shapes:");

            foreach (KeyValuePair<string, Visio.Shape> keyValuePair in pageShapeDictionary)
            {
                string key = keyValuePair.Key;
                Visio.Shape shape = keyValuePair.Value;

                Console.WriteLine("    " + key + ": [" + shape.Data1 + ", " + shape.Data2 + ", " + shape.Data3 + "]");
            }

            Console.WriteLine("\nMissing shapes:");

            List<GraphicShape> shapesToAdd = new List<GraphicShape>();

            foreach (var kvp in pageShapeDictionary)
            {
                string key = kvp.Key;

                var shape = kvp.Value;

                if (!layerShapeDictionary.ContainsKey(key))
                {
                    Console.WriteLine("    " + key + ": [" + shape.Data1 + ", " + shape.Data2 + ", " + shape.Data3 + "]");

                    layerShapeDictionary.Add(key, shape);
                }

            }

            Console.WriteLine("\nPage Shape Dictionary:");

            foreach (var kvp in currentPage.PageShapeDict)
            {
                string key = kvp.Key;

                var shape = kvp.Value.Shape.VisioShape;

                Console.WriteLine("    " + key + ": [" + shape.Data1 + ", " + shape.Data2 + ", " + shape.Data3 + "]");

            }
#endif
        }

        private void generateSelectionAndRotate()
        {
            graphicShapeDictFromCanvasLayouts = GenerateGraphicShapeDictFromCanvasLayoutAreas();
            graphicShapeDictFromLayers = GenerateGraphicShapeDictFromLayers();
            graphicShapeDictFromPage = GenerateGraphicShapeDictFromPage();

            List<Visio.Shape> visioShapeList = new List<Visio.Shape>();
            
            foreach (GraphicShape shape in layerShapeDictionary.Values)
            {
                if (shape.VisioShape is null)
                {
                    continue;
                }

                // MDD Reset

                //string data1 = shape.Data1;

                //if (data1.Contains("Layout"))
                //{
                //    continue;
                //}

                visioShapeList.Add(shape.VisioShape);
            }

#if true
            foreach (var layer in lockedLayers)
            {
                layer.Unlock();
            }

            foreach (var shape in lockedShapes)
            {
                shape.Unlock();
            }

            //foreach (GraphicsLayer layer in  layerList)
            //{
            //    if (layer.IsLocked)
            //    {
            //        ;
            //    }
            //}

            Visio.Selection selection = null;

            if (visioShapeList.Count > 0)
            {
                selection = visioShapeList[0].CreateSelection(VisSelectionTypes.visSelTypeEmpty);

                for (int i = 0; i < visioShapeList.Count; i++)
                {
                    selection.Select(visioShapeList[i], (short)Visio.VisSelectArgs.visSelect);
                }

                if (currentPage.Drawing != null)
                {
                    if (currentPage.Drawing.VisioShape != null)
                    {
                        selection.Select(currentPage.Drawing.VisioShape, (short)Visio.VisSelectArgs.visSelect);
                    }
                }
            }

            else if(currentPage.Drawing != null)
            {
                if (currentPage.Drawing.VisioShape != null)
                {
                    selection = currentPage.Drawing.VisioShape.CreateSelection(VisSelectionTypes.visSelTypeEmpty);
                    selection.Select(currentPage.Drawing.VisioShape, (short)Visio.VisSelectArgs.visSelect);
                }
            }

            DebugSetup();

            DebugPrintShapeList("Pre-rotate shape list:");

#endif
            selection.Rotate(degrees,Visio.VisUnitCodes.visDegrees,false,VisRotationTypes.visRotateSelection);

            selection.DeselectAll();

            //foreach (var layer in lockedLayers)
            //{
            //    layer.Lock();
            //}

            //foreach (var shape in lockedShapes)
            //{
            //    shape.Lock();
            //}

            selection = null;


            DebugPrintShapeList("Post-rotate shape list:");

            DebugComplete();
        }


        private void syncRotatedShapesWithCanvas()
        {
            foreach (GraphicShape graphicsShape in layerShapeDictionary.Values)
            {
                if (graphicsShape.VisioShape is null)
                {
                    continue;
                }

                // MDD Reset 2024-12-30

                string data1 = graphicsShape.Data1;

                graphicsShape.SyncWithCanvas();
            }
        }
        #region Debug

        List<string> outpLines;

        private void DebugSetup()
        {
            if (File.Exists(@"C:\Temp\Debug1.txt"))
            {
                File.Delete(@"C:\Temp\Debug1.txt");
            }

            outpLines = new List<string>();
        }

        private void DebugComplete()
        {
            StreamWriter sw = new StreamWriter(@"C:\Temp\Debug1.txt");

            foreach (string outputLine in outpLines)
            {
                sw.WriteLine(outputLine);
            }

            sw.Flush();
            sw.Close();
        }
        private void DebugPrintShapeList(string header)
        {
            outpLines.Add("\n" + header + "\n");

            DebugPrintShapeList(this.graphicShapeDictFromCanvasLayouts.Values, "Canvas Layout Shapes", outpLines);

            DebugPrintShapeList(this.graphicShapeDictFromLayers.Values, "Layers Shapes", outpLines);

        }

        private void DebugPrintShapeList(IEnumerable<GraphicShape> shapeList, string header, List<string> outpLines)
        {
            outpLines.Add("\n" + header + "\n");

            foreach (GraphicShape graphicShape in this.graphicShapeDictFromCanvasLayouts.Values)
            {
                if (graphicShape.VisioShape is null)
                {
                    continue;
                }

                if (graphicShape.ShapeType == ShapeType.Line)
                {
                    outpLines.Add("Line: " + generateLineSpecs(graphicShape));
                }

                else
                {
                    outpLines.Add("Shape: " + generateShapeSpecs(graphicShape));
                }
                
            }
        }

        private string generateLineSpecs(GraphicShape graphicShape)
        {
            string rtrnStr = string.Empty;

            Coordinate vShapeBeginCoord = VisioInterop.GetLineStartpoint(graphicShape);
            Coordinate vShapeEndCoord = VisioInterop.GetLineEndpoint(graphicShape);

            if (graphicShape.Data1 == "Associated Line" || graphicShape.Data1 == "External Perimeter")
            {
                Coordinate baseCoord1 = Coordinate.NullCoordinate;
                Coordinate baseCoord2 = Coordinate.NullCoordinate;

                if (currentPage.PageShapeDict.ContainsKey(graphicShape.Guid))
                {
                    GraphicShape shape = currentPage.PageShapeDict[graphicShape.Guid];

                    var x = shape.GetType();

                    GraphicsDirectedLine directedLine = (GraphicsDirectedLine) currentPage.PageShapeDict[graphicShape.Guid];

                    baseCoord1 = directedLine.Coord1;
                    baseCoord2 = directedLine.Coord2;
                }

                if (!Coordinate.IsNullCoordinate(baseCoord1) && !Coordinate.IsNullCoordinate(baseCoord2))
                {
                    if (vShapeBeginCoord != null && vShapeEndCoord != null)
                    {
                        rtrnStr = graphicShape.ToString() + " "
                                + "Line coords: [" + baseCoord1.ToString() + ", " + baseCoord2.ToString()
                                + "], visio coords: ["
                                + vShapeBeginCoord.ToString() + ", " + vShapeEndCoord.ToString() + "]";
                    }
                    else
                    {
                        rtrnStr = graphicShape.ToString() + " "
                                + "Line coords: [" + baseCoord1.ToString() + ", " + baseCoord2.ToString()
                                + "], visio coords: [(null, null), (null, null)";
                    }
                }

            }

            Coordinate lineCoord1 = graphicShape.UpperLeftLocation;
            Coordinate lineCoord2 = graphicShape.LowerRightLocation;

            if (vShapeBeginCoord != null && vShapeEndCoord != null)
            {
                rtrnStr = graphicShape.ToString() + " "
                        + "Line coords: [" + lineCoord1.ToString() + ", " + lineCoord2.ToString()
                        + "], visio coords: ["
                        + vShapeBeginCoord.ToString() + ", " + vShapeEndCoord.ToString() + "]";
            }
            else
            {
                rtrnStr = graphicShape.ToString() + " "
                        + "Line coords: [" + lineCoord1.ToString() + ", " + lineCoord2.ToString() + "], visio coords:"
                        + "[(null, null), (null, null)";
            }

            return rtrnStr;

        }

        private string generateShapeSpecs(GraphicShape graphicShape)
        {
            return graphicShape.ToString();
        }

        #endregion

        private Dictionary<string, GraphicShape> GenerateGraphicShapeDictFromLayers()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();
            
            foreach (GraphicsLayer layer in this.layerList)
            {
                foreach (var graphicShape in layer.Shapes)
                {
                    if (!rtrnDict.ContainsKey(graphicShape.Guid))
                    {
                        rtrnDict[graphicShape.Guid] = graphicShape.Shape;
                    }
                }
            }

            return rtrnDict;
        }

        private Dictionary<string, GraphicShape> GenerateGraphicShapeDictFromPage()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();

            foreach (GraphicShape graphicShape in currentPage.PageShapeDictValues)
            {
                Console.WriteLine("Page Graphic Shape: " + graphicShape.ToString());

                if (rtrnDict.ContainsKey(graphicShape.Guid))
                {
                    continue;
                }

                if (isRotatableGraphicShape(graphicShape))
                {
                    rtrnDict.Add(graphicShape.Guid, graphicShape);
                }
            }

            return rtrnDict;
        }


        private Dictionary<string, GraphicShape> GenerateGraphicShapeDictFromCanvasLayoutAreas()
        {
            Dictionary<string, GraphicShape> rtrnDict = new Dictionary<string, GraphicShape>();   

            foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
            {
                foreach (CanvasLayoutArea canvasLayoutArea in areaFinishManager.CanvasLayoutAreas)
                {
                    Dictionary<string, GraphicShape> layoutAreaShapeDict = canvasLayoutArea.GenerateGraphicShapeDict();

                    foreach (KeyValuePair<string, GraphicShape> kvp  in layoutAreaShapeDict)
                    {
                        if (!rtrnDict.ContainsKey((string)kvp.Key))
                        {
                            rtrnDict[kvp.Key] = kvp.Value;
                        }
                    }
                }
            }

            return rtrnDict;
        }

        private void relockShapes()
        {
            foreach (var shape in lockedShapes)
            {
                shape.Lock();
            }
        }

        private void relockLayers()
        {
            foreach (var layer in lockedLayers)
            {
                layer.Lock();
            }
        }

    }
}
