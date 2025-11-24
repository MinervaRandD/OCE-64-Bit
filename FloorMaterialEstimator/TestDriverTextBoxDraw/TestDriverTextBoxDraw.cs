

using System.Windows.Forms;
using Graphics;

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
            //var x = VsoDocument.Application.Settings;

            //x.ShowMoreShapeHandlesOnHover = true;
            
            VsoWindow.MouseDown += VsoWindow_MouseDown;
        }

        Visio.Shape visioShape;

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            
            visioShape = VsoPage.DrawRectangle(x, y, x + 4, y + 2);

            VsoWindow.Application.BeforeShapeDelete += Application_BeforeShapeDelete;

            Graphics.TextBoxEditForm ef = new TextBoxEditForm();

            ef.ShowDialog();

            CancelDefault = false;
        }

        private void Application_BeforeShapeDelete(Visio.Shape Shape)
        {
           
        }

        private void VisioShape_CellChanged(Visio.Cell Cell)
        {
            
        }

        private void VisioShape_ShapeChanged(Visio.Shape Shape)
        {
            
        }

        private void VisioShape_BeforeSelectionDelete(Visio.Selection Selection)
        {
            
        }

        private void VisioShape_BeforeShapeDelete(Visio.Shape Shape)
        {
            
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            visioShape.Delete();
        }
    }
}
