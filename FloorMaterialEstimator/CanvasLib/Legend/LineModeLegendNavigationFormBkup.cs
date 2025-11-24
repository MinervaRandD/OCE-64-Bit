

using Utilities;

namespace CanvasLib.Legend
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;
    using Globals;
    using System.Runtime.InteropServices;

    public partial class LineModeLegendNavigationFormBkup : Form
    {
        public bool IsActive = false;

        private LineModeLegend lineModeLegend;

        public Label Title = null;

        public Panel TitleBar = null;

        public Label CloseButton = null;

        private LegendController legendController;

        [DllImport("user32.dll")] public static extern bool ReleaseCapture();

        [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        public LineModeLegendNavigationFormBkup()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            Title = new Label
            {
                Text = "Line Mode Legend", 
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

            this.Activated += LineModeLegendNavigationForm_Activated;

            this.Deactivate += LineModeLegendNavigationForm_Deactivate;


        }

        public void Init(LegendController legendController, LineModeLegend lineModeLegend)
        {
            this.legendController = legendController;

            this.lineModeLegend = lineModeLegend;

            SetupFormForDesignState();

            this.FormClosed += LegendNavigationForm_FormClosed;

            SystemState.DesignStateChanged += SystemState_DesignStateChanged;

            SetDefaultLegendSize();

            this.ckbLocateOnClick.Checked = true;

            ckbLocateOnClick_CheckedChanged(null, null);

            this.CkbShowLegendInLineMode.Checked = SystemGlobals.ShowLineLegendInLineMode;

            this.CkbShowLegendInLineMode.Checked = SystemGlobals.ShowLineLegendInLineMode;

            this.CkbShowLegendInAreaMode.CheckedChanged += CkbShowLegendInAreaMode_CheckedChanged;

            this.CkbShowLegendInLineMode.CheckedChanged += CkbShowLegendInLineMode_CheckedChanged;
        }

        private void LineModeLegendNavigationForm_Deactivate(object sender, EventArgs e)
        {
            this.TitleBar.BackColor = SystemColors.ControlLight;

        }

        private void LineModeLegendNavigationForm_Activated(object sender, EventArgs e)
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

        
            this.trbLegendSize.Value = (int)SystemGlobals.CurrentLineModeLegendSize;

            size = (double) SystemGlobals.CurrentLineModeLegendSize / (double)trbLegendSize.Maximum;

            lineModeLegend.Setlocation(size);
        

        }

        private void SystemState_DesignStateChanged(DesignState previousDesignState, DesignState currentDesignState)
        {
            SetupFormForDesignState();
        }

        private void LegendNavigationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemState.DesignStateChanged -= SystemState_DesignStateChanged;

            //lineModeLegend.LegendShowLocation = LegendLocation.NotSet;
            //lineModeLegend.LegendShowLocation = LegendLocation.NotSet;
        }

        public void SetupFormForDesignState()
        {
            this.ckbLocateOnClick.Checked = lineModeLegend.LegendShowLocation == LegendLocation.OnClick;

            this.txbNotes.Text = lineModeLegend.Notes;

            this.trbLegendSize.Value = Math.Min(this.trbLegendSize.Maximum, Math.Max(this.trbLegendSize.Minimum, SystemGlobals.CurrentLineModeLegendSize));

            double size = (double)SystemGlobals.CurrentLineModeLegendSize / (double)trbLegendSize.Maximum;

            //lineModeLegend.Setlocation(2.0 * size);
            // this.trbLegendSize.Value = Math.Min(this.trbLegendSize.Maximum, (int)Math.Floor(lineModeLegend.CurrentSize * ((double) this.trbLegendSize.Maximum)));
            

        }

        private void btnShowLeft_Click(object sender, EventArgs e)
        {
            if (!LineModeLegendIsSelectedToShow())
            {
                return;
            }

            lineModeLegend.ShowLeft();

            lineModeLegend.LegendShowLocation = LegendLocation.Left;

            this.Activate();
        }

        private void btnShowRight_Click(object sender, EventArgs e)
        {
            if (!LineModeLegendIsSelectedToShow())
            {
                return;
            }

            lineModeLegend.ShowRight();

            lineModeLegend.LegendShowLocation = LegendLocation.Right;

            this.Activate();

        }

        private bool LineModeLegendIsSelectedToShow()
        {
            if (SystemState.DesignState == DesignState.Line)
            {
                return this.CkbShowLegendInLineMode.Checked;
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
                lineModeLegend.LegendShowLocation = LegendLocation.OnClick;
            }

            else
            {
                lineModeLegend.LegendShowLocation = LegendLocation.NotSet;
            }

            lineModeLegend.LocateToClick = ckbLocateOnClick.Checked;
        }

        private void trbLegendSize_Scroll(object sender, EventArgs e)
        {
            double size = (double)trbLegendSize.Value / (double) trbLegendSize.Maximum;

            lineModeLegend.Setlocation(2.0 * size);

            SystemGlobals.CurrentLineModeLegendSize = trbLegendSize.Value;
            

        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {
            lineModeLegend.NotesChanged(txbNotes.Text);
           
        }

        private void btnSaveLegendSizeAsDefault_Click(object sender, EventArgs e)
        {
            SystemGlobals.DefaultLineModeLegendSize = this.trbLegendSize.Value;
            
            XmlDocument doc = new XmlDocument();

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement rootElement = doc.CreateElement(string.Empty, "DefaultLegendSize", string.Empty);
            doc.AppendChild(rootElement);

            XmlElement defaultLineModeLegendSize = doc.CreateElement(string.Empty, "DefaultLineModeLegendSize", string.Empty);

            defaultLineModeLegendSize.InnerText = SystemGlobals.DefaultLineModeLegendSize.ToString();
            
            rootElement.AppendChild(defaultLineModeLegendSize);
          
            try
            {
                doc.Save(@"C:\OCEOperatingData\Defaults\LineLegendSize.xml");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to save default legend size: " + ex.Message);
            }
        }

        internal void ResetCurrentLegendSizes()
        {
            SystemGlobals.CurrentLineModeLegendSize = SystemGlobals.DefaultLineModeLegendSize;
            SystemGlobals.CurrentLineModeLegendSize = SystemGlobals.DefaultLineModeLegendSize;
        }

        private void btnShowFilters_Click(object sender, EventArgs e)
        {
            SystemGlobals.BtnFilterLinesClick();

        }

        public void Delete()
        {
            if (Utilities.WindowUtils.IsFormOpen<LineModeLegendNavigationForm>())
            {
                this.Close();
            }
        }

        private void CkbShowLegendInLineMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowLineLegendInLineMode = CkbShowLegendInLineMode.Checked;

            if (SystemState.DesignState == DesignState.Line)
            {
                legendController.SetLineModeLegendLayerVisibility(SystemGlobals.ShowLineLegendInLineMode);
            }
        }

        private void CkbShowLegendInAreaMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowLineLegendInAreaMode = CkbShowLegendInAreaMode.Checked;

            if (SystemState.DesignState == DesignState.Area)
            {
                legendController.SetLineModeLegendLayerVisibility(SystemGlobals.ShowLineLegendInAreaMode);
            }
        }
    }
}
