#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: Rollout.cs. Project: MaterialsLayout. Created: 9/2/2024         */
/*                                                                                                     */
/* Copyright (c) 2025, Minerva Research and Development, LLC. All rights reserved.                     */
/*                                                                                                     */
/* Not to be copied or distributed in any way without prior authorization. If provided with permission,*/
/* this software is provided without warranty of any kind, express or implied,                         */
/* including but not limited to the warranties of merchantability, fitness for a particular            */
/* purpose, and non-infringement. In no event shall the authors or copyright holders be liable         */
/* for any claim, damages, or other liability, whether in an action of contract, tort, or              */
/* otherwise, arising from, out of, or in connection with the software or the use or other             */
/* dealings in the software.                                                                           */
/*                                                                                                     */
/* Author: Marc Diamond, Minerva Research and Development, LLC                                         */
/*                                                                                                     */
/*******************************************************************************************************/
#endregion


namespace MaterialsLayout
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using FinishesLib;
    using Geometry;
    using Graphics;
    using SettingsLib;
    using Utilities;

    public class Rollout
    {
        #region Key Elements

        // Pointer to parent object, ParentGraphicsRollout. But in the future ParentGraphicsRollout should inheret from rollout.

        private GraphicsRollout _graphicsRollout = null;

        public GraphicsRollout GraphicsRollout
        {
            get
            {
                if (_graphicsRollout == null)
                {
                    throw new NotImplementedException();
                }

                return _graphicsRollout;
            }

            set
            {
                if (_graphicsRollout == value)
                {
                    return;
                }

                _graphicsRollout = value;
            }
        }

        private FinishesLibElements _finishesLibElements => GraphicsRollout.FinishesLibElements;

        public FinishesLibElements FinishesLibElements
        {
            get
            {
                if (_finishesLibElements == null)
                {
                    throw new NotImplementedException();
                }

                return _finishesLibElements;
            }

            //set
            //{
            //    if (_finishesLibElements == value)
            //    {
            //        return;
            //    }
            //}
        }


        #endregion

        public GraphicsWindow Window => GraphicsRollout.Window;

        public GraphicsPage Page => GraphicsRollout.Page;


        #region Constructors and Cloners

        public Rollout() { }

        public Rollout(
            GraphicsRollout parentArea
            , FinishesLibElements finishesLibElements
            , Rectangle rolloutRectangle
            , double seamWidth
            , double materiaWidth
            , double materialOverlap)
        {
            this.RolloutRectangle = rolloutRectangle;

            this.SeamWidth = seamWidth;

            this.MaterialWidth = materiaWidth;

            this.MaterialOverlap = materialOverlap;

            //FinishesLibElements = finishesLibElements;

            this.GraphicsRollout = parentArea;
        }

        #endregion

        internal Rollout Clone()
        {
            Rollout clonedRollout = new Rollout(this.GraphicsRollout, FinishesLibElements, RolloutRectangle, SeamWidth, MaterialWidth, MaterialOverlap);

            return clonedRollout;
        }

        public List<GraphicsCut> GraphicsCutList { get; set; } = new List<GraphicsCut>();

        public List<Overage> EmbeddedOverageList = new List<Overage>();

        public double SeamWidth { get; set; }

        public double MaterialWidth { get; set; }

        public double MaterialOverlap { get; set; }

        public Rectangle RolloutRectangle { get; set; }

        public double RolloutAngle { get; set; }


        [XmlIgnore]
        public List<Tuple<Rectangle, LayoutArea>> BoundingBoxLayoutAreaList { get; } = new List<Tuple<Rectangle, LayoutArea>>();

        public List<Overage> OverageList
        {
            get
            {
                List<Overage> overageList = new List<Overage>();

                GraphicsCutList.ForEach(c => overageList.AddRange(c.OverageList));

                return overageList;
            }
        }

        public List<GraphicsUndrage> GraphicsUndrageList {
            get; set;
        } = new List<GraphicsUndrage>();

        private LayoutArea originalLayoutArea = null;

        private List<GraphicsLayoutArea> boundedLayoutAreas = null;

        private List<DirectedPolygon> internalAreaList = new List<DirectedPolygon>();

        private List<DirectedPolygon> externalAreaList = new List<DirectedPolygon>();

        public bool[,] FilledRectangle;
        public Rectangle[,] InternalRectangle;
        public int?[,] GroupNumber;

        public int xPnts;
        public int yPnts;

        public int xRecs;
        public int yRecs;

        public Dictionary<int, List<Tuple<int, int>>> RectangleGroups = new Dictionary<int, List<Tuple<int, int>>>();

        public Dictionary<int, LayoutArea> LayoutAreaDict = new Dictionary<int, LayoutArea>();

        public void GenerateCutsOveragesAndUndrages(
            GraphicsWindow window
            , GraphicsPage page
            , FinishesLibElements finishesLibElements
            , double theta
            , LayoutArea originalLayoutArea
            , double drawingScaleInInches
            , bool generateEmbeddedCuts = false
            , bool generateEmbeddedOvers = false)
        {
            this.originalLayoutArea = originalLayoutArea;

            boundedLayoutAreas = generateBoundedLayoutAreas();

            BoundingBoxLayoutAreaList.AddRange(generateBoundingBoxLayoutAreaList());

            foreach (Tuple<Rectangle, LayoutArea> boundingBoxLayoutArea in BoundingBoxLayoutAreaList)
            {
                generateCutsOveragesAndUndrages(
                    window
                    , page
                    , theta
                    , boundingBoxLayoutArea
                    , drawingScaleInInches);
            }

            GraphicsCutList.Sort((c1, c2) => Math.Sign(c1.CutRectangle.UpperRght.X - c2.CutRectangle.UpperRght.X));

            mergeFinalCuts();

            if (generateEmbeddedCuts)
            {
                foreach (Cut cut in GraphicsCutList)
                {
                    foreach (DirectedPolygon directedPolygon in cut.CutPolygonList)
                    {
                        List<DirectedPolygon> remnantPolygonList = directedPolygon.Intersect(originalLayoutArea.ExternalArea);

                        foreach (DirectedPolygon remnantPolygon in remnantPolygonList)
                        {
                            RemnantCut remnantCut = generateRemnantCut(cut, remnantPolygon);

                            if (Utilities.IsNotNull(remnantCut))
                            {
                                cut.RemnantCutList.Add(remnantCut);
                            }
                        }
                    }
                }
            }

            if (generateEmbeddedOvers)
            {

                EmbeddedOverageList.Clear();

                foreach (Cut cut in GraphicsCutList)
                {
                    foreach (DirectedPolygon directedPolygon in cut.CutPolygonList)
                    {
                        List<DirectedPolygon> oversPolygonList = directedPolygon.Intersect(originalLayoutArea.ExternalArea);

                        foreach (DirectedPolygon overPolygon in oversPolygonList)
                        {
                            Overage embeddedOverage = generateEmbeddedOverage(cut, overPolygon);

                            if (Utilities.IsNotNull(embeddedOverage))
                            {
                                embeddedOverage.IsEmbeddedOverage = true;
                                EmbeddedOverageList.Add(embeddedOverage);
                            }
                        }

                    }
                }

            }
        }

        private void mergeFinalCuts()
        {
            bool mergesFound = true;

            while (mergesFound)
            {
                mergesFound = false;

                int iStart = 0;

                for (int i = iStart; i < GraphicsCutList.Count - 1; i++)
                {
                    GraphicsCut mergedCut = mergedCuts(GraphicsCutList[i], GraphicsCutList[i + 1]);

                    if (!(mergedCut is null))
                    {
                        GraphicsCutList.RemoveAt(i + 1);
                        GraphicsCutList[i] = mergedCut;

                        iStart = i;

                        mergesFound = true;

                        break;
                    }
                }
            }
        }

        private GraphicsCut mergedCuts(GraphicsCut cut1, GraphicsCut cut2)
        {
            if (cut1.CutRectangle.LowerRght.X >= cut2.CutRectangle.LowerLeft.X
                && cut1.CutAngle == cut2.CutAngle && cut1.ParentRollout == cut2.ParentRollout)
            {
                cut1.OverageList.AddRange(cut2.OverageList);
                cut1.CutPolygonList.AddRange(cut2.CutPolygonList);

                cut1.CutRectangle = new Rectangle(cut1.CutRectangle.UpperLeft, cut2.CutRectangle.LowerRght);

                return cut1;
            }

            return null;
        }

        private RemnantCut generateRemnantCut(Cut cut, DirectedPolygon embeddedPolygon)
        {
            double yMax = cut.CutRectangle.UpperLeft.Y;
            double yMin = cut.CutRectangle.LowerLeft.Y;

            double minXatMaxY = double.MaxValue;
            double maxXatMaxY = double.MinValue;

            double minXatMinY = double.MaxValue;
            double maxXatMinY = double.MinValue;

            List<Coordinate> coordinateList = embeddedPolygon.CoordinateList();

            foreach (Coordinate coord in coordinateList)
            {
                if (Math.Abs(coord.Y - yMin) < 1.0e-3)
                {
                    if (coord.X < minXatMinY)
                    {
                        minXatMinY = coord.X;
                    }

                    if (coord.X > maxXatMinY)
                    {
                        maxXatMinY = coord.X;
                    }
                }

                else if (Math.Abs(coord.Y - yMax) < 1.0e-3)
                {
                    if (coord.X < minXatMaxY)
                    {
                        minXatMaxY = coord.X;
                    }

                    if (coord.X > maxXatMaxY)
                    {
                        maxXatMaxY = coord.X;
                    }
                }
            }

            double xMin = Math.Max(minXatMinY, minXatMaxY);
            double xMax = Math.Min(maxXatMinY, maxXatMaxY);

            if (xMax - xMin <= 0.1)
            {
                return null;
            }

            Coordinate upperRght = new Coordinate(xMax, yMax);
            Coordinate lowerRght = new Coordinate(xMax, yMin);
            Coordinate upperLeft = new Coordinate(xMin, yMax);
            Coordinate lowerLeft = new Coordinate(xMin, yMin);


            RemnantCut remnantCut = new RemnantCut(upperRght, lowerRght, upperLeft, lowerLeft);

            return remnantCut;
        }

        private Overage generateEmbeddedOverage(Cut cut, DirectedPolygon embeddedPolygon)
        {
            double yMax = cut.CutRectangle.UpperLeft.Y;
            double yMin = cut.CutRectangle.LowerLeft.Y;

            double minXatMaxY = double.MaxValue;
            double maxXatMaxY = double.MinValue;

            double minXatMinY = double.MaxValue;
            double maxXatMinY = double.MinValue;

            List<Coordinate> coordinateList = embeddedPolygon.CoordinateList();

            foreach (Coordinate coord in coordinateList)
            {
                if (Math.Abs(coord.Y - yMin) < 1.0e-3)
                {
                    if (coord.X < minXatMinY)
                    {
                        minXatMinY = coord.X;
                    }

                    if (coord.X > maxXatMinY)
                    {
                        maxXatMinY = coord.X;
                    }
                }

                else if (Math.Abs(coord.Y - yMax) < 1.0e-3)
                {
                    if (coord.X < minXatMaxY)
                    {
                        minXatMaxY = coord.X;
                    }

                    if (coord.X > maxXatMaxY)
                    {
                        maxXatMaxY = coord.X;
                    }
                }
            }

            double xMin = Math.Max(minXatMinY, minXatMaxY);
            double xMax = Math.Min(maxXatMinY, maxXatMaxY);

            if (xMax - xMin <= 0.1)
            {
                return null;
            }

            //Coordinate upperRght = new Coordinate(xMax, yMax);
            Coordinate lowerRght = new Coordinate(xMax, yMin);
            Coordinate upperLeft = new Coordinate(xMin, yMax);
            //Coordinate lowerLeft = new Coordinate(xMin, yMin);

            Rectangle overageRectangle = new Rectangle(upperLeft, lowerRght);

            Overage embeddedOverage = new Overage(overageRectangle);

            embeddedOverage.EffectiveDimensions = new Tuple<double, double>(overageRectangle.Width, overageRectangle.Height);
            return embeddedOverage;
        }

        private void generateCutsOveragesAndUndrages(
            GraphicsWindow window
            , GraphicsPage page
            , double theta
            , Tuple<Rectangle, LayoutArea> boundingBoxLayoutArea
            , double drawingScaleInInches)
        {
            Rectangle boundingBox = boundingBoxLayoutArea.Item1;
            LayoutArea layoutArea = boundingBoxLayoutArea.Item2;

            // Check for the simple case, which will cover 80% of the cases presented.

            if (layoutArea.InternalAreas.Count <= 0)
            {
                if ((DirectedPolygon)boundingBox == layoutArea.ExternalArea)
                {
                    GraphicsCut graphicsCut = new GraphicsCut(this.GraphicsRollout, window, page, this.FinishesLibElements)
                    {
                       ParentRollout = this
                       , CutRectangle = boundingBox
                       , CutPolygonList = new List<DirectedPolygon>() { layoutArea.ExternalArea }
                       , SeamWidth = SeamWidth
                       , MaterialWidth = MaterialWidth
                       ,MaterialOverlap = MaterialOverlap
                       , CutAngle = theta
                    };
                   

                    this.GraphicsCutList.Add(graphicsCut);

                    return;
                }
            }

            // Generate a list of x points and y points.

            HashSet<double> xVals = new HashSet<double>();
            HashSet<double> yVals = new HashSet<double>();

            generateXYPoints(boundingBox, layoutArea, xVals, yVals);

            xPnts = xVals.Count;
            yPnts = yVals.Count;

            xRecs = xPnts - 1;
            yRecs = yPnts - 1;

            generateInternalRectangles(xVals, yVals, layoutArea);
            generateRectangleGroups();

            generateBoundaries();

            foreach (LayoutArea initialLayout in LayoutAreaDict.Values)
            {
                if (initialLayout.ExternalArea is null)
                {
                    continue;
                }

                List<Rectangle> undrageBoundingRectangles = new List<Rectangle>();
                List<Rectangle> overageBoundingRectangles = new List<Rectangle>();

                generateMergedCutAndUndrageAreas(initialLayout.ExternalArea, overageBoundingRectangles, undrageBoundingRectangles, boundingBox, drawingScaleInInches);

                foreach (Rectangle overageBoundingArea in overageBoundingRectangles)
                {
                    if (overageBoundingArea.AreaInSqrInches(1) <= 1e-4)
                    {
                        continue;
                    }

                    // Check here for issue.

                    generateCutAndOverages(
                        window
                        , page
                        , theta
                        , overageBoundingArea
                        , initialLayout
                        , drawingScaleInInches);
                }

                foreach (Rectangle undrageBoundingArea in undrageBoundingRectangles)
                {
                    if (undrageBoundingArea.AreaInSqrInches(1) <= 1e-4)
                    {
                        continue;
                    }

                    generateUnderages(undrageBoundingArea, initialLayout, drawingScaleInInches);
                }

            }
        }

        // MDD Debug remove drawing scale in inches
        private void generateMergedCutAndUndrageAreas(DirectedPolygon externalArea, List<Rectangle> overageBoundingRectangles, List<Rectangle> undrageBoundingRectangles, Rectangle boundingBox, double drawingScaleInInches)
        {
            double rolloutWidth = boundingBox.Height;

            double rolloutWidthLimit = rolloutWidth * 0.5;

            // MDD Debug

            var rw = rolloutWidth * drawingScaleInInches / 12.0;
            var rl = rolloutWidthLimit * drawingScaleInInches / 12.0;

            List<Rectangle> rectangleList = externalArea.ToHorizontalRectangleList(1.0e-3);

            List<DoublePair> overageXBoundList = new List<DoublePair>();
            List<DoublePair> undrageXBoundList = new List<DoublePair>();

            rectangleList.Sort((r1, r2) => (int)Math.Sign(r1.LowerLeft.X - r2.LowerLeft.X));

            foreach (Rectangle rectangle in rectangleList)
            {
                //double yMax = rectangle.UpperLeft.Y;
                //double yMin = rectangle.LowerLeft.Y;

                if (rectangle.Height >= rolloutWidthLimit)
                {
                    mergeToXBoundList(rectangle, overageXBoundList);
                }

                else
                {
                    mergeToXBoundList(rectangle, undrageXBoundList);
                }
            }

            foreach (DoublePair xBounds in overageXBoundList)
            {
                Coordinate upprLeft = new Coordinate(xBounds.Val1, boundingBox.UpperLeft.Y); // new Coordinate(Math.Round(xBounds.Val1, 4), Math.Round(boundingBox.UpperLeft.Y, 4));
                Coordinate lowrRght = new Coordinate(xBounds.Val2, boundingBox.LowerLeft.Y); //new Coordinate(Math.Round(xBounds.Val2, 4), Math.Round(boundingBox.LowerLeft.Y, 4));


                overageBoundingRectangles.Add(new Rectangle(upprLeft, lowrRght));
            }

            foreach (DoublePair xBounds in undrageXBoundList)
            {
                Coordinate upprLeft = new Coordinate(xBounds.Val1, boundingBox.UpperLeft.Y); //new Coordinate(Math.Round(xBounds.Val1, 4), Math.Round(boundingBox.UpperLeft.Y, 4));
                Coordinate lowrRght = new Coordinate(xBounds.Val2, boundingBox.LowerLeft.Y); //new Coordinate(Math.Round(xBounds.Val2, 4), Math.Round(boundingBox.LowerLeft.Y, 4));

                undrageBoundingRectangles.Add(new Rectangle(upprLeft, lowrRght));
            }
        }

        public double CutsAreaInSqrInches(double drawingScaleInInches)
        {
            double cutsAreaInSqrInches = 0;

            foreach (Cut cut in this.GraphicsCutList)
            {
                cutsAreaInSqrInches += cut.CutAreaInSqrInches(drawingScaleInInches);
            }

            return cutsAreaInSqrInches;
        }

        private void mergeToXBoundList(Rectangle rectangle, List<DoublePair> xBoundList)
        {
            if (xBoundList.Count <= 0)
            {
                xBoundList.Add(new DoublePair(rectangle.LowerLeft.X, rectangle.LowerRght.X));

                return;
            }

            DoublePair lastXBoundElem = xBoundList.Last();

            if (lastXBoundElem.Val2 < rectangle.LowerLeft.X)
            {
                xBoundList.Add(new DoublePair(rectangle.LowerLeft.X, rectangle.LowerRght.X));

                return;
            }

            lastXBoundElem.Val2 = rectangle.LowerRght.X;

        }

        private void generateCutAndOverages(
            GraphicsWindow window
            , GraphicsPage page
            , double theta
            , Rectangle boundingBox
            , LayoutArea layoutArea
            , double drawingScaleInInches)
        {
            double minOverLgthInLocalUnits = GlobalSettings.MinOverageLengthInInches / drawingScaleInInches;
            double minOverWdthInLocalUnits = GlobalSettings.MinOverageWidthInInches / drawingScaleInInches;

            double minX = layoutArea.MinX;
            double maxX = layoutArea.MaxX;

            double minY = layoutArea.MinY;
            double maxY = layoutArea.MaxY;

            GraphicsCut graphicsCut = new GraphicsCut(this.GraphicsRollout, window, page, this.GraphicsRollout.FinishesLibElements)
            {
                ParentRollout = this
                , CutRectangle = boundingBox
                , CutPolygonList = new List<DirectedPolygon>() { layoutArea.ExternalArea }
                , SeamWidth = SeamWidth
                , MaterialWidth = MaterialWidth
                , MaterialOverlap = MaterialOverlap
                , CutAngle = theta
            };
            
            this.GraphicsCutList.Add(graphicsCut);

            // MDD Debug

            var x1 = boundingBox.Height * drawingScaleInInches / 12.0;
            var x2 = (maxY - minY) * drawingScaleInInches / 12.0;

            List<DirectedPolygon> overagePolygonList = ((DirectedPolygon)boundingBox).Subtract(layoutArea.ExternalArea);

            overagePolygonList.AddRange(layoutArea.InternalAreas);

            FinishesLibElements finishesLibElements = ((GraphicsLayoutArea)layoutArea).FinishLibElements;

            foreach (DirectedPolygon directedPolygon in overagePolygonList)
            {
                List<Rectangle> overageRectangleList = directedPolygon.ToHorizontalRectangleList(1.0e-3);

                foreach (Rectangle overageRectangle in overageRectangleList)
                {
                    GraphicsOverage graphicsOverage = new GraphicsOverage(
                        graphicsCut
                        , window
                        , page
                        , finishesLibElements
                        , overageRectangle
                       );
                  

                    graphicsOverage.EffectiveDimensions =
                        new Tuple<double, double>(overageRectangle.LowerRght.X - overageRectangle.LowerLeft.X, overageRectangle.UpperLeft.Y - overageRectangle.LowerLeft.Y);

                    if (graphicsOverage.EffectiveDimensions.Item1 <= minOverLgthInLocalUnits || graphicsOverage.EffectiveDimensions.Item2 <= minOverWdthInLocalUnits)
                    {
                        continue;
                    }

                    graphicsCut.OverageList.Add(graphicsOverage);
                }
            }
        }

        private void generateUnderages(Rectangle boundingBox, LayoutArea layoutArea, double drawingScaleInInches)
        {

            double minUndrLgthInLocalUnits = GlobalSettings.MinUnderageLengthInInches / drawingScaleInInches;
            double minUndrWdthInLocalUnits = GlobalSettings.MinUnderageWidthInInches / drawingScaleInInches;

            List<DirectedPolygon> undragePolygonList = ((DirectedPolygon)boundingBox).Intersect(layoutArea.ExternalArea);

            List<Rectangle> undrageRectangleList = new List<Rectangle>();

            foreach (DirectedPolygon undragePolygon in undragePolygonList)
            {
                undrageRectangleList.AddRange(undragePolygon.ToHorizontalRectangleList(1.0e-3));
            }

            foreach (Rectangle undrageRectangle in undrageRectangleList)
            {
                FinishesLibElements finishesLibElements = this.FinishesLibElements;

                GraphicsUndrage undrage = new GraphicsUndrage((GraphicsRollout)this, Window, Page, finishesLibElements, undrageRectangle)
                {
                    MaterialWidth = MaterialWidth,
                    MaterialOverlap = MaterialOverlap
                };

                undrage.EffectiveDimensions =
                    new Tuple<double, double>(undrageRectangle.Width, undrageRectangle.Height);

                if (undrage.EffectiveDimensions.Item1 <= minUndrLgthInLocalUnits || undrage.EffectiveDimensions.Item2 <= minUndrWdthInLocalUnits)
                {
                    continue;
                }

                this.GraphicsUndrageList.Add(undrage);
            }
        }

        private void generateBoundaries()
        {
            foreach (KeyValuePair<int, List<Tuple<int, int>>> kvp in RectangleGroups)
            {
                GraphicsLayoutArea graphicsLayoutArea = generateLayoutArea(kvp.Value);

                if (graphicsLayoutArea == null)
                {
                    continue;
                }

                LayoutAreaDict[kvp.Key] = graphicsLayoutArea;
            }
        }

        private GraphicsLayoutArea generateLayoutArea(List<Tuple<int, int>> rectangleList)
        {
            GraphicsLayoutArea rtrnArea = null;

            if (rectangleList.Count <= 0)
            {
                return null;
            }

            HashSet<Tuple<int, int>> rectangleSet = new HashSet<Tuple<int, int>>(rectangleList);

            List<DirectedLine> exteriorLineList = new List<DirectedLine>();

            foreach (Tuple<int, int> rectangleIndex in rectangleList)
            {
                exteriorLineList.AddRange(getExteriorLines(rectangleIndex, rectangleSet));

                if (DebugContainsDuplicate(exteriorLineList))
                {
                    int x = 0;
                }
            }

            lineDict.Clear();

            foreach (DirectedLine directedLine in exteriorLineList)
            {
                addToLineDict(directedLine);
            }

            List<DirectedPolygon> boundaryList = new List<DirectedPolygon>();

            while (exteriorLineList.Count > 0)
            {
                DirectedLine directedLine = exteriorLineList[0];

                exteriorLineList.RemoveAt(0);

                DirectedPolygon directedPolygon = generateBoundary(directedLine, exteriorLineList);

                if (directedPolygon.AreaInSqrInches() <= 0.01)
                {
                    continue;
                }

                boundaryList.Add(directedPolygon);
            }

            if (boundaryList.Count <= 0)
            {
                return null;
            }

            GraphicsDirectedPolygon graphicsDirectedPolygon = new GraphicsDirectedPolygon(Window, Page, boundaryList[0]);

            if (boundaryList.Count == 1)
            {
               
                return new GraphicsLayoutArea(
                    GraphicsRollout.Window
                    , GraphicsRollout.Page
                    , FinishesLibElements
                    , graphicsDirectedPolygon
                    , new List<GraphicsDirectedPolygon>());
               
            }

            List<DirectedPolygon> interiorAreaList = new List<DirectedPolygon>();

            DirectedPolygon exteriorPolygon = getExteriorInteriorAreas(boundaryList, interiorAreaList);


            return new GraphicsLayoutArea(
                GraphicsRollout.Window
                , GraphicsRollout.Page
                , FinishesLibElements
                , graphicsDirectedPolygon
                , null);
        }


        private DirectedPolygon getExteriorInteriorAreas(List<DirectedPolygon> boundaryList, List<DirectedPolygon> interiorAreaList)
        {
            dumpBoundaryLists(boundaryList, @"C:\Temp\BoundaryLists.txt");

            int exteriorIndex = -1;

            for (int i = 0; i < boundaryList.Count - 1; i++)
            {
                for (int j = i + 1; j < boundaryList.Count; j++)
                {
                    if (boundaryList[i].Contains(boundaryList[j]))
                    {
                        exteriorIndex = i;
                        break;
                    }

                    if (boundaryList[j].Contains(boundaryList[i]))
                    {
                        exteriorIndex = j;
                        break;
                    }
                }

                if (exteriorIndex >= 0)
                {
                    break;
                }
            }

            if (exteriorIndex < 0)
            {
                return null;
            }

            for (int i = 0; i < boundaryList.Count; i++)
            {
                if (i != exteriorIndex)
                {
                    interiorAreaList.Add(boundaryList[i]);
                }
            }

            return boundaryList[exteriorIndex];
        }

        private DirectedPolygon generateBoundary(DirectedLine frstLine, List<DirectedLine> exteriorLineList)
        {
            List<DirectedLine> boundaryLineList = new List<DirectedLine>();

            boundaryLineList.Add(frstLine);

            exteriorLineList.Remove(frstLine);
            removeFromLineDict(frstLine);

            DirectedLine nextLine = getNextLineInSequence(frstLine.Coord2);

            boundaryLineList.Remove(nextLine);

            while (!(nextLine is null))
            {
                exteriorLineList.Remove(nextLine);
                removeFromLineDict(nextLine);

                DirectedLine lastLine = boundaryLineList.Last();

                Coordinate lastLineCoord2 = lastLine.Coord2;

                Coordinate nextLineCoord1 = nextLine.Coord1;
                Coordinate nextLineCoord2 = nextLine.Coord2;

                if (nextLineCoord2 == lastLineCoord2)
                {
                    Utilities.Swap(ref nextLineCoord1, ref nextLineCoord2);
                }

                if (lastLine.IsHorizontal() && nextLine.IsHorizontal() ||
                    lastLine.IsVertical() && nextLine.IsVertical())
                {
                    lastLine.Coord2 = nextLineCoord2;
                }

                else
                {
                    boundaryLineList.Add(new DirectedLine(nextLineCoord1, nextLineCoord2));
                }

                if (nextLineCoord2 == frstLine.Coord1)
                {
                    break;
                }

                nextLine = getNextLineInSequence(nextLineCoord2);

            }

            DirectedPolygon rtrnPolygon = new DirectedPolygon(boundaryLineList);

            return rtrnPolygon;
        }

        Dictionary<Coordinate, List<DirectedLine>> lineDict = new Dictionary<Coordinate, List<DirectedLine>>();

        private void addToLineDict(DirectedLine directedLine)
        {
            addToLineDict(directedLine.Coord1, directedLine);
            addToLineDict(directedLine.Coord2, directedLine);
        }

        private void addToLineDict(Coordinate coord, DirectedLine directedLine)
        {
            List<DirectedLine> lineList = null;

            if (!lineDict.ContainsKey(coord))
            {
                lineList = new List<DirectedLine>();

                lineDict.Add(coord, lineList);
            }

            else
            {
                lineList = lineDict[coord];
            }

            lineList.Add(directedLine);
        }

        private void removeFromLineDict(DirectedLine directedLine)
        {
            removeFromLineDict(directedLine.Coord1, directedLine);
            removeFromLineDict(directedLine.Coord2, directedLine);
        }

        private void removeFromLineDict(Coordinate coord, DirectedLine directedLine)
        {
            if (!lineDict.ContainsKey(coord))
            {
                return;
            }

            List<DirectedLine> lineList = lineDict[coord];

            lineList.Remove(directedLine);
        }

        private DirectedLine getNextLineInSequence(Coordinate coord2)
        {

            if (!lineDict.ContainsKey(coord2))
            {
                return null;
            }

            List<DirectedLine> lineList = lineDict[coord2];

            if (lineList.Count <= 0)
            {
                return null;
            }

            if (lineList.Count == 1)
            {
                return lineList[0];
            }

            return lineList[0];

            //throw new NotImplementedException();

        }

        private List<DirectedLine> getExteriorLines(Tuple<int, int> rectangleIndex, HashSet<Tuple<int, int>> rectangleSet)
        {
            List<DirectedLine> rtrnList = new List<DirectedLine>();

            int x = rectangleIndex.Item1;
            int y = rectangleIndex.Item2;

            Tuple<int, int> leftRect = new Tuple<int, int>(x - 1, y);
            Tuple<int, int> rghtRect = new Tuple<int, int>(x + 1, y);
            Tuple<int, int> upprRect = new Tuple<int, int>(x, y + 1);
            Tuple<int, int> lowrRect = new Tuple<int, int>(x, y - 1);

            Rectangle rectangle = internalRectangle(rectangleIndex);

            if (!rectangleSet.Contains(leftRect))
            {
                DirectedLine leftLine = new DirectedLine(rectangle.LowerLeft, rectangle.UpperLeft);

                rtrnList.Add(leftLine);
            }

            if (!rectangleSet.Contains(rghtRect))
            {
                DirectedLine rghtLine = new DirectedLine(rectangle.LowerRght, rectangle.UpperRght);

                rtrnList.Add(rghtLine);
            }

            if (!rectangleSet.Contains(upprRect))
            {
                DirectedLine upprLine = new DirectedLine(rectangle.UpperLeft, rectangle.UpperRght);

                rtrnList.Add(upprLine);
            }

            if (!rectangleSet.Contains(lowrRect))
            {
                DirectedLine lowrLine = new DirectedLine(rectangle.LowerLeft, rectangle.LowerRght);

                rtrnList.Add(lowrLine);
            }

            return rtrnList;
        }

        private Rectangle internalRectangle(Tuple<int, int> rectangleIndex) => InternalRectangle[rectangleIndex.Item1, rectangleIndex.Item2];

        private bool IsInteriorPoint(HashSet<Tuple<int, int>> rectangleSet, Tuple<int, int> rectangleIndex)
        {

            int x = rectangleIndex.Item1;
            int y = rectangleIndex.Item2;

            Tuple<int, int> leftRect = new Tuple<int, int>(x - 1, y);
            Tuple<int, int> rghtRect = new Tuple<int, int>(x + 1, y);
            Tuple<int, int> upprRect = new Tuple<int, int>(x, y + 1);
            Tuple<int, int> lowrRect = new Tuple<int, int>(x, y - 1);

            return (!rectangleSet.Contains(leftRect) || !!rectangleSet.Contains(rghtRect) || !rectangleSet.Contains(upprRect) || !rectangleSet.Contains(lowrRect));
        }

        private void generateInternalRectangles(HashSet<double> xVals, HashSet<double> yVals, LayoutArea layoutArea)
        {
            //FilledRectangleList.Clear();
            //UnFilledRectangleList.Clear();

            //foreach (var x in layoutArea.externalArea)
            //{
            //    Console.WriteLine(x.Coord1.ToString() + x.Coord2.ToString());
            //}

            List<double> xValList = xVals.ToList();
            List<double> yValList = yVals.ToList();

            xValList.Sort();
            yValList.Sort();

            InternalRectangle = new Rectangle[xRecs, yRecs];
            FilledRectangle = new bool[xRecs, yRecs];

            for (int i = 0; i < xValList.Count - 1; i++)
            {
                double xLeft = xValList[i];
                double xRght = xValList[i + 1];

                for (int j = 0; j < yValList.Count - 1; j++)
                {
                    double yLowr = yValList[j];
                    double yUppr = yValList[j + 1];

                    InternalRectangle[i, j] = new Rectangle(new Coordinate(xLeft, yUppr), new Coordinate(xRght, yLowr));

                    FilledRectangle[i, j] = isFilled(InternalRectangle[i, j], layoutArea);
                }
            }

            //DumpInternalRectangles(@"C:\Temp\InternalRectangles.txt");
        }

        private bool isFilled(Rectangle rectangle, LayoutArea layoutArea)
        {
            DirectedPolygon rectanglePolygon = (DirectedPolygon)rectangle;

            foreach (DirectedPolygon internalArea in layoutArea.InternalAreas)
            {
                if (internalArea.Contains(rectanglePolygon))
                {
                    return false;
                }
            }

            return rectanglePolygon.Intersects(layoutArea.ExternalArea);
        }

        private void generateXYPoints(Rectangle boundingBox, LayoutArea layoutArea, HashSet<double> xVals, HashSet<double> yVals)
        {
            xVals.Add(Math.Round(boundingBox.LowerLeft.X, 4));
            xVals.Add(Math.Round(boundingBox.LowerRght.X, 4));
            xVals.Add(Math.Round(boundingBox.UpperLeft.X, 4));
            xVals.Add(Math.Round(boundingBox.UpperRght.X, 4));

            yVals.Add(Math.Round(boundingBox.LowerLeft.Y, 4));
            yVals.Add(Math.Round(boundingBox.LowerRght.Y, 4));
            yVals.Add(Math.Round(boundingBox.UpperLeft.Y, 4));
            yVals.Add(Math.Round(boundingBox.UpperRght.Y, 4));

            List<Coordinate> externalCoordList = layoutArea.ExternalArea.CoordinateList();

            foreach (Coordinate coord in externalCoordList)
            {
                double x = Math.Round(coord.X, 4);
                double y = Math.Round(coord.Y, 4);

                if (!xVals.Contains(x))
                {
                    xVals.Add(x);
                }

                if (!yVals.Contains(y))
                {
                    yVals.Add(y);
                }
            }

            foreach (DirectedPolygon internalArea in layoutArea.InternalAreas)
            {
                List<Coordinate> internalCoordList = internalArea.CoordinateList();

                foreach (Coordinate coord in internalCoordList)
                {
                    double x = Math.Round(coord.X, 4);
                    double y = Math.Round(coord.Y, 4);

                    if (!xVals.Contains(x))
                    {
                        xVals.Add(x);
                    }

                    if (!yVals.Contains(y))
                    {
                        yVals.Add(y);
                    }
                }
            }
        }

        private List<GraphicsLayoutArea> generateBoundedLayoutAreas()
        {
            List<GraphicsLayoutArea> rtrnLayoutAreaList = new List<GraphicsLayoutArea>();

            externalAreaList.AddRange(((DirectedPolygon)this.RolloutRectangle).Intersect(originalLayoutArea.ExternalArea));

            // There may be more than one resulting external area.

            foreach (DirectedPolygon externalArea in externalAreaList)
            {
                GraphicsDirectedPolygon graphicsDirectedPolygon = new GraphicsDirectedPolygon(Window, Page, externalArea);

                rtrnLayoutAreaList.Add(
                    new GraphicsLayoutArea(
                        Window, Page, FinishesLibElements, graphicsDirectedPolygon, new List<GraphicsDirectedPolygon>())
                    );
            }

            foreach (DirectedPolygon internalArea in originalLayoutArea.InternalAreas)
            {
                internalAreaList.AddRange(((DirectedPolygon)this.RolloutRectangle).Intersect(internalArea));
            }

            // We build here the layout areas bounded by the roll out rectangle by embedding any internal areas.

            foreach (DirectedPolygon internalArea in internalAreaList)
            {
                foreach (GraphicsLayoutArea layoutArea in rtrnLayoutAreaList)
                {
                    if (!layoutArea.ExternalArea.Contains(internalArea))
                    {
                        layoutArea.InternalAreasAdd(internalArea);
                        break;
                    }
                }
            }

            foreach (LayoutArea layoutArea in rtrnLayoutAreaList)
            {
                // Round all coordinates in external and internal areas to 4 decimal places.

                layoutArea.NumericallyConditionAreas(4);
            }

            return rtrnLayoutAreaList;
        }

        private List<Tuple<Rectangle, LayoutArea>> generateBoundingBoxLayoutAreaList()
        {
            List<Tuple<Rectangle, LayoutArea>> rtrnList = new List<Tuple<Rectangle, LayoutArea>>();

            foreach (LayoutArea layoutArea in boundedLayoutAreas)
            {
                Rectangle boundingBox = generateBoundingBox(layoutArea);

                if (boundingBox is null)
                {
                    continue;
                }

                rtrnList.Add(new Tuple<Rectangle, LayoutArea>(boundingBox, layoutArea));
            }

            return rtrnList;
        }

        private Rectangle generateBoundingBox(LayoutArea layoutArea)
        {
            double? minX = null;
            double? maxX = null;

            layoutArea.GetMinMaxAtLevelY(RolloutRectangle.LowerLeft.Y, RolloutRectangle.UpperLeft.Y, out minX, out maxX);

            if (minX is null || maxX is null)
            {
                return null;
            }

            return new Rectangle(new Coordinate(minX.Value, RolloutRectangle.UpperLeft.Y), new Coordinate(maxX.Value, RolloutRectangle.LowerRght.Y));
        }


        private void generateRectangleGroups()
        {
            GroupNumber = new int?[xRecs, yRecs];

            HashSet<Tuple<int, int>> rectangleSet = new HashSet<Tuple<int, int>>();

            for (int i = 0; i < xRecs; i++)
            {
                for (int j = 0; j < yRecs; j++)
                {
                    GroupNumber[i, j] = null;

                    if (this.FilledRectangle[i, j])
                    {
                        rectangleSet.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            int groupNum = 0;

            while (rectangleSet.Count > 0)
            {
                RectangleGroups[groupNum] = buildGroup(rectangleSet, groupNum);

                groupNum++;
            }

            //DumpRectangleGroups(@"C:\Temp\RectangleGroups.txt");

        }

        private List<Tuple<int, int>> buildGroup(HashSet<Tuple<int, int>> rectangleSet, int groupNum)
        {
            List<Tuple<int, int>> rtrnList = new List<Tuple<int, int>>();

            Tuple<int, int> currentRectangle = rectangleSet.First();

            Queue<Tuple<int, int>> buildingGroup = new Queue<Tuple<int, int>>();

            buildingGroup.Enqueue(currentRectangle);

            while (buildingGroup.Count > 0)
            {
                currentRectangle = buildingGroup.Dequeue();

                int x = currentRectangle.Item1;
                int y = currentRectangle.Item2;

                GroupNumber[x, y] = groupNum;

                rtrnList.Add(currentRectangle);

                rectangleSet.Remove(currentRectangle);

                Tuple<int, int> leftRect = new Tuple<int, int>(x - 1, y);
                Tuple<int, int> rghtRect = new Tuple<int, int>(x + 1, y);
                Tuple<int, int> upprRect = new Tuple<int, int>(x, y + 1);
                Tuple<int, int> lowrRect = new Tuple<int, int>(x, y - 1);

                if (rectangleSet.Contains(leftRect))
                {
                    if (!buildingGroup.Contains(leftRect))
                    {
                        buildingGroup.Enqueue(leftRect);
                    }
                }

                if (rectangleSet.Contains(rghtRect))
                {
                    if (!buildingGroup.Contains(rghtRect))
                    {
                        buildingGroup.Enqueue(rghtRect);
                    }
                }

                if (rectangleSet.Contains(upprRect))
                {
                    if (!buildingGroup.Contains(upprRect))
                    {
                        buildingGroup.Enqueue(upprRect);
                    }
                }

                if (rectangleSet.Contains(lowrRect))
                {
                    if (!buildingGroup.Contains(lowrRect))
                    {
                        buildingGroup.Enqueue(lowrRect);
                    }
                }
            }

            rtrnList.Sort();

            return rtrnList;
        }

        internal void Rotate(double[,] rotationMatrix, double angle)
        {
            GraphicsCutList.ForEach(c => c.Rotate(rotationMatrix, angle));

            RolloutRectangle.Rotate(angle);

            GraphicsUndrageList.ForEach(u => u.Rotate(angle, rotationMatrix));

            internalAreaList.ForEach(i => i.Rotate(rotationMatrix));

            externalAreaList.ForEach(e => e.Rotate(rotationMatrix));

            EmbeddedOverageList.ForEach(o => o.Rotate(angle, rotationMatrix));
        }

        internal void Translate(Coordinate offset)
        {
            GraphicsCutList.ForEach(c => c.Translate(offset));

            RolloutRectangle.Translate(offset);

            GraphicsUndrageList.ForEach(u => u.Translate(offset));

            internalAreaList.ForEach(i => i.Translate(offset));

            externalAreaList.ForEach(e => e.Translate(offset));

            EmbeddedOverageList.ForEach(o => o.Translate(offset));
        }

        public void Clear()
        {
            GraphicsCutList.Clear();

            RolloutRectangle = null;

            BoundingBoxLayoutAreaList.Clear();

            GraphicsUndrageList.Clear();

            boundedLayoutAreas = null;

            internalAreaList.Clear();

            externalAreaList.Clear();

            FilledRectangle = null;
            InternalRectangle = null;
            GroupNumber = null;

            RectangleGroups.Clear();
            LayoutAreaDict.Clear();

            xPnts = 0;
            yPnts = 0;

            xRecs = 0;
            yRecs = 0;
        }


        private bool DebugContainsDuplicate(List<DirectedLine> exteriorLineList)
        {
            HashSet<string> lineSet = new HashSet<string>();

            for (int i = 0; i < exteriorLineList.Count; i++)
            {
                string lineStr = exteriorLineList[i].ToNormalizedString();

                if (lineSet.Contains(lineStr))
                {
                    return true;
                }
            }

            return false;
        }

        private void DumpRectangleGroups(string outpFilePath)
        {
            StreamWriter sw = new StreamWriter(outpFilePath);

            foreach (KeyValuePair<int, List<Tuple<int, int>>> kvp in RectangleGroups)
            {
                int group = kvp.Key;

                List<Tuple<int, int>> rectangleList = kvp.Value;

                foreach (Tuple<int, int> rectIndex in rectangleList)
                {
                    Rectangle rectangle = InternalRectangle[rectIndex.Item1, rectIndex.Item2];

                    sw.WriteLine(group.ToString() + ": " + rectangle.UpperLeft.ToString() + ", " + rectangle.LowerRght.ToString());
                }
            }

            sw.Flush();

            sw.Close();
        }

        private void DumpInternalRectangles(string outpFilePath)
        {
            StreamWriter sw = new StreamWriter(outpFilePath);

            int xlmt = InternalRectangle.GetLength(0);
            int ylmt = InternalRectangle.GetLength(1);

            for (int x = 0; x < xlmt; x++)
            {
                for (int y = 0; y < ylmt; y++)
                {
                    Rectangle rectangle = InternalRectangle[x, y];

                    sw.WriteLine('[' + x + ',' + y + ']' + ": " + rectangle.UpperLeft.ToString() + ", " + rectangle.LowerRght.ToString());
                }
            }

            sw.Flush();

            sw.Close();
        }

        private void dumpBoundaryLists(List<DirectedPolygon> boundaryList, string outpFilePath)
        {
            StreamWriter sw = new StreamWriter(outpFilePath);

            int i = 1;

            foreach (DirectedPolygon boundary in boundaryList)
            {
                double area = boundary.AreaInSqrInches();

                sw.WriteLine("Boundary " + i.ToString() + "Area: " + area.ToString("0.0000") + "\n");

                foreach (var x in boundary)
                {
                    sw.WriteLine(x.Coord1.ToString() + ", " + x.Coord2.ToString());
                }

                i++;
            }

            sw.Flush();

            sw.Close();
        }

        internal string ToString(string delimeter)
        {
            string rtrnValu = string.Empty;

            foreach (var x in this.externalAreaList)
            {
                rtrnValu += x.ToString(delimeter) + delimeter;
            }

            return rtrnValu;
        }

    }
}

