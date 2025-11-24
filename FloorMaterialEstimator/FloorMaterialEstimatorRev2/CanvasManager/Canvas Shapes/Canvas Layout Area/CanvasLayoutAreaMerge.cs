using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using MaterialsLayout;

    using FloorMaterialEstimator.Finish_Controls;
    using FinishesLib;
    using SettingsLib;
    using Utilities;
    using Globals;
    using MaterialsLayout.MaterialsLayout;
    using CanvasLib.Markers_and_Guides;
    using DebugSupport;
    using TracerLib;

    public partial class CanvasLayoutArea : GraphicsLayoutArea, IAreaShape
    {
     
        internal void MergeOffsprings()
        {
            foreach (CanvasLayoutArea offspringLayoutArea in OffspringAreas)
            {
                offspringLayoutArea.Delete();
            }

            ClearOffspringAreas();

            if (this.ParentArea is null)
            {
                // We do not need to redraw the shape if it is a top level layout area as it will already
                // exist in area mode.

                return;
            }
        }

    }
}
