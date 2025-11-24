using FloorMaterialEstimator.Finish_Controls;
using System;
using System.Collections.Generic;
using PaletteLib;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class UCLinePaletteSerializable
    {
        public List<UCLinePaletteElementSerializable> LineFinishList { get; set; }

        public UCLinePaletteSerializable() { }

        public UCLinePaletteSerializable(UCLineFinishPalette linePalette)
        {
            if (linePalette.ucFlowLayoutPanel.Controls != null)
            {
                LineFinishList = new List<UCLinePaletteElementSerializable>();

                foreach (UCLineFinishPaletteElement linePanelElement in linePalette.ucFlowLayoutPanel.Controls)
                {
                    LineFinishList.Add(new UCLinePaletteElementSerializable(linePanelElement));
                }
            }
        }
    }
}
