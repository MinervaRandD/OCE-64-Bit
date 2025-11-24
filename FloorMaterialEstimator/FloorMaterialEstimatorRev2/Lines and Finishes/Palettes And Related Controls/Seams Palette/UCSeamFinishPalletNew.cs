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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using CanvasLib.Design_States_and_Modes;
    using FinishesLib;
    using Utilities.Supporting_Controls;
    using Graphics;

    public partial class UCSeamFinishPallet : UserControl
    {
        public AreaFinishBaseList AreaFinishBaseList;

        public FloorMaterialEstimatorBaseForm BaseForm { get; set; }

        //public UCFlowLayoutPanel<UCAreaFinishPalletElement> ucFlowLayoutPanel;

        public UCFlowLayoutPanel<UCAreaFinishPalletElement> ucFlowLayoutPanel;

        public List<UCAreaFinishPalletElement> UCAreaFinishList = new List<UCAreaFinishPalletElement>();

        public List<UCSeamPalletElement> SeamFinishList = new List<UCSeamPalletElement>();

        public CanvasManager CanvasManager { get; set; } = null;

        public GraphicsWindow Window => CanvasManager.Window;

        public GraphicsPage Page => CanvasManager.Page;

        public UCLineFinishSummary selectedLineSummary;

        public DesignState LineAreaMode
        {
            get
            {
                return CanvasManager.DesignState;
            }
        }

        internal void Clear()
        {
            throw new NotImplementedException();
        }

        private int ucSeamListTotalHeight = 0;


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

        internal void DeleteLayers()
        {
            foreach (UCSeamPalletElement seamPalletElement in SeamFinishList)
            {
                seamPalletElement.DeleteLayers();
            }
        }

        private UCAreaFinishPalletElement selectedArea = null;

        public UCAreaFinishPalletElement SelectedArea
        {
            get
            {
                return selectedArea;
            }

            set
            {
                selectedArea = value;

                foreach (UCAreaFinishPalletElement ucArea in UCAreaFinishList)
                {
                    if (ucArea == selectedArea)
                    {
                        ucArea.Selected = true;
                    }
                    
                    else
                    {
                        ucArea.Selected = false;
                    }

                }
            }
        }
        public UCSeamFinishPallet()
        {
            InitializeComponent();

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCAreaFinishPalletElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.pnlAreaList.Controls.Add(ucFlowLayoutPanel);

            this.SizeChanged += UCSeamPallet_SizeChanged;
        }

        private void UCSeamPallet_SizeChanged(object sender, EventArgs e)
        {
            this.pnlSeamList.Size = this.Size;
        }

        public void Init(
            FloorMaterialEstimatorBaseForm baseForm,
            CanvasManager canvasManager,
            AreaFinishBaseList areaFinishBaseList,
            SeamFinishBaseList seamFinishBaseList)
        {
            this.BaseForm = baseForm;

            this.CanvasManager = canvasManager;

            Init(areaFinishBaseList);
            Init(seamFinishBaseList);

            // Link the seam finishes to the appropriate area finishes.

            this.selectedLineSummary = new UCLineFinishSummary(this.CanvasManager);


            BaseForm.seamPallet.selectedLineSummary.SetLineFinish(0);

            this.pnlLineFinish.Controls.Add(this.selectedLineSummary);

            this.pnlAreaList.BringToFront();

            this.rbnAreaSelection.CheckedChanged += RbnAreaSelection_CheckedChanged;
            this.rbnSeamSelection.CheckedChanged += RbnSeamSelection_CheckedChanged;

            AreaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
            AreaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;

            BaseForm.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

        }

        private void LineFinishBaseList_ItemSelected(int itemIndex)
        {
            this.selectedLineSummary.SetLineFinish(itemIndex);
        }

        public void Reinit(AreaFinishBaseList areaFinishBaseList, SeamFinishBaseList seamFinishBaseList)
        {
            AreaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected -= AreaFinishBaseList_ItemSelected;
            AreaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;

            BaseForm.LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;

            Init(areaFinishBaseList);
            Init(seamFinishBaseList);

            this.pnlAreaList.BringToFront();

            this.rbnAreaSelection.CheckedChanged += RbnAreaSelection_CheckedChanged;
            this.rbnSeamSelection.CheckedChanged += RbnSeamSelection_CheckedChanged;

            AreaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
            AreaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;

            BaseForm.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;
        }

        private void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            for (int i = 0; i < UCAreaFinishList.Count; i++)
            {
                if (i == itemIndex)
                {
                    UCAreaFinishList[i].Selected = true;
                }

                else
                {
                    UCAreaFinishList[i].Selected = false;
                }
            }
        }

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase item)
        {
            UCAreaFinishPalletElement ucAreaFinish = BaseForm.areaPallet[item.Guid];

            UCAreaFinishList.Add(ucAreaFinish);

            UpdateSeamAreaPallet();
        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase item, int position)
        {
            UCAreaFinishPalletElement ucAreaFinish = BaseForm.areaPallet[item.Guid];

            UCAreaFinishList.Insert(position, ucAreaFinish);

            UpdateSeamAreaPallet();
        }

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            if (position1 == position2)
            {
                return;
            }

            UCAreaFinishPalletElement finish1 = this.UCAreaFinishList[position1];
            UCAreaFinishPalletElement finish2 = this.UCAreaFinishList[position2];

            this.UCAreaFinishList[position1] = finish2;
            this.UCAreaFinishList[position2] = finish1;

            UpdateSeamAreaPallet();
        }

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            UCAreaFinishList.RemoveAt(position);

            UpdateSeamAreaPallet();
        }

        private void RbnAreaSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnAreaSelection.Checked)
            {
                this.pnlAreaList.BringToFront();
            }
        }

        private void RbnSeamSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnSeamSelection.Checked)
            {
                this.pnlSeamList.BringToFront();
            }
        }


        public void Init(AreaFinishBaseList areaFinishBaseList)
        {
            this.ucFlowLayoutPanel.Controls.Clear();

            this.UCAreaFinishList.Clear();

            this.AreaFinishBaseList = areaFinishBaseList;

            int indx = 0;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                UCAreaFinishPalletElement uAreaFinish = BaseForm.areaPallet[areaFinishBase.Guid];

                this.UCAreaFinishList.Add(uAreaFinish);

                uAreaFinish.PositionOnPallet = indx++;

                uAreaFinish.ControlClicked += UcSeamAreaFinish_ControlClicked;
            }

            foreach (UCAreaFinishPalletElement ucSeamAreaFinish in UCAreaFinishList)
            {
                if (ucSeamAreaFinish.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    this.ucFlowLayoutPanel.Add(ucSeamAreaFinish);

                    ucFlowLayoutPanel.AutoScrollPosition = new Point(ucSeamAreaFinish.Left, ucSeamAreaFinish.Top);
                }
            }

            SelectArea(areaFinishBaseList.SelectedItemIndex);
        }


        private void UcSeamAreaFinish_ControlClicked(UCAreaFinishPalletElement sender)
        {
            AreaFinishBaseList.SelectElem(sender.AreaFinishBase);
        }

        internal void UpdateSeamAreaPallet()
        {
            this.ucFlowLayoutPanel.Controls.Clear();

            foreach (UCAreaFinishPalletElement ucSeamAreaFinish in UCAreaFinishList)
            {
                if (ucSeamAreaFinish.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    this.ucFlowLayoutPanel.Add(ucSeamAreaFinish);

                    ucFlowLayoutPanel.AutoScrollPosition = new Point(ucSeamAreaFinish.Left, ucSeamAreaFinish.Top);

                    ucSeamAreaFinish.PositionOnPallet = GetChildIndex(ucSeamAreaFinish);
                }
            }
        }

        public void Init(SeamFinishBaseList seamFinishBaseList)
        {
#if false
            HashSet<SeamFinishBase> usedFinishes = usedSeamFinishes();

            for (int i = 0; i < seamFinishBaseList.Count; i++)
            {
                SeamFinishBase finishSeamBase = seamFinishBaseList[i];

                UCSeamPalletElement ucSeam = new UCSeamPalletElement(this, finishSeamBase, false);
                
                ucSeam.lblSeamName.Text = finishSeamBase.SeamName;

                this.SeamFinishList.Add(ucSeam);

                ucSeam.AreaDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamPalletElement]" + ucSeam.SeamName + "_AreaDesignState", GraphicsLayerType.Dynamic);
                ucSeam.LineDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamPalletElement]" + ucSeam.SeamName + "_LineDesignState", GraphicsLayerType.Dynamic);
                ucSeam.SeamDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamPalletElement]" + ucSeam.SeamName + "_SeamDesignState", GraphicsLayerType.Dynamic);
            }

            addSeamsToPallet(usedFinishes);  
#endif
        }

        public void UpdateSeamList()
        {
            HashSet<SeamFinishBase> usedFinishes = usedSeamFinishes();

            if (seamListHasChanged(usedFinishes))
            {
                addSeamsToPallet(usedFinishes);
            }
        }

        private void addSeamsToPallet(HashSet<SeamFinishBase> usedSeamFinishes)
        {
            int lineLocY = 0;

            this.pnlSeamList.Controls.Clear();

            int i = 0;

            foreach (UCSeamPalletElement ucSeam in this.SeamFinishList)
            {
                if (!usedSeamFinishes.Contains(ucSeam.SeamFinishBase))
                {
                    continue;
                }

                ucSeam.sIndex = i++;

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

        private bool seamListHasChanged(HashSet<SeamFinishBase> usedFinishes)
        {
            if (pnlSeamList.Controls.Count != usedFinishes.Count)
            {
                return true;
            }

            foreach (UCSeamPalletElement finishElement in pnlSeamList.Controls)
            {
                if (!usedFinishes.Contains(finishElement.SeamFinishBase))
                {
                    return true;
                }
            }

            return false;
        }

        public void SetSize(System.Drawing.Size containerSize)
        {
            int cntnSizeX = containerSize.Width;
            int cntnSizeY = containerSize.Height;

            int cntlSizeX = cntnSizeX - 2;
            int cntlSizeY = cntnSizeY - 120;

            this.Size = new Size(cntlSizeX, cntlSizeY);
            this.Location = new Point(1, 1);


            int slcdPanlSizeX = cntlSizeX - 32;
            int slcdPanlSizeY = pnlLineFinish.Height;

            int elemSizeX = cntlSizeX - 2;

            int grbxSizeX = elemSizeX;
            int grbxSizeY = this.grbSelection.Height;

            int grbxLocnX = 1;
            int grbxLocnY = cntlSizeY - grbxSizeY - 2;

            this.grbSelection.Size = new Size(grbxSizeX, grbxSizeY);
            this.grbSelection.Location = new Point(grbxLocnX, grbxLocnY);

            int panlSizeX = elemSizeX;
            int panlSizeY = cntlSizeY - grbxSizeY - slcdPanlSizeY - 16;
           
            this.pnlSeamList.Size = new Size(panlSizeX, panlSizeY);
            this.pnlSeamList.Location = new Point(1, 1);

            this.pnlAreaList.Size = new Size(panlSizeX, panlSizeY);
            this.pnlAreaList.Location = new Point(1, 1);

            this.ucFlowLayoutPanel.Size = new Size(panlSizeX - 2, panlSizeY - 2);
            this.ucFlowLayoutPanel.Location = new Point(1, 1);


            int slcdPanlLocnX = 1;
            int slcdPanlLocnY = pnlAreaList.Location.Y + pnlAreaList.Height + 8;

            this.pnlLineFinish.Size = new Size(slcdPanlSizeX, slcdPanlSizeY);
            this.pnlLineFinish.Location = new Point(slcdPanlLocnX, slcdPanlLocnY);
        }

        internal void UCLineControlKeyClick(UCSeamPalletElement ucSeam)
        {
            CanvasManager.UpdateSeamSelections(ucSeam);
        }
    
        public void SelectSeam(int index)
        {
            if (index < 0 || index >= this.SeamFinishList.Count)
            {
                return;
            }

            this.SelectedSeam = this.SeamFinishList[index];
        }

        private void SelectArea(int index)
        {
            if (index < 0 || index >= this.AreaFinishBaseList.Count)
            {
                return;
            }

            this.SelectedArea = this.UCAreaFinishList[index];
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
#if false
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

            ucSeam.AreaDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPalletElement]" + ucSeam.SeamName + "_SeamAreaDesignState", GraphicsLayerType.Dynamic);
            ucSeam.LineDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPalletElement]" + ucSeam.SeamName + "_SeamLineDesignState", GraphicsLayerType.Dynamic);
            ucSeam.SeamDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPalletElement]" + ucSeam.SeamName + "_SeamSeamDesignState", GraphicsLayerType.Dynamic);

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

            this.SelectSeam(ucSeam.sIndex);

            this.Refresh();

            return ucSeam;
#endif
            return null;
        }

        internal UCSeamPalletElement Insert(int selectedElement, string seamNameParm, Color seamColor, int lineWidthInPts, short visioDashType)
        {
#if false
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

            ucSeam.AreaDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPalletElement]" + ucSeam.SeamName + "_SeamAreaDesignState", GraphicsLayerType.Dynamic);

            ucSeam.LineDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPalletElement]" + ucSeam.SeamName + "_SeamLineDesignState", GraphicsLayerType.Dynamic);

            ucSeam.SeamDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPalletElement]" + ucSeam.SeamName + "_SeamSeamDesignState", GraphicsLayerType.Dynamic);

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

            this.SelectSeam(ucSeam.sIndex);

            this.Refresh();

            return ucSeam;
#endif
            return null;
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

            // Order is important here since the Delete1 function clears the Guid.

            Page.DeleteLayer(ucSeam.AreaDesignStateLayer);
            Page.DeleteLayer(ucSeam.LineDesignStateLayer);
            Page.DeleteLayer(ucSeam.SeamDesignStateLayer);

            ucSeam.AreaDesignStateLayer.Delete1();
            ucSeam.LineDesignStateLayer.Delete1();
            ucSeam.SeamDesignStateLayer.Delete1();

            ucSeam.AreaDesignStateLayer = null;
            ucSeam.LineDesignStateLayer = null;
            ucSeam.SeamDesignStateLayer = null;

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

            this.SelectSeam(selectedElement);
        }

        private HashSet<SeamFinishBase> usedSeamFinishes()
        {
            HashSet<SeamFinishBase> rtrnList = new HashSet<SeamFinishBase>();

            foreach (AreaFinishBase areaFinishBase in BaseForm.AreaFinishBaseList)
            {
                if (areaFinishBase.MaterialsType == MaterialsType.Tiles)
                {
                    continue;
                }

                if (areaFinishBase.SeamFinishBase is null)
                {
                    continue;
                }

                if (rtrnList.Contains(areaFinishBase.SeamFinishBase))
                {
                    continue;
                }

                rtrnList.Add(areaFinishBase.SeamFinishBase);
            }

            return rtrnList;
        }
        
        private int GetChildIndex(UCAreaFinishPalletElement ucFinish) => this.ucFlowLayoutPanel.GetChildIndex(ucFinish);

        //private void SetChildIndex(UCAreaFinishPalletElement ucFinish, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucFinish, position);

        private HashSet<SeamFinishBase> currSeamFinishes()
        {
            HashSet<SeamFinishBase> rtrnList = new HashSet<SeamFinishBase>();

            foreach (UCSeamPalletElement seamElement in this.SeamFinishList)
            {
                rtrnList.Add(seamElement.SeamFinishBase);
            }

            return rtrnList;
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
