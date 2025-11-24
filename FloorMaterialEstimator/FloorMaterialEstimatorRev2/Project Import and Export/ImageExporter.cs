
namespace FloorMaterialEstimator
{
    using System.IO;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Utilities;
    using FinishesLib;
    using Globals;
    using Graphics;
    using System;

    public class ImageExporter
    {
        List<string> extensions = new List<string>()
        {
            ".jpg", ".png", ".svg", ".tif", ".html", ".dwg", ".dxf"
        };

        FloorMaterialEstimatorBaseForm baseForm;

        public ImageExporter(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;
        }

        public string ExportImage()
        {
            string imageSaveDirectory = RegistryUtils.GetRegistryStringValue("ImageSaveDirectory", string.Empty);

            SaveFileDialog sfd = new SaveFileDialog();

            if (!string.IsNullOrEmpty(imageSaveDirectory))
            {
                sfd.InitialDirectory = imageSaveDirectory;
            }

            string initialFileName = SystemState.ProjectName;

            sfd.FileName = initialFileName;

            sfd.CheckPathExists = true;
            sfd.Title = "Save Image";

            // pdf is not working. Maybe fix it some time in the future.

            //sfd.Filter = "Image Files (*.png, *.jpg, *.svg, *.tif)|*.png;*.jpg;*.svg;*.tif|PDF (*.pdf)|*.pdf|HTML (*.html)|*.html|Visio (*.vsdx)|*.vsdx|CAD (*.dwg, *.dxf)|*.dwg;*.dxf";

            sfd.Filter = "Image Files (*.png, *.jpg, *.svg, *.tif)|*.png;*.jpg;*.svg;*.tif|HTML (*.html)|*.html|Visio (*.vsdx)|*.vsdx|CAD (*.dwg, *.dxf)|*.dwg;*.dxf";

            DialogResult dr = sfd.ShowDialog();

            if (dr != DialogResult.OK)
            {
                return string.Empty;
            }

            string fileName = sfd.FileName;

            string result = DoExportImage(fileName);

            return result;
        }

        public string DoExportImage(string fileName)
        {
            string directory = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string extension = Path.GetExtension(fileName).ToLower();


            if (extension == ".pdf")
            {

                //if (SystemState.DesignState == DesignState.Area)
                //{
                //    removeAreaLines();
                //}

                //baseForm.CanvasManager.CurrentPage.VisioPage.Export(fileName);

                //baseForm.CanvasManager.VsoDocument.ExportAsFixedFormat(
                //    Microsoft.Office.Interop.Visio.VisFixedFormatTypes.visFixedFormatPDF,
                //    fileName,
                //    Microsoft.Office.Interop.Visio.VisDocExIntent.visDocExIntentScreen, Microsoft.Office.Interop.Visio.VisPrintOutRange.visPrintAll);
                
                //if (SystemState.DesignState == DesignState.Area)
                //{
                //    restoreAreaLines();
                //}

            }

            else if (extension == ".vsdx")
            {
                baseForm.CanvasManager.VsoDocument.SaveAs(fileName);
            }

            else if (extensions.Contains(extension))
            {

                if (SystemState.DesignState == DesignState.Area)
                {
                    removeAreaLines();
                }
                
                // VisioInterop.SetupExportResolution(baseForm.CanvasManager.VsoAppSettings);

                try
                {
                    baseForm.CanvasManager.CurrentPage.VisioPage.Export(fileName);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Unable to create image of this project. Use a full screen screen shot instead.");

                    return string.Empty;

                }
                if (SystemState.DesignState == DesignState.Area)
                {
                    restoreAreaLines();
                }
            }

            else
            {
                ManagedMessageBox.Show("Unable to save to file format '" + extension + "'.");

                return string.Empty;
            }

            RegistryUtils.SetRegistryValue("ImageSaveDirectory", directory);

            MessageBox.Show("Image '" + Path.GetFileName(fileName) + "' has been saved.");

            return fileName;
        }


        bool[] prevFiltered;

        private void removeAreaLines()
        {
            prevFiltered = new bool[baseForm.LineFinishBaseList.Count];

            for (int i = 0; i < baseForm.LineFinishBaseList.Count; i++)
            {
                LineFinishBase lineFinishBase = baseForm.LineFinishBaseList[i];

                prevFiltered[i] = lineFinishBase.Filtered;

                if (!lineFinishBase.Filtered)
                {
                    lineFinishBase.Filtered = true;
                }
            }
        }

        private void restoreAreaLines()
        {
            for (int i = 0; i < baseForm.LineFinishBaseList.Count; i++)
            {
                LineFinishBase lineFinishBase = baseForm.LineFinishBaseList[i];

                if (!prevFiltered[i])
                {
                    lineFinishBase.Filtered = false;
                }
            }
        }
    }
}
