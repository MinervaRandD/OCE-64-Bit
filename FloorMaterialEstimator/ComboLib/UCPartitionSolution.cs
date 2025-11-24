

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
    using MaterialsLayout;

    public partial class UCPartitionSolution : UserControl
    {
        ComboPartitionSolution partitionSolution;

        //List<ParentGraphicsCut> cutList;

        double PartWdth = 0;

        public UCPartitionSolution(ComboPartitionSolution partitionSolution, double drawingScaleInFeet)
        {
            InitializeComponent();

            this.partitionSolution = partitionSolution;

            this.lblPartitionDefinition.Text = this.partitionSolution.partitionDefinitionText();

            int totlWdth = 0;

            // Get the maximum width of any cut

            double maxY = double.MinValue;

            foreach (ComboPartitionUnitSolution partitionUnitSolution in this.partitionSolution)
            {
                foreach (ComboPath path in partitionUnitSolution)
                {
                    double currMax = path.MaxY;

                    if (currMax > maxY)
                    {
                        maxY = currMax;
                    }
                }
            }

            for (int i = this.partitionSolution.Count - 1; i >= 0; i--)
            {
                ComboPartitionUnitSolution partitionUnitSolution = this.partitionSolution[i];

                if (partitionUnitSolution.Count <= 0)
                {
                    continue;
                }

                UCPartitionUnitSolution ucUnitSolution = new UCPartitionUnitSolution(partitionUnitSolution, maxY, drawingScaleInFeet);

                this.flpPartitionDisplay.Controls.Add(ucUnitSolution);

                PartWdth += ucUnitSolution.UnitWdth;

                totlWdth += ucUnitSolution.Width;
            }

            this.Width = this.flpPartitionDisplay.Location.X + totlWdth;

            this.flpPartitionDisplay.Width = totlWdth;

            this.lblTotalLength.Text = (maxY * drawingScaleInFeet).ToString("#,##0.00") + " X " + (PartWdth * drawingScaleInFeet).ToString("#,##0.00");
        }
        
    }
}
