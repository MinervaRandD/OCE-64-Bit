

using Utilities;

namespace CanvasLib.Legend
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;
    using Globals;
    using System.Runtime.InteropServices;

    public partial class AreaLegendNavigationForm : Form
    {
        public bool IsActive = false;

        private AreaModeLegend areaModeLegend;

        public Label Title = null;

        public Panel TitleBar = null;

        public Label CloseButton = null;

        private LegendController legendController;

        public bool LocateToClick
        {
            get
            {
                return SystemGlobals.AreaLegendLocateToClick;
            }

            set
            {
                if (value == SystemGlobals.AreaLegendLocateToClick)
                {
                    return;
                }

                SystemGlobals.AreaLegendLocateToClick = value;
            }
        }

        [DllImport("user32.dll")] public static extern bool ReleaseCapture();

        [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public IntPtr HTCAPTION = (IntPtr) 0x2;

        public AreaLegendNavigationForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            Title = new Label
            {
                Text = "Area Mode Legend", 
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(Font.FontFamily, 10)
            };


            TitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                BackColor = SystemColors.ControlLight

            };

            CloseButton = new Label
            {
                Size = new Size(16, 16)
                , AutoSize = false
               , TextAlign = ContentAlignment.MiddleCenter
               , Text = "X"
               , Dock = DockStyle.Right
            };

            
            TitleBar.Controls.Add(CloseButton);

            TitleBar.Controls.Add(Title);

            this.Controls.Add(TitleBar);

            this.CloseButton.Click += CloseButton_Click;

            this.Title.MouseDown += DragMouseDown;

            this.Activated += AreaModeLegendNavigationForm_Activated;

            this.Deactivate += AreaModeLegendNavigationForm_Deactivate;
        }

        public void Init(LegendController legendController, AreaModeLegend areaModeLegend)
        {
            this.legendController = legendController;

            this.areaModeLegend = areaModeLegend;

            this.txbNotes.Text = SystemGlobals.AreaLegendNotes;

            ckbLocateOnClick_CheckedChanged(null, null);

            this.ckbLocateOnClick.Checked = SystemGlobals.AreaLegendLocateToClick;

            this.CkbShowLegendInAreaMode.Checked = SystemGlobals.ShowAreaLegendInAreaMode;

            this.CkbShowLegendInLineMode.Checked = SystemGlobals.ShowAreaLegendInLineMode;

            this.CkbIncludeFinishes.Checked = SystemGlobals.ShowAreaLegendFinishes;

            this.CkbIncludeCounters.Checked = SystemGlobals.ShowAreaLegendCounters;

            this.CkbIncludeNotes.Checked = SystemGlobals.ShowAreaLegendNotes;

            this.CkbShowLegendInAreaMode.CheckedChanged += CkbShowLegendInAreaMode_CheckedChanged;

            this.CkbShowLegendInLineMode.CheckedChanged += CkbShowLegendInLineMode_CheckedChanged;

            this.CkbIncludeFinishes.CheckedChanged += new System.EventHandler(this.CkbIncludeFinishes_CheckedChanged);

            this.CkbIncludeCounters.CheckedChanged += new System.EventHandler(this.CkbIncludeCounters_CheckedChanged);

            this.CkbIncludeNotes.CheckedChanged += new System.EventHandler(this.CkbIncludeNotes_CheckedChanged);

            this.txbNotes.TextChanged += new System.EventHandler(this.txbNotes_TextChanged);

            this.trbLegendSize.MouseUp += TrbLegendSize_MouseUp;
        }

        private void AreaModeLegendNavigationForm_Deactivate(object sender, EventArgs e)
        {
            this.TitleBar.BackColor = SystemColors.ControlLight;

        }

        private void AreaModeLegendNavigationForm_Activated(object sender, EventArgs e)
        {
            this.TitleBar.BackColor = Color.LightBlue;
            this.IsActive = true;

        }

        private void DragMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, IntPtr.Zero);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowLeft_Click(object sender, EventArgs e)
        {
            if (!AreaModeLegendIsSelectedToShow())
            {
                return;
            }

            areaModeLegend.ShowLeft();

            areaModeLegend.LegendShowLocation = LegendLocation.Left;

            this.Activate();
        }

        private void btnShowRight_Click(object sender, EventArgs e)
        {
            if (!AreaModeLegendIsSelectedToShow())
            {
                return;
            }

            areaModeLegend.ShowRight();

            areaModeLegend.LegendShowLocation = LegendLocation.Right;

            this.Activate();

        }

        private bool AreaModeLegendIsSelectedToShow()
        {
            if (SystemState.DesignState == DesignState.Area)
            {
                return this.CkbShowLegendInAreaMode.Checked;
            }

            if (SystemState.DesignState == DesignState.Line)
            {
                return this.CkbShowLegendInLineMode.Checked;
            }

            return false;
        }
            
        private void ckbLocateOnClick_CheckedChanged(object sender, EventArgs e)
        {
           
            if (ckbLocateOnClick.Checked)
            {
                areaModeLegend.LegendShowLocation = LegendLocation.OnClick;
            }

            else
            {
                areaModeLegend.LegendShowLocation = LegendLocation.NotSet;
            }

            this.LocateToClick = ckbLocateOnClick.Checked;
        }

        private void trbLegendSize_Scroll(object sender, EventArgs e)
        {
            int legendSizeValue = this.trbLegendSize.Value;

            this.LblLegendSize.Text = legendSizeValue.ToString() + '%';
        }

        private void TrbLegendSize_MouseUp(object sender, MouseEventArgs e)
        {

            double scale = (double)trbLegendSize.Value / (double)trbLegendSize.Maximum;

            if (scale == SystemGlobals.AreaLegendScale)
            {
                return;
            }

            SystemGlobals.AreaLegendScale = scale;

            areaModeLegend.Init(true);
        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {
            legendController.AreaModeLegend.Init();
        }

        private void btnSaveLegendSizeAsDefault_Click(object sender, EventArgs e)
        {
            SystemGlobals.DefaultAreaLegendScale =
                (double)this.trbLegendSize.Value / (double)this.trbLegendSize.Maximum;
            
            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement rootElement = doc.CreateElement(string.Empty, "DefaultLegendSize", string.Empty);
            doc.AppendChild(rootElement);

            XmlElement defaultAreaModeLegendSize = doc.CreateElement(string.Empty, "DefaultAreaModeLegendSize", string.Empty);

            defaultAreaModeLegendSize.InnerText = SystemGlobals.DefaultAreaLegendScale.ToString();
            
            rootElement.AppendChild(defaultAreaModeLegendSize);
          
            try
            {
                doc.Save(@"C:\OCEOperatingData\Defaults\AreaLegendSize.xml");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to save default legend size: " + ex.Message);
            }
        }

        private void btnShowFilters_Click(object sender, EventArgs e)
        {
            SystemGlobals.BtnFilterAreasClick();

        }

        public void Delete()
        {
            if (Utilities.WindowUtils.IsFormOpen<AreaLegendNavigationForm>())
            {
                this.Close();
            }
        }

        private void CkbShowLegendInAreaMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendInAreaMode = CkbShowLegendInAreaMode.Checked;

            if (SystemGlobals.ShowAreaLegendInAreaMode && areaModeLegend.Shape is null)
            {
                areaModeLegend.Init();
            }
           
            if (SystemState.DesignState == DesignState.Area)
            {
                legendController.SetAreaModeLegendLayerVisibility(SystemGlobals.ShowAreaLegendInAreaMode);
            }
        }


        private void CkbShowLegendInLineMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendInLineMode = CkbShowLegendInLineMode.Checked;

            if (SystemGlobals.ShowAreaLegendInLineMode && areaModeLegend.Shape is null)
            {
                areaModeLegend.Init();
            }

            if (SystemState.DesignState == DesignState.Line)
            {
                legendController.SetAreaModeLegendLayerVisibility(SystemGlobals.ShowAreaLegendInLineMode);
            }
        }

        private void CkbIncludeFinishes_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendFinishes = CkbIncludeFinishes.Checked;

            this.legendController.AreaModeLegend.Init();
        }

        private void CkbIncludeCounters_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendCounters = CkbIncludeCounters.Checked;

            this.legendController.AreaModeLegend.Init();
        }
        private void CkbIncludeNotes_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendNotes = CkbIncludeNotes.Checked;

            this.legendController.AreaModeLegend.Init();
        }

        private void txbNotes_TextChanged(object sender, EventArgs e)
        {
            if (txbNotes.Text.Trim().Length <= 0)
            {
                SystemGlobals.AreaLegendNotes = null;

                return;
            }

            SystemGlobals.AreaLegendNotes = txbNotes.Text.Trim();
        }
    }
}
