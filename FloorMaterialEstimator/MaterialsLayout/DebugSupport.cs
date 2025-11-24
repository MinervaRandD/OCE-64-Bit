#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: DebugSupport.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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

    using Visio = Microsoft.Office.Interop.Visio;
    using Graphics;

    using Geometry;
    using System.Windows.Forms;

    public static class DebugSupport
    {


        internal static void DumpCutCoordinateDict(Dictionary<Coordinate, CutCoordinate> cutCoordinateDict)
        {
            Debug.WriteLine("Cut Coordinate Dictionary:\n");

            foreach (CutCoordinate cutCordinate in cutCoordinateDict.Values)
            {
                DumpCutCordinate(cutCordinate, true);
            }
        }

        internal static void DumpCutCordinate(CutCoordinate cutCoordinate, bool full = false)
        {
            if (cutCoordinate is null)
            {
                Debug.WriteLine("<null>");
                return;
            }

            Coordinate coord = cutCoordinate.Coordinate;

            if (coord == Coordinate.NullCoordinate)
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
            if (cutLineSegment is null)
            {
                Debug.WriteLine("<null>");
                return;
            }

            string dumpString = cutLineSegment.Coord1.ToString() + " --- " + cutLineSegment.Coord2.ToString() + " " + cutLineSegment.LineIndex.ToString();

            Debug.WriteLine(dumpString);

        }

        internal static void DumpCutLineSegment(string header, CutLineSegment leftLineSegment)
        {
            Debug.WriteLine(header);
            DumpCutLineSegment(leftLineSegment);

            throw new NotImplementedException();
        }

        public static void CheckForUnlinkedPolyline(GraphicsPage page, int checkPoint)
        {
            foreach (Visio.Shape visioShape in page.VisioPage.Shapes)
            {
                if (string.IsNullOrEmpty(visioShape.Data1) && visioShape.Data2 == "Polyline")
                {
                    MessageBox.Show("Unlinked polyline found at point " + checkPoint);

                    return;
                }
            }
        }
    }
}
