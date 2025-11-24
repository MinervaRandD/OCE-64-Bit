
namespace CombosLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Geometry;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CombosBaseForm : Form
    {
        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public CombosBaseForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            

        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int panlSizeX = this.pnlSelection.Width;
            int panlSizeY = formSizeY - 24;

            int panlLocnX = this.pnlSelection.Location.X;
            int panlLocnY = this.pnlSelection.Location.Y;

            int cntlLocnX = panlSizeX + panlLocnX + 24;
            int cntlLocnY = panlSizeY;

            int cntlSizeX = formSizeX - cntlLocnX - 12;
            int cntlSizeY = formSizeY - 24;
        }
    }
}
