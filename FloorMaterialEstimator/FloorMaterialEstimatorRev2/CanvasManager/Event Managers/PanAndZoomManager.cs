namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows;

    using FloorMaterialEstimator;

    using Geometry;
    using Graphics;
    using Utilities;
    using Globals;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CanvasManager
    {
        private double panStartLeft=0;
        private double panStartUppr=0;
        private double panStartWdth=0;
        private double panStartHght=0;

        private double panStartX = 0;
        private double panStartY = 0;

        private bool panning = false;

        private void processPanModeMouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            BaseForm.Cursor = Cursors.Hand;

            if (KeyButtonState != 1)
            {
                return;
            }

            panStartX = x;
            panStartY = y;

            VsoWindow.GetViewRect(out panStartLeft, out panStartUppr, out panStartWdth, out panStartHght);

            panning = true;
        }

        private void processPanModeMouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {

            if (MathUtils.H0Dist(this.panStartX, this.panStartY, x, y) <= 0.1)
            {
                processPanModeClick(Button, KeyButtonState, x, y);

                return;
            }

            panStartLeft = 0;
            panStartUppr = 0;
            panStartWdth = 0;
            panStartHght = 0;

            panStartX = 0;
            panStartY = 0;

            panning = false;

            CancelDefault = true;

        }

        private void processPanModeClick(int button, int keyButtonState, double x, double yt)
        {
            if (BaseForm.CounterMode)
            {
                ManagedMessageBox.Show("Counters are not active in pan mode");
                return;
            }

            if (SystemState.TakeoutAreaMode)
            {
                ManagedMessageBox.Show("Takeouts are not active in pan mode");
                return;
            }
        }

        private void processPanModeMouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (!panning)
            {
                return;
            }

            BaseForm.Cursor = Cursors.Hand;

            if (KeyButtonState != 1)
            {
                return;
            }

            double deltaX = x - panStartX;
            double deltaY = y - panStartY;

            VsoWindow.SetViewRect(panStartLeft - deltaX, panStartUppr - deltaY, panStartWdth, panStartHght);

            CancelDefault = true;
        }

    }
}
