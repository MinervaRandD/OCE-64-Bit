using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FloorMaterialEstimator.Models;
using FloorMaterialEstimator.Seams_And_Cuts;

namespace FloorMaterialEstimator.Test_and_Debug
{
    public static class TestOutput
    {
        internal static void DumpCutCoordinateDict(Dictionary<Coordinate, CutCoordinate> cutCoordinateDict)
        {
            Debug.WriteLine("Cut Coordinate Dictionary:\n");

            foreach (CutCoordinate cutCordinate in cutCoordinateDict.Values)
            {
                DumpCutCordinate(cutCordinate, true);
            }
        }

        internal static void DumpCutCordinate(CutCoordinate cutCoordinate, bool full=false)
        {
            if (cutCoordinate == null)
            {
                Debug.WriteLine("<null>");
                return;
            }

            Coordinate coord = cutCoordinate.Coordinate;

            if (coord == null)
            {
                Debug.WriteLine("<coord = null>");
                return;
            }

            Debug.WriteLine(coord.ToString());

            if (!full)
            {
                return;
            }

            cutCoordinate.CutLineSegmentList.ForEach(l =>
                {
                    Debug.Write("  ");
                    DumpCutLineSegment(l);
                });
        }

        internal static void DumpCutLineSegmentDict(Dictionary<int, CutLineSegment> cutLineSegmentDict)
        {
            Debug.WriteLine("Cut line segment dictionary:");

            foreach (CutLineSegment cutLineSegment in cutLineSegmentDict.Values)
            {
                DumpCutLineSegment(cutLineSegment);
            }

        }

        internal static void DumpCutLineSegment(CutLineSegment cutLineSegment)
        {
            if (cutLineSegment == null)
            {
                Debug.WriteLine("<null>");
                return;
            }

            string dumpString = cutLineSegment.Coord1.ToString() + " --- " + cutLineSegment.Coord2.ToString() + " " + cutLineSegment.LineIndex.ToString();

            Debug.WriteLine(dumpString);

        }
    }
}
