using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace FloorMaterialEstimator
{
    public partial class ExceptionForm : Form
    {
        private string exceptionMessage = string.Empty;

        private string callStack = string.Empty;

        private FloorMaterialEstimatorBaseForm baseForm;
        public ExceptionForm(Exception ex, FloorMaterialEstimatorBaseForm baseForm)
        {
            InitializeComponent();

            this.baseForm = baseForm;

            exceptionMessage = ex.Message;

            Exception ex1 = ex;

            while (Utilities.Utilities.IsNotNull(ex1.InnerException))
            {
                exceptionMessage += '\n' + ex1.InnerException.Message;

                ex1 = ex1.InnerException;
            }

            callStack = "Call Stack:\n\n" + ex.StackTrace;

            this.txbException.Text = exceptionMessage;
            this.txbCallStack.Text = callStack;

            this.txbException.TabStop = false;
            this.txbCallStack.TabStop = false;
        }

        private void btnCopyInformationToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(exceptionMessage + "\n\n" + callStack);
        }

        private void btnSaveInformationToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.DefaultExt = "txt";
            sfd.Filter = "Text files (*.txt)|*.txt";

            DialogResult dr = sfd.ShowDialog();

            if (dr != DialogResult.OK)
            {
                return;
            }

            try
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);

                sw.WriteLine(exceptionMessage + "\n\n" + callStack);

                sw.Flush();

                sw.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show("Attempt to write contents to file '" + sfd.FileName + "' failed: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            try
            {
                baseForm.DoSaveProjectAs(false);
            }

            catch (Exception ex)
            {

            }
        }
    }
}
