//-------------------------------------------------------------------------------//
// <copyright file="UCLinePalette.cs" company="Bruun Estimating, LLC">            // 
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
    using System.Drawing;
    using System.Windows.Forms;

    using Utilities.Supporting_Controls;
    using System.Diagnostics;
    using System.Collections;
    using FinishesLib;
    using Globals;
    using Graphics;

    public partial class UCLineFinishPalette : UserControl, IEnumerable<UCLineFinishPaletteElement>
    {

        //public CanvasManager CanvasManager = null;

        public LineFinishBaseList LineFinishBaseList;

        //public FloorMaterialEstimatorBaseForm BaseForm = null;

        public GraphicsWindow Window { get; set; }
    
        public GraphicsPage Page { get; set; }

        public UCFlowLayoutPanel<UCLineFinishPaletteElement> ucFlowLayoutPanel;

        public Dictionary<string, UCLineFinishPaletteElement> ucLineFinishDict = new Dictionary<string, UCLineFinishPaletteElement>();

        public DesignState DesignState => SystemState.DesignState;

        public SeamMode SeamMode => SystemState.SeamMode;

        public DesignState LineAreaMode
        {
            get
            {
                return SystemState.DesignState;
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                return LineFinishBaseList.SelectedItemIndex;
            }
        }

        public UCLineFinishPaletteElement SelectedLine
        {
            get
            {
                return this[SelectedItemIndex];
            }
        }

        public UCLineFinishPalette()
        {
            InitializeComponent();

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCLineFinishPaletteElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.Controls.Add(ucFlowLayoutPanel);
        }

        public void Init(//FloorMaterialEstimatorBaseForm baseForm, CanvasManager canvasManager, LineFinishBaseList lineFinishBaseList)
            GraphicsWindow window
                ,GraphicsPage page
                ,LineFinishBaseList lineFinishBaseList)
        {
            this.Window = window;

            this.Page = page;

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

            //BaseForm.areaPalette.selectedLineSummary.SetLineFinish(0);

            UCLineFinishPaletteElement ucLineListElement = (UCLineFinishPaletteElement)ucFlowLayoutPanel.Controls[0];

            ucFlowLayoutPanel.AutoScrollPosition = new Point(ucLineListElement.Left, ucLineListElement.Top);

            LineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;
            LineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;
            LineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;
            LineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;
            LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

            //BaseForm.DesignStateChanged += BaseForm_DesignStateChanged;

            //eventsAdded = true;
        }

        //public void DeleteLayers()
        //{
        //    foreach (UCLineFinishPaletteElement ucLine in this)
        //    {
        //        ucLine.LineFinishManager.Delete();
        //    }
        //}

        //public void BaseForm_DesignStateChanged(DesignState prevDesignState, DesignState currDesignState, SeamMode currSeamMode)
        //{
        //    foreach (UCLineFinishPaletteElement ucLineFinish in this)
        //    {
        //        ucLineFinish.LineFinishManager.SetLineState(currDesignState, currSeamMode, ucLineFinish.Selected);
        //    }
        //}

        public void SetSize(System.Drawing.Size containerSize)
        {
            this.Size = new Size(containerSize.Width, containerSize.Height);
            this.Location = new Point(0, 0);
            this.ucFlowLayoutPanel.Size = new Size(containerSize.Width, containerSize.Height - this.pnlLineFinish.Size.Height);
            this.ucFlowLayoutPanel.Location = new Point(0, 0);
        }

        #region Palette Changes

        #region Add Element

        private void LineFinishBaseList_ItemAdded(LineFinishBase lineFinishBase)
        {
            Add(lineFinishBase);
        }

        private void Add(LineFinishBase lineFinishBase)
        {
            UCLineFinishPaletteElement ucLineFinish = new UCLineFinishPaletteElement(Window, Page, this, lineFinishBase);

            Add(ucLineFinish);

            ucLineFinish.PositionOnPalette = GetChildIndex(ucLineFinish);

            ucLineFinishDict.Add(ucLineFinish.Guid, ucLineFinish);

            ucFlowLayoutPanel.AutoScrollPosition = new Point(ucLineFinish.Left, ucLineFinish.Top);
     
            lineFinishBase.Filtered = false;

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

            UCLineFinishPaletteElement ucLine = new UCLineFinishPaletteElement(Window, Page, this, lineFinishBase);

            this.ucLineFinishDict.Add(ucLine.Guid, ucLine);

            ucLine.ControlClicked += UcLineFinish_ControlClicked;

            //ucLine.AreaDesignStateLayer = new GraphicsLayer(CanvasManager.Window, CanvasManager.Page, "[UCLineFinishPaletteElement]" + ucLine.LineName + "_AreaDesignState", GraphicsLayerType.Dynamic);
            //ucLine.LineDesignStateLayer = new GraphicsLayer(CanvasManager.Window, CanvasManager.Page, "[UCLineFinishPaletteElement]" + ucLine.LineName + "_LineDesignState", GraphicsLayerType.Dynamic);
            //ucLine.SeamDesignStateLayer = new GraphicsLayer(CanvasManager.Window, CanvasManager.Page, "[UCLineFinishPaletteElement]" + ucLine.LineName + "_SeamDesignState", GraphicsLayerType.Dynamic);

           // VisioInterop.LockLayer(ucLine.SeamDesignStateLayer.GetBaseLayer());

            ucLine.LineFinishBase.Filtered = false;

            Add(ucLine);

            SetChildIndex(ucLine, position);

            ucFlowLayoutPanel.AutoScrollPosition = new Point(ucLine.Left, ucLine.Top);

            for (int i = 0; i < this.Count; i++)
            {
                this[i].PositionOnPalette = i;
            }

            return;
        }

#endregion

#region Remove Element

        private void LineFinishBaseList_ItemRemoved(string guid, int position)
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

            UCLineFinishPaletteElement ucLine = this[position];

            this.ucLineFinishDict.Remove(ucLine.Guid);

            RemoveAt(position);

            ucLine.ControlClicked -= UcLineFinish_ControlClicked;

            

            //ucLine.AreaDesignStateLayer = null;
            //ucLine.LineDesignStateLayer = null;
            //ucLine.SeamDesignStateLayer = null;

            for (int index = position; index < Count; index++)
            {
                UCLineFinishPaletteElement ucFinish1 = this[index];

                ucFinish1.PositionOnPalette = index;
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

            UCLineFinishPaletteElement ucFinish1 = this[position1];
            UCLineFinishPaletteElement ucFinish2 = this[position2];

            SetChildIndex(ucFinish2, position1);
            SetChildIndex(ucFinish1, position2);

            ucFinish1.PositionOnPalette = position2;
            ucFinish2.PositionOnPalette = position1;
        }


#endregion

#region Select Element

        private void UcLineFinish_ControlClicked(UCLineFinishPaletteElement sender)
        {
            LineFinishBaseList.Select(sender.PositionOnPalette);

           // BaseForm.uclSelectedLine.Invalidate();
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
            foreach (UCLineFinishPaletteElement ucLine in this)
            {
                if (ucLine.LineName == lineNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        //internal void MoveLineToSelectedLineType(string guid)
        //{
        //    if (string.IsNullOrEmpty(guid))
        //    {
        //        return;
        //    }

        //    if (!CanvasManager.CurrentPage.DirectedLineDictContains(guid))
        //    { 
        //        return;
        //    }

        //    CanvasDirectedLine canvasDirectedLine = CanvasManager.CurrentPage.GetDirectedLine(guid);

        //    MoveLineToLineType(canvasDirectedLine, BaseForm.selectedLineFinishManager);
        //}

        //internal void MoveLineToSelectedLineType(CanvasDirectedLine canvasDirectedLine)
        //{
        //    if ((object)canvasDirectedLine == null)
        //    {
        //        return;
        //    }

        //    MoveLineToLineType(canvasDirectedLine, BaseForm.selectedLineFinishManager);
        //}

        //internal void MoveLineToLineType(CanvasDirectedLine line, LineFinishManager nextLineFinishManager)
        //{
        //    if ((object)line == null)
        //    {
        //        return;
        //    }

        //    LineFinishManager currLineFinishManager = line.LineFinishManager;

        //    if (nextLineFinishManager == currLineFinishManager)
        //    {
        //        return;
        //    }

        //    line.RemoveLineFinish();
        //    line.SetLineFinish(nextLineFinishManager);

        //    line.Shape.SetShapeData2("line[" + nextLineFinishManager.LineName + "]");

        //}

        //public void MoveLine(CanvasDirectedLine line, UCLineFinishPaletteElement currUCLine, UCLineFinishPaletteElement nextUCLine)
        //{
        //    if ((object)line == null)
        //    {
        //        return;
        //    }

        //    if (nextUCLine == currUCLine)
        //    {
        //        return;
        //    }

        //    line.RemoveLineFinish();
        //    line.SetLineFinish(nextUCLine);
        //}

        //public void UpdateFinishStats()
        //{
        //    foreach (LineFinishManager lineFinishManager in BaseForm.LineFinishManagerList)
        //    {
        //        lineFinishManager.UpdateFinishStats();
        //    }
        //}

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

        public IEnumerator<UCLineFinishPaletteElement> GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        public UCLineFinishPaletteElement this[int index]
        {
            get
            {
                return ucFlowLayoutPanel[index];
            }
        }

        public UCLineFinishPaletteElement this[string guid]
        {
            get
            {
                return this.ucLineFinishDict[guid];
            }
        }

        public int Count => this.ucFlowLayoutPanel.Count;

        private void Add(UCLineFinishPaletteElement ucLine) => this.ucFlowLayoutPanel.Add(ucLine);

        private void RemoveAt(int selectedElement) => this.ucFlowLayoutPanel.RemoveAt(selectedElement);

        private int GetChildIndex(UCLineFinishPaletteElement ucLine) => this.ucFlowLayoutPanel.GetChildIndex(ucLine);

        private void SetChildIndex(UCLineFinishPaletteElement ucLine, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucLine, position);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        internal void ClearTallyDisplays()
        {
            foreach (UCLineFinishPaletteElement linePaletteElement in this)
            {
                linePaletteElement.ClearTallyDisplays();
            }
        }


        internal void Clear()
        {
            LineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            LineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            LineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            LineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;
            LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;

            //BaseForm.DesignStateChanged -= BaseForm_DesignStateChanged;

            foreach (UCLineFinishPaletteElement palletElement in this.ucLineFinishDict.Values)
            {
                palletElement.ControlClicked -= UcLineFinish_ControlClicked;

                palletElement.Delete();
            }
        }

    }
}
