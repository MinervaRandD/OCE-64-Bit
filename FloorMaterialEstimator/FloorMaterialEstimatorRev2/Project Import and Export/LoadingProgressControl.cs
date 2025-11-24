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
    public partial class LoadingProgressControl : UserControl
    {
        public LoadingProgressControl()
        {
            InitializeComponent();
        }


        double cumulativeStepWeight = 0;

        public void Init()
        {
            cumulativeStepWeight = 0;

            this.Size = new System.Drawing.Size(800, 100);
            this.Location = new System.Drawing.Point(10, 10);
        }
        public void UpdateImportStatus(string stepName)
        {
            this.lblStatus.Text = stepName;
            this.lblStatus.Refresh();
        }

        public void UpdateImportProgress(double stepWeight)
        {
            this.cumulativeStepWeight += stepWeight / 1.8;

            int totalValue = (int)Math.Min(100.0, Math.Round(this.cumulativeStepWeight));

            this.progressBarAdv1.Value = totalValue;
            this.progressBarAdv1.Refresh();
        }

        private void ProjectImporter_ImportStatusEvent(string stepName)
        {
            this.lblStatus.Text = stepName;
        }

        private void ProjectImporter_ImportProgressEvent(string stepName, int stepWeight)
        {
            this.progressBarAdv1.Value = Math.Min(100, this.progressBarAdv1.Value + stepWeight);
        }

        internal void CompleteProgress()
        {
            this.progressBarAdv1.Value = 100;
            this.progressBarAdv1.Refresh();

            System.Threading.Thread.Sleep(100);
        }
    }
}
