using System;
using System.Drawing;
using System.Windows.Forms;

namespace TestDriverCursors
{
    using Visio = Microsoft.Office.Interop.Visio;

    using Utilities;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;

    public partial class CursorTestForm : Form, IMessageFilter
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        private Rectangle AxEffectiveAreaBounds;

        public CursorTestForm()
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

            VsoWindow.ShowRulers = 1;

            AxEffectiveAreaBounds = new Rectangle(this.axDrawingControl.Location, new Size(this.axDrawingControl.Size.Width, this.axDrawingControl.Height - 36));

            this.MouseEnter += CursorTestForm_MouseEnter;

            this.Activated += CursorTestForm_Activated;
            //VsoWindow.MouseDown += VsoWindow_MouseDown;
            //VsoWindow.MouseUp += VsoWindow_MouseUp;
            //VsoWindow.MouseMove += VsoWindow_MouseMove;

            //this.axDrawingControl.Enter += AxDrawingControl_Enter;
            //this.axDrawingControl.Leave += AxDrawingControl_Leave;
            //this.axDrawingControl.GotFocus += AxDrawingControl_GotFocus;
            //VsoWindow.WindowActivated += VsoWindow_WindowActivated;

        }

        private void CursorTestForm_Activated(object sender, EventArgs e)
        {
            //SetCursorForCurrentLocation();
        }

        private void CursorTestForm_MouseEnter(object sender, EventArgs e)
        {
            this.Activate();
            SetCursorForCurrentLocation();
        }

        //private void AxDrawingControl_GotFocus(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Cross;
        //}

        //private void AxDrawingControl_Leave(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Default;
        //}

        //private void AxDrawingControl_Enter(object sender, EventArgs e)
        //{
        //    this.Cursor = Cursors.Cross;
        //}

        private void VsoWindow_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            //this.Cursor = Cursors.Cross;
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

        //protected override void WndProc(ref Message m)
        //{
        //    Point clientPos = PointToClient(Cursor.Position);

        //    this.lblCursorPosition.Text = '(' + clientPos.X.ToString("#,##0.00") + ", " + clientPos.Y.ToString("#,##0.00") + ')';

        //    if (base.Bounds.Contains(Cursor.Position))
        //    {
        //        SetCursorForCurrentLocation();

        //        //ActivateForm();

        //        this.lblWithinBounds.Text = "True";
        //    }

        //    else
        //    {
        //        //this.cursorTestForm.SetCursorForCurrentLocation();
        //        this.lblWithinBounds.Text = "False";
        //    }

        //    base.WndProc(ref m);
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

            //if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
            //{
            //    return SetCursorForCurrentLocation();
            //}

            //if (m.Msg == (int) WindowsMessage.WM_MOUSEHOVER)
            //{
            //    return SetCursorForCurrentLocation();
            //}

            return rtrnCode;
        }

        private uint cursorType;

        public bool SetCursorForCurrentLocation()
        {
            
            foreach (DisplayTestForm form in FormList)
            {
                if (form.CursorWithinBounds())
                {
                    return false;
                }
            }

            if (AxEffectiveAreaBounds.Contains(PointToClient(Cursor.Position)))
            {

                CursorManager.SetCursorToCross();

                this.lblCursorType.Text = "Cross";
                this.sssCursorTest.Refresh();

                return false;
            }

            else
            {
                CursorManager.SetCursorToArrow();

                this.lblCursorType.Text = "Arrow";
                this.sssCursorTest.Refresh();

                return false;
            }
        }


        protected override void WndProc(ref Message m)
        {
            Point clientPos = PointToClient(Cursor.Position);

            this.lblCursorPosition.Text = '(' + clientPos.X.ToString("#,##0.00") + ", " + clientPos.Y.ToString("#,##0.00") + ')';

            if (base.Bounds.Contains(Cursor.Position))
            {
                SetCursorForCurrentLocation();
                //CursorManager.SetCursorToArrow();

                //ActivateForm();

                this.lblWithinBounds.Text = "True";
            }

            else
            {
                // this.cursorTestForm.SetCursorForCurrentLocation();
                this.lblWithinBounds.Text = "False";
            }

            base.WndProc(ref m);
        }

        public List<DisplayTestForm> FormList = new List<DisplayTestForm>();

        private int formNumber = 1;

        private void btnLaunchTestForm_Click(object sender, EventArgs e)
        {
            DisplayTestForm testForm1 = new DisplayTestForm(this, formNumber);

            formNumber++;

            FormList.Add(testForm1);

            testForm1.FormClosed += TestForm1_FormClosed;
            testForm1.Show(this);

        }

        private void TestForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormList.Remove((DisplayTestForm)sender);
        }
    }
}
