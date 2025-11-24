//-------------------------------------------------------------------------------//
// <copyright file="TestPolygons.cs" company="Bruun Estimating, LLC">            // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class TestPolygons
    {
        public static List<DirectedLine> case1LineList = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(2, 12), new Coordinate(8, 12)),
            new DirectedLine(new Coordinate(8, 12), new Coordinate(8, 10)),
            new DirectedLine(new Coordinate(8, 10), new Coordinate(2, 10)),
            new DirectedLine(new Coordinate(2, 10), new Coordinate(2, 12))
        };

        public static DirectedPolygon Case1DirectedPolygon = new DirectedPolygon(case1LineList);

        public static List<DirectedLine> case2LineList = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(2, 12), new Coordinate(14, 12)),
            new DirectedLine(new Coordinate(14, 12), new Coordinate(14, 8)),
            new DirectedLine(new Coordinate(14, 8), new Coordinate(8,12)),
            new DirectedLine(new Coordinate(8,12), new Coordinate(2,8)),
            new DirectedLine(new Coordinate(2,8), new Coordinate(2, 12))
        };

        public static DirectedPolygon Case2DirectedPolygon = new DirectedPolygon(case2LineList);

        public static List<DirectedLine> case2InternalAreaLineList = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(3, 10), new Coordinate(4, 10)),
            new DirectedLine(new Coordinate(4, 10), new Coordinate(4, 12)),
            new DirectedLine(new Coordinate(4, 12), new Coordinate(3,12)),
            new DirectedLine(new Coordinate(3,12), new Coordinate(3,10))
        };

        public static DirectedPolygon Case2InternalAreaDirectedPolygon = new DirectedPolygon(case2InternalAreaLineList);

        public static List<DirectedLine> case3LineList = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(2, 12), new Coordinate(14, 12)),
            new DirectedLine(new Coordinate(14, 12), new Coordinate(14, 7)),
            new DirectedLine(new Coordinate(14, 7), new Coordinate(8,11)),
            new DirectedLine(new Coordinate(8,11), new Coordinate(2,7)),
            new DirectedLine(new Coordinate(2,7), new Coordinate(2, 12))
        };

        public static DirectedPolygon Case3DirectedPolygon = new DirectedPolygon(case3LineList);

        public static List<DirectedLine> case4LineList = new List<DirectedLine>()
        {
            new DirectedLine(new Coordinate(2, 12), new Coordinate(4, 12)),
            new DirectedLine(new Coordinate(4, 12), new Coordinate(6, 10)),
            new DirectedLine(new Coordinate(6, 10), new Coordinate(8,10)),
            new DirectedLine(new Coordinate(8,10), new Coordinate(10,12)),
            new DirectedLine(new Coordinate(10,12), new Coordinate(12, 12)),
            new DirectedLine(new Coordinate(12,12), new Coordinate(12, 6)),
            new DirectedLine(new Coordinate(12,6), new Coordinate(10, 6)),
            new DirectedLine(new Coordinate(10,6), new Coordinate(8, 8)),
            new DirectedLine(new Coordinate(8,8), new Coordinate(6, 8)),
            new DirectedLine(new Coordinate(6,8), new Coordinate(4, 6)),
            new DirectedLine(new Coordinate(4,6), new Coordinate(2, 6)),
            new DirectedLine(new Coordinate(2,6), new Coordinate(2, 12))
        };

        public static DirectedPolygon Case4DirectedPolygon = new DirectedPolygon(case4LineList);

    }
}
