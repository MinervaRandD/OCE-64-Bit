using System;
using System.Collections.Generic;
using System.Linq;
using Geometry;
using Graphics;
using VoronoiLib;
using VoronoiLib.Structures;
using Utilities;

namespace VoronoiLib
{
    public class VoronoiRunner
    {

        private int nPntsPerLine = 3;

        private List<DirectedLine> directedLineList;

        public List<DirectedLine> VoronoiEdgeList { get; set; }

        public HashSet<Coordinate> VoronoiInternalCoords { get; set; }

        public VoronoiRunner(
            List<DirectedLine> directedLineList
            ,int nPntsPerLine = 3)
        {
            this.nPntsPerLine = nPntsPerLine;
            this.directedLineList = directedLineList;
        }

        public Coordinate RunVoroniAlgo(
           )
        {
            double divisor = (double)nPntsPerLine + 1.0;

            double multiplier = 1.0 / divisor;

            List<FortuneSite> sites = new List<FortuneSite>();

            double minX = directedLineList.Min(l => l.MinX);
            double maxX = directedLineList.Max(l => l.MaxX);

            double minY = directedLineList.Min(l => l.MinY);
            double maxY = directedLineList.Max(l => l.MaxY);

            foreach (DirectedLine directedLine in directedLineList)
            {
                Coordinate coord1 = directedLine.Coord1;
                Coordinate coord2 = directedLine.Coord2;


                double deltaX = coord2.X - coord1.X;
                double deltaY = coord2.Y - coord1.Y;

                double x0 = coord1.X;
                double y0 = coord1.Y;

                for (int i = 1; i <= nPntsPerLine; i++)
                {
                    double x1 = coord1.X + (double)i * deltaX * multiplier;
                    double y1 = coord1.Y + (double)i * deltaY * multiplier;

                    sites.Add(new FortuneSite(x1, y1));
                }
            }

            LinkedList<VEdge> result = VoronoiLib.FortunesAlgorithm.Run(sites, minX, minY, maxX, maxY);

            VoronoiEdgeList = new List<DirectedLine>();

            VoronoiInternalCoords = new HashSet<Coordinate>();

            foreach (VEdge vedge in result)
            {
                Coordinate coord1 = new Coordinate(vedge.Start.X, vedge.Start.Y);
                Coordinate coord2 = new Coordinate(vedge.End.X, vedge.End.Y);

                if (coord1 == coord2)
                {
                    continue;
                }

                DirectedLine directedLine = new DirectedLine(coord1, coord2);

                VoronoiEdgeList.Add(directedLine);

                if (!VoronoiInternalCoords.Contains(coord1))
                {
                    if (isInternalCoord(coord1, directedLineList))
                    {
                        VoronoiInternalCoords.Add(coord1);
                    }
                }

                if (!VoronoiInternalCoords.Contains(coord2))
                {
                    if (isInternalCoord(coord2, directedLineList))
                    {
                        VoronoiInternalCoords.Add(coord2);
                    }
                }
            }

            List<Tuple<double, Coordinate>> bestCoordList = new List<Tuple<double, Coordinate>>();

            Coordinate bestCoord = Coordinate.NullCoordinate;
            double bestDist = double.MinValue;

            foreach (Coordinate internalCoord in VoronoiInternalCoords)
            {
                double minDist = double.MaxValue;

                foreach (FortuneSite site in sites)
                {
                    double nextDist = MathUtils.H2Distance(internalCoord.X, internalCoord.Y, site.X, site.Y);

                    if (nextDist <= bestDist)
                    {
                        minDist = nextDist;

                        break;
                    }

                    if (nextDist < minDist)
                    {
                        minDist = nextDist;
                    }
                }

                if (minDist >= bestDist)
                {
                    bestCoord = internalCoord;
                    bestDist = minDist;

                    bestCoordList.Add(new Tuple<double, Coordinate>(bestDist, internalCoord));
                }
            }

            List<Coordinate> bestCoords = bestCoordList.Where(b => b.Item1 == bestDist).Select(t => t.Item2).ToList();

            int count = bestCoords.Count;

            if (count == 1)
            {
                return bestCoords[0];
            }

            int index = count / 2;

            if ((count % 2) == 1)
            {
                return bestCoords[index];
            }

            bestCoord = bestCoords[index] + bestCoords[index - 1];

            bestCoord = new Coordinate(bestCoord.X / 2.0, bestCoord.Y / 2.0);

            return bestCoord;
        }

        private bool isInternalCoord(Coordinate coord, List<DirectedLine> perimeterLines)
        {
            int intersectCount = 0;

            foreach (DirectedLine directedLine in perimeterLines)
            {
                if (directedLine.MinY >= coord.Y || directedLine.MaxY <= coord.Y)
                {
                    continue;
                }

                double xIntercept = directedLine.XInterceptForY(coord.Y);

                if (xIntercept == coord.X)
                {
                    return false;
                }

                if (directedLine.XInterceptForY(coord.Y) > coord.X)
                {
                    continue;
                }

                intersectCount++;
            }

            return intersectCount % 2 > 0;
        }
    }
}
