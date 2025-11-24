using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracerLib;

namespace TestDriverVoronoiAlgo
{
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

            TracerLib.Tracer.TraceGen = new Tracer(TraceLevel.Error | TraceLevel.Exception | TraceLevel.Info, @"C:\temp\temp.log", true);


            Application.Run(new VoronoiAlgoTestForm());
        }
    }
}
