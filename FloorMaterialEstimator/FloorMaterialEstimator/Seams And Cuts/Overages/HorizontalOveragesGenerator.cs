//-------------------------------------------------------------------------------//
// <copyright file="OveragesGenerator.cs" company="Bruun Estimating, LLC">       // 
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
    using FloorMaterialEstimator.Test_and_Debug;
    using FloorMaterialEstimator.Utilities;

    public class HorizontalOverageGenerator
    {
        private Cut horizontalCut;
        private double overageWidthInInches;

        public HorizontalOverageGenerator(Cut horizontalCut, double overageWidthInInches)
        {
            this.horizontalCut = horizontalCut;
            this.overageWidthInInches = overageWidthInInches;
        }
        
        public List<Overage> GenerateOverages()
        {
            List<Overage> overageList = new List<Overage>();

            GenerateOveragesConnectingPointsAndLines();

            while (overageLineSegmentDict.Count > 0)
            {
                Overage overage = GenerateNextOverage();
                overageList.Add(overage);
            }

            overageList.ForEach(o => o.GenerateHorizontalEmbeddedCuts(overageWidthInInches));

            return overageList;
        }

        private Dictionary<Coordinate, OverageCoordinate> overageCoordinateDict = new Dictionary<Coordinate, OverageCoordinate>();
        private Dictionary<Tuple<Coordinate, Coordinate>, OverageLineSegment> overageLineSegmentDict = new Dictionary<Tuple<Coordinate, Coordinate>, OverageLineSegment>();

        private HashSet<Coordinate> upprLineCoords = new HashSet<Coordinate>();
        private HashSet<Coordinate> lowrLineCoords = new HashSet<Coordinate>();
        private HashSet<Coordinate> leftLineCoords = new HashSet<Coordinate>();
        private HashSet<Coordinate> rghtLineCoords = new HashSet<Coordinate>();

        private HashSet<Tuple<Coordinate, Coordinate>> excludedSegments = new HashSet<Tuple<Coordinate, Coordinate>>();

        private double upprBound;
        private double leftBound;
        private double rghtBound;
        private double lowrBound;

        Coordinate upperLeft;
        Coordinate lowerLeft;
        Coordinate upperRght;
        Coordinate lowerRght;

        private void GenerateOveragesConnectingPointsAndLines()
        {
            excludedSegments.Clear();

            overageLineSegmentDict.Clear();
            overageCoordinateDict.Clear();

            upprLineCoords.Clear();
            lowrLineCoords.Clear();
            leftLineCoords.Clear();
            rghtLineCoords.Clear();

            upperLeft = horizontalCut.CutPolygon.UpperLeftCorner;
            lowerLeft = horizontalCut.CutPolygon.LowerLeftCorner;
            upperRght = horizontalCut.CutPolygon.UpperRghtCorner;
            lowerRght = horizontalCut.CutPolygon.LowerRghtCorner;

            upprBound = upperLeft.Y;
            leftBound = upperLeft.X;
            rghtBound = lowerRght.X;
            lowrBound = lowerRght.Y;

            horizontalCut.CutPolygon.PolygonPerimeter.ForEach(p => BuildSegmentAndCoordinateLists(p));

            // Add in corner points to the coordinate set for each boundary

            AddToCoordHashSet(upperLeft, upprLineCoords);
            AddToCoordHashSet(upperRght, upprLineCoords);

            AddToCoordHashSet(lowerLeft, lowrLineCoords);
            AddToCoordHashSet(lowerRght, lowrLineCoords);

            AddToCoordHashSet(upperLeft, leftLineCoords);
            AddToCoordHashSet(lowerLeft, leftLineCoords);

            AddToCoordHashSet(upperRght, rghtLineCoords);
            AddToCoordHashSet(lowerRght, rghtLineCoords);

            // Generate (external) boundary lines factoring in elements of the original cut.

            GenerateHorizontalBoundaryLines(upperLeft, upperRght, upprLineCoords);
            GenerateHorizontalBoundaryLines(lowerLeft, lowerRght, lowrLineCoords);

            GenerateVerticalBoundaryLines(upperLeft, lowerLeft, leftLineCoords);
            GenerateVerticalBoundaryLines(upperRght, lowerRght, rghtLineCoords);

        }

        private void AddToCoordHashSet(Coordinate coord, HashSet<Coordinate> boundaryCoordSet)
        {
            if (!boundaryCoordSet.Contains(coord))
            {
                boundaryCoordSet.Add(coord);
            }
        }

        private void GenerateHorizontalBoundaryLines(Coordinate leftCoord, Coordinate rghtCoord, HashSet<Coordinate> coordSet)
        {
            Debug.Assert(leftCoord.Y == rghtCoord.Y);
            Debug.Assert(coordSet.Count > 0);

            double y = leftCoord.Y;

            List<double> xIntercepts = new List<double>();

            foreach (Coordinate coord in coordSet)
            {
                xIntercepts.Add(coord.X);
            }

            xIntercepts.Sort();

            int count = xIntercepts.Count;

            for (int i = 0; i < count - 1; i++)
            {
                double x1 = xIntercepts[i];
                double x2 = xIntercepts[i + 1];

                Coordinate coord1 = new Coordinate(x1, y);
                Coordinate coord2 = new Coordinate(x2, y);

                if (coord1 > coord2)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                Tuple<Coordinate, Coordinate> Key = new Tuple<Coordinate, Coordinate>(coord1, coord2);

                OverageLineSegment overageLineSegment;

                if (!excludedSegments.Contains(Key))
                {
                    overageLineSegment = new OverageLineSegment(coord1, coord2);
                    AddToInternalSegmentsAndCoordinates(overageLineSegment);
                }
            }
        }

        private void GenerateVerticalBoundaryLines(Coordinate upprCoord, Coordinate lowrCoord, HashSet<Coordinate> coordSet)
        {
            Debug.Assert(upprCoord.X == lowrCoord.X);
            Debug.Assert(coordSet.Count > 0);

            double x = upprCoord.X;

            List<double> yIntercepts = new List<double>();

            foreach (Coordinate coord in coordSet)
            {
                yIntercepts.Add(coord.Y);
            }

            yIntercepts.Sort();

            int count = yIntercepts.Count;

            for (int i = 0; i < count - 1; i++)
            {
                double y1 = yIntercepts[i];
                double y2 = yIntercepts[i + 1];

                Coordinate coord1 = new Coordinate(x, y1);
                Coordinate coord2 = new Coordinate(x, y2);

                if (coord1 > coord2)
                {
                    Utilities.Swap(ref coord1, ref coord2);
                }

                Tuple<Coordinate, Coordinate> Key = new Tuple<Coordinate, Coordinate>(coord1, coord2);

                OverageLineSegment overageLineSegment;

                if (!excludedSegments.Contains(Key))
                {
                    overageLineSegment = new OverageLineSegment(coord1, coord2);
                    AddToInternalSegmentsAndCoordinates(overageLineSegment);
                }
            }
        }

        private void BuildSegmentAndCoordinateLists(CutLineSegment cutLineSegment)
        {
            if (isOuterBoundarySegment(cutLineSegment))
            {
                AddToBoundarySegmentAndCoordinates(cutLineSegment);
            }

            else
            {
                OverageLineSegment overageLineSegment = new OverageLineSegment(cutLineSegment.Coord1, cutLineSegment.Coord2);

                AddToInternalSegmentsAndCoordinates(overageLineSegment);
            }
        }

        private bool isOuterBoundarySegment(CutLineSegment cutLineSegment)
        {
            if (cutLineSegment.IsHorizontal())
            {
                if (cutLineSegment.Coord1.Y == upprBound || cutLineSegment.Coord1.Y == lowrBound)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            if (cutLineSegment.IsVertical())
            {
                if (cutLineSegment.Coord1.X == leftBound || cutLineSegment.Coord1.X == rghtBound)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            return false;
        }

        private void AddToBoundarySegmentAndCoordinates(CutLineSegment cutLineSegment)
        {
            excludedSegments.Add(cutLineSegment.Key);

            Coordinate coord1 = cutLineSegment.Coord1;
            Coordinate coord2 = cutLineSegment.Coord2;

            if (cutLineSegment.IsHorizontal())
            {
                if (coord1.Y == upprBound)
                {
                    upprLineCoords.Add(coord1);
                    upprLineCoords.Add(coord2);
                }

                else if (coord1.Y == lowrBound)
                {
                    lowrLineCoords.Add(coord1);
                    lowrLineCoords.Add(coord2);
                }
            }

            else if (cutLineSegment.IsVertical())
            {
                if (coord1.X == leftBound)
                {
                    leftLineCoords.Add(coord1);
                    leftLineCoords.Add(coord2);
                }

                else if (coord1.X == rghtBound)
                {
                    rghtLineCoords.Add(coord1);
                    rghtLineCoords.Add(coord2);
                }
            }
        }

        private void AddToInternalSegmentsAndCoordinates(OverageLineSegment overageLineSegment)
        {
            if (overageLineSegmentDict.ContainsKey(overageLineSegment.Key))
            {
                return;
            }

            overageLineSegmentDict.Add(overageLineSegment.Key, overageLineSegment);

            AddToOverageCoordinateDict(overageLineSegment);
            AddToOverageCoordinateDict(overageLineSegment);
        }

        private void AddToOverageCoordinateDict(OverageLineSegment overageLineSegment)
        {
            Coordinate coord1 = overageLineSegment.Coord1;
            Coordinate coord2 = overageLineSegment.Coord2;

            AddToOverageCoordinateDict(overageLineSegment, coord1);
            AddToOverageCoordinateDict(overageLineSegment, coord2);
        }

        private void AddToOverageCoordinateDict(OverageLineSegment overageLineSegment, Coordinate coord)
        {
            OverageCoordinate overageCoordinate;

            if (overageCoordinateDict.ContainsKey(coord))
            {
                overageCoordinate = overageCoordinateDict[coord];
            }

            else
            {
                overageCoordinate = new OverageCoordinate(coord);

                overageCoordinateDict.Add(coord, overageCoordinate);
            }

            overageCoordinate.Add(overageLineSegment);
        }

        private Overage GenerateNextOverage()
        {
            Overage overage = new Overage();

            OverageLineSegment overageLineSegment = getStartOverageLineSegment();

            OverageCoordinate startOverageCoordinate = overageCoordinateDict[overageLineSegment.Coord1];

            overage.Add(overageLineSegment);

            OverageCoordinate overageCoordinate = getNextOverageCoordinate(overageLineSegment, startOverageCoordinate);

            RemoveOverageLineSegment(overageLineSegment);

            while (overageCoordinate != startOverageCoordinate)
            {
                overageLineSegment = getNextOverageLineSegment(overageCoordinate, overageLineSegment);

                overage.Add(overageLineSegment);

                overageCoordinate = getNextOverageCoordinate(overageLineSegment, overageCoordinate);

                RemoveOverageLineSegment(overageLineSegment);
            }

            return overage;
        }

        private void RemoveOverageLineSegment(OverageLineSegment overageLineSegment)
        {
            OverageCoordinate coord1 = overageCoordinateDict[overageLineSegment.Coord1];
            OverageCoordinate coord2 = overageCoordinateDict[overageLineSegment.Coord2];

            overageLineSegmentDict.Remove(overageLineSegment.Key);

            coord1.Remove(overageLineSegment);
            coord2.Remove(overageLineSegment);
        }

        private OverageLineSegment getNextOverageLineSegment(OverageCoordinate overageCoordinate, OverageLineSegment overageLineSegment)
        {
            if (overageCoordinate.OverageLineSegmentDict.Count == 1)
            {
                return overageCoordinate.OverageLineSegmentDict.Values.ElementAt(0);
            }

            else
            {
                return getMinAtanSegment(overageCoordinate, overageLineSegment, overageCoordinate.OverageLineSegmentDict.Values);
            }
        }

        private OverageLineSegment getMinAtanSegment(OverageCoordinate overageCoordinate, OverageLineSegment overageLineSegment0, IEnumerable<OverageLineSegment> overageLineSegmentList)
        {
            double minAtan = double.MaxValue;

            Coordinate coord1_0 = overageLineSegment0.Coord1.Clone();
            Coordinate coord2_0 = overageLineSegment0.Coord2.Clone();

            if (coord2_0 == overageCoordinate.Coordinate)
            {
                Utilities.Swap(ref coord1_0, ref coord2_0);
            }

            double atan0 = MathUtils.Atan(coord1_0, coord2_0);

            OverageLineSegment minOverageLineSegment = null;

            foreach (OverageLineSegment overageLineSegment in overageLineSegmentList)
            {
                Coordinate coord1 = overageLineSegment.Coord1;
                Coordinate coord2 = overageLineSegment.Coord2;

                if (coord2 == overageCoordinate.Coordinate)
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
                    minOverageLineSegment = overageLineSegment;
                }
            }

            return minOverageLineSegment;
        }

        private OverageCoordinate getNextOverageCoordinate(OverageLineSegment overageLineSegment, OverageCoordinate overageCoordinate)
        {
            if (overageLineSegment.Coord1 == overageCoordinate.Coordinate)
            {
                return overageCoordinateDict[overageLineSegment.Coord2];
            }

            if (overageLineSegment.Coord2 == overageCoordinate.Coordinate)
            {
                return overageCoordinateDict[overageLineSegment.Coord1];
            }

            throw new NotImplementedException();
        }

        private OverageLineSegment getStartOverageLineSegment()
        {
            OverageLineSegment overageLineSegment;

            overageLineSegment = getUpprStartLineSegment();

            if (overageLineSegment != null)
            {
                return overageLineSegment;
            }

            overageLineSegment = getRghtStartLineSegment();

            if (overageLineSegment != null)
            {
                return overageLineSegment;
            }

            overageLineSegment = getLowrStartLineSegment();

            if (overageLineSegment != null)
            {
                return overageLineSegment;
            }

            overageLineSegment = getLeftStartLineSegment();

            if (overageLineSegment != null)
            {
                return overageLineSegment;
            }

            return null;
        }

        OverageLineSegment getUpprStartLineSegment()
        {
            var ls = overageLineSegmentDict.Values.
                Where(l => l.Coord1.Y == upprBound && l.Coord2.Y == upprBound);

            if (ls == null)
            {
                return null;
            }

            return ls.OrderBy(l => l.Coord1.X).First();
        }

        OverageLineSegment getRghtStartLineSegment()
        {
            var ls = overageLineSegmentDict.Values.
                 Where(l => l.Coord1.X == rghtBound && l.Coord2.X == rghtBound);

            if (ls == null)
            {
                return null;
            }

            return ls.OrderBy(l => l.Coord1.Y).First();
        }

        OverageLineSegment getLowrStartLineSegment()
        {
            var ls = overageLineSegmentDict.Values.
                 Where(l => l.Coord1.Y == lowrBound && l.Coord2.Y == lowrBound);

            if (ls == null)
            {
                return null;
            }

            return ls.OrderBy(l => -l.Coord1.X).First();
        }

        OverageLineSegment getLeftStartLineSegment()
        {
            var ls = overageLineSegmentDict.Values.
                 Where(l => l.Coord1.X == leftBound && l.Coord2.X == leftBound);

            if (ls == null)
            {
                return null;
            }

            return ls.OrderBy(l => -l.Coord1.Y).First();
        }
    }
}
