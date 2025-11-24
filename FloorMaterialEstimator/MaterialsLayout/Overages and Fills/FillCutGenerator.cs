#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: FillCutGenerator.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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

    public class FillCutGenerator
    {
        private List<Seam> seamList;

        public FillCutGenerator(List<Seam> seamList)
        {
            this.seamList = seamList;
        }

        SortedDictionary<double, SortedList<double, Seam>> seamsByLevel = new SortedDictionary<double, SortedList<double, Seam>>();

        public List<FillCut> GenerateFillCuts()
        {
            List<FillCut> returnList = new List<FillCut>();
            // Organize the seams by 'level' (Y value)

            seamList.ForEach(s => addToSeamsByLevel(s));

            // Pair up the seam lists by successive levels (y values) and generate embedded cuts

            int count = seamsByLevel.Count;

            for (int i = 0; i < count - 1; i++)
            {
                List<FillCut> fillCutListByLevel =
                    GenerateFillCutsByLevel(seamsByLevel.Values.ElementAt(i + 1), seamsByLevel.Values.ElementAt(i));

                returnList.AddRange(fillCutListByLevel);
            }

            return returnList;
        }

        private List<FillCut> GenerateFillCutsByLevel(SortedList<double, Seam> upperSeamList, SortedList<double, Seam> lowerSeamList)
        {
            List<FillCut> returnList = new List<FillCut>();

            if (upperSeamList is null)
            {
                return returnList;
            }

            if (upperSeamList.Count <= 0)
            {
                return returnList;
            }

            if (lowerSeamList is null)
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

            for (int i = 0; i < count; i += 2)
            {
                Coordinate upperLeft = new Coordinate(xValueList[i], y1);
                Coordinate lowerRght = new Coordinate(xValueList[i + 1], y2);

                FillCut fillCut = new FillCut(upperLeft, lowerRght);

                returnList.Add(fillCut);
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