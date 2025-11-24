

namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;

    using System.IO;
    using System.Windows.Forms;

    using Utilities;
    using Graphics;

    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using MaterialsLayout;
    using FinishesLib;
    using PaletteLib;
    using CanvasLib.Counters;
    using Globals;

    using CanvasLib.Markers_and_Guides;
    using CanvasLib.DoorTakeouts;

    public class ProjectSetupImporter
    {
        FloorMaterialEstimatorBaseForm baseForm;

        private GraphicsWindow window => baseForm.CanvasManager.Window;

        private GraphicsPage page => baseForm.CanvasManager.Page;

        public ProjectSetupImporter(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;
        }

        public List<GraphicsCounter> GraphicsCounterList = new List<GraphicsCounter>();

        private FloorMaterialEstimator.CanvasManager.CanvasManager canvasManager => baseForm.CanvasManager;

        private CanvasPage currentPage => canvasManager.CurrentPage;

        public string ImportProjectSetup(string projectSetupFileName)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { projectFileName });
#endif
            if (string.IsNullOrEmpty(projectSetupFileName))
            {
                return string.Empty;
            }

            string extension = Path.GetExtension(projectSetupFileName);

            if (extension == ".sproj")
            {
                return importSProjSetup(projectSetupFileName);
            }

            else if (extension == ".eproj")
            {
                return importEProjSetup(projectSetupFileName);
            }

            else
            {
                MessageBox.Show("Unknown setup file type.");

                return string.Empty;
            }

            return projectSetupFileName;
        }

        private string importSProjSetup(string projectSetupFileName)
        {
            try
            {
                StreamReader serialReader = new StreamReader(projectSetupFileName);

                ProjectSetupSerializable setup = ProjectSetupSerializable.Deserialize(serialReader);

                if (setup.Version != Program.Version)
                {
                    DialogResult result = ManagedMessageBox.Show("The version of this project is not consistent with the current release. Do you want to continue?", "Inconsistent Version", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                    {
                        return string.Empty;
                    }
                }

                baseForm.tbcPageAreaLine.Hide();

                //if (FinishManagerGlobals.AreaFinishManagerList != null)
                //{
                //    FinishManagerGlobals.AreaFinishManagerList.Dispose();
                //}

                // Kludge to assure all area finishes are represented in square feet

                setup.AreaFinishBaseList.ForEach(a => a.AreaDisplayUnits = AreaDisplayUnits.SquareFeet);

                FinishGlobals.AreaFinishBaseList = setup.AreaFinishBaseList;

                if (FinishManagerGlobals.AreaFinishManagerList != null)
                {
                    FinishManagerGlobals.AreaFinishManagerList.Dispose();
                }

                FinishManagerGlobals.AreaFinishManagerList = new AreaFinishManagerList(window, page, FinishGlobals.AreaFinishBaseList);

                baseForm.LineFinishBaseList = setup.LineFinishBaseList;

                if (FinishManagerGlobals.LineFinishManagerList != null)
                {
                    FinishManagerGlobals.LineFinishManagerList.Dispose();
                }

                FinishManagerGlobals.LineFinishManagerList = new LineFinishManagerList(baseForm.CanvasManager, window, page, baseForm.LineFinishBaseList);

                baseForm.SeamFinishBaseList = setup.SeamFinishBaseList;

                if (baseForm.SeamFinishManagerList != null)
                {
                    baseForm.SeamFinishManagerList.Dispose();
                }

                baseForm.SeamFinishManagerList = new SeamFinishManagerList(baseForm.CanvasManager, window, page, baseForm.SeamFinishBaseList);


                if (setup.ZeroLineBase != null)
                {
                    baseForm.ZeroLineBase = setup.ZeroLineBase.Clone();
                }

                baseForm.CanvasManager.CountersList = setup.CountersList;

                setupSeamFinishPalette(setup.SeamFinishBaseList, setup.AreaFinishBaseList);

                setupLineFinishPalette(setup.LineFinishBaseList);

                setupAreaFinishPalette(FinishGlobals.AreaFinishBaseList, FinishManagerGlobals.AreaFinishManagerList);

                setupFieldGuides(setup.fieldGuideController);

                baseForm.tbcPageAreaLine.Show();

                if (setup.NormalSeamDisplayStatus == "ShowAll")
                {
                    baseForm.RbnSeamModeAutoSeamsShowAll.Checked = true;
                }

                else
                {
                    baseForm.RbnSeamModeAutoSeamsShowUnHideable.Checked = true;
                }

                if (setup.ManaulSeamDisplayStatus == "ShowAll")
                {
                    baseForm.RbnSeamModeManualSeamsShowAll.Checked = true;
                }

                else if (setup.ManaulSeamDisplayStatus == "HideAll")
                {
                    baseForm.RbnSeamModeManualSeamsHideAll.Checked = true;
                }

                else
                {
                    baseForm.RbnSeamModeManualSeamsShowAll.Checked = true; // Default. Should not be needed but may be an issue because unhideable manual seams were removed along with the associated radio button.
                }

                if (canvasManager.selectedFinishType.MaterialsType == MaterialsType.Rolls)
                {
                    baseForm.EnableOversUndersButton(true);
                }

                if (Utilities.IsNotNull(setup.Options))
                {
                    setup.Options.DeSerialize(baseForm);
                }

                baseForm.nudFixedWidthFeet.Value = setup.FixedWidthFeet;

                baseForm.nudFixedWidthInches.Value = setup.FixedWidthInch;


                canvasManager.LegendController.Init(FinishGlobals.AreaFinishBaseList, FinishGlobals.LineFinishBaseList, canvasManager.CountersList);

                PalettesGlobal.AreaFinishPalette.SetFiltered();
                PalettesGlobal.LineFinishPalette.SetFiltered();

                SystemGlobals.paletteSource = "CustomSSetup";

                return projectSetupFileName;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Attempt to load setup from file '" + projectSetupFileName + "' failed: " + ex.Message);

                return string.Empty;
            }
        }

        private string importEProjSetup(string projectSetupFileName)
        {
            try
            {
                StreamReader serialReader = new StreamReader(projectSetupFileName);

                ProjectSerializable project = ProjectSerializable.Deserialize(serialReader);

                if (project == null)
                {
                    return string.Empty;
                }

                if (project.Version != Program.Version)
                {
                    DialogResult result = ManagedMessageBox.Show("The version of this project is not consistent with the current release. Do you want to continue?", "Inconsistent Version", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes)
                    {
                        return string.Empty;
                    }
                }

                baseForm.tbcPageAreaLine.Hide();

                SystemGlobals.OriginalImagePath= projectSetupFileName;

                // Kludge to assure all area finishes are represented in square feet

                project.AreaFinishBaseList.ForEach(a => a.AreaDisplayUnits = AreaDisplayUnits.SquareFeet);

                FinishGlobals.AreaFinishBaseList = project.AreaFinishBaseList;

                if (FinishManagerGlobals.AreaFinishManagerList != null)
                {
                    FinishManagerGlobals.AreaFinishManagerList.Dispose();
                }

                FinishManagerGlobals.AreaFinishManagerList = new AreaFinishManagerList(window, page, FinishGlobals.AreaFinishBaseList);

                baseForm.LineFinishBaseList = project.LineFinishBaseList;

                if (FinishManagerGlobals.LineFinishManagerList != null)
                {
                    FinishManagerGlobals.LineFinishManagerList.Dispose();
                }

                FinishManagerGlobals.LineFinishManagerList = new LineFinishManagerList(baseForm.CanvasManager, window, page, baseForm.LineFinishBaseList);

                baseForm.SeamFinishBaseList = project.SeamFinishBaseList;

                if (baseForm.SeamFinishManagerList != null)
                {
                    baseForm.SeamFinishManagerList.Dispose();
                }

                baseForm.SeamFinishManagerList = new SeamFinishManagerList(baseForm.CanvasManager, window, page, baseForm.SeamFinishBaseList);

                baseForm.CanvasManager.CountersList = project.CountersList;

                setupSeamFinishPalette(project.SeamFinishBaseList, project.AreaFinishBaseList);

                setupLineFinishPalette(project.LineFinishBaseList);

                setupAreaFinishPalette(FinishGlobals.AreaFinishBaseList, FinishManagerGlobals.AreaFinishManagerList);

                setupFieldGuides(project.fieldGuideController);

                baseForm.tbcPageAreaLine.Show();

                if (project.NormalSeamDisplayStatus == "ShowAll")
                {
                    baseForm.RbnSeamModeAutoSeamsShowAll.Checked = true;
                }

                else
                {
                    baseForm.RbnSeamModeAutoSeamsShowUnHideable.Checked = true;
                }

                if (project.ManaulSeamDisplayStatus == "ShowAll")
                {
                    baseForm.RbnSeamModeManualSeamsShowAll.Checked = true;
                }

                else if (project.ManaulSeamDisplayStatus == "HideAll")
                {
                    baseForm.RbnSeamModeManualSeamsHideAll.Checked = true;
                }

                else
                {
                    baseForm.RbnSeamModeManualSeamsShowAll.Checked = true; // Default. Should not be needed but may be an issue because unhideable manual seams were removed along with the associated radio button.
                }

                if (canvasManager.selectedFinishType.MaterialsType == MaterialsType.Rolls)
                {
                    baseForm.EnableOversUndersButton(true);
                }

                if (Utilities.IsNotNull(project.Options))
                {
                    project.Options.DeSerialize(baseForm);
                }

                baseForm.nudFixedWidthFeet.Value = project.FixedWidthFeet;

                baseForm.nudFixedWidthInches.Value = project.FixedWidthInch;

                if (FinishManagerGlobals.AreaFinishManagerList != null)
                {
                    FinishManagerGlobals.AreaFinishManagerList.Dispose();
                }

                SystemGlobals.ShowAreaLegendInAreaMode = project.ShowAreaLegendInAreaMode;

                SystemGlobals.ShowAreaLegendInLineMode = project.ShowAreaLegendInLineMode;

                SystemGlobals.ShowLineLegendInAreaMode = project.ShowLineLegendInAreaMode;

                SystemGlobals.ShowLineLegendInLineMode = project.ShowAreaLegendInLineMode;

                canvasManager.LegendController.Init(FinishGlobals.AreaFinishBaseList, FinishGlobals.LineFinishBaseList, canvasManager.CountersList);


                SystemGlobals.paletteSource = "CustomESetup";

                return projectSetupFileName;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Attempt to load setup from file '" + projectSetupFileName + "' failed: " + ex.Message);

                return string.Empty;
            }
        }

        private void setupAreaFinishPalette(
            AreaFinishBaseList areaFinishBaseList
            ,AreaFinishManagerList areaFinishManagerList)
        {
            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (string.IsNullOrEmpty(areaFinishBase.Guid))
                {
                    areaFinishBase.Guid = GuidMaintenance.CreateGuid(areaFinishBase);
                }

                else
                {
                    if (GuidMaintenance.ContainsGuid(areaFinishBase.Guid))
                    {
                        GuidMaintenance.RemoveGuid(areaFinishBase.Guid);
                    }

                    GuidMaintenance.AddGuid(areaFinishBase.Guid, areaFinishBase);
                }
            }

            UCAreaFinishPalette areaPalette = PalettesGlobal.AreaFinishPalette;

            areaPalette.SuspendLayout();


            FinishGlobals.AreaFinishBaseList.ItemSelected += baseForm.AreaFinishBaseList_ItemSelected;

            areaPalette.Init(canvasManager.Window, canvasManager.Page);

            areaPalette.Select(areaFinishBaseList.SelectedItemIndex);

            areaPalette.SetLineFinish(PalettesGlobal.LineFinishPalette.SelectedItemIndex);

            areaPalette.ResumeLayout();
        }

        private void setupLineFinishPalette(LineFinishBaseList lineFinishBaseList)
        {
            foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
            {
                if (string.IsNullOrEmpty(lineFinishBase.Guid))
                {
                    lineFinishBase.Guid = GuidMaintenance.CreateGuid(lineFinishBase);
                }

                else
                {
                    if (GuidMaintenance.ContainsGuid(lineFinishBase.Guid))
                    {
                        GuidMaintenance.RemoveGuid(lineFinishBase.Guid);
                    }

                    GuidMaintenance.AddGuid(lineFinishBase.Guid, lineFinishBase);
                }
            }

            UCLineFinishPalette linePalette = baseForm.linePalette;

            linePalette.SuspendLayout();

            baseForm.LineFinishBaseList.ItemSelected += baseForm.LineFinishBaseList_ItemSelected;

            linePalette.Init(lineFinishBaseList);

            linePalette.Select(lineFinishBaseList.SelectedItemIndex);

            linePalette.ResumeLayout();
        }

        private void setupSeamFinishPalette(SeamFinishBaseList seamFinishBaseList, AreaFinishBaseList areaFinishBaseList)
        {
            foreach (SeamFinishBase seamFinishBase in seamFinishBaseList)
            {
                if (string.IsNullOrEmpty(seamFinishBase.Guid))
                {
                    seamFinishBase.Guid = GuidMaintenance.CreateGuid(seamFinishBase);
                }

                else
                {
                    if (GuidMaintenance.ContainsGuid(seamFinishBase.Guid))
                    {
                        GuidMaintenance.RemoveGuid(seamFinishBase.Guid);
                    }

                    GuidMaintenance.AddGuid(seamFinishBase.Guid, seamFinishBase);
                }
            }
           
            UCSeamFinishPalette seamPalette = baseForm.seamPalette;

            seamPalette.SuspendLayout();

            seamPalette.Reinit(areaFinishBaseList, seamFinishBaseList);

            // The following sets up the subscription in the area finish elements to the changes in the seams so that the area seams 
            // react immediately to changes in the seam format.

            seamFinishBaseList.ForEach(s => { seamFinishBaseList.SeamFinishByGuid.Add(s.Guid, s); seamFinishBaseList.SeamFinishByName.Add(s.SeamName, s); });

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    if (areaFinishBase.SeamFinishBase is null)
                    {
                        continue;
                    }

                    string seamGuid = areaFinishBase.SeamFinishBase.Guid;

                    if (baseForm.SeamFinishBaseList.SeamFinishByGuid.ContainsKey(seamGuid))
                    {
                        areaFinishBase.SeamFinishBase = baseForm.SeamFinishBaseList.SeamFinishByGuid[seamGuid];

                        continue;
                    }

                    string seamName = areaFinishBase.SeamFinishBase.SeamName;

                    if (baseForm.SeamFinishBaseList.SeamFinishByName.ContainsKey(seamName))
                    {
                        areaFinishBase.SeamFinishBase = baseForm.SeamFinishBaseList.SeamFinishByName[seamName];

                        continue;
                    }

                    throw new Exception("Unable to link seam finish base into area finish base.");
                }

                else
                {
                    areaFinishBase.SeamFinishBase = null;
                }
            }

            seamPalette.ResumeLayout();
        }

        private void setupFieldGuides(FieldGuideControllerSerializable fieldGuideControllerSerializable)
        {
            FieldGuideController fieldGuideContoller = baseForm.CanvasManager.FieldGuideController;

            fieldGuideContoller.Init(
                fieldGuideControllerSerializable.FieldGuideLineColor,
                fieldGuideControllerSerializable.FieldGuideLineOpacity,
                fieldGuideControllerSerializable.FieldGuideLineWidthInPts,
                fieldGuideControllerSerializable.FieldGuideLineStyle
                );
        }
    }
}
