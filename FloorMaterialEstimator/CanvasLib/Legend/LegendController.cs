

using System.Security.Cryptography;
using Geometry;
using Utilities;

namespace CanvasLib.Legend
{
    using System;
    using System.Drawing;
    using Globals;
    using Graphics;
    using FinishesLib;
    using System.Windows.Forms;
    using CanvasLib.Counters;

    public class LegendController
    {
        public AreaLegendNavigationForm AreaModeLegendNavigationForm
        {
            get;
            set;
        } = null;

        public LineLegendNavigationForm LineModeLegendNavigationForm
        {
            get;
            set;
        } = null;

        public LegendModeEnum ActiveLegendMode
        {
            get;
            set;
        } = LegendModeEnum.Unknown;
        
        private GraphicsWindow window;

        public GraphicsPage Page { get; set; } = null;


        private Form baseForm;

        private CounterController counterController;

        public AreaModeLegend AreaModeLegend
        {
            get;
            set;
        } = null;

        public LineModeLegend LineModeLegend
        {
            get;
            set;
        } = null;

        ToolStripButton btnShowLegendForm = null;

        public LegendController(
            Form baseForm
            , GraphicsWindow window
            , GraphicsPage page
            , ToolStripButton btnShowLegendForm
            , CounterController counterController)
        {
            this.baseForm = baseForm;

            this.window = window;

            this.Page = page;

            this.btnShowLegendForm = btnShowLegendForm;

            this.counterController = counterController;

            this.btnShowLegendForm.Click += BtnShowLegendForm_Click; ;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;
        }

        public void Init(AreaFinishBaseList areaFinishBaseList, LineFinishBaseList lineFinishBaseList, CounterList counterList)
        {
            InitializeLegendParameters();

            if (AreaModeLegend != null)
            {
                AreaModeLegend.Delete();
                AreaModeLegend = null;
            }

            if (LineModeLegend != null)
            {
                LineModeLegend.Delete();
                LineModeLegend = null;
            }

            if (AreaModeLegendNavigationForm != null)
            {
                AreaModeLegendNavigationForm.Delete();
                AreaModeLegendNavigationForm = null;
            }

            if (LineModeLegendNavigationForm != null)
            {
                LineModeLegendNavigationForm.Delete();
                LineModeLegendNavigationForm = null;
            }

            InitializeLegendParameters();


            AreaModeLegend = new AreaModeLegend(window, Page, areaFinishBaseList, counterList);
            LineModeLegend = new LineModeLegend(window, Page, lineFinishBaseList);

            createAreaModeLegendNavigationForm();
            createLineModeLegendNavigationForm();

            SetupFormForDesignState();
        }

        public void InitializeLegendParameters()
        {
            SystemGlobals.AreaLegendLocateToClick = false;

            SystemGlobals.ShowAreaLegendInAreaMode = false;

            SystemGlobals.ShowAreaLegendInLineMode = false;

            SystemGlobals.ShowAreaLegendFinishes = true;

            SystemGlobals.ShowAreaLegendCounters = false;

            SystemGlobals.ShowAreaLegendNotes = false;

            SystemGlobals.AreaLegendLocation = new Coordinate(0, Page.PageHeight);

            SystemGlobals.AreaLegendScale = SystemGlobals.DefaultAreaLegendScale;

            SystemGlobals.AreaLegendNotes = null;

            SystemGlobals.LineLegendLocateToClick = false;

            SystemGlobals.ShowLineLegendInAreaMode = false;

            SystemGlobals.ShowLineLegendInLineMode = false;

            SystemGlobals.ShowLineLegendLines = true;

            SystemGlobals.ShowLineLegendNotes = false;

            SystemGlobals.LineLegendLocation = new Coordinate(0, Page.PageHeight);

            SystemGlobals.LineLegendScale = SystemGlobals.DefaultLineLegendScale;

            SystemGlobals.LineLegendNotes = null;
        }

        public void createAreaModeLegendNavigationForm()
        {

            AreaModeLegendNavigationForm = new AreaLegendNavigationForm();
            AreaModeLegendNavigationForm.Init(this, AreaModeLegend);
            AreaModeLegendNavigationForm.FormClosed += AreaModeLegendNavigationForm_FormClosed;
            AreaModeLegendNavigationForm.Activated += AreaModeLegendNavigationForm_Activated;
        }


        private void createLineModeLegendNavigationForm()
        {

            LineModeLegendNavigationForm = new LineLegendNavigationForm();
            LineModeLegendNavigationForm.Init(this, LineModeLegend);
            LineModeLegendNavigationForm.FormClosed += LineModeLegendNavigationForm_FormClosed;
            LineModeLegendNavigationForm.Activated += LineModeLegendNavigationForm_Activated;
        }

        public void AreaModeLegendNavigationForm_Activated(object sender, EventArgs e)
        {
            ActiveLegendMode = LegendModeEnum.AreaMode;
            AreaModeLegendNavigationForm.TopMost = true;
            AreaModeLegendNavigationForm.IsActive = true;

            if (LineModeLegendNavigationForm != null)
            {
                LineModeLegendNavigationForm.IsActive = false;
            }
           
           
        }

        public void LineModeLegendNavigationForm_Activated(object sender, EventArgs e)
        {
            ActiveLegendMode = LegendModeEnum.LineMode;
            LineModeLegendNavigationForm.TopMost = true;
            LineModeLegendNavigationForm.IsActive = true;

            if (AreaModeLegendNavigationForm != null)
            {
                AreaModeLegendNavigationForm.IsActive = false;
            }
            
        }

        private void AreaModeLegendNavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AreaModeLegendNavigationForm = null;

            if (ActiveLegendMode == LegendModeEnum.AreaMode)
            {
                if (LineModeLegendNavigationForm != null)
                {
                    LineModeLegendNavigationForm.Select();
                    LineModeLegendNavigationForm_Activated(null, null);
                }

                else
                {
                    ActiveLegendMode = LegendModeEnum.Unknown;
                }
            }

        }

        private void LineModeLegendNavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LineModeLegendNavigationForm = null;

            if (ActiveLegendMode == LegendModeEnum.LineMode)
            {
                if (AreaModeLegendNavigationForm != null)
                {
                    AreaModeLegendNavigationForm.Select();
                    AreaModeLegendNavigationForm_Activated(null, null);
                }

                else
                {
                    ActiveLegendMode = LegendModeEnum.Unknown;
                }
            }

        }

        private void BtnShowLegendForm_Click(object sender, EventArgs e)
        {
            if (SystemState.LegendFormFirstLoad)
            {
                this.counterController.UnshowAllCounters();

                SystemState.LegendFormFirstLoad = false;
            }

            LegendModeEnum legendMode = LegendModeEnum.Unknown;

            if (KeyboardUtils.AltKeyPressed)
            {
                if (SystemState.DesignState == DesignState.Area)
                {
                    legendMode = LegendModeEnum.LineMode;
                }

                else if (SystemState.DesignState == DesignState.Line)
                {
                    legendMode = LegendModeEnum.AreaMode;
                }
            }

            else
            {
                if (SystemState.DesignState == DesignState.Area)
                {
                    legendMode = LegendModeEnum.AreaMode;
                }

                else if (SystemState.DesignState == DesignState.Line)
                {
                    legendMode = LegendModeEnum.LineMode;
                }
            }
           

            if (legendMode == LegendModeEnum.AreaMode)
            {
                if (AreaModeLegendNavigationForm == null)
                {
                    createAreaModeLegendNavigationForm();
                }

                this.AreaModeLegendNavigationForm.Show();
                this.AreaModeLegendNavigationForm.Activate();
                this.AreaModeLegendNavigationForm.TopMost = true;
            }

            else if (legendMode == LegendModeEnum.LineMode)
            {
                if (LineModeLegendNavigationForm == null)
                {
                    createLineModeLegendNavigationForm();
                }

                this.LineModeLegendNavigationForm.Show();
                this.LineModeLegendNavigationForm.TopMost = true;
            }

            btnShowLegendForm.BackColor = Color.Orange;

           // _legendNavigationForm.WindowState = FormWindowState.Normal;
        }

        public bool AreaModeLegendIsActive()
        {
            if (AreaModeLegendNavigationForm == null)
            {
                return false;
            }

            return AreaModeLegendNavigationForm.IsActive;
        }

        public bool LineModeLegendIsActive()
        {
            if (LineModeLegendNavigationForm == null)
            {
                return false;
            }

            return LineModeLegendNavigationForm.IsActive;
        }

        private void SystemState_DesignStateChanged(DesignState previousDesignState, DesignState currentDesignState)
        {
            SetupFormForDesignState();
        }


        public void SetupFormForDesignState()
        {
            if (SystemState.DesignState == DesignState.Area)
            {
                SetAreaModeLegendLayerVisibility(SystemGlobals.ShowAreaLegendInAreaMode);
                SetLineModeLegendLayerVisibility(SystemGlobals.ShowLineLegendInAreaMode);
               
                return;
            }

            if (SystemState.DesignState == DesignState.Line)
            {
                SetAreaModeLegendLayerVisibility(SystemGlobals.ShowAreaLegendInLineMode);
                SetLineModeLegendLayerVisibility(SystemGlobals.ShowLineLegendInLineMode);
                
                return;
            }
        }

        public void SetAreaModeLegendLayerVisibility(bool visible)
        {
            if (Page.AreaLegendLayer.Visibility == visible)
            {
                return;
            }

            Page.AreaLegendLayer.SetLayerVisibility(visible);
        }

        public void SetLineModeLegendLayerVisibility(bool visible)
        {
            if (Page.LineLegendLayer.Visibility == visible)
            {
                return;
            }

            Page.LineLegendLayer.SetLayerVisibility(visible);
        }

    }
}

