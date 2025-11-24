

using Utilities;

namespace CanvasLib.Legend
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;
    using Globals;
    using System.Runtime.InteropServices;

    public partial class AreaModeLegendNavigationFormBkup2 : Form
    {
        public bool IsActive = false;

        private AreaModeLegend areaModeLegend;

        public Label Title = null;

        public Panel TitleBar = null;

        public Label CloseButton = null;

        private LegendController legendController;

        [DllImport("user32.dll")] public static extern bool ReleaseCapture();

        [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        public AreaModeLegendNavigationFormBkup2()
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

            SetupFormForDesignState();

            this.FormClosed += LegendNavigationForm_FormClosed;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;

            SetDefaultLegendSize();

            this.ckbLocateOnClick.Checked = true;

            ckbLocateOnClick_CheckedChanged(null, null);

            this.CkbShowLegendInAreaMode.Checked = SystemGlobals.ShowAreaLegendInAreaMode;

            this.CkbShowLegendInLineMode.Checked = SystemGlobals.ShowAreaLegendInLineMode;

            this.CkbShowLegendInAreaMode.CheckedChanged += CkbShowLegendInAreaMode_CheckedChanged;

            this.CkbShowLegendInLineMode.CheckedChanged += CkbShowLegendInLineMode_CheckedChanged;
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
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void SetDefaultLegendSize()
        {
            DesignState designState = SystemState.DesignState;

            double size;

        
            this.trbLegendSize.Value = (int)SystemGlobals.CurrentAreaModeLegendSize;

            size = (double) SystemGlobals.CurrentAreaModeLegendSize / (double)trbLegendSize.Maximum;

            areaModeLegend.Setlocation(size);
        

        }

        private void SystemState_DesignStateChanged(DesignState previousDesignState, DesignState currentDesignState)
        {
            SetupFormForDesignState();
        }

        public void Reset(AreaModeLegend areaModeLegend, LineModeLegend lineModeLegend)
        {
            this.areaModeLegend = areaModeLegend;

        }

        private void LegendNavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemState.DesignStateChanged -= SystemState_DesignStateChanged;

            //areaModeLegend.LegendShowLocation = LegendLocation.NotSet;
            //lineModeLegend.LegendShowLocation = LegendLocation.NotSet;
        }

        public void SetupFormForDesignState()
        {
            this.ckbLocateOnClick.Checked = areaModeLegend.LegendShowLocation == LegendLocation.OnClick;

            this.txbNotes.Text = areaModeLegend.Notes;

            this.trbLegendSize.Value = Math.Min(this.trbLegendSize.Maximum, Math.Max(this.trbLegendSize.Minimum, SystemGlobals.CurrentAreaModeLegendSize));

            double size = (double)SystemGlobals.CurrentAreaModeLegendSize / (double)trbLegendSize.Maximum;

            areaModeLegend.Setlocation(2.0 * size);
            // this.trbLegendSize.Value = Math.Min(this.trbLegendSize.Maximum, (int)Math.Floor(areaModeLegend.CurrentSize * ((double) this.trbLegendSize.Maximum)));
            

        }

        private void btnShowLeft_Click(object sender, EventArgs e)
        {
            if (!AreaModeLegendIsSelectedToShow())
            {
                return;
            }

            areaModeLegend.ShowLeft();

            areaModeLegend.LegendShowLocation = LegendLocation.Left;
        }

        private void btnShowRight_Click(object sender, EventArgs e)
        {
            if (!AreaModeLegendIsSelectedToShow())
            {
                return;
            }

            areaModeLegend.ShowRight();

            areaModeLegend.LegendShowLocation = LegendLocation.Right;
            
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

            areaModeLegend.LocateToClick = ckbLocateOnClick.Checked;
        }

        private void trbLegendSize_Scroll(object sender, EventArgs e)
        {
            double size = (double)trbLegendSize.Value / (double) trbLegendSize.Maximum;

            areaModeLegend.Setlocation(2.0 * size);

            SystemGlobals.CurrentAreaModeLegendSize = trbLegendSize.Value;
            

        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {
            areaModeLegend.NotesChanged(txbNotes.Text);
           
        }

        private void btnSaveLegendSizeAsDefault_Click(object sender, EventArgs e)
        {
            SystemGlobals.DefaultAreaModeLegendSize = this.trbLegendSize.Value;
            
            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement rootElement = doc.CreateElement(string.Empty, "DefaultLegendSize", string.Empty);
            doc.AppendChild(rootElement);

            XmlElement defaultAreaModeLegendSize = doc.CreateElement(string.Empty, "DefaultAreaModeLegendSize", string.Empty);
            XmlElement defaultLineModeLegendSize = doc.CreateElement(string.Empty, "DefaultLineModeLegendSize", string.Empty);

            defaultAreaModeLegendSize.InnerText = SystemGlobals.DefaultAreaModeLegendSize.ToString();
            
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

        internal void ResetCurrentLegendSizes()
        {
            SystemGlobals.CurrentAreaModeLegendSize = SystemGlobals.DefaultAreaModeLegendSize;
            SystemGlobals.CurrentLineModeLegendSize = SystemGlobals.DefaultLineModeLegendSize;
        }

        private void btnShowFilters_Click(object sender, EventArgs e)
        {
            SystemGlobals.BtnFilterAreasClick();

        }

        public void Delete()
        {
            if (Utilities.WindowUtils.IsFormOpen<AreaModeLegendNavigationFormBkup2>())
            {
                this.Close();
            }
        }

        private void CkbShowLegendInAreaMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendInAreaMode = CkbShowLegendInAreaMode.Checked;

            if (SystemState.DesignState == DesignState.Area)
            {
                legendController.SetAreaModeLegendLayerVisibility(SystemGlobals.ShowAreaLegendInAreaMode);
            }
        }

        private void CkbShowLegendInLineMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowAreaLegendInLineMode = CkbShowLegendInLineMode.Checked;

            if (SystemState.DesignState == DesignState.Line)
            {
                legendController.SetAreaModeLegendLayerVisibility(SystemGlobals.ShowAreaLegendInLineMode);
            }
        }
    }
}
