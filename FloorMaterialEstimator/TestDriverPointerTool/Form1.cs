using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.VisOcx;
using Microsoft.Office.Interop.Visio;
using Visio = Microsoft.Office.Interop.Visio;
using Office = Microsoft.Office.Core;
using Extensibility;

namespace TestDriverPointerTool
{
   
    public partial class Form1 : Form
    {
        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl axDrawingControl;

        public Visio.Application VsoApplication;
        public Visio.Page VsoPage;
        public Visio.Window VsoWindow;
        public Visio.Document VsoDocument;

        public Form1()
        {
            InitializeComponent();


            this.axDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).BeginInit();

            //var resources = new ComponentResourceManager(this.GetType());
            //axDrawingControl.OcxState = (AxHost.State)resources.GetObject("axDrawingControl.OcxState");

            // 
            // axDrawingControl
            // 
            this.axDrawingControl.Enabled = true;
            this.axDrawingControl.Location = new System.Drawing.Point(1, 26);
            this.axDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.axDrawingControl.Name = "axDrawingControl";
            this.axDrawingControl.Size = new System.Drawing.Size(this.Width - 4, this.Height - 30);
            this.axDrawingControl.TabIndex = 3;

            this.Controls.Add(this.axDrawingControl);

            ((System.ComponentModel.ISupportInitialize)(this.axDrawingControl)).EndInit();


            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;
            VsoApplication = VsoDocument.Application;

            Visio.Pages pages = this.VsoDocument.Pages;
            VsoPage = pages[1];


            var line = VsoPage.DrawLine(1, 1, 5, 5);

            this.SizeChanged += Form1_SizeChanged;
            this.VsoWindow.MouseDown += VsoApplication_MouseDown;
            this.VsoWindow.MouseMove += VsoApplication_MouseMove;
            this.VsoWindow.MouseUp += VsoWindow_MouseUp;
            this.Load += Form1_Load; ;
            var x = VsoApplication.EventsEnabled;
            VsoApplication.EventsEnabled = 1;
            // VisioToolHelpers.TryActivatePointerTool(this.axDrawingControl.Document.Application);
        }

        private Visio.Shape marquee = null;

        private void VsoWindow_MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = true;

            return;

            if (Button != 1)
            {
                return;
            }

            if (marquee != null)
            {
                marquee.Delete();
            }

            marquee = null;
        }

        private void VsoApplication_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = false;

            return;

            if (KeyButtonState != 1)
            {
                return;
            }

            if (marquee == null)
            {
                marquee = VsoPage.DrawRectangle(origX, origY, x, y);
                VsoWindow.Select(marquee, (short)Visio.VisSelectArgs.visDeselect);
                // No fill (optional for marquee look)
                //marquee.CellsU["NoFill"].FormulaU = "1";

                //// Dotted outline
                //marquee.CellsU["LinePattern"].FormulaU = "3";      // 1=solid, 2=dashed, 3=dotted, 4=dash-dot, 5=dash-dot-dot

                //// Thin line weight (pick what looks right for you)
                //marquee.CellsU["LineWeight"].FormulaU = "0.75 pt"; // or "0.012 in", etc.
                //VsoApplication.EventsEnabled = 0;
                //VsoApplication.UndoEnabled = false;
               // VsoApplication.Settings.enable = false;
                return;
            }

            SetRectByCorners(marquee, origX, origY, x, y);
        }

        private double origX;
        private double origY;
        private void VsoApplication_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = false ;

            return;

            if (KeyButtonState != 1)
            {
                return;
            }

            if (marquee != null)
            {
                marquee.Delete();
            }

            origX = x;
            origY = y;
        }
        void SetRectByCorners(Visio.Shape rect, double x1, double y1, double x2, double y2)
        {
            double L = Math.Min(x1, x2);
            double R = Math.Max(x1, x2);
            double B = Math.Min(y1, y2);
            double T = Math.Max(y1, y2);

            double cx = (L + R) / 2.0;
            double cy = (B + T) / 2.0;
            double w = (R - L);
            double h = (T - B);

            // Optional: temporarily disable events/undo for smoother updates
            var app = rect.Application;
            short evt = app.EventsEnabled; bool undo = app.UndoEnabled;
            app.EventsEnabled = 0; app.UndoEnabled = false;
            try
            {
                rect.CellsU["PinX"].ResultIU = cx;
                rect.CellsU["PinY"].ResultIU = cy;
                rect.CellsU["Width"].ResultIU = w;
                rect.CellsU["Height"].ResultIU = h;
                // If the rectangle might be rotated, you can also set Angle = 0 first, then apply coords.
            }
            finally { app.UndoEnabled = undo; app.EventsEnabled = evt; }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.axDrawingControl.Size = new System.Drawing.Size(this.Width - 4, this.Height - 30);
        }
      
        private Office.CommandBar _myBar;
        private Office.CommandBarButton _btnPointer;


        private void Form1_Load(object sender, EventArgs e)
        {
            VsoApplication.DoCmd((short) Visio.VisUICmds.visCmdDRLineTool);

            return;
            var cbs = ((Visio.Application)VsoApplication).CommandBars;

            // Avoid duplicates if already created
            _myBar = null;

            foreach (var cb in cbs)
            {
                if (_myBar == null)
                    _myBar = cbs.Add(cb.ToString(), Office.MsoBarPosition.msoBarTop, System.Type.Missing, /*Temporary:*/ true);

                _myBar.Visible = true;

                break;
                //if (cb.Name == "MyAddinBar")
                //{
                //    _myBar = cb;
                //    break;
                //}
            }
            if (_myBar == null)
                _myBar = cbs.Add("MyAddinBar", Office.MsoBarPosition.msoBarTop, System.Type.Missing, /*Temporary:*/ true);

            _myBar.Visible = true;

            // Add a button
            _btnPointer = (Office.CommandBarButton)_myBar.Controls.Add(
                Office.MsoControlType.msoControlButton, Type.Missing, Type.Missing, Type.Missing, /*Temporary:*/ true);

            _btnPointer.Caption = "Pointer Tool";
            _btnPointer.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            _btnPointer.FaceId = 59; // any built-in icon id; change as you like
            _btnPointer.Tag = "MyAddin.PointerTool";
            _btnPointer.Click += _btnPointer_Click;
        }

        private void _btnPointer_Click(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            try { ((Visio.Application)VsoApplication).DoCmd(700); } catch { /* swallow/no-op */ }
        }

        public void OnDisconnection(Extensibility.ext_DisconnectMode mode, ref Array custom)
        {
            try { _btnPointer.Click -= _btnPointer_Click; } catch { }
            try { _myBar?.Delete(); } catch { }
        }

    public static class VisioToolHelpers
        {
            // Known command IDs
            private const short CMD_POINTER_TOOLS = 700;   // visCmdToolsPointerTool
            private const short CMD_POINTER_DR = 1283; // visCmdDRPointerTool
            private const short CMD_CANCEL_EDITING = 1060; // visCmdCancelInPlaceEditing (fallback, see note)

            public static bool TryActivatePointerTool(Visio.Application app, Visio.Window preferredWin = null)
            {
                try
                {
                    var win = preferredWin ?? app.ActiveWindow;
                    if (win == null) return false;

                    // Must be a drawing window
                    if (win.Type != (short)Visio.VisWinTypes.visDrawing) return false;

                    // Ensure focus is on the drawing window (especially important with AxDrawingControl)
                    try { win.Activate(); } catch { /* ignore */ }

                    // Exit text edit / modal states first (best-effort)
                    TryCancelInPlaceEditing(app);

                    // Some contexts need a tiny defer so Visio exits editing before switching tools
                    System.Windows.Forms.Application.DoEvents();

                    // Try the generic Tools command first
                    //if (TryDoCmd(win, CMD_POINTER_TOOLS)) return true;

                    // Fallback to the Drawing (DR) variant
                    //if (TryDoCmd(win, CMD_POINTER_DR)) return true;

                    // Last resort: app-level instead of window (rarely helps)
                    if (TryDoCmd(app, CMD_POINTER_TOOLS)) return true;
                    if (TryDoCmd(app, CMD_POINTER_DR)) return true;

                    return false;
                }
                catch { return false; }
            }

            //private static bool TryDoCmd(Visio.Window win, short cmd)
            //{
            //    try { win.DoCmd(cmd); return true; } catch { return false; }
            //}

            private static bool TryDoCmd(Visio.Application app, short cmd)
            {
                try { app.DoCmd(cmd); return true; } catch { return false; }
            }

            private static void TryCancelInPlaceEditing(Visio.Application app)
            {
                try
                {
                    // Preferred: built-in cancel command (ID can vary across builds; 1060 works in many)
                    try { app.DoCmd(CMD_CANCEL_EDITING); } catch { /* ignore */ }

                    // Fallback: send ESC to exit text edit if needed (only if you control the host window)
                    // System.Windows.Forms.SendKeys.SendWait("{ESC}");
                }
                catch { /* ignore */ }
            }
        }

}
}
