using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.Models
{
    public enum LineType
    {
        Unknown = 0,
        GraphicsLine = 1,
        PerimeterLine = 2,
        SeamLine = 3,
        ScaleLine = 4,
        MeasureLine = 5
    }
}
