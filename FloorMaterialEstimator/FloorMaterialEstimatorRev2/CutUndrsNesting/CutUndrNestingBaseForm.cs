using CutOversNestingLib;
using Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator
{
    public partial class CutUndrsNestingBaseForm : Form
    {
        public FloorMaterialEstimatorBaseForm BaseForm;

        public CutUndrsNestingBaseForm(FloorMaterialEstimatorBaseForm baseForm)
        {
            InitializeComponent();

            this.BaseForm = baseForm;

            ucCutOversNestingSelectionControl.Init(this);

            ucCutUndrsNestingNestControl.Init(this);

            setSize();

            this.SizeChanged += CutOversNestingBaseForm_SizeChanged;

        }

        private void CutOversNestingBaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int sizeX = this.ClientSize.Width;
            int sizeY = this.ClientSize.Height;

            this.tbcCutOversNesting.Size = new Size(sizeX - 4, sizeY - 4);
            this.tbcCutOversNesting.Location = new Point(2, 2);

            int tbpSelectionSizeX = this.tbcCutOversNesting.Width;
            int tbpSelectionSizeY = this.tbcCutOversNesting.Height;

            ucCutOversNestingSelectionControl.Size = new Size(tbpSelectionSizeX - 4, tbpSelectionSizeY);
            ucCutOversNestingSelectionControl.Location = new Point(2, 8);

            ucCutUndrsNestingNestControl.Size = new Size(tbpSelectionSizeX - 4, tbpSelectionSizeY);
            ucCutUndrsNestingNestControl.Location = new Point(2, 8);
        }

        internal void CutElementSelected(UCCutElement ucCutElement)
        {
            ucCutUndrsNestingNestControl.CutElementSelected(ucCutElement);
        }

        internal void RemoveUndrElement(GraphicsUndrElem graphicsUndrElem)
        {
            ucCutUndrsNestingNestControl.RemoveUndrElement(graphicsUndrElem);
        }

        internal void AddUndrElement(GraphicsUndrElem graphicsUndrElem)
        {
            ucCutUndrsNestingNestControl.AddUndrElement(graphicsUndrElem);
        }
    }
}
