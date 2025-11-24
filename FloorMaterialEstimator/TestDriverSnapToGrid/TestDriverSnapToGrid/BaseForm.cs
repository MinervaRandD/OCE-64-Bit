using Microsoft.Office.Interop.Visio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Visio = Microsoft.Office.Interop.Visio;

namespace TestDriverSnapToGrid
{
    public partial class BaseForm : Form
    {
        public AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl AxDrawingControl;

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Visio.Application VsoApplication { get; set; }

        private Visio.Shape guideX = null;
        private Visio.Shape guideY = null;

        private Visio.Shape snapGuideX = null;
        private Visio.Shape snapGuideY = null;

        private Visio.Layer snapLayer = null;
        private Visio.Layer baseLayer = null;

        public BaseForm()
        {
            InitializeComponent();

            this.AxDrawingControl = new AxMicrosoft.Office.Interop.VisOcx.AxDrawingControl();
            ((System.ComponentModel.ISupportInitialize)(this.AxDrawingControl)).BeginInit();

            this.Controls.Add(this.AxDrawingControl);
            // 
            // axDrawingControl
            // 
            this.AxDrawingControl.Enabled = true;
            this.AxDrawingControl.Location = new System.Drawing.Point(1, 1);
            this.AxDrawingControl.Margin = new System.Windows.Forms.Padding(4);
            this.AxDrawingControl.Name = "AxDrawingControl";
            this.AxDrawingControl.Size = new System.Drawing.Size(this.Width - 1, this.Height - 1);
            this.AxDrawingControl.BringToFront();

            this.AxDrawingControl.EndInit();
            this.AxDrawingControl.ResumeLayout(false);
            ;

            VsoWindow = this.AxDrawingControl.Window;
            VsoDocument = this.AxDrawingControl.Document;
            VsoPage = this.VsoDocument.Pages[1];
            
            snapLayer = VsoPage.Layers.Add("SnapLayer");
            baseLayer = VsoPage.Layers.Add("BaseLayer");

            snapLayer.CellsC[(short)Visio.VisCellIndices.visLayerSnap].FormulaU = "TRUE";
            baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerSnap].FormulaU = "FALSE";

            snapGuideX = VsoPage.AddGuide((short)Visio.VisGuideTypes.visVert, 1, 0.0);
            snapGuideY = VsoPage.AddGuide((short)Visio.VisGuideTypes.visHorz, 0.0, 1);

            snapLayer.Add(snapGuideX,1);
            snapLayer.Add(snapGuideY, 1);

            snapLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "TRUE";
            baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "TRUE";
            VsoApplication = this.VsoDocument.Application;

            VsoDocument.SnapEnabled = true;
            VsoWindow.ShowGuides = -1;

            this.SizeChanged += BaseForm_SizeChanged;
            //VsoWindow.MouseMove += VsoWindow_MouseMove;
            
            VsoWindow.Application.MouseUp += VsoWindow_MouseUp;
            VsoWindow.Application.KeyDown += VsoWindow_KeyDown;
            VsoWindow.Application.KeyUp += VsoWindow_KeyUp;
            VsoWindow.Application.MouseDown += Application_MouseDown;
            VsoWindow.Application.MouseMove += Application_MouseMove;
        }

        private bool _baseFormActive = true;
        private bool baseFormActive
        {
            get
            {
                return _baseFormActive;
            }

            set
            {
                if (_baseFormActive == value)
                {
                    return;
                }

                _baseFormActive = value;
            }
        }

        private void VsoWindow_KeyUp(int KeyCode, int KeyButtonState, ref bool CancelDefault)
        {
            //if (KeyCode != 16)
            //{
            //    return;
            //}
            //VsoWindow.Application.KeyDown += VsoWindow_KeyDown;
            //if (baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU != "FALSE")
            //{
            //    baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "FALSE";
            //}
        }

        private void VsoWindow_KeyDown(int KeyCode, int KeyButtonState, ref bool CancelDefault)
        {
            //if (KeyCode != 16)
            //{
            //    return;
            //}

            //VsoWindow.Application.KeyDown -= VsoWindow_KeyDown;

            //if (baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU != "TRUE")
            //{
            //    baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "TRUE";
            //}
        }

        bool mouseDown = false;

        private double mouseDownX = 0;
        private double mouseDownY = 0;

        private void VsoWindow_MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (!mouseDown)
            {
                return;
            }

            mouseDown = false;

            if (x != mouseDownX || y != mouseDownY)
            {
                return;
            }

            if (!baseFormActive)
            {
                return;
            }


            if (baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU != "TRUE")
            {
                baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "TRUE";
            }

            if (guideX == null)
            {
                guideX = VsoPage.AddGuide((short)Visio.VisGuideTypes.visVert, x, 0.0);
                baseLayer.Add(guideX, 1);
            }

            if (guideY == null)
            {
                guideY = VsoPage.AddGuide((short)Visio.VisGuideTypes.visHorz, 0.0, y);
                baseLayer.Add(guideY, 1);
            }

            guideX.Cells["PinX"].ResultIU = x;
            guideY.Cells["PinY"].ResultIU = y;

        }


        private void Application_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (mouseDown)
            {
                return;
            }

            mouseDownX = x;
            mouseDownY = y;

            mouseDown = true;

            //if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left || !baseFormActive)
            //{
            //    return;
            //}


            //if (baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU != "TRUE")
            //{
            //    baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "TRUE";
            //}

            //if (guideX == null)
            //{
            //    guideX = VsoPage.AddGuide((short)Visio.VisGuideTypes.visVert, x, 0.0);
            //    baseLayer.Add(guideX, 1);
            //}

            //if (guideY == null)
            //{
            //    guideY = VsoPage.AddGuide((short)Visio.VisGuideTypes.visHorz, 0.0, y);
            //    baseLayer.Add(guideY, 1);
            //}

            //guideX.Cells["PinX"].ResultIU = x;
            //guideY.Cells["PinY"].ResultIU = y;

        }

        private void BaseForm_SizeChanged(object sender, EventArgs e)
        {
            int sizeX = this.Size.Width;
            int sizeY = this.Size.Height;

            this.AxDrawingControl.Size = new Size(sizeX - 2, sizeY - 2);
        }

        private void Application_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
          
            try
            {
                
                
                if ((Control.MouseButtons & MouseButtons.Left) != MouseButtons.Left)
                {
                    return;
                }

                if (!baseFormActive)
                {
                    return;
                }

                if (baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU != "TRUE")
                {
                    baseLayer.CellsC[(short)Visio.VisCellIndices.visLayerVisible].FormulaU = "TRUE";
                }

                if (guideX == null)
                {
                    guideX = VsoPage.AddGuide((short)Visio.VisGuideTypes.visVert, x, 0.0);
                    baseLayer.Add(guideX, 1);
                }

                if (guideY == null)
                {
                    guideY = VsoPage.AddGuide((short)Visio.VisGuideTypes.visHorz, 0.0, y);
                    baseLayer.Add(guideY, 1);
                }

                guideX.Cells["PinX"].ResultIU = x;
                guideY.Cells["PinY"].ResultIU = y;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error tracking guides: " + ex.Message);
            }
        }
    }
}
