

using FloorMaterialEstimator.CanvasManager;

namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.Finish_Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Utilities;

    using FinishesLib;
    using PaletteLib;

    public class UCLinePaletteElementSerializable
    {
        public LineFinishBase LineFinishBase { get; set; }

        public bool Filtered { get; set; }

        public int PositionOnPalette { get; set; }

        //public List<string> LayoutAreaGuidList { get; set; }

        public UCLinePaletteElementSerializable() { }

        public UCLinePaletteElementSerializable(UCLineFinishPaletteElement linePaletteElement)
        {
            this.LineFinishBase = linePaletteElement.LineFinishBase;

            this.Filtered = linePaletteElement.Filtered;

            this.PositionOnPalette = linePaletteElement.PositionOnPalette;

            //if (Utilities.IsNotNull(linePaletteElement.LineFinishManager.LineDict))
            //{
            //    this.LayoutAreaGuidList = linePaletteElement.LineFinishManager.CanvasDirectedLines.Select(l => l.Guid).ToList();
            //}
        }
    }
}
