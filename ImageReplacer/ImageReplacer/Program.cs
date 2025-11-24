namespace ImageReplacer
{
    using System.IO;

    internal static class Program
    {
       

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeApplication();
            ApplicationConfiguration.Initialize();
            Application.Run(new ImageReplacerForm());
        }

        private static void InitializeApplication()
        {
            if (!Directory.Exists(@"C:\OCEOperatingData"))
            {
                Directory.CreateDirectory(@"C:\\OCEOperatingData\");
            }

            if (!Directory.Exists(@"C:\OCEOperatingData\Workspace"))
            {
                Directory.CreateDirectory(@"C:\\OCEOperatingData\Workspace");
            }
        }
    }
}