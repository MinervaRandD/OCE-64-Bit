using System;
using System.Collections.Generic;


namespace FloorMaterialEstimator
{
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
    using ComboLib;

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
                string label = combElem.GraphicsComboElem.Index.ToString() + '(' + combElem.lblGroupNumber.Text + ')';

                elemTagList.Add(label);
            }

            this.lblGroupNmbrs.Text = string.Join(", ", elemTagList);

            this.btnShowOptions.BackColor = SystemColors.Control;

            this.btnShowOptions.MouseEnter += BtnShowOptions_MouseEnter;

            this.btnShowOptions.MouseLeave += BtnShowOptions_MouseLeave;

            this.btnShowOptions.Cursor = Cursors.Arrow;

            this.btnDeleteGroup.BackColor = SystemColors.Control;

            this.btnDeleteGroup.MouseEnter += BtnDeleteGroup_MouseEnter;

            this.btnDeleteGroup.MouseLeave += BtnDeleteGroup_MouseLeave;

            this.btnDeleteGroup.Cursor = Cursors.Arrow;
        }

        private void BtnDeleteGroup_MouseEnter(object sender, EventArgs e)
        {
            this.btnDeleteGroup.BackColor = Color.Aqua;

            Cursor.Current = Cursors.Arrow;
            this.Cursor = Cursors.Arrow;

        }

        private void BtnDeleteGroup_MouseLeave(object sender, EventArgs e)
        {
            this.btnDeleteGroup.BackColor = SystemColors.Control;

            this.Cursor = Cursors.Cross;
        }

        private void BtnShowOptions_MouseEnter(object sender, EventArgs e)
        {
            this.btnShowOptions.BackColor = Color.Aqua;

            Cursor.Current = Cursors.Arrow;
            this.Cursor = Cursors.Arrow;
        }

        private void BtnShowOptions_MouseLeave(object sender, EventArgs e)
        {
            this.btnShowOptions.BackColor = SystemColors.Control;

            Cursor.Current = Cursors.Cross;
        }

        private void btnShowOptions_Click(object sender, EventArgs e)
        {
            int groupCount = GroupList.Count;

            //List<ParentGraphicsCut> graphicsCutList = new List<ParentGraphicsCut>();

            ComboSolutionGenerator comboSolutionGenerator = new ComboSolutionGenerator(GroupList);

            List<Tuple<PartitionSolution, GraphicsComboElem[], double[,]>> searchSolutionList = comboSolutionGenerator.GenerateSolution();

            List<ComboPartitionSolution> solutionList = new List<ComboPartitionSolution>();

            foreach (Tuple<PartitionSolution, GraphicsComboElem[], double[,]> searchSolution in searchSolutionList)
            {
                PartitionSolution partitionSolution = searchSolution.Item1;
                GraphicsComboElem[] graphicsComboElemList = searchSolution.Item2;
                double[,] wghtMtrx = searchSolution.Item3;

                ComboPartitionSolution comboPartitionSolution = new ComboPartitionSolution(partitionSolution, graphicsComboElemList, wghtMtrx);

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
