using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinishesLib
{
    public static class FinishGlobals
    {
        public static AreaFinishBaseList AreaFinishBaseList { get; set; }

        public static LineFinishBaseList LineFinishBaseList { get; set; }

        public static LineFinishBase ZeroLineBase { get; set; }

        public static SeamFinishBaseList SeamFinishBaseList { get; set; }

        public static AreaFinishBase SelectedAreaFinish => AreaFinishBaseList.SelectedItem;

        public static List<AreaFinishLayers> AreaFinishLayers { get; set; }
    }
}
