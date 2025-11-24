

namespace TestDriverPolygonDistanceGenerator
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

    using Geometry;

    public partial class PolygonDistanceGeneratorMainForm : Form
    {
        public PolygonDistanceGeneratorMainForm()
        {
            InitializeComponent();
        }

        private void btnTest1_Click(object sender, EventArgs e)
        {
            PolygonDistanceGenerator polyDistanceGenerator = new PolygonDistanceGenerator(TestCases.t1p1, TestCases.t1p2);

            this.lblTest1Distance.Text = polyDistanceGenerator.GenPolyDistance().ToString();
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            PolygonDistanceGenerator polyDistanceGenerator = new PolygonDistanceGenerator(TestCases.t1p2, TestCases.t1p1);

            this.lblTest2Distance.Text = polyDistanceGenerator.GenPolyDistance().ToString();
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            PolygonDistanceGenerator polyDistanceGenerator = new PolygonDistanceGenerator(TestCases.t1p2, TestCases.t1p3);

            this.lblTest3Distance.Text = polyDistanceGenerator.GenPolyDistance().ToString();
        }

        private void btnTest4_Click(object sender, EventArgs e)
        {
            PolygonDistanceGenerator polyDistanceGenerator = new PolygonDistanceGenerator(TestCases.t1p4, TestCases.t1p4);

            this.lblTest4Distance.Text = polyDistanceGenerator.GenPolyDistance().ToString();
        }
    }
}
