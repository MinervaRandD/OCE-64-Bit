

namespace FloorMaterialEstimator.Seams_And_Cuts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Seams_And_Cuts;

    public class CutCoordinate
    {
        public Coordinate Coordinate;

        public List<CutLineSegment> CutLineSegmentList = new List<CutLineSegment>();

        public CutCoordinate(Coordinate coord)
        {
            Coordinate = coord;
        }

        public CutCoordinate(Coordinate coord, CutLineSegment leftLineSegment, CutLineSegment rghtLineSegment)
        {
            this.Coordinate = coord;

            CutLineSegmentList.Add(leftLineSegment);
            CutLineSegmentList.Add(rghtLineSegment);
        }

        internal void InverseTransform(Coordinate inverseTranslationCoordinate, double[,] inverseRotationMatrix)
        {
            Coordinate.Rotate(inverseRotationMatrix);
            Coordinate.Translate(inverseTranslationCoordinate);
        }
    }
}
