//-------------------------------------------------------------------------------//
// <copyright file="HorizontalCutGenerator.cs" company="Bruun Estimating, LLC">  // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace MaterialsLayout
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Utilities;

    public class HorizontalCutGeneratorOld
    {
        private List<Cut> HorizontalCutList;

        //private Dictionary<int, CutLineSegment> cutLineSegmentDictOld = new Dictionary<int, CutLineSegment>();
        private Dictionary<Tuple<Coordinate, Coordinate>, CutLineSegment> cutLineSegmentDict = new Dictionary<Tuple<Coordinate, Coordinate>, CutLineSegment>();

        private Dictionary<Coordinate, CutCoordinate> cutCoordinateDict = new Dictionary<Coordinate, CutCoordinate>();
        private List<CutCoordinate> cutCoordinateList = new List<CutCoordinate>();
        private List<CutLineSegment> cutLineSegmentList = new List<CutLineSegment>();

        HashSet<int> usedCutLineSegments = new HashSet<int>(); // to avoid backtracking.

        private List<DirectedLine> transformedPerimeter;
        private List<Seam> transformedSeamList;

        private double minY;
        private double maxY;

        private double rollWidthInInches;
        private LayoutArea layoutArea;

        public HorizontalCutGeneratorOld(List<Seam> transformedSeamList, List<DirectedLine> perimeter)
        {
            this.transformedSeamList = transformedSeamList;
            this.transformedPerimeter = perimeter;
        }

        public HorizontalCutGeneratorOld(LayoutArea layoutArea)
        {
            
        }

        private List<CutLineSegment> GenerateCutPerimeter()
        {
            //if (cutLineSegmentDictOld.Count <= 0)
            if (cutLineSegmentDict.Count <= 0)
            {
                return null;
            }

            cutCoordinateList.Clear();
            cutLineSegmentList.Clear();
            usedCutLineSegments.Clear();

            List<CutLineSegment> returnList = new List<CutLineSegment>();

            CutCoordinate startCoord = GetStartCoord();

            cutCoordinateList.Add(startCoord);

            CutLineSegment cutLineSegment = GetInitialCutLineSegment(startCoord);

            addToCutLineSegmentList(cutLineSegment);

            CutCoordinate cutCoordinate = GetNextCutCoordinate(cutLineSegment, startCoord);

            while (startCoord != cutCoordinate)
            {
                cutCoordinateList.Add(cutCoordinate);

                cutLineSegment = GetNextCutLineSegment(cutLineSegment, cutCoordinate);

                addToCutLineSegmentList(cutLineSegment);

                cutCoordinate = GetNextCutCoordinate(cutLineSegment, cutCoordinate);
            }

            cutCoordinateList.Add(cutCoordinate);

            int count = cutCoordinateList.Count - 1;

            Debug.Assert(count == cutLineSegmentList.Count);

            for (int i = 0; i < count; i++)
            {
                CutLineSegment cutLineSegmentFromList = this.cutLineSegmentList[i];

                DirectedLine line = cutLineSegmentFromList.OriginalLine;
                int lineIndex = cutLineSegmentFromList.LineIndex;

                Coordinate coord1 = cutCoordinateList[i].Coordinate;
                Coordinate coord2 = cutCoordinateList[i + 1].Coordinate;

                CutLineSegment newLineSegment = new CutLineSegment(line, lineIndex, coord1, coord2);

                returnList.Add(newLineSegment);
            }

            cutLineSegmentList.ForEach(s => RemoveFromCutLineSegmentDict(s)); // cutLineSegmentDictOld.Remove(s.LineIndex));
       
            cutCoordinateList.ForEach(c => { if (c.CutLineSegmentList.Count <= 0) cutCoordinateDict.Remove(c.Coordinate); });

            return returnList;
        }

        private CutLineSegment GetNextCutLineSegment(CutLineSegment cutLineSegment, CutCoordinate cutCoordinate)
        {
            if (cutCoordinate.CutLineSegmentList.Count <= 0)
            {
                throw new NotImplementedException();
            }

            if (cutCoordinate.CutLineSegmentList.Count == 1)
            {
                return cutCoordinate.CutLineSegmentList[0];
            }

            Coordinate coord1 = cutLineSegment.Coord1;
            Coordinate coord2 = cutLineSegment.Coord2;

            //if (coord1.Y == coord2.Y)
            //{
            //    return getMinAtanSegment(cutCoordinate.CutLineSegmentList);
            //}

            return getMinAtanSegment(cutCoordinate, cutLineSegment, cutCoordinate.CutLineSegmentList);

            // edge cases not implemented.

            throw new NotImplementedException();
        }

        private CutLineSegment getMinAtanSegment(List<CutLineSegment> cutLineSegmentList)
        {
            double minAtan = double.MaxValue;

            CutLineSegment minCutLineSegment = null;

            foreach (CutLineSegment cutLineSegment in cutLineSegmentList)
            {
                int lineIndex = cutLineSegment.LineIndex;

                if (lineIndex >= 0)
                {
                    double atan = transformedPerimeter[lineIndex].NormalizedAtan();

                    if (atan < minAtan)
                    {
                        minAtan = atan;
                        minCutLineSegment = cutLineSegment;
                    }
                }
            }

            return minCutLineSegment;
        }


        private CutLineSegment getMinAtanSegment(CutCoordinate cutCoordinate, CutLineSegment cutLineSegment0, List<CutLineSegment> cutLineSegmentList)
        {
            double minAtan = double.MaxValue;

            Coordinate coord1_0 = cutLineSegment0.Coord1;
            Coordinate coord2_0 = cutLineSegment0.Coord2;

            if (coord2_0 == cutCoordinate.Coordinate)
            {
                Utilities.Swap(ref coord1_0, ref coord2_0);
            }

            double atan0 = MathUtils.Atan(coord1_0.X, coord1_0.Y, coord2_0.X, coord2_0.Y);

            CutLineSegment minCutLineSegment = null;

            foreach (CutLineSegment cutLineSegment in cutLineSegmentList)
            {
                if (cutLineSegment.LineIndex == cutLineSegment0.LineIndex)
                {
                    continue;
                }

                Coordinate coord1 = cutLineSegment.Coord1;
                Coordinate coord2 = cutLineSegment.Coord2;

                if (coord2 == cutCoordinate.Coordinate)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                double atan = (MathUtils.Atan(coord1.X, coord1.Y, coord2.X, coord2.Y) - atan0);

                if (atan < 0.0)
                {
                    atan += MathUtils.c2PI;
                }

                if (atan < minAtan)
                {
                    minAtan = atan;
                    minCutLineSegment = cutLineSegment;
                }
            }

            return minCutLineSegment;
        }

        private CutCoordinate GetNextCutCoordinate(CutLineSegment cutLineSegment, CutCoordinate cutCoordinate)
        {
            Coordinate coord1 = cutLineSegment.Coord1;
            Coordinate coord2 = cutLineSegment.Coord2;

            if (cutCoordinate.Coordinate == coord1)
            {
                return cutCoordinateDict[coord2];
            }

            if (cutCoordinate.Coordinate == coord2)
            {
                return cutCoordinateDict[coord1];
            }

            throw new NotImplementedException();
        }

        private void addToCutLineSegmentList(CutLineSegment cutLineSegment)
        {
            Coordinate coord1 = cutLineSegment.Coord1;
            Coordinate coord2 = cutLineSegment.Coord2;

            int lineIndex = cutLineSegment.LineIndex;

            if (coord1 > coord2)
            {
                throw new NotImplementedException();
            }

            usedCutLineSegments.Add(lineIndex);

            CutCoordinate cutCoordinate1 = cutCoordinateDict[coord1];
            CutCoordinate cutCoordinate2 = cutCoordinateDict[coord2];

            cutCoordinate1.CutLineSegmentList.Remove(cutLineSegment);
            cutCoordinate2.CutLineSegmentList.Remove(cutLineSegment);

            cutLineSegmentList.Add(cutLineSegment);
        }

        private CutLineSegment getInitialCutLineSegment()
        {
            double minY = cutLineSegmentDict.Values.Min(c => Math.Min(c.Coord1.Y, c.Coord2.Y));

            List<CutLineSegment> l = cutLineSegmentDict.Values.Where(c => c.Coord1.Y == minY || c.Coord2.Y == minY).ToList();

            double minX = l.Min(c => Math.Min(c.Coord1.X, c.Coord2.X));

            return l.Where(c => c.Coord1.X == minX || c.Coord2.X == minX).First();
        }

        private CutCoordinate GetStartCoord()
        {
            Coordinate bestCoord = new Coordinate(double.MaxValue, double.MinValue);

            foreach (CutLineSegment cutLineSegment in cutLineSegmentDict.Values)
            {
                Coordinate coord = cutLineSegment.Coord1;

                if (coord.Y >= bestCoord.Y)
                {
                    if (coord.X < bestCoord.X)
                    {
                        bestCoord = coord;
                    }
                }
            }

            return cutCoordinateDict[bestCoord];
        }

        private CutLineSegment GetInitialCutLineSegment(CutCoordinate cutCoordinate)
        {
            double minAtan = double.MaxValue;

            CutLineSegment maxC = null;

            foreach (CutLineSegment cutLineSegment in cutCoordinate.CutLineSegmentList)
            {
                Coordinate testCoord;

                if (cutLineSegment.Coord1 == cutCoordinate.Coordinate)
                {
                    testCoord = cutLineSegment.Coord2;
                }

                else if (cutLineSegment.Coord2 == cutCoordinate.Coordinate)
                {
                    testCoord = cutLineSegment.Coord1;
                }

                else
                {
                    throw new NotImplementedException();
                }

                double deltaY = testCoord.Y - cutCoordinate.Coordinate.Y;
                double deltaX = testCoord.X - cutCoordinate.Coordinate.X;

                double atan = Math.Atan2(-deltaY, deltaX);

                if (atan < minAtan)
                {
                    maxC = cutLineSegment;
                    minAtan = atan;
                }
            }

            if (maxC is null)
            {
                throw new NotImplementedException();
            }

            return maxC;
        }

        private void GenerateCutCoordinates(double y1, double y2)
        {
            cutLineSegmentDict.Clear();
            cutCoordinateDict.Clear();
            cutLineSegmentList.Clear();

            List<Seam> Level1SeamList = GenerateLevel1SeamList(y1);
            List<Seam> Level2SeamList = GenerateLevel2SeamList(y2);

            foreach (Seam seam in Level1SeamList)
            {
                CutLineSegment cutLineSegment = new CutLineSegment(null, seam.ID, seam.Coord1, seam.Coord2);

                AddToCutLineSegmentDict(cutLineSegment);

                CutLineSegment leftLineSegment = GetCutLineSegmentDown(seam.Line1Index, y1, y2);
#if DEBUG
                if (leftLineSegment.Coord1 == leftLineSegment.Coord2)
                {
                    DebugSupport.DumpCutLineSegment("Invalid cut line segment: ", leftLineSegment);
                }
#endif
                CutLineSegment rghtLineSegment = GetCutLineSegmentDown(seam.Line2Index, y1, y2);
#if DEBUG
                if (rghtLineSegment.Coord1 == rghtLineSegment.Coord2)
                {
                    DebugSupport.DumpCutLineSegment("Invalid cut line segment: ", rghtLineSegment);
                }
#endif
            }

            foreach (Seam seam in Level2SeamList)
            {
                CutLineSegment cutLineSegment = new CutLineSegment(null, seam.ID, seam.Coord1, seam.Coord2);

                AddToCutLineSegmentDict(cutLineSegment);

                CutLineSegment leftLineSegment = GetCutLineSegmentUp(seam.Line1Index, y1, y2);
#if DEBUG
                if (leftLineSegment.Coord1 == leftLineSegment.Coord2)
                {
                    DebugSupport.DumpCutLineSegment("Invalid cut line segment: ", leftLineSegment);
                }
#endif
                CutLineSegment rghtLineSegment = GetCutLineSegmentUp(seam.Line2Index, y1, y2);
#if DEBUG
                if (rghtLineSegment.Coord1 == rghtLineSegment.Coord2)
                {
                    Debug.WriteLine("Invalid cut line segment: ");
                    DebugSupport.DumpCutLineSegment(rghtLineSegment);

                    throw new NotImplementedException();
                }
#endif
            }
#if DEBUG
            foreach (CutLineSegment cutLineSegment in cutLineSegmentDict.Values)
            {
                Coordinate coord1 = cutLineSegment.Coord1;
                Coordinate coord2 = cutLineSegment.Coord2;

                if (coord1 == coord2)
                {
                    Debug.WriteLine("Invalid cut line segment: ");
                    DebugSupport.DumpCutLineSegment(cutLineSegment);

                    throw new NotImplementedException();
                }

            }
#endif
            int count = transformedPerimeter.Count;

            for (int i = 0; i < count; i++)
            {
                if (cutLineSegmentDict.ContainsKey(transformedPerimeter[i].Key))
                {
                    continue;
                }

                DirectedLine currLine = transformedPerimeter[i];
                CutLineSegment cutLineSegment = null;

                if (currLine.Coord1.Y == y1 && currLine.Coord2.Y == y1 && y1 == maxY)
                {
                    AddParallelCutLineSegment(y1, currLine, i);

                    continue;
                }

                if (currLine.Coord1.Y == y2 && currLine.Coord2.Y == y2 && y2 == minY)
                {
                    AddParallelCutLineSegment(y2, currLine, i);

                    continue;
                }

                if (!currLine.FallsBetween(y1, y2))
                {
                    continue;
                }

                cutLineSegment = new CutLineSegment(currLine, i, currLine.Coord1, currLine.Coord2);

                AddToCutLineSegmentDict(cutLineSegment);
            }

            // Need to eliminate overlapping cut line segments

            foreach (CutLineSegment cutLineSegment in cutLineSegmentDict.Values)
            {
                Coordinate coord1 = cutLineSegment.Coord1;
                Coordinate coord2 = cutLineSegment.Coord2;

                AddCutCoordinate(coord1, cutLineSegment);
                AddCutCoordinate(coord2, cutLineSegment);
            }

#if DEBUG
            foreach (CutCoordinate cutCoordinate in cutCoordinateDict.Values)
            {
                if (cutCoordinate.CutLineSegmentList.Count < 2)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine("y1 = " + y1 + ", y2 = " + y2);
                    Debug.WriteLine("GenerateCutCoordinates fails on cut coordinate: ");
                    DebugSupport.DumpCutCordinate(cutCoordinate);
                    Debug.WriteLine("");

                    DebugSupport.DumpCutCoordinateDict(cutCoordinateDict);
                    //DebugSupport.DumpCutLineSegmentDict(cutLineSegmentDict);
                }
            }
#endif      
        }

        private List<Seam> GenerateLevel1SeamList(double y1)
        {
            List<Seam> returnSeamList = new List<Seam>();

            List<Seam> masterSeamList = GetTransformedSeamsAtLevel(y1);

            foreach (Seam seam in masterSeamList)
            {
                List<int> intersectingDownLineList = GetIntersectingDownLines(y1, seam.Coord1.X, seam.Coord2.X);

                if (intersectingDownLineList.Count <= 0)
                {
                    continue;
                }

                Debug.Assert(intersectingDownLineList.Count % 2 == 0);

                List<Tuple<double, int>> intersectionList = new List<Tuple<double, int>>();

                foreach (int lineIndex in intersectingDownLineList)
                {
                    double x = transformedPerimeter[lineIndex].XInterceptForY(y1);

                    intersectionList.Add(new Tuple<double, int>(x, lineIndex));
                }

                intersectionList.Sort(downLineSorter);

                int count = intersectionList.Count;

                for (int i = 0; i < count; i += 2)
                {
                    Tuple<double, int> t1 = intersectionList[i];
                    Tuple<double, int> t2 = intersectionList[i + 1];

                    Coordinate coord1 = new Coordinate(t1.Item1, y1);
                    Coordinate coord2 = new Coordinate(t2.Item1, y1);

                    int lineIndex1 = t1.Item2;
                    int lineIndex2 = t2.Item2;

                    Seam newSeam = new Seam(new DirectedLine(coord1, coord2), lineIndex1, lineIndex2);

                    returnSeamList.Add(newSeam);
                }
            }

            return returnSeamList;
        }

        private int downLineSorter(Tuple<double, int> t1, Tuple<double, int> t2)
        {
            if (t1.Item1 < t2.Item1)
            {
                return -1;
            }

            if (t1.Item1 > t2.Item1)
            {
                return 1;
            }

            double aTan1 = transformedPerimeter[t1.Item2].NormalizedAtan();
            double aTan2 = transformedPerimeter[t2.Item2].NormalizedAtan();

            if (aTan1 < aTan2)
            {
                return -1;
            }

            if (aTan1 > aTan2)
            {
                return 1;
            }

            return 0;
        }

        private List<int> GetIntersectingDownLines(double y, double x1, double x2)
        {
            List<int> returnLineList = new List<int>();

            double xMin = Math.Min(x1, x2);
            double xMax = Math.Max(x1, x2);

            for (int i = 0; i < transformedPerimeter.Count; i++)
            {
                DirectedLine line = transformedPerimeter[i];

                if (line.MinY < y && line.MaxY >= y)
                {
                    double x = line.XInterceptForY(y);

                    if (x >= xMin && x <= xMax)
                    {
                        returnLineList.Add(i);
                    }
                }
            }

            return returnLineList;
        }

        private List<Seam> GetTransformedSeamsAtLevel(double y)
        {
            List<Seam> returnSeamList = new List<Seam>();

            foreach (Seam seam in transformedSeamList)
            {
                if (seam.Coord1.Y == y && seam.Coord2.Y == y)
                {
                    returnSeamList.Add(seam);
                }
            }

            return returnSeamList;
        }

        private List<Seam> GenerateLevel2SeamList(double y2)
        {
            List<Seam> returnSeamList = new List<Seam>();

            List<Seam> masterSeamList = GetTransformedSeamsAtLevel(y2);

            foreach (Seam seam in masterSeamList)
            {
                List<int> intersectingUpLineList = GetIntersectingUpLines(y2, seam.Coord1.X, seam.Coord2.X);

                if (intersectingUpLineList.Count <= 0)
                {
                    continue;
                }

                Debug.Assert(intersectingUpLineList.Count % 2 == 0);

                List<Tuple<double, int>> intersectionList = new List<Tuple<double, int>>();

                foreach (int lineIndex in intersectingUpLineList)
                {
                    double x = transformedPerimeter[lineIndex].XInterceptForY(y2);

                    intersectionList.Add(new Tuple<double, int>(x, lineIndex));
                }

                intersectionList.Sort(upLineSorter);

                int count = intersectionList.Count;

                for (int i = 0; i < count; i += 2)
                {
                    Tuple<double, int> t1 = intersectionList[i];
                    Tuple<double, int> t2 = intersectionList[i + 1];

                    Coordinate coord1 = new Coordinate(t1.Item1, y2);
                    Coordinate coord2 = new Coordinate(t2.Item1, y2);

                    int lineIndex1 = t1.Item2;
                    int lineIndex2 = t2.Item2;

                    Seam newSeam = new Seam(new DirectedLine(coord1, coord2), lineIndex1, lineIndex2);

                    returnSeamList.Add(newSeam);
                }
            }

            return returnSeamList;
        }


        private int upLineSorter(Tuple<double, int> t1, Tuple<double, int> t2)
        {
            if (t1.Item1 > t2.Item1)
            {
                return -1;
            }

            if (t1.Item1 < t2.Item1)
            {
                return 1;
            }

            double aTan1 = transformedPerimeter[t1.Item2].NormalizedAtan();
            double aTan2 = transformedPerimeter[t2.Item2].NormalizedAtan();

            if (aTan1 > aTan2)
            {
                return -1;
            }

            if (aTan1 < aTan2)
            {
                return 1;
            }

            return 0;
        }

        private List<int> GetIntersectingUpLines(double y, double x1, double x2)
        {
            List<int> returnLineList = new List<int>();

            double xMin = Math.Min(x1, x2);
            double xMax = Math.Max(x1, x2);

            for (int i = 0; i < transformedPerimeter.Count; i++)
            {
                DirectedLine line = transformedPerimeter[i];

                if (line.MinY <= y && line.MaxY > y)
                {
                    double x = line.XInterceptForY(y);

                    if (x >= xMin && x <= xMax)
                    {
                        returnLineList.Add(i);
                    }
                }
            }

            return returnLineList;
        }

        private void AddParallelCutLineSegment(double y, DirectedLine line, int lineIndex)
        {
            Coordinate lineCoord1 = line.Coord1;
            Coordinate lineCoord2 = line.Coord2;

            if (lineCoord1.Y != y || lineCoord2.Y != y)
            {
                throw new NotImplementedException();
            }

            if (lineCoord1.X > lineCoord2.X)
            {
                Utilities.Swap(ref lineCoord1, ref lineCoord2);
            }

            foreach (CutLineSegment cutLineSegment in cutLineSegmentDict.Values)
            {
                Coordinate cutCoord1 = cutLineSegment.Coord1;
                Coordinate cutCoord2 = cutLineSegment.Coord2;


                if (cutCoord1.Y != y || cutCoord2.Y != y)
                {
                    continue;
                }

                if (cutCoord1.X > cutCoord2.X)
                {
                    Utilities.Swap(ref cutCoord1, ref cutCoord2);
                }

                if (cutCoord1.X <= lineCoord1.X && cutCoord2.X >= lineCoord2.X)
                {
                    // current line is contained in an existing cut segment

                    return;
                }

                if (lineCoord1.X < cutCoord1.X && lineCoord2.X > cutCoord1.X)
                {
                    throw new NotImplementedException();
                }

                if (lineCoord1.X < cutCoord2.X && lineCoord2.X > cutCoord2.X)
                {
                    throw new NotImplementedException();
                }

            }

#if DEBUG
            if (lineCoord1 == lineCoord2)
            {
                Debug.WriteLine("Invalid coordinate set for new cut line:");


                throw new NotImplementedException();
            }
#endif
            // May need to split this boundary if there are intersecting lines that meet at the boundary point.
            // This occurs because seams span sections with up wedges /\ and down wedges \/

            SortedDictionary<double, int> intersectionXPointsDict = new SortedDictionary<double, int>();

            foreach (CutLineSegment cutLineSegment in cutLineSegmentDict.Values)
            {
                Coordinate cutCoord1 = cutLineSegment.Coord1;
                Coordinate cutCoord2 = cutLineSegment.Coord2;

                if (cutCoord1.Y == y && cutCoord2.Y == y)
                {
                    continue;

                    //throw new NotImplementedException();
                }

                double x;

                if (cutCoord1.Y == y)
                {
                    x = cutCoord1.X;
                }

                else if (cutCoord2.Y == y)
                {
                    x = cutCoord2.X;
                }

                else
                {
                    continue;
                }
                
                if (x <= lineCoord1.X || x >= lineCoord2.X)
                {
                    continue;
                }

                if (intersectionXPointsDict.ContainsKey(x))
                {
                    intersectionXPointsDict[x]++;
                }

                else
                {
                    intersectionXPointsDict.Add(x, 1);
                }
            }

            if (intersectionXPointsDict.Count > 0)
            {
                double currX = lineCoord1.X;
                double nextX = 0;

                Coordinate currCoord = new Coordinate(currX, y);
                Coordinate nextCoord;

                CutLineSegment cls;

                for (int i = 0; i < intersectionXPointsDict.Count; i++)
                {
                    nextX = intersectionXPointsDict.Keys.ElementAt(i);

                    nextCoord = new Coordinate(nextX, y);

                    cls = new CutLineSegment(line, lineIndex, currCoord, nextCoord, true);

                    AddToCutLineSegmentDict(cls);

                    currX = nextX;

                    currCoord = nextCoord;
                }

                nextX = lineCoord2.X;

                nextCoord = new Coordinate(nextX, y);

                cls = new CutLineSegment(line, lineIndex, currCoord, nextCoord, true);

                AddToCutLineSegmentDict(cls);
            }

            else
            {
                CutLineSegment newCutLineSegment = new CutLineSegment(line, lineIndex, lineCoord1, lineCoord2);

                AddToCutLineSegmentDict(newCutLineSegment);
            }
        }

        private void AddCutCoordinate(Coordinate coord, CutLineSegment cutLineSegment)
        {
            CutCoordinate cutCoordinate;

            if (cutCoordinateDict.ContainsKey(coord))
            {
                cutCoordinate = cutCoordinateDict[coord];

            }

            else
            {
                cutCoordinate = new CutCoordinate(coord);
                cutCoordinateDict.Add(coord, cutCoordinate);
            }

            cutCoordinate.CutLineSegmentList.Add(cutLineSegment);
        }

        private CutLineSegment GetCutLineSegmentDown(int lineIndex, double y1, double y2)
        {
            DirectedLine line = transformedPerimeter[lineIndex];

            if (cutLineSegmentDict.ContainsKey(line.NormalizedKey))
            {
                return cutLineSegmentDict[line.NormalizedKey];
            }


            if (line.IsHorizontal())
            {
                throw new NotImplementedException();
                //return new CutLineSegment(line, lineIndex, line.Coord1, line.Coord2);
            }

            if (!line.HasPointsBelow(y1))
            {
                // The seam intersects a corner and the original index is from the line above the seam.
                // by construction, the next line in the sequence should be below or on the seam.

                return GetCutLineSegmentDown((lineIndex + 1) % transformedPerimeter.Count, y1, y2);
            }

            CutLineSegment cutLineSegment = new CutLineSegment(line, lineIndex, y1, y2);

            AddToCutLineSegmentDict(cutLineSegment);

            return cutLineSegment;
        }

        private CutLineSegment GetCutLineSegmentUp(int lineIndex, double y1, double y2)
        {
            DirectedLine line = transformedPerimeter[lineIndex];

            if (cutLineSegmentDict.ContainsKey(line.NormalizedKey))
            {
                return cutLineSegmentDict[line.NormalizedKey];
            }

            //if (line.IsZeroSlopeLine())
            //{
            //    throw new NotImplementedException();
            //    //return new CutLineSegment(line, lineIndex, line.Coord1, line.Coord2);
            //}

            if (!line.HasPointsAbove(y2))
            {
                // The seam intersects a corner and the original index is from the line above the seam.
                // by construction, the next line in the sequence should be below or on the seam.

                return GetCutLineSegmentUp((lineIndex + 1) % transformedPerimeter.Count, y1, y2);
            }


            CutLineSegment cutLineSegment = new CutLineSegment(line, lineIndex, y1, y2);

            if (cutLineSegmentDict.ContainsKey(cutLineSegment.Key))
            {
                return cutLineSegmentDict[cutLineSegment.Key];
            }

            AddToCutLineSegmentDict(cutLineSegment);

            return cutLineSegment;
        }

        private void AddToCutLineSegmentDict(CutLineSegment cutLineSegment)
        {
            if (!cutLineSegment.IsNormalized())
            {
                throw new NotImplementedException();
            }

            cutLineSegmentDict.Add(cutLineSegment.Key, cutLineSegment);
        }

        private void RemoveFromCutLineSegmentDict(CutLineSegment cutLineSegment)
        {
            if (!cutLineSegmentDict.ContainsKey(cutLineSegment.Key))
            {
                return;
            }

            cutLineSegmentDict.Remove(cutLineSegment.Key);
        }
    }
}

