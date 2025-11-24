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

namespace FloorMaterialEstimator.CanvasManager
{
    public partial class RotationBaseForm : Form
    {
        CanvasManager canvasManager;

        public RotationBaseForm()
        {
            InitializeComponent();
        }

        public RotationBaseForm(CanvasManager canvasManager)
        {
            InitializeComponent();

            this.canvasManager = canvasManager;
        }

        private void txbRotationInDegrees_TextChanged(object sender, EventArgs e)
        {
            string strRotationInDegrees = this.txbRotationInDegrees.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(strRotationInDegrees))
            {
                this.txbRotationInDegrees.BackColor = Color.Pink;

                return;
            }

            double rotation = 0;

            if (!double.TryParse(strRotationInDegrees, out rotation))
            {
                this.txbRotationInDegrees.BackColor = Color.Pink;

                return;
            }

            if (rotation < -180 || rotation > 180)
            {
                this.txbRotationInDegrees.BackColor = Color.Pink;

                return;
            }

            this.txbRotationInDegrees.BackColor = SystemColors.ControlLightLight;

        }

        private void btnRotateByDegrees_Click(object sender, EventArgs e)
        {
            if (this.txbRotationInDegrees.BackColor == Color.Pink)
            {
                MessageBoxAdv.Show("A valid rotation >= -180 and <= 180 degrees is required", "Invalid Rotation Specified", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);

                return;
            }
            string strRotationInDegrees = this.txbRotationInDegrees.Text.Trim();

            if (!Utilities.Utilities.IsValidDbl(strRotationInDegrees))
            {
                this.txbRotationInDegrees.BackColor = Color.Pink;

                return;
            }

            double rotation = 0;

            if (!double.TryParse(strRotationInDegrees, out rotation))
            {
                MessageBoxAdv.Show("A valid rotation >= -180 and <= 180 degrees is required", "Invalid Rotation Specified", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);

                return;
            }

            if (rotation < -180 || rotation > 180)
            {
                MessageBoxAdv.Show("A valid rotation >= -180 and <= 180 degrees is required", "Invalid Rotation Specified", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);

                return;
            }

            Rotater rotater = new Rotater(canvasManager.CurrentPage);

            rotater.DoRotate(rotation); 
        }
    }
}
