
namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using FinishesLib;
    using Utilities;
    using Utilities.Supporting_Controls;
    using System.Collections;
    using System.Diagnostics;
    using CanvasLib.Markers_and_Guides;
    using SettingsLib;

    public partial class FieldGuideEditForm : Form, ICursorManagementForm
    {
        private FloorMaterialEstimatorBaseForm baseForm;

        private FieldGuideController fieldGuideController;

        private bool modal;

        private argb[] palletColors = new argb[]
        {
            new argb(255, 255, 255, 192),
            new argb(255, 255, 125, 125),
            new argb(255, 160, 255, 64),
            new argb(255, 128, 192, 255),
            new argb(255, 194, 133, 255),
            new argb(255, 255, 255, 0),
            new argb(255, 255, 0, 0),
            new argb(255, 0, 192, 0),
            new argb(255, 47, 151, 255),
            new argb(255, 167, 79, 255),
            new argb(255, 255, 159, 63),
            new argb(255, 192, 0, 0),
            new argb(255, 0, 128, 0),
            new argb(255, 0, 0, 255),
            new argb(255, 128, 0, 255),
            new argb(255, 192, 96, 0),
            new argb(255, 255, 128, 255),
            new argb(255, 0, 255, 128),
            new argb(255, 0, 102, 204),
            new argb(255, 0, 192, 192),
            new argb(255, 122, 61, 0),
            new argb(255, 255, 64, 160),
            new argb(255, 0, 192, 96),
            new argb(255, 0, 64, 128),
            new argb(255, 0, 128, 128),
            new argb(255, 0, 0, 0),
            new argb(255, 134, 134, 134),
            new argb(255, 182, 182, 182),
            new argb(255, 228, 228, 228),
            new argb(255, 255, 255, 255)
        };

        public FieldGuideEditForm(FloorMaterialEstimatorBaseForm baseForm, FieldGuideController fieldGuideController, bool modal)
        {
            InitializeComponent();

            this.ucCustomColorPallet.Init(palletColors);

            this.baseForm = baseForm;

            this.fieldGuideController = fieldGuideController;

            this.modal = modal;


            ElementsChanged = false;

            if (modal)
            {
                this.btnLineFinishEditorHide.Text = "Close";
            }

            else
            {
                this.btnLineFinishEditorHide.Text = "Hide";
            }

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.ucCustomColorPallet.SetSelectedButtonFormat(this.fieldGuideController.LineColor);
            this.ucCustomDashType.SetSelectedDashElementFormat(this.fieldGuideController.LineStyle);
            this.trbLineWidth.Value = 10 * (int) Math.Round(this.fieldGuideController.LineWidthInPts, 1);
            this.trbOpacity.Value = 100 * (int) (Math.Max(0, Math.Min(1, this.fieldGuideController.Opacity)));

            this.lblLineWidth.Text = this.fieldGuideController.LineWidthInPts.ToString("0.0");
            this.lblOpacity.Text = (100 * (int)(Math.Max(0, Math.Min(1, this.fieldGuideController.Opacity)))).ToString() + '%';

            this.ucCustomColorPallet.ColorSelected += UcCustomColorPallet_ColorSelected;
            this.ucCustomDashType.DashTypeSelected += UcCustomDashType_DashTypeSelected;

            this.trbOpacity.ValueChanged += TrbOpacity_ValueChanged;
            this.trbLineWidth.ValueChanged += TrbLineWidth_ValueChanged;

            AddToCursorManagementList();

            this.Disposed += LineFinishesEditForm_Disposed;
        }

        private void TrbOpacity_ValueChanged(object sender, EventArgs e)
        {
            int lineOpacity = Math.Min(100, Math.Max(0, trbOpacity.Value));
            this.lblOpacity.Text = lineOpacity.ToString() + '%';

            fieldGuideController.SetLineOpacity((double)lineOpacity / 100.0);
        }

        private void TrbLineWidth_ValueChanged(object sender, EventArgs e)
        {
            double lineWidthInPts = (double)trbLineWidth.Value / 10.0;
            this.lblLineWidth.Text = lineWidthInPts.ToString("0.0");

            fieldGuideController.SetLineWidth(lineWidthInPts);
        }

        private void UcCustomColorPallet_ColorSelected(ColorSelectedEventArgs args)
        {
            fieldGuideController.SetLineColor(Color.FromArgb(args.A, args.R, args.G, args.B));
        }
        public void setGuidecolor(Color cl, double size)
        {
            fieldGuideController.SetLineColor(cl);
            fieldGuideController.SetLineWidth(size);
        }
        private void UcCustomDashType_DashTypeSelected(DashTypeSelectedEventArgs args)
        {
            fieldGuideController.SetLineType(args.VisioDashTypeIndex);
        }

        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            GlobalSettings.FieldGuideColor = this.fieldGuideController.LineColor;
            GlobalSettings.FieldGuideOpacity = this.fieldGuideController.Opacity;
            GlobalSettings.FieldGuideStyle = this.fieldGuideController.LineStyle;
            GlobalSettings.FieldGuideWidthInPts = this.fieldGuideController.LineWidthInPts;

            ManagedMessageBox.Show("The global default guide line formats have been saved."); ;

            return;
        }

        public bool ElementsChanged { get; set; } = true;

        private void btnLineFinishEditorHide_Click(object sender, EventArgs e)
        {
            if (!modal)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            else
            {
                this.Close();
            }
        }

        #region Closing and Disposing
        private void LineFinishesEditForm_Disposed(object sender, EventArgs e)
        {
            RemoveFromCursorManagementList();
        }

        #endregion

        #region Cursor Management
        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }
        public bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }

        public void AddToCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Add(this);
        }

        public void RemoveFromCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Remove(this);
        }
        public bool IsTopMost { get; set; } = false;

        #endregion
    }
}
