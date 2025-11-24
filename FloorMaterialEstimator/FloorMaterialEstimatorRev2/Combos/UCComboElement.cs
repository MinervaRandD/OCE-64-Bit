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
using ComboLib;

namespace FloorMaterialEstimator
{
    public partial class UCComboElement : UserControl
    {
        public bool Selected { get; set; }

        public bool Combined { get; set; }

        public GraphicsComboElem GraphicsComboElem { get; set; }

        public UCComboElement(GraphicsComboElem graphicsComboElem)
        {
            InitializeComponent();

            this.GraphicsComboElem = graphicsComboElem;


            this.lblElementNumber.Text = graphicsComboElem.Index.ToString();
            this.lblFlipped.Text = string.Empty;
            this.lblGroupNumber.Text = string.Empty;
            this.BackColor = SystemColors.ControlLightLight;

            this.Click += UCComboElement_Click;

            this.lblElementNumber.Click += LblElementNumber_Click;
            this.lblGroupNumber.Click += LblGroupNumber_Click;
            this.lblFlipped.Click += LblFlipped_Click;

            Selected = false;
            Combined = false;
        }

        private void LblGroupNumber_Click(object sender, EventArgs e)
        {
            if (ComboElementClick != null)
            {
                ComboElementClick.Invoke(this, "GroupNumber", ModifierKeys.HasFlag(Keys.Control));
            }
        }

        private void LblFlipped_Click(object sender, EventArgs e)
        {
            if (ComboElementClick != null)
            {
                ComboElementClick.Invoke(this, "Flipped", ModifierKeys.HasFlag(Keys.Control));
            }
        }

        private void LblElementNumber_Click(object sender, EventArgs e)
        {
            if (ComboElementClick != null)
            {
                ComboElementClick.Invoke(this, "ElementNumber", ModifierKeys.HasFlag(Keys.Control));
            }
        }

        private void UCComboElement_Click(object sender, EventArgs e)
        {
            if (ComboElementClick != null)
            {
                ComboElementClick.Invoke(this, string.Empty, ModifierKeys.HasFlag(Keys.Control));
            }
        }

        public void SetSelected(bool selected)
        {
            this.Selected = selected;

            if (selected)
            {
                lblGroupNumber.Text = string.Empty;

                VisioInterop.SetBaseFillColor(GraphicsComboElem.Shape, GlobalSettings.SelectedAreaColor);
                VisioInterop.SetFillOpacity(GraphicsComboElem.Shape, GlobalSettings.SelectedAreaOpacity);

                SetBackgroundColor(GlobalSettings.SelectedAreaColor);
            }

            else
            {
                lblGroupNumber.Text = string.Empty;
                lblFlipped.Text = string.Empty;

                VisioInterop.SetBaseFillColor(GraphicsComboElem.Shape, GraphicsComboElem.FinishColor);
                VisioInterop.SetFillOpacity(GraphicsComboElem.Shape, 0.0);

                GraphicsComboElem.Shape.SetShapeText(GraphicsComboElem.Index.ToString(), Color.Black, 12);

                SetBackgroundColor(SystemColors.ControlLightLight);
            }

            this.Invalidate();
        }

        internal void SetCombined(bool combined)
        {
            this.Combined = combined;

            if (combined)
            {
                Selected = false;

                SetBackgroundColor(Color.Orange);

                VisioInterop.SetBaseFillColor(GraphicsComboElem.Shape, Color.Orange);
                VisioInterop.SetFillOpacity(GraphicsComboElem.Shape, 1.0);
            }

            else
            {
                lblGroupNumber.Text = string.Empty;
                lblFlipped.Text = string.Empty;

                VisioInterop.SetBaseFillColor(GraphicsComboElem.Shape, GraphicsComboElem.FinishColor);
                VisioInterop.SetFillOpacity(GraphicsComboElem.Shape, 0.0);

                GraphicsComboElem.Shape.SetShapeText(GraphicsComboElem.Index.ToString(), Color.Black, 12);

                SetBackgroundColor(SystemColors.ControlLightLight);
            }
        }

        public void SetBackgroundColor(Color color)
        {
            this.BackColor = color;
            this.lblElementNumber.BackColor = color;
            this.lblFlipped.BackColor = color;
            this.lblGroupNumber.BackColor = color;
        }

        public int CutIndex => (int)GraphicsComboElem.Index;

        public delegate void ComboElementClickHandler(UCComboElement sender, string label, bool cntlKeyPressed = false);

        public event ComboElementClickHandler ComboElementClick;

    }
}
