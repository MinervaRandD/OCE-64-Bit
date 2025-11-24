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

using Utilities;

namespace SettingsLib
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Graphics;
    using Utilities;
    using TracerLib;

    public static class GlobalSettings
    {

        #region Default Values

        #region Debug


        #endregion

        private static int horizontalScrollIncrementDefaultValue = 20;

        private static int verticalScrollIncrementDefaultValue = 20;

        private static OperatingMode operatingModeDefaultValue = OperatingMode.Normal;

        private static bool showMarkerDefaultValue = false;

        private static bool showGuidesDefaultValue = false;

        private static bool showGridDefaultValue = false;

        private static bool showRulersDefaultValue = false;

        private static bool showPanAndZoomDefaultValue = false;

        private static bool keepInitialGuideOnCanvasDefaultValue = false;

        private static bool keepAllGuidesOnCanvasDefaultValue = false;

        private static LineDrawoutMode lineDrawoutModeDefaultValue = LineDrawoutMode.ShowNormalDrawout;

        private static int yGridlineCountDefaultValue = 24;

        private static double gridlineOffsetDefaultValue = 0.0;

        private static double gridSpacingInInchesDefaultValue = 0.25;

        private static double gridOffsetInInchesDefaultValue = 0.0;

        private static bool showGridlineNumbersDefaultValue = true;

        private static double smallFillWastePercentDefaultValue = 25.0;

        #region Snap controls

        private static bool snapToAxisDefaultValue = true;

        private static double snapResolutionInDegreesDefaultValue = 20.0;

        private static double snapDistanceDefaultValue = 0.5;

        #endregion

        private static int fieldGuideADefaultValue = 255;
        private static int fieldGuideRDefaultValue = 255;
        private static int fieldGuideGDefaultValue = 0;
        private static int fieldGuideBDefaultValue = 0;

        private static double fieldGuideOpacityDefaultValue = 1.0;

        private static double fieldGuideWidthInPtsDefaultValue = 2;

        private static int fieldGuideStyleDefaultValue = 1;

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

        #region Filter form display format

        private static bool showAreaFilterFormAsModalDefaultValue = true;

        private static bool showLineFilterFormAsModalDefaultValue = true;

        private static bool showSeamFilterFormAsModalDefaultValue = true;

        #endregion

        #region Edit form display format

        private static bool showAreaEditFormAsModalDefaultValue = false;

        private static bool showLineEditFormAsModalDefaultValue = false;

        private static bool showSeamEditFormAsModalDefaultValue = false;

        private static bool showFieldGuideEditFormAsModalDefaultValue = false;

        #endregion

        private static bool autoReseamAndCutOnRollWidthOrScaleChangeDefaultValue = false;

        private static string shortcutOrientationDefaultValue = "RightHanded";

        private static bool lockScrollWhenDrawingSmallerThanCanvasDefaultValue = false;

        private static double counterSmallCircleRadiusDefaultValue = 0.25;

        private static double counterMediumCircleRadiusDefaultValue = 0.50;

        private static double counterLargeCircleRadiusDefaultValue = 0.75;

        private static double counterSmallFontInPtsDefaultValue = 6;

        private static double counterMediumFontInPtsDefaultValue = 8;

        private static double counterLargeFontInPtsDefaultValue = 10;

        private static double areaIndexFontInPtsDefaultValue = 20;

        private static double cutIndexFontInPtsDefaultValue = 14;

        private static double cutIndexCircleRadiusDefaultValue = .25;

        private static bool showCutIndexCirclesDefaultValue = true;

        private static double overageIndexFontInPtsDefaultValue = 14;

        private static double underageIndexFontInPtsDefaultValue = 14;

        private static double defaultDrawingScaleInInchesDefaultValue = 12;

        private static double defaultNewDrawingWidthInInchesDefaultValue = 20;

        private static double defaultNewDrawingHeightInInchesDefaultValue = 12;

        private static bool defaultLockAreaWhen100PctSeamed = true;

        private static int graphicsPrecisionDefaultValue = 6;

        private static int initProjectDesignStateDefaultValue = 1;

        private static bool defaultStartupFullScreenMode = false;

        private static bool showSetScaleReminderDefaultValue = false;

        private static bool allowEditingOfShortcutKeysDefaultValue = false;

        private static bool allowEditingOfToolTipsDefaultValue = false;

        private static int mouseWheelZoomIntervalDefaultValue = 5;

        private static int zoomInOutButtonPercentDefaultValue = 25;

        private static int arrowMoveIncrementDefaultValue = 1;

        private static bool autosaveEnabledDefaultValue = false;

        private static int autosaveIntervalInSecondsDefaultValue = 0;

        private static TraceLevel traceLevelDefaultValue = TraceLevel.Info | TraceLevel.Error | TraceLevel.Exception;

        private static bool updateDebugFormDynamicallyDefaultValue = false;

        private static bool validateOnProjectSaveDefaultValue = true;


        #endregion

        #region Minimums for overs and unders

        private static int minOverageWidthInInchesDefaultValue = 1;

        private static int minOverageLengthInInchesDefaultValue = 1;

        private static int minUnderageWidthInInchesDefaultValue = 1;

        private static int minUnderageLengthInInchesDefaultValue = 1;

        private static int minOverComboWidthInInchesDefaultValue = 12;

        private static int minOverComboLengthInInchesDefaultValue = 12;

        private static int minUnderComboWidthInInchesDefaultValue = 12;

        private static int minUnderComboLengthInInchesDefaultValue = 12;

        #endregion

        #region Roll Width Defaults

        private static int rollWidthDefaultValueFeetDefaultValue = 12;

        private static int rollWidthDefaultValueInchesDefaultValue = 0;

        private static int rollOverlapDefaultValueInchesDefaultValue = 3;

        private static int rollExtraPerCutDefaultValueInchesDefaultValue = 3;

        #endregion

        public static bool showAreaOutlineInLineModeDefaultValue = false;

        #region Debug

        private static bool validateRolloutAndCutWidthDefaultValue = false;

        #endregion

        public static void Initialize(Dictionary<string, string> appConfig)
        {
            if (appConfig.ContainsKey("showmarker"))
            {
                bool showMarkerVal = false;

                if (bool.TryParse(appConfig["showmarker"], out showMarkerVal))
                {
                    showMarkerDefaultValue = showMarkerVal;
                }
            }

            if (appConfig.ContainsKey("showguides"))
            {
                bool showGuidesVal = false;

                if (bool.TryParse(appConfig["showguides"], out showGuidesVal))
                {
                    showGuidesDefaultValue = showGuidesVal;
                }
            }

            if (appConfig.ContainsKey("showgrid"))
            {
                bool showGridVal = false;

                if (bool.TryParse(appConfig["showgrid"], out showGridVal))
                {
                    showGridDefaultValue = showGridVal;
                }
            }

            if (appConfig.ContainsKey("showrulers"))
            {
                bool showRulersVal = false;

                if (bool.TryParse(appConfig["showrulers"], out showRulersVal))
                {
                    showRulersDefaultValue = showRulersVal;
                }
            }

            if (appConfig.ContainsKey("showpanandzoom"))
            {
                bool showPanAndZoomVal = false;

                if (bool.TryParse(appConfig["showpanandzoom"], out showPanAndZoomVal))
                {
                    showPanAndZoomDefaultValue = showPanAndZoomVal;
                }
            }

            if (appConfig.ContainsKey("keepinitialguideoncanvas"))
            {
                bool keepInitialGuideOnCanvasVal = false;

                if (bool.TryParse(appConfig["keepinitialguideoncanvas"], out keepInitialGuideOnCanvasVal))
                {
                    keepInitialGuideOnCanvasDefaultValue = keepInitialGuideOnCanvasVal;
                }
            }

            if (appConfig.ContainsKey("keepallguidesoncanvas"))
            {
                bool keepAllGuidesOnCanvasVal = false;

                if (bool.TryParse(appConfig["keepallguidesoncanvas"], out keepAllGuidesOnCanvasVal))
                {
                    keepAllGuidesOnCanvasDefaultValue = keepAllGuidesOnCanvasVal;
                }
            }

            if (appConfig.ContainsKey("linedrawoutmode"))
            {
                string lineDrawoutMode = appConfig["linedrawoutmode"];

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

            if (appConfig.ContainsKey("snapresolutionindegrees"))
            {
                double.TryParse(appConfig["snapresolutionindegrees"], out snapResolutionInDegreesDefaultValue);
            }

            if (appConfig.ContainsKey("snapdistance"))
            {
                double.TryParse(appConfig["snapdistance"], out snapDistanceDefaultValue);
            }

            if (appConfig.ContainsKey("gridlineoffset"))
            {
                double.TryParse(appConfig["gridlineoffset"], out gridlineOffsetDefaultValue);
            }

            if (appConfig.ContainsKey("gridspacingininches"))
            {
                double.TryParse(appConfig["gridspacingininches"], out gridSpacingInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("gridoffsetininches"))
            {
                double.TryParse(appConfig["gridoffsetininches"], out gridOffsetInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("showgridlinenumbers"))
            {
                bool.TryParse(appConfig["showgridlinenumbers"], out showGridlineNumbersDefaultValue);
            }

            if (appConfig.ContainsKey("snaptoaxis"))
            {
                bool.TryParse(appConfig["snaptoaxis"], out snapToAxisDefaultValue);
            }


            #region Field guide format elements

            if (appConfig.ContainsKey("fieldguideadefaultvalue"))
            {
                int.TryParse(appConfig["fieldguideadefaultvalue"], out fieldGuideADefaultValue);
            }

            if (appConfig.ContainsKey("fieldguiderdefaultvalue"))
            {
                int.TryParse(appConfig["fieldguiderdefaultvalue"], out fieldGuideRDefaultValue);
            }

            if (appConfig.ContainsKey("fieldguidegdefaultvalue"))
            {
                int.TryParse(appConfig["fieldguidegdefaultvalue"], out fieldGuideGDefaultValue);
            }

            if (appConfig.ContainsKey("fieldguidebdefaultvalue"))
            {
                int.TryParse(appConfig["fieldguidebdefaultvalue"], out fieldGuideBDefaultValue);
            }

            if (appConfig.ContainsKey("fieldguidewidthinptsdefaultvalue"))
            {
                double.TryParse(appConfig["fieldguidewidthinptsdefaultvalue"], out fieldGuideWidthInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("fieldguideopacitydefaultvalue"))
            {
                double.TryParse(appConfig["fieldguideopacitydefaultvalue"], out fieldGuideOpacityDefaultValue);
            }

            if (appConfig.ContainsKey("fieldguidelinestyledefaultvalue"))
            {
                int.TryParse(appConfig["fieldguidelinestyledefaultvalue"], out fieldGuideStyleDefaultValue);
            }

            #endregion

            if (appConfig.ContainsKey("areaeditsettingsdefaultindex"))
            {
                int.TryParse(appConfig["areaeditsettingsdefaultindex"], out areaEditSettingsDefaultIndexDefaultValue);
            }

            if (appConfig.ContainsKey("areaeditsettingcolor1"))
            {
                areaEditSettingColor1DefaultValue = appConfig["areaeditsettingcolor1"];
            }

            if (appConfig.ContainsKey("areaeditsettingcolor2"))
            {
                areaEditSettingColor2DefaultValue = appConfig["areaeditsettingcolor2"];
            }


            if (appConfig.ContainsKey("lineeditsettingsdefaultindex"))
            {
                int.TryParse(appConfig["lineeditsettingsdefaultindex"], out lineEditSettingsDefaultIndexDefaultValue);
            }

            if (appConfig.ContainsKey("lineeditsettingcolor1"))
            {
                lineEditSettingColor1DefaultValue = appConfig["lineeditsettingcolor1"];
            }

            if (appConfig.ContainsKey("lineeditsettingcolor2"))
            {
                lineEditSettingColor2DefaultValue = appConfig["lineeditsettingcolor2"];
            }

            if (appConfig.ContainsKey("lineeditsettingcolor1intensity"))
            {
                int.TryParse(appConfig["lineeditsettingcolor1intensity"], out lineEditSettingColor1IntensityDefaultValue);
            }

            if (appConfig.ContainsKey("lineeditsettingcolor2intensity"))
            {
                int.TryParse(appConfig["lineeditsettingcolor2intensity"], out lineEditSettingColor2IntensityDefaultValue);
            }

            #region Filter form display format

            if (appConfig.ContainsKey("showareafilterformasmodal"))
            {
                bool.TryParse(appConfig["showareafilterformasmodal"], out showAreaFilterFormAsModalDefaultValue);
            }

            if (appConfig.ContainsKey("showlinefilterformasmodal"))
            {
                bool.TryParse(appConfig["showlinefilterformasmodal"], out showLineFilterFormAsModalDefaultValue);
            }

            if (appConfig.ContainsKey("showseamfilterformasmodal"))
            {
                bool.TryParse(appConfig["showseamfilterformasmodal"], out showSeamFilterFormAsModalDefaultValue);
            }

            #endregion

            #region Edit form display format

            if (appConfig.ContainsKey("showareaeditformasmodal"))
            {
                bool.TryParse(appConfig["showareaeditformasmodal"], out showAreaEditFormAsModalDefaultValue);
            }

            if (appConfig.ContainsKey("showlineeditformasmodal"))
            {
                bool.TryParse(appConfig["showlineeditformasmodal"], out showLineEditFormAsModalDefaultValue);
            }

            if (appConfig.ContainsKey("showseameditformasmodal"))
            {
                bool.TryParse(appConfig["showseameditformasmodal"], out showSeamEditFormAsModalDefaultValue);
            }

            if (appConfig.ContainsKey("showfieldguideeditformasmodal"))
            {
                bool.TryParse(appConfig["showfieldguideeditformasmodal"], out showFieldGuideEditFormAsModalDefaultValue);
            }

            #endregion

            #region Minimums for Over and Unders

            if (appConfig.ContainsKey("minoveragewidthininchesdefaultvalue "))
            {
                int.TryParse(appConfig["minoveragewidthininchesdefaultvalue"], out minOverageWidthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minoveragelengthininchesdefaultvalue "))
            {
                int.TryParse(appConfig["minoveragelengthininchesdefaultvalue"], out minOverageLengthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minunderagewidthininchesdefaultvalue "))
            {
                int.TryParse(appConfig["minunderagewidthininchesdefaultvalue"], out minUnderageWidthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minunderagelengthininchesdefaultvalue "))
            {
                int.TryParse(appConfig["minunderagelengthininchesdefaultvalue"], out minUnderageLengthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minovercombowidthininches"))
            {
                int.TryParse(appConfig["minovercombowidthininches"], out minOverComboWidthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minovercombolengthininches"))
            {
                int.TryParse(appConfig["minovercombolengthininches"], out minOverComboLengthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minundercombowidthininches"))
            {
                int.TryParse(appConfig["minundercombowidthininches"], out minUnderComboWidthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("minundercombolengthininches"))
            {
                int.TryParse(appConfig["minundercombolengthininches"], out minUnderComboLengthInInchesDefaultValue);
            }

            #endregion

            #region Roll Width Default Values

            if (appConfig.ContainsKey("rollwidthdefaultvaluefeet"))
            {
                int.TryParse(appConfig["rollwidthdefaultvaluefeet"], out rollWidthDefaultValueFeetDefaultValue);
            }

            if (appConfig.ContainsKey("rollwidthdefaultvalueinches"))
            {
                int.TryParse(appConfig["rollwidthdefaultvalueinches"], out rollWidthDefaultValueInchesDefaultValue);
            }

            if (appConfig.ContainsKey("rolloverlapdefaultvalueinches"))
            {
                int.TryParse(appConfig["rolloverlapdefaultvalueinches"], out rollOverlapDefaultValueInchesDefaultValue);
            }

            if (appConfig.ContainsKey("rollextrapercutdefaultvalueinches"))
            {
                int.TryParse(appConfig["rollextrapercutdefaultvalueinches"], out rollExtraPerCutDefaultValueInchesDefaultValue);
            }

            #endregion

            if (appConfig.ContainsKey("autoreseamandcutonrollwidthorscalechange"))
            {
                bool.TryParse(appConfig["autoreseamandcutonrollwidthorscalechange"], out autoReseamAndCutOnRollWidthOrScaleChangeDefaultValue);
            }

            if (appConfig.ContainsKey("shortcutorientation"))
            {
                shortcutOrientationDefaultValue = appConfig["shortcutorientation"];
            }

            if (appConfig.ContainsKey("lockscrollwhendrawingsmallerthancanvas"))
            {
                bool.TryParse(appConfig["lockscrollwhendrawingsmallerthancanvas"], out lockScrollWhenDrawingSmallerThanCanvasDefaultValue);
            }

            if (appConfig.ContainsKey("countersmallcircleradius"))
            {
                double.TryParse(appConfig["countersmallcircleradius"], out counterSmallCircleRadiusDefaultValue);
            }

            if (appConfig.ContainsKey("countermediumcircleradius"))
            {
                double.TryParse(appConfig["countermediumcircleradius"], out counterMediumCircleRadiusDefaultValue);
            }

            if (appConfig.ContainsKey("counterlargecircleradius"))
            {
                double.TryParse(appConfig["counterlargecircleradius"], out counterLargeCircleRadiusDefaultValue);
            }

            if (appConfig.ContainsKey("countersmallfontinpts"))
            {
                double.TryParse(appConfig["countersmallfontinpts"], out counterSmallFontInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("countermediumfontinpts"))
            {
                double.TryParse(appConfig["countermediumfontinpts"], out counterMediumFontInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("counterlargefontinpts"))
            {
                double.TryParse(appConfig["counterlargefontinpts"], out counterLargeFontInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("defaultdrawingscaleininches"))
            {
                double.TryParse(appConfig["defaultdrawingscaleininches"], out defaultDrawingScaleInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("defaultnewdrawingwidthininches"))
            {
                double.TryParse(appConfig["defaultnewdrawingwidthininches"], out defaultNewDrawingWidthInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("defaultnewdrawingheightininches"))
            {
                double.TryParse(appConfig["defaultnewdrawingheightininches"], out defaultNewDrawingHeightInInchesDefaultValue);
            }

            if (appConfig.ContainsKey("graphicsprecision"))
            {
                int.TryParse(appConfig["graphicsprecision"], out graphicsPrecisionDefaultValue);
            }

            if (appConfig.ContainsKey("smallfillwastepercent"))
            {
                double.TryParse(appConfig["smallfillwastepercent"], out smallFillWastePercentDefaultValue);
            }

            if (appConfig.ContainsKey("lockareawhen100pctseamed"))
            {
                bool.TryParse(appConfig["lockareawhen100pctseamed"], out defaultLockAreaWhen100PctSeamed);
            }

            #region Initial Design State

            if (appConfig.ContainsKey("initprojectdesignstate"))
            {
                int.TryParse(appConfig["initprojectdesignstate"], out initProjectDesignStateDefaultValue);
            }

            if (appConfig.ContainsKey("startupfullscreenmode"))
            {
                bool.TryParse(appConfig["startupfullscreenmode"], out defaultStartupFullScreenMode);
            }

            if (appConfig.ContainsKey("showsetscalereminder"))
            {
                bool.TryParse(appConfig["showsetscalereminder"], out showSetScaleReminderDefaultValue);
            }

            #endregion

            if (appConfig.ContainsKey("alloweditingofshortcutkeys"))
            {
                bool.TryParse(appConfig["alloweditingofshortcutkeys"], out allowEditingOfShortcutKeysDefaultValue);
            }

            if (appConfig.ContainsKey("alloweditingoftooltips"))
            {
                bool.TryParse(appConfig["alloweditingoftooltips"], out allowEditingOfToolTipsDefaultValue);
            }

            if (appConfig.ContainsKey("zoominoutbuttonpercent"))
            {
                int.TryParse(appConfig["zoominoutbuttonpercent"], out zoomInOutButtonPercentDefaultValue);
            }

            if (appConfig.ContainsKey("mousewheelzoominterval"))
            {
                int.TryParse(appConfig["mousewheelzoominterval"], out mouseWheelZoomIntervalDefaultValue);
            }

            if (appConfig.ContainsKey("arrowmoveincrementdefaultvalue"))
            {
                int.TryParse(appConfig["arrowmoveincrementdefaultvalue"], out arrowMoveIncrementDefaultValue);
            }

            #region Trace Level

            traceLevelDefaultValue = TraceLevel.None;

            if (appConfig.ContainsKey("traceloglevelinfo"))
            {
                bool bTemp = false;

                bool.TryParse(appConfig["traceloglevelinfo"], out bTemp);

                if (bTemp) { traceLevelDefaultValue |= TraceLevel.Info; }
            }

            if (appConfig.ContainsKey("traceloglevelerror"))
            {
                bool bTemp = false;

                bool.TryParse(appConfig["traceloglevelerror"], out bTemp);

                if (bTemp) { traceLevelDefaultValue |= TraceLevel.Error; }
            }

            if (appConfig.ContainsKey("traceloglevelexception"))
            {
                bool bTemp = false;

                bool.TryParse(appConfig["traceloglevelexception"], out bTemp);

                if (bTemp) { traceLevelDefaultValue |= TraceLevel.Exception; }
            }

            if (appConfig.ContainsKey("traceloglevelmethodcall"))
            {
                bool bTemp = false;

                bool.TryParse(appConfig["traceloglevelmethodcall"], out bTemp);

                if (bTemp) { traceLevelDefaultValue |= TraceLevel.MethodCall; }
            }

            #endregion

            #region Validate On Project Save

            if (appConfig.ContainsKey("validateonprojectsave"))
            {
                bool.TryParse(appConfig["validateonprojectsave"], out validateOnProjectSaveDefaultValue);
            }

            #endregion

            #region Autosave

            if (appConfig.ContainsKey("autosaveenabled"))
            {
                bool.TryParse(appConfig["autosaveenabled"], out autosaveEnabledDefaultValue);
            }

            if (appConfig.ContainsKey("autosaveintervalinseconds"))
            {
                int.TryParse(appConfig["autosaveintervalinseconds"], out autosaveIntervalInSecondsDefaultValue);
            }
            #endregion

            #region Debug

            if (appConfig.ContainsKey("validaterolloutandcutwidth"))
            {
                bool.TryParse(appConfig["validaterolloutandcutwidth"], out validateRolloutAndCutWidthDefaultValue);
            }

            if (appConfig.ContainsKey("updatedebugformdynamically"))
            {
                bool.TryParse(appConfig["updatedebugformdynamically"], out updateDebugFormDynamicallyDefaultValue);
            }

            #endregion

            if (appConfig.ContainsKey("areaindexfontinpts"))
            {
                double.TryParse(appConfig["areaindexfontinpts"], out areaIndexFontInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("cutindexfontinpts"))
            {
                double.TryParse(appConfig["cutindexfontinpts"], out cutIndexFontInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("cutindexcircleradius"))
            {
                double.TryParse(appConfig["cutindexcircleradius"], out cutIndexCircleRadiusDefaultValue);
            }

            if (appConfig.ContainsKey("showcutindexcircles"))
            {
                bool.TryParse(appConfig["showcutindexcircles"], out showCutIndexCirclesDefaultValue);
            }

            if (appConfig.ContainsKey("overageindexfontinpts"))
            {
                double.TryParse(appConfig["overageindexfontinpts"], out overageIndexFontInPtsDefaultValue);
            }

            if (appConfig.ContainsKey("underageindexfontinpts"))
            {
                double.TryParse(appConfig["underageindexfontinpts"], out underageIndexFontInPtsDefaultValue);
            }


            if (appConfig.ContainsKey("showareaoutlineinlinemode"))
            {
                bool.TryParse(appConfig["showareaoutlineinlinemode"], out showAreaOutlineInLineModeDefaultValue);
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

            #region Grid

            YGridlineCountSetting = (int)RegistryUtils.InitializeValFromReg<int>("YGridlineCount", yGridlineCountDefaultValue);

            GridlineOffsetSetting = (double)RegistryUtils.InitializeValFromReg<double>("GridlineOffset", gridlineOffsetDefaultValue);

            GridSpacingInInches = (double)RegistryUtils.InitializeValFromReg<double>("GridSpacingInInches", gridSpacingInInchesDefaultValue);
            
            GridOffsetInInches = (double)RegistryUtils.InitializeValFromReg<double>("GridOffsetInInches", gridSpacingInInchesDefaultValue);

            ShowGridlineNumbersSetting = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowGridlineNumbers", showGridlineNumbersDefaultValue);

            #endregion

            ValidateOnProjectSave = (bool)RegistryUtils.InitializeValFromReg<bool>("ValidateOnProjectSave", validateOnProjectSaveDefaultValue);

            SnapToAxis = (bool)RegistryUtils.InitializeValFromReg<bool>("SnapToAxis", snapToAxisDefaultValue);

            #region Field guide format elements

            int A = (int)RegistryUtils.InitializeValFromReg<int>("FieldGuideA", fieldGuideADefaultValue);
            int R = (int)RegistryUtils.InitializeValFromReg<int>("FieldGuideR", fieldGuideRDefaultValue);
            int G = (int)RegistryUtils.InitializeValFromReg<int>("FieldGuideG", fieldGuideGDefaultValue);
            int B = (int)RegistryUtils.InitializeValFromReg<int>("FieldGuideB", fieldGuideBDefaultValue);

            FieldGuideColor = Color.FromArgb(A, R, G, B);
           
            FieldGuideWidthInPts = (double)RegistryUtils.InitializeValFromReg<double>("FieldGuideWidthInPts", fieldGuideWidthInPtsDefaultValue);

            FieldGuideOpacity = (double)RegistryUtils.InitializeValFromReg<double>("FieldGuideOpacity", fieldGuideOpacityDefaultValue);

            FieldGuideStyle = (int)RegistryUtils.InitializeValFromReg<int>("FieldGuideStyle", fieldGuideStyleDefaultValue);

            #endregion

            ShowGrid = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowGrid", showGridDefaultValue);

            ShowRulers = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowRulers", showRulersDefaultValue);

            ShowPanAndZoom = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowPanAndZoom", showPanAndZoomDefaultValue);

            SnapDistance = (double)RegistryUtils.InitializeValFromReg<double>("SnapDistance", snapDistanceDefaultValue);

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

            #region Filter form display format

            ShowAreaFilterFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowAreaFilterFormAsModal", showAreaFilterFormAsModalDefaultValue);
            ShowLineFilterFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowLineFilterFormAsModal", showLineFilterFormAsModalDefaultValue);
            ShowSeamFilterFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowSeamFilterFormAsModal", showSeamFilterFormAsModalDefaultValue);

            #endregion

            #region Edit form display format

            ShowAreaEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowAreaEditFormAsModal", showAreaEditFormAsModalDefaultValue);
            ShowLineEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowLineEditFormAsModal", showLineEditFormAsModalDefaultValue);
            ShowSeamEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowSeamEditFormAsModal", showSeamEditFormAsModalDefaultValue);
            ShowFieldGuideEditFormAsModal = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowFieldGuideEditFormAsModal", showFieldGuideEditFormAsModalDefaultValue);

            #endregion

            MinOverageWidthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinOverageWidthInInches", minOverageWidthInInchesDefaultValue);
            MinOverageLengthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinOverageLengthInInches", minOverageLengthInInchesDefaultValue);
            MinUnderageWidthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinUnderageWidthInInches", minUnderageWidthInInchesDefaultValue);
            MinUnderageLengthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinUnderageLengthInInches", minUnderageLengthInInchesDefaultValue);

            AutoReseamAndCutOnRollWidthOrScaleChange = (bool)RegistryUtils.InitializeValFromReg<bool>("AutoReseamAndCutOnRollWidthOrScaleChange", autoReseamAndCutOnRollWidthOrScaleChangeDefaultValue);

            ShortcutOrientation
                = (string)RegistryUtils.InitializeValFromReg<string>("ShortcutOrientation", shortcutOrientationDefaultValue) == "RightHanded" ?
                  ShortCutOrientation.RightHanded : ShortCutOrientation.LeftHanded;

            LockScrollWhenDrawingSmallerThanCanvas
                = (bool)RegistryUtils.InitializeValFromReg<bool>("LockScrollWhenDrawingSmallerThanCanvas", lockScrollWhenDrawingSmallerThanCanvasDefaultValue);

            CounterSmallCircleRadius = (double)RegistryUtils.InitializeValFromReg<double>("CounterSmallCircleRadius", counterSmallCircleRadiusDefaultValue);
            CounterMediumCircleRadius = (double)RegistryUtils.InitializeValFromReg<double>("CounterMediumCircleRadius", counterMediumCircleRadiusDefaultValue);
            CounterLargeCircleRadius = (double)RegistryUtils.InitializeValFromReg<double>("CounterLargeCircleRadius", counterLargeCircleRadiusDefaultValue);

            CounterSmallFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("CounterSmallFontInPts", counterSmallFontInPtsDefaultValue);
            CounterMediumFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("CounterMediumFontInPts", counterMediumFontInPtsDefaultValue);
            CounterLargeFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("CounterLargeFontInPts", counterLargeFontInPtsDefaultValue);

            ShowCutIndexCircles = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowCutIndexCircles", showCutIndexCirclesDefaultValue);

            // For the following, we initialize the base attribute instead of the property because there are associated events that we dont want to trigger.
            // In fact, this is the way it should have been for all the settings...

            areaIndexFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("AreaIndexFontInPts", areaIndexFontInPtsDefaultValue);
            cutIndexFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("CutIndexFontInPts", cutIndexFontInPtsDefaultValue);
            cutIndexCircleRadius = (double)RegistryUtils.InitializeValFromReg<double>("CutIndexCircleRadius", cutIndexCircleRadiusDefaultValue);
            overageIndexFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("OverageIndexFontInPts", overageIndexFontInPtsDefaultValue);
            underageIndexFontInPts = (double)RegistryUtils.InitializeValFromReg<double>("UnderageIndexFontInPts", underageIndexFontInPtsDefaultValue);

            DefaultDrawingScaleInInches = (double)RegistryUtils.InitializeValFromReg<double>("DefaultDrawingScaleInInches", defaultDrawingScaleInInchesDefaultValue);

            DefaultNewDrawingWidthInInches = (double)RegistryUtils.InitializeValFromReg<double>("DefaultNewDrawingWidthInInches", defaultNewDrawingWidthInInchesDefaultValue);

            DefaultNewDrawingHeightInInches = (double)RegistryUtils.InitializeValFromReg<double>("DefaultNewDrawingHeightInInches", defaultNewDrawingHeightInInchesDefaultValue);

            LockAreaWhen100PctSeamed = (bool)RegistryUtils.InitializeValFromReg<bool>("LockAreaWhen100PctSeamed", defaultLockAreaWhen100PctSeamed);
            
            GraphicsPrecision = (int)RegistryUtils.InitializeValFromReg<int>("GraphicsPrecision", graphicsPrecisionDefaultValue);

            SmallFillWastePercent = (double) RegistryUtils.InitializeValFromReg<double>("SmallFillWastePercent", smallFillWastePercentDefaultValue);

            InitProjectDesignState = (int) RegistryUtils.InitializeValFromReg<int>("InitProjectDesignState", initProjectDesignStateDefaultValue);

            StartupFullScreenMode = (bool)RegistryUtils.InitializeValFromReg<bool>("StartupFullScreenMode", defaultStartupFullScreenMode);

            AllowEditingOfShortcutKeys = (bool)RegistryUtils.InitializeValFromReg<bool>("AllowEditingOfShortcutKeys", allowEditingOfShortcutKeysDefaultValue);

            AllowEditingOfToolTips = (bool)RegistryUtils.InitializeValFromReg<bool>("AllowEditingOfToolTips", allowEditingOfToolTipsDefaultValue);

            MouseWheelZoomInterval = (int)RegistryUtils.InitializeValFromReg<int>("MouseWheelZoomInterval", mouseWheelZoomIntervalDefaultValue);

            ZoomInOutButtonPercent = (int)RegistryUtils.InitializeValFromReg<int>("ZoomInOutButtonPercent", zoomInOutButtonPercentDefaultValue);

            ArrowMoveIncrement = (int)RegistryUtils.InitializeValFromReg<int>("ArrowMoveIncrement", arrowMoveIncrementDefaultValue);

            AutosaveEnabled = (bool)RegistryUtils.InitializeValFromReg<bool>("AutosaveEnabled", autosaveEnabledDefaultValue);

            AutosaveIntervalInSeconds = (int)RegistryUtils.InitializeValFromReg<int>("AutosaveIntervalInSeconds", autosaveIntervalInSecondsDefaultValue);

            ValidateRolloutAndCutWidth = (bool)RegistryUtils.InitializeValFromReg<bool>("ValidateRolloutAndCutWidth", validateRolloutAndCutWidthDefaultValue);

            UpdateDebugFormDynamically = (bool)RegistryUtils.InitializeValFromReg<bool>("UpdateDebugFormDynamically", updateDebugFormDynamicallyDefaultValue);

            MinOverComboWidthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinOverComboWidthInInches", minOverComboWidthInInchesDefaultValue);

            MinOverComboLengthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinOverComboLengthInInches", minOverComboLengthInInchesDefaultValue);

            MinUnderComboWidthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinUnderComboWidthInInches", minUnderComboWidthInInchesDefaultValue);

            MinUnderComboLengthInInches = (int)RegistryUtils.InitializeValFromReg<int>("MinUnderComboLengthInInches", minUnderComboLengthInInchesDefaultValue);

            RollWidthDefaultValueFeet = (int)RegistryUtils.InitializeValFromReg<int>("RollWidthDefaultValueFeet", rollWidthDefaultValueFeetDefaultValue);

            RollWidthDefaultValueInches = (int)RegistryUtils.InitializeValFromReg<int>("RollWidthDefaultValueInches", rollWidthDefaultValueInchesDefaultValue);

            RollOverlapDefaultValueInches = (int)RegistryUtils.InitializeValFromReg<int>("RollOverlapDefaultValueInches", rollOverlapDefaultValueInchesDefaultValue);

            RollExtraPerCutDefaultValueInches = (int)RegistryUtils.InitializeValFromReg<int>("RollExtraPerCutDefaultValueInches", rollExtraPerCutDefaultValueInchesDefaultValue);

            ShowAreaOutlineInLineMode = (bool)RegistryUtils.InitializeValFromReg<bool>("ShowAreaOutlineInLineMode", showAreaOutlineInLineModeDefaultValue);

            TraceLevel = (TraceLevel)(int)RegistryUtils.InitializeValFromReg<int>("TraceLevel", (int) traceLevelDefaultValue);
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

        #region Gridline

        private static double gridSpacingInInches = gridSpacingInInchesDefaultValue;

        public static double GridSpacingInInches
        {
            get
            {
                return gridSpacingInInches;
            }

            set
            {
                if (value == gridSpacingInInches)
                {
                    return;
                }

                gridSpacingInInches = value;

                RegistryUtils.SetRegistryValue("GridSpacingInInches", gridSpacingInInches.ToString());
            }
        }

        private static double gridOffsetInInches = gridSpacingInInchesDefaultValue;

        public static double GridOffsetInInches
        {
            get
            {
                return gridOffsetInInches;
            }

            set
            {
                if (value == gridOffsetInInches)
                {
                    return;
                }

                gridOffsetInInches = value;

                RegistryUtils.SetRegistryValue("GridOffsetInInches", gridOffsetInInches.ToString());

            }
        }

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

        #region Field guide format elements

        private static Color fieldGuideColor;

        public static Color FieldGuideColor
        {
            get
            {
                return fieldGuideColor;
            }

            set
            {
                fieldGuideColor = value;

                RegistryUtils.SetRegistryValue("FieldGuideA", fieldGuideColor.A.ToString());
                RegistryUtils.SetRegistryValue("FieldGuideR", fieldGuideColor.R.ToString());
                RegistryUtils.SetRegistryValue("FieldGuideG", fieldGuideColor.G.ToString());
                RegistryUtils.SetRegistryValue("FieldGuideB", fieldGuideColor.B.ToString());
            }
        }

        private static double fieldGuideOpacity;

        public static double FieldGuideOpacity
        {
            get
            {
                return fieldGuideOpacity;
            }

            set
            {
                fieldGuideOpacity = value;

                RegistryUtils.SetRegistryValue("FieldGuideOpacity", fieldGuideOpacity.ToString());
            }
        }

        private static double fieldGuideWidthInPts;

        public static double FieldGuideWidthInPts
        {
            get
            {
                return fieldGuideWidthInPts;
            }

            set
            {
                fieldGuideWidthInPts = value;

                RegistryUtils.SetRegistryValue("FieldGuideWidthInPts", fieldGuideWidthInPts.ToString());
            }
        }


        private static int fieldGuideStyle;

        public static int FieldGuideStyle
        {
            get
            {
                return fieldGuideStyle;
            }

            set
            {
                fieldGuideStyle = value;

                RegistryUtils.SetRegistryValue("FieldGuideStyle", fieldGuideStyle.ToString());
            }
        }

        #endregion

        #endregion

        #region Snap Resolution

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


        private static double snapDistance = snapDistanceDefaultValue;

        public static double SnapDistance
        {
            get
            {
                return snapDistance;
            }

            set
            {
                snapDistance = value;

                RegistryUtils.SetRegistryValue("SnapDistance", snapDistance.ToString());
            }
        }

        #endregion

        #region Grid, Rulers and Pan and Zoom

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

        private static bool showPanAndZoom = showPanAndZoomDefaultValue;

        public static bool ShowPanAndZoom
        {
            get { return showPanAndZoom; }

            set { showPanAndZoom = value; RegistryUtils.SetRegistryValue("ShowPanAndZoom", showPanAndZoom.ToString()); }
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

        #region Edit form display format

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

        private static bool showFieldGuideEditFormAsModal;

        public static bool ShowFieldGuideEditFormAsModal
        {
            get
            {
                return showFieldGuideEditFormAsModal;
            }

            set
            {
                showFieldGuideEditFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowFieldGuideEditFormAsModal", showFieldGuideEditFormAsModal.ToString());
            }
        }

        #endregion

        #region Filter form display format

        private static bool showAreaFilterFormAsModal;

        public static bool ShowAreaFilterFormAsModal
        {
            get
            {
                return showAreaFilterFormAsModal;
            }

            set
            {
                showAreaFilterFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowAreaFilterFormAsModal", showAreaFilterFormAsModal.ToString());
            }
        }

        private static bool showLineFilterFormAsModal;

        public static bool ShowLineFilterFormAsModal
        {
            get
            {
                return showLineFilterFormAsModal;
            }

            set
            {
                showLineFilterFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowLineFilterFormAsModal", showLineFilterFormAsModal.ToString());
            }
        }

        private static bool showSeamFilterFormAsModal;

        public static bool ShowSeamFilterFormAsModal
        {
            get
            {
                return showSeamFilterFormAsModal;
            }

            set
            {
                showSeamFilterFormAsModal = value;

                RegistryUtils.SetRegistryValue("ShowSeamFilterFormAsModal", showSeamFilterFormAsModal.ToString());
            }
        }

        #endregion

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

        private static bool autoReseamAndCutOnRollWidthOrScaleChange;

        public static bool AutoReseamAndCutOnRollWidthOrScaleChange
        {
            get
            {
                return autoReseamAndCutOnRollWidthOrScaleChange;
            }

            set
            {
                autoReseamAndCutOnRollWidthOrScaleChange = value;

                RegistryUtils.SetRegistryValue("AutoReseamAndCutOnRollWidthOrScaleChange", autoReseamAndCutOnRollWidthOrScaleChange.ToString());
            }
        }

        private static ShortCutOrientation shortcutOrientation;

        public static ShortCutOrientation ShortcutOrientation
        {
            get
            {
                return shortcutOrientation;
            }

            set
            {
                shortcutOrientation = value;

                string orientation = value == ShortCutOrientation.RightHanded ? "RightHanded" : "LeftHanded";

                RegistryUtils.SetRegistryValue("ShortcutOrientation", orientation);
            }
        }

        private static double defaultNewDrawingWidthInInches;
        public static double DefaultNewDrawingWidthInInches
        {
            get
            {
                return defaultNewDrawingWidthInInches;
            }

            set
            {
                defaultNewDrawingWidthInInches = value;

                RegistryUtils.SetRegistryValue("DefaultNewDrawingWidthInInches", defaultNewDrawingWidthInInches.ToString());
            }
        }

        private static bool showSetScaleReminder;

        public static bool ShowSetScaleReminder
        {
            get
            {
                return showSetScaleReminder;
            }

            set
            {
                showSetScaleReminder = value;

                RegistryUtils.SetRegistryValue("ShowSetScaleReminder", showSetScaleReminder.ToString());
            }
        }

        private static double defaultNewDrawingHeightInInches;
        public static double DefaultNewDrawingHeightInInches
        {
            get
            {
                return defaultNewDrawingHeightInInches;
            }

            set
            {
                defaultNewDrawingHeightInInches = value;

                RegistryUtils.SetRegistryValue("DefaultNewDrawingHeightInInches", defaultNewDrawingHeightInInches.ToString());
            }
        }


        #region Counter Settings

        public delegate void CounterSmallCircleRadiusChangedHandler(double radius);

        public static event CounterSmallCircleRadiusChangedHandler CounterSmallCircleRadiusChanged;

        public delegate void CounterMediumCircleRadiusChangedHandler(double radius);

        public static event CounterMediumCircleRadiusChangedHandler CounterMediumCircleRadiusChanged;

        public delegate void CounterLargeCircleRadiusChangedHandler(double radius);

        public static event CounterLargeCircleRadiusChangedHandler CounterLargeCircleRadiusChanged;

        public delegate void CounterSmallFontSizeChangedHandler(double fontSize);

        public static event CounterSmallFontSizeChangedHandler CounterSmallFontSizeChanged;

        public delegate void CounterMediumFontSizeChangedHandler(double fontSize);

        public static event CounterMediumFontSizeChangedHandler CounterMediumFontSizeChanged;

        public delegate void CounterLargeFontSizeChangedHandler(double fontSize);

        public static event CounterLargeFontSizeChangedHandler CounterLargeFontSizeChanged;

        private static double counterSmallCircleRadius;

        public static double CounterSmallCircleRadius
        {
            get
            {
                return counterSmallCircleRadius;
            }

            set
            {
                bool valueChanged = counterSmallCircleRadius != value;

                counterSmallCircleRadius = value;

                RegistryUtils.SetRegistryValue("CounterSmallCircleRadius", counterSmallCircleRadius.ToString());

                if (valueChanged && CounterSmallCircleRadiusChanged != null)
                {
                    CounterSmallCircleRadiusChanged.Invoke(value);
                }
            }
        }


        private static double counterMediumCircleRadius;

        public static double CounterMediumCircleRadius
        {
            get
            {
                return counterMediumCircleRadius;
            }

            set
            {
                bool valueChanged = counterMediumCircleRadius != value;

                counterMediumCircleRadius = value;

                RegistryUtils.SetRegistryValue("CounterMediumCircleRadius", counterMediumCircleRadius.ToString());

                if (valueChanged && CounterMediumCircleRadiusChanged != null)
                {
                    CounterMediumCircleRadiusChanged.Invoke(value);
                }
            }
        }


        private static double counterLargeCircleRadius;

        public static double CounterLargeCircleRadius
        {
            get
            {
                return counterLargeCircleRadius;
            }

            set
            {
                bool valueChanged = counterMediumCircleRadius != value;

                counterLargeCircleRadius = value;

                RegistryUtils.SetRegistryValue("CounterLargeCircleRadius", counterLargeCircleRadius.ToString());

                if (valueChanged && CounterLargeCircleRadiusChanged != null)
                {
                    CounterLargeCircleRadiusChanged.Invoke(value);
                }
            }
        }

        private static double counterSmallFontInPts;

        public static double CounterSmallFontInPts
        {
            get
            {
                return counterSmallFontInPts;
            }

            set
            {
                bool valueChanged = counterSmallFontInPts != value;

                counterSmallFontInPts = value;

                RegistryUtils.SetRegistryValue("CounterSmallFontInPts", counterSmallFontInPts.ToString());

                if (valueChanged && CounterSmallFontSizeChanged != null)
                {
                    CounterSmallFontSizeChanged.Invoke(value);
                }
            }
        }

        private static double counterMediumFontInPts;

        public static double CounterMediumFontInPts
        {
            get
            {
                return counterMediumFontInPts;
            }

            set
            {
                bool valueChanged = counterMediumFontInPts != value;

                counterMediumFontInPts = value;

                RegistryUtils.SetRegistryValue("CounterMediumFontInPts", counterMediumFontInPts.ToString());

                if (valueChanged && CounterMediumFontSizeChanged != null)
                {
                    CounterMediumFontSizeChanged.Invoke(value);
                }
            }
        }

        private static double counterLargeFontInPts;

        public static double CounterLargeFontInPts
        {
            get
            {
                return counterLargeFontInPts;
            }

            set
            {
                bool valueChanged = counterLargeFontInPts != value;

                counterLargeFontInPts = value;

                RegistryUtils.SetRegistryValue("CounterLargeFontInPts", counterLargeFontInPts.ToString());

                if (valueChanged && CounterLargeFontSizeChanged != null)
                {
                    CounterLargeFontSizeChanged.Invoke(value);
                }
            }
        }

        #endregion

        #region Cuts/Area/Over/Under Settings



        private static double areaIndexFontInPts;
        private static double cutIndexFontInPts;
        private static double cutIndexCircleRadius;
        private static bool showCutIndexCircles;
        private static double overageIndexFontInPts;
        private static double underageIndexFontInPts;

        public static Color CutIndexFontColor = Color.Blue;
        public static Color AreaIndexFontColor = Color.Red;
        public static Color OverageIndexFontColor = Color.Red;

        public delegate void AreaIndexFontInPtsChangedHandler(double areaIndexFontInPts);
        public delegate void CutIndexFontInPtsChangedHandler(double cutIndexFontInPts);
        public delegate void CutIndexCircleRadiusChangedHandler(double cutIndexCircleRadius);
        public delegate void ShowCutIndexCirclesChangedHandler(bool showCutIndexCircles);
        public delegate void OverageIndexFontInPtsChangedHandler(double overageIndexFontInPts);
        public delegate void UnderageIndexFontInPtsChangedHandler(double underageIndexFontInPts);

        public static event AreaIndexFontInPtsChangedHandler AreaIndexFontInPtsChanged;
        public static event CutIndexFontInPtsChangedHandler CutIndexFontInPtsChanged;
        public static event CutIndexCircleRadiusChangedHandler CutIndexCircleRadiusChanged;
        public static event ShowCutIndexCirclesChangedHandler ShowCutIndexCirclesChanged;
        public static event OverageIndexFontInPtsChangedHandler OverageIndexFontInPtsChanged;
        public static event UnderageIndexFontInPtsChangedHandler UnderageIndexFontInPtsChanged;

        public static double AreaIndexFontInPts
        {
            get { return areaIndexFontInPts; }
            
            set
            {
                if (value == areaIndexFontInPts)
                {
                    return;
                }
               
                areaIndexFontInPts = value;
                RegistryUtils.SetRegistryValue("AreaIndexFontInPts", value.ToString());

                if (AreaIndexFontInPtsChanged != null)
                {
                    AreaIndexFontInPtsChanged.Invoke(areaIndexFontInPts);
                }
            }
        }

        public static double CutIndexFontInPts
        {
            get { return cutIndexFontInPts; }
            
            set
            {
                if (value == cutIndexFontInPts)
                {
                    return;
                }
                
                cutIndexFontInPts = value;
                RegistryUtils.SetRegistryValue("CutIndexFontInPts", value.ToString());
                
                if (CutIndexFontInPtsChanged != null)
                {
                    CutIndexFontInPtsChanged.Invoke(cutIndexFontInPts);
                }
            }
        }

        public static double CutIndexCircleRadius // Radius in 1/100 inches
        {
            get { return cutIndexCircleRadius; }

            set
            {
                if (value == cutIndexCircleRadius)
                {
                    return;
                }

                cutIndexCircleRadius = value;
                RegistryUtils.SetRegistryValue("CutIndexCircleRadius", value.ToString());

                if (CutIndexCircleRadiusChanged != null)
                {
                    CutIndexCircleRadiusChanged.Invoke(cutIndexCircleRadius);
                }
            }
        }

        public static bool ShowCutIndexCircles
        {
            get { return showCutIndexCircles; }

            set
            {
                if (value == showCutIndexCircles)
                {
                    return;
                }

                showCutIndexCircles = value;
                RegistryUtils.SetRegistryValue("ShowCutIndexCircles", value.ToString());

                if (ShowCutIndexCirclesChanged != null)
                {
                    ShowCutIndexCirclesChanged.Invoke(showCutIndexCircles);
                }
            }
        }

        public static double OverageIndexFontInPts
        {
            get { return overageIndexFontInPts; }
            set
            {
                if (value == overageIndexFontInPts)
                {
                    return;
                }
                
                overageIndexFontInPts = value;

                RegistryUtils.SetRegistryValue("OverageIndexFontInPts", value.ToString());

                if (OverageIndexFontInPtsChanged != null)
                {
                    OverageIndexFontInPtsChanged.Invoke(overageIndexFontInPts);
                }
            }
        }

        public static double UnderageIndexFontInPts
        {
            get { return underageIndexFontInPts; }
            set
            {
                if (value == underageIndexFontInPts)
                {
                    return;
                }
                
                underageIndexFontInPts = value;
                RegistryUtils.SetRegistryValue("UnderageIndexFontInPts", value.ToString());

                if (UnderageIndexFontInPtsChanged != null)
                {
                    UnderageIndexFontInPtsChanged.Invoke(underageIndexFontInPts);
                }
            }
        }
        #endregion

        #region Drawing Scale

        private static double defaultDrawingScaleInInches;

        public static double DefaultDrawingScaleInInches
        {
            get
            {
                return defaultDrawingScaleInInches;
            }

            set
            {
                defaultDrawingScaleInInches = value;

                RegistryUtils.SetRegistryValue("DefaultDrawingScaleInInches", defaultDrawingScaleInInches.ToString());
            }
        }

        private static bool lockScrollWhenDrawingSmallerThanCanvas;

        public static bool LockScrollWhenDrawingSmallerThanCanvas
        {
            get
            {
                return lockScrollWhenDrawingSmallerThanCanvas;
            }

            set
            {
                lockScrollWhenDrawingSmallerThanCanvas = value;

                RegistryUtils.SetRegistryValue("LockScrollWhenDrawingSmallerThanCanvas", lockScrollWhenDrawingSmallerThanCanvas.ToString());
            }
        }

        #endregion

        #region Precisions

        private static int graphicsPrecision;
        
        public static int GraphicsPrecision
        {
            get
            {
                return graphicsPrecision;
            }

            set
            {
                graphicsPrecision = value;

                RegistryUtils.SetRegistryValue("GraphicsPrecision", graphicsPrecision.ToString());
            }
        }

        #endregion

        private static double smallFillWastePercent;

        public static double SmallFillWastePercent
        {
            get
            {
                return smallFillWastePercent;
            }

            set
            {
                smallFillWastePercent = value;

                RegistryUtils.SetRegistryValue("SmallFillWastePercent", SmallFillWastePercent.ToString());
            }
        }

        #region Initial Design State

        private static int initProjectDesignState = 1;

        public static int InitProjectDesignState
        {
            get
            {
                return initProjectDesignState;
            }

            set
            {
                initProjectDesignState = value;

                RegistryUtils.SetRegistryValue("GraphicsPrecision", initProjectDesignState.ToString());
            }
        }

        private static bool startupFullScreenMode = true;

        public static bool StartupFullScreenMode
        {
            get
            {
                return startupFullScreenMode;
            }

            set
            {
                startupFullScreenMode = value;

                RegistryUtils.SetRegistryValue("StartupFullScreenMode", startupFullScreenMode.ToString());
            }
        }

        private static bool lockAreaWhen100PctSeamed = true;

        public static bool LockAreaWhen100PctSeamed
        {
            get
            {
                return lockAreaWhen100PctSeamed;
            }

            set
            {
                lockAreaWhen100PctSeamed = value;

                RegistryUtils.SetRegistryValue("LockAreaWhen100PctSeamed", lockAreaWhen100PctSeamed.ToString());
            }
        }

        private static bool allowEditingOfShortcutKeys = false;

        public static bool AllowEditingOfShortcutKeys
        {
            get
            {
                return allowEditingOfShortcutKeys;
            }

            set
            {
                allowEditingOfShortcutKeys = value;

                RegistryUtils.SetRegistryValue("AllowEditingOfShortcutKeys", allowEditingOfShortcutKeys.ToString());
            }
        }

        private static bool allowEditingOfToolTips = false;

        public static bool AllowEditingOfToolTips
        {
            get
            {
                return allowEditingOfToolTips;
            }

            set
            {
                allowEditingOfToolTips = value;

                RegistryUtils.SetRegistryValue("AllowEditingOfToolTips", allowEditingOfToolTips.ToString());
            }
        }

        #endregion

        private static int mouseWheelZoomInterval = 5;

        public static int MouseWheelZoomInterval
        {
            get
            {
                return mouseWheelZoomInterval;
            }

            set
            {
                mouseWheelZoomInterval = value;

                RegistryUtils.SetRegistryValue("MouseWheelZoomInterval", mouseWheelZoomInterval.ToString());
            }
        }
        private static int zoomInOutButtonPercent = 25;

        public static int ZoomInOutButtonPercent
        {
            get
            {
                return zoomInOutButtonPercent;
            }

            set
            {
                zoomInOutButtonPercent = value;

                RegistryUtils.SetRegistryValue("ZoomInOutButtonPercent", zoomInOutButtonPercent.ToString());
            }
        }

        private static int arrowMoveIncrement = 1;

        public static int ArrowMoveIncrement
        {
            get
            {
                return arrowMoveIncrement;
            }

            set
            {
                arrowMoveIncrement = value;

                RegistryUtils.SetRegistryValue("ArrowMoveIncrement", arrowMoveIncrement.ToString());
            }
        }

        #region Minimum Overs / Unders For Combinations

        private static int minOverComboWidthInInches = 12;
        
        public static int MinOverComboWidthInInches
        {
            get
            {
                return minOverComboWidthInInches;
            }

            set
            {
                minOverComboWidthInInches = value;

                RegistryUtils.SetRegistryValue("MinOverComboWidthInInches", minOverComboWidthInInches.ToString());
            }
        }

        private static int minOverComboLengthInInches = 12;

        public static int MinOverComboLengthInInches
        {
            get
            {
                return minOverComboLengthInInches;
            }

            set
            {
                minOverComboLengthInInches = value;

                RegistryUtils.SetRegistryValue("MinOverComboLengthInInches", minOverComboLengthInInches.ToString());
            }
        }

        private static int minUnderComboWidthInInches = 12;

        public static int MinUnderComboWidthInInches
        {
            get
            {
                return minUnderComboWidthInInches;
            }

            set
            {
                minUnderComboWidthInInches = value;

                RegistryUtils.SetRegistryValue("MinUnderComboWidthInInches", minUnderComboWidthInInches.ToString());
            }
        }

        private static int minUnderComboLengthInInches = 12;

        public static int MinUnderComboLengthInInches
        {
            get
            {
                return minUnderComboLengthInInches;
            }

            set
            {
                minUnderComboLengthInInches = value;

                RegistryUtils.SetRegistryValue("MinUnderComboLengthInInches", minUnderComboLengthInInches.ToString());
            }
        }

        #endregion

        #region Autosave

        private static bool autosaveEnabled = false;
        
        public static bool AutosaveEnabled
        {
            get
            {
                return autosaveEnabled;
            }

            set
            {
                autosaveEnabled = value;

                RegistryUtils.SetRegistryValue("AutosaveEnabled", autosaveEnabled.ToString());
            }
        }

        private static int autosaveIntervalInSeconds = 30;
        public static int AutosaveIntervalInSeconds
        {
            get
            {
                return autosaveIntervalInSeconds;
            }

            set
            {
                autosaveIntervalInSeconds = value;

                RegistryUtils.SetRegistryValue("AutosaveIntervalInSeconds", autosaveIntervalInSeconds.ToString());
            }
        }

        #endregion

        #region Roll Width Defaults

        private static int rollWidthDefaultValueFeet = 12;

        public static int RollWidthDefaultValueFeet
        {
            get
            {
                return rollWidthDefaultValueFeet;
            }

            set
            {
                rollWidthDefaultValueFeet = value;

                RegistryUtils.SetRegistryValue("RollWidthDefaultValueFeet", rollWidthDefaultValueFeet.ToString());
            }
        }

        private static int rollWidthDefaultValueInches = 12;

        public static int RollWidthDefaultValueInches
        {
            get
            {
                return rollWidthDefaultValueInches;
            }

            set
            {
                rollWidthDefaultValueInches = value;

                RegistryUtils.SetRegistryValue("RollWidthDefaultValueInches", rollWidthDefaultValueInches.ToString());
            }
        }

        private static int rollOverlapDefaultValueInches = 12;

        public static int RollOverlapDefaultValueInches
        {
            get
            {
                return rollOverlapDefaultValueInches;
            }

            set
            {
                rollOverlapDefaultValueInches = value;

                RegistryUtils.SetRegistryValue("RollOverlapDefaultValueInches", rollOverlapDefaultValueInches.ToString());
            }
        }

        private static int rollExtraPerCutDefaultValueInches = 12;

        public static int RollExtraPerCutDefaultValueInches
        {
            get
            {
                return rollExtraPerCutDefaultValueInches;
            }

            set
            {
                rollExtraPerCutDefaultValueInches = value;

                RegistryUtils.SetRegistryValue("RollExtraPerCutDefaultValueInches", rollExtraPerCutDefaultValueInches.ToString());
            }
        }

        #endregion

        #region Debug

        private static bool validateRolloutAndCutWidth = false;

        public static bool ValidateRolloutAndCutWidth
        {
            get
            {
                return validateRolloutAndCutWidth;
            }

            set
            {
                validateRolloutAndCutWidth = value;

                RegistryUtils.SetRegistryValue("ValidateRolloutAndCutWidth", validateRolloutAndCutWidth.ToString());
            }
        }

        private static bool updateDebugFormDynamically = false;

        public static bool UpdateDebugFormDynamically
        {
            get
            {
                return updateDebugFormDynamically;
            }

            set
            {
                updateDebugFormDynamically = value;

                RegistryUtils.SetRegistryValue("UpdateDebugFormDynamically", updateDebugFormDynamically.ToString());
            }
        }

        #endregion

        #region Trace Logging

        private static TraceLevel traceLevel = TraceLevel.Info | TraceLevel.Error | TraceLevel.Exception;

        public static TraceLevel TraceLevel
        {
            get
            {
                return traceLevel;
            }

            set
            {
                traceLevel = value;

                RegistryUtils.SetRegistryValue("TraceLevel", ((int)traceLevel).ToString());
            }
        }

        #endregion

        #region Validate On Project Save

        private static bool validateOnProjectSave = false;

        public static bool ValidateOnProjectSave
        {
            get
            {
                return validateOnProjectSave;
            }

            set
            {
                validateOnProjectSave = value;

                RegistryUtils.SetRegistryValue("ValidateOnProjectSave", validateOnProjectSave.ToString());
            }
        }


        #endregion

        #region Area Outline

        private static bool showAreaOutlineInLineMode = false;

        public static bool ShowAreaOutlineInLineMode
        {
            get
            {
                return showAreaOutlineInLineMode;
            }

            set
            {
                showAreaOutlineInLineMode = value;

                RegistryUtils.SetRegistryValue("ShowAreaOutlineInLineMode", showAreaOutlineInLineMode.ToString());
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

    public enum LineDrawoutMode
    {
        Unknown = 0,
        ShowNormalDrawout = 1,
        NoLineDrawout = 2
    }
}
