using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Geometry;
using Utilities;
using FloorMaterialEstimator.CanvasManager;
using MaterialsLayout;

namespace FloorMaterialEstimator
{
    public partial class UCGroupElement : UserControl
    {
        CombosBaseForm baseForm;

        int GroupNumber;

        public List<UCComboElement> GroupList;

        private double drawingScaleInInches => baseForm.DrawingScaleInInches;
       
        public UCGroupElement(CombosBaseForm baseForm, int groupNumber, List<UCComboElement> groupList)
        {
            InitializeComponent();

            this.baseForm = baseForm;

            this.GroupNumber = groupNumber;

            this.GroupList = groupList;

            this.lblGroupBaseNumber.Text = GroupNumber.ToString();

            List<string> elemTagList = new List<string>();

            foreach (UCComboElement combElem in groupList)
            {
                string label = combElem.canvasCut.Tag.ToString() + '(' + combElem.lblGroupNumber.Text + ')';

                elemTagList.Add(label);
            }

            this.lblGroupNmbrs.Text = string.Join(", ", elemTagList);
        }

        private void btnShowOptions_Click(object sender, EventArgs e)
        {
            int groupCount = GroupList.Count;

            //List<GraphicsCut> cutList = new List<GraphicsCut>();

            ComboSolutionGenerator comboSolutionGenerator = new ComboSolutionGenerator(GroupList);

            List<Tuple<PartitionSolution, GraphicsCut[], double[,]>> searchSolutionList = comboSolutionGenerator.GenerateSolution();

            List<ComboPartitionSolution> solutionList = new List<ComboPartitionSolution>();

            foreach (Tuple<PartitionSolution, GraphicsCut[], double[,]> searchSolution in searchSolutionList)
            {
                PartitionSolution partitionSolution = searchSolution.Item1;
                GraphicsCut[] cutList = searchSolution.Item2;
                double[,] wghtMtrx = searchSolution.Item3;

                ComboPartitionSolution comboPartitionSolution = new ComboPartitionSolution(partitionSolution, cutList, wghtMtrx);

                solutionList.Add(comboPartitionSolution);
            }

            ComboOptionsForm comboOptionsForm = new ComboOptionsForm(baseForm.BaseForm, solutionList, drawingScaleInInches / 12.0);

            comboOptionsForm.Show();
        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            baseForm.DeleteGroup(this);
        }
    }
}
