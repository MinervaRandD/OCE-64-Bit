
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
    public class LayoutArea
    {
        public DirectedPolygon ExternalArea { get; set; }

        public List<DirectedPolygon> InternalAreas { get; set; } = new List<DirectedPolygon>();

        public void InternalAreasAdd(DirectedPolygon directedPolygon)
        {
            if (InternalAreas == null)
            {
                InternalAreas = new List<DirectedPolygon>();
            }

            InternalAreas.Add(directedPolygon);
        }

        CoordinatedList<SeamingBoundary> SeamingBoundaryList = new CoordinatedList<SeamingBoundary>();

        //public int ID = -1;

        //public static int IDGenerator = 1;

        public int CutIndex = -1;

        // Seam list coordination
        public CoordinatedList<Seam> SeamList { get; set; } = new CoordinatedList<Seam>();

        public List<Cut> CutList
        {
            get
            {
                List<Cut> rtrnList = new List<Cut>();

                RolloutList.ForEach(r => rtrnList.AddRange(r.CutList));

                return rtrnList;
            }
        }

        public List<Undrage> UndrageList
        {
            get
            {
                List<Undrage> rtrnList = new List<Undrage>();

                RolloutList.ForEach(r => rtrnList.AddRange(r.UndrageList));

                return rtrnList;
            }
        }

        public List<Rollout> RolloutList { get; set; } = new List<Rollout>();

        public double SeamWidthInInches { get; set; }

        public double[,] rotationMatrix;

        public double[,] inverseRotationMatrix;

        public void GenerateSeamsAndRollouts(DirectedLine baseLine, double offset, double seamWidthInInches)
        {
            double graphicsTolerance = Math.Pow(10.0, -GlobalSettings.GraphicsPrecision);

            SeamWidthInInches = seamWidthInInches;

            double theta = baseLine.Atan();

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

            Coordinate transformCoord = baseLine.Coord1;

            Transform(-transformCoord, rotationMatrix, theta);

            Overage.OverageIndexGenerator = 1;
            Undrage.UndrageIndexGenerator = 1;

            GenerateHorizontalSeamsAndRollouts(offset, seamWidthInInches, graphicsTolerance, -theta);

            InverseTransform(transformCoord, inverseRotationMatrix, -theta);
        }

        public void GenerateHorizontalSeamsAndRollouts(double yOffset, double seamWidth, double graphicsTolerance, double theta)
        {
            generateHorizontalRollout(yOffset, theta, seamWidth, graphicsTolerance);
            generateHorizontalSeams(yOffset, seamWidth, graphicsTolerance, 4);
        }

        private void generateHorizontalRollout(double yOffset, double theta, double seamWidth, double graphicsTolerance)
        {
            LayoutArea layoutArea = (LayoutArea)this;

            HorizontalRolloutGenerator horizontalRolloutGenerator = new HorizontalRolloutGenerator(layoutArea);

            horizontalRolloutGenerator.GenerateRollouts(theta, yOffset, seamWidth);

            RolloutList = horizontalRolloutGenerator.RolloutList;
        }

        private void generateHorizontalSeams(double yOffset, double seamWidth, double graphicsTolerance, int keyTolerance)
        {
            HorizontalSeamGenerator horizontalSeamGenerator = new HorizontalSeamGenerator(this);

            horizontalSeamGenerator.GenerateSeams(yOffset, seamWidth, graphicsTolerance, keyTolerance);

            foreach (DirectedLine line in horizontalSeamGenerator.SeamList)
            {
                SeamList.Add(new Seam(line));
            }
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

        public LayoutArea()
        {
            //ID = IDGenerator++;
        }

        public LayoutArea(int cutIndex)
        {
            //ID = IDGenerator++;

            this.CutIndex = cutIndex;
        }

        public LayoutArea(DirectedPolygon externalPolygon) 
        {
            //ID = IDGenerator++;

            ExternalArea = externalPolygon;
        }

        public LayoutArea(DirectedPolygon externalPolygon, List<DirectedPolygon> internalPerimenters)
        {
            //ID = IDGenerator++;

            ExternalArea = externalPolygon;
            InternalAreas = internalPerimenters;
        }

        //public LayoutArea(DirectedPolygon directedPolygon, int cutIndex) 
        //{
        //    ID = IDGenerator++;

        //    ExternalArea = directedPolygon;

        //    this.CutIndex = cutIndex;
        //}

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

                LayoutArea layoutArea1 = new LayoutArea(externalArea, internalAreas);

                rtrnList.Add(layoutArea1);
            }
           
            for (int i = 1; i < ilu.Count; i++)
            {
                rtrnList.Add(new LayoutArea(ilu[i]));
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
                rtrnList.Add(new LayoutArea(ilu[i]));
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
        //public List<LayoutArea> Subtract(List<LayoutArea> layoutAreaList)
        //{
        //    return new List<LayoutArea>();
        //}

        //public List<LayoutArea> Subtract(LayoutArea layoutArea)
        //{
        //    List<DirectedPolygon> externalAreaList = ExternalArea.Subtract(layoutArea.ExternalArea);


        //}

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
                    LayoutArea layoutArea = new LayoutArea(externalPolygon);

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
                        returnList.Add(new LayoutArea(results[0]));
                    }

                    else if (results.Count == 2)
                    {
                        if (results[0].Contains(results[1])) // this will be the most frequent case
                        {
                            returnList.Add(new LayoutArea(results[0], new List<DirectedPolygon>() { results[1] }));
                        }

                        else if (!results[0].Intersects(results[1])) // second most frequent case
                        {
                            returnList.Add(new LayoutArea(results[0]));
                            returnList.Add(new LayoutArea(results[1]));
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
                                returnList.Add(new LayoutArea(externalPerimeter1));
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

        public LayoutArea Clone()
        {
            DirectedPolygon externalArea = this.ExternalArea.Clone();

            List<DirectedPolygon> internalAreas = new List<DirectedPolygon>();

            foreach (DirectedPolygon internalArea in this.InternalAreas)
            {
                internalAreas.Add(internalArea.Clone());
            }

            LayoutArea clonedLayoutArea = new LayoutArea(externalArea, internalAreas);

            return clonedLayoutArea;
        }
    }
}
