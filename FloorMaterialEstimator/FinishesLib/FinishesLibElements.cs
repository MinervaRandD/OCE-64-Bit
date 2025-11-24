using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinishesLib
{
    public class FinishesLibElements
    {
        public AreaFinishBase AreaFinishBase { get;set; }

        public LineFinishBase LineFinishBase { get;set; }

        public SeamFinishBase SeamFinishBase { get;set; }

        public AreaFinishLayers AreaFinishLayers { get;set; }

        public LineFinishLayers LineFinishLayers { get;set; }

        public SeamFinishLayers SeamFinishLayers { get;set; }

        public FinishesLibElements(
            AreaFinishBase areaFinishBase
            , LineFinishBase lineFinishBase
            , SeamFinishBase seamFinishBase
            , AreaFinishLayers areaFinishLayers
            , LineFinishLayers lineFinishLayers
            , SeamFinishLayers seamFinishLayers)
        {
            AreaFinishBase = areaFinishBase;
            LineFinishBase = lineFinishBase;
            SeamFinishBase = seamFinishBase;
            AreaFinishLayers = areaFinishLayers;
            LineFinishLayers = lineFinishLayers;
            SeamFinishLayers = seamFinishLayers;
        }
    }
}
