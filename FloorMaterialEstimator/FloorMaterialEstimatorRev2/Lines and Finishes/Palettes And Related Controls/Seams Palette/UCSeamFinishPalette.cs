//-------------------------------------------------------------------------------//
// <copyright file="UCLinePalette.cs" company="Bruun Estimating, LLC">           // 
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
    //using FloorMaterialEstimator.CanvasManager;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    using Globals;
    using FinishesLib;
    using PaletteLib;
    using Utilities.Supporting_Controls;
    using Graphics;

    public partial class UCSeamFinishPalette : UserControl
    {
        public AreaFinishBaseList AreaFinishBaseList;

        //public FloorMaterialEstimatorBaseForm BaseForm { get; set; }

        //public UCFlowLayoutPanel<UCAreaFinishPaletteElement> ucFlowLayoutPanel;

        public UCFlowLayoutPanel<UCSeamAreaFinishPaletteElement> ucFlowLayoutPanel;

        public List<UCSeamAreaFinishPaletteElement> UCSeamAreaFinishList = new List<UCSeamAreaFinishPaletteElement>();

        public List<UCSeamPaletteElement> SeamFinishList = new List<UCSeamPaletteElement>();

        //public CanvasManager CanvasManager { get; set; } = null;

        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public UCLineFinishSummary selectedLineSummary;

        public DesignState LineAreaMode
        {
            get
            {
                return SystemState.DesignState;
            }
        }


        private int ucSeamListTotalHeight = 0;


        public int lineCount
        {
            get
            {
                return SeamFinishList.Count;
            }
        }

        private UCSeamPaletteElement selectedSeam = null;

        public UCSeamPaletteElement SelectedSeam
        {
            get
            {
                return selectedSeam;
            }

            set
            {
                selectedSeam = value;

                foreach (UCSeamPaletteElement ucSeam in SeamFinishList)
                {
                    ucSeam.Selected = ucSeam.sIndex == selectedSeam.sIndex;
                }
            }
        }

        private UCSeamAreaFinishPaletteElement selectedArea = null;

        public UCSeamAreaFinishPaletteElement SelectedArea
        {
            get
            {
                return selectedArea;
            }

            set
            {
                selectedArea = value;

                foreach (UCSeamAreaFinishPaletteElement ucArea in UCSeamAreaFinishList)
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
        public UCSeamFinishPalette()
        {
            InitializeComponent();

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCSeamAreaFinishPaletteElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.pnlAreaList.Controls.Add(ucFlowLayoutPanel);

            this.SizeChanged += UCSeamPalette_SizeChanged;
        }

        private void UCSeamPalette_SizeChanged(object sender, EventArgs e)
        {
            this.pnlSeamList.Size = this.Size;
        }

        public void Init(
            GraphicsWindow window 
            ,GraphicsPage page
            ,AreaFinishBaseList areaFinishBaseList
            ,SeamFinishBaseList seamFinishBaseList)
        {
            this.Window = window;

            this.Page = page;

            Init(areaFinishBaseList);
            Init(seamFinishBaseList);

            // Link the seam finishes to the appropriate area finishes.

            if (this.selectedLineSummary is null)
            {
                this.selectedLineSummary = new UCLineFinishSummary(this.Window, this.Page);

                this.pnlLineFinish.Controls.Add(this.selectedLineSummary);
            }

            //BaseForm.seamPalette.selectedLineSummary.SetLineFinish(0);

            this.pnlAreaList.BringToFront();

            this.rbnAreaSelection.CheckedChanged += RbnAreaSelection_CheckedChanged;
            this.rbnSeamSelection.CheckedChanged += RbnSeamSelection_CheckedChanged;

            AreaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
            AreaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;

            StaticGlobals.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

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

            StaticGlobals.LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;

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

            StaticGlobals.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;
        }

        private void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            for (int i = 0; i < UCSeamAreaFinishList.Count; i++)
            {
                if (i == itemIndex)
                {
                    UCSeamAreaFinishList[i].Selected = true;
                }

                else
                {
                    UCSeamAreaFinishList[i].Selected = false;
                }
            }
        }

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase item)
        {
            UCSeamAreaFinishPaletteElement ucSeamAreaFinish = new UCSeamAreaFinishPaletteElement(this, item);

            UCSeamAreaFinishList.Add(ucSeamAreaFinish);

            UpdateSeamAreaPalette();
        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase item, int position)
        {
            UCSeamAreaFinishPaletteElement ucSeamAreaFinish = new UCSeamAreaFinishPaletteElement(this, item);

            UCSeamAreaFinishList.Insert(position, ucSeamAreaFinish);

            UpdateSeamAreaPalette();
        }

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            if (position1 == position2)
            {
                return;
            }

            UCSeamAreaFinishPaletteElement finish1 = this.UCSeamAreaFinishList[position1];
            UCSeamAreaFinishPaletteElement finish2 = this.UCSeamAreaFinishList[position2];

            this.UCSeamAreaFinishList[position1] = finish2;
            this.UCSeamAreaFinishList[position2] = finish1;

            UpdateSeamAreaPalette();
        }

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            UCSeamAreaFinishList.RemoveAt(position);

            UpdateSeamAreaPalette();
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

            this.UCSeamAreaFinishList.Clear();

            this.AreaFinishBaseList = areaFinishBaseList;

            int indx = 0;

            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                UCSeamAreaFinishPaletteElement ucSeamAreaFinish = new UCSeamAreaFinishPaletteElement(this, areaFinishBase);

                this.UCSeamAreaFinishList.Add(ucSeamAreaFinish);

                ucSeamAreaFinish.PositionOnPalette = indx++;

                ucSeamAreaFinish.ControlClicked += UcSeamAreaFinish_ControlClicked;
            }

            foreach (UCSeamAreaFinishPaletteElement ucSeamAreaFinish in UCSeamAreaFinishList)
            {
                if (ucSeamAreaFinish.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    this.ucFlowLayoutPanel.Add(ucSeamAreaFinish);

                    ucFlowLayoutPanel.AutoScrollPosition = new Point(ucSeamAreaFinish.Left, ucSeamAreaFinish.Top);
                }
            }

            SelectArea(areaFinishBaseList.SelectedItemIndex);
        }


        private void UcSeamAreaFinish_ControlClicked(UCSeamAreaFinishPaletteElement sender)
        {
            AreaFinishBaseList.SelectElem(sender.AreaFinishBase);
        }

        internal void UpdateSeamAreaPalette()
        {
            this.ucFlowLayoutPanel.Controls.Clear();

            foreach (UCSeamAreaFinishPaletteElement ucSeamAreaFinish in UCSeamAreaFinishList)
            {
                if (ucSeamAreaFinish.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    this.ucFlowLayoutPanel.Add(ucSeamAreaFinish);

                    ucFlowLayoutPanel.AutoScrollPosition = new Point(ucSeamAreaFinish.Left, ucSeamAreaFinish.Top);

                    ucSeamAreaFinish.PositionOnPalette = GetChildIndex(ucSeamAreaFinish);
                }
            }
        }

        public void Init(SeamFinishBaseList seamFinishBaseList)
        {
            HashSet<SeamFinishBase> usedFinishes = usedSeamFinishes();

            for (int i = 0; i < seamFinishBaseList.Count; i++)
            {
                SeamFinishBase finishSeamBase = seamFinishBaseList[i];

                UCSeamPaletteElement ucSeam = new UCSeamPaletteElement(this.Window, this.Page, this, finishSeamBase, false);
                
                ucSeam.lblSeamName.Text = finishSeamBase.SeamName;

                this.SeamFinishList.Add(ucSeam);
            }

            addSeamsToPalette(usedFinishes);  
        }

        public void UpdateSeamList()
        {
            HashSet<SeamFinishBase> usedFinishes = usedSeamFinishes();

            if (seamListHasChanged(usedFinishes))
            {
                addSeamsToPalette(usedFinishes);
            }
        }

        private void addSeamsToPalette(HashSet<SeamFinishBase> usedSeamFinishes)
        {
            int lineLocY = 0;

            this.pnlSeamList.Controls.Clear();

            int i = 0;

            foreach (UCSeamPaletteElement ucSeam in this.SeamFinishList)
            {
                if (!usedSeamFinishes.Contains(ucSeam.SeamFinishBase))
                {
                    continue;
                }

                ucSeam.sIndex = i++;

                this.pnlSeamList.Controls.Add(ucSeam);

                ucSeam.Location = new Point(2, lineLocY);

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

            foreach (UCSeamPaletteElement finishElement in pnlSeamList.Controls)
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
            this.Size = new Size(containerSize.Width, containerSize.Height);
            this.Location = new Point(0, 0);

            this.pnlSeamList.Size = new Size(containerSize.Width, containerSize.Height - this.pnlLineFinish.Size.Height);
            this.pnlSeamList.Location = new Point(0, 0);

            this.pnlAreaList.Size = new Size(containerSize.Width, containerSize.Height - this.pnlLineFinish.Size.Height);
            this.pnlAreaList.Location = new Point(0, 0);

            this.ucFlowLayoutPanel.Size = new Size(containerSize.Width, containerSize.Height - this.pnlLineFinish.Size.Height);
            this.ucFlowLayoutPanel.Location = new Point(0, 0);

            this.selectedLineSummary.Location = new Point(0, this.grbSelection.Location.Y + this.grbSelection.Size.Height);
        }

        //internal void UCLineControlKeyClick(UCSeamPaletteElement ucSeam)
        //{
        //    CanvasManager.UpdateSeamSelections(ucSeam);
        //}
    
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

            this.SelectedArea = this.UCSeamAreaFinishList[index];
        }

        public void Swap(int loc1Index, int loc2Index)
        {
            Point locn1 = SeamFinishList[loc1Index].Location;
            Point locn2 = SeamFinishList[loc2Index].Location;

            UCSeamPaletteElement tempUCLine = SeamFinishList[loc1Index];

            SeamFinishList[loc1Index] = SeamFinishList[loc2Index];
            SeamFinishList[loc2Index] = tempUCLine;

            SeamFinishList[loc1Index].Location = locn1;
            SeamFinishList[loc2Index].Location = locn2;

            SeamFinishList[loc1Index].sIndex = loc1Index;
            SeamFinishList[loc2Index].sIndex = loc2Index;
        }

        public UCSeamPaletteElement Add(string seamNameParm, Color seamColor, double lineWidthInPts, short visioDashType)
        {
            Debug.Assert(!lineTypeListContainsName(seamNameParm));

            if (lineTypeListContainsName(seamNameParm))
            {
                return null;
            }

            UCSeamPaletteElement ucSeam = new UCSeamPaletteElement(this.Window, this.Page, this);

            ucSeam.SeamName = seamNameParm;
            ucSeam.SeamColor = seamColor;
            ucSeam.LineWidthInPts = lineWidthInPts;
            ucSeam.VisioDashType = visioDashType;

            //ucSeam.AreaDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPaletteElement]" + ucSeam.SeamName + "_SeamAreaDesignState", GraphicsLayerType.Dynamic);
            //ucSeam.LineDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPaletteElement]" + ucSeam.SeamName + "_SeamLineDesignState", GraphicsLayerType.Dynamic);
            //ucSeam.SeamDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPaletteElement]" + ucSeam.SeamName + "_SeamSeamDesignState", GraphicsLayerType.Dynamic);

            ucSeam.bIsFiltered = false;
            ucSeam.sIndex = this.SeamFinishList.Count;

            this.pnlSeamList.Controls.Add(ucSeam);

            int lineLocY = 0;

            if (this.SeamFinishList.Count > 0)
            {
                UCSeamPaletteElement lastUCLine = this.SeamFinishList[this.SeamFinishList.Count - 1];

                lineLocY = lastUCLine.Location.Y + lastUCLine.Height + 1;
            }

            ucSeam.Location = new Point(0, lineLocY);

            this.SeamFinishList.Add(ucSeam);
            //this.lineTypeDict.Add(ucLine.LineName, ucLine);

            int width = SystemState.TbcPageAreaLine.Width;
            int height = SystemState.TbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.SelectSeam(ucSeam.sIndex);

            this.Refresh();

            return ucSeam;
        }

        internal UCSeamPaletteElement Insert(int selectedElement, string seamNameParm, Color seamColor, int lineWidthInPts, short visioDashType)
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

            UCSeamPaletteElement ucSeam = new UCSeamPaletteElement(this.Window, this.Page, this);

            ucSeam.SeamName = seamNameParm;
            ucSeam.SeamColor = seamColor;
            ucSeam.LineWidthInPts = lineWidthInPts;
            ucSeam.VisioDashType = visioDashType;

            //ucSeam.AreaDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPaletteElement]" + ucSeam.SeamName + "_SeamAreaDesignState", GraphicsLayerType.Dynamic);

            //ucSeam.LineDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPaletteElement]" + ucSeam.SeamName + "_SeamLineDesignState", GraphicsLayerType.Dynamic);

            //ucSeam.SeamDesignStateLayer = new GraphicsLayer(Window, Page, "[UCSeamFinishPaletteElement]" + ucSeam.SeamName + "_SeamSeamDesignState", GraphicsLayerType.Dynamic);

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
                UCSeamPaletteElement ucSeam1 = this.SeamFinishList[index];

                ucSeam1.sIndex = index;
                ucSeam1.Location = new Point(locnX, locnY);

                locnY += ucSeam1.Height + 1;
            }

            int width = SystemState.TbcPageAreaLine.Width;
            int height = SystemState.TbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.SelectSeam(ucSeam.sIndex);

            this.Refresh();

            return ucSeam;
        }

        internal void ClearTallyDisplays()
        {
            foreach (UCSeamAreaFinishPaletteElement areaFinishPaletteElement in UCSeamAreaFinishList)
            {
                areaFinishPaletteElement.ClearTallyDisplays();
            }
        }

        private bool lineTypeListContainsName(string seamNameParm)
        {
            foreach (UCSeamPaletteElement ucSeam in SeamFinishList)
            {
                if (ucSeam.SeamName == seamNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        private HashSet<SeamFinishBase> usedSeamFinishes()
        {
            HashSet<SeamFinishBase> rtrnList = new HashSet<SeamFinishBase>();

            foreach (AreaFinishBase areaFinishBase in StaticGlobals.AreaFinishBaseList)
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
        
        private int GetChildIndex(UCSeamAreaFinishPaletteElement ucFinish) => this.ucFlowLayoutPanel.GetChildIndex(ucFinish);

        //private void SetChildIndex(UCSeamAreaFinishPaletteElement ucFinish, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucFinish, position);

        private HashSet<SeamFinishBase> currSeamFinishes()
        {
            HashSet<SeamFinishBase> rtrnList = new HashSet<SeamFinishBase>();

            foreach (UCSeamPaletteElement seamElement in this.SeamFinishList)
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

        internal void Clear()
        {
            this.rbnAreaSelection.CheckedChanged -= RbnAreaSelection_CheckedChanged;
            this.rbnSeamSelection.CheckedChanged -= RbnSeamSelection_CheckedChanged;

            AreaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected -= AreaFinishBaseList_ItemSelected;
            AreaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;

            StaticGlobals.LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;

            foreach (UCSeamPaletteElement palletElement in SeamFinishList)
            {
                palletElement.Delete();
            }

            // The following needs to be implemented at some point.

            //foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            //{
            //    areaFinishBase.ControlClicked += UcSeamAreaFinish_ControlClicked;
            //}
        }
    }
}
