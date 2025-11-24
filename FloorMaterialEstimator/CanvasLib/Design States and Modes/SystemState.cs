using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasLib.Design_States_and_Modes
{
    public static class SystemState
    {
        public static DesignState DesignState
        {
            get;
            set;
        } = DesignState.Area;

       // public static AreaMode AreaMode { get; set; } = AreaMode.Layout;

        //public static LineMode LineMode { get; set; } = LineMode.Layout;

        public static SeamMode SeamMode { get; set; } = SeamMode.Selection;
    }
}
