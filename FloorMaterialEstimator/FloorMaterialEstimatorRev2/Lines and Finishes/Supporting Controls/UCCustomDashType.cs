using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorMaterialEstimator.Finish_Controls
{
    public class DashTypeSelectedEventArgs : EventArgs
    {
        public float[] DashPattern;
        public int VisioDashTypeIndex;
    }

    public delegate void DashTypeSelectedEvent(DashTypeSelectedEventArgs args);

    public partial class UCCustomDashType : UserControl
    {
        private float[][] dashPatterns = new float[][]
        {
                new float[] { 1 },
                new float[] { 1, 1, 1, 1 },
                new float[] { 2, 1, 2, 1 },
                new float[] { 4, 1, 4, 1 },
                new float[] { 6, 2, 6, 2 },
                new float[] { 4, 2, 2, 2 },
                new float[] { 8, 2, 3, 2 }
        };

        private int[] visioDashTypeIndex = new int[]
        {
            1, 10, 23, 2, 16, 4, 14
        };

        public event DashTypeSelectedEvent DashTypeSelected;

        private UCDashTypeListElement[] dashTypeElementList;

        public int SelectedDashTypeIndex = 0;

        public UCCustomDashType()
        {
            InitializeComponent();

            int cntlLocnX = 2;
            int cntlLocnY = 2;

            dashTypeElementList = new UCDashTypeListElement[dashPatterns.Length];

            this.pnlDashTypes.Width = this.Width - 2;

            for (int i = 0; i < dashPatterns.Length; i++)
            {
                UCDashTypeListElement ucDashTypeListElement =
                    new UCDashTypeListElement(Color.Black, 2, dashPatterns[i], visioDashTypeIndex[i], i);

                this.pnlDashTypes.Controls.Add(ucDashTypeListElement);

                ucDashTypeListElement.Location = new Point(cntlLocnX, cntlLocnY);
                ucDashTypeListElement.Size = new Size(this.pnlDashTypes.Width - 4, ucDashTypeListElement.Height);

                dashTypeElementList[i] = ucDashTypeListElement;

                cntlLocnY += ucDashTypeListElement.Height + 2;

                ucDashTypeListElement.Click += UcDashTypeListElement_Click;
            }

            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void UcDashTypeListElement_Click(object sender, EventArgs e)
        {
            if (DashTypeSelected == null)
            {
                return;
            }

            UCDashTypeListElement dashType = (UCDashTypeListElement)sender;

            SelectedDashTypeIndex = dashType.ListElementIndex;

            DashTypeSelectedEventArgs args = new DashTypeSelectedEventArgs()
            {
                DashPattern = dashPatterns[SelectedDashTypeIndex],
                VisioDashTypeIndex = dashType.VisioDashTypeIndex
            };

            SetSelectedDashElementFormat(dashType.VisioDashTypeIndex);

            DashTypeSelected(args);
        }

        internal void SetSelectedDashElementFormat(int visioLineType)
        {
            for (int i = 0; i < dashTypeElementList.Count(); i++)
            {
                if (visioDashTypeIndex[i] == visioLineType)
                {
                    dashTypeElementList[i].BorderStyle = BorderStyle.FixedSingle;
                }

                else
                {
                    dashTypeElementList[i].BorderStyle = BorderStyle.None;
                }
            }
        }
    }
}
