//-------------------------------------------------------------------------------//
// <copyright file="GeometryUtils.cs" company="Bruun Estimating, LLC">           // 
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
    using Utilities;
    using ClipperLib;
    using System.Diagnostics;

    public static class GeometryUtils
    {
        public static double H0Dist(Coordinate c1, Coordinate c2)
        {
            return System.Math.Max(System.Math.Abs(c1.X - c2.X), System.Math.Abs(c1.Y - c2.Y));
        }

        internal static double H2Dist(Coordinate c1, Coordinate c2)
        {
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2.0) + Math.Pow(c1.Y - c2.Y, 2.0));
        }

        internal static double NormalizedAtan(Coordinate coord1, Coordinate coord2)
        {
            double maxX = Math.Max(coord1.X, coord2.X);
            double minX = Math.Min(coord1.X, coord2.X);

            double maxY = Math.Max(coord1.Y, coord2.Y);
            double minY = Math.Min(coord1.Y, coord2.Y);

            double deltaY = maxY - minY;
            double deltaX = maxX - minX;

            return Math.Atan2(deltaY, deltaX);

        }

        internal static double Atan(Coordinate coord1, Coordinate coord2)
        {

            double deltaY = coord2.Y - coord1.Y;
            double deltaX = coord2.X - coord1.X;

            return Math.Atan2(deltaY, deltaX);
        }

        private const double scale = 1.0e8;

        public static List<IntPoint> RectangleToPolygon(Rectangle rectangle)
        {
            List<IntPoint> returnList = new List<IntPoint>();

            double x0 = rectangle.UpperLeft.X;
            double y0 = rectangle.LowerRght.Y;

            double x1 = rectangle.LowerRght.X;
            double y1 = rectangle.LowerRght.Y;

            double x2 = rectangle.LowerRght.X;
            double y2 = rectangle.UpperLeft.Y;

            double x3 = rectangle.UpperLeft.X;
            double y3 = rectangle.UpperLeft.Y;

            returnList.Add(new IntPoint(x0 * scale, y0 * scale));
            returnList.Add(new IntPoint(x1 * scale, y1 * scale));
            returnList.Add(new IntPoint(x2 * scale, y2 * scale));
            returnList.Add(new IntPoint(x3 * scale, y3 * scale));

            return returnList;
        }

        public static List<DirectedPolygon> PolygonToDirectedPolygon(List<IntPoint> polygon)
        {
            List<DirectedPolygon> rtrnList = new List<DirectedPolygon>();

            const int precision = 12;

            int count = polygon.Count;

            if (count <= 0)
            {
                return rtrnList;
            }
            
            List<Coordinate> coordList = new List<Coordinate>();

            Coordinate coord = new Coordinate(
                                    Math.Round((double)polygon[0].X / scale, precision),
                                    Math.Round((double)polygon[0].Y / scale, precision));

            coordList.Add(coord);

            Coordinate lastCoord;
            Coordinate frstCoord;

            for (int i = 1; i < count; i++)
            {
                coord = new Coordinate(
                                    Math.Round((double)polygon[i].X / scale, precision),
                                    Math.Round((double)polygon[i].Y / scale, precision));

                lastCoord = coordList.Last();

                if (MathUtils.H0Dist(coord.X, coord.Y, lastCoord.X, lastCoord.Y) >= 10e-4)
                {
                    coordList.Add(coord);
                }
            }

            lastCoord = coordList.Last();
            frstCoord = coordList.First();

            if (MathUtils.H0Dist(frstCoord.X, frstCoord.Y, lastCoord.X, lastCoord.Y) < 10e-4)
            {
                coordList.RemoveAt(coordList.Count - 1);
            }

            // Because of rounding occaisionally a polygon will intersect itself. This needs to be fixed
            // by breaking the polygon into separate polygons. To be done some time in the future.

            List<List<Coordinate>> separatedCoordList = new List<List<Coordinate>>();

            if (ContainsRevistedCoordinate(coordList))
            {
                separatedCoordList = createSeparatePolygons(coordList);
            }

            else
            {
                separatedCoordList.Add(coordList);
            }

            foreach (List<Coordinate> separateCoordList in separatedCoordList)
            {
                rtrnList.Add(generateDirectedPolygon(separateCoordList));
            }

        
            return rtrnList;
        }

        private static DirectedPolygon generateDirectedPolygon(List<Coordinate> coordList)
        {
            List<DirectedLine> directedLineList = new List<DirectedLine>();

            for (int i = 0; i < coordList.Count; i++)
            {
                DirectedLine line = new DirectedLine(coordList[i], coordList[(i + 1) % coordList.Count]);

                directedLineList.Add(line);
            }

            if (ContainsZeroLengthLine(directedLineList))
            {
                throw new NotImplementedException();
            }

            DirectedPolygon directedPolygon = new DirectedPolygon(directedLineList);

            Debug.Assert(directedPolygon.HasValidPerimeter());

            return directedPolygon;
        }

        private static List<List<Coordinate>> createSeparatePolygons(List<Coordinate> coordList)
        {
            List<List<Coordinate>> rtrnList = new List<List<Coordinate>>();

            while (coordList.Count > 0)
            {
                rtrnList.Add(createSeparatePolygon(coordList));
            }

            return rtrnList;

        }

        private static List<Coordinate> createSeparatePolygon(List<Coordinate> coordList)
        {
            Dictionary<Coordinate, int> coordDict = new Dictionary<Coordinate, int>();

            List<Coordinate> rtrnList = new List<Coordinate>();

            for (int i = 0; i < coordList.Count; i++)
            {
                Coordinate coord = coordList[i];

                if (coordDict.ContainsKey(coord))
                {
                    rtrnList = createSeparatePolygon(coordList, coordDict[coord], i);

                    return rtrnList;
                }

                coordDict.Add(coord, i);
            }

            rtrnList.AddRange(coordList);

            coordList.Clear();

            return rtrnList;
        }

        private static List<Coordinate> createSeparatePolygon(List<Coordinate> coordList, int i1, int i2)
        {
            Debug.Assert(i2 > i1);

            List<Coordinate> rtrnList = new List<Coordinate>();

            for (int i = i1; i < i2; i++)
            {
                rtrnList.Add(coordList[i]);
            }

            coordList.RemoveRange(i1, i2 - i1);

            return rtrnList;
        }

        private static bool ContainsZeroLengthLine(List<DirectedLine> lineList)
        {
            foreach (DirectedLine line in lineList)
            {
                if (line.Length <= 10e-6)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsRevistedCoordinate(List<Coordinate> coordList)
        {
            HashSet<Coordinate> coordSet = new HashSet<Coordinate>();

            foreach (Coordinate coord in coordList)
            {
                if (coordSet.Contains(coord))
                {
                    return true;
                }

                coordSet.Add(coord);
            }

            return false;
        }

        public static List<IntPoint> DirectedPolygonToPolygon(DirectedPolygon directedPolygon)
        {
            List<IntPoint> returnList = new List<IntPoint>();

            foreach (DirectedLine line in directedPolygon)
            {
                returnList.Add(new IntPoint(line.Coord1.X * scale, line.Coord1.Y * scale));
            }

            return returnList;
        }

        public static List<DirectedPolygon> Intersection(DirectedPolygon p1, DirectedPolygon p2)
        {
            List<DirectedPolygon> returnList = new List<DirectedPolygon>();

            Clipper clipper = new Clipper();

            List<IntPoint> p1Clip = DirectedPolygonToPolygon(p1);
            List<IntPoint> p2Clip = DirectedPolygonToPolygon(p2);

            clipper.AddPath(p1Clip, PolyType.ptSubject, true);
            clipper.AddPath(p2Clip, PolyType.ptClip, true);

            List<List<IntPoint>> solutions = new List<List<IntPoint>>();

            solutions.Clear();

            bool succeeded = clipper.Execute(ClipType.ctIntersection, solutions, PolyFillType.pftNonZero, PolyFillType.pftNonZero);
            //bool succeeded = clipper.Execute(ClipType.ctIntersection, solutions, PolyFillType.pftPositive, PolyFillType.pftNegative);
            if (!succeeded)
            {
                return returnList;
            }

            foreach (List<IntPoint> solution in solutions)
            {
                List<DirectedPolygon> directedPolygonList = PolygonToDirectedPolygon(solution);

                foreach (DirectedPolygon directedPolygon in directedPolygonList)
                { 
                    if (directedPolygon.Count > 0)
                    {
                        returnList.Add(directedPolygon);
                    }
                } 
            }

            return returnList;
        }

        public static List<DirectedPolygon> ApproxIntersection(DirectedPolygon p1, DirectedPolygon p2, double intersectSizeLimit)
        {
            List<DirectedPolygon> initialList = Intersection(p1, p2);

            List<DirectedPolygon> rtrnList = new List<DirectedPolygon>();

            foreach (DirectedPolygon directedPolygon in initialList)
            {
                if (directedPolygon.AreaInSqrInches() >= intersectSizeLimit)
                {
                    rtrnList.Add(directedPolygon);
                }
            }
            return rtrnList;
        }

        public static List<DirectedPolygon> Subtract(DirectedPolygon p, DirectedPolygon s)
        {
            List<DirectedPolygon> returnList = new List<DirectedPolygon>();

            Clipper clipper = new Clipper();

            List<List<IntPoint>> solutions = new List<List<IntPoint>>();

            clipper.AddPath(DirectedPolygonToPolygon(p), PolyType.ptSubject, true);

            clipper.AddPath(DirectedPolygonToPolygon(s), PolyType.ptClip, true);

            solutions.Clear();

            bool succeeded = clipper.Execute(ClipType.ctDifference, solutions, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            if (!succeeded)
            {
                return returnList;
            }

            foreach (List<IntPoint> solution in solutions)
            {
                returnList.AddRange(PolygonToDirectedPolygon(solution));
            }

            return returnList;
        }

        public static List<DirectedPolygon> Difference(DirectedPolygon p, List<DirectedPolygon> s)
        {
            List<DirectedPolygon> returnList = new List<DirectedPolygon>();

            Clipper clipper = new Clipper();

            List<List<IntPoint>> solutions = new List<List<IntPoint>>();

            List<IntPoint> pClip = DirectedPolygonToPolygon(p);

            clipper.AddPath(pClip, PolyType.ptSubject, true);

            foreach (DirectedPolygon polygon in s)
            {
                clipper.AddPath(DirectedPolygonToPolygon(polygon), PolyType.ptClip, true);
            }

            solutions.Clear();

            bool succeeded = clipper.Execute(ClipType.ctDifference, solutions, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            if (!succeeded)
            {
                return returnList;
            }

            foreach (List<IntPoint> solution in solutions)
            {
                returnList.AddRange(PolygonToDirectedPolygon(solution));
            }

            return returnList;
        }

        public static List<DirectedPolygon> UnionOld(List<DirectedPolygon> pList)
        {
            // Note that this version of the union will only work if the overlapping polygon is added last.
            // The new version is more general but has not been tested, and will take longer to execute.

            List<DirectedPolygon> returnList = new List<DirectedPolygon>();

            Clipper clipper = new Clipper();

            List<List<IntPoint>> solutions = new List<List<IntPoint>>();

            List<IntPoint> pClip = DirectedPolygonToPolygon(pList[0]);
            clipper.AddPath(pClip, PolyType.ptSubject, true);

            for (int i = 1; i < pList.Count ; i++)
            {
                pClip = DirectedPolygonToPolygon(pList[i]);
                clipper.AddPath(pClip, PolyType.ptClip, true);
            }

            solutions.Clear();

            bool succeeded = clipper.Execute(ClipType.ctUnion, solutions, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            if (!succeeded)
            {
                return new List<DirectedPolygon>(pList);
            }

            if (solutions.Count == 0)
            {
                return new List<DirectedPolygon>();
            }

            foreach (List<IntPoint> solution in solutions)
            {
                returnList.AddRange(PolygonToDirectedPolygon(solution));
            }

            return returnList;
        }

        public static List<DirectedPolygon> UnionNew(List<DirectedPolygon> pList)
        {
            // A considerable amount of work is required here because clipper library doesn't correctly
            // handle situations where the subject does not intersect the clips.

            // First step is to form cliques of the polygons, where adjacency is defined by a positive intersection.

            List<DirectedPolygon> rtrnList = new List<DirectedPolygon>();

            List<HashSet<DirectedPolygon>> cliques = formCliques(pList);

            foreach (HashSet<DirectedPolygon> clique in cliques)
            {
                rtrnList.AddRange(cliqueUnion(clique));
            }

            return rtrnList;
        }

        public static List<DirectedPolygon> Union1(List<DirectedPolygon> pList)
        {
            // For this to work, the first element in the list must be adjacent to every other element in the list

            if (pList.Count <= 0)
            {
                return new List<DirectedPolygon>();
            }

            if (pList.Count == 1)
            {
                return new List<DirectedPolygon>() { pList[0] };
            }
#if DEBUG
            for (int i = 1; i < pList.Count; i++)
            {
                Debug.Assert(pList[0].Intersects(pList[i]));
            }
#endif
            List<DirectedPolygon> rtrnList = listUnion(pList);

            return rtrnList;
        }

        private static List<DirectedPolygon> cliqueUnion(HashSet<DirectedPolygon> clique)
        {
            // Note that by definition, the union of a clique should return exactly one directed polygon.

            Debug.Assert(clique.Count > 0);

            if (clique.Count == 1)
            {
                return new List<DirectedPolygon>() { clique.First() };
            }

            List<DirectedPolygon> returnList = new List<DirectedPolygon>();

            HashSet<DirectedPolygon> source = new HashSet<DirectedPolygon>(clique);

            List<DirectedPolygon> internalList = new List<DirectedPolygon>();

            DirectedPolygon rtrnPolygon = source.First();

            source.Remove(rtrnPolygon);

            // Because of a limitation in the clipper lib, we need to build the union of the current clique
            // by iteratively adding polygons that are adjacent to the building polygon.

            while (source.Count > 0)
            {
                // Get a list of all polygons adjacent to the current return polygon.

                List<DirectedPolygon> adjacentList = new List<DirectedPolygon>() { rtrnPolygon };

                foreach (DirectedPolygon adjacentPolygon in source.ToList())
                {
                    if (rtrnPolygon.Intersects(adjacentPolygon))
                    {
                        adjacentList.Add(adjacentPolygon);
                        source.Remove(adjacentPolygon);
                    }
                }

                // The clique union for adjacent list will only work if the rtrn polygon is adjacent to everything else in the list.

                List<DirectedPolygon> unionList = listUnion(adjacentList);

                rtrnPolygon = unionList[0];

                for (int i = 1; i < unionList.Count;i++)
                {
                    internalList.Add(unionList[i]);
                }
            }

            returnList.Add(rtrnPolygon);

            returnList.AddRange(internalList);

            return returnList;

        }


        public static List<DirectedPolygon> listUnion(List<DirectedPolygon> pList)
        {
           // for this to work with the clipper lib, the first element must be adjacent to everything else

            List<DirectedPolygon> returnList = new List<DirectedPolygon>();

            Clipper clipper = new Clipper();

            List<List<IntPoint>> solutions = new List<List<IntPoint>>();

            List<IntPoint> pClip = DirectedPolygonToPolygon(pList[0]);
            clipper.AddPath(pClip, PolyType.ptSubject, true);

            for (int i = 1; i < pList.Count; i++)
            {
                pClip = DirectedPolygonToPolygon(pList[i]);
                clipper.AddPath(pClip, PolyType.ptClip, true);
            }

            solutions.Clear();

            bool succeeded = clipper.Execute(ClipType.ctUnion, solutions, PolyFillType.pftNonZero, PolyFillType.pftNonZero);

            if (!succeeded)
            {
                return null;
            }

            if (solutions.Count == 0)
            {
                return null;
            }

            List<DirectedPolygon> interiorPolygonList = new List<DirectedPolygon>();

            for (int i = 0; i < solutions.Count;i++)
            {
                returnList.AddRange(PolygonToDirectedPolygon(solutions[i]));
            }

            return returnList;
        }

        private static List<HashSet<DirectedPolygon>> formCliques(List<DirectedPolygon> pList)
        {
            List<HashSet<DirectedPolygon>> rtrnList = new List<HashSet<DirectedPolygon>>();

            HashSet<DirectedPolygon> source = new HashSet<DirectedPolygon>(pList);

            while (source.Count > 0)
            {
                DirectedPolygon startPolygon = source.First();

                HashSet<DirectedPolygon> clique = new HashSet<DirectedPolygon>() { startPolygon };

                source.Remove(startPolygon);

                buildClique(startPolygon, source, clique);

                rtrnList.Add(clique);
            }

            return rtrnList;
        }

        private static void buildClique(DirectedPolygon srcPolygon, HashSet<DirectedPolygon> inptSet, HashSet<DirectedPolygon> outpSet)
        {
            List<DirectedPolygon> addedList = new List<DirectedPolygon>();

            foreach (DirectedPolygon trgPolygon in inptSet.ToList())
            {
                if (srcPolygon.Intersects(trgPolygon))
                {
                    inptSet.Remove(trgPolygon);
                    outpSet.Add(trgPolygon);

                    addedList.Add(trgPolygon);
                }
            }

            foreach (DirectedPolygon trgPolygon in addedList)
            {
                buildClique(trgPolygon, inptSet, outpSet);
            }
        }

        public static double InteriorAngle(DirectedLine l1, DirectedLine l2)
        {
            double dX1 = l1.Coord2.X - l1.Coord1.X;
            double dY1 = l1.Coord2.Y - l1.Coord1.Y;

            double dX2 = l2.Coord2.X - l2.Coord1.X;
            double dY2 = l2.Coord2.Y - l2.Coord1.Y;

            return MathUtils.InteriorAngle(dX1, dY1, dX2, dY2);
        }

        public static bool LineIntersectsPolyline(DirectedLine directedLine, DirectedPolyline directedPolyline)
        {
            foreach (DirectedLine directedLine1 in directedPolyline)
            {
                if (directedLine.Intersects(directedLine1))
                {
                    return true;
                }
            }

            return false;
        }

        public static double DistancePointToLineSegment(Coordinate point, DirectedLine line)
        {
            DirectedLine line1 = line.Clone();

            Coordinate point1 = point.Clone();

            double atan = line1.Atan();

            line1.Rotate(-atan);
            
            point1.Rotate(-atan);

            double x1 = line1.MinX;
            double x2 = line1.MaxX;

            double y = (line1.Coord1.Y + line1.Coord2.Y) * 0.5;

            double delY = y - point1.Y;

            double delX;

            if (point1.X < x1)
            {
                delX = x1 - point1.X;

                return Math.Sqrt(delX * delX + delY * delY);
            }

            if (point1.X > x2)
            {
                delX = point1.X - x2;

                return Math.Sqrt(delX * delX + delY * delY);
            }

            return Math.Abs(delY);
        }
    }
}
