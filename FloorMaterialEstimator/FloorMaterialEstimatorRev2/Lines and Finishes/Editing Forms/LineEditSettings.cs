
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

    public partial class LineEditSettings : Form, ICursorManagementForm
    {
        public Color[] LineEditSettingColorArray;
        public int[] LineEditSettingIntensityArray;

        public int DefaultColorIndex;

        private Button[] LineEditButtonArray;

        private SelectableColorPanel selectedPanel;

        private ExamplePolygon uclExamplePolygon;

        private FloorMaterialEstimatorBaseForm baseForm;

        public LineEditSettings(
            FloorMaterialEstimatorBaseForm baseForm,
            Color[] lineEditSettingColorArray, int[] lineEditSettingIntensityArray, int defaultColorIndex)
        {
            InitializeComponent();

            AddToCursorManagementList();
            this.FormClosed += FinishesEditForm_FormClosed;

            this.baseForm = baseForm;

            LineEditButtonArray = new Button[2];

            LineEditButtonArray[0] = btnColor1;
            LineEditButtonArray[1] = btnColor2;

            LineEditSettingColorArray = lineEditSettingColorArray;
            LineEditSettingIntensityArray = lineEditSettingIntensityArray;

            for (int i = 0; i < LineEditButtonArray.Length; i++)
            {
                LineEditButtonArray[i].Click += LineSettings_Click;

                LineEditButtonArray[i].BackColor = DrawingUtils.modifyColorByIntensity(LineEditSettingColorArray[i], LineEditSettingIntensityArray[i]);
            }

            uclExamplePolygon = new ExamplePolygon(Color.LightGray);

            this.Controls.Add(uclExamplePolygon);

            uclExamplePolygon.BringToFront();

            uclExamplePolygon.Location = new Point(80, 80);
            uclExamplePolygon.Size = new Size(220, 180);

            DefaultColorIndex = defaultColorIndex;

            setupSelectedColor();

            //this.MouseEnter += LineEditSettings_MouseEnter;
            //this.MouseLeave += LineEditSettings_MouseLeave;

            this.trbIntensity.ValueChanged += TrbTransparency_ValueChanged;
            this.nudIntensity.ValueChanged += NudTransparency_ValueChanged;

        }

        //private void LineEditSettings_MouseEnter(object sender, EventArgs e)
        //{
        //   baseForm.Cursor = Cursors.Arrow;
        //}

        //private void LineEditSettings_MouseLeave(object sender, EventArgs e)
        //{
        //   baseForm.SetCursorForCurrentLocation();
        //}

        private bool changingIntensity = false;

        public bool SetDefault = false;

        private void TrbTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (changingIntensity)
            {
                return;
            }

            changingIntensity = true;

            this.nudIntensity.Value = this.trbIntensity.Value;

            updateIntensity(this.trbIntensity.Value);

            changingIntensity = false;
        }


        private void NudTransparency_ValueChanged(object sender, EventArgs e)
        {
            if (changingIntensity)
            {
                return;
            }

            changingIntensity = true;

            this.trbIntensity.Value = (int) this.nudIntensity.Value;

            updateIntensity((int)this.nudIntensity.Value);

            changingIntensity = false;
        }

        private void updateIntensity(int intensity)
        {
            LineEditSettingIntensityArray[DefaultColorIndex] = Math.Max(0, Math.Min(100, intensity));

            Color color = DrawingUtils.modifyColorByIntensity(LineEditSettingColorArray[DefaultColorIndex],
                                         LineEditSettingIntensityArray[DefaultColorIndex]);

            LineEditButtonArray[DefaultColorIndex].BackColor = color;

            this.uclExamplePolygon.SetColor(Color.LightGray, color);
        }

        private void LineSettings_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;

            for (int i = 0; i < LineEditButtonArray.Length; i++)
            {
                if (LineEditButtonArray[i] == selectedButton)
                {
                    DefaultColorIndex = i;

                    break;
                }
            }

            setupSelectedColor();
        }

        private void setupSelectedColor()
        {
            Color selectedColor = LineEditSettingColorArray[DefaultColorIndex];

            Color color = DrawingUtils.modifyColorByIntensity(LineEditSettingColorArray[DefaultColorIndex], LineEditSettingIntensityArray[DefaultColorIndex]);

            this.uclExamplePolygon.SetColor(Color.LightGray, color);

            changingIntensity = true;

            this.trbIntensity.Value = LineEditSettingIntensityArray[DefaultColorIndex];
            this.nudIntensity.Value = LineEditSettingIntensityArray[DefaultColorIndex];

            changingIntensity = false ;
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
            SetDefault = true;
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
