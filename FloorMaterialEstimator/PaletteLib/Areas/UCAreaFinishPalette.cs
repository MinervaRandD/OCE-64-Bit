//-------------------------------------------------------------------------------//
// <copyright file="UCFinishPalette.cs" company="Bruun Estimating, LLC">          // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace PaletteLib
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Utilities.Supporting_Controls;
    using FinishesLib;
    using System.Diagnostics;
    using System.Collections;
    using Globals;
    using Graphics;

    public partial class UCAreaFinishPalette : UserControl, IEnumerable<UCAreaFinishPaletteElement>
    {
        public GraphicsWindow Window { get; set; }

        public GraphicsPage Page { get; set; }

        public AreaFinishBaseList AreaFinishBaseList => FinishGlobals.AreaFinishBaseList;

        public UCFlowLayoutPanel<UCAreaFinishPaletteElement> ucFlowLayoutPanel;

        public UCLineFinishSummary SelectedLineSummary;

        public UCZeroLineSummary zeroLineSummary;

        public Dictionary<string, UCAreaFinishPaletteElement> UCAreaFinishDict = new Dictionary<string, UCAreaFinishPaletteElement>();

        public IEnumerable<UCAreaFinishPaletteElement> UCAreaFinishes => UCAreaFinishDict.Values;

        public DesignState DesignState => SystemState.DesignState;
       
        public int SelectedItemIndex => AreaFinishBaseList.SelectedItemIndex;

        public delegate void SelectedFinishEventHandler(UCAreaFinishPaletteElement selectedFinish);

        public event SelectedFinishEventHandler FinishSelected;

        public UCAreaFinishPaletteElement SelectedFinish
        {
            get
            {
                return this[SelectedItemIndex];
            }
        }
        
        public UCAreaFinishPalette()
        {
            InitializeComponent();

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCAreaFinishPaletteElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.Controls.Add(ucFlowLayoutPanel);

        }

        public void Init(
             GraphicsWindow window
            , GraphicsPage page
           )
        {
            this.Window = window;

            this.Page = page;

            this.ucFlowLayoutPanel.Controls.Clear();

            this.UCAreaFinishDict.Clear();
 
            //this.AreaFinishManagerList = areaFinishManagerList;

            foreach (AreaFinishBase areaFinishBase in AreaFinishBaseList)
            {
                Add(areaFinishBase);
            }

            Select(0);

            UCAreaFinishPaletteElement ucAreaListElement = (UCAreaFinishPaletteElement)ucFlowLayoutPanel.Controls[0];

           ucFlowLayoutPanel.AutoScrollPosition = new Point(ucAreaListElement.Left, ucAreaListElement.Top);

            AreaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            AreaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;

            if (this.SelectedLineSummary is null)
            {
                this.SelectedLineSummary = new UCLineFinishSummary(this.Window, this.Page);
                this.pnlLineFinish.Controls.Add(this.SelectedLineSummary);
            }

            if (this.zeroLineSummary is null)
            {
                this.zeroLineSummary = new UCZeroLineSummary();

                this.pnlLineFinish.Controls.Add(this.zeroLineSummary);
            }

            UpdateZeroLineDisplay(SystemState.ZeroLineState);


            FinishGlobals.LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

        }

        public void SetLineFinish(int selectedIndex)
        {
            SelectedLineSummary.SetLineFinish(selectedIndex);
        }

        private void LineFinishBaseList_ItemSelected(int itemIndex)
        {

            UpdateZeroLineDisplay(false);

            SystemState.BtnAreaDesignStateZeroLine.BackColor = SystemColors.ControlLightLight;

            this.SelectedLineSummary.SetLineFinish(itemIndex);
            this.pnlLineFinish.Invalidate();

        }

        #region Design State Management


        #endregion

        //public delegate void FinishChangedHandler(UCAreaFinishPaletteElement ucAreaFinish);

        //public event FinishChangedHandler FinishChanged;
        
        public void SetSize(System.Drawing.Size containerSize)
        {
            this.Size = new Size(containerSize.Width, containerSize.Height);
            this.Location = new Point(0, 0);
            this.ucFlowLayoutPanel.Size = new Size(containerSize.Width, containerSize.Height - this.pnlLineFinish.Size.Height);
            this.ucFlowLayoutPanel.Location = new Point(0, 0);
        }

        #region Palette Changes

        #region Add Element

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase finishAreaBase)
        {
            Add(finishAreaBase);
        }

        public void Add(AreaFinishBase areaFinishBase)
        {
            //AreaFinishManager areaFinishManger = .AreaFinishManagerList[areaFinishBase.Guid];

            UCAreaFinishPaletteElement ucAreaFinish = new UCAreaFinishPaletteElement(this, Window, Page, areaFinishBase);

            //areaFinishManger.UCAreaPaletteElement = ucAreaFinish;

            Add(ucAreaFinish);

            ucFlowLayoutPanel.ScrollControlIntoView(ucAreaFinish);

            ucAreaFinish.PositionOnPalette = GetChildIndex(ucAreaFinish);

            //areaFinishBase.Filtered = false;

            ucAreaFinish.ControlClicked += UcAreaFinish_ControlClicked;
        }
      
        #endregion

        #region Insert Element

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase finishAreaBase, int position)
        {
            Insert(finishAreaBase, position);
        }

        internal void Insert(AreaFinishBase areaFinishBase, int position)
        {
            if (Count <= 0)
            {
                Debug.Assert(position == 0);

                Add(areaFinishBase);

                return;
            }

            Debug.Assert(!areaFinishListContainsName(areaFinishBase.AreaName));
            Debug.Assert(position >= 0 && position < Count);

            if (areaFinishListContainsName(areaFinishBase.AreaName))
            {
                return;
            }

            if (position < 0 || position >= Count)
            {
                return;
            }

            // The following can be removed after testing

            if (Window is null || Page is null)
            {
                throw new Exception("E00001: Window or Page is null in the initialization of UCAreaFinish.");
            }

            //AreaFinishManager areaFinishManager = AreaFinishManagerList[areaFinishBase.Guid];


            UCAreaFinishPaletteElement ucAreaFinish = new UCAreaFinishPaletteElement(this, Window, Page, areaFinishBase);

            ucAreaFinish.Size = ucAreaFinish.ExpandedSizeWithNoRollout;

            ucAreaFinish.ControlClicked += UcAreaFinish_ControlClicked;

            //ucAreaFinish.areaDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(areaFinishBase.Guid + "_AreaDesignState");
            //ucAreaFinish.seamDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(areaFinishBase.Guid + "_SeamDesignState");

            areaFinishBase.Filtered = false;

            Add(ucAreaFinish);
            SetChildIndex(ucAreaFinish, position);

            ucFlowLayoutPanel.AutoScrollPosition = new Point(ucAreaFinish.Left, ucAreaFinish.Top);

            for (int i = 0; i < this.Count; i++)
            {
                this[i].PositionOnPalette = i;
            }

            return;
        }

        //internal void Insert(AreaFinishBase areaFinishBase, int position)
        //{
        //    if (Count <= 0)
        //    {
        //        Debug.Assert(position == 0);

        //        Add(areaFinishBase);

        //        return;
        //    }

        //    Debug.Assert(!areaFinishListContainsName(areaFinishBase.AreaName));
        //    Debug.Assert(position >= 0 && position < Count);

        //    if (areaFinishListContainsName(areaFinishBase.AreaName))
        //    {
        //        return;
        //    }

        //    if (position < 0 || position >= Count)
        //    {
        //        return;
        //    }

        //    // The following can be removed after testing

        //    if (Window is null || Page is null)
        //    {
        //        throw new Exception("E00001: Window or Page is null in the initialization of UCAreaFinish.");
        //    }

        //    UCAreaFinishPaletteElement ucAreaFinish = new UCAreaFinishPaletteElement(this, Window, Page, areaFinishBase);

        //    ucAreaFinish.ControlClicked += UcAreaFinish_ControlClicked;

        //    areaFinishManager.UCAreaPaletteElement = ucAreaFinish;

        //    //ucAreaFinish.areaDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(areaFinishBase.Guid + "_AreaDesignState");
        //    //ucAreaFinish.seamDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(areaFinishBase.Guid + "_SeamDesignState");

        //    areaFinishBase.Filtered = false;

        //    Add(ucAreaFinish);
        //    SetChildIndex(ucAreaFinish, position);

        //    ucFlowLayoutPanel.AutoScrollPosition = new Point(ucAreaFinish.Left, ucAreaFinish.Top);

        //    for (int i = 0; i < this.Count; i++)
        //    {
        //        this[i].PositionOnPalette = i;
        //    }

        //    return;
        //}
        #endregion

        #region Remove Element

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            Remove(position);
        }

        internal void Remove(int selectedElement)
        {
            Debug.Assert(selectedElement >= 0 && selectedElement < Count);

            if (selectedElement < 0 || selectedElement >= Count)
            {
                return;
            }

            UCAreaFinishPaletteElement ucFinish = this[selectedElement];

            RemoveAt(selectedElement);

            ucFinish.ControlClicked -= UcAreaFinish_ControlClicked;

            ucFinish.Delete();
            
            for (int index = selectedElement; index < Count; index++)
            {
                UCAreaFinishPaletteElement ucFinish1 = this[index];

                ucFinish1.PositionOnPalette = index;
            }

            if (this.Count <= 0)
            {
                return;
            }

            if (selectedElement >= Count)
            {
                selectedElement = Count - 1;
            }

            AreaFinishBaseList.SelectElem(selectedElement);

            //Debug.Assert(ValidateControlPositions());
        }


        #endregion

        #region Swap Elements

        private void AreaFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            if (position1 == position2)
            {
                return;
            }

            if (position1 < position2)
            {
                Swap(position1, position2);
            }

            else
            {
                Swap(position2, position1);
            }
        }

        public void Swap(int position1, int position2)
        {
            Debug.Assert(position1 >= 0 && position1 < Count);
            Debug.Assert(position2 >= 0 && position2 < Count);

            UCAreaFinishPaletteElement ucFinish1 = this[position1];
            UCAreaFinishPaletteElement ucFinish2 = this[position2];

            SetChildIndex(ucFinish2, position1);
            SetChildIndex(ucFinish1, position2);

            ucFinish1.PositionOnPalette = position2;
            ucFinish2.PositionOnPalette = position1;

            //Debug.Assert(ValidateControlPositions());
        }


        #endregion

        #region Select Element

        private void UcAreaFinish_ControlClicked(UCAreaFinishPaletteElement sender)
        {
            AreaFinishBaseList.SelectElem(sender.PositionOnPalette);
        }

        private void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            
            Select(itemIndex);

            if (this[itemIndex].MaterialsType == MaterialsType.Rolls)
            {
                SystemGlobals.OversUndersFormUpdate(true);
            }

            if (this.FinishSelected != null)
            {
                FinishSelected(SelectedFinish);
            }

        }

        public void Select(int selectedItemIndex)
        {
            if (selectedItemIndex < 0 || selectedItemIndex >= Count)
            {
                return;
            }
            
            for (int i = 0; i < Count; i++)
            {
                if (i != selectedItemIndex)
                {
                    this[i].SetSelected(false);
                }
            }

           // ucFlowLayoutPanel.AutoScrollPosition = new Point(ucAreaFinish.Left, ucAreaFinish.Top);

            SelectedFinish.SetSelected(true);

            if (DesignState == DesignState.Seam)
            {
                SystemGlobals.SetupAllSeamStateSeamLayersForSelectedArea();
            }
        }

        #endregion

        private bool areaFinishListContainsName(string areaNameParm)
        {
            foreach (UCAreaFinishPaletteElement ucFinish in this)
            {
                if (ucFinish.AreaName == areaNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        public IEnumerator<UCAreaFinishPaletteElement> GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }
         
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        public UCAreaFinishPaletteElement this[int index]
        {
            get
            {
                return ucFlowLayoutPanel[index];
            }
        }

        public UCAreaFinishPaletteElement this[string guid]
        {
            get
            {
                return this.UCAreaFinishDict[guid];
            }
        }

        public int Count => this.ucFlowLayoutPanel.Count;

        private void Add(UCAreaFinishPaletteElement ucFinish)
        {
            this.ucFlowLayoutPanel.Add(ucFinish); this.UCAreaFinishDict.Add(ucFinish.Guid, ucFinish);

            ucFinish.Invalidate();
        }

        private void RemoveAt(int selectedElement)
        {
            this.UCAreaFinishDict.Remove(ucFlowLayoutPanel[selectedElement].Guid);
            this.ucFlowLayoutPanel.RemoveAt(selectedElement);
        }

        private int GetChildIndex(UCAreaFinishPaletteElement ucFinish) => this.ucFlowLayoutPanel.GetChildIndex(ucFinish);

        private void SetChildIndex(UCAreaFinishPaletteElement ucFinish, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucFinish, position);

        public void ClearTallyDisplays()
        {
            foreach (UCAreaFinishPaletteElement finishPaletteElement in this)
            {
                finishPaletteElement.ClearTallyDisplays();
            }
            //ckbColorOnly.Checked = false;
        }

        public void SetFiltered()
        {
            foreach (UCAreaFinishPaletteElement paletteElement in this)
            {
                paletteElement.SetFiltered();
            }
        }


        public void UpdateZeroLineDisplay(bool zeroLineSelected)
        {
            if (!zeroLineSelected)
            {
                this.SelectedLineSummary.BringToFront();
                this.SelectedLineSummary.Visible = true;

                this.zeroLineSummary.SendToBack();
                this.zeroLineSummary.Visible = false;
            }

            else
            {
                this.SelectedLineSummary.SendToBack();
                this.SelectedLineSummary.Visible = false;

                this.zeroLineSummary.BringToFront();
                this.zeroLineSummary.Visible = true;
            }
        }

        public void UpdateAreaPaletteState(AreaPaletteState areaPaletteState)
        {
            this.ucFlowLayoutPanel.SuspendLayout();

            foreach (UCAreaFinishPaletteElement paletteElement in this.ucFlowLayoutPanel.Controls)
            {
                paletteElement.UpdateAreaPaletteState(areaPaletteState);
            }

            this.ucFlowLayoutPanel.ResumeLayout();


            //foreach (UCAreaFinishPaletteElement paletteElement in this.ucFlowLayoutPanel.Controls)
            //{
            //    paletteElement.UpdateAreaPaletteState(areaPaletteState);
            //}
        }

        public void Clear()
        {
            AreaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            AreaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected -= AreaFinishBaseList_ItemSelected;

            FinishGlobals.LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;

            foreach (UCAreaFinishPaletteElement palletElement in this.UCAreaFinishDict.Values)
            {
                palletElement.ControlClicked -= UcAreaFinish_ControlClicked;

                palletElement.Delete();
            }
        }

        public void ClearAllFilters()
        {
            foreach (AreaFinishBase areaFinishBase in AreaFinishBaseList)
            {
                areaFinishBase.Filtered = false;
            }
        }
    }

    public class FinishChangedEventArgs : EventArgs
    {
        public FinishChangedEventArgs(UCAreaFinishPaletteElement ucFinish)
        {
            this.UCFinish = ucFinish;
        }

        public UCAreaFinishPaletteElement UCFinish;
    }
}
