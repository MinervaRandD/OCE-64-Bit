//-------------------------------------------------------------------------------//
// <copyright file="Overage.cs" company="Bruun Estimating, LLC">                // 
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

    public class Overage
    {
        public int OverageIndex;

        private int OverageIndexGenerator = 0;

        public OveragePolygon OveragePolygon = new OveragePolygon();

        public List<EmbeddedCut> EmbeddedCutList;

        public Overage() { OverageIndex = OverageIndexGenerator++; }

        internal void Add(OverageLineSegment overageLineSegment)
        {
            OveragePolygon.Add(overageLineSegment);
        }

        SortedDictionary<double, SortedList<double, Seam>> seamsByLevel = new SortedDictionary<double, SortedList<double, Seam>>();

        internal void GenerateHorizontalEmbeddedCuts(double overageWidthInInches)
        {
            EmbeddedCutList = new List<EmbeddedCut>();

            List<Line> lineList = OveragePolygon.OrientedLineList();

            Line line = OveragePolygon.LongestHorizontalLine();

            double baselineY = line.Coord1.Y;

            HorizontalSeamsGenerator horizontalSeamsGenerator = new HorizontalSeamsGenerator(lineList);

            List<Seam> horizontalSeamList = horizontalSeamsGenerator.GenerateHorizontalSeamList(overageWidthInInches, baselineY, false);

            // The seams generator currently does not generate seams for borders (walls)
            // The following is a kludge to add in top and bottom borders until this can be
            // fixed.

            int count = lineList.Count;

            for (int i = 0; i < count; i++)
            {
                Line borderLine = lineList[i];

                if (!borderLine.IsHorizontal())
                {
                    continue;
                }

                double dOffset = (borderLine.Coord1.Y - line.Coord1.Y) / overageWidthInInches;

                double iOffset = (int)Math.Round(dOffset);

                if (Math.Abs(dOffset - iOffset) <= 1.0e-12)
                {
                    Seam seam = new Seam(borderLine.Coord1, borderLine.Coord2, (i + count + 1) % count, (i + 1) % count);

                    horizontalSeamList.Add(seam);
                }
            }

            // Organize the seams by 'level' (Y value)

            horizontalSeamList.ForEach(s => addToSeamsByLevel(s));

            // Pair up the seam lists by successive levels (y values) and generate embedded cuts

            count = seamsByLevel.Count;

            for (int i = 0; i < count - 1; i++)
            {
                List<EmbeddedCut> embeddedCutList =
                    GenerateHorizontalEmbeddedCuts(seamsByLevel.Values.ElementAt(i+1), seamsByLevel.Values.ElementAt(i));

                EmbeddedCutList.AddRange(embeddedCutList);
            }
        }

        private List<EmbeddedCut> GenerateHorizontalEmbeddedCuts(SortedList<double, Seam> upperSeamList, SortedList<double, Seam> lowerSeamList)
        {
            List<EmbeddedCut> returnList = new List<EmbeddedCut>();

            if (upperSeamList == null)
            {
                return returnList;
            }

            if (upperSeamList.Count <= 0)
            {
                return returnList;
            }

            if (lowerSeamList == null)
            {
                return returnList;
            }

            if (lowerSeamList.Count <= 0)
            {
                return returnList;
            }

            HashSet<double> xValueSet = new HashSet<double>();

            double y1 = upperSeamList.Values.First().Coord1.Y;
            double y2 = lowerSeamList.Values.First().Coord1.Y;


            foreach (Seam seam in upperSeamList.Values)
            {
                double x1 = seam.Coord1.X;
                double x2 = seam.Coord2.X;

                if (isContainedInASeam(lowerSeamList, x1))
                {
                    if (!xValueSet.Contains(x1))
                    {
                        xValueSet.Add(x1);
                    }
                }

                if (isContainedInASeam(lowerSeamList, x2))
                {
                    if (!xValueSet.Contains(x2))
                    {
                        xValueSet.Add(x2);
                    }
                }
            }

            foreach (Seam seam in lowerSeamList.Values)
            {
                double x1 = seam.Coord1.X;
                double x2 = seam.Coord2.X;

                if (isContainedInASeam(upperSeamList, x1))
                {
                    if (!xValueSet.Contains(x1))
                    {
                        xValueSet.Add(x1);
                    }
                }

                if (isContainedInASeam(upperSeamList, x2))
                {
                    if (!xValueSet.Contains(x2))
                    {
                        xValueSet.Add(x2);
                    }
                }
            }

            List<double> xValueList = new List<double>(xValueSet);

            Debug.Assert(xValueList.Count % 2 == 0);

            xValueList.Sort();

            int count = xValueList.Count;

            for (int i = 0; i < count; i+= 2)
            {
                Coordinate upperLeft = new Coordinate(xValueList[i], y1);
                Coordinate lowerRght = new Coordinate(xValueList[i + 1], y2);

                EmbeddedCut embeddedCut = new EmbeddedCut(upperLeft, lowerRght);

                returnList.Add(embeddedCut);
            }

            return returnList;
        }

        private bool isContainedInASeam(SortedList<double, Seam> seamList, double x)
        {
            foreach (Seam seam in seamList.Values)
            {
                if (x < seam.Coord1.X)
                {
                    return false;
                }

                if (x >= seam.Coord1.X && x <= seam.Coord2.X)
                {
                    return true;
                }
            }

            return false;
        }

        private void addToSeamsByLevel(Seam seam)
        {
            Debug.Assert(seam.Coord1.Y == seam.Coord2.Y);

            double level = seam.Coord1.Y;

            SortedList<double, Seam> seamList = null;

            if (!seamsByLevel.ContainsKey(level))
            {
                seamList = new SortedList<double, Seam>();

                seamsByLevel.Add(level, seamList);
            }

            else
            {
                seamList = seamsByLevel[level];
            }

            seamList.Add(seam.Coord1.X, seam);
        }
    }
}
