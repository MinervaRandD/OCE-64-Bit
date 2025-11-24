

namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Utilities;
    using Geometry;
    using MaterialsLayout;

    public partial class UCPartitionUnitSolution : UserControl
    {
        ComboPartitionUnitSolution comboPartitionUnitSolution;
        
        public double UnitWdth = 0;

        public UCPartitionUnitSolution(ComboPartitionUnitSolution comboPartitionUnitSolution, double maxY, double drawingScaleInFeet)
        {
            InitializeComponent();

            this.comboPartitionUnitSolution = comboPartitionUnitSolution;
            
            foreach (ComboPath path in comboPartitionUnitSolution)
            {
                UCUnitPath ucUnitPath = new UCUnitPath(path, maxY, drawingScaleInFeet);
                this.flpUnitSolution.Controls.Add(ucUnitPath);

                UnitWdth += ucUnitPath.PathWdth;
            }

            int totlWdth = 0;

            foreach (Control control in this.flpUnitSolution.Controls)
            {
                totlWdth += control.Width;
            }

            this.flpUnitSolution.Width = totlWdth;
            this.Width = totlWdth;

        }
    }
}
