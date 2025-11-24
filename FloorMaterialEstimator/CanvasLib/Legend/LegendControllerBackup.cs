

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

    public class LegendControllerBackup
    {
        LegendNavigationForm _legendNavigationForm = null;

        public LegendNavigationForm AreaModeLegendNavigationForm
        {
            get;
            set;
        } = null;

        private GraphicsWindow window;

        private GraphicsPage page;

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

        public LegendControllerBackup(
            Form baseForm
            , GraphicsWindow window
            , GraphicsPage page
            , ToolStripButton btnShowLegendForm
            , CounterController counterController)
        {
            this.baseForm = baseForm;

            this.window = window;

            this.page = page;

            this.btnShowLegendForm = btnShowLegendForm;

            this.counterController = counterController;

            this.btnShowLegendForm.Click += BtnShowLegendForm_Click; ;
        }

        public void Init(AreaFinishBaseList areaFinishBaseList, LineFinishBaseList lineFinishBaseList, CounterList counterList)
        {
            if (AreaModeLegend != null)
            {
                AreaModeLegend.Delete();
            }

            if (LineModeLegend != null)
            {
                LineModeLegend.Delete();
            }

            AreaModeLegend = new AreaModeLegend(window, page, areaFinishBaseList, counterList);

            LineModeLegend = new LineModeLegend(window, page, lineFinishBaseList);

            if (_legendNavigationForm != null)
            {
                _legendNavigationForm.Reset(AreaModeLegend, LineModeLegend);
            }

            SystemState.LegendFormFirstLoad = true;
        }

        public void ResetCurrentLegendSizes()
        {
            if (_legendNavigationForm != null)
            {
                _legendNavigationForm.ResetCurrentLegendSizes();
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

            if (KeyboardUtils.AKeyPressed)
            {
                legendMode = LegendModeEnum.AreaMode;
            }

            else if (KeyboardUtils.LKeyPressed)
            {
                legendMode = LegendModeEnum.LineMode;
            }

            else if (SystemState.DesignState == DesignState.Area)
            {
                legendMode = LegendModeEnum.AreaMode;
            }

            else if (SystemState.DesignState == DesignState.Line)
            {
                legendMode = LegendModeEnum.LineMode;
            }

            if (_legendNavigationForm is null)
            {
                _legendNavigationForm = new LegendNavigationForm();

                _legendNavigationForm.Init(AreaModeLegend, LineModeLegend);

                _legendNavigationForm.FormClosed += _legendNavigationForm_FormClosed;

                _legendNavigationForm.Location = new System.Drawing.Point(baseForm.Location.X + 32, baseForm.Location.Y + 32);

                _legendNavigationForm.Show(baseForm);

            }

            _legendNavigationForm.Location = new System.Drawing.Point(baseForm.Location.X + 32, baseForm.Location.Y + 32);

            _legendNavigationForm.SetDefaultLegendSize();

            btnShowLegendForm.BackColor = Color.Orange;

           // _legendNavigationForm.WindowState = FormWindowState.Normal;
        }

        public void SetupFormForDesignState()
        {
            if (_legendNavigationForm is null)
            {
                return;
            }

            _legendNavigationForm.SetupFormForDesignState();
        }

        private void _legendNavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _legendNavigationForm = null;

            btnShowLegendForm.BackColor = SystemColors.ControlLight;
        }

        public void SetShowLegendText(string legendText)
        {
            if (_legendNavigationForm != null)
            {
                _legendNavigationForm.BtnShowLegend.Text = legendText;
            }
        }

        public bool IsActive()
        {
            if (_legendNavigationForm == null)
            {
                return false;
            }

            return true;
        }
    }
}

