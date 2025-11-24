

namespace FloorMaterialEstimator.Finish_Controls
{
    using FloorMaterialEstimator.CanvasManager;

    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Utilities;
    using Graphics;
    using System.Diagnostics;

    public partial class UCSeamPallet : UserControl
    {
        public CanvasManager CanvasManager = null;

        public FloorMaterialEstimatorBaseForm BaseForm = null;

        public FinishSeamBaseList FinishSeamList;
        
        private UCSeam selectedSeamFinish = null;

        private List<UCSeam> finishSeamList;

        private int ucSeamListTotalHeight = 0;

        public UCSeam SelectedSeamFinish
        {
            get
            {
                return selectedSeamFinish;
            }

            set
            {
                selectedSeamFinish = value;

                foreach (UCSeam ucSeamFinish in finishSeamList)
                {
                    ucSeamFinish.Selected = ucSeamFinish.fsIndex == selectedSeamFinish.fsIndex;

                    ucSeamFinish.Invalidate();
                }
            }
        }

        public UCSeamPallet()
        {
            InitializeComponent();

            this.SizeChanged += UCFinishSeamPallet_SizeChanged;
        }

        private void UCFinishSeamPallet_SizeChanged(object sender, EventArgs e)
        {
            this.pnlFinishSeamList.Size = this.Size;
        }

        public void Init(
            FloorMaterialEstimatorBaseForm baseForm,
            CanvasManager canvasManager,
            string finishSeamFilePath)
        {
            this.BaseForm = baseForm;

            this.CanvasManager = canvasManager;
            
            if (File.Exists(finishSeamFilePath))
            {
                LoadFinishSeamLineList(finishSeamFilePath);
            }

            finishSeamList = new List<UCSeam>();

            int finishSeamLocY = 0;

            for (int i = 0; i < ucFinishPallet.areaFinishList.Count; i++)
            {
                UCFinish ucFinish = ucFinishPallet.areaFinishList[i];
                FinishSeamBase finishSeamLine;

                if (i < FinishSeamList.Count)
                {
                    finishSeamLine = FinishSeamList[i];
                }

                else
                {
                    finishSeamLine = FinishSeamBase.DefaultFinishSeamLine;
                }

                UCSeam ucFinishSeam = new UCSeam();

                ucFinishSeam.Init(this, FinishSeamList[i]);

                this.pnlFinishSeamList.Controls.Add(ucFinishSeam);
                this.finishSeamList.Add(ucFinishSeam);

                ucFinishSeam.Location = new Point(0, finishSeamLocY);

                finishSeamLocY += ucFinishSeam.Height + 1;
            }

            this.ucSeamListTotalHeight = finishSeamLocY + this.finishSeamList.Count * 40;
        }

        private void LoadFinishSeamLineList(string inptFilePath)
        {
            try
            {
                StreamReader sr = new StreamReader(inptFilePath);

                var serializer = new XmlSerializer(typeof(FinishSeamBaseList));

                FinishSeamList = (FinishSeamBaseList)serializer.Deserialize(sr);
            }

            catch (Exception ex)
            {
                ;
            }

        }
        
        public void Save(string outpFilePath)
        {
            var serializer = new XmlSerializer(typeof(FinishSeamBaseList));

            StreamWriter sw = new StreamWriter(outpFilePath);
            
            serializer.Serialize(sw, FinishSeamList);
        }


        public void SetSize(System.Drawing.Size containerSize)
        {
            int cntnSizeX = containerSize.Width;
            int cntnSizeY = containerSize.Height;

            int cntlSizeX = cntnSizeX - 2;
            int cntlSizeY = cntnSizeY - 2;

            this.Size = new Size(cntlSizeX, cntlSizeY);
            this.Location = new Point(1, 1);

            int slcdPanlSizeX = cntlSizeX - 32;
            int slcdPanlSizeY = this.pnlSelectedFinishSeam.Height;

            int seamPanlSizeX = cntlSizeX - 2;

            int seamPanlSizeY1 = cntlSizeY - slcdPanlSizeY - 16;
            int seamPanlSizeY2 = ucSeamListTotalHeight;

            int seamPanlSizeY = Math.Min(seamPanlSizeY1, seamPanlSizeY2);

            int seamPanlLocnX = 1;
            int seamPanlLocnY = 1;

            int slcdPanlLocnX = 1;
            int slcdPanlLocnY = seamPanlSizeY + seamPanlLocnY + 8;

            this.pnlFinishSeamList.Size = new Size(seamPanlSizeX, seamPanlSizeY);
            this.pnlFinishSeamList.Location = new Point(seamPanlLocnX, seamPanlLocnY);

            this.pnlSelectedFinishSeam.Size = new Size(slcdPanlSizeX, slcdPanlSizeY);
            this.pnlSelectedFinishSeam.Location = new Point(slcdPanlLocnX, slcdPanlLocnY);
        }
    }
}
