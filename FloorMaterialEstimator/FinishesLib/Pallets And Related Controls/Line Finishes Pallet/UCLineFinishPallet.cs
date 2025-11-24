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
    using System.Collections;
    using FinishesLib;

    public partial class UCLineFinishPallet : UserControl, IEnumerable<UCLineFinishPalletElement>
    {

        public CanvasManager CanvasManager = null;

        public LineFinishBaseList LineFinishBaseList;

        public FloorMaterialEstimatorBaseForm BaseForm = null;

        public UCFlowLayoutPanel<UCLineFinishPalletElement> ucFlowLayoutPanel;

        public Dictionary<string, UCLineFinishPalletElement> ucLineFinishDict = new Dictionary<string, UCLineFinishPalletElement>();

        public DesignState DesignState
        {
            get
            {
                return CanvasManager.DesignState;
            }
        }

        public DesignState LineAreaMode
        {
            get
            {
                return CanvasManager.DesignState;
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                return LineFinishBaseList.SelectedItemIndex;
            }
        }

        public UCLineFinishPalletElement SelectedLine
        {
            get
            {
                return this[SelectedItemIndex];
            }
        }

        public UCLineFinishPallet()
        {
            InitializeComponent();

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCLineFinishPalletElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.Controls.Add(ucFlowLayoutPanel);
        }
        
        public void Init(FloorMaterialEstimatorBaseForm baseForm, CanvasManager canvasManager, LineFinishBaseList lineFinishBaseList)
        {
            this.BaseForm = baseForm;

            this.CanvasManager = canvasManager;

            Init(lineFinishBaseList);
        }

        private bool eventsAdded = false;

        public void Init(LineFinishBaseList lineFinishBaseList)
        {
            this.LineFinishBaseList = lineFinishBaseList;

            ucLineFinishDict.Clear();

            this.ucFlowLayoutPanel.Controls.Clear();

            foreach (LineFinishBase lineFinishBase in LineFinishBaseList)
            {
                Add(lineFinishBase);
            }

            Select(0);

            LineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;
            LineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;
            LineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;
            LineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;
            LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

            if (eventsAdded)
            {
                return;
            }

            BaseForm.DesignStateChanged += BaseForm_DesignStateChanged;

            eventsAdded = true;
        }

        private void BaseForm_DesignStateChanged(DesignState prevDesignState, DesignState currDesignState)
        {
            foreach (UCLineFinishPalletElement ucLineFinish in this)
            {
                ucLineFinish.SetLineState(currDesignState, ucLineFinish.Selected);
            }
        }

        public void SetSize(System.Drawing.Size containerSize)
        {
            this.Size = new System.Drawing.Size(containerSize.Width - 2, containerSize.Height - 2);
            this.ucFlowLayoutPanel.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
            this.ucFlowLayoutPanel.Location = new Point(1, 1);
        }

        #region Pallet Changes

        #region Add Element

        private void LineFinishBaseList_ItemAdded(LineFinishBase lineFinishBase)
        {
            Add(lineFinishBase);
        }

        private void Add(LineFinishBase lineFinishBase)
        {
            UCLineFinishPalletElement ucLineFinish = new UCLineFinishPalletElement(this, lineFinishBase);

            Add(ucLineFinish);

            ucLineFinish.PositionOnPallet = GetChildIndex(ucLineFinish);

            ucLineFinishDict.Add(ucLineFinish.Guid, ucLineFinish);

            ucLineFinish.AreaModeLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucLineFinish.Guid + "_AreaMode");
            ucLineFinish.LineModeLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucLineFinish.Guid + "_LineMode");

            ucLineFinish.Filtered = false;

            ucLineFinish.ControlClicked += UcLineFinish_ControlClicked;
        }

        #endregion

        #region Insert Element

        private void LineFinishBaseList_ItemInserted(LineFinishBase lineFinishBase, int position)
        {
            Insert(lineFinishBase, position);
        }

        internal void Insert(LineFinishBase lineFinishBase, int position)
        {
            if (Count <= 0)
            {
                Debug.Assert(position == 0);

                Add(lineFinishBase);

                return;
            }

            Debug.Assert(!listFinishListContainsName(lineFinishBase.LineName));
            Debug.Assert(position >= 0 && position < Count);

            if (listFinishListContainsName(lineFinishBase.LineName))
            {
                return;
            }

            if (position < 0 || position >= Count)
            {
                return;
            }

            UCLineFinishPalletElement ucLine = new UCLineFinishPalletElement(this, lineFinishBase);

            this.ucLineFinishDict.Add(ucLine.Guid, ucLine);

            ucLine.ControlClicked += UcLineFinish_ControlClicked;

            ucLine.AreaModeLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucLine.Guid + "_AreaMode");
            ucLine.LineModeLayer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucLine.Guid + "_LineMode");

            ucLine.Filtered = false;

            Add(ucLine);

            SetChildIndex(ucLine, position);

            for (int i = 0; i < this.Count; i++)
            {
                this[i].PositionOnPallet = i;
            }

            return;
        }

        #endregion

        #region Remove Element

        private void LineFinishBaseList_ItemRemoved(int position)
        {
            Remove(position);
        }

        internal void Remove(int position)
        {
            Debug.Assert(position >= 0 && position < Count);

            if (position < 0 || position >= Count)
            {
                return;
            }

            UCLineFinishPalletElement ucLine = this[position];

            this.ucLineFinishDict.Remove(ucLine.Guid);

            RemoveAt(position);

            ucLine.ControlClicked -= UcLineFinish_ControlClicked;

            ucLine.AreaModeLayer.Delete(1);
            ucLine.LineModeLayer.Delete(1);

            for (int index = position; index < Count; index++)
            {
                UCLineFinishPalletElement ucFinish1 = this[index];

                ucFinish1.PositionOnPallet = index;
            }

            if (this.Count <= 0)
            {
                return;
            }

            if (position >= Count)
            {
                position = Count - 1;
            }

            LineFinishBaseList.Select(position);

            //Debug.Assert(ValidateControlPositions());
        }


        #endregion

        #region Swap Elements

        private void LineFinishBaseList_ItemsSwapped(int position1, int position2)
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

            UCLineFinishPalletElement ucFinish1 = this[position1];
            UCLineFinishPalletElement ucFinish2 = this[position2];

            SetChildIndex(ucFinish2, position1);
            SetChildIndex(ucFinish1, position2);

            ucFinish1.PositionOnPallet = position2;
            ucFinish2.PositionOnPallet = position1;
        }


        #endregion

        #region Select Element

        private void UcLineFinish_ControlClicked(UCLineFinishPalletElement sender)
        {
            LineFinishBaseList.Select(sender.PositionOnPallet);

            BaseForm.uclSelectedLine.Invalidate();
        }

        private void LineFinishBaseList_ItemSelected(int itemIndex)
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

            SelectedLine.Selected = true;
        }

        #endregion

        private bool listFinishListContainsName(string lineNameParm)
        {
            foreach (UCLineFinishPalletElement ucLine in this)
            {
                if (ucLine.LineName == lineNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        internal void MoveLineToSelectedLineType(string nameID)
        {
            if (string.IsNullOrEmpty(nameID))
            {
                return;
            }

            if (!CanvasManager.CurrentPage.ContainsDirectedLine(nameID))
            {
                return;
            }

            CanvasDirectedLine canvasDirectedLine = CanvasManager.CurrentPage.GetDirectedLineByNameId(nameID);

            MoveLineToLineType(canvasDirectedLine, SelectedLine);
        }

        internal void MoveLineToSelectedLineType(CanvasDirectedLine canvasDirectedLine)
        {
            if ((object)canvasDirectedLine == null)
            {
                return;
            }

            MoveLineToLineType(canvasDirectedLine, SelectedLine);
        }

        internal void MoveLineToLineType(CanvasDirectedLine line, UCLineFinishPalletElement nextUCLine)
        {
            if ((object)line == null)
            {
                return;
            }

            UCLineFinishPalletElement currUCLine = line.ucLine;

            if (nextUCLine == currUCLine)
            {
                return;
            }

            currUCLine.RemoveLine(line);
            nextUCLine.AddLine(line);

        }

        public void MoveLine(CanvasDirectedLine line, UCLineFinishPalletElement currUCLine, UCLineFinishPalletElement nextUCLine)
        {
            if ((object)line == null)
            {
                return;
            }

            if (nextUCLine == currUCLine)
            {
                return;
            }

            currUCLine.RemoveLine(line);
            nextUCLine.AddLine(line);
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
        public IEnumerator<UCLineFinishPalletElement> GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        public UCLineFinishPalletElement this[int index]
        {
            get
            {
                return ucFlowLayoutPanel[index];
            }
        }

        public UCLineFinishPalletElement this[string guid]
        {
            get
            {
                return this.ucLineFinishDict[guid];
            }
        }

        public int Count => this.ucFlowLayoutPanel.Count;

        private void Add(UCLineFinishPalletElement ucLine) => this.ucFlowLayoutPanel.Add(ucLine);

        private void RemoveAt(int selectedElement) => this.ucFlowLayoutPanel.RemoveAt(selectedElement);

        private int GetChildIndex(UCLineFinishPalletElement ucLine) => this.ucFlowLayoutPanel.GetChildIndex(ucLine);

        private void SetChildIndex(UCLineFinishPalletElement ucLine, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucLine, position);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }
    }
}
