
using System.Drawing.Drawing2D;
using System.Security.Policy;

namespace Globals
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    using Geometry;

    public static class SystemGlobals
    {
       public static void Init()
        {
            //------------------------------------------------------------------//
            Assembly asm = Assembly.GetExecutingAssembly();

            CustomScaleLineColoredImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.CustomScaleColored.png"));
            CustomScaleLineUncoloredImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.CustomWarning.png"));

            ExpandCanvasImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.HidePanes.png"));
            ContractCanvasImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.ShowPanes.png"));

            RedoSeamsOffImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.RedoSeamsOff.png"));
            RedoSeamsOnImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.RedoSeamsOn.png"));

            ShowAreaImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.ShowArea.png"));
            HideAreaImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.HideArea.png"));

            ShowDrawingImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.ShowDrawing.png"));
            HideDrawingImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.HideDrawing.png"));

            MeasuringStickNotSelectedImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.MeasuringStickNotSelected.png"));
            MeasuringStickSelectedImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.MeasuringStickSelected.png"));

            MRghtButtonDownImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.MRghtButtonDown.png"));
            MLeftButtonDownImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.MLeftButtonDown.png"));

            MButtonUpImage = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.MButtonUp.png"));

            SmallUpArrow = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.SmallUpArrow.png"));

            SmallDownArrow = new Bitmap(asm.GetManifestResourceStream("Globals.Image_Resources.SmallDownArrow.png"));
        }

        public static Bitmap CustomScaleLineColoredImage { get; private set; }
        public static Bitmap CustomScaleLineUncoloredImage { get; private set; }

        public static Bitmap RedoSeamsOffImage { get; private set; }
        public static Bitmap RedoSeamsOnImage { get; private set; }

        public static Bitmap ExpandCanvasImage { get; private set; }

        public static Bitmap ShowDrawingImage { get; private set; }
        public static Bitmap HideDrawingImage { get; private set; }

        public static Bitmap MeasuringStickNotSelectedImage { get; private set; }
        public static Bitmap MeasuringStickSelectedImage { get; private set; }

        public static Bitmap MRghtButtonDownImage { get; private set; }
        public static Bitmap MLeftButtonDownImage { get; private set; }

        public static Bitmap MButtonUpImage { get; private set; }

        public static Bitmap SmallUpArrow { get; private set; }

        public static Bitmap SmallDownArrow { get; private set; }

        public static double FixedWidthScaleInInches()
        {
            return (12.0 * (double)SystemState.NudFixedWidthFeet.Value) + (double)SystemState.NudFixedWidthInches.Value;
        }

        public const double CompletedShapeLineWidthInPts = 0.25;

        public const string ZeroLineStyleFormula = "9";

        public const string AreaModePerimeterLineStyleFormula = "1";

        public static Color AreaModePerimeterLineColor = Color.Gray;

        // The following is added so as to decouple the area finish palette from the base form. //

        public static Action SetupAllSeamStateSeamLayersForSelectedArea;

        public static Action ActivateTapeMeasure;

        public static Action<bool> OversUndersFormUpdate;

        public static Func<Point> GetCursorPosition;

        public static Action<Point> SetCursorPosition;

        public static Action UpdateAreaSeamsUndrsOversDataDisplay;

        public static Action BtnFilterAreasClick;

        public static Action BtnFilterLinesClick;

        public static UserControl RemnantsView;

        public static UserControl AreaView;

        public static UserControl LineView;

        public static UserControl SeamView;

        public static Action<string> RemoveRemnantArea { get; set; }

        public static Action<int> CompletePolylineDraw { get; set; }

        public static Bitmap ContractCanvasImage { get; set; }
        public static Bitmap ShowAreaImage { get; set; }
        public static Bitmap HideAreaImage { get; set; }

        public static IBaseForm BaseForm { get; set; }

        //public static double DefaultAreaModeLegendSize
        //{
        //    get;
        //    set;
        //}

        public static double DefaultLineModeLegendSize { get; set; }

        //public static double CurrentAreaModeLegendSize
        //{
        //    get;
        //    set;
        //}

        public static double CurrentLineModeLegendSize { get; set; }

        public static string OriginalImagePath { get; set; } = string.Empty;

        public static string paletteSource { get;set; } = string.Empty;
        public static double Rotation { get; set; } = 0;

        public static bool DebugFlag { get; set; }

        public static Action<bool> SetAreaCopyAndPastState { get;  set; }

        /************************************************************************/
        /* The following section includes control parameters for legend display */
        /************************************************************************/

        /* Area Legend */

        public static bool AreaLegendLocateToClick { get; set; } = false;

        public static bool ShowAreaLegendInAreaMode { get; set; } = false ;

        public static bool ShowAreaLegendInLineMode { get; set; } = false;

        public static bool ShowAreaLegendFinishes { get; set; } = true;

        public static bool ShowAreaLegendCounters { get; set; } = false;

        public static bool ShowAreaLegendNotes { get; set; } = false;

        public static Coordinate AreaLegendLocation { get; set; } = new Coordinate();

        public static double AreaLegendScale { get; set; } = .25;

        public static double DefaultAreaLegendScale { get; set; } = 0.25;

        /* Line Legend */

        public static string AreaLegendNotes { get; set; } = null;

        public static bool LineLegendLocateToClick { get; set; } = false;

        public static bool ShowLineLegendInAreaMode { get; set; } = false;

        public static bool ShowLineLegendInLineMode { get; set; } = false;

        public static bool ShowLineLegendLines { get; set; } = true;

        public static bool ShowLineLegendNotes { get; set; } = false;

        public static Coordinate LineLegendLocation { get; set; } = new Coordinate();

        public static double LineLegendScale { get; set; } = 0.25;

        public static double DefaultLineLegendScale { get; set; } = 0.25;

        public static string LineLegendNotes { get; set; } = null;
    }


}
