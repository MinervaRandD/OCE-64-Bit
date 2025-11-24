//-------------------------------------------------------------------------------//
// <copyright file="DebugMode.cs"                                                //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2020        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using System;
    using System.Windows.Forms;
    using SettingsLib;
    using TracerLib;

    public partial class FloorMaterialEstimatorBaseForm
    {
        private DebugForm debugForm = null;

        public void BtnDebug_Click(object sender, EventArgs e)
        {
            if (debugForm is null)
            {
                debugForm = new DebugForm(this, CanvasManager.Window, CanvasManager.Page);

                debugForm.FormClosed += DebugForm_FormClosed;
                debugForm.Show();
                debugForm.BringToFront();
            }

            else
            {
                debugForm.Show();
                debugForm.BringToFront();
                debugForm.WindowState = FormWindowState.Normal;
            }
        }

        private void DebugForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            debugForm = null;
        }

        public void UpdateDebugForm()
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif

            try
            {
                if (!GlobalSettings.UpdateDebugFormDynamically)
                {
                    return;
                }

                if (debugForm != null)
                {
                    debugForm.Refresh();
                }
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("UpdateDebugForm throws an exception.", ex, 1, true);
            }
        }
    }
}
