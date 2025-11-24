using CanvasLib.SeamingTool;
using Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TracerLib;

namespace TestDriverMeasuringStick
{

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class TestDriverSeamingToolForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public SeamingTool3 seamingTool;

        GraphicsWindow Window;

        GraphicsPage Page;

        public TestDriverSeamingToolForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            Tracer.TraceGen = new Tracer((TraceLevel)15, @"C:\Test\TraceLog.txt", true);

            LoadSeamingTool();
        }

        private void LoadSeamingTool()
        {
            //Visio.Shape shape1 = VsoPage.DrawLine(1, 1, 2, 2);

            //shape1.Text = "+";

            //Visio.Shape shape2 = VsoPage.DrawRectangle(3, 3, 4, 4);

            //shape2.CellsU["Width"].ResultIU = 0;

            seamingTool = new SeamingTool3(Window, Page);

            //seamingTool.Show();

            //seamingTool.IsVisible = true;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.seamingTool.Show();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.seamingTool.Hide();
        }
    }
}
