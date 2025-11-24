

namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Utilities;
    using Globals;
    using FloorMaterialEstimator.CanvasManager;

    public partial class FloorMaterialEstimatorBaseForm
    {

        private void FloorMaterialEstimatorBaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void FloorMaterialEstimatorBaseForm_KeyUp(object sender, KeyEventArgs e)
        {
            // ManagedMessageBox.Show("FloorMaterialEstimatorBaseForm_KeyUp: " + e.KeyCode.ToString());

            // Most key clicks are dispatched from the 'prefilter' functionality in the base form to the canvas manager key management
            // routines. However, the prefilter functionality, for whatever reason, does not pick up the alt key or cntl key modifications
            // so they are handled here and dispatched seperately to the canvas manager key management routines
            
            if (e.Alt)
            {
                CanvasManager.ProcessKeyDown((int) e.KeyCode, KeyModifiers.Alt);

                e.Handled = true;
            }
        }


        private void FloorMaterialEstimatorBaseForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ManagedMessageBox.Show("FloorMaterialEstimatorBaseForm_KeyPress: " + e.KeyChar.ToString());

            if (e.KeyChar == 14)
            {
                btnNewProject_Click(null, null);

                return;
            }

            if (e.KeyChar == 15)
            {
                btnExistingProject_Click(null, null);

                return;
            }

            if (e.KeyChar == 19)
            {
                btnSaveProject_Click(null, null);

                return;
            }


            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {

                int KeyAscii = e.KeyChar - '0';

                if (this.DesignState == DesignState.Area)
                {
                    CanvasManager.ProcessAreaModeFinishNumericShortCut(KeyAscii);
                    return;

                }

                else if (this.DesignState == DesignState.Line)
                {
                    CanvasManager.ProcessLineModeFinishNumericShortCut(KeyAscii);
                    return;
                }

                else if (this.DesignState == DesignState.Seam)
                {
                    CanvasManager.ProcessSeamModeFinishNumericShortCut(KeyAscii);
                    return;
                }
            }
        }

    }
}
