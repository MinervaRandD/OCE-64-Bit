#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: LayoutArea.cs. Project: MaterialsLayout. Created: 10/7/2024         */
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
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using SettingsLib;
    using Utilities;
    using Seaming_Boundary;
    using VoronoiLib;
    using Graphics;
    using System.IO;
    using Globals;
    using FinishesLib;

    public class LayoutArea
    {
        // MDD Reset this.

        public DirectedPolygon _externalArea = null;

        public DirectedPolygon ExternalArea
        {
            get
            {
                return _externalArea;
            }

            set
            {
                _externalArea = value;
            }
        }

        public List<DirectedPolygon> InternalAreas { get; set; } = new List<DirectedPolygon>();

        public LayoutAreaType LayoutAreaType
        {
            get;
            set;
        } = LayoutAreaType.Normal;

        public bool CreatedFromFixedWidth
        {
            get;
            set;
        } = false;

        public void InternalAreasAdd(DirectedPolygon directedPolygon)
        {
            if (InternalAreas == null)
            {
                InternalAreas = new List<DirectedPolygon>();
            }

            InternalAreas.Add(directedPolygon);
        }

        CoordinatedList<SeamingBoundary> SeamingBoundaryList = new CoordinatedList<SeamingBoundary>();

        public int ID = -1;

        public static int IDGenerator = 1;

        public int CutIndex = -1;

        // The following constructs are a kludge to incorporate fixed width areas into the layout area schema.
        // In the long run it would be better to define two types of layout areas -- one for fixed width and 
        // one for regular layout areas.

        /// <summary>
        /// Flag indicating that this is a border area type layout area. See comment above.
        /// </summary>
        public bool IsBorderArea
        {
            get
            {
                return LayoutAreaType == LayoutAreaType.FixedWidth;
            }

            set
            {
                LayoutAreaType = LayoutAreaType.FixedWidth;
            }
        }

        /// <summary>
        /// The border layout area as an underage
        /// </summary>
        public GraphicsUndrage BorderAreaUndrage { get; set; } = null;

        /// <summary>
        /// The border layout area as an overage
        /// </summary>
        /// 
        public Overage BorderAreaOverage { get; set; } = null;

        /// <summary>
        /// The width of the fixed with when this area was created
        /// </summary>
        public double BorderWidthInInches { get; set; } = 0;

        /// <summary>
        /// The border area rectangle if appropriate
        /// </summary>
        //public Geometry.Rectangle BorderAreaRectangle { get; set; } = null;


        public CoordinatedList<Rollout> RolloutList { get; set; } = new CoordinatedList<Rollout>();

        // Seam list coordination
        public CoordinatedList<Seam> SeamList { get; set; } = new CoordinatedList<Seam>();
        public CoordinatedList<Seam> DisplaySeamList { get; set; } = new CoordinatedList<Seam>();

        //public List<Overage> OverageGeneratorOvers = new List<Overage>();

        public List<Cut> CutList
        {
            get
            {
                List<Cut> rtrnList = new List<Cut>();

                RolloutList.ForEach(r => rtrnList.AddRange(r.GraphicsCutList));


                return rtrnList;
            }
        }

        public List<Undrage> UndrageList
        {
            get
            {
                List<Undrage> rtrnList = new List<Undrage>();

                if (!IsBorderArea)
                {
                    // If this is not a border area then we check the rollouts to get the underages.
                    RolloutList.ForEach(r => rtrnList.AddRange(r.GraphicsUndrageList));
                }

                else
                {
                    // If this is border area than we consider the area itself as a potential underage (or overage)

                    // Note for border areas, there should be no internal areas, but we do not check explicitly here.

                    if (Utilities.IsNotNull(BorderAreaUndrage))
                    {
                        rtrnList.Add(BorderAreaUndrage);
                    }
                }

                return rtrnList;
            }
        }

        public List<VirtualOverage> VirtualOverageList { get; set; } = new List<VirtualOverage>();

        public List<VirtualUndrage> VirtualUndrageList { get; set; } = new List<VirtualUndrage>();

        public double SmallElementWidthInInches { get; set; } = 0;

        public double CutExtraElement { get; set; }

        public double SeamWidthInInches { get; set; }

        public double MaterialWidthInInches { get; set; }

        public double[,] rotationMatrix;

        public double[,] inverseRotationMatrix;

        public AreaFinishLayers AreaFinishLayers
        {
            get;
            set;
        }= null;

        #region Constructors and Cloners

        public LayoutArea()
        {
            ID = IDGenerator++;
        }

        public LayoutArea(int cutIndex)
        {
            ID = IDGenerator++;

            this.CutIndex = cutIndex;
        }

        public LayoutArea(DirectedPolygon externalPolygon, AreaFinishLayers areaFinishLayers)
        {
            ID = IDGenerator++;

            ExternalArea = externalPolygon;

            this.AreaFinishLayers = areaFinishLayers;
        }

        public LayoutArea(DirectedPolygon externalPolygon, List<DirectedPolygon> internalPerimenters)
        {
            ID = IDGenerator++;

            ExternalArea = externalPolygon;

            InternalAreas = new List<DirectedPolygon>(internalPerimenters);
        }

        #endregion

        internal void NumericallyConditionAreas(int precision)
        {
            ExternalArea.NumericallyCondition(precision);

            foreach (DirectedPolygon internalArea in InternalAreas)
            {
                internalArea.NumericallyCondition(precision);
            }
        }

        private double[,] generateRotationMatrix(double theta)
        {
            rotationMatrix = new double[2, 2];

            rotationMatrix[0, 0] = Math.Cos(theta);
            rotationMatrix[0, 1] = -Math.Sin(theta);
            rotationMatrix[1, 0] = Math.Sin(theta);
            rotationMatrix[1, 1] = Math.Cos(theta);

            return rotationMatrix;
        }

        private double[,] generateInverseRotationMatrix(double theta)
        {
            inverseRotationMatrix = new double[2, 2];

            inverseRotationMatrix[0, 0] = Math.Cos(theta);
            inverseRotationMatrix[0, 1] = -Math.Sin(theta);
            inverseRotationMatrix[1, 0] = Math.Sin(theta);
            inverseRotationMatrix[1, 1] = Math.Cos(theta);

            return inverseRotationMatrix;
        }

        private void transformArea(DirectedLine baseLine, double theta)
        {
            double[,] rotationMatrix = generateRotationMatrix(-theta);

            Coordinate transformCoord = baseLine.Coord1;

            Transform(-transformCoord, rotationMatrix, theta);
        }

        private void inverseTransformArea(DirectedLine baseLine, double theta)
        {
            double[,] rotationMatrix = generateRotationMatrix(theta);

            Coordinate transformCoord = baseLine.Coord1;

            InverseTransform(transformCoord, rotationMatrix, -theta);
        }

        private void setupSeamAndPatternGeneration(DirectedLine baseLine)
        {
            graphicsTolerance = Math.Pow(10.0, -GlobalSettings.GraphicsPrecision);

            theta = baseLine.Atan();

            transformCoord = baseLine.Coord1;

            rotationMatrix = new double[2, 2];

            rotationMatrix[0, 0] = Math.Cos(-theta);
            rotationMatrix[0, 1] = -Math.Sin(-theta);
            rotationMatrix[1, 0] = Math.Sin(-theta);
            rotationMatrix[1, 1] = Math.Cos(-theta);

            inverseRotationMatrix = new double[2, 2];

            inverseRotationMatrix[0, 0] = Math.Cos(theta);
            inverseRotationMatrix[0, 1] = -Math.Sin(theta);
            inverseRotationMatrix[1, 0] = Math.Sin(theta);
            inverseRotationMatrix[1, 1] = Math.Cos(theta);

        }

        double graphicsTolerance = 0;
        double theta = 0;
        Coordinate transformCoord;

        public void GenerateSeamsAndRollouts(
            DirectedLine baseLine
            , double offset
            , double seamWidthInInches
            , double materialWidthInInches
            , double materialOverlapInInches
            , double drawingScaleInInches)
        {
 
            SeamWidthInInches = seamWidthInInches;

            setupSeamAndPatternGeneration(baseLine);

            Transform(-transformCoord, rotationMatrix, theta);

            if (LayoutAreaType == LayoutAreaType.Normal)
            {
                GenerateHorizontalSeamsAndRollouts(offset, seamWidthInInches, materialWidthInInches, materialOverlapInInches, graphicsTolerance, -theta, drawingScaleInInches, false, false);
            }

            else if (LayoutAreaType == LayoutAreaType.OversGenerator)
            {
                GenerateHorizontalSeamsAndOvers(offset, seamWidthInInches, materialWidthInInches, materialOverlapInInches, graphicsTolerance, -theta, drawingScaleInInches, false, true);
            }

            InverseTransform(transformCoord, inverseRotationMatrix, -theta);

            foreach (var rollout in this.RolloutList)
            {
                foreach (var cut in rollout.GraphicsCutList)
                {
                    foreach (var overage in  cut.GraphicsOverageList)
                    {
                        overage.UpdateIndexLocation();
                    }
                }

                foreach (var undrage in rollout.GraphicsUndrageList)
                {
                    undrage.UpdateIndexLocation();
                }
            }
        }


        public void GenerateHorizontalSeamsAndRollouts(
            double yOffset
            , double seamWidth
            , double materialWidth
            , double materialOverlap
            , double graphicsTolerance
            , double theta, double drawingScaleInInches
            , bool generateEmbeddedCuts
            , bool generateEmbeddedOvers)
        {
            generateHorizontalRollout(yOffset, theta, seamWidth, materialWidth, materialOverlap, graphicsTolerance, drawingScaleInInches, generateEmbeddedCuts, generateEmbeddedOvers);
            generateHorizontalSeams(yOffset, seamWidth, materialWidth, graphicsTolerance, 1);
        }

        public void GenerateHorizontalSeamsAndOvers(
            double yOffset
            , double seamWidth
            , double materialWidth
            , double materialOverlap
            , double graphicsTolerance
            , double theta, double drawingScaleInInches
            , bool generateEmbeddedCuts
            , bool generateEmbeddedOvers)
        {
           // OverageGeneratorOvers.Clear();

            generateHorizontalRollout(yOffset, theta, seamWidth, materialWidth, materialOverlap, graphicsTolerance, drawingScaleInInches, generateEmbeddedCuts, generateEmbeddedOvers);
            generateHorizontalSeams(yOffset, seamWidth, materialWidth, graphicsTolerance, 1);
        }

        private void generateHorizontalRollout(
            double yOffset
            , double theta
            , double seamWidth
            , double materialWidth
            , double materialOverlap
            , double graphicsTolerance
            , double drawingScaleInInches
            , bool generateEmbeddedCuts
            , bool generateEmbeddedOvers)
        {
            
            HorizontalRolloutGenerator horizontalRolloutGenerator =
                new HorizontalRolloutGenerator(
                    VisioInterop.Window
                    , VisioInterop.Page
                    , (GraphicsLayoutArea)this
                    , generateEmbeddedCuts
                    , generateEmbeddedOvers)
                {
                    FinishesLibElements = ((GraphicsLayoutArea)this).FinishLibElements
                };

            horizontalRolloutGenerator.GenerateRollouts(theta, yOffset, seamWidth, materialWidth, materialOverlap, drawingScaleInInches, this.LayoutAreaType);

            horizontalRolloutGenerator.RolloutList.ForEach(r => RolloutList.Add(r));

            if (this.LayoutAreaType == LayoutAreaType.OversGenerator)
            {
                uint overageIndex = Overage.OverageIndexGenerator();

                foreach (Rollout rollout in RolloutList)
                {
                    foreach (Overage embeddedOverage in rollout.EmbeddedOverageList)
                    {
                        embeddedOverage.OverageIndex = overageIndex;
                    }
                }
            }
        }

        private void generateHorizontalSeams(
            double yOffset
            , double seamWidth
            , double materialWidth
            , double graphicsTolerance
            , int keyTolerance)
        {
            HorizontalSeamGenerator horizontalSeamGenerator = new HorizontalSeamGenerator(this);

            horizontalSeamGenerator.GenerateSeams(yOffset, seamWidth, graphicsTolerance, keyTolerance);

            foreach (DirectedLine line in horizontalSeamGenerator.SeamList)
            {
                SeamList.Add(new Seam(line, SeamType.Basic, false));
            }
        }

        public Coordinate VoronoiLabelLocation()
        {
            List<DirectedLine> directedLineList = new List<DirectedLine>();

            if (Utilities.IsNotNull(ExternalArea))
            {
                directedLineList.AddRange(ExternalArea);
            }

            foreach (DirectedPolygon internalArea in this.InternalAreas)
            {
                directedLineList.AddRange(internalArea);
            }

            if (directedLineList.Count <= 0)
            {
                return Coordinate.NullCoordinate;
            }

            VoronoiRunner voronoiRunner = new VoronoiRunner(directedLineList, 3);

            return voronoiRunner.RunVoroniAlgo();
        }

        public void Transform(Coordinate offset, double[,] rotationMatrix, double theta)
        {
            Translate(offset);
            Rotate(rotationMatrix, theta);
        }

        public void InverseTransform(Coordinate offset, double[,] rotationMatrix, double theta)
        {
            Rotate(rotationMatrix, theta);
            Translate(offset);
        }

        public void Translate(Coordinate offset)
        {
            if (ExternalArea != null)
            {
                ExternalArea.Translate(offset);
            }

            foreach (DirectedPolygon internalPerimeter in InternalAreas)
            {
                internalPerimeter.Translate(offset);
            }

            foreach (Seam seam in SeamList)
            {
                seam.Translate(offset);
            }

            foreach (Rollout rollout in RolloutList)
            {
                rollout.Translate(offset);

                foreach (GraphicsCut cut in rollout.GraphicsCutList)
                {
                    cut.Translate(offset);

                    foreach (var overage in cut.OverageList)
                    {
                        overage.Translate(offset);
                    }
                }

                foreach (var undrage in rollout.GraphicsUndrageList)
                {
                    undrage.Translate(offset);
                }
                
            }

        }

        public void Rotate(double[,] rotationMatrix, double theta)
        {
            if (ExternalArea != null)
            {
                ExternalArea.Rotate(rotationMatrix);
            }

            foreach (DirectedPolygon internalPerimeter in InternalAreas)
            {
                internalPerimeter.Rotate(rotationMatrix);
            }

            foreach (Seam seam in SeamList)
            {
                seam.Rotate(rotationMatrix);
            }

            foreach (Rollout rollout in RolloutList)
            {
                rollout.Rotate(rotationMatrix, theta);

                foreach (GraphicsCut cut in rollout.GraphicsCutList)
                {
                    cut.Rotate(rotationMatrix, theta);

                    foreach (var overage in cut.OverageList)
                    {
                        overage.Rotate(theta, rotationMatrix);
                    }
                }

                foreach (var undrage in rollout.GraphicsUndrageList)
                {
                    undrage.Rotate(theta, rotationMatrix);
                }
            }

        }

        public double MinY
        {
            get
            {
                if (ExternalArea != null)
                {
                    return ExternalArea.MinY;
                }

                return double.NaN;
            }
        }

        public double MaxY
        {
            get
            {
                if (ExternalArea != null)
                {
                    return ExternalArea.MaxY;
                }

                return double.NaN;
            }
        }

        public double MinX
        {
            get
            {
                if (ExternalArea != null)
                {
                    return ExternalArea.MinX;
                }

                return double.NaN;
            }
        }

        public double MaxX
        {
            get
            {
                if (ExternalArea != null)
                {
                    return ExternalArea.MaxX;
                }

                return double.NaN;
            }
        }

        //internal void GetMinMaxAtLevelY(double y, out double minX, out double maxX)
        //{
        //    ExternalArea.GetMinMaxAtLevelY(y, out minX, out maxX);
        //}

        internal void GetMinMaxAtLevelY(double y1, double y2, out double? minX, out double? maxX)
        {
            ExternalArea.GetMinMaxAtLevelY(y1, y2, out minX, out maxX);
        }


        public List<LayoutArea> Intersect(DirectedPolygon polygon)
        {
            DirectedPolygon externalPerimeter = ExternalArea;

            List<DirectedPolygon> externalResultsList = externalPerimeter.Intersect(polygon);

            List<LayoutArea> returnList = generateLayoutAreas(externalResultsList);

            return returnList;
        }

        public List<LayoutArea> Subtract(DirectedPolygon polygon)
        {
            DirectedPolygon externalPerimeter = ExternalArea;

            List<DirectedPolygon> internalAreas = new List<DirectedPolygon>(this.InternalAreas);

            internalAreas.Add(polygon);

            return Subtract(internalAreas);

        }

        public List<LayoutArea> Subtract(List<DirectedPolygon> polygonList)
        {
            List<DirectedPolygon> ilu = DirectedPolygon.Union(polygonList);

            List<DirectedPolygon> elu = ExternalArea.Subtract(ilu);

            if (elu.Count <= 0)
            {
                return new List<LayoutArea>();
            }

            List<LayoutArea> rtrnList = new List<LayoutArea>();

            List<DirectedPolygon> externalAreaList = new List<DirectedPolygon>();
            List<DirectedPolygon> internalAreaList = new List<DirectedPolygon>();

            getExternalInternalAreaList(elu, externalAreaList, internalAreaList);

            foreach (DirectedPolygon externalArea in externalAreaList)
            {
                List<DirectedPolygon> internalAreas = new List<DirectedPolygon>();

                for (int i = internalAreaList.Count - 1; i >= 0; i--)
                {
                    if (externalArea.Contains(internalAreaList[i]))
                    {
                        internalAreas.Add(internalAreaList[i]);

                        internalAreaList.RemoveAt(i);
                    }
                }

                LayoutArea layoutArea1 = new LayoutArea(externalArea, internalAreas)
                {
                    AreaFinishLayers = this.AreaFinishLayers
                };

                rtrnList.Add(layoutArea1);
            }
           
            for (int i = 1; i < ilu.Count; i++)
            {
                rtrnList.Add(new LayoutArea(ilu[i], AreaFinishLayers));
            }

            return rtrnList;
        }

        public List<LayoutArea> Subtract1(List<DirectedPolygon> pList)
        {
            List<DirectedPolygon> ilu = DirectedPolygon.Union1(pList);

            List<DirectedPolygon> elu = ExternalArea.Subtract(ilu);

            if (elu.Count <= 0)
            {
                return new List<LayoutArea>();
            }

            List<LayoutArea> rtrnList = new List<LayoutArea>();

            List<DirectedPolygon> externalAreaList = new List<DirectedPolygon>();
            List<DirectedPolygon> internalAreaList = new List<DirectedPolygon>();

            getExternalInternalAreaList(elu, externalAreaList, internalAreaList);

            foreach (DirectedPolygon externalArea in externalAreaList)
            {
                List<DirectedPolygon> internalAreas = new List<DirectedPolygon>();

                for (int i = internalAreaList.Count - 1; i >= 0; i--)
                {
                    if (externalArea.Contains(internalAreaList[i]))
                    {
                        internalAreas.Add(internalAreaList[i]);

                        internalAreaList.RemoveAt(i);
                    }
                }

                LayoutArea layoutArea1 = new LayoutArea(externalArea, internalAreas);

                rtrnList.Add(layoutArea1);
            }

            for (int i = 1; i < ilu.Count; i++)
            {
                rtrnList.Add(new LayoutArea(ilu[i], AreaFinishLayers));
            }

            return rtrnList;
        }

        public List<LayoutArea> Subtract2(List<DirectedPolygon> pList)
        {
            // Subtract 2 is design specifically to handle the case where the layout area has no internal areas and elements of the polygon list are disjoint.

#if DEBUG
            Debug.Assert(InternalAreas.Count == 0);

            for (int i = 0; i < pList.Count - 1;i++)
            {
                for (int j = i + 1; j < pList.Count;j++)
                {
                    Debug.Assert(!pList[i].Intersects(pList[j]));
                }
            }
#endif

            List<DirectedPolygon> elu = ExternalArea.Subtract(pList);

            if (elu.Count <= 0)
            {
                return new List<LayoutArea>();
            }

            List<LayoutArea> rtrnList = new List<LayoutArea>();

            List<DirectedPolygon> externalAreaList = new List<DirectedPolygon>();
            List<DirectedPolygon> internalAreaList = new List<DirectedPolygon>();

            getExternalInternalAreaList(elu, externalAreaList, internalAreaList);

            foreach (DirectedPolygon externalArea in externalAreaList)
            {
                List<DirectedPolygon> internalAreas = new List<DirectedPolygon>();

                for (int i = internalAreaList.Count - 1; i >= 0; i--)
                {
                    if (externalArea.Contains(internalAreaList[i]))
                    {
                        internalAreas.Add(internalAreaList[i]);

                        internalAreaList.RemoveAt(i);
                    }
                }

                LayoutArea layoutArea1 = new LayoutArea(externalArea, internalAreas);

                rtrnList.Add(layoutArea1);
            }

            return rtrnList;

        }

        public Coordinate GetClosestVertex(double x, double y)
        {
            return ExternalArea.GetClosestVertex(x, y);
        }

        private void getExternalInternalAreaList(List<DirectedPolygon> elu, List<DirectedPolygon> externalAreaList, List<DirectedPolygon> internalAreaList)
        {
            HashSet<DirectedPolygon> internalAreaSet = new HashSet<DirectedPolygon>();
            HashSet<DirectedPolygon> externalAreaSet = new HashSet<DirectedPolygon>(elu);

            for (int i = 0; i < elu.Count- 1; i++)
            {
                if (internalAreaSet.Contains(elu[i]))
                {
                    continue;
                }

                for (int j = i+1; j < elu.Count; j++)
                {
                    if (internalAreaSet.Contains(elu[j]))
                    {
                        continue;
                    }

                    if (elu[i].Contains(elu[j]))
                    {
                        internalAreaSet.Add(elu[j]);
                        externalAreaSet.Remove(elu[j]);
                    }

                    else if (elu[j].Contains(elu[i]))
                    {
                        internalAreaSet.Add(elu[i]);
                        externalAreaSet.Remove(elu[i]);
                    }
                }
            }

            internalAreaList.AddRange(internalAreaSet);
            externalAreaList.AddRange(externalAreaSet);

        }
        public bool Contains(Coordinate coord)
        {
            // Test to see if the directed polygon overlaps any internal area

            foreach (DirectedPolygon internalArea in InternalAreas)
            {
                if (internalArea.Contains(coord))
                {
                    return false;
                }
            }

            // Test to see if the directed polygon is contained within the external perimeter.

            return ExternalArea.Contains(coord);
        }

        /// <summary>
        /// Determines whether a layout area contains a directed polygon. In order to contain a polygon, the
        /// polygon must be contained within the external perimeter and not overlap any internal areas.
        /// </summary>
        /// <param name="polygon">The directed polygon to test</param>
        /// <returns>True if the directed polygon is contained within the layout area, false otherwise</returns>
        public bool Contains(DirectedPolygon polygon)
        {
            // Test to see if the directed polygon overlaps any internal area

            foreach (DirectedPolygon internalArea in InternalAreas)
            {
                if (polygon.Intersects(internalArea))
                {
                    return false;
                }
            }

            // Test to see if the directed polygon is contained within the external perimeter.

            return ExternalArea.Contains(polygon);
        }

        public bool ApproxContains(DirectedPolygon directedPolygon, double precision)
        {

            foreach (DirectedPolygon internalArea in InternalAreas)
            {
                if (directedPolygon.ApproxIntersects(internalArea, precision))
                {
                    return false;
                }
            }

            // Test to see if the directed polygon is contained within the external perimeter.

            return ExternalArea.ApproxContains(directedPolygon, precision);
        }

        /// <summary>
        /// Determines whether a layout area intersects a directed polygon. In order to intersect a polygon, the
        /// polygon must not be fully contained in an internal area and it must intersect the external area.
        /// </summary>
        /// <param name="polygon">The directed polygon to test</param>
        /// <returns>True if the directed polygon is intersects within the layout area, false otherwise</returns>
        internal bool Intersects(DirectedPolygon polygon)
        {
            // Test to see if the polygon intersects the external boundary of the layout area.

            if (!ExternalArea.Intersects(polygon))
            {
                return false;
            }

            // Test to see if the directed polygon is contained any internal area

            foreach (DirectedPolygon internalArea in InternalAreas)
            {
                if (internalArea.Contains(polygon))
                {
                    return false;
                }
            }

            return true;
        }

        private List<LayoutArea> generateLayoutAreas(List<DirectedPolygon> externalResultsList)
        { 
            List<LayoutArea> returnList = new List<LayoutArea>();

            if (InternalAreas.Count <= 0)
            {
                foreach (DirectedPolygon externalPolygon in externalResultsList)
                {
                    LayoutArea layoutArea = new LayoutArea(externalPolygon, AreaFinishLayers);

                    returnList.Add(layoutArea);
                }
            }

            else
            {
                foreach (DirectedPolygon externalPolygon in externalResultsList)
                {
                    List<DirectedPolygon> results = externalPolygon.Subtract(InternalAreas);

                    if (results.Count == 1)
                    {
                        returnList.Add(new LayoutArea(results[0], AreaFinishLayers));
                    }

                    else if (results.Count == 2)
                    {
                        if (results[0].Contains(results[1])) // this will be the most frequent case
                        {
                            returnList.Add(new LayoutArea(results[0], new List<DirectedPolygon>() { results[1] }));
                        }

                        else if (!results[0].Intersects(results[1])) // second most frequent case
                        {
                            returnList.Add(new LayoutArea(results[0], AreaFinishLayers));
                            returnList.Add(new LayoutArea(results[1], AreaFinishLayers));
                        }

                        else if (results[1].Contains(results[0])) // should never happen
                        {
                            returnList.Add(new LayoutArea(results[1], new List<DirectedPolygon>() { results[0] }));
                        }

                        else
                        {
                            throw new NotImplementedException();
                        }
                    }

                    else
                    {
                        setExternalInternalPerimeterLists(results);
                        
                        foreach (DirectedPolygon externalPerimeter1 in externalPerimeters)
                        {
                            if (internalPerimeterDict.ContainsKey(externalPerimeter1))
                            {
                                returnList.Add(new LayoutArea(externalPerimeter1, internalPerimeterDict[externalPerimeter1]));
                            }

                            else
                            {
                                returnList.Add(new LayoutArea(externalPerimeter1, AreaFinishLayers));
                            }
                        }
                    }

                }
            }

            return returnList;
        }


        HashSet<DirectedPolygon> internalPerimeters;
        HashSet<DirectedPolygon> externalPerimeters;

        Dictionary<DirectedPolygon, List<DirectedPolygon>> internalPerimeterDict;

        private void setExternalInternalPerimeterLists(List<DirectedPolygon> polygonList)
        {

            externalPerimeters = new HashSet<DirectedPolygon>(polygonList);
            internalPerimeters = new HashSet<DirectedPolygon>();

            internalPerimeterDict = new Dictionary<DirectedPolygon, List<DirectedPolygon>>();

            for (int i = 0; i < polygonList.Count; i++)
            {
                DirectedPolygon p1 = polygonList[i];

                if (internalPerimeters.Contains(p1))
                {
                    continue;
                }

                for (int j = i + 1; j < polygonList.Count; j++)
                {
                    DirectedPolygon p2 = polygonList[j];

                    if (p1.Contains(p2))
                    {
                        updateContainmentLists(p1, p2, internalPerimeterDict, externalPerimeters, internalPerimeters);
                    }

                    else if (p2.Contains(p1))
                    {
                        updateContainmentLists(p2, p1, internalPerimeterDict, externalPerimeters, internalPerimeters);
                    }
                }
            }
              
        }

        private void updateContainmentLists(
            DirectedPolygon p1,
            DirectedPolygon p2,
            Dictionary<DirectedPolygon, List<DirectedPolygon>> internalPerimeterDict,
            HashSet<DirectedPolygon> externalPerimeters,
            HashSet<DirectedPolygon> internalPerimeters)
        {
            List<DirectedPolygon> pList = null;

            if (!internalPerimeterDict.ContainsKey(p1))
            {
                pList = new List<DirectedPolygon>();

                internalPerimeterDict.Add(p1, pList);
            }

            else
            {
                pList = internalPerimeterDict[p1];
            }

            pList.Add(p2);

            externalPerimeters.Remove(p2);
            internalPerimeters.Add(p2);
        }

        private Coordinate _centroid = Coordinate.NullCoordinate;

        public Coordinate Centroid
        {
            get
            {
                if (Coordinate.IsNullCoordinate(_centroid))
                {
                    _centroid = ExternalArea.Centroid();
                }

                return _centroid;
            }

            set
            {
                if (_centroid == value)
                {
                    return;
                }

                _centroid = value;
            }
        }
    }
}
