using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator
{
    public partial class ProgressReporter : UserControl
    {
        ProjectImporter projectImporter { get; set; } = null;

        public ProgressReporter()
        {
            InitializeComponent();
        }

        public void Init(ProjectImporter projectImporter)
        {
            this.projectImporter = projectImporter;

            this.projectImporter.ImportProgressEvent += ProjectImporter_ImportProgressEvent;

            this.prgImportProgress.Value = 0;
        }

        public void Delete()
        {
            this.projectImporter.ImportProgressEvent -= ProjectImporter_ImportProgressEvent;
        }

        private void ProjectImporter_ImportProgressEvent(string stepName, int stepWeight)
        {
            this.prgImportProgress.Value = Math.Min(100, this.prgImportProgress.Value + stepWeight);
        }
    }
}
