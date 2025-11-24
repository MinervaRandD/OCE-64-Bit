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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;

    public class OverageCoordinate
    {
        public Dictionary<Tuple<Coordinate, Coordinate>, OverageLineSegment> OverageLineSegmentDict = new Dictionary<Tuple<Coordinate, Coordinate>, OverageLineSegment>();

        public Coordinate Coordinate;
        
        public OverageCoordinate(Coordinate coordinate)
        {
            Coordinate = coordinate.Clone();
        }

        public void Add(OverageLineSegment overageLineSegment)
        {
            if (!OverageLineSegmentDict.ContainsKey(overageLineSegment.Key))
            {
                OverageLineSegmentDict.Add(overageLineSegment.Key, overageLineSegment);
            }
        }

        internal void Remove(OverageLineSegment overageLineSegment)
        {
            OverageLineSegmentDict.Remove(overageLineSegment.Key);
        }

        public static bool operator ==(OverageCoordinate o1, OverageCoordinate o2)
        {
            return o1.Coordinate == o2.Coordinate;
        }

        public static bool operator !=(OverageCoordinate o1, OverageCoordinate o2)
        {
            return o1.Coordinate != o2.Coordinate;
        }
    }
}
