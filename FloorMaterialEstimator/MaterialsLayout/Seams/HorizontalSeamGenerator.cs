#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: HorizontalSeamGenerator.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Graphics;
    using Utilities;

    public class HorizontalSeamGenerator
    {
        private LayoutArea layoutArea;

        public List<DirectedLine> SeamList = new List<DirectedLine>();

        public HorizontalSeamGenerator(LayoutArea layoutArea)
        {
            this.layoutArea = layoutArea;
        }

        internal void GenerateSeams(double yOffset, double seamWidth, double graphicsTolerance, int keyTolerance)
        {
            Dictionary<double, List<DirectedLine>> wallList = generateHorizontalWallList(yOffset, seamWidth, graphicsTolerance, keyTolerance);


            Dictionary<double, List<DirectedLine>> seamList = generateHorizontalSeamList(yOffset, seamWidth, graphicsTolerance, keyTolerance);

            foreach (KeyValuePair<double, List<DirectedLine>> kvp in seamList)
            {
                double y = kvp.Key;

                List<DirectedLine> seamList1 = kvp.Value;

                if (!wallList.ContainsKey(y))
                {
                    SeamList.AddRange(seamList1);

                    continue;
                }

                List<DirectedLine> wallList1 = wallList[y];

                List<DirectedLine> inptList = new List<DirectedLine>(seamList1);
                List<DirectedLine> outpList = new List<DirectedLine>();

                foreach (DirectedLine wallLine in wallList1)
                {
                    outpList.Clear();

                    foreach (DirectedLine seamLine in inptList)
                    {
                        List<DirectedLine> seamOutputList = SeamNetOfLine(seamLine, wallLine, graphicsTolerance);

                        outpList.AddRange(seamOutputList);
                    }

                    inptList.Clear();

                    inptList.AddRange(outpList);
                }

                // Removing duplicates here is really stupid. Better to correctly generate non-intersecting
                // seams in generateHorizontalSeamList. For now, quick and dirty solution.

                SeamList.AddRange(removeDuplicatesAndOverlaps(outpList));
            }

            SeamList = removeDuplicates(SeamList);
        }

        //
        // Following assumes are lines are horizontal.
        //
        List<DirectedLine> removeDuplicates(List<DirectedLine> inptList)
        {
            //--------------------------------------------------------------------------//
            // inptList lines have the same value for Y, so only need to check X values //
            //--------------------------------------------------------------------------//

            if (inptList.Count <= 1)
            {
                return inptList;
            }

            if (inptList.Count == 2)
            {
                DirectedLine line1 = inptList[0];
                DirectedLine line2 = inptList[1];

                if (line1.Contains(line2))
                {
                    return new List<DirectedLine>() { line1 };
                }

                if (line2.Contains(line1))
                {
                    return new List<DirectedLine>() { line2 };
                }

                return new List<DirectedLine>() { line1, line2 };
            }

            List<DirectedLine> sortedList = new List<DirectedLine>(inptList);

            sortedList.Sort((l1, l2) => Math.Sign(l1.Coord1.Y - l2.Coord1.Y));
            

            bool[] isContained = new bool[inptList.Count];

            for (int i1 = 0; i1 < sortedList.Count - 1; i1++)
            {
                if (isContained[i1])
                {
                    continue;
                }

                DirectedLine line1 = sortedList[i1];

                double y1 = line1.Coord1.Y;

                for (int i2 = i1 + 1; i2 < sortedList.Count; i2++)
                {

                    if (isContained[i2])
                    {
                        continue;
                    }

                    DirectedLine line2 = sortedList[i2];

                    double y2 = line2.Coord1.Y;

                    if (y2 - y1 > 0.001)
                    {
                        break;
                    }

                    if (line1.Contains(line2))
                    {
                        isContained[i2] = true;
                    }

                    else if (line2.Contains(line1))
                    {
                        isContained[i1] = true;
                    }
                }
            }

            List<DirectedLine> outpList = new List<DirectedLine>();

            for (int i = 0; i < sortedList.Count; i++)
            {
                if (!isContained[i])
                {
                    outpList.Add(sortedList[i]);
                }
            }

            return outpList;
        }

        List<DirectedLine> removeDuplicatesAndOverlaps(List<DirectedLine> inptList)
        {
            //--------------------------------------------------------------------------//
            // inptList lines have the same value for Y, so only need to check X values //
            //--------------------------------------------------------------------------//

            if (inptList.Count <= 1)
            {
                return inptList;
            }

            if (inptList.Count == 2)
            {
                DirectedLine line1 = inptList[0];
                DirectedLine line2 = inptList[1];

                if (line1.ContainsByXDimension(line2))
                {
                    return new List<DirectedLine>() { line1 };
                }

                if (line2.ContainsByXDimension(line1))
                {
                    return new List<DirectedLine>() { line2 };
                }

                return new List<DirectedLine>() { line1, line2 };
            }


            List<DirectedLine> sortedList = new List<DirectedLine>(inptList);

            sortedList.Sort((l1, l2) => lineCompareByXVals(l1, l2));

            bool[] isOverlapped = new bool[sortedList.Count];

            List<DirectedLine> outpList = new List<DirectedLine>();

            for (int i1 = 0; i1 < sortedList.Count; i1++)
            {

                DirectedLine line1 = sortedList[i1];

                if (isOverlapped[i1])
                {
                    continue;
                }

                outpList.Add(line1);

                for (int i2 = i1 + 1; i2 < sortedList.Count; i2++)
                {
                    DirectedLine line2 = sortedList[i2];

                    if (isOverlapped[i2])
                    {
                        continue;
                    }

                    if (line1.ContainsByXDimension(line2))
                    {
                        isOverlapped[i2] = true;
                    }
                }
            }

            return outpList;
        }

        int lineCompareByXVals(DirectedLine line1, DirectedLine line2)
        {

            double x1_left = line1.Coord1.X;
            double x1_rght = line1.Coord2.X;

            double x2_left = line2.Coord1.X;
            double x2_rght = line2.Coord2.X;

            if (x1_left > x1_rght)
            {
                Utilities.Swap(ref x1_left, ref x1_rght);
            }

            if (x2_left > x2_rght)
            {
                Utilities.Swap(ref x2_left, ref x2_rght);
            }

            if (x1_left > x2_left)
            {
                return 1;
            }

            if (x1_left < x2_left)
            {
                return -1;
            }

            if (x1_rght > x2_rght)
            {
                return -1;
            }

            if (x1_rght < x2_rght)
            {
                return 1;
            }

            return 0;
        }

        private Tuple<double, double> generateKey(DirectedLine line)
        {
            double x1 = Math.Round(line.Coord1.X, 6);
            double x2 = Math.Round(line.Coord2.X, 6);

            if (x1 > x2)
            {
                return new Tuple<double, double>(x2, x1);
            }

            else
            {
                return new Tuple<double, double>(x1, x2);
            }
        }

        private List<DirectedLine> SeamNetOfLine(DirectedLine seamLine, DirectedLine wallLine, double graphicsTolerance)
        {
            Debug.Assert(seamLine.IsHorizontal(graphicsTolerance));
            Debug.Assert(wallLine.IsHorizontal(graphicsTolerance));
            Debug.Assert(Math.Abs(seamLine.Coord1.Y - wallLine.Coord1.Y) <= graphicsTolerance);

            double y = seamLine.Coord1.Y;

            // The following normalizes both the wall line and seam line so x1 < x2

            bool seamLineFlipped = false;

            double seamLineX1 = Math.Round(seamLine.Coord1.X, 8);
            double seamLineX2 = Math.Round(seamLine.Coord2.X, 8);

            if (seamLineX1 > seamLineX2)
            {
                seamLineFlipped = true;

                Utilities.Swap(ref seamLineX1, ref seamLineX2);
            }

            double wallLineX1 = Math.Round(wallLine.Coord1.X, 8);
            double wallLineX2 = Math.Round(wallLine.Coord2.X, 8);

            if (wallLineX1 > wallLineX2)
            {
                Utilities.Swap(ref wallLineX1, ref wallLineX2);
            }

            List<DirectedLine> returnList = new List<DirectedLine>();

            if (seamLineX2 - seamLineX1 <= graphicsTolerance)
            {
                // seam line is effectively zero length

                return returnList;
            }

            if (wallLineX2 - wallLineX1 <= graphicsTolerance)
            {
                // Wall is effectively zero length

                returnList.Add(seamLine);

                return returnList;
            }

            if (wallLineX2 < seamLineX1 || wallLineX1 > seamLineX2)
            {
                // Seam line does not overlap wall

                returnList.Add(seamLine);

                return returnList;
            }

            if (wallLineX1 <= seamLineX1 && wallLineX2 >= seamLineX2)
            {
                // Wall line contains seam line. Return empty list

                return returnList;
            }

            if (wallLineX1 <= seamLineX1 && wallLineX2 < seamLineX2)
            {
                // Deduct part of wall on the left

                if (Math.Abs(wallLineX2 - seamLineX2) <= graphicsTolerance)
                {
                    // result will be effectively zero length

                    return returnList;
                }

                DirectedLine updatedSeamLine = formUnnormalizedSeamLine(wallLineX2, seamLineX2, y, seamLineFlipped);

                returnList.Add(updatedSeamLine);

                return returnList;
            }

            if (wallLineX2 >= seamLineX2 && wallLineX1 > seamLineX1)
            {
                // Deduct part of wall on right

                if (Math.Abs(wallLineX1 - seamLineX1) <= graphicsTolerance)
                {
                    // result will be effectively zero length

                    return returnList;
                }

                DirectedLine updatedSeamLine = formUnnormalizedSeamLine(seamLineX1, wallLineX1, y, seamLineFlipped);

                returnList.Add(updatedSeamLine);

                return returnList;
            }

            Debug.Assert(seamLineX1 < wallLineX1 && seamLineX2 > wallLineX2);

            // wall splits seam

            if (Math.Abs(seamLineX1 - wallLineX1) > graphicsTolerance)
            {
                DirectedLine updatedSeamLine = formUnnormalizedSeamLine(seamLineX1, wallLineX1, y, seamLineFlipped);
                returnList.Add(updatedSeamLine);
            }

            if (Math.Abs(seamLineX2 - wallLineX2) > graphicsTolerance)
            {
                DirectedLine updatedSeamLine = formUnnormalizedSeamLine(seamLineX2, wallLineX2, y, seamLineFlipped);

                returnList.Add(updatedSeamLine);
            }

            return returnList;
        }

        private DirectedLine formUnnormalizedSeamLine(double x1, double x2, double y, bool seamLineFlipped)
        {
            if (seamLineFlipped)
            {
                Utilities.Swap(ref x1, ref x2);
            }

            return new DirectedLine(new Coordinate(x1, y), new Coordinate(x2, y));
        }

        private Dictionary<double, List<DirectedLine>> generateHorizontalWallList(double yBase, double seamWidth, double tolerance, int keyTolerance)
        {
            Dictionary<double, List<DirectedLine>> returnList = new Dictionary<double, List<DirectedLine>>();

            foreach (Tuple<double, DirectedLine> wall in generateHorizontalWallList(layoutArea.ExternalArea, yBase, seamWidth, tolerance, keyTolerance))
            {
                double y = wall.Item1;
                DirectedLine line = wall.Item2;

                if (!returnList.ContainsKey(y))
                {
                    returnList[y] = new List<DirectedLine>();
                }

                returnList[y].Add(line);
            }

            foreach (GraphicsDirectedPolygon polygon in layoutArea.InternalAreas)
            {
                foreach (Tuple<double, DirectedLine> wall in generateHorizontalWallList(polygon, yBase, seamWidth, tolerance, keyTolerance))
                {
                    double y = wall.Item1;
                    DirectedLine line = wall.Item2;

                    if (!returnList.ContainsKey(y))
                    {
                        returnList[y] = new List<DirectedLine>();
                    }

                    returnList[y].Add(line);
                }
            }

            return returnList;
        }

        private List<Tuple<double, DirectedLine>> generateHorizontalWallList(DirectedPolygon polygon, double yBase, double seamWidth, double tolerance, int keyTolerance)
        {
            List<Tuple<double, DirectedLine>> returnList = new List<Tuple<double, DirectedLine>>();

            foreach (DirectedLine line in polygon)
            {
                if (line.IsSeamable)
                {
                    continue;
                }

                if (!line.IsHorizontal(tolerance))
                {
                    continue;
                }

                double y = Math.Round(0.5 * (line.Coord1.Y + line.Coord2.Y), keyTolerance);

                returnList.Add(new Tuple<double, DirectedLine>(y, line));
            }

            return returnList;
        }

        private Dictionary<double, List<DirectedLine>> generateHorizontalSeamList(double yBase, double seamWidth, double tolerance, int keyTolerance)
        {
            Dictionary<double, List<DirectedLine>> returnList = new Dictionary<double, List<DirectedLine>>();

            List<DirectedPolygon> truncatedPolygonList = new List<DirectedPolygon>();

            foreach (Cut cut in layoutArea.CutList)
            {
                truncatedPolygonList.Clear();

                foreach (DirectedPolygon directedPolygon in cut.CutPolygonList)
                {
                    List<DirectedPolygon> polyList1 = directedPolygon.Intersect(layoutArea.ExternalArea);

                    if (layoutArea.InternalAreas.Count > 0)
                    {
                        foreach (DirectedPolygon poly1 in polyList1)
                        {
                            foreach (DirectedPolygon poly2 in layoutArea.InternalAreas)
                            {
                                truncatedPolygonList.AddRange(poly1.Subtract(poly2));
                            }
                        }
                    }

                    else
                    {
                        truncatedPolygonList.AddRange(polyList1);
                    }
                }

                foreach (DirectedPolygon truncatedPolygon in truncatedPolygonList)
                {
                    foreach (DirectedLine line in truncatedPolygon)
                    {

                        if (!line.IsHorizontal(1e-3))
                        {
                            // Only horizontal lines need to be considered as seams at this point.

                            continue;
                        }

                        //double y = Math.Round(0.5 * (line.Coord1.Y + line.Coord2.Y), keyTolerance);

                        double y = 0.5 * (line.Coord1.Y + line.Coord2.Y);

                        //double multDbl = y / seamWidth;

                        double multInt = Math.Round(y, keyTolerance);

                        //double err = Math.Abs(y - multInt);

                        //if (err > 1e-2)
                        //{
                        //    // Line must be on a roll width boundary

                        //    continue;
                        //}

                        if (!returnList.ContainsKey(multInt))
                        {
                            returnList.Add(multInt, new List<DirectedLine>());
                        }

                        double x1 = Math.Round(line.Coord1.X, keyTolerance);
                        double x2 = Math.Round(line.Coord2.X, keyTolerance);

                        if (x1 > x2)
                        {
                            Utilities.Swap(ref x1, ref x2);
                        }

                        y = Math.Round(y, 4);

                        Coordinate Coord1 = new Coordinate(x1, y);
                        Coordinate Coord2 = new Coordinate(x2, y);

                        DirectedLine directedLine = new DirectedLine(Coord1, Coord2);

                        // Danger here because it assumes that x1 < x2 in list and directed line.

                        if (!directedLine.IsInList(returnList[multInt]))
                        {
                            returnList[multInt].Add(directedLine);
                        }
                    }
                }
            }

            return returnList;
        }
    }
}
