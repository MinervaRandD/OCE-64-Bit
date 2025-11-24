using CanvasLib.Filters.Area_Filter;
using CanvasLib.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globals;

namespace FloorMaterialEstimator
{
    public class ProjectOptionsSerializable
    {
        public bool AreaModeShowCutsIndices { get; set; }

        public bool AreaModeShowSeams { get; set; }

        public bool SeamModeShowCuts { get; set; }

        public bool SeamModeShowOvers { get; set; }

        public bool SeamModeShowUnders { get; set; }

        public bool SeamModeShowEmbeddedCuts { get; set; }

        public double AreaLegendScale { get; set; }

        public double CurrentLineModeLegendSize { get; set; }

        public ProjectOptionsSerializable(FloorMaterialEstimatorBaseForm baseForm)
        {
            AreaModeShowCutsIndices = baseForm.ckbShowAreaModeCutIndices.Checked;
 //           AreaModeShowSeams = baseForm.ckbShowAreaModeSeams.Checked;

            SeamModeShowCuts = baseForm.ckbShowSeamModeCuts.Checked;
            SeamModeShowOvers = baseForm.ckbShowSeamModeOvers.Checked;
            SeamModeShowUnders = baseForm.ckbShowSeamModeUndrs.Checked;
            SeamModeShowEmbeddedCuts = baseForm.ckbShowEmbeddedCuts.Checked;

            AreaLegendScale = SystemGlobals.AreaLegendScale;
            CurrentLineModeLegendSize = SystemGlobals.CurrentLineModeLegendSize;
        }

        public ProjectOptionsSerializable() { }

        public void DeSerialize(FloorMaterialEstimatorBaseForm baseForm)
        {
            baseForm.ckbShowAreaModeCutIndices.Checked = AreaModeShowCutsIndices;

            baseForm.ckbShowSeamModeCuts.Checked = SeamModeShowCuts;
            baseForm.ckbShowSeamModeOvers.Checked = SeamModeShowOvers;
            baseForm.ckbShowSeamModeUndrs.Checked = SeamModeShowUnders; ;
            baseForm.ckbShowEmbeddedCuts.Checked = SeamModeShowEmbeddedCuts;

            SystemGlobals.AreaLegendScale = AreaLegendScale;
            SystemGlobals.CurrentLineModeLegendSize = CurrentLineModeLegendSize; 

        }
    }


    public enum SeamOption
    {
        Hide = 0,
        Show = 1,
        ShowUnhidable = 2
    }
}
