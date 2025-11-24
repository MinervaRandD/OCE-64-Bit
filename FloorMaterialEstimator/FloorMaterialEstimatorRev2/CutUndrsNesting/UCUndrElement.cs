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
using Utilities;

namespace FloorMaterialEstimator
{
    public partial class UCUndrElement : UserControl
    {
        public bool Selected { get; set; }

        public GraphicsUndrElem GraphicsUndrElem { get; set; }

        public UCUndrElement(GraphicsUndrElem graphicsUndrElem)
        {
            InitializeComponent();

            this.GraphicsUndrElem = graphicsUndrElem;

            this.BackColor = SystemColors.Control;

            this.Click += UCUndrElement_Click;

            this.lblUndrNbr.Click += UCUndrElement_Click;


            lblUndrNbr.Size = new Size(this.Width - 2, this.Height - 2);
            lblUndrNbr.Location = new Point(1, 1);

            lblUndrNbr.Text = Utilities.Utilities.IndexToLowerCaseString(this.GraphicsUndrElem.Index);

            Selected = false;
        }

        private void UCUndrElement_Click(object sender, EventArgs e)
        {
            if (UndrElementClick != null)
            {
                UndrElementClick.Invoke(this, CutIndex);
            }
        }

        public void SetSelected(bool selected)
        {
            this.Selected = selected;

            if (selected)
            {
             
                VisioInterop.SetBaseFillColor(GraphicsUndrElem.Shape, GlobalSettings.SelectedAreaColor);
                VisioInterop.SetFillOpacity(GraphicsUndrElem.Shape, GlobalSettings.SelectedAreaOpacity);

                SetBackgroundColor(GlobalSettings.SelectedAreaColor);
            }

            else
            {
              
                VisioInterop.SetBaseFillColor(GraphicsUndrElem.Shape, GraphicsUndrElem.FinishColor);
                VisioInterop.SetFillOpacity(GraphicsUndrElem.Shape, 0.0);

                SetBackgroundColor(SystemColors.Control);
            }

            this.Invalidate();
        }

        public void SetBackgroundColor(Color color)
        {
            this.BackColor = color;
        }

        public int CutIndex => (int)GraphicsUndrElem.Index;

        public delegate void UndrElementClickHandler(UCUndrElement sender, int label);

        public event UndrElementClickHandler UndrElementClick;

    }
}
