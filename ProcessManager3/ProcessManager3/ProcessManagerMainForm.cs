using System.Diagnostics;
using System.Security.Principal;
using System.Management;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;

namespace ProcessManager
{
    public partial class ProcessManagerMainForm : Form
    {
        public ProcessManagerMainForm()
        {
            InitializeComponent();

            getDefaultExecutablePath();

            generateProcessList();

            this.txbPathToExecutable.TextChanged += TxbPathToExecutable_TextChanged;
        }

        private void TxbPathToExecutable_TextChanged(object? sender, EventArgs e)
        {
            RegistryUtils.SetRegistryValue("ProcessManager:DefaultExecutablePath", this.txbPathToExecutable.Text.Trim());

        }

        List<Process> processesToRemove = new List<Process>();

        private void btnScan_Click(object sender, EventArgs e)
        {
            generateProcessList();

            string msg = string.Empty;

            if (processesToRemove.Count == 1)
            {
                msg = "1 case found.";
            }

            else
            {
                msg = processesToRemove.Count + " cases found."; 
            }

            MessageBox.Show(msg);
        }

        private void getDefaultExecutablePath()
        {
            string defaultExecutablePath = RegistryUtils.GetRegistryStringValue("ProcessManager:DefaultExecutablePath", string.Empty);

            this.txbPathToExecutable.Text = defaultExecutablePath;
        }

        private void generateProcessList()
        {

            processesToRemove = ProcessListGenerator.FindForCurrentUserByNamePrefix("floormaterialestimator");


            if (processesToRemove.Count == 1)
            {
                this.lblCasesFound.Text = "1 case found.";
            }

            else
            {
                this.lblCasesFound.Text = processesToRemove.Count.ToString() + " cases found.";
            }
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            
            string defaultPath = string.Empty;
            string defaultFile = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();

            if (!string.IsNullOrWhiteSpace(this.txbPathToExecutable.Text))
            {
                defaultPath = Path.GetFullPath(this.txbPathToExecutable.Text);
                defaultFile = Path.GetFileName(this.txbPathToExecutable.Text);

                ofd.InitialDirectory = defaultPath;
                ofd.FileName = defaultFile;
            }

            ofd.DefaultExt = "exe";

            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.txbPathToExecutable.Text = ofd.FileName;
            }
        }


        private void btnEndAll_Click(object sender, EventArgs e)
        {
            foreach (Process process in processesToRemove)
            {
                process.Kill();
            }

            generateProcessList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = this.txbPathToExecutable.Text.Trim();

            if (!fileName.EndsWith(".exe"))
            {
                MessageBox.Show("The current application specification is not a valid executable.");
                return;
            }

            if (!File.Exists(fileName))
            {
                MessageBox.Show("The application file '" + fileName + "' does not exist.");
                return;
            }

            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = fileName;
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.Verb = "runas";
                proc.Start();
            }

            catch (Exception ex)
            {
                MessageBox.Show("The application failed to launch: " + ex.Message + ".");
                return;
            }

            generateProcessList();
        }
    }
}
