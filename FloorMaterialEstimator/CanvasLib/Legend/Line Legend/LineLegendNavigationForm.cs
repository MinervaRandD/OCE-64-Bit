

using Utilities;

namespace CanvasLib.Legend
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;
    using Globals;
    using System.Runtime.InteropServices;

    public partial class LineLegendNavigationForm : Form
    {
        public bool IsActive = false;

        private LineModeLegend lineModeLegend;

        public Label Title = null;

        public Panel TitleBar = null;

        public Label CloseButton = null;

        private LegendController legendController;

        public bool LocateToClick
        {
            get
            {
                return SystemGlobals.LineLegendLocateToClick;
            }

            set
            {
                if (value == SystemGlobals.LineLegendLocateToClick)
                {
                    return;
                }

                SystemGlobals.LineLegendLocateToClick = value;
            }
        }

        [DllImport("user32.dll")] public static extern bool ReleaseCapture();

        [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public IntPtr HTCAPTION = (IntPtr)0x2;

        public LineLegendNavigationForm()
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

            this.txbNotes.Text = SystemGlobals.LineLegendNotes;

            ckbLocateOnClick_CheckedChanged(null, null);

            this.ckbLocateOnClick.Checked = SystemGlobals.AreaLegendLocateToClick;

            this.CkbShowLegendInAreaMode.Checked = SystemGlobals.ShowLineLegendInAreaMode;

            this.CkbShowLegendInLineMode.Checked = SystemGlobals.ShowLineLegendInLineMode;

            this.CkbIncludeLines.Checked = SystemGlobals.ShowLineLegendLines;

            this.CkbIncludeNotes.Checked = SystemGlobals.ShowAreaLegendNotes;

            this.CkbShowLegendInAreaMode.CheckedChanged += CkbShowLegendInAreaMode_CheckedChanged;

            this.CkbShowLegendInLineMode.CheckedChanged += CkbShowLegendInLineMode_CheckedChanged;

            this.CkbIncludeLines.CheckedChanged += new System.EventHandler(this.CkbIncludeLines_CheckedChanged);

            this.CkbIncludeNotes.CheckedChanged += new System.EventHandler(this.CkbIncludeNotes_CheckedChanged);

            this.txbNotes.TextChanged += new System.EventHandler(this.txbNotes_TextChanged);

            this.trbLegendSize.MouseUp += TrbLegendSize_MouseUp;
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
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, IntPtr.Zero);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
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
                lineModeLegend.LegendShowLocation = LegendLocation.OnClick;
            }

            else
            {
                lineModeLegend.LegendShowLocation = LegendLocation.NotSet;
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

            if (scale == SystemGlobals.LineLegendScale)
            {
                return;
            }

            SystemGlobals.LineLegendScale = scale;

            lineModeLegend.Init(true);
        }

        private void btnUpdateNotes_Click(object sender, EventArgs e)
        {
            legendController.LineModeLegend.Init();
        }

        private void btnSaveLegendSizeAsDefault_Click(object sender, EventArgs e)
        {
            SystemGlobals.DefaultLineModeLegendSize =
                (double)this.trbLegendSize.Value / (double)this.trbLegendSize.Maximum;
            
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

        private void btnShowFilters_Click(object sender, EventArgs e)
        {
            SystemGlobals.BtnFilterLinesClick();

        }

        public void Delete()
        {
            if (Utilities.WindowUtils.IsFormOpen<LineLegendNavigationForm>())
            {
                this.Close();
            }
        }

        private void CkbShowLegendInLineMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowLineLegendInLineMode = CkbShowLegendInLineMode.Checked;

            if (SystemGlobals.ShowLineLegendInLineMode && lineModeLegend.Shape is null)
            {
                lineModeLegend.Init();
            }
            
            if (SystemState.DesignState == DesignState.Line)
            {
                legendController.SetLineModeLegendLayerVisibility(SystemGlobals.ShowLineLegendInLineMode);
            }
        }

        private void CkbShowLegendInAreaMode_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowLineLegendInAreaMode = CkbShowLegendInAreaMode.Checked;

            if (SystemGlobals.ShowLineLegendInAreaMode && lineModeLegend.Shape is null)
            {
                lineModeLegend.Init();
            }

            if (SystemState.DesignState == DesignState.Area)
            {
                legendController.SetLineModeLegendLayerVisibility(SystemGlobals.ShowLineLegendInAreaMode);
            }
        }

        private void CkbIncludeLines_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowLineLegendLines = CkbIncludeLines.Checked;

            this.legendController.LineModeLegend.Init();
        }

        private void CkbIncludeNotes_CheckedChanged(object sender, EventArgs e)
        {
            SystemGlobals.ShowLineLegendNotes = CkbIncludeNotes.Checked;

            this.legendController.LineModeLegend.Init();
        }
        private void txbNotes_TextChanged(object sender, EventArgs e)
        {
            if (txbNotes.Text.Trim().Length <= 0)
            {
                SystemGlobals.LineLegendNotes = null;

                return;
            }

            SystemGlobals.LineLegendNotes = txbNotes.Text.Trim();
        }
    }
}
