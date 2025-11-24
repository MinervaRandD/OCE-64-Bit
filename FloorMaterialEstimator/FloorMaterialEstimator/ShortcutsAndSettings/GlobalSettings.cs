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

    using FloorMaterialEstimator.Utilities;

    public static class GlobalSettings
    {
        #region Default Values

        private static OperatingMode operatingModeDefaultValue = OperatingMode.Normal;

        private static LineDrawoutMode lineDrawoutModeDefaultValue = LineDrawoutMode.ShowLineDrawout;

        private static int yGridlineCountDefaultValue = 24;

        private static double gridlineOffsetDefaultValue = 0.1;

        private static bool showGridlineNumbersDefaultValue = true;

        private static bool snapToAxisDefaultValue = true;

        private static double snapResolutionInDegreesDefaultValue = 20.0;

        #endregion

        public static void Initialize()
        {
            #region Operating Mode

            object operatingModeRegValue = Utilities.GetRegistryValue("OperatingMode");

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

            object lineDrawoutModeRegValue = Utilities.GetRegistryValue("LineDrawoutMode");

            if (lineDrawoutModeRegValue == null)
            {
                LineDrawoutModeSetting = lineDrawoutModeDefaultValue;
            }

            else
            {
                switch (lineDrawoutModeRegValue)
                {
                    case "ShowLineDrawout":
                        lineDrawoutModeSetting = LineDrawoutMode.ShowLineDrawout;
                        break;

                    case "HideLineDrawout":
                        lineDrawoutModeSetting = LineDrawoutMode.HideLineDrawout;
                        break;

                    default:
                        LineDrawoutModeSetting = lineDrawoutModeDefaultValue;
                        break;
                }
            }

            #endregion

            YGridlineCountSetting = (int)Utilities.InitializeValFromReg<int>("YGridlineCount", yGridlineCountDefaultValue);

            GridlineOffsetSetting = (double)Utilities.InitializeValFromReg<double>("GridlineOffset", gridlineOffsetDefaultValue);

            ShowGridlineNumbersSetting = (bool) Utilities.InitializeValFromReg<bool>("ShowGridlineNumbers", showGridlineNumbersDefaultValue);

            SnapToAxisSetting = (bool)Utilities.InitializeValFromReg<bool>("SnapToAxis", snapToAxisDefaultValue);

            SnapResolutionInDegrees = (double)Utilities.InitializeValFromReg<double>("SnapResolutionInDegrees", snapResolutionInDegreesDefaultValue);
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

                Utilities.SetRegistryValue("OperatingMode", operatingModeSetting.ToString());
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

                Utilities.SetRegistryValue("LineDrawoutMode", lineDrawoutModeSetting.ToString());
            }
        }

        #endregion

        #region Y GridlineCount

        private static int yGridlineCountSetting = yGridlineCountDefaultValue ;

        public static int YGridlineCountSetting
        {
            get
            {
                return yGridlineCountSetting;
            }

            set
            {
                yGridlineCountSetting = value;

                Utilities.SetRegistryValue("YGridlineCount", yGridlineCountSetting.ToString());
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

                Utilities.SetRegistryValue("GridlineOffset", gridlineOffsetSetting.ToString());
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

                Utilities.SetRegistryValue("ShowGridlineNumbers", showGridlineNumbersSetting.ToString());
            }
        }

        #endregion

        #region Snap To Axis

        private static bool snapToAxisSetting = snapToAxisDefaultValue;

        public static bool SnapToAxisSetting
        {
            get
            {
                return snapToAxisSetting;
            }

            set
            {
                snapToAxisSetting = value;

                Utilities.SetRegistryValue("SnapToAxis", snapToAxisSetting.ToString());
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

                Utilities.SetRegistryValue("SnapResolutionInDegrees", snapResolutionInDegrees.ToString());
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
        ShowLineDrawout = 1,
        HideLineDrawout = 2
    }
}
