//-------------------------------------------------------------------------------//
// <copyright file="Program.cs" company="Bruun Estimating, LLC">                 // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.Test_and_Debug;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Configuration;

    using FloorMaterialEstimator.Test_and_Debug;

    static class Program
    {
        public static Dictionary<string, string> AppConfig = new Dictionary<string, string>();

        public static DebugCond Debug = DebugCond.None; // DebugCond.VisioMouseEvents | DebugCond.VisioPolyLineDraw;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (string configKey in ConfigurationManager.AppSettings.Keys)
            {
                string configVal = ConfigurationManager.AppSettings[configKey];

                AppConfig.Add(configKey.ToLower(), configVal);
            }

            TestOverages.Test2();

            Application.Run(new FloorMaterialEstimatorBaseForm());
        }
    }
}
