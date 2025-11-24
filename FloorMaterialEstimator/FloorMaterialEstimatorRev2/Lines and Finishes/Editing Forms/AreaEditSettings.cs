
namespace FloorMaterialEstimator.Supporting_Forms
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

    using Utilities;

    public partial class AreaEditSettings : Form, ICursorManagementForm
    {
        public Color[] AreaEditSettingColorArray;
        public double[] AreaEditSettingTransparencyArray;

        public int DefaultColorIndex;

        public bool SetDefault = false;

        private Button[] AreaEditButtonArray;

        private ExamplePolygon uclExamplePolygon;

        private FloorMaterialEstimatorBaseForm baseForm;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaEditSettingColorArray"></param>
        /// <param name="areaEditSettingTransparencyArray"></param>
        /// <param name="defaultColorIndex"></param>
        public AreaEditSettings(
            FloorMaterialEstimatorBaseForm baseForm,
            Color[] areaEditSettingColorArray, double[] areaEditSettingTransparencyArray, int defaultColorIndex)
        {
            // Note. Currently, color and transparency are broken out separately, which is not really needed as
            // transparency can be encoded in the color itself. This should be changed when time permits.

            InitializeComponent();

            AddToCursorManagementList();
            this.FormClosed += FinishesEditForm_FormClosed;

            this.baseForm = baseForm;

            AreaEditButtonArray = new Button[2];

            AreaEditButtonArray[0] = btnColor1;
            AreaEditButtonArray[1] = btnColor2;

            AreaEditSettingColorArray = areaEditSettingColorArray;
            AreaEditSettingTransparencyArray = areaEditSettingTransparencyArray;

            // This update is done because color and transparency are broken out separately, which is not
            // the best way to do things. Should be changed in the future.

            for (int i = 0; i < AreaEditSettingColorArray.Length; i++)
            {
                double transparency = areaEditSettingTransparencyArray[i];
                Color color = AreaEditSettingColorArray[i];

                int A = 255 - (int)Math.Min(255.0, Math.Max(0.0, Math.Round(255.0 * transparency)));
                int R = color.R;
                int G = color.G;
                int B = color.B;

                AreaEditSettingColorArray[i] = Color.FromArgb(A, R, G, B);
            }

            for (int i = 0; i < AreaEditButtonArray.Length; i++)
            {
                AreaEditButtonArray[i].Click += AreaLineSettings_Click;

                AreaEditButtonArray[i].BackColor = AreaEditSettingColorArray[i];
            }

            uclExamplePolygon = new ExamplePolygon(AreaEditSettingColorArray[DefaultColorIndex]);

            this.Controls.Add(uclExamplePolygon);

            uclExamplePolygon.BringToFront();

            uclExamplePolygon.Location = new Point(80, 80);
            uclExamplePolygon.Size = new Size(220, 180);

            DefaultColorIndex = defaultColorIndex;

            AreaEditButtonArray[DefaultColorIndex].Select();

            setupSelectedColor();

            //this.MouseEnter += AreaEditSettings_MouseEnter;
            //this.MouseLeave += AreaEditSettings_MouseLeave;

            this.trbTransparency.Value = (int)Math.Min(100.0, Math.Max(0.0, Math.Round(100.0 * AreaEditSettingTransparencyArray[defaultColorIndex])));
            this.nudTransparency.Value = this.trbTransparency.Value;

            this.trbTransparency.ValueChanged += TrbTransparency_ValueChanged;
            this.nudTransparency.ValueChanged += NudTransparency_ValueChanged;
        }

        //private void AreaEditSettings_MouseEnter(object sender, EventArgs e)
        //{
        //    baseForm.Cursor = Cursors.Arrow;
        //}

        //private void AreaEditSettings_MouseLeave(object sender, EventArgs e)
        //{
        //    baseForm.SetCursorForCurrentLocation();
        //}

        private bool changingTransparency = false;

        private void TrbTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (changingTransparency)
            {
                return;
            }

            changingTransparency = true;

            this.nudTransparency.Value = this.trbTransparency.Value;

            updateTransparency(this.trbTransparency.Value);

            changingTransparency = false;
        }

        private void NudTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (changingTransparency)
            {
                return;
            }

            changingTransparency = true;

            this.trbTransparency.Value = (int) this.nudTransparency.Value;

            updateTransparency((int)this.nudTransparency.Value);

            changingTransparency = false;
        }

        private void updateTransparency(int transparency)
        {
            Color color = AreaEditSettingColorArray[DefaultColorIndex];

            int A = 255 - (int)Math.Min(255.0, Math.Max(0.0, Math.Round(2.55 * (double) transparency)));
            int R = color.R;
            int G = color.G;
            int B = color.B;

            AreaEditSettingColorArray[DefaultColorIndex] = Color.FromArgb(A, R, G, B);

            this.uclExamplePolygon.SetColor(AreaEditSettingColorArray[DefaultColorIndex]);

            AreaEditButtonArray[DefaultColorIndex].BackColor = AreaEditSettingColorArray[DefaultColorIndex];

            this.AreaEditSettingTransparencyArray[DefaultColorIndex] = Math.Min(1.0, Math.Max(0.0, (double)transparency * 0.01));
        }

        private void AreaLineSettings_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;

            for (int i = 0; i < AreaEditButtonArray.Length; i++)
            {
                if (AreaEditButtonArray[i] == selectedButton)
                {
                    DefaultColorIndex = i;

                    break;
                }
            }

            setupSelectedColor();
        }

        private void setupSelectedColor()
        {
            this.uclExamplePolygon.SetColor(AreaEditSettingColorArray[DefaultColorIndex]);

            changingTransparency = false ;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            this.SetDefault = true;
        }

        #region Cursor Management

        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
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
