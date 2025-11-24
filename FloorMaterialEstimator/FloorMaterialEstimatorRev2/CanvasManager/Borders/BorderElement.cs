using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.CanvasManager
{
    public class BorderElement
    {
        Coordinate coord1;
        Coordinate coord2;
        Coordinate coord3;
        Coordinate coord4;

        char direction;

        double baseSlope;

        public BorderElement(Coordinate coord1, Coordinate coord2, char direction)
        {
            this.coord1 = coord1;
            this.coord2 = coord2;

            this.direction = direction;

            setBaseSlope();
        }

        private void setBaseSlope()
        {

        }
    }
}
