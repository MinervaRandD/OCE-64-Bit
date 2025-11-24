using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace TestScrollControl
{
    using Visio = Microsoft.Office.Interop.Visio;

    public partial class ScrollTestForm : Form, IMessageFilter
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public ScrollTestForm()
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

            VsoWindow.Windows.ItemFromID[1653].Visible = true;

            setSize();

            this.SizeChanged += ScrollTestForm_SizeChanged;

            this.VsoWindow.ViewChanged += VsoWindow_ViewChanged;

            this.VsoWindow.WindowChanged += VsoWindow_WindowChanged;

            this.VsoWindow.ScrollLock = true;

            this.VsoWindow.ShowGuides = (short)1;

            this.VsoWindow.ViewFit = 1;

            this.VsoWindow.ZoomLock = true;
        }

        private void VsoWindow_WindowChanged(Visio.Window Window)
        {
            
        }

        private void VsoWindow_ViewChanged(Visio.Window Window)
        {
            
        }

        private void ScrollTestForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.ClientRectangle.Width;
            int formSizeY = this.ClientRectangle.Height;

            int cntlSizeX = formSizeX - 16;
            int cntlSizeY = formSizeY - 16;

            int cntlLocnX = 8;
            int cntlLocnY = 8;

            axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);
            axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
        }
        public bool PreFilterMessage(ref Message m)
        {
            bool rtrnCode = false;

            if (m.Msg == Windowsm
    }
}
