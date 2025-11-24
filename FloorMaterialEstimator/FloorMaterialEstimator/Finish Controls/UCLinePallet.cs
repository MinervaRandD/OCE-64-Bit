//-------------------------------------------------------------------------------//
// <copyright file="UCLinePallet.cs" company="Bruun Estimating, LLC">            // 
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

    public partial class UCLinePallet : UserControl
    {
        public CanvasManager canvasManager = null;

        public Dictionary<string, UCLine> lineTypeDict = new Dictionary<string, UCLine>();

        public int lineCount
        {
            get
            {
                return lineTypeDict.Count;
            }
        }

        private UCLine _selectedLine = null;

        public UCLine selectedLine
        {
            get
            {
                return _selectedLine;
            }

            set
            {
                _selectedLine = value;

                foreach (UCLine ucLine in lineTypeDict.Values)
                {
                    ucLine.bSelected = ucLine.lIndex == _selectedLine.lIndex;
                }
            }
        }

        public UCLinePallet()
        {
            InitializeComponent();

            this.SizeChanged += UCFinishPallet_SizeChanged;
        }

        private void UCFinishPallet_SizeChanged(object sender, EventArgs e)
        {
            this.pnlLineList.Size = this.Size;
        }

        public void Init(CanvasManager canvasManager, string lineFilePath)
        {
            this.canvasManager = canvasManager;

            if (string.IsNullOrEmpty(lineFilePath))
            {
                CreateDefaultUCLine();
            }

            if (File.Exists(lineFilePath))
            {
                LoadLine(lineFilePath);
            }

            else
            {
                CreateDefaultUCLine();
            }

            int lineLocY = 0;

            foreach (UCLine ucLine in this.lineTypeDict.Values)
            {
                this.pnlLineList.Controls.Add(ucLine);

                ucLine.Location = new Point(0, lineLocY);

                lineLocY += ucLine.Height + 1;
            }

            selectedLine = lineTypeDict.Values.First();
        }

        private void LoadLine(string sFile)
        {
            try
            {
                byte[] outB = Utilities.DecryptFile(sFile);

                using (var reader = new MemoryStream(outB))
                {
                    var serializer = new XmlSerializer(typeof(ListFinishLine));

                    ListFinishLine lConfigFinishLine = (ListFinishLine)serializer.Deserialize(reader);

                    if (lConfigFinishLine.lLine.Count > 0)
                    {
                        for (int i = 0; i < lConfigFinishLine.lLine.Count; i++)
                        {
                            Color col = Color.FromArgb(lConfigFinishLine.lLine[i].R, lConfigFinishLine.lLine[i].G, lConfigFinishLine.lLine[i].B);

                            UCLine ucLine = new UCLine(this);

                            ucLine.lineColor = Color.FromArgb((byte)lConfigFinishLine.lLine[i].A, col);

                            ucLine.lineStyle = (short)lConfigFinishLine.lLine[i].DashStyle;

                            ucLine.LineWidthInPts = ((double)lConfigFinishLine.lLine[i].Width)  ;

                            ucLine.lineName = lConfigFinishLine.lLine[i].sName;

                            ucLine.bSelected = false;

                            ucLine.lIndex = i;

                            ucLine.bBase = lConfigFinishLine.lLine[i];

                            this.lineTypeDict.Add(ucLine.lineName, ucLine);

                            ucLine.layer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucLine.lineName);
                        }
                    }

                    else
                    {
                        // Set up a default line type

                        CreateDefaultUCLine();
                    }

                }
            }

            catch (Exception ex)
            {
                ;
            }

        }

        private void CreateDefaultUCLine()
        {
            Color col = Color.FromArgb(0, 0, 0);

            UCLine ucLine = new UCLine(this);

            ucLine.lineColor = Color.FromArgb(255, col);

            ucLine.lineStyle = 1;

            ucLine.LineWidthInPts = 2;

            ucLine.lineName = "Default";

            ucLine.bSelected = false;

            ucLine.lIndex = 0;

            ucLine.bBase = new Base(
                1, "Default", 255, ucLine.lineColor, 2, 1, 1, 1);

            this.lineTypeDict.Add(ucLine.lineName, ucLine);

            ucLine.layer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucLine.lineName);
        }

        public void SetSize(System.Drawing.Size containerSize)
        {
            int palletSizeY = 80;
            int finishSizeY = 80;

            if (lineCount > 0)
            {
                finishSizeY = lineTypeDict.Values.First().Height;

                palletSizeY = (finishSizeY + 1) * lineCount + 6;
            }

            if (palletSizeY > containerSize.Height)
            {
                palletSizeY = containerSize.Height;
            }

            this.Size = new System.Drawing.Size(containerSize.Width - 2, palletSizeY);
            this.pnlLineList.Size = this.Size;
        }

        internal void UCLineControlKeyClick(UCLine ucLine)
        {
            canvasManager.UpdateLineSelections(ucLine);
        }

        internal void UCLineSimpleClick(UCLine ucLine)
        {
            selectedLine = ucLine;

            canvasManager.UpdateLineSelections(ucLine);
        }

        public void MoveLine(GraphicsLine line, UCLine currUCLine, UCLine nextUCLine)
        {
            if (nextUCLine == currUCLine)
            {
                return;
            }

            currUCLine.RemoveLine(line);
            nextUCLine.AddLine(line);
        }

        internal void MoveLineToLineType(GraphicsLine line, UCLine nextUCLine)
        {
            UCLine currUCLine = line.ucLine;

            if (nextUCLine == currUCLine)
            {
                return;
            }

            currUCLine.RemoveLine(line);
            nextUCLine.AddLine(line);

        }
    }
}
