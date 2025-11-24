namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using PaletteLib;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using Utilities;

    using FloorMaterialEstimator.Finish_Controls;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CanvasManager
    {
        public List<CanvasDirectedLine> LineHistoryList = new List<CanvasDirectedLine>();

        public List<CanvasDirectedLine> DeletedLineShapes = new List<CanvasDirectedLine>();

        public List<Tuple<LineFinishManager, CanvasDirectedLine>> MovedLineShapes = new List<Tuple<LineFinishManager, CanvasDirectedLine>>();

        public List<Tuple<int, CanvasDirectedLine>> ChangedLineTypeShapes = new List<Tuple<int, CanvasDirectedLine>>();

        public CanvasDirectedLine LineModeBuildingLine
        {
            get;
            set;
        }

        private void processLineDesignStateClick(int button, int keyButtonState, double x, double y, ref bool cancelDefault)
        {
            /* if (BaseForm.btnLineDesignStateLayoutMode.BackColor == Color.Orange)
            { */
                processLineDesignStateLayoutModeClick(button, keyButtonState, x, y, ref cancelDefault);
           /* }

            else
            { */
                processLineEditModeClick(button, keyButtonState, x, y, ref cancelDefault);
           /* } */

            BaseForm.CurrentProjectChanged = true;

            Window?.DeselectAll();

        }


        private void ProcessLineModeToggleZeroLineMode()
        {
            // The reference to the layout area manager here is incorrect. But there is no current
            // button for setting zero line mode in the line mode pallet.

            BaseForm.BtnAreaDesignModeZeroLine_Click(null, null);
        }

        public void ProcessLineModeFinishNumericShortCut(int key)
        {
            int position = key - 1;

            if (position < 0 || position >= BaseForm.LineFinishBaseList.Count)
            {
                return;
            }
            
            BaseForm.LineFinishBaseList.Select(position);

            if (true) // || KeyboardUtils.CntlKeyPressed) //The control key is screwing up visio
            {
                ProcessAreaModeSetAreasToSelectedMaterial();
            }
        }

        public void ProcessLineModeSetSelectedLineToCurrentMaterial()
        {
            UCLineFinishPaletteElement ucFinishElem = BaseForm.selectedLineFinish;

            List<CanvasDirectedLine> selectedLineList = new List<CanvasDirectedLine>(CurrentPage.SelectedLines);

            foreach (CanvasDirectedLine canvasDirectedLine in selectedLineList)
            {
                ProcessEditLineModeActionChangeLineToFinish(canvasDirectedLine, ucFinishElem);

                canvasDirectedLine.SetSelectionMode(false);
            }
        }

    }
}
