//-------------------------------------------------------------------------------//
// <copyright file="UCFinishPallet.cs" company="Bruun Estimating, LLC">          // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Finish_Controls
{
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Models;
    using FloorMaterialEstimator.Utilities;

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
 
    public partial class UCFinishPallet : UserControl
    {
        public CanvasManager canvasManager = null;

        public Dictionary<string, UCFinish> finishDict = new Dictionary<string, UCFinish>();

        public int finishCount
        {
            get
            {
                return finishDict.Count;
            }
        }

        private UCFinish _selectedFinish = null;

        public UCFinish selectedFinish
        {
            get
            {
                return _selectedFinish;
            }

            set
            {
                _selectedFinish = value;

                foreach (UCFinish ucFinish in finishDict.Values)
                {
                    ucFinish.Selected = ucFinish.fIndex == _selectedFinish.fIndex;
                }
            }
        }

        public UCFinishPallet()
        {
            InitializeComponent();

            this.SizeChanged += UCFinishPallet_SizeChanged;
        }

        private void UCFinishPallet_SizeChanged(object sender, EventArgs e)
        {
            this.pnlFinishList.Size = this.Size;
        }

        public void Init(CanvasManager canvasManager, string finishFilePath)
        {
            this.canvasManager = canvasManager;

            if (string.IsNullOrEmpty(finishFilePath))
            {
                CreateDefaultUCFinish();
            }

            else if (File.Exists(finishFilePath))
            {
                LoadFinish(finishFilePath);
            }

            else
            {
                CreateDefaultUCFinish();
            }

            int finishLocY = 0;

            foreach (UCFinish ucFinish in this.finishDict.Values)
            {
                ucFinish.layer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucFinish.finishName);

                this.pnlFinishList.Controls.Add(ucFinish);

                ucFinish.Location = new Point(0, finishLocY);

                finishLocY += ucFinish.Height + 1;
            }

            selectedFinish = finishDict.Values.First();
        }

        private void LoadFinish(string sFile)
        {
            try
            {
                byte[] outB = Utilities.DecryptFile(sFile);//Utilities.DecryptFile(sFile);

                using (var reader = new MemoryStream(outB))
                {
                    var serializer = new XmlSerializer(typeof(ListFinishLine));

                    ListFinishLine lConfigFinishLine = (ListFinishLine)serializer.Deserialize(reader);

                    if (lConfigFinishLine.lFinish.Count > 0)
                    {
                        for (int i = 0; i < lConfigFinishLine.lFinish.Count; i++)
                        {
                            Color col = Color.FromArgb(lConfigFinishLine.lFinish[i].R, lConfigFinishLine.lFinish[i].G, lConfigFinishLine.lFinish[i].B);

                            UCFinish uc = new UCFinish(this);

                            uc.finishColor = Color.FromArgb((byte)lConfigFinishLine.lFinish[i].A, col);

                            uc.finishName = lConfigFinishLine.lFinish[i].sName;

                            uc.Selected = false;

                            uc.fIndex = i;

                            this.finishDict.Add(uc.finishName, uc);
                        }
                    }

                    else
                    {
                        CreateDefaultUCFinish();
                    }
                }
            }

            catch (Exception ex)
            {
                ;
            }

        }

        private void CreateDefaultUCFinish()
        {

        }

        public void SetSize(System.Drawing.Size containerSize)
        {
            int palletSizeY = 80;
            int finishSizeY = 80;

            if (finishCount > 0)
            {
                finishSizeY = finishDict.Values.First().Height;

                palletSizeY = (finishSizeY + 1) * finishCount + 6;
            }

            if (palletSizeY > containerSize.Height)
            {
                palletSizeY = containerSize.Height;
            }

            this.Size = new System.Drawing.Size(containerSize.Width - 2, palletSizeY);
            this.pnlFinishList.Size = this.Size;
        }
        
    }
}
