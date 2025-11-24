using FloorMaterialEstimator.Finish_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FloorMaterialEstimator.Finish_Controls;

namespace FloorMaterialEstimator
{
    public partial class FloorMaterialEstimatorBaseForm2 : Form
    {
        private int cntlPanesBaseLocY = 32;
        private int pageAreaLineWidth = 272;

        public DrawingForm drawingForm;

        public UCFinishPallet finishPallet;
        public UCLinePallet linePallet;

        public FloorMaterialEstimatorBaseForm2()
        {
            InitializeComponent();

            drawingForm = new DrawingForm();

            this.tbcPageAreaLine.Location = new Point(0, cntlPanesBaseLocY);

            string finishIniFilePath = string.Empty;

            if (Program.AppConfig.ContainsKey("finishinifilepath"))
            {
                finishIniFilePath = Program.AppConfig["finishinifilepath"];
            }

            finishPallet = new UCFinishPallet();
            finishPallet.Init(drawingForm, finishIniFilePath);

            linePallet = new UCLinePallet();
            linePallet.Init(drawingForm, finishIniFilePath);

            this.tbpAreas.Controls.Add(finishPallet);
            this.tbpLines.Controls.Add(linePallet);

            setTbcPageAreaSize();

            this.SizeChanged += FloorMaterialEstimatorBaseForm_SizeChanged;
           
            
        }

        private void FloorMaterialEstimatorBaseForm_SizeChanged(object sender, EventArgs e)
        {
            setTbcPageAreaSize();
        }

        private void setTbcPageAreaSize()
        {
            this.tbcPageAreaLine.Size = new Size(pageAreaLineWidth, this.Height - cntlPanesBaseLocY);

            finishPallet.SetSize(new Size(this.tbcPageAreaLine.Width, this.tbcPageAreaLine.Height-32));
            linePallet.SetSize(new Size(this.tbcPageAreaLine.Width, this.tbcPageAreaLine.Height - 32));
        }

    }
}
