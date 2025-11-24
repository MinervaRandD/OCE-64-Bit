using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.CanvasManager
{
    public enum LineLayoutState
    {
        Unknown = 0
        , Default = 1
        , New1XLineSequenceStarted = 2
        , Ongoing1XLineSequence = 3
        , New2XLineStarted = 4

    }
}
