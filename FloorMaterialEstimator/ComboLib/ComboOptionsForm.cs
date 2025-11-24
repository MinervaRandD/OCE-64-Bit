

namespace FloorMaterialEstimator
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
    //using FloorMaterialEstimator.CanvasManager;
    using Graphics;
    using MaterialsLayout;
    using Utilities;

    public partial class ComboOptionsForm : Form, IMessageFilter
    {
        //UCGroupElement ucGroupElement;

        List<ComboPartitionSolution> solutionList;

        private double drawingScaleInFeet;

        Form baseForm;

        public ComboOptionsForm(Form baseForm, List<ComboPartitionSolution> solutionList, double drawingScaleInFeet)
        {
            InitializeComponent();

            this.baseForm = baseForm;

            //this.ucGroupElement = ucGroupElement;
            this.solutionList = solutionList;

            this.drawingScaleInFeet = drawingScaleInFeet;

            initSolutionDisplay();

            this.MouseEnter += ComboOptionsForm_MouseEnter;
            this.MouseLeave += ComboOptionsForm_MouseLeave;

            setSize();
         
            this.SizeChanged += ComboOptionsForm_SizeChanged;
            
        }

        private void ComboOptionsForm_MouseEnter(object sender, EventArgs e)
        {
            baseForm.Cursor = Cursors.Arrow;
        }

        private void ComboOptionsForm_MouseLeave(object sender, EventArgs e)
        {
            //baseForm.SetCursorForCurrentLocation();
        }

        private void ComboOptionsForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.ClientSize.Width;
            int formSizeY = this.ClientSize.Height;

            // Set up visio control size and location;

            int flowLocnX = 12;
            int flowLocnY = 12;

            int flowSizeX = formSizeX - 24;
            int flowSizeY = formSizeY - 24;

            this.flpCombOptions.Size = new Size(flowSizeX, flowSizeY);
            this.flpCombOptions.Location = new Point(flowLocnX, flowLocnY);   
        }

        private void initSolutionDisplay()
        {
            for (int i = 0; i < solutionList.Count; i++)
            {
                ComboPartitionSolution solution = solutionList[i];

                UCPartitionSolution ucPartitionSolution = new UCPartitionSolution(solution, drawingScaleInFeet);

                this.flpCombOptions.Controls.Add(ucPartitionSolution);
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(this);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
            {
                //baseForm.SetCursorForCurrentLocation();
#if DEBUG
                //baseForm.UpdateMousePositionDisplay();
#endif
            }

            return false;
        }
    }
}
