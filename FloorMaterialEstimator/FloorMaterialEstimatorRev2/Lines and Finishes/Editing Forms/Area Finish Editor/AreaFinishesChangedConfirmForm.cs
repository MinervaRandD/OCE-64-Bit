

namespace FloorMaterialEstimator.Finish_Controls
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
    using FinishesLib;

    public partial class AreaFinishesChangedConfirmForm : Form
    {
        public AreaFinishesChangedConfirmForm(List<AreaFinishBase> changedAreaFinishBaseList )
        {
            InitializeComponent();

            foreach (AreaFinishBase changedAreaFinishBase in changedAreaFinishBaseList)
            {
                Label changedAreaNameLabel = new Label();

                changedAreaNameLabel.Text = changedAreaFinishBase.AreaName;

                flpChangedAreaFinishes.Controls.Add(changedAreaNameLabel);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
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
