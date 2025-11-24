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

namespace FinishesLib
{
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

    using System.Diagnostics;
    using System.Collections;

    public partial class UCAreaFinishPallet : UserControl, IEnumerable<UCAreaFinishPalletElement>
    {
        public CanvasManager CanvasManager = null;

        public AreaFinishBaseList AreaFinishBaseList;

        public FloorMaterialEstimatorBaseForm BaseForm = null;

        public UCFlowLayoutPanel<UCAreaFinishPalletElement> ucFlowLayoutPanel;

        public Dictionary<string, UCAreaFinishPalletElement> UCAreaFinishDict = new Dictionary<string, UCAreaFinishPalletElement>();

        public IEnumerable<UCAreaFinishPalletElement> UCAreaFinishes => UCAreaFinishDict.Values;

        public DesignState DesignState
        {
            get
            {
                return BaseForm.DesignState;
            }
        }

        public bool ShowAreas
        {
            get
            {
                return BaseForm.btnShowAreas.Checked;
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                return AreaFinishBaseList.SelectedItemIndex;
            }
        }

        public UCAreaFinishPalletElement SelectedFinish
        {
            get
            {
                return this[SelectedItemIndex];
            }
        }
        
        public UCAreaFinishPallet()
        {
            InitializeComponent();

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCAreaFinishPalletElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.Controls.Add(ucFlowLayoutPanel);
        }

        public void Init(FloorMaterialEstimatorBaseForm baseForm, CanvasManager canvasManager, AreaFinishBaseList areaFinishBaseList)
        {
            this.BaseForm = baseForm;

            this.CanvasManager = canvasManager;

            Init(areaFinishBaseList);
        }

        private bool eventsAdded = false;

        public void Init(AreaFinishBaseList areaFinishBaseList)
        {
            this.ucFlowLayoutPanel.Controls.Clear();

            this.UCAreaFinishDict.Clear();

            this.AreaFinishBaseList = areaFinishBaseList;

            foreach (AreaFinishBase areaFinishBase in AreaFinishBaseList)
            {
                Add(areaFinishBase);
            }

            Select(0);

            AreaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            AreaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;

            if (eventsAdded)
            {
                return;
            }

            BaseForm.DesignStateChanged += BaseForm_DesignStateChanged;

            eventsAdded = true;
        }

        #region Design State Management

        private void BaseForm_DesignStateChanged(DesignState prevDesignState, DesignState currDesignState)
        {
            SetDesignStateFormat(currDesignState, BaseForm.btnShowAreas.Checked);
        }
    
        internal void SetDesignStateFormat(DesignState designState, bool showAreas)
        {
            foreach (UCAreaFinishPalletElement ucAreaFinish in this)
            {
                ucAreaFinish.SetDesignStateFormat(designState, showAreas);
            }
        }

        internal void SetAreaDesignStateFormat(AreaMode areaMode)
        {
            throw new NotImplementedException();
        }

        #endregion

        public delegate void FinishChangedHandler(UCAreaFinishPalletElement ucAreaFinish);

        public event FinishChangedHandler FinishChanged;
        
        public void SetSize(System.Drawing.Size containerSize)
        {
            int cntnSizeX = containerSize.Width;
            int cntnSizeY = containerSize.Height;

            int cntlSizeX = cntnSizeX - 2;
            int cntlSizeY = cntnSizeY - 2;

            this.Size = new Size(cntlSizeX, cntlSizeY);
            this.Location = new Point(1, 1);

            int slcdPanlSizeX = cntlSizeX - 32;
            int slcdPanlSizeY = this.pnlSeam.Height;

            int areaPanlSizeX = cntlSizeX - 2;

            int areaPanlSizeY1 = cntlSizeY - slcdPanlSizeY - 48;
            int areaPanlSizeY2 = Count * UCAreaFinishPalletElement.CntlSizeY;

            int areaPanlSizeY = Math.Min(areaPanlSizeY1, areaPanlSizeY2);

            int areaPanlLocnX = 1;
            int areaPanlLocnY = 1;

            int slcdPanlLocnX = 1;
            int slcdPanlLocnY = areaPanlLocnY + areaPanlSizeY + 8;
            
            this.ucFlowLayoutPanel.Size = new Size(areaPanlSizeX, areaPanlSizeY);
            this.ucFlowLayoutPanel.Location = new Point(areaPanlLocnX, areaPanlLocnY);

            this.pnlSeam.Size = new Size(slcdPanlSizeX, slcdPanlSizeY);
            this.pnlSeam.Location = new Point(slcdPanlLocnX, slcdPanlLocnY);
        }

        #region Pallet Changes

        #region Add Element

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase finishAreaBase)
        {
            Add(finishAreaBase);
        }

        public void Add(AreaFinishBase areaFinishBase)
        {
            UCAreaFinishPalletElement ucAreaFinish = new UCAreaFinishPalletElement(this, areaFinishBase);

            Add(ucAreaFinish);

            ucAreaFinish.PositionOnPallet = GetChildIndex(ucAreaFinish);

            ucAreaFinish.areaDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucAreaFinish.Guid + "_AreaDesignState");
            ucAreaFinish.seamDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucAreaFinish.Guid + "_SeamDesignState");

            CanvasManager.CurrentPage.InitSeamMaintenanceTables(ucAreaFinish.Guid);

            ucAreaFinish.Filtered = false;

            ucAreaFinish.FinishChanged += UcAreaFinish_FinishChanged;
            ucAreaFinish.ControlClicked += UcAreaFinish_ControlClicked;

        }

        private void UcAreaFinish_FinishChanged(UCAreaFinishPalletElement ucAreaFinish)
        {
            if (this.FinishChanged != null)
            {
                FinishChanged.Invoke(ucAreaFinish);
            }
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

            UCAreaFinishPalletElement ucAreaFinish = new UCAreaFinishPalletElement(this, areaFinishBase);

            ucAreaFinish.ControlClicked += UcAreaFinish_ControlClicked;

            ucAreaFinish.areaDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(areaFinishBase.Guid + "_AreaDesignState");
            ucAreaFinish.seamDesignStateLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(areaFinishBase.Guid + "_SeamDesignState");

            ucAreaFinish.Filtered = false;

            Add(ucAreaFinish);
            SetChildIndex(ucAreaFinish, position);

            for (int i = 0; i < this.Count; i++)
            {
                this[i].PositionOnPallet = i;
            }

            return;
        }

        #endregion

        #region Remove Element

        private void AreaFinishBaseList_ItemRemoved(int position)
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

            UCAreaFinishPalletElement ucFinish = this[selectedElement];

            RemoveAt(selectedElement);

            ucFinish.ControlClicked -= UcAreaFinish_ControlClicked;

            ucFinish.areaDesignStateLayer.Delete(1);
            ucFinish.seamDesignStateLayer.Delete(1);

            for (int index = selectedElement; index < Count; index++)
            {
                UCAreaFinishPalletElement ucFinish1 = this[index];

                ucFinish1.PositionOnPallet = index;
            }

            if (this.Count <= 0)
            {
                return;
            }

            if (selectedElement >= Count)
            {
                selectedElement = Count - 1;
            }

            AreaFinishBaseList.Select(selectedElement);

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

            UCAreaFinishPalletElement ucFinish1 = this[position1];
            UCAreaFinishPalletElement ucFinish2 = this[position2];

            SetChildIndex(ucFinish2, position1);
            SetChildIndex(ucFinish1, position2);

            ucFinish1.PositionOnPallet = position2;
            ucFinish2.PositionOnPallet = position1;

            //Debug.Assert(ValidateControlPositions());
        }


        #endregion

        #region Select Element

        private void UcAreaFinish_ControlClicked(UCAreaFinishPalletElement sender)
        {
            AreaFinishBaseList.Select(sender.PositionOnPallet);
        }

        private void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            Select(itemIndex);
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
                    this[i].Selected = false;
                }
            }

            SelectedFinish.Selected = true;
        }

        #endregion

        private bool areaFinishListContainsName(string areaNameParm)
        {
            foreach (UCAreaFinishPalletElement ucFinish in this)
            {
                if (ucFinish.AreaName == areaNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        internal void MoveShapeToFinishType(CanvasLayoutArea canvasLayoutArea, UCAreaFinishPalletElement nextUCFinish)
        {
            UCAreaFinishPalletElement currUCLine = canvasLayoutArea.UCAreaFinish;

            if (nextUCFinish == currUCLine)
            {
                return;
            }


            currUCLine.RemoveShape(canvasLayoutArea);
            nextUCFinish.AddShape(canvasLayoutArea);
        }

        internal void MoveCanvasLayoutAreaToSelectedFinishType(CanvasLayoutArea canvasLayoutArea, bool updateColor = true)
        {
            MoveCanvasLayoutAreaToFinishType(canvasLayoutArea, SelectedFinish, updateColor);
        }

        internal void MoveCanvasLayoutAreaToFinishType(CanvasLayoutArea canvasLayoutArea, UCAreaFinishPalletElement nextUCFinish, bool updateColor = true)
        {
  
            Debug.Assert(canvasLayoutArea.ParentArea == null); // Only root areas can be removed
            
            UCAreaFinishPalletElement currUCFinish = canvasLayoutArea.UCAreaFinish;

            if (nextUCFinish == currUCFinish)
            {
                return;
            }

            currUCFinish.RemoveShape(canvasLayoutArea);
            nextUCFinish.AddShape(canvasLayoutArea, true, true);
        }

        internal void UCAreaSimpleClick(UCAreaFinishPalletElement ucFinish)
        {
            //CanvasManager.UpdateAreaSelections(ucFinish);
            //BaseForm.UpdateAreaSelections(ucFinish);
        }

        public void UpdateFinishStats()
        {
            foreach (UCAreaFinishPalletElement ucFinish in this)
            {
                ucFinish.UpdateFinishStats();
            }
        }


        internal void SetSeamForSelectedFinish(SeamFinishBase finishSeamBase)
        {
            SelectedFinish.SetSeam(finishSeamBase);

            this.lblTagLabel.Text = finishSeamBase.SeamName;
        }

        public IEnumerator<UCAreaFinishPalletElement> GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        public UCAreaFinishPalletElement this[int index]
        {
            get
            {
                return ucFlowLayoutPanel[index];
            }
        }

        public UCAreaFinishPalletElement this[string guid]
        {
            get
            {
                return this.UCAreaFinishDict[guid];
            }
        }

        public int Count => this.ucFlowLayoutPanel.Count;

        private void Add(UCAreaFinishPalletElement ucFinish)
        {
            this.ucFlowLayoutPanel.Add(ucFinish); this.UCAreaFinishDict.Add(ucFinish.Guid, ucFinish);
        }

        private void RemoveAt(int selectedElement)
        {
            this.UCAreaFinishDict.Remove(ucFlowLayoutPanel[selectedElement].Guid);
            this.ucFlowLayoutPanel.RemoveAt(selectedElement);
        }

        private int GetChildIndex(UCAreaFinishPalletElement ucFinish) => this.ucFlowLayoutPanel.GetChildIndex(ucFinish);

        private void SetChildIndex(UCAreaFinishPalletElement ucFinish, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucFinish, position);
    }

    public class FinishChangedEventArgs : EventArgs
    {
        public FinishChangedEventArgs(UCAreaFinishPalletElement ucFinish)
        {
            this.UCFinish = ucFinish;
        }

        public UCAreaFinishPalletElement UCFinish;
    }
}
