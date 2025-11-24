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

namespace TestDriverTextBoxDraw
{

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class TestDriverTextBoxDraw : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        GraphicsWindow Window;

        GraphicsPage Page;

        public TestDriverTextBoxDraw()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            Window = new GraphicsWindow(VsoWindow);

            Page = new GraphicsPage(Window, VsoPage);

            var x = VsoDocument.Application.Settings;

            x.ShowMoreShapeHandlesOnHover = true;

           VsoWindow.MouseDown += VsoWindow_MouseDown;
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            Page.DrawRectangle(x, y, x + 4, y + 2);
        }
    }
}
