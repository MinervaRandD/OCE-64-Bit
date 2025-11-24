
namespace CanvasLib.Pan_And_Zoom
{
    using System;
    using System.Windows.Forms;
    using Utilities;

    public partial class CustomZoom : Form
    {
        public double zoomPercent;

        public CustomZoom()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strZoom = this.txbZoom.Text.Trim();

            if (string.IsNullOrEmpty(strZoom))
            {
                MessageBox.Show("Please provide a valid zoom percent.");
                return;
            }

            if (strZoom.EndsWith("%"))
            {
                strZoom = strZoom.Substring(0, strZoom.Length - 1).Trim();
            }

            if (!Utilities.IsAllDigits(strZoom))
            {
                MessageBox.Show("Please provide a valid zoom percent.");
                return;
            }

            zoomPercent = double.Parse(strZoom);

            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            this.Close();
        }
    }
}
