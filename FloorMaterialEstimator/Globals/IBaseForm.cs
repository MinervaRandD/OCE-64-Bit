using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    using System.Windows.Forms;

    public interface IBaseForm
    {
        Form Form { get; }


        CheckBox CkbShowAreaModeCutIndices { get; }


        RadioButton RbnAreaModeAutoSeamsShowAll { get; }

        RadioButton RbnAreaModeAutoSeamsHideAll { get; }

        RadioButton RbnAreaModeAutoSeamsShowUnHideable { get; }

        RadioButton RbnAreaModeManualSeamsShowAll { get; }




        RadioButton RbnSeamModeAutoSeamsShowAll { get; }

        RadioButton RbnSeamModeAutoSeamsHideAll { get; }

        RadioButton RbnSeamModeAutoSeamsShowUnHideable { get; }

        RadioButton RbnSeamModeManualSeamsShowAll { get; }

        CheckBox CkbShowSeamModeCuts { get; }

        CheckBox CkbShowSeamModeCutIndices { get; }

        CheckBox CkbShowSeamModeOvers { get; }

        CheckBox CkbShowSeamModeUndrs { get; }

        CheckBox CkbShowEmbeddedCuts { get; }

        CheckBox CkbShowEmbeddedOvers { get; }

        CheckBox CkbShowSeamModeAreaNmbrs { get; }

        RadioButton RbnDoorTakeoutOther { get; }

        RadioButton RbnDoorTakeout3Ft { get; }

        RadioButton RbnDoorTakeout6Ft { get; }




        Button BtnLayoutLine1XMode { get; }

        Button BtnLayoutLine2XMode { get; }



        ToolStripButton BtnShowLegendForm { get; }

        ToolStripButton BtnHideFieldGuides { get; }

        ToolStripButton BtnShowFieldGuides { get; }


        ToolStripButton BtnSnapToGrid { get; }

        //------------------------------//
        //   Area mode panel exposures  //
        //------------------------------//

        Button BtnNormalLayoutArea { get; }

        Button BtnColorOnly { get; }

        Button BtnFixedWidth { get; }

        Button BtnOversGenerator { get; }
        
        Button BtnAreaDesignStateZeroLine { get; }

        Button BtnLayoutAreaTakeout { get; }

        Button BtnLayoutAreaTakeOutAndFill { get; }

        Button BtnEmbedLayoutAreas { get; }

        Button BtnCopyAndPasteShapes { get; }

        //------------------------------//
        //   Seam mode panel exposures  //
        //------------------------------//

        Button BtnSeamDesignStateSelectionMode { get; }


        void SetLineLengthStatusStripDisplay(double length);

        void ResetAutoSelectOption();

        void ResetDesignLayers();

        //---------------------------------------------------------- //
        // Necessary for interactions of door takeouts with counters //
        //---------------------------------------------------------- //

        Button BtnDoorTakeoutShow { get; }

        Button BtnDoorTakeoutActivate { get; }

        Action<object, EventArgs> BtnDoorTakeoutActivate_Click { get; }

        Button BtnLineModeCounterActivate { get; set; }

        Action<object, EventArgs> BtnLineModeCounterActivate_Click { get; set; }
        // -- End -- //
    }
}
