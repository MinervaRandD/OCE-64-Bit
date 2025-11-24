using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestDriverCursors
{
    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using System.Runtime.InteropServices;
    using System.Windows.Input;
    public partial class CursorTestForm : Form, IMessageFilter
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        private Rectangle AxEffectiveAreaBounds;

        private System.Windows.Forms.Cursor testCursor;

        private Visio.Shape horzLine;

        public CursorTestForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.axDrawingControl.NegotiateMenus = true;
            this.axDrawingControl.NegotiateToolbars = true;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Masters masters = this.VsoDocument.Masters;

            foreach (var x in masters)
            {
                MessageBox.Show(x.ToString());
            }
            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 1;

            Visio.Application app = VsoDocument.Application;

            horzLine = VsoPage.DrawLine(0, 0, 100, 0);

            app.ScreenUpdating = 0;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Cross;

            AxEffectiveAreaBounds = new Rectangle(this.axDrawingControl.Location, new Size(this.axDrawingControl.Size.Width, this.axDrawingControl.Height - 36));

            VsoWindow.MouseMove += VsoWindow_MouseMove;

            this.Load += CursorTestForm_Load;
        }


        private void CursorTestForm_Load(object sender, EventArgs e)
        {
            // Fit the background image.
            this.ClientSize = this.axDrawingControl.Size;

            // Draw the cursor image.
            const int wid = 256;
            const int hgt = 256;
            Bitmap bm = new Bitmap(wid, hgt);
            using (Graphics gr = Graphics.FromImage(bm))
            //using (Graphics gr = pictureBox1.CreateGraphics())
            {
                gr.Clear(Color.Transparent);

                int cx = wid / 2;
                int cy = hgt / 2;

                Point p1 = new Point(cx, 0);
                Point p2 = new Point(cx, hgt);
                Point p3 = new Point(0, cy);
                Point p4 = new Point(wid, cy);
                    //new Point(2 * cx, cy),
                    //new Point(cx, 2 * cy),
                    //new Point(0, cy),

                //using (SolidBrush br =
                //    new SolidBrush(Color.FromArgb(128, 255, 255, 0)))
                //{
                //    gr.FillPolygon(br, outer_points);
                //}
                gr.DrawLine(Pens.Red, p1, p2);
                gr.DrawLine(Pens.Red, p3, p4);

            }

            // Turn the bitmap into a cursor.
            testCursor = new System.Windows.Forms.Cursor(bm.GetHicon());
        }
        private void VsoWindow_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {

            Visio.Application app = VsoDocument.Application;


            app.ScreenUpdating = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Cross;

            horzLine.SetCenter(0, y);
            System.Windows.Forms.Cursor.Current = testCursor;
        }

        private void VsoWindow_WindowActivated(Visio.Window Window)
        {
            
        }

        //private void VsoWindow_MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        //{
        //    this.Cursor = Cursors.Default;
        //}

        //private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        //{
        //    this.Cursor = Cursors.Cross;
        //    CancelDefault = true;
        //}


        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {

            bool rtrnCode = false;

            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
            {
                return SetCursorForCurrentLocation();
            }

            if (m.Msg == (int) WindowsMessage.WM_MOUSEHOVER)
            {
                return SetCursorForCurrentLocation();
            }

            return rtrnCode;
        }

        public bool SetCursorForCurrentLocation()
        {
            Mouse.SetCursor(System.Windows.Input.Cursors.Wait);

            //if (AxEffectiveAreaBounds.Contains(PointToClient()
            //{
            //    //if (Cursor != Cursors.Cross)
            //    //{
            //        Mouse.SetCursor(Cursors.Cross);


            //        this.lblCursorType.Text = "Cross";
            //        this.sssCursorTest.Refresh();

            //        return false;
            //    //}
            //}

            //else
            //{
            //    //if (Cursor != Cursors.Default)
            //    //{
            //    Mouse.SetCursor(Cursors.Arrow);
            //    this.lblCursorType.Text = "Arrow";
            //        this.sssCursorTest.Refresh();

            //        return false;
            //    //}
            //}

            return false;
        }

        
    }
}
