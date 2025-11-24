

namespace TestDriverRolloutOversAndUnders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using SettingsLib;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GlobalSettings.GraphicsPrecision = 6;

            GlobalSettings.MinOverageWidthInInches = 1;
            GlobalSettings.MinOverageLengthInInches = 1;

            GlobalSettings.MinUnderageWidthInInches = 1;
            GlobalSettings.MinUnderageLengthInInches = 1;

            Application.Run(new TestSeamsAndCutsForm());
        }
    }
}
