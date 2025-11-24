
namespace Globals
{
    using System.Windows.Forms;
    using System.Drawing;

    public static class SystemState
    {
        public static IBaseForm BaseForm => SystemGlobals.BaseForm;

        //-----------------------------------//
        //             Design State          //
        //-----------------------------------//

        public delegate void DesignStateChangeHandler(DesignState previousDesignState, DesignState currentDesignState);

        public static event DesignStateChangeHandler DesignStateChanged;

        private static DesignState _designState = DesignState.Area;

        public static DesignState DesignState
        {
            get
            {
                return _designState;
            }

            set
            {
                if (value == _designState)
                {
                    return;
                }

                DesignState _prevDesignState = _designState;

                _designState = value;

                if (DesignStateChanged != null)
                {
                    DesignStateChanged.Invoke(_prevDesignState, _designState);
                }
            }
        }

        //-----------------------------------//
        //             Drawing Mode          //
        //-----------------------------------//

        public delegate void DrawingModeChangedHandler(DrawingMode drawingMode);

        public static event DrawingModeChangedHandler DrawingModeChanged;

        private static DrawingMode _drawingMode = DrawingMode.Default;

        public static DrawingMode DrawingMode
        {
            get
            {
                return _drawingMode;
            }
            set
            {
                if (value == _drawingMode)
                {
                    return;
                }

                _drawingMode = value;

                if (DrawingModeChanged != null)
                {
                    DrawingModeChanged.Invoke(_drawingMode);
                }
            }
        }

        //-----------------------------------//
        //             Drawing Shape         //
        //-----------------------------------//

        public delegate void DrawingShapeChangedHandler(bool drawingShape);

        public static event DrawingShapeChangedHandler DrawingShapeChanged;

        private static bool _drawingShape = false;

        public static bool DrawingShape
        {
            get
            {
                return _drawingShape;
            }

            set
            {
                if (value == _drawingShape)
                {
                    return;
                }

                _drawingShape = value;

                if (DrawingShapeChanged != null)
                {
                    DrawingShapeChanged.Invoke(_drawingShape);
                }
            }
        }

        public static SeamMode SeamMode { get; set; } = SeamMode.Selection;

        public static bool ShowAreas
        {
            get;
            set;
        } = true;

        public static bool ShowDrawings
        {
            get;
            set;
        } = true;

        public delegate void ScaleHasBeenSetChangedEventHandler(bool scaleHasBeenSet);

        public static event ScaleHasBeenSetChangedEventHandler ScaleHasBeenSetChanged;

        private static bool _scaleHasBeenSet = false;

        public static bool ScaleHasBeenSet
        {
            get
            {
                return _scaleHasBeenSet;
            }

            set
            {
                if (_scaleHasBeenSet == value)
                {
                    return;
                }

                _scaleHasBeenSet = value;

                if (ScaleHasBeenSetChanged != null)
                {
                    ScaleHasBeenSetChanged.Invoke(_scaleHasBeenSet);
                }

            }
        }

        public static double ZoomPercent
        {
            get;
            set;
        } = 1.0;

        //-----------------------------------//
        // Base form buttons and check boxes //
        //-----------------------------------//

        private static ToolStripButton _btnRedoSeamsAndCuts;

        private static ToolStripButton _btnTapeMeasure;

        private static  TabControl _tbcPageAreaLine;

        private static ToolStripButton _btnZoomIn;

        private static ToolStripButton _btnZoomOut;

        private static ToolStripDropDownButton _ddbZoomPercent;

        private static ToolStripButton _btnFitCanvas;

        private static UserControl _cccAreaMode;

        private static UserControl _cccLineMode;

        private static ToolStripButton _btnCounters;

        private static ToolStripButton _btnShowLegendForm;

        private static ToolStripButton _btnShowFieldGuides;

        private static ToolStripStatusLabel _tlsDrawingShape;

        private static ToolStripButton _btnSetCustomScale;

        private static ToolStripStatusLabel _tssDrawoutLength;

        private static NumericUpDown _nudFixedWidthFeet;

        private static NumericUpDown _nudFixedWidthInches ;

        private static ToolStripSplitButton _btnMeasuringStick;

        private static Button _btnDoorTakeoutShow;

        private static Button _btnAreaDesignStateZeroLine;

        private static Button _btnNormalLayoutArea;

        private static Button _btnColorOnly;

        private static Button _btnFixedWidth;

        private static Button _btnOversGenerator;

        private static Button _btnEmbeddLayoutAreas;

        private static NumericUpDown _nudOversGeneratorWidthFeet;

        private static NumericUpDown _nudOversGeneratorWidthInches;

        private static Button _btnCopyAndPasteShapes;

        private static ToolStripButton _btnShowLabelEditor;

        private static ToolStripLabel _tssHighlightedArea;

        private static TextBox _txbRemnantWidthFeet;

        private static TextBox _txbRemnantWidthInches;

        private static CheckBox _ckbShowAllSeamsInSeamMode;

        public static bool ShowAreaModeCutIndices => BaseForm.CkbShowAreaModeCutIndices.Checked;

        public static bool AreaModeAutoSeamsShowAll => BaseForm.RbnAreaModeAutoSeamsShowAll.Checked;

        public static bool AreaModeAutoSeamsHideAll => BaseForm.RbnAreaModeAutoSeamsHideAll.Checked;

        public static bool AreaModeAutoSeamsShowUnHideable => BaseForm.RbnAreaModeAutoSeamsShowUnHideable.Checked;

        public static bool SeamModeManualSeamsShowAll => BaseForm.RbnSeamModeManualSeamsShowAll.Checked;

        public static bool SeamModeAutoSeamsHideAll => BaseForm.RbnSeamModeAutoSeamsHideAll.Checked;

        public static bool SeamModeAutoSeamsShowUnHideable => BaseForm.RbnSeamModeAutoSeamsShowUnHideable.Checked;

        public static bool SeamModeAutoSeamsShowAll => BaseForm.RbnSeamModeAutoSeamsShowAll.Checked;

        public static bool ShowSeamModeCuts => BaseForm.CkbShowSeamModeCuts.Checked;

        public static bool ShowSeamModeCutIndices => BaseForm.CkbShowSeamModeCutIndices.Checked;

        public static bool ShowSeamModeOvers => BaseForm.CkbShowSeamModeOvers.Checked;

        public static bool ShowSeamModeUndrs => BaseForm.CkbShowSeamModeUndrs.Checked;

        public static bool ShowEmbeddedCuts => BaseForm.CkbShowEmbeddedCuts.Checked;

        public static bool ShowEmbeddedOvers => BaseForm.CkbShowEmbeddedOvers.Checked;

        public static bool ShowSeamAreaNmbrs => BaseForm.CkbShowSeamModeAreaNmbrs.Checked;

        public static bool ZeroLineState => BaseForm.BtnAreaDesignStateZeroLine.BackColor == Color.Orange;

        public static ToolStripButton BtnRedoSeamsAndCuts => _btnRedoSeamsAndCuts;

        public static bool FixedWidthMode => CurrentLayoutType == LayoutAreaType.FixedWidth;

        public static int FixedWidthFeet => (int)_nudFixedWidthFeet.Value;

        public static int FixedWidthInches => (int)_nudFixedWidthInches.Value;

        public static int FixedWidthInInches => 12 * FixedWidthFeet + FixedWidthInches;

        public static bool TakeoutAreaMode => BaseForm.BtnLayoutAreaTakeout.BackColor == Color.Orange;

        public static bool TakeoutAreaAndFillMode => BaseForm.BtnLayoutAreaTakeOutAndFill.BackColor == Color.Orange;

        public static bool BtnTapeMeasureChecked => _btnTapeMeasure.Checked;

        public static ToolStripButton BtnTapeMeasure => _btnTapeMeasure;

        public static TabControl TbcPageAreaLine => _tbcPageAreaLine;

        public static ToolStripButton BtnZoomIn => _btnZoomIn;

        public static ToolStripButton BtnZoomOut => _btnZoomOut;

        public static ToolStripDropDownButton DdbZoomPercent => _ddbZoomPercent;

        public static ToolStripButton BtnFitCanvas => _btnFitCanvas;

        public static UserControl CccAreaMode => _cccAreaMode;

        public static UserControl CccLineMode => _cccLineMode;

        public static ToolStripButton BtnCounters => _btnCounters;

        public static bool CounterMode => _btnCounters.Checked;

        public static ToolStripButton BtnShowLegendForm => _btnShowLegendForm;
        
        public static ToolStripButton BtnShowFieldGuides => _btnShowFieldGuides;

        public static ToolStripStatusLabel TssDrawoutLength => _tssDrawoutLength;

        public static ToolStripStatusLabel TlsDrawingShape => _tlsDrawingShape;

        public static ToolStripButton BtnSetCustomScale => _btnSetCustomScale;

        public static NumericUpDown NudFixedWidthFeet => _nudFixedWidthFeet;

        public static NumericUpDown NudFixedWidthInches => _nudFixedWidthInches;

        public static ToolStripSplitButton BtnMeasuringStick => _btnMeasuringStick;

        public static Button BtnDoorTakeoutShow => _btnDoorTakeoutShow;

        public static Button BtnAreaDesignStateZeroLine => _btnAreaDesignStateZeroLine;

        public static Button BtnNormalLayoutArea => _btnNormalLayoutArea;

       // public static Button BtnOversGenerator => _btnOversGenerator;

        public static Button BtnColorOnly => _btnColorOnly;

        public static Button BtnFixedWidth => _btnFixedWidth;

        public static Button BtnOversGenerator => _btnOversGenerator;

        public static Button BtnEmbeddLayoutAreas => _btnEmbeddLayoutAreas;

        public static bool EmbedLayoutAreas => BtnEmbeddLayoutAreas.BackColor == Color.Orange;

        public static NumericUpDown NudOversGeneratorWidthFeet => _nudOversGeneratorWidthFeet;

        public static NumericUpDown NudOversGeneratorWidthInches => _nudOversGeneratorWidthInches;

        public static Button BtnCopyAndPasteShapes => _btnCopyAndPasteShapes;

        public static ToolStripButton BtnShowLabelEditor => _btnShowLabelEditor;

        public static bool AreaModeLabelMode => BtnShowLabelEditor.Checked;

        public static ToolStripLabel TssHighlightedArea => _tssHighlightedArea;

        public static TextBox TxbRemnantWidthFeet => _txbRemnantWidthFeet;

        public static TextBox TxbRemnantWidthInches => _txbRemnantWidthInches;

        public static CheckBox CkbShowAllSeamsInSeamMode => _ckbShowAllSeamsInSeamMode;

        // The following is used to signal that the seam state of an existing project needs to be
        // initialized. This is specific to initializing the seam state area locks, which have to be
        // set up the first time the seam state is entered after loading an existing project.

        public static bool InitializeSeamStateForExistingProject = false;

        private static RadioButton _rbnHideAllSeams;
        public static bool CurrentProjectChanged { get; set; }

        public static bool LegendFormFirstLoad { get; set; } = true;

        public static string ProjectName { get; set; } = string.Empty;

        public static bool LoadingExistingProject { get; set; } = false;

        public static double RemnantSeamWidthInFeet { get; set; } = 0;

        private static int? _xtraLgthInch = null;
        private static int? _xtraLgthFeet = null;

        public static int? XtraLgthInch
        {
            get
            {
                return _xtraLgthInch;
            }

            set
            {
                _xtraLgthInch = value;
            }
        }

        public static int? XtraLgthFeet
        {
            get
            {
                return _xtraLgthFeet;
            }

            set
            {
                _xtraLgthFeet = value;
            }
        }

        public static bool OversGeneratorMode => CurrentLayoutType == LayoutAreaType.OversGenerator;

        public static bool AreaCutAndPasteActive => BaseForm.BtnCopyAndPasteShapes.BackColor == Color.Orange;

        public static int OversGeneratorWidthInInches => (int)NudOversGeneratorWidthFeet.Value * 12 + (int) NudOversGeneratorWidthInches.Value;


        public static bool BaseFormHasFocus { get; set; } = false;

        public static AreaPaletteState AreaPaletteState { get; set; } = AreaPaletteState.Unknown;


        public static LayoutAreaType CurrentLayoutType
        {
            get;
            set;
        } 

        public static void Init(
            ToolStripButton btnRedoSeamsAndCuts
            , ToolStripButton btnTapeMeasure
            , TabControl tbcPageAreaLine
            , ToolStripButton btnZoomIn
            , ToolStripButton btnZoomOut
            , ToolStripDropDownButton ddbZoomPercent
            , ToolStripButton btnFitCanvas
            , UserControl cccAreaMode
            , UserControl cccLineMode
            , ToolStripButton btnCounters
            , ToolStripButton btnShowLegendForm
            , ToolStripButton btnShowFieldGuides
            , ToolStripStatusLabel tssDrawoutLength
            , ToolStripStatusLabel tlsDrawingShape
            , ToolStripButton btnSetCustomScale
            , NumericUpDown nudFixedWidthFeet 
            , NumericUpDown nudFixedWidthInches
            , ToolStripSplitButton btnMeasuringStick
            , Button btnDoorTakeoutShow
            , Button btnAreaDesignStateZeroLine
            // Layout area buttons

            , Button btnNormalLayoutArea
            , Button btnColorOnly
            , Button btnFixedWidth
            , Button btnOversGenerator

            , Button btnEmbeddLayoutAreas
            , NumericUpDown nudOversGeneratorWidthFeet
            , NumericUpDown nudOversGeneratorWidthInches
            , Button btnCopyAndPasteShapes
            , ToolStripButton btnShowLabelEditor
            , ToolStripLabel tssHighlightedArea
            , TextBox txbRemnantWidthFeet
            , TextBox txbRemnantWidthInches
            , CheckBox ckbShowAllSeamsInSeamMode)
        {
            _btnRedoSeamsAndCuts = btnRedoSeamsAndCuts;

            _btnTapeMeasure = btnTapeMeasure;

            _tbcPageAreaLine = tbcPageAreaLine;

            _btnZoomIn = btnZoomIn;

            _btnZoomOut = btnZoomOut;

            _ddbZoomPercent = ddbZoomPercent;

            _btnFitCanvas = btnFitCanvas;

            _cccAreaMode = cccAreaMode;

            _cccLineMode = cccLineMode;

            _btnCounters = btnCounters;

            _btnShowLegendForm = btnShowLegendForm;

            _btnShowFieldGuides = btnShowFieldGuides;

            _tssDrawoutLength = tssDrawoutLength;

            _tlsDrawingShape = tlsDrawingShape;

            _btnSetCustomScale = btnSetCustomScale;

            _nudFixedWidthFeet = nudFixedWidthFeet;

            _nudFixedWidthInches = nudFixedWidthInches;

            _btnMeasuringStick = btnMeasuringStick;

            _btnDoorTakeoutShow = btnDoorTakeoutShow;

            _btnAreaDesignStateZeroLine = btnAreaDesignStateZeroLine;

            // Layout Area Buttons

            _btnNormalLayoutArea = btnNormalLayoutArea;
            _btnColorOnly = btnColorOnly;
            _btnFixedWidth = btnFixedWidth;
            _btnOversGenerator = btnOversGenerator;



            _btnEmbeddLayoutAreas = btnEmbeddLayoutAreas;
            
            _nudOversGeneratorWidthFeet = nudOversGeneratorWidthFeet;

            _nudOversGeneratorWidthInches = nudOversGeneratorWidthInches;

            _btnCopyAndPasteShapes = btnCopyAndPasteShapes;

            _btnShowLabelEditor = btnShowLabelEditor;

            _tssHighlightedArea = tssHighlightedArea;

            _txbRemnantWidthFeet = txbRemnantWidthFeet;

            _txbRemnantWidthInches = txbRemnantWidthInches;

            _ckbShowAllSeamsInSeamMode = ckbShowAllSeamsInSeamMode;
        }
    }
}
