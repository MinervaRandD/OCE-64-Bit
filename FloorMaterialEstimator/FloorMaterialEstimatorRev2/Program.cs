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
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Configuration;

    using Utilities;
    using TracerLib;

    using System.Threading;
    using System.Runtime.ExceptionServices;
    using SettingsLib;

    static class Program
    {
        public static string Version { get; set; }
        
        public static string ReleaseDate { get; set; }
        
        public static string CompileDate { get; set; } 
        public static string MessageLogFilePath { get; set; } = string.Empty;

        public static string TraceLogFilePath { get; set; } = string.Empty;

        public static string AutosaveFolder { get; set; }
        
        public static string DefaultsFolder { get; set; }

        public static string DrawingsFolder { get; set; }

        public static Task AutosaveTask { get; set; }
        
        public static string OCEOperatingDataFolder { get; set; }

        public static string WorkSpaceFolder { get; set; }

        public static TraceLevel TraceLevel { get; set; }

        public static string LogFileFolder { get; set; }

        public static Dictionary<string, string> AppConfig = new Dictionary<string, string>();

        public static DebugCond Debug = DebugCond.None; // DebugCond.VisioMouseEvents | DebugCond.VisioPolyLineDraw;

        public static int logLevel = 0;

        public static UserType UserType = UserType.Administrator;
        public static FloorMaterialEstimatorBaseForm BaseForm { get; set; }

        public static System.Timers.Timer autosaveTimer = null;

        public static string initialProject = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [HandleProcessCorruptedStateExceptions]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    initialProject = args[0];
                }

                System.Diagnostics.Process myProcess = System.Diagnostics.Process.GetCurrentProcess();
                myProcess.PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;

                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
                Version = fvi.FileVersion;

                CompileDate = DateTime.Now.ToString("yyyy-MM-dd");
                try
                {

                    runMain();
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    Console.WriteLine($"COM Exception: {ex.Message}");
                    Console.WriteLine($"HRESULT: {ex.HResult}");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Startup exception thrown: " + ex.Message);
            }

            CursorManager.SystemParametersInfo(0x0057, 0, null, 0);
        }

        private static void runMain()
        {
           // int test = ShapeNestLib.Class1.shoutOut();

            const string appName = "FloorMaterialEstimator";

            bool createdNew;

            Mutex mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                ManagedMessageBox.Show("A copy of this application is already running on this machine");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            foreach (string configKey in ConfigurationManager.AppSettings.Keys)
            {
                string configVal = ConfigurationManager.AppSettings[configKey];

                string configKey1 = configKey.ToLower();

                AppConfig.Add(configKey1, configVal);
            }

            if (!AppConfig.ContainsKey("oceoperatingdatafolder"))
            {
                OCEOperatingDataFolder = @"C:\OCEOperatingData";
            }

            else
            {
                OCEOperatingDataFolder = AppConfig["oceoperatingdatafolder"];
            }

            if (!Directory.Exists(OCEOperatingDataFolder))
            {
                Directory.CreateDirectory(OCEOperatingDataFolder);
            }

            WorkSpaceFolder = Path.Combine(OCEOperatingDataFolder, "Workspace");

            if (!Directory.Exists(WorkSpaceFolder))
            {
                Directory.CreateDirectory(WorkSpaceFolder);
            }

            LogFileFolder = Path.Combine(OCEOperatingDataFolder, "Logs");

            if (!Directory.Exists(LogFileFolder))
            {
                Directory.CreateDirectory(LogFileFolder);
            }

            // Get a log file that can be used.

            string MessageLogFilePathBase = Path.Combine(LogFileFolder, Environment.MachineName + "." + Environment.UserName + ".MessageLog");

            MessageLogFilePath = null;

            for (int i = 1; i <= 999; i++)
            {
                MessageLogFilePath = MessageLogFilePathBase + i.ToString("000") + ".txt";

                if (!FileUtils.IsFileLocked(MessageLogFilePath))
                {
                    break;
                }
            }

            if (MessageLogFilePath is null)
            {
                MessageBox.Show("Unable to create message log file.", "Startup Failed", MessageBoxButtons.OK);
                System.Environment.Exit(-1);
            }

            // Get a trace file that can be used.

            TraceLogFilePath = null;

            string TraceLogFilePathBase = Path.Combine(LogFileFolder, Environment.MachineName + "." + Environment.UserName + ".TraceLog");

            for (int i = 1; i <= 999; i++)
            {
                TraceLogFilePath = TraceLogFilePathBase + i.ToString("000") + ".txt";

                if (!FileUtils.IsFileLocked(TraceLogFilePath))
                {
                    break;
                }
            }

            if (TraceLogFilePath is null)
            {
                MessageBox.Show("Unable to create trace log file.", "Startup Failed", MessageBoxButtons.OK);
                System.Environment.Exit(-1);
            }

            AutosaveFolder = Path.Combine(OCEOperatingDataFolder, "Autosave");

            if (!Directory.Exists(AutosaveFolder))
            {
                Directory.CreateDirectory(AutosaveFolder);
            }

            DefaultsFolder = Path.Combine(OCEOperatingDataFolder, "Defaults");

            if (!Directory.Exists(DefaultsFolder))
            {
                Directory.CreateDirectory(DefaultsFolder);
            }

            DrawingsFolder = Path.Combine(OCEOperatingDataFolder, "Drawings");
             
            if (!Directory.Exists(DrawingsFolder))
            {
                Directory.CreateDirectory(DrawingsFolder);
            }

            Logger.Intialize(MessageLogFilePath);

            try
            {
               // Logger.LogMessage("Application starts.", MessageSeparator.SingleLine);

                BaseForm = new FloorMaterialEstimatorBaseForm();
               
                Application.Run(BaseForm);
                

                // Make sure autosave is not in progress.

                if (Utilities.IsNotNull(AutosaveTask))
                {
                    while (AutosaveTask.Status == TaskStatus.Running)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }

                Logger.LogMessage("Application terminates.", MessageSeparator.DoubleLine);
            }

            catch (Exception ex)
            {
                string exceptionMessage = ex.Message;

                Exception ex1 = ex;

                while (Utilities.IsNotNull(ex1.InnerException))
                {
                    exceptionMessage += '\n' + ex1.InnerException.Message;

                    ex1 = ex1.InnerException;
                }

                Tracer.TraceGen.TraceException("Program:runMain throws an exception.", ex, 1, true);
                
                ExceptionForm exceptionForm = new ExceptionForm(ex, BaseForm);

                exceptionForm.ShowDialog();
            }
        }
    }
}
