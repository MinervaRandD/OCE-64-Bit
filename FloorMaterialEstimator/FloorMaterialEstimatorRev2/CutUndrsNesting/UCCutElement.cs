using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FloorMaterialEstimator.CanvasManager;
using Graphics;
using SettingsLib;
using MaterialsLayout;
using CutOversNestingLib;

namespace FloorMaterialEstimator
{
    public partial class UCCutElement : UserControl
    {
        public bool Selected { get; set; }

        public GraphicsCutElem GraphicsCutElem { get; set; }

        public UCCutElement(GraphicsCutElem graphicsCutElem)
        {
            InitializeComponent();

            this.GraphicsCutElem = graphicsCutElem;

            this.BackColor = SystemColors.Control;

            this.Click += UCCutElement_Click;

            this.lblCutNbr.Click += UCCutElement_Click;

            lblCutNbr.Size = new Size(this.Width - 2, this.Height - 2);
            lblCutNbr.Location = new Point(1, 1);

            lblCutNbr.Text = this.GraphicsCutElem.Index.ToString();

            Selected = false;
        }

        private void UCCutElement_Click(object sender, EventArgs e)
        {
            if (CutElementClick != null)
            {
                CutElementClick.Invoke(this, CutIndex);
            }
        }

        public void SetSelected(bool selected)
        {
            this.Selected = selected;

            if (selected)
            {
             
                VisioInterop.SetBaseFillColor(GraphicsCutElem.Shape, GlobalSettings.SelectedAreaColor);
                VisioInterop.SetFillOpacity(GraphicsCutElem.Shape, GlobalSettings.SelectedAreaOpacity);

                SetBackgroundColor(GlobalSettings.SelectedAreaColor);
            }

            else
            {
              
                VisioInterop.SetBaseFillColor(GraphicsCutElem.Shape, GraphicsCutElem.FinishColor);
                VisioInterop.SetFillOpacity(GraphicsCutElem.Shape, 0.0);

                SetBackgroundColor(SystemColors.Control);
            }

            this.Invalidate();
        }

        public void SetBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        public int CutIndex => (int)GraphicsCutElem.Index;

        public delegate void CutElementClickHandler(UCCutElement sender, int label);

        public event CutElementClickHandler CutElementClick;

    }
}
