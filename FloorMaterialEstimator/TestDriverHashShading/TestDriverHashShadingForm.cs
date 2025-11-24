using System;
using System.Drawing;
using System.Windows.Forms;
using Graphics;

namespace TestDriverHashShading
{
    using Visio = Microsoft.Office.Interop.Visio;
    
    using System.Runtime.InteropServices;
  
    public partial class TestDriverHashShadingForm : Form
    {
        private TrackBar trbTransparency;
        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public GraphicsPage page { get; set; }

        public GraphicsWindow window { get; set; }

        GraphicShape shape = null;

        public TestDriverHashShadingForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, VsoPage);
            
            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            shape = page.DrawRectangle(1, 1, 11, 11);

            VisioInterop.SetFillPattern(shape, "7", Color.Black);

            setSize();

            this.trbTransparency.ValueChanged += TrbTransparency_ValueChanged;
            this.SizeChanged += TestDriverHashShadingForm_SizeChanged;
        }

        private void TrbTransparency_ValueChanged(object sender, EventArgs e)
        {
            double opacity = 1.0 - ((double)trbTransparency.Value) * 0.01;

            VisioInterop.SetPatternOpacity(shape, opacity);
        }

        private void TestDriverHashShadingForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.ClientRectangle.Width;
            int formSizeY = this.ClientRectangle.Height;

            int cntlSizeX = formSizeX - 48;
            int cntlSizeY = formSizeY - 64;

            int cntlLocnX = 18;
            int cntlLocnY = 12;

            axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        private void InitializeComponent()
        {
            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            this.trbTransparency = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(180, 30);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(695, 510);
            this.axDrawingControl.TabIndex = 7;
            // 
            // trbTransparency
            // 
            this.trbTransparency.Location = new System.Drawing.Point(26, 169);
            this.trbTransparency.Maximum = 100;
            this.trbTransparency.Name = "trbTransparency";
            this.trbTransparency.Size = new System.Drawing.Size(104, 45);
            this.trbTransparency.TabIndex = 8;
            this.trbTransparency.Value = 100;
            // 
            // TestDriverHashShadingForm
            // 
            this.ClientSize = new System.Drawing.Size(906, 577);
            this.Controls.Add(this.trbTransparency);
            this.Controls.Add(this.axDrawingControl);
            this.Name = "TestDriverHashShadingForm";
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
