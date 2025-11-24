using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    using Globals;
    using System.Windows.Forms;

    public partial class FloorMaterialEstimatorBaseForm: IBaseForm
    {
        public Form Form { get { return this; } }

        CheckBox IBaseForm.CkbShowAreaModeCutIndices
        {
            get { return this.ckbShowAreaModeCutIndices; }
        }



        RadioButton IBaseForm.RbnAreaModeAutoSeamsShowAll
        {
            get { return this.RbnAreaModeAutoSeamsShowAll; }
        }

        RadioButton IBaseForm.RbnAreaModeAutoSeamsHideAll
        {
            get { return this.RbnAreaModeAutoSeamsHideAll; }
        }

        RadioButton IBaseForm.RbnAreaModeAutoSeamsShowUnHideable
        {
            get { return this.RbnAreaModeAutoSeamsShowUnHideable; }
        }

        RadioButton IBaseForm.RbnAreaModeManualSeamsShowAll
        {
            get { return this.RbnAreaModeManualSeamsShowAll; }
        }

        RadioButton IBaseForm.RbnSeamModeAutoSeamsShowAll
        {
            get { return this.RbnSeamModeAutoSeamsShowAll; }
        }

        RadioButton IBaseForm.RbnSeamModeAutoSeamsHideAll
        {
            get { return this.RbnSeamModeAutoSeamsHideAll; }
        }

        RadioButton IBaseForm.RbnSeamModeAutoSeamsShowUnHideable
        {
            get { return this.RbnSeamModeAutoSeamsShowUnHideable; }
        }

        RadioButton IBaseForm.RbnSeamModeManualSeamsShowAll
        {
            get { return this.RbnSeamModeManualSeamsShowAll; }
        }

        CheckBox IBaseForm.CkbShowSeamModeCuts
        {
            get { return this.ckbShowSeamModeCuts; }
        }

        CheckBox IBaseForm.CkbShowSeamModeCutIndices
        {
            get { return this.ckbShowSeamModeCutIndices; }
        }

        CheckBox IBaseForm.CkbShowSeamModeOvers
        {
            get { return this.ckbShowSeamModeOvers; }
        }

        CheckBox IBaseForm.CkbShowSeamModeUndrs
        {
            get { return this.ckbShowSeamModeUndrs; }
        }

        CheckBox IBaseForm.CkbShowEmbeddedCuts
        {
            get { return this.ckbShowEmbeddedCuts; }
        }

        CheckBox IBaseForm.CkbShowEmbeddedOvers
        {
            get { return this.ckbShowEmbeddedOvers; }
        }

        CheckBox IBaseForm.CkbShowSeamModeAreaNmbrs
        {
            get { return this.ckbShowSeamModeAreaNmbrs; }
        }

        RadioButton IBaseForm.RbnDoorTakeoutOther
        {
            get { return this.rbnDoorTakeoutOther; }
        }

        RadioButton IBaseForm.RbnDoorTakeout3Ft
        {
            get { return this.rbnDoorTakeout3Ft; }
        }

        RadioButton IBaseForm.RbnDoorTakeout6Ft
        {
            get { return this.rbnDoorTakeout6Ft; }
        }

        Button IBaseForm.BtnLayoutLine1XMode
        {
            get { return this.BtnLayoutLine1XMode;  }
        }

        Button IBaseForm.BtnLayoutLine2XMode
        {
            get { return this.btnLayoutLine2XMode; }
        }



        ToolStripButton IBaseForm.BtnShowLegendForm
        {
            get { return this.BtnShowLegendForm; }
        }



        ToolStripButton IBaseForm.BtnHideFieldGuides
        {
            get { return this.btnHideFieldGuides; }
        }

        ToolStripButton IBaseForm.BtnShowFieldGuides
        {
            get { return this.BtnShowFieldGuides; }
        }


        public ToolStripButton BtnSnapToGrid
        {
            get { return this.btnSnapToGrid; }
        }


        //------------------------------//
        //   Area mode panel exposures  //
        //------------------------------//

        Button IBaseForm.BtnNormalLayoutArea
        {
            get { return this.btnNormalLayoutArea; }
        }

        Button IBaseForm.BtnColorOnly
        {
            get { return this.btnColorOnly; }
        }

        Button IBaseForm.BtnFixedWidth
        {
            get { return this.btnFixedWidth; }
        }

        Button IBaseForm.BtnOversGenerator
        {
            get { return this.btnOversGenerator; }
        }

        Button IBaseForm.BtnDoorTakeoutActivate
        {
            get { return this.BtnDoorTakeoutActivate; }
        }

        Button IBaseForm.BtnDoorTakeoutShow
        {
            get { return this.BtnDoorTakeoutShow; }
        }

        Action<object, EventArgs> IBaseForm.BtnDoorTakeoutActivate_Click
        {
            get { return this.BtnDoorTakeoutActivate_Click;  }
        }

        private Button BtnLineModeCounterActivate = null;

        Button IBaseForm.BtnLineModeCounterActivate
        {
            get { return this.BtnLineModeCounterActivate;  }
            set { BtnLineModeCounterActivate = value; }
        }

        private Action<object, EventArgs> BtnLineModeCounterActivate_Click;

        Action<object, EventArgs> IBaseForm.BtnLineModeCounterActivate_Click
        {
            get { return this.BtnLineModeCounterActivate_Click; }
            set { BtnLineModeCounterActivate_Click = value; }
        }

        public Button BtnAreaDesignStateZeroLine
        {
            get { return this.btnAreaDesignStateZeroLine; }
        }

        public Button BtnLayoutAreaTakeout
        {
            get { return this.btnLayoutAreaTakeout; }
        }

        public Button BtnLayoutAreaTakeOutAndFill
        {
            get { return this.btnLayoutAreaTakeOutAndFill; }
        }

        public Button BtnEmbedLayoutAreas
        {
            get { return this.btnEmbeddLayoutAreas; }
        }

        public Button BtnCopyAndPasteShapes
        {
            get { return this.btnCopyAndPasteShapes; }
        }


        //------------------------------//
        //   Seam mode panel exposures  //
        //------------------------------//

        public Button BtnSeamDesignStateSelectionMode
        {
            get { return btnSeamDesignStateSelectionMode; }
        }

        void IBaseForm.SetLineLengthStatusStripDisplay(double length) => this.SetLineLengthStatusStripDisplay(length);

        void IBaseForm.ResetAutoSelectOption() => this.ResetAutoSelectOption();

        void IBaseForm.ResetDesignLayers() => this.ResetDesignLayers();

    }
}
