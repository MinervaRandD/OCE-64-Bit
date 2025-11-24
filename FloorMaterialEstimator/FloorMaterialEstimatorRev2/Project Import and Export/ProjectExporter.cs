

using System;

namespace FloorMaterialEstimator
{
    using System.IO;
    using System.Windows.Forms;
    using System.Diagnostics;
    using Utilities;
    using SettingsLib;
    using System.Xml.Serialization;

    public class ProjectExporter
    {
        private FloorMaterialEstimatorBaseForm baseForm;

        public ProjectExporter(FloorMaterialEstimatorBaseForm baseForm)
        {
            this.baseForm = baseForm;
        }

        public bool ExportProject(string projectPathName, string projectFileName)
        {
            string outpFilePath = string.Empty;

            try
            {
                Debug.Assert(!string.IsNullOrEmpty(projectPathName) && !string.IsNullOrEmpty(projectFileName));

                string projectFullPath = string.Empty;

                if (!Directory.Exists(projectPathName))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(projectPathName));
                }

                outpFilePath = Path.Combine(projectPathName, projectFileName);

                StreamWriter exportStream = new StreamWriter(outpFilePath);

                ProjectSerializable project = new ProjectSerializable(this.baseForm);

                if (!ProjectSerializable.ProjectSerializationSucceeded)
                {
                    MessageBoxAdv.Show(
                        "Unable to save project. Project database is inconsistent."
                        , "Inconsistent Database"
                        , MessageBoxAdv.Buttons.OK
                        , MessageBoxAdv.Icon.Error);

                    return false;
                }

                project.Serialize(exportStream);

                if (GlobalSettings.ValidateOnProjectSave)
                {
                    if (!validateProjectSave(outpFilePath))
                    {
                        MessageBoxAdv.Show(
                            "Saved project is corrupted. Project not saved."
                            , "Save Project Corrupted"
                            , MessageBoxAdv.Buttons.OK
                            , MessageBoxAdv.Icon.Error);

                        return false;
                    }
                }

                projectFullPath = Path.Combine(projectPathName, projectFileName);

                string CurrentProjectName = Path.GetFileNameWithoutExtension(projectFullPath);

                MessageBoxAdv.Show(
                    "Project '" + CurrentProjectName + "' has been saved."
                    , "Project Saved"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Info);

                return true;
            }

            catch (Exception ex)
            {
                MessageBoxAdv.Show(
                    "Attempt to export project to '" + outpFilePath + "' failed: " + ex.Message
                    , "Project Save Failed"
                    , MessageBoxAdv.Buttons.OK
                    , MessageBoxAdv.Icon.Error);

                return false;
            }
        }

        private bool validateProjectSave(string outpFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectSerializable));

            ProjectSerializable project = null;

            StreamReader serialReader = null;

            try
            {
                serialReader = new StreamReader(outpFilePath);

                project = (ProjectSerializable)xmlSerializer.Deserialize(serialReader);
            }

            catch (Exception ex)
            {
                serialReader.Close();

                return false;
            }

            serialReader.Close();

            return true;
        }

    }

}
