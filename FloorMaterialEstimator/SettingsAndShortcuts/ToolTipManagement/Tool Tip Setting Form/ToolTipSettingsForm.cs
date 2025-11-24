using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SettingsLib
{
    public partial class ToolTipSettingsForm : Form
    {
        ToolTipEditManager toolTipEditManager;

        ToolTip toolTip;

        public ToolTipSettingsForm()
        {
            InitializeComponent();
        }

        public void Init(ToolTipEditManager toolTipEditManager, IEnumerable<object> toolTipObjectList, ToolTip toolTip)
        {
            this.toolTipEditManager = toolTipEditManager;

            this.toolTip = toolTip;

            int posnX = 4;
            int toolStripButtonPosnY = 4;
            int buttonPosnY = 4;
            int radioButtonPosnY = 4;
            int checkBoxPosnY = 4;

            foreach (object toolTipObject in toolTipObjectList)
            {
                if (toolTipObject is ToolStripButton)
                {
                    UCToolTipToolBarButtonItem ucToolTipToolBarButtonItem = new UCToolTipToolBarButtonItem();
                    ucToolTipToolBarButtonItem.Init((ToolStripButton)toolTipObject);

                    pnlToolStripButtonControls.Controls.Add(ucToolTipToolBarButtonItem);

                    ucToolTipToolBarButtonItem.Location = new Point(posnX, toolStripButtonPosnY);

                    toolStripButtonPosnY = toolStripButtonPosnY + ucToolTipToolBarButtonItem.Height + 1;

                    continue;
                }

                if (toolTipObject is Button)
                {
                    UCToolTipButtonItem ucToolTipButtonItem = new UCToolTipButtonItem();
                    ucToolTipButtonItem.Init((Button)toolTipObject, toolTip);

                    pnlButtonControls.Controls.Add(ucToolTipButtonItem);

                    ucToolTipButtonItem.Location = new Point(posnX, buttonPosnY);

                    buttonPosnY = buttonPosnY + ucToolTipButtonItem.Height + 1;

                    continue;
                }

                if (toolTipObject is RadioButton)
                {
                    UCToolTipRadioButtonItem ucToolTipRadioButtonItem = new UCToolTipRadioButtonItem();
                    ucToolTipRadioButtonItem.Init((RadioButton)toolTipObject, toolTip);

                    pnlRadioButtonControls.Controls.Add(ucToolTipRadioButtonItem);

                    ucToolTipRadioButtonItem.Location = new Point(posnX, radioButtonPosnY);

                    radioButtonPosnY = radioButtonPosnY + ucToolTipRadioButtonItem.Height + 1;

                    continue;
                }

                if (toolTipObject is CheckBox)
                {
                    UCToolTipCheckBoxItem ucToolTipCheckBoxItem = new UCToolTipCheckBoxItem();
                    ucToolTipCheckBoxItem.Init((CheckBox)toolTipObject, toolTip);

                    pnlCheckBoxControls.Controls.Add(ucToolTipCheckBoxItem);

                    ucToolTipCheckBoxItem.Location = new Point(posnX, checkBoxPosnY);

                    checkBoxPosnY = checkBoxPosnY + ucToolTipCheckBoxItem.Height + 1;

                    continue;
                }
            }
        }

        private void btnSaveChanges_Click(object sender, System.EventArgs e)
        {
            foreach (Control control in this.pnlToolStripButtonControls.Controls)
            {
                ((UCToolTipToolBarButtonItem)control).UpdateToolTip();
            }

            foreach (Control control in this.pnlButtonControls.Controls)
            {
                ((UCToolTipButtonItem)control).UpdateToolTip(toolTip);
            }

            foreach (Control control in this.pnlRadioButtonControls.Controls)
            {
                ((UCToolTipRadioButtonItem)control).UpdateToolTip(toolTip);
            }

            foreach (Control control in this.pnlCheckBoxControls.Controls)
            {
                ((UCToolTipCheckBoxItem)control).UpdateToolTip(toolTip);
            }

            toolTipEditManager.SaveToolTipDefinitions();

            MessageBox.Show("Tool tip definitions have been updated.");
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
