using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Utilities;
using Globals;
using Visio = Microsoft.Office.Interop.Visio;
using System.Runtime.InteropServices;

namespace ImageReplacer
{
    public partial class ImageReplacerForm : Form
    {
        Visio.Document vsoDocument = null;

        public ImageReplacerForm()
        {
            InitializeComponent();
        }

        public void Init(
            Visio.Document vsoDocument, string outpVisioProjectPath, string replacementImagePath)
        {
            this.vsoDocument = vsoDocument;

            this.txbOutputVisioProject.Text = outpVisioProjectPath;
            this.txbReplacementImage.Text = replacementImagePath;
        }

        private void btnBrowseOutputVisioProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.CheckFileExists = false;

            DialogResult dr = ofd.ShowDialog(this);

            string initFolder = RegistryUtils.GetRegistryStringValue("ImageReplacerOutpProjectFolder", null);

            if (string.IsNullOrEmpty(initFolder))
            {
                ofd.InitialDirectory = initFolder;
            }

            if (dr == DialogResult.OK)
            {
                this.txbOutputVisioProject.Text = ofd.FileName;

                RegistryUtils.SetRegistryValue("ImageReplacerOutpProjectFolder", Path.GetDirectoryName(ofd.FileName));
            }
        }

        private void btnBrowseReplacementImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            DialogResult dr = ofd.ShowDialog(this);

            string initFolder = RegistryUtils.GetRegistryStringValue("ImageReplacerReplacementImageFolder", null);

            if (string.IsNullOrEmpty(initFolder))
            {
                ofd.InitialDirectory = initFolder;
            }

            if (dr == DialogResult.OK)
            {

                if (dr == DialogResult.OK)
                {
                    if (!File.Exists(ofd.FileName))
                    {
                        MessageBox.Show("Replacement image'" + ofd.FileName + "' does not exist");
                        return;
                    }

                    this.txbReplacementImage.Text = ofd.FileName;

                    RegistryUtils.SetRegistryValue("ImageReplacerReplacementImageFolder", Path.GetDirectoryName(ofd.FileName));
                }

                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;

            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            doSaveWithImageReplacement();

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnSaveAndLaunchVisio_Click(object sender, EventArgs e)
        {
            if (!checkFileInUse())
            {
                return;
            }

            string projectFilePath = doSaveWithImageReplacement();

            if (projectFilePath == null)
            {
                return;
            }

            OpenOrLaunchVisio(projectFilePath);

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private bool checkFileInUse()
        {
            string outpVisioProject = this.txbOutputVisioProject.Text.Trim();

            if (File.Exists(outpVisioProject))
            {
                try
                {
                    File.Delete(outpVisioProject);
                }
                catch (Exception ex)
                {
                    MessageBoxAdv.Show(
                        "The output project '" + outpVisioProject +
                        "' is currently in use, possibly by Visio. Please close off this application to proceed", "File In Use", MessageBoxAdv.Buttons.OK);

                    return false;
                }

            }

            return true;
        }

        private string doSaveWithImageReplacement()
        {
            string outpVisioProject = this.txbOutputVisioProject.Text.Trim();

            try
            {
                

                if (string.IsNullOrEmpty(outpVisioProject))
                {
                    MessageBox.Show("An output visio project must be provided.");
                    return null;
                }

                if (Path.GetExtension(outpVisioProject) != ".vsdx")
                {
                    MessageBox.Show("The output project must be of type visio (extension '.vsdx')");
                    return null;
                }

                string replacementImage = this.txbReplacementImage.Text.Trim();

                if (string.IsNullOrEmpty(replacementImage))
                {
                    MessageBox.Show("A replacement image must be provided.");
                    return null;
                }

                if (Path.GetExtension(replacementImage) != ".png")
                {
                    MessageBox.Show("The replacement image must be of png (extension '.png')");
                    return null;
                }

                if (!File.Exists(replacementImage))
                {
                    MessageBox.Show("Replacement image '" + replacementImage + "' does not exist.");
                    return null;
                }

                if (File.Exists(@"C:\OCEOperatingData\Workspace\image1.png"))
                {
                    File.Delete(@"C:\OCEOperatingData\Workspace\image1.png");
                }

                string inptVisioProject = @"C:\OCEOperatingData\Workspace\TempVisioProject.vsdx";

                string workingVisioProject = @"C:\OCEOperatingData\Workspace\TempVisioProject.zip";

                if (File.Exists(workingVisioProject))
                {
                    File.Delete(workingVisioProject);
                }

                vsoDocument.SaveAs(inptVisioProject);

                File.Move(inptVisioProject, workingVisioProject);

                var zipArchive = ZipFile.Open(workingVisioProject, ZipArchiveMode.Read);

                string tempArchiveDirectory = @"C:\OCEOperatingData\Workspace\TempArchive";

                if (Directory.Exists(tempArchiveDirectory))
                {
                    Directory.Delete(tempArchiveDirectory, true);
                }

                zipArchive.ExtractToDirectory(tempArchiveDirectory);

                zipArchive.Dispose();

                string imageToReplace = @"C:\OCEOperatingData\Workspace\TempArchive\visio\media\image1.png";

                if (File.Exists(imageToReplace))
                {
                    File.Delete(imageToReplace);
                }

                File.Copy(replacementImage, imageToReplace);

                string tempZipArchive = @"C:\OCEOperatingData\Workspace\TempArchive.zip";


                if (File.Exists(tempZipArchive))
                {
                    File.Delete(tempZipArchive);
                }


                ZipFile.CreateFromDirectory(tempArchiveDirectory, tempZipArchive);

                if (File.Exists(outpVisioProject))
                {
                    File.Delete(outpVisioProject);
                }

                File.Copy(tempZipArchive, outpVisioProject, true);

                if (File.Exists(workingVisioProject))
                {
                    File.Delete(workingVisioProject);
                }

                if (Directory.Exists(tempArchiveDirectory))
                {
                    Directory.Delete(tempArchiveDirectory, true);
                }

                if (File.Exists(tempZipArchive))
                {
                    File.Delete(tempZipArchive);
                }

                MessageBox.Show("Image has been replaced into output project '" + outpVisioProject + "'.");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Image replacer failed: '" + ex.Message + "'.");
                return null;

            }

            return outpVisioProject;
        }

        [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hWnd);

        private void OpenOrLaunchVisio(string visioFilePath)
        {

             dynamic visioApp = null;
             string visioProjectPath = null;

            if (string.IsNullOrWhiteSpace(visioFilePath) || !File.Exists(visioFilePath))
            {
                MessageBoxAdv.Show("Visio file not found: " + visioFilePath);
                return;
            }

            try
            {
                
                Type visioType = Type.GetTypeFromProgID("Visio.Application");
                visioApp = Activator.CreateInstance(visioType);
                visioApp.Visible = true;
                // Step 3: Open the file
                visioApp.Documents.Open(visioFilePath);
                //Console.WriteLine("Opened file: " + visioFilePath);
                IntPtr hwnd = visioApp.WindowHandle64;
                SetForegroundWindow((IntPtr)hwnd);
                
                Utilities.WindowUtils.ResizeAndMoveVisioWindow((IntPtr)hwnd, 100, 50, 4096, 2048);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error launching or attaching to Visio: " + ex.Message);
            }
        }

    }
}
