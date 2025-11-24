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
    public partial class AboutForm : Form
    {
         public AboutForm()
        {
            InitializeComponent();

            this.VersionLabel.Text = Program.Version;
            this.ReleaseDateLabel.Text = Program.CompileDate;
            this.lblMessageLogFilePath.Text = Program.MessageLogFilePath;
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
