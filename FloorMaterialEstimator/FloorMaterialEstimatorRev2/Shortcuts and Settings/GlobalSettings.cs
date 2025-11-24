//-------------------------------------------------------------------------------//
// <copyright file="GlobalSettings.cs" company="Bruun Estimating, LLC">          // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.ShortcutsAndSettings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Utilities;
    using Graphics;

    public static class GlobalSettings
    {
        #region Default Values

        private static int horizontalScrollIncrementDefaultValue = 20;

        private static int verticalScrollIncrementDefaultValue = 20;

        private static OperatingMode operatingModeDefaultValue = OperatingMode.Normal;

        private static bool showMarkerDefaultValue = false;

        private static bool showGuidesDefaultValue = false;

        private static bool showGridDefaultValue = false;

        private static bool showRulersDefaultValue = false;

        private static bool keepInitialGuideOnCanvasDefaultValue = false;

        private static bool keepAllGuidesOnCanvasDefaultValue = false;

        private static LineDrawoutMode lineDrawoutModeDefaultValue = LineDrawoutMode.ShowNormalDrawout;

        private static int yGridlineCountDefaultValue = 24;

        private static double gridlineOffsetDefaultValue = 0.1;

        private static bool showGridlineNumbersDefaultValue = true;

        private static bool snapToAxisDefaultValue = true;

        private static double snapResolutionInDegreesDefaultValue = 20.0;

        private static int areaEditSettingsDefaultIndexDefaultValue = 0;

        private static string areaEditSettingColor1DefaultValue = "Argb(255,0,255,255)";

        private static string areaEditSettingColor2DefaultValue = "Argb(255,255,0,255)";

        private static double areaEditSettingColor1TransparencyDefaultValue = 0.0;

        private static double areaEditSettingColor2TransparencyDefaultValue = 0.0;

        private static int lineEditSettingsDefaultIndexDefaultValue = 0;

        private static string lineEditSettingColor1DefaultValue = "Argb(255,0,255,255)";

        private static string lineEditSettingColor2DefaultValue = "Argb(255,255,0,255)";

        private static int lineEditSettingColor1IntensityDefaultValue = 100;

        private static int lineEditSettingColor2IntensityDefaultValue = 100;

        private static bool showAreaEditFormAsModalDefaultValue = true;

        private static bool showLineEditFormAsModalDefaultValue = true;

        private static bool showSeamEditFormAsModalDefaultValue = true;

        private static int minOverageWidthInInchesDefaultValue = 12;

        private static int minOverageLengthInInchesDefaultValue = 24;

        private static int minUnderageWidthInInchesDefaultValue = 12;

        private static int minUnderageLengthInInchesDefaultValue = 24;

        #endregion

        public static void Initialize()
        {
            if (Program.AppConfig.ContainsKey("showmarker"))
            {
                bool showMarkerVal = false;

                if (bool.TryParse(Program.AppConfig["showmarker"], out showMarkerVal))
                {
                    showMarkerDefaultValue = showMarkerVal;
                }
            }

            if (Program.AppConfig.ContainsKey("showguides"))
            {
                bool showGuidesVal = false;

                if (bool.TryParse(Program.AppConfig["showguides"], out showGuidesVal))
                {
                    showGuidesDefaultValue = showGuidesVal;
                }
            }

            if (Program.AppConfig.ContainsKey("showgrid"))
            {
                bool showGridVal = false;

                if (bool.TryParse(Program.AppConfig["showgrid"], out showGridVal))
                {
                    showGridDefaultValue = showGridVal;
                }
            }

            if (Program.AppConfig.ContainsKey("showrulers"))
            {
                bool showRulersVal = false;

                if (bool.TryParse(Program.AppConfig["showrulers"], out showRulersVal))
                {
                    showRulersDefaultValue = showRulersVal;
                }
            }

            if (Program.AppConfig.ContainsKey("keepinitialguideoncanvas"))
            {
                bool keepInitialGuideOnCanvasVal = false;

                if (bool.TryParse(Program.AppConfig["keepinitialguideoncanvas"], out keepInitialGuideOnCanvasVal))
                {
                    keepInitialGuideOnCanvasDefaultValue = keepInitialGuideOnCanvasVal;
                }
            }

            if (Program.AppConfig.ContainsKey("keepallguidesoncanvas"))
            {
                bool keepAllGuidesOnCanvasVal = false;

                if (bool.TryParse(Program.AppConfig["keepallguidesoncanvas"], out keepAllGuidesOnCanvasVal))
                {
                    keepAllGuidesOnCanvasDefaultValue = keepAllGuidesOnCanvasVal;
                }
            }

            if (Program.AppConfig.ContainsKey("linedrawoutmode"))
            {
                string lineDrawoutMode = Program.AppConfig["linedrawoutmode"];

                switch (lineDrawoutMode)
                {
                    case "ShowLineDrawout":
                        lineDrawoutModeDefaultValue = LineDrawoutMode.ShowNormalDrawout;
                        break;

                    case "HideLineDrawout":
                        lineDrawoutModeDefaultValue = LineDrawoutMode.NoLineDrawout;
                        break;

                    default:

                        break;
                }
            }

            if (Program.AppConfig.ContainsKey("snapresolutionindegrees"))
            {
                double.TryParse(Program.AppConfig["snapresolutionindegrees"], out snapResolutionInDegreesDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("gridlineoffset"))
            {
                double.TryParse(Program.AppConfig["gridlineoffset"], out gridlineOffsetDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("showgridlinenumbers"))
            {
                bool.TryParse(Program.AppConfig["showgridlinenumbers"], out showGridlineNumbersDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("snaptoaxis"))
            {
                bool.TryParse(Program.AppConfig["snaptoaxis"], out snapToAxisDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("areaeditsettingsdefaultindex"))
            {
                int.TryParse(Program.AppConfig["areaeditsettingsdefaultindex"], out areaEditSettingsDefaultIndexDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("areaeditsettingcolor1"))
            {
                areaEditSettingColor1DefaultValue = Program.AppConfig["areaeditsettingcolor1"];
            }

            if (Program.AppConfig.ContainsKey("areaeditsettingcolor2"))
            {
                areaEditSettingColor2DefaultValue = Program.AppConfig["areaeditsettingcolor2"];
            }


            if (Program.AppConfig.ContainsKey("lineeditsettingsdefaultindex"))
            {
                int.TryParse(Program.AppConfig["lineeditsettingsdefaultindex"], out lineEditSettingsDefaultIndexDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("lineeditsettingcolor1"))
            {
                lineEditSettingColor1DefaultValue = Program.AppConfig["lineeditsettingcolor1"];
            }

            if (Program.AppConfig.ContainsKey("lineeditsettingcolor2"))
            {
                lineEditSettingColor2DefaultValue = Program.AppConfig["lineeditsettingcolor2"];
            }

            if (Program.AppConfig.ContainsKey("lineeditsettingcolor1intensity"))
            {
                int.TryParse(Program.AppConfig["lineeditsettingcolor1intensity"], out lineEditSettingColor1IntensityDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("lineeditsettingcolor2intensity"))
            {
                int.TryParse(Program.AppConfig["lineeditsettingcolor2intensity"], out lineEditSettingColor2IntensityDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("showareaeditformasmodal"))
            {
                bool.TryParse(Program.AppConfig["showareaeditformasmodal"], out showAreaEditFormAsModalDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("showlineeditformasmodal"))
            {
                bool.TryParse(Program.AppConfig["showlineeditformasmodal"], out showLineEditFormAsModalDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("showseameditformasmodal"))
            {
                bool.TryParse(Program.AppConfig["showseameditformasmodal"], out showSeamEditFormAsModalDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("minoveragewidthininchesdefaultvalue "))
            {
                int.TryParse(Program.AppConfig["minoveragewidthininchesdefaultvalue"], out minOverageWidthInInchesDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("minoveragelengthininchesdefaultvalue "))
            {
                int.TryParse(Program.AppConfig["minoveragelengthininchesdefaultvalue"], out minOverageLengthInInchesDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("minunderagewidthininchesdefaultvalue "))
            {
                int.TryParse(Program.AppConfig["minunderagewidthininchesdefaultvalue"], out minUnderageWidthInInchesDefaultValue);
            }

            if (Program.AppConfig.ContainsKey("minunderagelengthininchesdefaultvalue "))
            {
                int.TryParse(Program.AppConfig["minunderagelengthininchesdefaultvalue"], out minUnderageLengthInInchesDefaultValue);
            }

            #region Operating Mode

            object operatingModeRegValue = RegistryUtils.GetRegistryValue("OperatingMode");

            if (operatingModeRegValue == null)
            {
                OperatingModeSetting = operatingModeDefaultValue;
            }

            else
            {
                switch (operatingModeRegValue)
                {
                    case "Normal":
                        operatingModeSetting = OperatingMode.Normal;
                        break;

                    case "Development":
                        operatingModeSetting = OperatingMode.Development;
                        break;

                    default:
                        OperatingModeSetting = operatingModeDefaultValue;
                        break;
                }
            }

            #endregion

            #region Line Drawout Mode

            object lineDrawoutModeRegValue = RegistryUtils.GetRegistryValue("LineDrawoutMode");

            if (lineDrawoutModeRegValue == null)
            {
                LineDrawoutModeSetting = lineDrawoutModeDefaultValue;
            }

            else
            {
                switch (lineDrawoutModeRegValue)
                {
                    case "ShowLineDrawout":
                        lineDrawoutModeSetting = LineDrawoutMode.ShowNormalDrawout;
                        break;

                    case "NoLineDrawout":
                        lineDrawoutModeSetting = LineDrawoutMode.NoLineDrawout;
                        break;

                    default:
                        LineDrawoutModeSetting = lineDrawoutModeDefaultValue;
                        break;
                }
            }

            #endregion

            YGridlineCountSetting = (int)RegistryUtils.InitializeValFromReg<int>("YGridlineCount", yGridlineCountDefaultValue);

            GridlineOffsetSetting = (double)RegistryUtils.InitializeValFromReg<double>("GridlineOffset", gridlineOffsetDefaultValue);

            ShowGridlineNumbersSetting = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowGridlineNumbers", showGridlineNumbersDefaultValue);

            SnapToAxis = (bool)RegistryUtils.InitializeValFromReg<bool>("SnapToAxis", snapToAxisDefaultValue);

            ShowGrid = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowGrid", showGridDefaultValue);

            ShowRulers = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowRulers", showRulersDefaultValue);

            SnapResolutionInDegrees = (double)RegistryUtils.InitializeValFromReg<double>("SnapResolutionInDegrees", snapResolutionInDegreesDefaultValue);

            AreaEditSettingsDefaultIndex = (int)RegistryUtils.InitializeValFromReg<int>("AreaEditSettingsDefaultIndex", areaEditSettingsDefaultIndexDefaultValue);

            AreaEditSettingColor1Str = (string)RegistryUtils.InitializeValFromReg<string>("AreaEditSettingColor1Str", areaEditSettingColor1DefaultValue);

            AreaEditSettingColor2Str = (string)RegistryUtils.InitializeValFromReg<string>("AreaEditSettingColor2Str", areaEditSettingColor2DefaultValue);

            AreaEditSettingColor1Transparency = (double)RegistryUtils.InitializeValFromReg<double>("AreaEditSettingColor1Transparency", areaEditSettingColor1TransparencyDefaultValue);

            AreaEditSettingColor2Transparency = (double)RegistryUtils.InitializeValFromReg<double>("AreaEditSettingColor2Transparency", areaEditSettingColor2TransparencyDefaultValue);

            LineEditSettingsDefaultIndex = (int)RegistryUtils.InitializeValFromReg<int>("LineEditSettingsDefaultIndex", lineEditSettingsDefaultIndexDefaultValue);

            LineEditSettingColor1Str = (string)RegistryUtils.InitializeValFromReg<string>("LineEditSettingColor1Str", lineEditSettingColor1DefaultValue);

            LineEditSettingColor2Str = (string)RegistryUtils.InitializeValFromReg<string>("LineEditSettingColor2Str", lineEditSettingColor2DefaultValue);

            LineEditSettingColor1Intensity = (int)RegistryUtils.InitializeValFromReg<int>("LineEditSettingColor1Intensity", lineEditSettingColor1IntensityDefaultValue);

            LineEditSettingColor2Intensity = (int)RegistryUtils.InitializeValFromReg<int>("LineEditSettingColor2Intensity", lineEditSettingColor2IntensityDefaultValue);

            ShowMarker = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowMarker", showMarkerDefaultValue);

            ShowGuides = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowGuides", showGuidesDefaultValue);

            KeepInitialGuideOnCanvas = (bool)RegistryUtils.InitializeValFromReg<bool>("KeepInitialGuideOnCanvas", keepInitialGuideOnCanvasDefaultValue);

            KeepAllGuidesOnCanvas = (bool)RegistryUtils.InitializeValFromReg<bool>("KeepAllGuidesOnCanvas", keepAllGuidesOnCanvasDefaultValue);

            ShowAreaEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowAreaEditFormAsModal", showAreaEditFormAsModalDefaultValue);
            ShowLineEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowLineEditFormAsModal", showLineEditFormAsModalDefaultValue);
            ShowSeamEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowSeamEditFormAsModal", showSeamEditFormAsModalDefaultValue);

            MinOverageWidthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinOverageWidthInInches", minOverageWidthInInchesDefaultValue);
            MinOverageLengthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinOverageLengthInInches", minOverageLengthInInchesDefaultValue);
            MinUnderageWidthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinUnderageWidthInInches", minUnderageWidthInInchesDefaultValue);
            MinUnderageLengthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinUnderageLengthInInches", minUnderageLengthInInchesDefaultValue);
        }

        #region Button Background Colors

        public static Color CheckedBackgroundColor = Color.Gold;

        public static Color UncheckedBackgroundColor = Color.FromArgb(224, 224, 224);

        #endregion

        #region Operating Mode

        private static OperatingMode operatingModeSetting = OperatingMode.Unknown;

        public static OperatingMode OperatingModeSetting
        {
            get
            {
                return operatingModeSetting;
            }

            set
            {
                operatingModeSetting = value;

                RegistryUtils.SetRegistryValue("OperatingMode", operatingModeSetting.ToString());
            }
        }

        #endregion

        #region Line Drawout Mode

        private static LineDrawoutMode lineDrawoutModeSetting = LineDrawoutMode.Unknown;

        public static LineDrawoutMode LineDrawoutModeSetting
        {
            get
            {
                return lineDrawoutModeSetting;
            }

            set
            {
                lineDrawoutModeSetting = value;

                RegistryUtils.SetRegistryValue("LineDrawoutMode", lineDrawoutModeSetting.ToString());
            }
        }

        #endregion

        #region Y GridlineCount

        private static int yGridlineCountSetting = yGridlineCountDefaultValue;

        public static int YGridlineCountSetting
        {
            get
            {
                return yGridlineCountSetting;
            }

            set
            {
                yGridlineCountSetting = value;

                RegistryUtils.SetRegistryValue("YGridlineCount", yGridlineCountSetting.ToString());
            }
        }

        #endregion

        #region Gridline Offset

        private static double gridlineOffsetSetting = gridlineOffsetDefaultValue;

        public static double GridlineOffsetSetting
        {
            get
            {
                return gridlineOffsetSetting;
            }

            set
            {
                gridlineOffsetSetting = value;

                RegistryUtils.SetRegistryValue("GridlineOffset", gridlineOffsetSetting.ToString());
            }
        }

        #endregion

        #region Show Gridline Numbers

        private static bool showGridlineNumbersSetting = showGridlineNumbersDefaultValue;

        public static bool ShowGridlineNumbersSetting
        {
            get
            {
                return showGridlineNumbersSetting;
            }

            set
            {
                showGridlineNumbersSetting = value;

                RegistryUtils.SetRegistryValue("ShowGridlineNumbers", showGridlineNumbersSetting.ToString());
            }
        }

        #endregion

        #region Scrolling

        private static int horizontalScrollIncrement = horizontalScrollIncrementDefaultValue;
        private static int verticalScrollIncrement = verticalScrollIncrementDefaultValue;

        public static int HorizontalScrollIncrement
        {
            get
            {
                return horizontalScrollIncrement;
            }

            set
            {
                horizontalScrollIncrement = value;

                RegistryUtils.SetRegistryValue("HorizontalScrollIncrement", horizontalScrollIncrement.ToString());
            }
        }

        public static int VerticalScrollIncrement
        {
            get
            {
                return verticalScrollIncrement;
            }

            set
            {
                verticalScrollIncrement = value;

                RegistryUtils.SetRegistryValue("VerticalScrollIncrement", verticalScrollIncrement.ToString());
            }
        }
        #endregion

        #region Snap To Axis

        private static bool snapToAxis = snapToAxisDefaultValue;

        public static bool SnapToAxis
        {
            get
            {
                return snapToAxis;
            }

            set
            {
                snapToAxis = value;

                RegistryUtils.SetRegistryValue("SnapToAxis", snapToAxis.ToString());
            }
        }

        #endregion

        #region Snap Resolution In Degrees

        private static double snapResolutionInDegrees = snapResolutionInDegreesDefaultValue;

        public static double SnapResolutionInDegrees
        {
            get
            {
                return snapResolutionInDegrees;
            }

            set
            {
                snapResolutionInDegrees = value;

                RegistryUtils.SetRegistryValue("SnapResolutionInDegrees", snapResolutionInDegrees.ToString());
            }
        }

        #endregion

        #region Grid and Rulers

        private static bool showGrid = showGridDefaultValue;

        public static bool ShowGrid
        {
            get { return showGrid; }

            set { showGrid = value; RegistryUtils.SetRegistryValue("ShowGrid", showGrid.ToString()); }
        }

        private static bool showRulers = showGridDefaultValue;

        public static bool ShowRulers
        {
            get { return showRulers; }

            set { showRulers = value; RegistryUtils.SetRegistryValue("ShowRulers", showRulers.ToString()); }
        }

        #endregion

        #region Area Edit Settings

        public static Color SelectedAreaColor
        {
            get
            {
                if (areaEditSettingsDefaultIndex == 0)
                {
                    return AreaEditSettingColor1;
                }

                else if (areaEditSettingsDefaultIndex == 1)
                {
                    return AreaEditSettingColor2;
                }

                else
                {
                    return Color.FromArgb(3, 252, 239);
                }
            }
        }

        public static double SelectedAreaOpacity
        {
            get
            {
                if (areaEditSettingsDefaultIndex == 0)
                {
                    return Math.Min(1.0, Math.Max(0.0, 1.0 - AreaEditSettingColor1Transparency));
                }

                else if (areaEditSettingsDefaultIndex == 1)
                {
                    return Math.Min(1.0, Math.Max(0.0, 1.0 - AreaEditSettingColor2Transparency));
                }

                else
                {
                    return 1.0;
                }
            }
        }

        private static int areaEditSettingsDefaultIndex;

        public static int AreaEditSettingsDefaultIndex
        {
            get
            {
                return areaEditSettingsDefaultIndex;
            }

            set
            {
                areaEditSettingsDefaultIndex = value;

                RegistryUtils.SetRegistryValue("AreaEditSettingsDefaultIndex", areaEditSettingsDefaultIndex.ToString());
            }
        }

        public static Color AreaEditSettingColor1;
        public static Color AreaEditSettingColor2;

        public static string AreaEditSettingColor1Str
        {
            get
            {
                return DrawingUtils.FormatArgbColorStr(AreaEditSettingColor1);
            }

            set
            {
                AreaEditSettingColor1 = DrawingUtils.ParseArgbColorStr(value);
            }
        }

        public static string AreaEditSettingColor2Str
        {
            get
            {
                return DrawingUtils.FormatArgbColorStr(AreaEditSettingColor2);
            }

            set
            {
                AreaEditSettingColor2 = DrawingUtils.ParseArgbColorStr(value);
            }
        }

        private static double areaEditSettingColor1Transparency = 0.0;

        public static double AreaEditSettingColor1Transparency
        {
            get
            {
                return areaEditSettingColor1Transparency;
            }

            set
            {
                areaEditSettingColor1Transparency = value;

                RegistryUtils.SetRegistryValue("AreaEditSettingColor1Transparency", areaEditSettingColor1Transparency.ToString());
            }
        }


        private static double areaEditSettingColor2Transparency = 0.0;

        public static double AreaEditSettingColor2Transparency
        {
            get
            {
                return areaEditSettingColor2Transparency;
            }

            set
            {
                areaEditSettingColor2Transparency = value;

                RegistryUtils.SetRegistryValue("AreaEditSettingColor2Transparency", areaEditSettingColor2Transparency.ToString());
            }
        }

        #endregion


        #region Line Edit Settings

        public static Color SelectedLineColor
        {
            get
            {
                if (lineEditSettingsDefaultIndex == 0)
                {
                    return DrawingUtils.modifyColorByIntensity(
                        LineEditSettingColor1, lineEditSettingColor1Intensity);
                }

                else if (lineEditSettingsDefaultIndex == 1)
                {
                    return DrawingUtils.modifyColorByIntensity(
                        LineEditSettingColor2, lineEditSettingColor2Intensity);
                }

                else
                {
                    return Color.FromArgb(0, 0, 0);
                }
            }
        }

        private static int lineEditSettingsDefaultIndex;

        public static int LineEditSettingsDefaultIndex
        {
            get
            {
                return lineEditSettingsDefaultIndex;
            }

            set
            {
                lineEditSettingsDefaultIndex = value;

                RegistryUtils.SetRegistryValue("LineEditSettingsDefaultIndex", lineEditSettingsDefaultIndex.ToString());
            }
        }

        public static Color LineEditSettingColor1;
        public static Color LineEditSettingColor2;

        public static string LineEditSettingColor1Str
        {
            get
            {
                return DrawingUtils.FormatArgbColorStr(LineEditSettingColor1);
            }

            set
            {
                LineEditSettingColor1 = DrawingUtils.ParseArgbColorStr(value);
            }
        }

        public static string LineEditSettingColor2Str
        {
            get
            {
                return DrawingUtils.FormatArgbColorStr(LineEditSettingColor2);
            }

            set
            {
                LineEditSettingColor2 = DrawingUtils.ParseArgbColorStr(value);
            }
        }

        private static int lineEditSettingColor1Intensity;
        private static int lineEditSettingColor2Intensity;

        public static int LineEditSettingColor1Intensity
        {
            get
            {
                return lineEditSettingColor1Intensity;
            }

            set
            {
                lineEditSettingColor1Intensity = value;

                RegistryUtils.SetRegistryValue("LineEditSettingColor1Intensity", lineEditSettingColor1Intensity.ToString());
            }
        }

        public static int LineEditSettingColor2Intensity
        {
            get
            {
                return lineEditSettingColor2Intensity;
            }

            set
            {
                lineEditSettingColor2Intensity = value;

                RegistryUtils.SetRegistryValue("LineEditSettingColor2Intensity", lineEditSettingColor2Intensity.ToString());
            }
        }

        private static bool showGuides;

        public static bool ShowGuides
        {
            get
            {
                return showGuides;
            }

            set
            {
                showGuides = value;

                RegistryUtils.SetRegistryValue("ShowGuides", showGuides.ToString());

            }
        }

        private static bool showMarker;

        public static bool ShowMarker
        {
            get
            {
                return showMarker;
            }

            set
            {
                showMarker = value;

                RegistryUtils.SetRegistryValue("ShowMarker", showMarker.ToString());
            }
        }

        private static bool keepInitialGuideOnCanvas;

        public static bool KeepInitialGuideOnCanvas
        {
            get
            {
                return keepInitialGuideOnCanvas;
            }

            set
            {
                keepInitialGuideOnCanvas = value;

                RegistryUtils.SetRegistryValue("KeepInitialGuideOnCanvas", keepInitialGuideOnCanvas.ToString());
            }
        }

        private static bool keepAllGuidesOnCanvas;

        public static bool KeepAllGuidesOnCanvas
        {
            get
            {
                return keepAllGuidesOnCanvas;
            }

            set
            {
                keepAllGuidesOnCanvas = value;

                RegistryUtils.SetRegistryValue("KeepAllGuidesOnCanvas", keepAllGuidesOnCanvas.ToString());
            }
        }

        private static double markerWidth = 0.05;

        public static double MarkerWidth
        {
            get
            {
                return markerWidth;
            }

            set
            {
                markerWidth = value;

                RegistryUtils.SetRegistryValue("MarkerWidth", markerWidth.ToString());
            }
        }

        #endregion

        #region Edit Form Display Policies

        private static bool showAreaEditFormAsModal;

        public static bool ShowAreaEditFormAsModal
        {
            get
            {
                return showAreaEditFormAsModal;
            }

            set
            {
                showAreaEditFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowAreaEditFormAsModal", showAreaEditFormAsModal.ToString());
            }
        }

        private static bool showLineEditFormAsModal;

        public static bool ShowLineEditFormAsModal
        {
            get
            {
                return showLineEditFormAsModal;
            }

            set
            {
                showLineEditFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowLineEditFormAsModal", showLineEditFormAsModal.ToString());
            }
        }

        private static bool showSeamEditFormAsModal;

        public static bool ShowSeamEditFormAsModal
        {
            get
            {
                return showSeamEditFormAsModal;
            }

            set
            {
                showSeamEditFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowSeamEditFormAsModal", showSeamEditFormAsModal.ToString());
            }
        }

        private static int minOverageWidthInInches;

        public static int MinOverageWidthInInches
        {
            get
            {
                return minOverageWidthInInches;
            }

            set
            {
                minOverageWidthInInches = value;

                RegistryUtils.SetRegistryValue("MinOverageWidthInInches", minOverageWidthInInches.ToString());
            }
        }

        private static int minOverageLengthInInches;

        public static int MinOverageLengthInInches
        {
            get
            {
                return minOverageLengthInInches;
            }

            set
            {
                minOverageLengthInInches = value;

                RegistryUtils.SetRegistryValue("MinOverageLengthInInches", minOverageLengthInInches.ToString());
            }
        }

        private static int minUnderageWidthInInches;

        public static int MinUnderageWidthInInches
        {
            get
            {
                return minUnderageWidthInInches;
            }

            set
            {
                minUnderageWidthInInches = value;

                RegistryUtils.SetRegistryValue("MinUnderageWidthInInches", minUnderageWidthInInches.ToString());
            }
        }

        private static int minUnderageLengthInInches;

        public static int MinUnderageLengthInInches
        {
            get
            {
                return minUnderageLengthInInches;
            }

            set
            {
                minUnderageLengthInInches = value;

                RegistryUtils.SetRegistryValue("MinUnderageLengthInInches", minUnderageLengthInInches.ToString());
            }
        }

        #endregion
    }

    public enum OperatingMode
    {
        Unknown = 0,
        Normal = 1,
        Development = 2
    }
}
