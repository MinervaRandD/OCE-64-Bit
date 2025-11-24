using Globals;

namespace FloorMaterialEstimator
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using FinishesLib;
    using CanvasLib.Counters;
    using Graphics;
    using FloorMaterialEstimator.CanvasManager;
    using System.Drawing;
    using FloorMaterialEstimator.Finish_Controls;
    using MaterialsLayout;
    using CanvasLib.DoorTakeouts;
    using TracerLib;
    using System.Windows.Forms;

    [Serializable]
    public class ProjectSetupSerializable
    {
        public string Version { get; set; }

        public AreaFinishBaseList AreaFinishBaseList { get; set; }

        public LineFinishBaseList LineFinishBaseList { get; set; }

        public LineFinishBase ZeroLineBase { get; set; }

        public SeamFinishBaseList SeamFinishBaseList { get; set; }

        public CounterList CountersList { get; set; }

        public List<VirtualUndrageSerializable> VirtualUndrageList { get; set; }

        public FieldGuideControllerSerializable fieldGuideController { get; set; }

        public string ManaulSeamDisplayStatus { get; set; }

        public string NormalSeamDisplayStatus { get; set; }

        public int FixedWidthFeet { get; set; }

        public int FixedWidthInch { get; set; }

        public ProjectOptionsSerializable Options { get; set; }

        public static bool ProjectSerializationSucceeded
        {
            get;
            set;
        } = true;

        public ProjectSetupSerializable() { }

        public ProjectSetupSerializable(FloorMaterialEstimatorBaseForm baseForm)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { baseForm });
#endif
            ProjectSerializationSucceeded = true;

            try
            {
                Version = Program.Version;

                AreaFinishBaseList = FinishGlobals.AreaFinishBaseList.Clone(true);

                LineFinishBaseList = baseForm.LineFinishBaseList.Clone(true);

                if (baseForm.ZeroLineBase != null)
                {
                    ZeroLineBase = baseForm.ZeroLineBase.Clone();
                }

                SeamFinishBaseList = baseForm.SeamFinishBaseList.Clone(true);

                CountersList = baseForm.CanvasManager.CountersList.Clone(true);

                fieldGuideController = new FieldGuideControllerSerializable(baseForm.CanvasManager.FieldGuideController, SystemState.BtnShowFieldGuides.Checked);

                if (baseForm.RbnSeamModeAutoSeamsShowAll.Checked)
                {
                    NormalSeamDisplayStatus = "ShowAll";
                }

                else
                {
                    NormalSeamDisplayStatus = "ShowUnhideable";
                }

                if (baseForm.RbnSeamModeManualSeamsShowAll.Checked)
                {
                    ManaulSeamDisplayStatus = "ShowAll";
                }

                else if (baseForm.RbnSeamModeManualSeamsHideAll.Checked)
                {
                    ManaulSeamDisplayStatus = "HideAll";
                }

                else
                {
                    ManaulSeamDisplayStatus = "ShowUnhideable";
                }

                FixedWidthFeet = (int)baseForm.nudFixedWidthFeet.Value;

                FixedWidthInch = (int)baseForm.nudFixedWidthInches.Value;

                Options = new ProjectOptionsSerializable(baseForm);

                //Notes = baseForm.Notes;
            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("ProjectSetupSerializable:ProjectSerializable throws an exception", ex, 1, true);

                ProjectSerializationSucceeded = false;
            }
        }

        public void Serialize(StreamWriter serialWriter)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectSetupSerializable));

            xmlSerializer.Serialize(serialWriter, this);

            serialWriter.Flush();
            serialWriter.Close();
        }

        public static ProjectSetupSerializable Deserialize(StreamReader serialReader)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectSetupSerializable));

            ProjectSetupSerializable projectSetup = (ProjectSetupSerializable)xmlSerializer.Deserialize(serialReader);

            serialReader.Close();

            return projectSetup;
        }
    }
}
