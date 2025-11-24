#region HeaderAndLicense
/*******************************************************************************************************/
/*                                                                                                     */
/* File: CutLineSegment.cs. Project: MaterialsLayout. Created: 6/10/2024         */
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Geometry;
    using Utilities;

    public class CutLineSegment: Line
    {
        public DirectedLine OriginalLine;
        public int LineIndex;

        public int CutLineSegmentIndex = 0;

        private static int CutLineSegmentIndexGenerator = 0;

        public CutLineSegment(DirectedLine line, int lineIndex, double y1, double y2): base(line.Coord1, line.Coord2)
        {
            CutLineSegmentIndex = CutLineSegmentIndexGenerator++;

            this.OriginalLine = line;
            this.LineIndex = lineIndex;

            Coordinate coord1 = line.Coord1;
            Coordinate coord2 = line.Coord2;

            if (coord1.Y > coord2.Y)
            {
                Utilities.Swap(ref coord1, ref coord2);
            }

            if (coord1.Y <= y2 && coord2.Y >= y1)
            {
                double x1 = line.XInterceptForY(y1);
                double x2 = line.XInterceptForY(y2);

                Coord1 = new Coordinate(x1, y1);
                Coord2 = new Coordinate(x2, y2);
            }

            else if (coord1.Y <= y2)
            {
                double x2 = line.XInterceptForY(y2);

                Coord1 = new Coordinate(x2, y2);
                Coord2 = coord2;
            }

            else if (coord2.Y >= y1)
            {
                double x1 = line.XInterceptForY(y1);

                Coord1 = coord1;
                Coord2 = new Coordinate(x1, y1); ;
            }

            else
            {
                throw new NotImplementedException();
            }
            
            if (Coord1 > Coord2)
            {
                Utilities.Swap(ref Coord1, ref Coord2);
            }

            return;
        }
        
        public CutLineSegment(DirectedLine line, int lineIndex, Coordinate coord1, Coordinate coord2, bool lineIsSeam = false) : base(coord1, coord2)
        {
            OriginalLine = line;
            LineIndex = lineIndex;

            CutLineSegmentIndex = CutLineSegmentIndexGenerator++;

            if (lineIsSeam)
            {
                LineIndex = CutLineSegmentIndex;
            }
            Coord1 = coord1;
            Coord2 = coord2;

            Coord1.NumericalCondition(1.0e-12);
            Coord2.NumericalCondition(1.0e-12);

            if (Coord1 > Coord2)
            {
                Utilities.Swap(ref Coord1, ref Coord2);
            }
        }
        
        //public void Translate(Coordinate translateCoordinate)
        //{
        //    Coord1 += translateCoordinate;
        //    Coord2 += translateCoordinate;
        //}

        //internal void Rotate(double[,] rotationMatrix)
        //{
        //    Coord1.Rotate(rotationMatrix);
        //    Coord2.Rotate(rotationMatrix);
        //}

        public bool IsSeamSegment()
        {
            return LineIndex < 0;
        }

        internal void InverseTransform(Coordinate inverseTranslationCoordinate, double[,] inverseRotationMatrix)
        {
            Rotate(inverseRotationMatrix);
            Translate(inverseTranslationCoordinate);
        }

        internal bool IsNormalized()
        {
            return Coord1 <= Coord2;
        }

        internal new CutLineSegment Clone()
        {
            Coordinate coord1 = this.Coord1.Clone();
            Coordinate coord2 = this.Coord2.Clone();

            DirectedLine line = null;

            if (!(this.OriginalLine is null))
            {
                line = this.OriginalLine.Clone();
            }

            return new CutLineSegment(line, this.LineIndex, coord1, coord2);
        }
    }
}
