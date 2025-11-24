using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.CanvasManager
{
    using Graphics;

    public partial class CanvasManager
    {

        public List<CanvasLayoutArea> AreaDesignStateSelectedAreas => CurrentPage.AreaDesignStateSelectedAreas();

        public void ClearAreaModeSelectedAreaSelections()
        {
            foreach (CanvasLayoutArea canvasLayoutArea in AreaDesignStateSelectedAreas)
            {
                canvasLayoutArea.AreaDesignStateEditModeSelected = false;
            }
        }
    }
}
