

namespace CanvasLib.Scale_Line
{
    using System;
    using System.Windows.Forms;
    using SettingsLib;

    public partial class SetScaleWarningForm : Form
    {
        public SetScaleWarningForm()
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ckbDontRemindAgain.Checked)
            {
                GlobalSettings.ShowSetScaleReminder = false;
            }

            this.Close();
        }
    }
}
