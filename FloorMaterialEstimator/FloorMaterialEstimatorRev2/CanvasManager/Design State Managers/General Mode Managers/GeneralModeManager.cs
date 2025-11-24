using PaletteLib;

namespace FloorMaterialEstimator.CanvasManager
{
    using System.Drawing;
    using System.Collections.Generic;

    using Globals;
    using Geometry;
    using Graphics;
   
    using FloorMaterialEstimator.Supporting_Forms;

    public partial class CanvasManager
    {
        public void ProcessToggleShowMeasuringStickMode()
        {
            BaseForm.BtnMeasuringStick_ButtonClick(null, null);
        }

        public void SetZeroLineActive()
        {
           if (BaseForm.btnAreaDesignStateZeroLine.BackColor != Color.Orange)
           {
                ProcessSwitchZeroLine();
            }
        }

        public void ProcessSwitchZeroLine()
        {
            if (BaseForm.btnAreaDesignStateZeroLine.BackColor == Color.Orange)
            {
                BaseForm.btnAreaDesignStateZeroLine.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                BaseForm.btnAreaDesignStateZeroLine.BackColor = Color.Orange;
            }

            PalettesGlobal.AreaFinishPalette.UpdateZeroLineDisplay(BaseForm.btnAreaDesignStateZeroLine.BackColor == Color.Orange);

            if (SystemState.DrawingMode != DrawingMode.Polyline || !SystemState.DrawingShape || (object)buildingPolyline == null)
            {
                return;
            }

            if (buildingPolyline.Count <= 0)
            {
                return;
            }

            if (BaseForm.btnAreaDesignStateZeroLine.BackColor == Color.Orange)
            {
                return;
            }

            else
            {
                return;
            }
        }

    }
}
