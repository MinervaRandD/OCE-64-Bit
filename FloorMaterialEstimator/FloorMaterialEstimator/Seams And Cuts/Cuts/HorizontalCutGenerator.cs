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

namespace FloorMaterialEstimator.Seams_And_Cuts
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

    public class HorizontalCutGenerator
    {
        private List<Cut> HorizontalCutList;

        private Dictionary<int, CutLineSegment> cutLineSegmentDict = new Dictionary<int, CutLineSegment>();
        private Dictionary<Coordinate, CutCoordinate> cutCoordinateDict = new Dictionary<Coordinate, CutCoordinate>();
        private List<CutCoordinate> cutCoordinateList = new List<CutCoordinate>();
        private List<CutLineSegment> cutLineSegmentList = new List<CutLineSegment>();

        HashSet<int> usedCutLineSegments = new HashSet<int>(); // to avoid backtracking.

        private List<Line> transformedPerimeter;
        private List<Seam> transformedSeamList;

        private double minY;
        private double maxY;

        private double rollWidthInInches;

        public HorizontalCutGenerator(List<Seam> transformedSeamList, List<Line> perimeter)
        {
            this.transformedSeamList = transformedSeamList;
            this.transformedPerimeter = perimeter;
        }

        public List<Cut> GenerateHorizontalCuts(double rollWidthInInches)
        {
            this.rollWidthInInches = rollWidthInInches;

            HorizontalCutList = new List<Cut>();

            minY = transformedPerimeter.Min(l => Math.Min(l.Coord1.Y, l.Coord2.Y));
            maxY = transformedPerimeter.Max(l => Math.Max(l.Coord1.Y, l.Coord2.Y));

            double y = 0.0;

            while (y < maxY + rollWidthInInches)
            {
                List<Cut> yCutList = generateCutsPerLevel(y);

                HorizontalCutList.AddRange(yCutList);

                y += rollWidthInInches;
            }

            y = -rollWidthInInches;

            while (y > minY)
            {
                List<Cut> yCutList = generateCutsPerLevel(y);

                HorizontalCutList.AddRange(yCutList);

                y -= rollWidthInInches;
            }
            
            return HorizontalCutList;
        }

        private List<Cut> generateCutsPerLevel(double y1)
        {
            double y2 = y1 - rollWidthInInches;

            List<Cut> returnCutList = new List<Cut>();

            GenerateCutCoordinates(y1, y2);

            List<CutLineSegment> cutLineSegmentList = GenerateCutPerimeter();

            while (cutLineSegmentList != null)
            {
                CutPolygon cutPolygon = new CutPolygon(cutLineSegmentList);

                cutPolygon.GenerateBoundaries(y1, y2);

                Cut cut = new Cut(cutPolygon, y1, y2);

                returnCutList.Add(cut);

                cutLineSegmentList = GenerateCutPerimeter();
            }

            return returnCutList;
        }

        private List<CutLineSegment> GenerateCutPerimeter()
        {
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

                Line line = cutLineSegmentFromList.OriginalLine;
                int lineIndex = cutLineSegmentFromList.LineIndex;

                Coordinate coord1 = cutCoordinateList[i].Coordinate;
                Coordinate coord2 = cutCoordinateList[i + 1].Coordinate;

                CutLineSegment newLineSegment = new CutLineSegment(line, lineIndex, coord1, coord2);

                returnList.Add(newLineSegment);
            }

            cutLineSegmentList.ForEach(s => cutLineSegmentDict.Remove(s.LineIndex));
            //cutCoordinateList.ForEach(c => cutCoordinateDict.Remove(c.Coordinate));

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

            if (coord1.Y == coord2.Y)
            {
                return getMinAtanSegment(cutCoordinate.CutLineSegmentList);
            }

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

            double atan0 = MathUtils.Atan(coord1_0, coord2_0);

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

                double atan = (MathUtils.Atan(coord1, coord2) - atan0);

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

            if (maxC == null)
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
                CutLineSegment cutLineSegment = new CutLineSegment(null, seam.SeamIndex, seam.Coord1, seam.Coord2);

                AddToCutLineSegmentDict(cutLineSegment.LineIndex, cutLineSegment);

                CutLineSegment leftLineSegment = GetCutLineSegmentDown(seam.Line1Index, y1, y2);
#if DEBUG
                if (leftLineSegment.Coord1 == leftLineSegment.Coord2)
                {
                    Debug.WriteLine("Invalid cut line segment: ");
                    Test_and_Debug.TestOutput.DumpCutLineSegment(leftLineSegment);

                    throw new NotImplementedException();
                }
#endif
                CutLineSegment rghtLineSegment = GetCutLineSegmentDown(seam.Line2Index, y1, y2);
#if DEBUG
                if (rghtLineSegment.Coord1 == rghtLineSegment.Coord2)
                {
                    Debug.WriteLine("Invalid cut line segment: ");
                    Test_and_Debug.TestOutput.DumpCutLineSegment(rghtLineSegment);

                    throw new NotImplementedException();
                }
#endif

            }

            foreach (Seam seam in Level2SeamList)
            {
                CutLineSegment cutLineSegment = new CutLineSegment(null, seam.SeamIndex, seam.Coord1, seam.Coord2);

                AddToCutLineSegmentDict(cutLineSegment.LineIndex, cutLineSegment);

                CutLineSegment leftLineSegment = GetCutLineSegmentUp(seam.Line1Index, y1, y2);
#if DEBUG
                if (leftLineSegment.Coord1 == leftLineSegment.Coord2)
                {
                    Debug.WriteLine("Invalid cut line segment: ");
                    Test_and_Debug.TestOutput.DumpCutLineSegment(leftLineSegment);

                    throw new NotImplementedException();
                }
#endif
                CutLineSegment rghtLineSegment = GetCutLineSegmentUp(seam.Line2Index, y1, y2);
#if DEBUG
                if (rghtLineSegment.Coord1 == rghtLineSegment.Coord2)
                {
                    Debug.WriteLine("Invalid cut line segment: ");
                    Test_and_Debug.TestOutput.DumpCutLineSegment(rghtLineSegment);

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
                    Test_and_Debug.TestOutput.DumpCutLineSegment(cutLineSegment);

                    throw new NotImplementedException();
                }

            }
#endif
            int count = transformedPerimeter.Count;

            for (int i = 0; i < count; i++)
            {
                if (cutLineSegmentDict.ContainsKey(i))
                {
                    continue;
                }

                Line currLine = transformedPerimeter[i];
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

                AddToCutLineSegmentDict(cutLineSegment.LineIndex, cutLineSegment);
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
                    Test_and_Debug.TestOutput.DumpCutCordinate(cutCoordinate);
                    Debug.WriteLine("");

                    Test_and_Debug.TestOutput.DumpCutCoordinateDict(cutCoordinateDict);
                    Test_and_Debug.TestOutput.DumpCutLineSegmentDict(cutLineSegmentDict);
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

                    Seam newSeam = new Seam(coord1, coord2, lineIndex1, lineIndex2);

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
                Line line = transformedPerimeter[i];

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

                    Seam newSeam = new Seam(coord1, coord2, lineIndex1, lineIndex2);

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
                Line line = transformedPerimeter[i];

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

        private void AddParallelCutLineSegment(double y, Line line, int lineIndex)
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
            CutLineSegment newCutLineSegment = new CutLineSegment(line, lineIndex, lineCoord1, lineCoord2);

            AddToCutLineSegmentDict(newCutLineSegment.LineIndex, newCutLineSegment);
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
            if (cutLineSegmentDict.ContainsKey(lineIndex))
            {
                return cutLineSegmentDict[lineIndex];
            }

            Line line = transformedPerimeter[lineIndex];

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

            AddToCutLineSegmentDict(lineIndex, cutLineSegment);

            return cutLineSegment;
        }

        private CutLineSegment GetCutLineSegmentUp(int lineIndex, double y1, double y2)
        {
            if (cutLineSegmentDict.ContainsKey(lineIndex))
            {
                return cutLineSegmentDict[lineIndex];
            }

            Line line = transformedPerimeter[lineIndex];

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

            AddToCutLineSegmentDict(lineIndex, cutLineSegment);

            return cutLineSegment;
        }

        private void AddToCutLineSegmentDict(int index, CutLineSegment cutLineSegment)
        {
            if (!cutLineSegment.IsNormalized())
            {
                throw new NotImplementedException();
            }

            cutLineSegmentDict.Add(index, cutLineSegment);
        }
    }
}

