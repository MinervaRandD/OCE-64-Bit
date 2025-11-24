using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace FloorMaterialEstimator.CanvasManager
{
    public static class CanvasManagerGlobals
    {
        public static AreaFinishManagerList AreaFinishManagerList { get; set; }

        public static LineFinishManagerList LineFinishManagerList { get; set; }

        public static SeamFinishManagerList SeamFinishManagerList { get; set; }

        public static AreaFinishManager SelectedAreaFinishManager => AreaFinishManagerList.SelectedAreaFinishManager;

        public static LineFinishManager SelectedLineFinishManager => LineFinishManagerList.SelectedLineFinishManager;

        public static SeamFinishManager SelectedSeamFinishManager => SeamFinishManagerList.SelectedSeamFinishManager;
    }
}
