using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visio = Microsoft.Office.Interop.Visio;
using System.Configuration;
using SettingsLib;
using CanvasLib.Markers_and_Guides;
using FloorMaterialEstimator.Finish_Controls;
using CanvasLib.Legend;
using FinishesLib;
using System.Windows.Forms.Design;
using FloorMaterialEstimator.CanvasManager;

namespace FloorMaterialEstimator
{
    public partial class UCDrawColor : UserControl
    {
        const double COLOR_DISTANCE_THRESHOLD = 25;
        FloorMaterialEstimatorBaseForm BaseForm = (FloorMaterialEstimatorBaseForm)Application.OpenForms["FloorMaterialEstimatorBaseForm"];
        Dictionary<Color, Color> colorMap = new Dictionary<Color, Color>();
        public UCDrawColor()
        {
            InitializeComponent();
        }

        private void UCDrawColor_Load(object sender, EventArgs e)
        {
            lstWeights.SelectedIndex = 0;
            MapPanelColorsToMeasureStickColors();

            RadioButton rbSelected = FindSelectedRadioButton();
            UpdateControlsState(rbSelected);
        }

        private RadioButton FindSelectedRadioButton() 
        {
            RadioButton[] radioButtons = { rbMeasuringStick, rdCrosshairs, rdPerimeters, rdFieldGuides, rdCrosshairs };
            RadioButton rbSelected = null;
            foreach (RadioButton rb in radioButtons)
            {
                if (rb.Checked)
                {
                    UpdateControlsState(rb);
                    rbSelected = rb;
                    break;
                }
            }
            return rbSelected;
        }

        private void UpdateControlsState(RadioButton rbSelected) 
        {
            bool selectedMeasuringStick = rbSelected == this.rbMeasuringStick;

            this.lstWeights.Enabled = !selectedMeasuringStick;
            UpdatePaletteState(selectedMeasuringStick);
        }

        static private double CalculateColorDistance(Color c1, Color c2) 
        {
            return Math.Sqrt(Math.Pow(c2.R - c1.R, 2) + Math.Pow(c2.G - c1.G, 2) + Math.Pow(c2.B - c1.B, 2));
        }

        private void MapPanelColorsToMeasureStickColors()
        {
            Panel[] colorPanels = { panelBlack, panelBlue, panelBrick, panelGrass, panelGreen, panelMagenta, panelOrange, panelRed, panelSky, panelYellow };

            foreach (Color measuringStickStencilColor in BaseForm.MeasuringStickStencilColors)
            {
                foreach (Panel panel in colorPanels)
                {
                    double colorDistance = CalculateColorDistance(measuringStickStencilColor, panel.BackColor);
                    if (colorDistance < COLOR_DISTANCE_THRESHOLD)
                    {
                        this.colorMap.Add(panel.BackColor, measuringStickStencilColor);
                    }
                }
            }
        }

        private void UpdatePaletteState(bool selectedMeasuringStick)
        {
            Panel[] colorPanels = { panelBlack, panelBlue, panelBrick, panelGrass, panelGreen, panelMagenta, panelOrange, panelRed, panelSky, panelYellow };

            if (selectedMeasuringStick)
            {
                foreach (Panel panel in colorPanels)
                {
                    if (this.colorMap.ContainsKey(panel.BackColor))
                    {
                        panel.Enabled = true;
                    }
                    else
                    {
                        panel.Enabled = false;
                    }
                    panel.Invalidate();
                }
            }
            else
            {
                foreach (Panel panel in colorPanels)
                {
                    panel.Enabled = true;
                }
            }
        }


        private void DrawColorBox(System.Drawing.Graphics graphics, Rectangle rcColorBox, Color fillColor, bool isSelected) 
        {
            if (isSelected)
            {
                graphics.FillRectangle(new SolidBrush(fillColor), rcColorBox);
                graphics.DrawRectangle(new Pen(Color.DarkGray, 1), rcColorBox);
            }
            else 
            {
                graphics.FillRectangle(new SolidBrush(fillColor), rcColorBox);
            }
        }

        private void DrawColorBox(System.Drawing.Graphics graphics, Panel panel) 
        {
            if (panel.Enabled)
            {
                bool isColorSelected = IsColorSelected(panel);
                DrawColorBox(graphics, new Rectangle(new Point(0, 0), new Size(panel.Width, panel.Height)), panel.BackColor, isColorSelected);
            }
            else
            {
                DrawColorBox(graphics, new Rectangle(new Point(0, 0), new Size(panel.Width, panel.Height)), Color.LightGray, false);
            }
        }

        private bool IsColorSelected(Panel panel) 
        {
            bool isSelected = false;
            
            if (this.rbMeasuringStick.Checked)
            {
                isSelected = this.colorMap.ContainsKey(panel.BackColor);
            }

            return isSelected;
        }

        public void pnlColor_Click(object sender, EventArgs e)
        {
            try
            {
                Panel clickedPanel = sender as Panel;
                if (rbMeasuringStick.Checked) 
                {
                    BaseForm.SelectMeasuringStickStencil(this.colorMap[clickedPanel.BackColor]);
                    return;
                }

                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                string rgb, size;
                Color cl = clickedPanel.BackColor;
                 rgb = cl.R + "," + cl.G + "," + cl.B;
                size = (Convert.ToDecimal(lstWeights.SelectedItem.ToString().Replace(" pt", "")) / 20).ToString();

                if (rdFieldGuides.Checked)
                {
                    FieldGuideEditForm FormFieldGuides = new FieldGuideEditForm(BaseForm, BaseForm.CanvasManager.FieldGuideController, GlobalSettings.ShowFieldGuideEditFormAsModal);
                    FormFieldGuides.setGuidecolor(cl, (Convert.ToDouble(size) * 20) * 2);
                }
                else if (rdCursorGuides.Checked)
                {
                    Graphics.VisioInterop.color_size = rgb + "|" + size;
                    config.AppSettings.Settings["CursorGuides"].Value = rgb + "|" + size;
                  
                }
              
                else if(rdPerimeters.Checked)
                {
                    LineFinishesEditForm FormLineFinishes = new LineFinishesEditForm(BaseForm, BaseForm.linePalette, BaseForm.LineFinishBaseList, BaseForm.ZeroLineBase, GlobalSettings.ShowLineEditFormAsModal);

                    FormLineFinishes.setLineColor(cl, (Convert.ToDouble(size) * 20) * 2);

                }
                config.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
            }
        }

        public void radioButtons_CheckedChanged(object sender, EventArgs e) 
        {
            UpdateControlsState(sender as RadioButton);
        }

        private void panelBrick_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            DrawColorBox(e.Graphics, panel);
        }
    }


    [System.ComponentModel.DesignerCategory("code")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ContextMenuStrip | ToolStripItemDesignerAvailability.MenuStrip)]
    public partial class TrackBarMenuItem : ToolStripControlHost
    {
        public TrackBarMenuItem()
            : base(CreateControlInstance())
        {
        }
        /// <summary>
        /// Create a strongly typed property called TrackBar - handy to prevent casting everywhere.
        /// </summary>
        public TrackBar TrackBar
        {
            get
            {
                return Control as TrackBar;
            }
        }
        /// <summary>
        /// Create the actual control, note this is static so it can be called from the
        /// constructor.
        ///
        /// </summary>
        /// <returns></returns>
        private static Control CreateControlInstance()
        {
            TrackBar t = new TrackBar();
            t.AutoSize = false;
            // Add other initialization code here.
            return t;
        }
        [DefaultValue(0)]
        public int Value
        {
            get { return TrackBar.Value; }
            set { TrackBar.Value = value; }
        }
        /// <summary>
        /// Attach to events we want to re-wrap
        /// </summary>
        /// <param name="control"></param>
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            TrackBar trackBar = control as TrackBar;
            trackBar.ValueChanged += new EventHandler(trackBar_ValueChanged);
        }
        /// <summary>
        /// Detach from events.
        /// </summary>
        /// <param name="control"></param>
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            TrackBar trackBar = control as TrackBar;
            trackBar.ValueChanged -= new EventHandler(trackBar_ValueChanged);
        }
        /// <summary>
        /// Routing for event
        /// TrackBar.ValueChanged -> ToolStripTrackBar.ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void trackBar_ValueChanged(object sender, EventArgs e)
        {
            // when the trackbar value changes, fire an event.
            if (this.ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }
        // add an event that is subscribable from the designer.
        public event EventHandler ValueChanged;
        // set other defaults that are interesting
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 16);
            }
        }
    }
}
