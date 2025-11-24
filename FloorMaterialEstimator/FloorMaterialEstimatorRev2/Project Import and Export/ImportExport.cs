

namespace FloorMaterialEstimator
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.Dialog_Boxes;

    using Globals;
    using Utilities;
    using Graphics;
    using TracerLib;
    using SettingsLib;
    using CanvasLib.Scale_Line;
    using FloorMaterialEstimator.CanvasManager;
    using System.Collections.Generic;
    using ImageReplacer;
    using System.Linq;

    public partial class FloorMaterialEstimatorBaseForm
    {

        private void btnExistingProject_Click(object sender, EventArgs e)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { sender, e });
#endif
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                doExistingProject();
            }

            withinProjectAccess = false;

            //Tracer.TraceGen.TraceInfo("Exiting btnExistingProject_Click", 1, false);
        }

        private void doExistingProject()
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { });
#endif

            DialogResult dr;

            if (SystemState.ScaleHasBeenSet && currentPage.UnseamedAreasExist())
            {
                dr = MessageBoxAdv.Show(
                    "Some areas have not been seamed, do you want to continue to open an existing project?"
                    , "Open New Project"
                    , MessageBoxAdv.Buttons.YesNo
                    , MessageBoxAdv.Icon.Question);

                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }



            if (CanvasManager.DrawingInProgress())
            {
                OpenProjectWarning openProjectWarningForm = new OpenProjectWarning();

                openProjectWarningForm.StartPosition = FormStartPosition.CenterParent;

                dr = openProjectWarningForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    return;
                }
            }
            
            bool cancel = false;

            cancel = CheckCloseEditForms();

            if (cancel)
            {
                return;
            }

            cancel = SaveExistingProject(false);

            if (cancel)
            {
                return;
            }

            string projectPathName = CurrentProjectPath;

            string projectFullFileName = string.Empty;

            if (string.IsNullOrEmpty(projectPathName))
            {
                projectFullFileName = RegistryUtils.GetRegistryStringValue("ProjectFileName", string.Empty);

                Debug.Assert(projectFullFileName.EndsWith(".eproj") || string.IsNullOrEmpty(projectFullFileName));

                if (string.IsNullOrEmpty(projectFullFileName))
                {
                    projectPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                }

                else
                {
                    projectPathName = Path.GetDirectoryName(projectFullFileName);
                }
            }

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = projectPathName;
            ofd.Filter = "Estimator projects|*.eproj";

            dr = ofd.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            projectFullFileName = ofd.FileName;

            if (string.IsNullOrEmpty(projectFullFileName))
            {
                return;
            }

            CleanupOldProject();

            ProjectImporter projectImporter = new ProjectImporter(this);
            string updatedProjectFileName = projectImporter.ImportProject(projectFullFileName);

            if (!string.IsNullOrEmpty(updatedProjectFileName))
            {

                Debug.Assert(updatedProjectFileName.EndsWith(".eproj"));

                RegistryUtils.SetRegistryValue("ProjectFileName", projectFullFileName);

                CurrentProjectName = Path.GetFileNameWithoutExtension(updatedProjectFileName);
                CurrentProjectPath = Path.GetDirectoryName(updatedProjectFileName);
                CurrentProjectFullPath = updatedProjectFileName;
            }

            else
            {
                return;
            }

            if (SystemState.BtnTapeMeasureChecked)
            {
                CanvasManager.TapeMeasureController.CancelTapeMeasure();
            }

            if (SystemState.BtnTapeMeasureChecked)
            {
                CanvasManager.TapeMeasureController.CancelTapeMeasure();
            }

            BtnDoorTakeoutShow.Text = "Hide";

            setProjNameLabel(CurrentProjectName);

            setProjectPath(Utilities.FilePathSummary(projectFullFileName, 4, 1));
            setOriginalImage(SystemGlobals.OriginalImagePath);

            SystemState.ProjectName = CurrentProjectName;

            PrepareNewProject();

            //this.AreaFinishBaseList.SelectElem(0);

            //this.lblProjectName.Focus();

            // Start out in line mode if lines but no areas.

            if (isLineModeProject())
            {
                this.BtnLineDesignState_Click(null, null);
            }

            foreach (AreaFinishManager areaFinishManager in AreaFinishManagerList)
            {
                areaFinishManager.SetDesignStateFormat(DesignState, SeamMode, areaFinishManager.Filtered, true);
            }

            //foreach (UCAreaFinishPaletteElement ucAreaPaletteElement in areaPalette)
            //{
            //    ucAreaPaletteElement.AreaFinishManager.SetDesignStateFormat(DesignState, SeamMode, ucAreaPaletteElement.Filtered, true);
            //}

            CanvasManager.PanAndZoomController.SetPageSize(currentPage.PageWidth, currentPage.PageHeight);

            VisioInterop.SetGridSpacing(currentPage, 0.25, 0.25);
            VisioInterop.SetGridOrigin(currentPage, 0, 0);

            CanvasManager.PanAndZoomController.ZoomToFit();

            SystemState.InitializeSeamStateForExistingProject = true;

            // Tracer.TraceGen.TraceInfo("Exit doExistingProject.", 1, false);
            // CanvasManager.PanAndZoomController.SetZoom(.996, true);
            // CanvasManager.PanAndZoomController.CenterDrawing();
        }

        private bool isLineModeProject()
        {
            foreach (AreaFinishManager areaFinishManager in AreaFinishManagerList)
            {
                if (areaFinishManager.CanvasLayoutAreaDict.Count > 0)
                {
                    return false;
                }
            }

            foreach (LineFinishManager lineFinishManager in LineFinishManagerList)
            {
                if (lineFinishManager.LineDictCount> 0)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnCreateImage_Click(object sender, EventArgs e)
        {
            ImageExporter imageExporter = new ImageExporter(this);

            string result = imageExporter.ExportImage();

            //if (!string.IsNullOrEmpty(result))
            //{
            //    MessageBox.Show("Saved\n\nImage has been saved to '" + result + "'");
            //}
          
        }

        //private bool withinSaveProject = false;

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                doSaveProject(true);
            }

            withinProjectAccess = false;
        }

        private bool doSaveProject(bool warn = true)
        {
            if (string.IsNullOrEmpty(CurrentProjectFullPath))
            {
                return DoSaveProjectAs(warn);
            }

            if (warn)
            {
                if (SystemState.ScaleHasBeenSet && currentPage.UnseamedAreasExist())
                {
                    DialogResult dr = MessageBoxAdv.Show(
                        "Some areas have not been seamed, do you want to continue to save this project?"
                        , "Open New Project"
                        , MessageBoxAdv.Buttons.YesNo
                        , MessageBoxAdv.Icon.Question);

                    if (dr != DialogResult.Yes)
                    {
                        return false;
                    }
                }


                if (CanvasManager.DrawingInProgress())
                {
                    SaveProjectWarning saveProjectWarningForm = new SaveProjectWarning();

                    saveProjectWarningForm.StartPosition = FormStartPosition.CenterParent;

                    DialogResult dr = saveProjectWarningForm.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        return true;
                    }
                }
            }


            string projectPathName = Path.GetDirectoryName(CurrentProjectFullPath);
            string projectFileName = Path.GetFileName(CurrentProjectFullPath);

            ProjectExporter projectExporter = new ProjectExporter(this);

            
            bool success = projectExporter.ExportProject(projectPathName, projectFileName);

            if (!success)
            {
                return true;
            }

            CurrentProjectChanged = false;

            RegistryUtils.SetRegistryValue("ProjectFileName", CurrentProjectFullPath);

            return false;
        }

        private void btnSaveProjectAs_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                DoSaveProjectAs(true);
            }

            withinProjectAccess = false;
        }

        public bool DoSaveProjectAs(bool warn = true)
        {
            DialogResult dr;

            if (warn)
            {

                if (SystemState.ScaleHasBeenSet && currentPage.UnseamedAreasExist())
                {
                    dr = MessageBox.Show(
                        "Some areas have not been seamed, do you want to continue to save this project?",
                        "Open New Project", MessageBoxButtons.YesNo);

                    if (dr != DialogResult.Yes)
                    {
                        return false;
                    }
                }


                if (CanvasManager.DrawingInProgress())
                {
                    SaveProjectWarning saveProjectWarningForm = new SaveProjectWarning();

                    saveProjectWarningForm.StartPosition = FormStartPosition.CenterParent;

                    dr = saveProjectWarningForm.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        return true;
                    }
                }

            }

            string projectPathName;

            if (string.IsNullOrEmpty(CurrentProjectPath))
            {
                string projectFullFileName = RegistryUtils.GetRegistryStringValue("ProjectFileName", string.Empty);

                if (string.IsNullOrEmpty(projectFullFileName))
                {
                    projectPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                }

                else
                {
                    projectPathName = Path.GetDirectoryName(projectFullFileName);

                    if (string.IsNullOrEmpty(projectPathName))
                    {
                        projectPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                    }
                }
            }

            else
            {
                projectPathName = CurrentProjectPath;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = projectPathName;
            saveFileDialog.FileName = CurrentProjectName;
            saveFileDialog.Filter = "Estimator project|*.eproj|Visio File (*.vsdx)|*.vsdx|Image Files (*.png, *.jpg, *.svg, *.tif)|*.png;*.jpg;*.svg;*.tif|HTML (*.html)|*.html|CAD (*.dwg, *.dxf)|*.dwg;*.dxf";

            dr = saveFileDialog.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return true;
            }

            string outpFilePath = Path.GetDirectoryName(saveFileDialog.FileName);
            string outpFileName = Path.GetFileName(saveFileDialog.FileName);

            string fileType = Path.GetExtension(outpFileName);
           
            if (string.IsNullOrEmpty(fileType))
            {
                fileType = ".eproj";
                outpFileName += ".eproj";
            }

            if (fileType == ".eproj")
            {
                ProjectExporter projectExporter = new ProjectExporter(this);
                bool success = projectExporter.ExportProject(outpFilePath, outpFileName);

                if (!success)
                {
                    return true;
                }

                CurrentProjectName = Path.GetFileNameWithoutExtension(outpFileName);
                CurrentProjectPath = outpFilePath;
                CurrentProjectFullPath = saveFileDialog.FileName;

                setProjNameLabel(CurrentProjectName) ;

                setProjectPath(Utilities.FilePathSummary(CurrentProjectFullPath, 4, 1));
    
                SystemState.ProjectName = CurrentProjectName;

                RegistryUtils.SetRegistryValue("ProjectFileName", CurrentProjectFullPath);

                CurrentProjectChanged = false;

                return false;
            }
             
            else
            {
                ImageExporter imageExporter = new ImageExporter(this);

                string result = imageExporter.DoExportImage(Path.Combine(outpFilePath, outpFileName));

                if (string.IsNullOrEmpty(result))
                {
                    return true;
                }

                return false;
            }
        }

        private void btnSaveWithImageReplacement_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                DoSaveWithImageReplacement(true);
            }

            withinProjectAccess = false;
        }

        private void DoSaveWithImageReplacement(bool warn=true)
        {
            DialogResult dr;


            string replacementImagePath = SystemGlobals.OriginalImagePath;

            if (string.IsNullOrEmpty(replacementImagePath))
            {
                MessageBoxAdv.Show(
                    "The current project was not initiated with an image."
                    , "No Image Available"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);

                return;
            }

            if (warn)
            {

                if (SystemState.ScaleHasBeenSet && currentPage.UnseamedAreasExist())
                {
                    dr = MessageBoxAdv.Show(
                        "Some areas have not been seamed, do you want to continue to save this project?"
                        , "Open New Project"
                        , MessageBoxAdv.Buttons.YesNo
                        , MessageBoxAdv.Icon.Question);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }


                if (CanvasManager.DrawingInProgress())
                {
                    SaveProjectWarning saveProjectWarningForm = new SaveProjectWarning();

                    saveProjectWarningForm.StartPosition = FormStartPosition.CenterParent;

                    dr = saveProjectWarningForm.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        return;
                    }
                }

            }

            string projectPathName = CurrentProjectPath;
            string projectFullFileName = CurrentProjectFullPath;

            if (string.IsNullOrEmpty(projectFullFileName))
            {
                projectFullFileName = RegistryUtils.GetRegistryStringValue("ProjectFileName", string.Empty);

                if (string.IsNullOrEmpty(projectFullFileName))
                {
                    projectPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                }

                else
                {
                    projectPathName = Path.GetDirectoryName(projectFullFileName);

                    if (string.IsNullOrEmpty(projectPathName))
                    {
                        projectPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                    }
                }
            }

            string inptVisioProjectPath = string.Empty;
            string outpVisioProjectPath = string.Empty;
            

            if (!string.IsNullOrEmpty(projectFullFileName))
            {
                inptVisioProjectPath = Path.Combine(projectPathName, Path.GetFileNameWithoutExtension(projectFullFileName) + ".vsdx");
                outpVisioProjectPath = inptVisioProjectPath;
            }

            ImageReplacerForm imageReplacementForm = new ImageReplacerForm();

            imageReplacementForm.Init(CanvasManager.VsoDocument, outpVisioProjectPath, replacementImagePath);

            imageReplacementForm.ShowDialog(this);
        }

        public bool SaveExistingProject(bool warn)
        {
            if (!CurrentProjectChanged)
            {
                return false;
            }

            DialogResult dr = MessageBoxAdv.Show(
                "Save the current project?"
                , "Save Current Project"
                , MessageBoxAdv.Buttons.YesNoCancel
                , MessageBoxAdv.Icon.Question);

            if (dr == DialogResult.Cancel)
            {
                return true;
            }

            if (dr == DialogResult.No)
            {
                return false;
            }

            return doSaveProject(warn);
        }

        public bool CheckCloseEditForms()
        {
            bool warningNeeded = false;

            if (warningNeeded)
            {
                DialogResult dr = MessageBoxAdv.Show(
                    "Opening a new or existing project requires that all finish edit boxes be closed. You may have unsaved format changes. Do you want to continue?"
                    , "Unsaved Changes"
                    , MessageBoxAdv.Buttons.YesNo
                    , MessageBoxAdv.Icon.Question);

                if (dr == DialogResult.No)
                {
                    return true;
                }
            }

            return false;
        }

        public void CloseEditForms()
        {
            if (FormAreaFinishes != null)
            {
                FormAreaFinishes.ElementsChanged = false;

                FormAreaFinishes.Close();
            }

            if (FormLineFinishes != null)
            {
                FormLineFinishes.ElementsChanged = false;

                FormLineFinishes.Close();
            }

            if (FormSeamFinishes != null)
            {
                FormSeamFinishes.Close();
            }
        }

        private void btnSaveSetup_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                doSaveSetup();
            }

            withinProjectAccess = false;
        }

        private bool doSaveSetup()
        {
            if (string.IsNullOrEmpty(CurrentSetupFullPath))
            {
                return DoSaveSetupAs();
            }

            string setupPathName = Path.GetDirectoryName(CurrentSetupFullPath);
            string setupFileName = Path.GetFileName(CurrentSetupFullPath);

            ProjectSetupExporter setupExporter = new ProjectSetupExporter(this);


            bool success = setupExporter.ExportSetup(setupPathName, setupFileName);

            if (!success)
            {
                return true;
            }

            CurrentProjectChanged = false;

            RegistryUtils.SetRegistryValue("SetupFileName", CurrentSetupFullPath);

            return false;
        }

        private void SaveSetupAs_Click(object sender, EventArgs e)
        {
            while (withinAutosave)
            {
                System.Threading.Thread.Sleep(10);
            }

            withinProjectAccess = true;

            lock (AutosaveLock)
            {
                DoSaveSetupAs();
            }

            withinProjectAccess = false;
        }


        public bool DoSaveSetupAs()
        {
            DialogResult dr;

            string setupPathName = string.Empty;
            string setupFileName = string.Empty;

            if (string.IsNullOrEmpty(CurrentSetupFullPath))
            {
                string setupFullFileName = RegistryUtils.GetRegistryStringValue("SetupFullFilePath", string.Empty);

                if (string.IsNullOrEmpty(setupFullFileName))
                {
                    setupPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                }

                else
                {
                    setupPathName = Path.GetDirectoryName(setupFullFileName);
                    setupFileName = Path.GetFileName(setupFullFileName);

                    if (string.IsNullOrEmpty(setupPathName))
                    {
                        setupPathName = Path.Combine(Program.OCEOperatingDataFolder, "Projects");
                    }
                }
            }

            else
            {
                setupPathName = Path.GetDirectoryName(CurrentSetupFullPath);
                setupFileName = Path.GetFileName(CurrentSetupFullPath);
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = setupPathName;
            saveFileDialog.FileName = setupFileName;
            saveFileDialog.Filter = "Estimator project setup|*.sproj";

            dr = saveFileDialog.ShowDialog();

            if (dr == DialogResult.Cancel)
            {
                return true;
            }

            string outpFilePath = Path.GetDirectoryName(saveFileDialog.FileName);
            string outpFileName = Path.GetFileName(saveFileDialog.FileName);

            if (!outpFileName.EndsWith(".sproj"))
            {
                outpFileName += ".sproj";
            }

            ProjectSetupExporter setupExporter = new ProjectSetupExporter(this);
            bool success = setupExporter.ExportSetup(outpFilePath, outpFileName);

            if (!success)
            {
                return true;
            }

            //CurrentSetupName = Path.GetFileNameWithoutExtension(outpFileName);
            //CurrentProjectPath = outpFilePath;
            CurrentSetupFullPath = saveFileDialog.FileName;

            setProjNameLabel(CurrentProjectName);

            setProjectPath(Utilities.FilePathSummary(CurrentSetupFullPath, 4, 1));
            setOriginalImage(SystemGlobals.OriginalImagePath);

            SystemState.ProjectName = CurrentProjectName;

            RegistryUtils.SetRegistryValue("SetupFullFilePath", CurrentSetupFullPath);

            CurrentProjectChanged = false;

            return false;
        }
    }
}
