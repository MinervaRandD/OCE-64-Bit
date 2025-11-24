using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CanvasLib.SeamStateAreaLockIcon;

namespace TestDriverLineDrawouts
{
    using Visio = Microsoft.Office.Interop.Visio;
    using Graphics;

    public partial class TestDriverLineDrawoutForm : Form
    {

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public GraphicsPage page { get; set; }

        public GraphicsWindow window { get; set; }

        GraphicShape shape = null;

        public TestDriverLineDrawoutForm()
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

            //shape = Page.DrawRectangle(1, 1, 11, 11);

            //panel1.BackColor = Color.FromArgb(0, 0, 0, 0);

            setSize();

            SeamStateAreaLockIcon icon = new SeamStateAreaLockIcon(page, window);

            icon.Draw(6, 6);

            this.SizeChanged += TestDriverLineDrawoutForm_SizeChanged;
            this.Load += TestDriverLineDrawoutForm_Load;

            //this.MouseDown += TestDriverLineDrawoutForm_MouseDown;
            //this.MouseUp += TestDriverLineDrawoutForm_MouseUp;

            //this.Paint += TestDriverLineDrawoutForm_Paint;
        }

        private void TestDriverLineDrawoutForm_Paint(object sender, PaintEventArgs e)
        {
           // Pen pen = new Pen(Color.FromArgb(255, 255, 0, 0));
           //// System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
           // e.Graphics.DrawLine(pen, 10, 10, this.Width - 10, this.Height - 10);

            
        }

        private void TestDriverLineDrawoutForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.ClientRectangle.Width;
            int formSizeY = this.ClientRectangle.Height;

            int cntlSizeX = formSizeX - 96;
            int cntlSizeY = formSizeY - 96;

            int cntlLocnX = 48;
            int cntlLocnY = 48;

            axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        Bitmap bitmap;

        private void TestDriverLineDrawoutForm_Load(object sender, EventArgs e)
        {
            //bitmap = new Bitmap(this.ClientSize.Width,
            //this.ClientSize.Height,
            //System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        }

        Point p1 = new Point(10,10);
        Point p2 = new Point(50,50);
        Pen pen = new Pen(Color.Magenta, 10);

        private void TestDriverLineDrawoutForm_MouseDown(object sender, MouseEventArgs e)
        {
        //    if (e.Button == MouseButtons.Left)
        //        p1 = e.Location;
        }

        private void TestDriverLineDrawoutForm_MouseUp(object sender, MouseEventArgs e)
        {
            //Pen pen = new Pen(Color.FromArgb(255, 255, 0, 0));
            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //g.DrawLine(pen, 20, 10, 300, 100);
            //if (e.Button == MouseButtons.Left)
            //{
            //    p2 = e.Location;
            //
            //    g.DrawLine(pen, p1, p2);
            //    this.Invalidate();
            //    g.Dispose();
            //}
        }

    }
}
