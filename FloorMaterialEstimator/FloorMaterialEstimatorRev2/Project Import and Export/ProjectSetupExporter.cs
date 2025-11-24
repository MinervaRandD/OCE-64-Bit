

namespace FloorMaterialEstimator
{
    using System.IO;
    using System.Windows.Forms;
    using System.Diagnostics;
    using Utilities;

    public class ProjectSetupExporter
    {
        private FloorMaterialEstimatorBaseForm baseForm;

        public ProjectSetupExporter(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;
        }

        public bool ExportSetup(string setupPathName, string setupFileName)
        {
            Debug.Assert(!string.IsNullOrEmpty(setupPathName) && !string.IsNullOrEmpty(setupFileName));

            string setupFullPath = string.Empty;

            if (!Directory.Exists(setupPathName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(setupPathName));
            }

            StreamWriter exportStream = new StreamWriter(Path.Combine(setupPathName, setupFileName));

            ProjectSetupSerializable setup = new ProjectSetupSerializable(this.baseForm);

            if (!ProjectSetupSerializable.ProjectSerializationSucceeded)
            {
                MessageBox.Show("Unable to save setup. Project database is inconsistent.");

                return false;
            }

            setup.Serialize(exportStream);

            setupFullPath = Path.Combine(setupPathName, setupFileName);

            string CurrentProjectName = Path.GetFileNameWithoutExtension(setupFullPath);

            ManagedMessageBox.Show(setupFullPath + " has been saved.");

            return true;
        }
    }
}
