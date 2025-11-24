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
    using Geometry;

    [Serializable]
    public class ProjectSerializable
    {
       
        public string CurrentProjectName { get; set; }

        public string Version { get; set; }

        public PageSerializable Page { get; set; }

        #region Legend Elements
        /*******************/
        /* Legend Elements */
        /*******************/

        public LegendSerializable AreaModeLegend { get; set; }

        public LegendSerializable LineModeLegend { get; set; }
        #endregion

        public AreaFinishBaseList AreaFinishBaseList { get; set; }

        public LineFinishBaseList LineFinishBaseList { get; set;}

        public LineFinishBase ZeroLineBase { get; set; }

        public SeamFinishBaseList SeamFinishBaseList { get; set; }

        public List<LayoutAreaSerializable> LayoutAreaList { get; set; }

        public List<DirectedLineSerializable> DirectedLineList { get; set; }

        public CounterList CountersList { get; set; }

        public List<CounterSerializable> GraphicCounterList { get; set; }

        public List<CanvasLib.Labels.Label> LabelList { get; set; }

        public List<TakeoutSerializable> TakeoutList { get; set; }

        public List<RolloutSerializable> RolloutList { get; set; }

        public List<UndrageSerializable> UndrageList { get; set; }

        public List<VirtualUndrageSerializable> VirtualUndrageList { get; set; }

        public List<VirtualOverageSerializable> VirtualOverageList { get; set; }

        public FieldGuideControllerSerializable fieldGuideController { get; set;}

        public string ManaulSeamDisplayStatus { get; set; }

        public string NormalSeamDisplayStatus { get; set; }

        public int FixedWidthFeet { get; set; }

        public int FixedWidthInch { get; set; }

        public ProjectOptionsSerializable Options { get; set; }

        public String Notes { get; set; }

        public int? XtraLgthFeet { get; set; }

        public int? XtraLgthInch { get; set; }

        public string OriginalImagePath { get; set; }

        #region Legend Elements

        /*****************************************************/
        /* The following elements are for control of legends */
        /*****************************************************/

        /* Area Legend */

        public bool AreaLegendLocateToClick { get; set; }

        public bool ShowAreaLegendInAreaMode { get; set; }

        public bool ShowAreaLegendInLineMode { get; set; }

        public bool ShowAreaLegendFinishes { get; set; }

        public bool ShowAreaLegendCounters { get; set; }

        public bool ShowAreaLegendNotes { get; set; }

        public Coordinate AreaLegendLocation { get; set; }

        public double AreaLegendScale { get; set; }

        public string AreaLegendNotes { get; set; } = null;

        /* Line Legend */

        public bool LineLegendLocateToClick { get; set; }

        public bool ShowLineLegendInAreaMode { get; set; }

        public bool ShowLinLegendInLineMode { get; set; }

        public bool ShowLineLegendLines { get; set; }

        public bool ShowLineLegendNotes { get; set; }

        public Coordinate LineLegendLocation { get; set; }

        public double LineLegendScale { get; set; }

        public string LineLegendNotes { get; set; } = null;

        /*****************************************************/

        #endregion

        public static bool ProjectSerializationSucceeded
        {
            get;
            set;
        } = true;

        public ProjectSerializable() { }

        public ProjectSerializable(FloorMaterialEstimatorBaseForm baseForm)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { baseForm });
#endif
            ProjectSerializationSucceeded = true;

            try
            {
                Version = Program.Version;

                CurrentProjectName = baseForm.CurrentProjectName;

                CanvasPage currentPage = baseForm.CanvasManager.CurrentPage;

                Page = new PageSerializable(currentPage);

                AreaFinishBaseList = FinishGlobals.AreaFinishBaseList;

                LineFinishBaseList = baseForm.LineFinishBaseList;

                if (baseForm.ZeroLineBase != null)
                {
                    ZeroLineBase = baseForm.ZeroLineBase.Clone();
                }

                SeamFinishBaseList = baseForm.SeamFinishBaseList;

                CountersList = baseForm.CanvasManager.CountersList;

                LabelList = baseForm.CanvasManager.LabelManager.GetLabelList();

                VirtualOverageList = new List<VirtualOverageSerializable>();
                VirtualUndrageList = new List<VirtualUndrageSerializable>();

                foreach (AreaFinishManager areaFinishManager in FinishManagerGlobals.AreaFinishManagerList)
                {
                    foreach (VirtualOverage virtualOverage in areaFinishManager.VirtualOverageList)
                    {
                        VirtualOverageSerializable virtualOverageSerializable =
                            new VirtualOverageSerializable(virtualOverage, areaFinishManager.AreaFinishBase.Guid);

                        VirtualOverageList.Add(virtualOverageSerializable);
                    }

                    foreach (VirtualUndrage virtualUndrage in areaFinishManager.VirtualUndrageList)
                    {
                        VirtualUndrageSerializable virtualUndrageSerializable =
                            new VirtualUndrageSerializable(virtualUndrage, areaFinishManager.AreaFinishBase.Guid);

                        VirtualUndrageList.Add(virtualUndrageSerializable);
                    }
                }


                LayoutAreaList =
                    (List<LayoutAreaSerializable>)currentPage.LayoutAreas.Select(a => new LayoutAreaSerializable(a))
                        .ToList();

                DirectedLineList = (List<DirectedLineSerializable>)currentPage.DirectedLines
                    .Select(l => new DirectedLineSerializable(l)).ToList();

                GraphicCounterList = new List<CounterSerializable>();

                for (int i = 0; i < 26; i++)
                {
                    GraphicCounterList.AddRange
                    (
                        baseForm.CanvasManager.CounterController.GraphicsCounters(i)
                            .Select(c => new CounterSerializable(c)).ToList()
                    );
                }

                TakeoutList = new List<TakeoutSerializable>();

                foreach (LineFinishManager lineFinishManager in FinishManagerGlobals.LineFinishManagerList)
                {
                    foreach (DoorTakeout doorTakeout in lineFinishManager.DoorTakeoutList)
                    {
                        TakeoutList.Add(new TakeoutSerializable(doorTakeout));
                    }
                }
                //TakeoutList = (List<TakeoutSerializable>)currentPage.GraphicsTakeoutAreaDict.Values.Select(t => new TakeoutSerializable(t)).ToList();

                fieldGuideController = new FieldGuideControllerSerializable(baseForm.CanvasManager.FieldGuideController,
                    SystemState.BtnShowFieldGuides.Checked);

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

                XtraLgthFeet = SystemState.XtraLgthFeet;
                XtraLgthInch = SystemState.XtraLgthInch;

                if (baseForm.CanvasManager.LegendController.AreaModeLegend is null)
                {
                    this.AreaModeLegend = new LegendSerializable(Coordinate.NullCoordinate, 0,
                        CanvasLib.Legend.LegendLocation.NotSet, false, false, string.Empty);
                }

                else
                {
                    CanvasLib.Legend.AreaModeLegend areaModeLegend =
                        baseForm.CanvasManager.LegendController.AreaModeLegend;

                    Coordinate location = areaModeLegend.Location;
                    double size = areaModeLegend.CurrentSize;
                    CanvasLib.Legend.LegendLocation legendLocation = areaModeLegend.LegendShowLocation;
                    bool visibile = areaModeLegend.Visible;
                    bool locateToClick = SystemGlobals.AreaLegendLocateToClick;
                    string notes = areaModeLegend.Notes;

                    this.AreaModeLegend =
                        new LegendSerializable(location, size, legendLocation, visibile, locateToClick, notes);
                }

                //if (baseForm.CanvasManager.LegendController.LineModeLegend is null)
                //{
                //    this.LineModeLegend = new LegendSerializable(Coordinate.NullCoordinate, 0,
                //        CanvasLib.Legend.LegendLocation.NotSet, false, false, string.Empty);
                //}

                //else
                //{
                //    CanvasLib.Legend.LineModeLegend lineModeLegend =
                //        baseForm.CanvasManager.LegendController.LineModeLegend;

                //    Coordinate location = lineModeLegend.Location;
                //    double size = lineModeLegend.CurrentSize;
                //    CanvasLib.Legend.LegendLocation legendLocation = lineModeLegend.LegendShowLocation;
                //    bool visibile = lineModeLegend.Visible;
                //    bool locateToClick = lineModeLegend.LocateToClick;
                //    string notes = lineModeLegend.Notes;

                //    this.LineModeLegend =
                //        new LegendSerializable(location, size, legendLocation, visibile, locateToClick, notes);
                //}

                Options = new ProjectOptionsSerializable(baseForm);

                OriginalImagePath = SystemGlobals.OriginalImagePath;

                Notes = baseForm.Notes;

                #region Legend Elements

                /*************************************************/
                /* The following elements are related to legends */
                /*************************************************/

                /* Area Legend */

                AreaLegendLocateToClick = SystemGlobals.AreaLegendLocateToClick;

                ShowAreaLegendInAreaMode = SystemGlobals.ShowAreaLegendInAreaMode;

                ShowAreaLegendInLineMode = SystemGlobals.ShowAreaLegendInLineMode;

                ShowAreaLegendFinishes = SystemGlobals.ShowAreaLegendFinishes;

                ShowAreaLegendCounters = SystemGlobals.ShowAreaLegendCounters;

                AreaLegendLocation = SystemGlobals.AreaLegendLocation;

                ShowAreaLegendNotes = SystemGlobals.ShowAreaLegendNotes;

                AreaLegendScale = SystemGlobals.AreaLegendScale;

                AreaLegendNotes = SystemGlobals.AreaLegendNotes;

                /* Line Legend */

                LineLegendLocateToClick = SystemGlobals.LineLegendLocateToClick;

                ShowLineLegendInAreaMode = SystemGlobals.ShowLineLegendInAreaMode;

                ShowLinLegendInLineMode = SystemGlobals.ShowAreaLegendInAreaMode;

                ShowLineLegendLines = SystemGlobals.ShowLineLegendLines;

                ShowLineLegendNotes = SystemGlobals.ShowLineLegendNotes;

                LineLegendLocation = SystemGlobals.LineLegendLocation;

                LineLegendScale = SystemGlobals.LineLegendScale;

                LineLegendNotes = SystemGlobals.LineLegendNotes;

                /*************************************************/

                #endregion

            }

            catch (Exception ex)
            {
                Tracer.TraceGen.TraceException("ProjectSerializable:ProjectSerializable throws an exception", ex, 1,
                    true);

                ProjectSerializationSucceeded = false;
            }
        }

        public void Serialize(StreamWriter serialWriter)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectSerializable));

            xmlSerializer.Serialize(serialWriter, this);

            serialWriter.Flush();
            serialWriter.Close();
        }

        public static ProjectSerializable Deserialize(StreamReader serialReader)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectSerializable));

            ProjectSerializable project = null;

            try
            {
                project = (ProjectSerializable)xmlSerializer.Deserialize(serialReader);
            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occured during the attempt to load the project. Most likely this means that the project file is corrupted");

                serialReader.Close();

                return null;
            }

            #region Legend Elements

            /*************************************************/
            /* The following elements are related to legends */
            /*************************************************/

            /* Area Legend */

            SystemGlobals.AreaLegendLocateToClick = project.AreaLegendLocateToClick;

            SystemGlobals.ShowAreaLegendInAreaMode = project.ShowAreaLegendInAreaMode;

            SystemGlobals.ShowAreaLegendInLineMode = project.ShowAreaLegendInLineMode;

            SystemGlobals.ShowAreaLegendFinishes = project.ShowAreaLegendFinishes;

            SystemGlobals.ShowAreaLegendCounters = project.ShowAreaLegendCounters;

            SystemGlobals.ShowAreaLegendNotes = project.ShowAreaLegendNotes;

            SystemGlobals.AreaLegendLocation = project.AreaLegendLocation;

            SystemGlobals.AreaLegendScale = project.AreaLegendScale;


            /* Line Legend */

            SystemGlobals.ShowLineLegendInAreaMode = project.ShowLineLegendInAreaMode;

            SystemGlobals.ShowLineLegendInLineMode = project.ShowLinLegendInLineMode;

            SystemGlobals.ShowLineLegendLines = project.ShowLineLegendLines;

            SystemGlobals.ShowLineLegendNotes = project.ShowLineLegendNotes;

            SystemGlobals.LineLegendLocateToClick = project.LineLegendLocateToClick;

            SystemGlobals.LineLegendLocation = project.LineLegendLocation;

            /*************************************************/
            #endregion

            serialReader.Close();

            return project;
        }
    }
}
