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
    using Graphics;
    using System.Diagnostics;

    public partial class UCFinishPalletOld : UserControl
    {
        public CanvasManager CanvasManager = null;

        public FinishAreaBaseList FinishAreaBaseList;

        public FloorMaterialEstimatorBaseForm BaseForm = null;

        //public Dictionary<string, UCFinish> finishDict = new Dictionary<string, UCFinish>();

        public List<UCFinish> areaFinishList = new List<UCFinish>();

        private int ucAreaListTotalHeight = 0;

        public int finishCount
        {
            get
            {
                return areaFinishList.Count;
            }
        }

        public int SelectedItemIndex
        {
            get
            {
                return FinishAreaBaseList.SelectedItemIndex;
            }
        }

        public UCFinish SelectedFinish
        {
            get
            {
                return this[SelectedItemIndex];
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

        public void Init(FloorMaterialEstimatorBaseForm baseForm, CanvasManager canvasManager, FinishAreaBaseList finishAreaBaseList)
        {
            this.BaseForm = baseForm;

            this.CanvasManager = canvasManager;

            this.FinishAreaBaseList = finishAreaBaseList;
            
            int position = 0;

            foreach (FinishAreaBase finishAreaBase in FinishAreaBaseList)
            {
                Add(finishAreaBase);
                //addFinishAreaToPanel(finishAreaBase, position++);
            }

            Select(0);

            ucAreaListTotalHeight = (UCFinish.CntlSizeY + 1) * finishAreaBaseList.Count + 40 * areaFinishList[0].Height;

            FinishAreaBaseList.ItemAdded += FinishAreaBaseList_ItemAdded;
            FinishAreaBaseList.ItemSelected += FinishAreaBaseList_ItemSelected;

            Debug.Assert(ValidateControlPositions());
        }

        private void FinishAreaBaseList_ItemAdded(FinishAreaBase finishAreaBase)
        {
            //Add(finishAreaBase);
            addFinishAreaToPanel(finishAreaBase, FinishAreaBaseList.Count);

            this.pnlFinishList.Refresh();
        }

        private void FinishAreaBaseList_ItemSelected(int itemIndex)
        {
            Select(itemIndex);
        }

        private void addFinishAreaToPanel(FinishAreaBase finishAreaBase, int position)
        {
            UCFinish ucFinish = new UCFinish(this, finishAreaBase);

            ucFinish.layer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucFinish.Guid);

            ucFinish.positionOnPallet = position;

            this.pnlFinishList.Controls.Add(ucFinish);

            this.areaFinishList.Add(ucFinish);

            int cntlLocnY = 0;

            if (position != 0)
            {
                cntlLocnY = (this.areaFinishList[0].Height + 1) * position;
            }

            //cntlLocnY = position * (UCFinish.CntlSizeY + 1);

            ucFinish.Location = new Point(6, cntlLocnY);

            ucFinish.Size = new Size(ucFinish.Width, UCFinish.CntlSizeY);

            ucFinish.layer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucFinish.Guid);

            ucFinish.bIsFiltered = false;
            ucFinish.positionOnPallet = this.areaFinishList.Count;

            ucFinish.ControlClicked += UcFinish_ControlClicked;

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));
            
            ucFinish.BringToFront();
        }

        private void UcFinish_ControlClicked(UCFinish sender)
        {
            Debug.Assert(ValidateControlPositions());

            FinishAreaBaseList.Select(sender.positionOnPallet);
        }

        public delegate void FinishChangedHandler(object source, FinishChangedEventArgs e);

        public event FinishChangedHandler FinishChanged;

        private void UC_FinishChanged(object source, EventArgs e)
        {
            UCFinish ucFinish = (UCFinish)source;
            Color finishColor = ucFinish.FinishColor;

            if (FinishChanged != null)
            {
                FinishChanged(this, new FinishChangedEventArgs(ucFinish));
            }
        }

        private void CreateDefaultUCFinish()
        {

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
            int slcdPanlSizeY = this.pnlSeam.Height;

            int areaPanlSizeX = cntlSizeX - 2;

            int areaPanlSizeY1 = cntlSizeY - slcdPanlSizeY - 48;
            int areaPanlSizeY2 = ucAreaListTotalHeight;

            int areaPanlSizeY = Math.Min(areaPanlSizeY1, areaPanlSizeY2);

            int areaPanlLocnX = 1;
            int areaPanlLocnY = 1;

            int slcdPanlLocnX = 1;
            int slcdPanlLocnY = areaPanlLocnY + areaPanlSizeY + 8;

            this.pnlFinishList.Size = new Size(areaPanlSizeX, areaPanlSizeY);
            this.pnlFinishList.Location = new Point(areaPanlLocnX, areaPanlLocnY);

            this.pnlSeam.Size = new Size(slcdPanlSizeX, slcdPanlSizeY);
            this.pnlSeam.Location = new Point(slcdPanlLocnX, slcdPanlLocnY);
        }

        internal void MoveShapeToFinishType(IAreaShape iAreaShape, UCFinish nextUCFinish)
        {
            UCFinish currUCLine = iAreaShape.ucFinish;

            if (nextUCFinish == currUCLine)
            {
                return;
            }

            currUCLine.RemoveShape(iAreaShape);
            nextUCFinish.AddShape(iAreaShape);
        }

        internal void MoveShapeToSelectedFinishType(string nameID, bool updateColor = true)
        {
            MoveShapeToFinishType(nameID, SelectedFinish, updateColor);
        }

        internal void MoveShapeToFinishType(string nameID, UCFinish nextUCFinish, bool updateColor = true)
        {
            if (!this.CanvasManager.ShapeDict.ContainsKey(nameID))
            {
                return;
            }

            IAreaShape areaShape = (IAreaShape) CanvasManager.ShapeDict[nameID];

            if (areaShape.ShapeType != ShapeType.LayoutArea)
            {
                return;
            }

            UCFinish currUCFinish = areaShape.ucFinish;

            if (nextUCFinish == currUCFinish)
            {
                return;
            }

            currUCFinish.RemoveShape(areaShape);
            nextUCFinish.AddShape(areaShape, true);
        }

        internal void UCAreaSimpleClick(UCFinish ucFinish)
        {
            //CanvasManager.UpdateAreaSelections(ucFinish);
            //BaseForm.UpdateAreaSelections(ucFinish);
        }

        public void UpdateFinishStats()
        {
            foreach (UCFinish ucFinish in areaFinishList)
            {
                ucFinish.UpdateFinishStats();
            }
        }

        public void Select(int selectedItemIndex)
        {
            if (selectedItemIndex < 0 || selectedItemIndex >= this.areaFinishList.Count)
            {
                return;
            }

            //SelectedItemIndex = selectedItemIndex;

            for (int i = 0; i < this.areaFinishList.Count; i++)
            {
                if (i != selectedItemIndex)
                {
                    this[i].Selected = false;
                }
            }

            SelectedFinish.Selected = true;
        }

        public void Swap(int position1, int position2)
        {
            Point locn1 = areaFinishList[position1].Location;
            Point locn2 = areaFinishList[position2].Location;

            UCFinish tempUCFinish = areaFinishList[position1];

            areaFinishList[position1] = areaFinishList[position2];
            areaFinishList[position2] = tempUCFinish;

            areaFinishList[position1].Location = locn1;
            areaFinishList[position2].Location = locn2;

            areaFinishList[position1].positionOnPallet = position1;
            areaFinishList[position2].positionOnPallet = position2;

            Debug.Assert(ValidateControlPositions());
        }

        public UCFinish Add(FinishAreaBase finishAreaBase)
        {
            Debug.Assert(!areaFinishListContainsName(finishAreaBase.AreaName));

            if (areaFinishListContainsName(finishAreaBase.AreaName))
            {
                return null;
            }

            UCFinish ucFinish = new UCFinish(this, finishAreaBase);

            //ucFinish.AreaName = areaNameParm;
            //ucFinish.FinishColor = areaColor;
         

            ucFinish.layer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucFinish.Guid);
      
            ucFinish.bIsFiltered = false;
            ucFinish.positionOnPallet = this.areaFinishList.Count;

            this.pnlFinishList.Controls.Add(ucFinish);

            int lineLocY = 0;

            if (this.areaFinishList.Count > 0)
            {
                UCFinish lastUCFinish = this.areaFinishList[this.areaFinishList.Count - 1];

                lineLocY = lastUCFinish.Location.Y + lastUCFinish.Height + 1;
            }

            ucFinish.Location = new Point(0, lineLocY);

            this.areaFinishList.Add(ucFinish);
            //this.lineTypeDict.Add(UCFinish.LineName, UCFinish);

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            //this.Select(ucFinish.fIndex);

            this.Refresh();

            Debug.Assert(ValidateControlPositions());

            return ucFinish;
        }

        internal UCFinish Insert(int selectedElement, FinishAreaBase finishAreaBase)
        {
            if (this.areaFinishList.Count <= 0)
            {
               // return Add(finishAreaBase);
            }

            Debug.Assert(!areaFinishListContainsName(finishAreaBase.AreaName));
            Debug.Assert(selectedElement >= 0 && selectedElement < this.areaFinishList.Count);

            if (areaFinishListContainsName(finishAreaBase.AreaName))
            {
                return null;
            }

            if (selectedElement < 0 || selectedElement >= this.areaFinishList.Count)
            {
                return null;
            }

            UCFinish ucFinish = new UCFinish(this, finishAreaBase);
            
            ucFinish.layer = CanvasManager.CurrentPage.VisioPage.Layers.Add(ucFinish.Guid);
            ucFinish.bIsFiltered = false;

            ucFinish.positionOnPallet = selectedElement;

            this.pnlFinishList.Controls.Add(ucFinish);

            Size ucLineSize = this.areaFinishList[selectedElement].Size;

            ucFinish.Size = ucLineSize;

            int locnX = this.areaFinishList[selectedElement].Location.X;
            int locnY = this.areaFinishList[selectedElement].Location.Y;

            ucFinish.Location = new Point(locnX, locnY);

            this.areaFinishList.Insert(selectedElement, ucFinish);
            //this.lineTypeDict.Add(ucLine.LineName, ucLine);

            locnY += ucFinish.Height + 1;
        
            for (int index = selectedElement + 1; index < areaFinishList.Count; index++)
            {
                UCFinish ucFinish1 = this.areaFinishList[index];

                ucFinish1.positionOnPallet = index;
                ucFinish1.Location = new Point(locnX, locnY);

                locnY += ucFinish1.Height + 1;
            }

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.Select(ucFinish.positionOnPallet);

            this.Refresh();

            Debug.Assert(ValidateControlPositions());

            return ucFinish;
        }

        private bool areaFinishListContainsName(string areaNameParm)
        {
            foreach (UCFinish ucFinish in areaFinishList)
            {
                if (ucFinish.AreaName == areaNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        internal void Remove(int selectedElement)
        {
            Debug.Assert(selectedElement >= 0 && selectedElement < this.areaFinishList.Count);

            if (selectedElement < 0 || selectedElement >= this.areaFinishList.Count)
            {
                return;
            }

            UCFinish ucFinish = this.areaFinishList[selectedElement];

            //this.lineTypeDict.Remove(ucLine.LineName);
            this.areaFinishList.RemoveAt(selectedElement);

            this.pnlFinishList.Controls.Remove(ucFinish);

            ucFinish.layer.Delete(1);
           
            int locnX = ucFinish.Location.X;
            int locnY = ucFinish.Location.Y;

            for (int index = selectedElement; index < this.areaFinishList.Count; index++)
            {
                Point locn = new Point(locnX, locnY);

                UCFinish ucFinish1 = this.areaFinishList[index];

                locnX = ucFinish1.Location.X;
                locnY = ucFinish1.Location.Y;

                ucFinish1.positionOnPallet = index;
                ucFinish1.Location = locn;
            }

            if (this.areaFinishList.Count <= 0)
            {
                return;
            }

            if (selectedElement >= this.areaFinishList.Count)
            {
                selectedElement = this.areaFinishList.Count - 1;
            }

            int width = BaseForm.tbcPageAreaLine.Width;
            int height = BaseForm.tbcPageAreaLine.Height;

            this.SetSize(new System.Drawing.Size(width, height));

            this.Select(selectedElement);

            Debug.Assert(ValidateControlPositions());
        }

        internal void SetSeamForSelectedFinish(FinishSeamBase finishSeamBase)
        {
            SelectedFinish.SetSeam(finishSeamBase);

            this.lblTagLabel.Text = finishSeamBase.SeamName;
        }

        public UCFinish this[int index]
        {
            get
            {
                return areaFinishList[index];
            }
        }

#if DEBUG
        public bool ValidateControlPositions()
        {
            for (int i = 0; i < areaFinishList.Count; i++)
            {
                if (areaFinishList[i].positionOnPallet != i)
                {
                    Logger.LogError("In finish pallet, position of control " + i + " does not match actual position.");
                    return false;
                }

                if (areaFinishList[i].FinishAreaBase.Guid != FinishAreaBaseList[i].Guid)
                {
                    Logger.LogError("In finish pallet, control at location " + i + " does not sync with base element.");
                    return false;
                }
            }

            return true;
        }
#endif
    }

    public class FinishChangedEventArgs : EventArgs
    {
        public FinishChangedEventArgs(UCFinish ucFinish)
        {
            this.UCFinish = ucFinish;
        }

        public UCFinish UCFinish;
    }
}
