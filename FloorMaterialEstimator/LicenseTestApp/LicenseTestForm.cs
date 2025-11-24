using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenseTestApp
{
    using Visio = Microsoft.Office.Interop.Visio;

    
    public partial class LicenseTestForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public LicenseTestForm()
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

            VsoPage.DrawLine(1, 1, 2, 2);
        }
    }
}
