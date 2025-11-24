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
    using System.Diagnostics;

    public partial class UCSeamFinishPallet : UserControl
    {
        public FloorMaterialEstimatorBaseForm BaseForm = null;

        public CanvasManager canvasManager = null;

        public DesignState LineAreaMode
        {
            get
            {
                return canvasManager.DesignState;
            }
        }

        //public Dictionary<string, UCSeam> lineTypeDict = new Dictionary<string, UCSeam>();

        private int ucSeamListTotalHeight = 0;

        public List<UCSeamPalletElement> SeamFinishList = new List<UCSeamPalletElement>();

        public int lineCount
        {
            get
            {
                return SeamFinishList.Count;
            }
        }

        private UCSeamPalletElement selectedSeam = null;

        public UCSeamPalletElement SelectedSeam
        {
            get
            {
                return selectedSeam;
            }

            set
            {
                selectedSeam = value;

                foreach (UCSeamPalletElement ucSeam in SeamFinishList)
                {
                    ucSeam.Selected = ucSeam.sIndex == selectedSeam.sIndex;
                }
            }
        }

        public UCSeamFinishPallet()
        {
            InitializeComponent();

            this.SizeChanged += UCSeamPallet_SizeChanged;
        }

        private void UCSeamPallet_SizeChanged(object sender, EventArgs e)
        {
            this.pnlSeamList.Size = this.Size;
        }

        public void Init(FloorMaterialEstimatorBaseForm baseForm, CanvasManager canvasManager, SeamFinishBaseList seamFinishBaseList)
        {
            this.BaseForm = baseForm;

            this.canvasManager = canvasManager;

            Init(seamFinishBaseList);
        }

        public void Init(SeamFinishBaseList seamFinishBaseList)
        {
            for (int i = 0; i < seamFinishBaseList.Count; i++)
            {
                SeamFinishBase finishSeamBase = seamFinishBaseList[i];

                UCSeamPalletElement ucSeam = new UCSeamPalletElement(this)
                {
                    SeamFinish = finishSeamBase,
                    Selected = false,
                    sIndex = i
                };

                this.SeamFinishList.Add(ucSeam);

                ucSeam.AreaModeLayer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucSeam.Guid + "_SeamAreaMode");
                ucSeam.LineModeLayer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucSeam.Guid + "_SeamLineMode");
            }

            int lineLocY = 0;

            this.pnlSeamList.Controls.Clear();

            foreach (UCSeamPalletElement ucSeam in this.SeamFinishList)
            {
                this.pnlSeamList.Controls.Add(ucSeam);

                ucSeam.Location = new Point(8, lineLocY);

                lineLocY += ucSeam.Height + 1;
            }

            if (SeamFinishList.Count > 0)
            {
                SelectedSeam = SeamFinishList[0];
            }

            ucSeamListTotalHeight = lineLocY + SeamFinishList.Count * 40;
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

            this.pnlSeamList.Size = new Size(seamPanlSizeX, seamPanlSizeY);
            this.pnlSeamList.Location = new Point(seamPanlLocnX, seamPanlLocnY);

            this.pnlSelectedFinishSeam.Size = new Size(slcdPanlSizeX, slcdPanlSizeY);
            this.pnlSelectedFinishSeam.Location = new Point(slcdPanlLocnX, slcdPanlLocnY);
        }

        internal void UCLineControlKeyClick(UCSeamPalletElement ucSeam)
        {
            canvasManager.UpdateSeamSelections(ucSeam);
        }
    
        public void Select(int index)
        {
            if (index < 0 || index >= this.SeamFinishList.Count)
            {
                return;
            }

            this.SelectedSeam = this.SeamFinishList[index];
        }

        public void Swap(int loc1Index, int loc2Index)
        {
            Point locn1 = SeamFinishList[loc1Index].Location;
            Point locn2 = SeamFinishList[loc2Index].Location;

            UCSeamPalletElement tempUCLine = SeamFinishList[loc1Index];

            SeamFinishList[loc1Index] = SeamFinishList[loc2Index];
            SeamFinishList[loc2Index] = tempUCLine;

            SeamFinishList[loc1Index].Location = locn1;
            SeamFinishList[loc2Index].Location = locn2;

            SeamFinishList[loc1Index].sIndex = loc1Index;
            SeamFinishList[loc2Index].sIndex = loc2Index;
        }

        public UCSeamPalletElement Add(string seamNameParm, Color seamColor, double lineWidthInPts, short visioDashType)
        {
            Debug.Assert(!lineTypeListContainsName(seamNameParm));

            if (lineTypeListContainsName(seamNameParm))
            {
                return null;
            }

            UCSeamPalletElement ucSeam = new UCSeamPalletElement(this);

            ucSeam.SeamName = seamNameParm;
            ucSeam.SeamColor = seamColor;
            ucSeam.LineWidthInPts = lineWidthInPts;
            ucSeam.VisioDashType = visioDashType;
            ucSeam.AreaModeLayer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucSeam.Guid + "_AreaMode");
            ucSeam.LineModeLayer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucSeam.Guid + "_LineMode");
            ucSeam.bIsFiltered = false;
            ucSeam.sIndex = this.SeamFinishList.Count;

            this.pnlSeamList.Controls.Add(ucSeam);

            int lineLocY = 0;

            if (this.SeamFinishList.Count > 0)
            {
                UCSeamPalletElement lastUCLine = this.SeamFinishList[this.SeamFinishList.Count - 1];

                lineLocY = lastUCLine.Location.Y + lastUCLine.Height + 1;
            }

            ucSeam.Location = new Point(0, lineLocY);

            this.SeamFinishList.Add(ucSeam);
            //this.lineTypeDict.Add(ucLine.LineName, ucLine);

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.Select(ucSeam.sIndex);

            this.Refresh();

            return ucSeam;
        }

        internal UCSeamPalletElement Insert(int selectedElement, string seamNameParm, Color seamColor, int lineWidthInPts, short visioDashType)
        {
            if (this.SeamFinishList.Count <= 0)
            {
                return Add(seamNameParm, seamColor, lineWidthInPts, visioDashType);
            }

            Debug.Assert(!lineTypeListContainsName(seamNameParm));
            Debug.Assert(selectedElement >= 0 && selectedElement < this.SeamFinishList.Count);

            if (lineTypeListContainsName(seamNameParm))
            {
                return null;
            }

            if (selectedElement < 0 || selectedElement >= this.SeamFinishList.Count)
            {
                return null;
            }

            UCSeamPalletElement ucSeam = new UCSeamPalletElement(this);

            ucSeam.SeamName = seamNameParm;
            ucSeam.SeamColor = seamColor;
            ucSeam.LineWidthInPts = lineWidthInPts;
            ucSeam.VisioDashType = visioDashType;
            ucSeam.AreaModeLayer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucSeam.Guid + "_AreaMode");
            ucSeam.LineModeLayer = canvasManager.CurrentPage.VisioPage.Layers.Add(ucSeam.Guid + "_LineMode");
            ucSeam.bIsFiltered = false;

            ucSeam.sIndex = selectedElement;

            this.pnlSeamList.Controls.Add(ucSeam);

            Size ucLineSize = this.SeamFinishList[selectedElement].Size;

            ucSeam.Size = ucLineSize;

            int locnX = this.SeamFinishList[selectedElement].Location.X;
            int locnY = this.SeamFinishList[selectedElement].Location.Y;

            ucSeam.Location = new Point(locnX, locnY);

            this.SeamFinishList.Insert(selectedElement, ucSeam);
            //this.lineTypeDict.Add(ucLine.LineName, ucLine);

            locnY += ucSeam.Height + 1;

            for (int index = selectedElement + 1; index < SeamFinishList.Count; index++)
            {
                UCSeamPalletElement ucSeam1 = this.SeamFinishList[index];

                ucSeam1.sIndex = index;
                ucSeam1.Location = new Point(locnX, locnY);

                locnY += ucSeam1.Height + 1;
            }

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.Select(ucSeam.sIndex);

            this.Refresh();

            return ucSeam;
        }

        private bool lineTypeListContainsName(string seamNameParm)
        {
            foreach (UCSeamPalletElement ucSeam in SeamFinishList)
            {
                if (ucSeam.SeamName == seamNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        internal void Remove(int selectedElement)
        {
            Debug.Assert(selectedElement >= 0 && selectedElement < this.SeamFinishList.Count);

            if (selectedElement < 0 || selectedElement >= this.SeamFinishList.Count)
            {
                return;
            }

            UCSeamPalletElement ucSeam = this.SeamFinishList[selectedElement];

            //this.lineTypeDict.Remove(ucLine.LineName);
            this.SeamFinishList.RemoveAt(selectedElement);

            this.pnlSeamList.Controls.Remove(ucSeam);

            ucSeam.AreaModeLayer.Delete(1);
            ucSeam.LineModeLayer.Delete(1);

            int locnX = ucSeam.Location.X;
            int locnY = ucSeam.Location.Y;

            for (int index = selectedElement; index < this.SeamFinishList.Count; index++)
            {
                Point locn = new Point(locnX, locnY);

                UCSeamPalletElement ucLine1 = this.SeamFinishList[index];

                locnX = ucLine1.Location.X;
                locnY = ucLine1.Location.Y;

                ucLine1.sIndex = index;
                ucLine1.Location = locn;
            }

            if (this.SeamFinishList.Count <= 0)
            {
                return;
            }

            if (selectedElement >= this.SeamFinishList.Count)
            {
                selectedElement = this.SeamFinishList.Count - 1;
            }

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.Select(selectedElement);
        }

        public int TempStoredDashStyleMap(int dashStyle)
        {
            switch (dashStyle)
            {
                case 0: return 1;
                case 1: return 10;
                case 2: return 2;
                case 3: return 4;
                default: return 1;
            }
        }
    }
}
