using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasManager.Reports.SummaryReport
{
    public static class RowColumnWidths
    {
        public const int CkbFinishFilterSizeX = 22;
        public const int PnlColorSizeX = 34;
        public const int ValueSizeX = 96;
        public const int UnitsSizeX = 40;
        public const int TagSizeX = 164;
        public const int TypeSizeX = 80;
        public const int ValueSmallSizeX = 80;

        public const int CounterTagSizeX = 40;
        public const int CounterCountSizeX = 54;
        public const int CounterDescriptionSizeX = 256;
        public const int CounterSizeSizeX = 72;
        public const int CounterTotalSizeX = 72;

        public const int TotlSizeX = 
            CkbFinishFilterSizeX + PnlColorSizeX + TagSizeX
            + 5 * ValueSizeX
            + 2 * UnitsSizeX
            + 2 * ValueSmallSizeX ;
    }
}
