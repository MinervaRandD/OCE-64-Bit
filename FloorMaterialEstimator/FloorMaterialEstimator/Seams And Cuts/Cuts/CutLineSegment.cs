
namespace FloorMaterialEstimator.Seams_And_Cuts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

    public class CutLineSegment: Line
    {
        public Line OriginalLine;
        public int LineIndex;

        public int CutLineSegmentIndex = 0;
        
        public CutLineSegment(Line line, int lineIndex, double y1, double y2): base()
        {
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
        
        public CutLineSegment(Line line, int lineIndex, Coordinate coord1, Coordinate coord2): base(coord1, coord2)
        {
            OriginalLine = line;
            LineIndex = lineIndex;

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

            Line line = null;

            if (this.OriginalLine != null)
            {
                line = this.OriginalLine.Clone();
            }

            return new CutLineSegment(line, this.LineIndex, coord1, coord2);
        }
    }
}
