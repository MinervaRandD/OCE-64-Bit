using System;
using System.IO;
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
    public partial class AutosaveModelForm : Form
    {
        FloorMaterialEstimatorBaseForm baseForm;

        public AutosaveModelForm()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;

            this.Size = new Size(1, 1);
        }

        public void Init(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;

            this.Activated += AutosaveModelForm_Activated;
        }

        private void AutosaveModelForm_Activated(object sender, EventArgs e)
        {
            StreamWriter exportStream = new StreamWriter(Path.Combine(Program.AutosaveFolder, "Autosave.eproj"));

            ProjectSerializable project = new ProjectSerializable(baseForm);

            project.Serialize(exportStream);

            System.Threading.Thread.Sleep(250);

            this.Close();
        }

    }
}
