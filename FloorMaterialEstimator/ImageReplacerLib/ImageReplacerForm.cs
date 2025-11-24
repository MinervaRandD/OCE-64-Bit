using System;
using System.IO;
using System.IO.Compression;
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

namespace ImageReplacer
{
    public partial class ImageReplacerForm : Form
    {
        public ImageReplacerForm()
        {
            InitializeComponent();
        }

        private void btnBrowseInputVisioProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            string initFolder = RegistryUtils.GetRegistryStringValue("ImageReplacerInptProjectFolder", null);

            if (string.IsNullOrEmpty(initFolder) )
            {
                ofd.InitialDirectory = initFolder;
            }

            DialogResult dr = ofd.ShowDialog(this);

            if (dr == DialogResult.OK)
            {
                if (!File.Exists(ofd.FileName))
                {
                    MessageBox.Show("Input visio project '" + ofd.FileName + "' does not exist");
                    return;
                }

                this.txbInputVisioProject.Text = ofd.FileName;

                RegistryUtils.SetRegistryValue("ImageReplacerInptProjectFolder", Path.GetDirectoryName(ofd.FileName));

            }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string inptVisioProject = this.txbInputVisioProject.Text.Trim();

                if (string.IsNullOrEmpty(inptVisioProject))
                {
                    MessageBox.Show("An input visio project must be provided.");
                    return;
                }

                if (Path.GetExtension(inptVisioProject) != ".vsdx")
                {
                    MessageBox.Show("The input project must be of type visio (extension '.vsdx')");
                    return;
                }

                if (!File.Exists(inptVisioProject))
                {
                    MessageBox.Show("Input visio project '" + inptVisioProject + "' does not exist.");
                    return;
                }

                string outpVisioProject = this.txbOutputVisioProject.Text.Trim();

                if (string.IsNullOrEmpty(outpVisioProject))
                {
                    MessageBox.Show("An output visio project must be provided.");
                    return;
                }

                if (Path.GetExtension(outpVisioProject) != ".vsdx")
                {
                    MessageBox.Show("The output project must be of type visio (extension '.vsdx')");
                    return;
                }

                string replacementImage = this.txbReplacementImage.Text.Trim();

                if (string.IsNullOrEmpty(replacementImage))
                {
                    MessageBox.Show("A replacement image must be provided.");
                    return;
                }

                if (Path.GetExtension(replacementImage) != ".png")
                {
                    MessageBox.Show("The replacement image must be of png (extension '.png')");
                    return;
                }

                if (!File.Exists(replacementImage))
                {
                    MessageBox.Show("Replacement image '" + replacementImage + "' does not exist.");
                    return;
                }

                if (File.Exists(@"C:\OCEOperatingData\Workspace\image1.png"))
                {
                    File.Delete(@"C:\OCEOperatingData\Workspace\image1.png");
                }

                // File.Copy(replacementImage, @"C:\OCEOperatingData\Workspace\image1.png", true);

                string workingVisioProject = @"C:\OCEOperatingData\Workspace\TempVisioProject.zip";

                if (File.Exists(workingVisioProject))
                {
                    File.Delete(workingVisioProject);
                }

                File.Copy(inptVisioProject, workingVisioProject, true);

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

            }
           
        }
    }
}
