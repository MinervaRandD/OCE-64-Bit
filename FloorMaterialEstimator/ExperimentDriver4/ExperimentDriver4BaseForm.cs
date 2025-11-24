

namespace ExperimentDriver4
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using AxMicrosoft.Office.Interop.VisOcx;
    
    using Visio = Microsoft.Office.Interop.Visio;

    public partial class ExperimentDriver4BaseForm : Form
    {
        public static bool debug;

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Visio.Shape shape1;
        public Visio.Shape shape2;

        public Visio.Layer workLayer;
        public Visio.Layer mainLayer;

        public ExperimentDriver4BaseForm()
        {
            InitializeComponent();

            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            this.VsoPage = pages[1];

            VsoPage.PageSheet.CellsU["PageHeight"].ResultIU = 16;
            VsoPage.PageSheet.CellsU["PageWidth"].ResultIU = 20;

            VsoWindow.ShowGrid = 1;

            VsoWindow.ShowRulers = 0;

            this.txbXPixelOffset.Text = "20";
            this.txbYPixelOffset.Text = "35";

            this.SizeChanged += ExperimentDriver4BaseForm_SizeChanged;
            this.Move += ExperimentDriver4BaseForm_Move;
            this.MouseMove += ExperimentDriver4BaseForm_MouseMove;

            this.VsoWindow.MouseMove += VsoWindow_MouseMove;

            this.VsoWindow.MouseDown += VsoWindow_MouseDown;
            setupScreenSize();
            setScreenData(VsoWindow.ShowRulers != 0);
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            setScreenData(VsoWindow.ShowRulers != 0, true);

            this.txbMouseCoordX.Text = x.ToString() + " ";
            this.txbMouseCoordY.Text = y.ToString() + " ";

            double deltaX = visioX - x;
            double deltaY = visioY - y;

            Point p1 = Utilities.MapVisioToScreenCoords(this.axDrawingControl, VsoWindow, x, y, VsoWindow.ShowRulers != 0);
            //Point p2 = axDrawingControl.PointToScreen(new Point(0, 0));

            this.txbEstCursorX.Text = (p1.X).ToString() + " ";
            this.txbEstCursorY.Text = (p1.Y).ToString() + " ";
        }

        private void ExperimentDriver4BaseForm_SizeChanged(object sender, EventArgs e)
        {
            setupScreenSize();
        }

        private void ExperimentDriver4BaseForm_Move(object sender, EventArgs e)
        {
            setScreenData(VsoWindow.ShowRulers != 0);
        }

        private void setupScreenSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlLocnX = this.axDrawingControl.Location.X;
            int cntlLocnY = this.axDrawingControl.Location.Y;

            int cntlSizeX = formSizeX - cntlLocnX - 32;
            int cntlSizeY = formSizeY - cntlLocnY - 64;

            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }

        double visioX = 0;
        double visioY = 0;

        private void setScreenData(bool rulers, bool debug = false)
        {
            ExperimentDriver4BaseForm.debug = debug;

            int pixelLeft;
            int pixelTop;
            int pixelWidth;
            int pixelHeight;
            double visioLeft;
            double visioTop;
            double visioWidth;
            double visioHeight;

            VsoWindow.GetWindowRect(out pixelLeft, out pixelTop, out pixelWidth, out pixelHeight);
            VsoWindow.GetViewRect(out visioLeft, out visioTop, out visioWidth, out visioHeight);

            int leftMargin;
            int rightMargin;

            int topMargin;
            int bottomMargin;

            Point p;

            if (rulers)
            {
                leftMargin = 23;
                rightMargin = 0;

                topMargin = 35;
                bottomMargin = 20;

                p = axDrawingControl.PointToScreen(new Point(leftMargin, topMargin));
            }

            else
            {
                leftMargin = 6;
                rightMargin = -4;

                topMargin = 18;
                bottomMargin = 20;

                p = axDrawingControl.PointToScreen(new Point(leftMargin, topMargin));
            }
            //yPixelOffset = int.Parse(this.txbYPixelOffset.Text.Trim());

            p = axDrawingControl.PointToScreen(new Point(leftMargin, topMargin));

            int cursorRelPosnX = Cursor.Position.X - p.X;
            int cursorRelPosnY = Cursor.Position.Y - p.Y;

            Utilities.MapWindowsToVisio(
                    VsoWindow, cursorRelPosnX, cursorRelPosnY,
                    leftMargin + rightMargin,
                    topMargin + bottomMargin,
                    out visioX, out visioY);

            int pixelOffsetX = Cursor.Position.X- this.Left;
            int pixelOffsetY = Cursor.Position.Y - this.Top;
            
            this.txbCursorPosnX.Text = Cursor.Position.X + " ";
            this.txbCursorPosnY.Text = Cursor.Position.Y + " ";

            this.txbPixelOffsetX.Text = pixelOffsetX + " ";
            this.txbPixelOffsetY.Text = pixelOffsetY + " ";

            this.txbWinRecX.Text = pixelLeft.ToString() + " ";
            this.txbWinRecY.Text = pixelTop.ToString() + " ";

            this.txbVisioCoordX.Text = visioX.ToString() + " ";
            this.txbVisioCoordY.Text = visioY.ToString() + " ";

            this.txbCursorMinusPTSX.Text = cursorRelPosnX.ToString() + " ";
            this.txbCursorMinusPTSY.Text = cursorRelPosnY.ToString() + " ";

            this.txbPointToScreenX.Text = p.X.ToString() + " ";
            this.txbPointToScreenY.Text = p.Y.ToString() + " ";

        }

        private void VsoWindow_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            this.tssCursorPosn.Text = "(" + Cursor.Position.X + "," + Cursor.Position.Y + ")";
            setScreenData(VsoWindow.ShowRulers != 0);


            Point p1 = Utilities.MapVisioToScreenCoords(this.axDrawingControl, VsoWindow, x, y, VsoWindow.ShowRulers != 0);
            //Point p2 = axDrawingControl.PointToScreen(new Point(0, 0));

            this.txbEstCursorX.Text = (p1.X).ToString() + " ";
            this.txbEstCursorY.Text = (p1.Y).ToString() + " ";

            this.txbEstCursorDeltaX.Text = (p1.X - Cursor.Position.X).ToString() + " ";
            this.txbEstCursorDeltaY.Text = (p1.Y - Cursor.Position.Y).ToString() + " ";
        }

        private void ExperimentDriver4BaseForm_MouseMove(object sender, MouseEventArgs e)
        {
            //this.tssCursorPosn.Text = "(" + Cursor.Position.X + "," + Cursor.Position.Y + ")";
            //setScreenData(VsoWindow.ShowRulers != 0);
        }
        
    }
}
