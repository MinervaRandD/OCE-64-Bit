using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialsLayout;

namespace CanvasLib.Area_and_Seam_Views
{
    public partial class UCRemnantsView : UserControl
    {
        public UCRemnantsView()
        {
            InitializeComponent();

            this.Load += UCRemnantsView_Load;
        }

        private void UCRemnantsView_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoSize = false;

            this.lblOverallWasteFactor.AutoSize = false;
            this.lblWasteViewTitle.AutoSize = false;

            this.lblWasteViewTitle.Font = new Font(this.lblWasteViewTitle.Font.FontFamily, 9);
            this.lblWasteViewTitle.TextAlign = ContentAlignment.MiddleLeft;
            this.lblWasteViewTitle.Size = new Size(106, 32);

            this.lblOverallWasteFactor.Font = new Font(this.lblOverallWasteFactor.Font.FontFamily, 10);
            this.lblOverallWasteFactor.TextAlign = ContentAlignment.MiddleRight;

            this.tbcRemant.Size = new Size(this.Width, this.Height - 48);
            this.tbcRemant.Font = new Font(this.tbcRemant.Font.FontFamily, 9);

            this.tbcRemant.Location = new Point(1, 1);
        }

        public void Init()
        {
           
        }

        double totalAreaInInches = 0;
        double remnantAreaInInches = 0;

        Dictionary<string, Tuple<double, double>> remnantDimensionsDict = new Dictionary<string, Tuple<double, double>>();

        public void AddRemnantArea(
            string remnantAreaIndex
            , List<GraphicsCut> graphicsCutList
            , double remnantAreaInInchesParm
            , double totalAreaInInchesParm)
        {
            double wasteFactor = 1.0 - (remnantAreaInInchesParm / totalAreaInInchesParm);

            this.totalAreaInInches += totalAreaInInchesParm;
            this.remnantAreaInInches += remnantAreaInInchesParm;

            TabPage tabPage = new TabPage(remnantAreaIndex);

            tabPage.Name = remnantAreaIndex;

            UCRemnantViewElement ucRemnantViewElement = new UCRemnantViewElement();

            ucRemnantViewElement.Init();

            ucRemnantViewElement.Setup(graphicsCutList, remnantAreaInInchesParm, totalAreaInInchesParm);

            tabPage.Controls.Add(ucRemnantViewElement);

            ucRemnantViewElement.Location = new Point(1, 1);

            this.tbcRemant.TabPages.Add(tabPage);

            remnantDimensionsDict.Add(remnantAreaIndex, new Tuple<double, double>(remnantAreaInInchesParm, totalAreaInInchesParm));

            if (this.totalAreaInInches <= 0.0)
            {
                this.lblOverallWasteFactor.Text = "0.0%";
            }

            else
            {
                this.lblOverallWasteFactor.Text = (100.0 * (1.0 - this.remnantAreaInInches / this.totalAreaInInches)).ToString("0.0") + '%';
            }
        }

        public void RemoveRemnantArea(string remnantAreaIndex)
        {
            if (!this.tbcRemant.TabPages.ContainsKey(remnantAreaIndex))
            {
                return;
            }

            this.tbcRemant.TabPages.RemoveByKey(remnantAreaIndex);

            Tuple<double, double> remnantDimensions = this.remnantDimensionsDict[remnantAreaIndex];

            this.remnantAreaInInches -= remnantDimensions.Item1;
            this.totalAreaInInches -= remnantDimensions.Item2;

            this.remnantDimensionsDict.Remove(remnantAreaIndex);

            if (this.totalAreaInInches <= 0.0)
            {
                this.lblOverallWasteFactor.Text = "0.0%";
            }

            else
            {
                this.lblOverallWasteFactor.Text = (100.0 * (1.0 - this.remnantAreaInInches / this.totalAreaInInches)).ToString("0.0") + '%';
            }
        }

        public bool ContainsRemnantKey(string remnantKey) => this.remnantDimensionsDict.ContainsKey(remnantKey);
    }
}
