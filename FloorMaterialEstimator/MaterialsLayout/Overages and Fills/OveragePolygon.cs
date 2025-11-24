#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: OveragePolygon.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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

    public class OveragePolygon: List<OverageLineSegment>
    {
        //public List<OverageLineSegment> PolygonPerimeter = new List<OverageLineSegment>();

        //internal void Add(OverageLineSegment overageLineSegment)
        //{
        //    PolygonPerimeter.Add(overageLineSegment) ;
        //}

        public new void Add(OverageLineSegment overageLineSegment)
        {
            if (Count <= 0)
            {
                base.Add(overageLineSegment);

                return;
            }

            OverageLineSegment lastLineSegment = this[Count - 1];

            Coordinate lastCoord = lastLineSegment.Coord2;

            if (overageLineSegment.Coord1 == lastCoord)
            {
                base.Add(overageLineSegment);

                return;
            }

            if (overageLineSegment.Coord2 == lastCoord)
            {
                base.Add(new OverageLineSegment(overageLineSegment.Coord2, overageLineSegment.Coord1));

                return;
            }

            throw new NotImplementedException();

        }

#if false
        internal List<DirectedLine> OrientedLineList()
        {
            List<DirectedLine> returnList = new List<DirectedLine>();

            ForEach(p => returnList.Add(((DirectedLine)p).Clone()));

            // Orient the coordinates in the perimeter lines

            int count = returnList.Count;

            DirectedLine prevLine = returnList[0];

            if (prevLine.Coord1 > prevLine.Coord2)
            {
                Utilities.Swap(ref prevLine.Coord1, ref prevLine.Coord2);
            }

            for (int i = 1; i < count; i++)
            {
                DirectedLine nextLine = returnList[i];

                if (nextLine.Coord1 != prevLine.Coord2)
                {
                    Utilities.Swap(ref nextLine.Coord1, ref nextLine.Coord2);

                    Debug.Assert(nextLine.Coord1 == prevLine.Coord2);
                }

                prevLine = nextLine;
            }

            Debug.Assert(returnList[0].Coord1 == returnList[count-1].Coord2);

            return returnList;
        }
#endif

        internal DirectedLine LongestHorizontalLine()
        {
            double maxLen = double.MinValue;
            DirectedLine maxLine = null;

            foreach (OverageLineSegment overageLineSegment in this)
            {
                if (!overageLineSegment.IsHorizontal())
                {
                    continue;
                }

                double len = overageLineSegment.Length;

                if (len > maxLen)
                {
                    maxLine = overageLineSegment.Clone();
                }
            }

            return maxLine;
        }

        internal double[] GetCoordinates()
        {
            if (Count <= 0)
            {
                return null;
            }

            double[] Coordinates = new double[2 * (Count + 1)];

            for (int i = 0, j = 0; i < Count; i++, j+=2)
            {
                Coordinates[j] = this[i].Coord1.X;
                Coordinates[j+1] = this[i].Coord1.Y;
            }

            Coordinates[2 * Count] = this[0].Coord1.X;
            Coordinates[2 * Count + 1] = this[0].Coord1.Y;

            return Coordinates;
        }
    }
}
