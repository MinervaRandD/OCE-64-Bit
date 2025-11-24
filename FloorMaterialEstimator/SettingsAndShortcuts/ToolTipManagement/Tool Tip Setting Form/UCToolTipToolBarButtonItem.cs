using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SettingsLib
{
    public partial class UCToolTipToolBarButtonItem : UserControl
    {
        ToolStripButton toolStripButton;

        public UCToolTipToolBarButtonItem()
        {
            InitializeComponent();
        }

        internal void Init(ToolStripButton toolStripButton)
        {
            this.toolStripButton = toolStripButton;

            this.pcbToolStripButtonImage.Image = toolStripButton.Image;
            this.pcbToolStripButtonImage.SizeMode = PictureBoxSizeMode.StretchImage;

            this.txbToolTip.Text = toolStripButton.ToolTipText;
        }

        internal void UpdateToolTip()
        {
            toolStripButton.ToolTipText = this.txbToolTip.Text.Trim();
        }
    }
}
