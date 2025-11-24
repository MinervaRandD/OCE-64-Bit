//-------------------------------------------------------------------------------//
// <copyright file="OverageLineSegment.cs" company="Bruun Estimating, LLC">      // 
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

    public class OveragePolygon
    {
        public List<OverageLineSegment> PolygonPerimeter = new List<OverageLineSegment>();

        internal void Add(OverageLineSegment overageLineSegment)
        {
            PolygonPerimeter.Add(overageLineSegment) ;
        }

        internal List<Line> OrientedLineList()
        {
            List<Line> returnList = new List<Line>();

            PolygonPerimeter.ForEach(p => returnList.Add(((Line)p).Clone()));

            // Orient the coordinates in the perimeter lines

            int count = returnList.Count;

            Line prevLine = returnList[0];

            if (prevLine.Coord1 > prevLine.Coord2)
            {
                Utilities.Swap(ref prevLine.Coord1, ref prevLine.Coord2);
            }

            for (int i = 1; i < count; i++)
            {
                Line nextLine = returnList[i];

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

        internal Line LongestHorizontalLine()
        {
            double maxLen = double.MinValue;
            Line maxLine = null;

            foreach (OverageLineSegment overageLineSegment in PolygonPerimeter)
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
    }
}
