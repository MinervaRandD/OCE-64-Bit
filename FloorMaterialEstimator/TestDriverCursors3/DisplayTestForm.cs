

namespace TestDriverCursors
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Utilities;

    public partial class DisplayTestForm : Form//, IMessageFilter
    {
        private CursorTestForm cursorTestForm;

        public DisplayTestForm(CursorTestForm cursorTestForm, int formNumber)
        {
            InitializeComponent();

            this.cursorTestForm = cursorTestForm;

            this.Text = "Form number: " + formNumber;

           // this.MouseHover += TestForm1_MouseHover;
            //this.MouseEnter += TestForm1_MouseEnter;
            //this.MouseLeave += TestForm1_MouseLeave;
            //this.MouseMove += TestForm1_MouseMove;
        }

        private void TestForm1_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblCursorLocation.Text = '(' + Cursor.Position.X.ToString("#,##0.00") + ", " + Cursor.Position.Y.ToString("#,##0.00") + ')';
        }

        private void TestForm1_MouseHover(object sender, EventArgs e)
        {
            this.cursorTestForm.Activate();
            this.cursorTestForm.SetCursorForCurrentLocation();
        }

        private void TestForm1_MouseLeave(object sender, EventArgs e)
        {
            //this.cursorTestForm.Activate();
            this.cursorTestForm.SetCursorForCurrentLocation();
        }

        private void TestForm1_Leave(object sender, EventArgs e)
        {
            //this.cursorTestForm.SetCursorForCurrentLocation();
        }

        bool activated = false;
        private void ActivateForm()
        {
            if (activated)
            {
                return;
            }

            this.Activate();
        }
        private void TestForm1_MouseEnter(object sender, EventArgs e)
        {
            //ActivateForm();
            //CursorManager.SetCursorToArrow();
        }

        //protected override void OnActivated(EventArgs e)
        //{
        //    base.OnActivated(e);
        //    // install message filter when form activates
        //    Application.AddMessageFilter(this);
        //}

        //protected override void OnDeactivate(EventArgs e)
        //{
        //    base.OnDeactivate(e);
        //    // remove message filter when form deactivates
        //    Application.RemoveMessageFilter(this);
        //}

        //public bool PreFilterMessage(ref Message m)
        //{

        //    bool rtrnCode = false;

        //    //if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
        //    //{
        //    //    CursorManager.SetCursorToArrow();
        //    //}

        //    //if (m.Msg == (int)WindowsMessage.WM_MOUSEHOVER)
        //    //{
        //    //    CursorManager.SetCursorToArrow();
        //    //}

        //    return rtrnCode;
        //}

        protected override void WndProc(ref Message m)
        {

            Point clientPos = PointToClient(Cursor.Position);

            this.lblCursorLocation.Text = '(' + clientPos.X.ToString("#,##0.00") + ", " + clientPos.Y.ToString("#,##0.00") + ')';

            if (base.Bounds.Contains(Cursor.Position))
            {
                CursorManager.SetCursorToArrow();

                ActivateForm();

                this.lblWithinBounds.Text = "True";
            }

            else
            {
               this.cursorTestForm.SetCursorForCurrentLocation();
                this.lblWithinBounds.Text = "False";
            }

            base.WndProc(ref m);
        }

        internal bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }
    }
}
