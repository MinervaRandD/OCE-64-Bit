using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utilities
{
    public partial class SelectableColorPanel : UserControl
    {
        public int PanelIndex;

        public SelectableColorPanel()
        {
            InitializeComponent();

            this.pnlColorPanel.Size = new Size(this.Width - 8, this.Height - 8);

            this.pnlColorPanel.Location = new Point(2, 2);

            this.BackColor = SystemColors.ControlLightLight;

            this.BorderStyle = BorderStyle.None;

            this.pnlColorPanel.Click += PnlColorPanel_Click;
        }

        private void PnlColorPanel_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        public new int Select()
        {
            this.BackColor = Color.Black;

            return PanelIndex;
        }

        public int Deselect()
        {
            this.BackColor = SystemColors.ControlLightLight;

            return PanelIndex;
        }

        public void SetColor(Color color)
        {
            this.pnlColorPanel.BackColor = color;
        }
    }
}
