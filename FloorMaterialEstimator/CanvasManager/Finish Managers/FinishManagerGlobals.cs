using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using CanvasManager.FinishManager;

namespace CanvasManagerLib.FinishManager
{
    public static class FinishManagerGlobals
    {
        private static AreaFinishManagerList _areaFinishManagerList = null;

        public static AreaFinishManagerList AreaFinishManagerList
        {
            get
            {
                return _areaFinishManagerList;
            }

            set
            {
                _areaFinishManagerList = value;
            }
        }

        public static LineFinishManagerList LineFinishManagerList { get; set; }

        public static SeamFinishManagerList SeamFinishManagerList { get; set; }

        public static AreaFinishManager SelectedAreaFinishManager => AreaFinishManagerList.SelectedAreaFinishManager;

        public static LineFinishManager SelectedLineFinishManager => LineFinishManagerList.SelectedLineFinishManager;

        public static SeamFinishManager SelectedSeamFinishManager => SeamFinishManagerList.SelectedSeamFinishManager;

        public static Action<int> UpdateOversUndersStats { get; set; }
    }
}
