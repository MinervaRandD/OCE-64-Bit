using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
            this.ReleaseDateLabel.Text = Program.ReleaseDate;
            
            this.lblMessageLogFilePath.Text = Program.MessageLogFilePath;
           
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
