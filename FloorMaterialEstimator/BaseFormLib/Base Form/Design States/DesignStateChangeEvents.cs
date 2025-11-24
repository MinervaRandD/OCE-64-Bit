
namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Utilities;

    using Globals;

    public partial class FloorMaterialEstimatorBaseForm
    {
        public bool LinePaletteTemporaryPopUp = false;

        public bool AllowTabSelection = false;

        private void TbcPageAreaLine_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //if (!AllowTabSelection)
            //{
            //    // This is to disable the selecting of tabs from the tab control.

            //    e.Cancel = true;

            //    return;
            //}

            if (LinePaletteTemporaryPopUp)
            {
                return;
            }

            switch (this.tbcPageAreaLine.SelectedIndex)
            {
                case FloorMaterialEstimatorBaseForm.tbcAreaModeIndex:
                    {
                        if (DesignState == DesignState.Area)
                        {
                            Debug.Assert(btnAreaDesignState.Checked &&
                                this.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcAreaModeIndex);

                            e.Cancel = false;

                            break; ;
                        }

                        if (DesignState == DesignState.Seam && SystemState.SeamMode == SeamMode.Subdivision && SystemState.DrawingShape)
                        {
                            btnCancelSubdivision_Click(null, null);
                        }

                        if (validateChangeToAreaMode())
                        {
                            DesignState = DesignState.Area;

                        } else
                        {
                            e.Cancel = true;
                        }


                    }
                    break;

                case FloorMaterialEstimatorBaseForm.tbcLineModeIndex:
                    {
                        if (DesignState == DesignState.Line)
                        {
                            Debug.Assert(btnLineDesignState.Checked &&
                                this.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcLineModeIndex);

                            e.Cancel = false;

                            break; ;
                        }

                        if (DesignState == DesignState.Seam && SystemState.SeamMode == SeamMode.Subdivision && SystemState.DrawingShape)
                        {
                            btnCancelSubdivision_Click(null, null);
                        }

                        if (validateChangeToLineMode())
                        {
                            DesignState = DesignState.Line;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    break;

                case FloorMaterialEstimatorBaseForm.tbcSeamModeIndex:
                    {
                        if (DesignState == DesignState.Seam)
                        {
                            Debug.Assert(btnSeamDesignState.Checked &&
                                this.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcSeamModeIndex);

                            e.Cancel = false;

                            break; ;
                        }

                        if (validateChangeToSeamMode())
                        {
                            DesignState = DesignState.Seam;
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    break;
            }
        }


        private void TbcPageAreaLine_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private bool validateChangeToAreaMode()
        {
            if (SystemState.DrawingShape && DesignState == DesignState.Line)
            {
                CanvasManager.ResetLineDrawState();

                return true;
            }

            ResetTapeMeasure();

            if (SystemState.DrawingShape)
            {
                ManagedMessageBox.Show("Cannot change to area mode while a drawing is being completed.");

                btnAreaDesignState.Checked = false;

                return false;
            }

            return true;
        }

        private bool validateChangeToLineMode()
        {
            ResetTapeMeasure();

            if (SystemState.DrawingShape)
            {
                ManagedMessageBox.Show("Cannot change to line mode while a drawing is being completed.");

                btnLineDesignState.Checked = false;

                return false;
            }

            return true;
        }

        private bool validateChangeToSeamMode()
        {

            if (!CurrentPage.ScaleHasBeenSet)
            {
                ManagedMessageBox.Show("The scale must be set in order to go to the seam design state.");
                
                if (!btnSetCustomScale.Checked)
                {
                    btnSetScale_Click(null, null);
                }
            }

            if (!CurrentPage.ScaleHasBeenSet)
            {
                return false;
            }

            if (SystemState.DrawingShape && DesignState == DesignState.Line)
            {
                CanvasManager.ResetLineDrawState();

                return true;
            }

            ResetTapeMeasure();

            if (SystemState.DrawingShape)
            {
                ManagedMessageBox.Show("Cannot change to seam mode while a drawing is being completed.");

                btnAreaDesignState.Checked = false;

                return false;
            }

            return true;
        }
    }
}
