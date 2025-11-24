//-------------------------------------------------------------------------------//
// <copyright file="SeamFinishesEditForm.cs"                                     //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using FinishesLib;
    using Utilities;
    using Globals;

    public partial class SeamFinishesEditForm : Form, ICursorManagementForm, IDisposable
    {
        //private UCSeamPalette ucSeamPalette;

        private List<UCSeamListElement> seamListElementList = new List<UCSeamListElement>();

        private int selectedElement = 0;

        private AreaFinishBaseList areaFinishBaseList;

        private SeamFinishBaseList seamFinishBaseList;

        private FloorMaterialEstimatorBaseForm baseForm;

        private bool modal;

        private argb[] palletColors = new argb[]
        {
            new argb(255, 255, 255, 192),
            new argb(255, 255, 125, 125),
            new argb(255, 160, 255, 64),
            new argb(255, 128, 192, 255),
            new argb(255, 194, 133, 255),
            new argb(255, 255, 255, 0),
            new argb(255, 255, 0, 0),
            new argb(255, 0, 192, 0),
            new argb(255, 47, 151, 255),
            new argb(255, 167, 79, 255),
            new argb(255, 255, 159, 63),
            new argb(255, 192, 0, 0),
            new argb(255, 0, 128, 0),
            new argb(255, 0, 0, 255),
            new argb(255, 128, 0, 255),
            new argb(255, 192, 96, 0),
            new argb(255, 255, 128, 255),
            new argb(255, 0, 255, 128),
            new argb(255, 0, 102, 204),
            new argb(255, 0, 192, 192),
            new argb(255, 122, 61, 0),
            new argb(255, 255, 64, 160),
            new argb(255, 0, 192, 96),
            new argb(255, 0, 64, 128),
            new argb(255, 0, 128, 128),
            new argb(255, 0, 0, 0),
            new argb(255, 134, 134, 134),
            new argb(255, 182, 182, 182),
            new argb(255, 228, 228, 228),
            new argb(255, 255, 255, 255)
        };

        public bool ElementsChanged { get; set; }

        public SeamFinishesEditForm(FloorMaterialEstimatorBaseForm baseForm, bool modal)
        {
            InitializeComponent();

            this.ucCustomColorPalette.Init(palletColors);
            this.ucLineWidth.Init();
            //this.ucSeamPalette = ucLinePalette;

            this.baseForm = baseForm;

            this.areaFinishBaseList = baseForm.AreaFinishBaseList;

            this.seamFinishBaseList = baseForm.SeamFinishBaseList;

            this.modal = modal;

            buildSeamLineList();

            seamListElementList[0].Select();

            this.selectedElement = 0;

            selectSeamFromSelectedAreaFinish();

            //if (!modal)
            //{
            //    this.MouseEnter += SeamFinishesEditForm_MouseEnter;
            //    this.MouseLeave += SeamFinishesEditForm_MouseLeave;
            //}

            this.ucCustomColorPalette.ColorSelected += UcCustomColorPalette_ColorSelected;
            this.ucCustomDashType.DashTypeSelected += UcCustomDashType_DashTypeSelected;
            this.ucLineWidth.LineWidthSelected += UcLineWidth_LineWidthSelected;

            this.txbTag.TextChanged += TxbTag_TextChanged;
            this.txbTag.GotFocus += TxbTag_GotFocus;
            this.txbTag.LostFocus += TxbTag_LostFocus;
           
            ElementsChanged = false;

            if (modal)
            {
                this.btnSeamFinishEditorHide.Text = "Close";
            }

            else
            {
                this.btnSeamFinishEditorHide.Text = "Hide";
            }

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.areaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;

            AddToCursorManagementList();

            this.Disposed += SeamFinishesEditForm_Disposed;

            this.FormClosing += SeamFinishesEditForm_FormClosing;
        }


        private void AreaFinishBaseList_ItemSelected(int itemIndex)
        {
            selectSeamFromSelectedAreaFinish();
        }

        private void selectSeamFromSelectedAreaFinish()
        {
            if (areaFinishBaseList.SelectedItem.SeamFinishBase is null)
            {
                return;
            }

            string guid = areaFinishBaseList.SelectedItem.SeamFinishBase.Guid;

            foreach (UCSeamListElement seamListElement in this.seamListElementList)
            {
                if (seamListElement.Guid != guid)
                {
                    continue;
                }

                seamListElement.Select();

                this.selectedElement = seamListElement.lineListIndex;

                for (int i = 0; i < this.seamListElementList.Count ; i++)
                {
                    if (i == this.selectedElement)
                    {
                        continue;
                    }

                    seamListElementList[i].Deselect();
                }

                return;
            }
        }

        //private void SeamFinishesEditForm_MouseEnter(object sender, EventArgs e)
        //{
        //    baseForm.Cursor = Cursors.Arrow;
        //}

        //private void SeamFinishesEditForm_MouseLeave(object sender, EventArgs e)
        //{
        //    //baseForm.SetCursorForCurrentLocation();
        //}
        private void UcCustomColorPalette_ColorSelected(ColorSelectedEventArgs args)
        {
            ElementsChanged = true;

            SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

            // This assignment generates events to update all elements that use seams.

            finishSeamBase.SeamColor = Color.FromArgb(args.A, args.R, args.G, args.B);

            seamListElementList[selectedElement].Invalidate();

            updateColorDisplay(args.R, args.G, args.B, args.A);
        }

        private void UcCustomDashType_DashTypeSelected(DashTypeSelectedEventArgs args)
        {
            ElementsChanged = true;

            SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

            // This assignment generates events to update all elements that use seams.

            finishSeamBase.VisioDashType = (int)args.VisioDashTypeIndex;

            seamListElementList[selectedElement].Invalidate();

            //baseForm.areaPalette.SelectedFinish.Invalidate();

            //baseForm.UpdateSeamDefinition(finishSeamBase);

            //if (!(baseForm.FormAreaFinishes is null))
            //{
            //    baseForm.FormAreaFinishes.UpdateSeam();
            //}
        }

        private void UcLineWidth_LineWidthSelected(LineWidthSelectedEventArgs args)
        {
            ElementsChanged = true;

            SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

            // This assignment generates events to update all elements that use seams.

            finishSeamBase.SeamWidthInPts = args.WidthInPts;

            seamListElementList[selectedElement].Invalidate();
        }

        private void buildSeamLineList()
        {
            int lineCntlLocX = 4;
            int lineCntlLocY = 4;

            for (int i = 0; i < seamFinishBaseList.Count; i++)
            {
                SeamFinishBase seamFinishBase = seamFinishBaseList[i];

                UCSeamListElement ucSeam = new UCSeamListElement(seamFinishBase, i);

                seamListElementList.Add(ucSeam);

                this.pnlLineList.Controls.Add(ucSeam);

                ucSeam.Location = new Point(lineCntlLocX, lineCntlLocY);
                ucSeam.Size = new Size(this.pnlLineList.Width - 2 * lineCntlLocX, ucSeam.Height);

                lineCntlLocY += 2 + ucSeam.Height;

                ucSeam.Click += UcLine_Click;
            }

            seamFinishBaseList.ItemSelected += SeamFinishBaseList_ItemSelected;
        }

        private void UcLine_Click(object sender, EventArgs e)
        {
            UCSeamListElement ucLine = (UCSeamListElement)sender;

            // Update the selected seam. This triggers an event that is then caught by 'SeamFinishBaseList_ItemSelected'
            // below that actually causes the local pallet update.

            seamFinishBaseList.SelectElem(ucLine.SeamFinishBase);
        }

        private void SeamFinishBaseList_ItemSelected(int itemIndex)
        {
            select(itemIndex);
        }

        private void select(UCSeamListElement ucLine)
        {
            select(ucLine.lineListIndex);
        }

        private void select(int index)
        {
            selectedElement = index;

            for (int i = 0; i < seamListElementList.Count; i++)
            {
                if (i != selectedElement)
                {
                    seamListElementList[i].Deselect();
                }
            }

            UCSeamListElement ucSeam = seamListElementList[selectedElement];

            ucSeam.Select();

            //baseForm.SelectUCSeam(selectedElement);

            this.txbTag.Text = ucSeam.SeamName;

            this.txbProduct.Text = ucSeam.Product;

            this.TxbTag_GotFocus(null, null);

            this.txbNotes.Text = ucSeam.Notes;

            if (!(baseForm.FormAreaFinishes is null))
            {
                baseForm.FormAreaFinishes.UpdateSeam();
            }

            ucCustomColorPalette.SetSelectedButtonFormat(ucSeam.SeamFinishBase.SeamColor);

            ucCustomDashType.SetSelectedDashElementFormat(ucSeam.SeamFinishBase.VisioDashType);

            ucLineWidth.SetSelectedLineWidth(ucSeam.SeamFinishBase.SeamWidthInPts);

            updateColorDisplay(ucSeam.SeamFinishBase.SeamColor);

            //this.ucSeamPalette.Select(selectedElement);
        }
        private void updateColorDisplay(Color color)
        {
            updateColorDisplay(color.R, color.G, color.B, color.A);
        }

        private void updateColorDisplay(int r, int g, int b, int a)
        {
            this.lblCurrentColorR.Text = "R-" + r.ToString();
            this.lblCurrentColorG.Text = "G-" + g.ToString();
            this.lblCurrentColorB.Text = "B-" + b.ToString();
            this.lblCurrentColorA.Text = "A-" + a.ToString();

            this.lblCurrentColorR.ForeColor =  Color.FromArgb(255, r, 0, 0);
            this.lblCurrentColorG.ForeColor =  Color.FromArgb(255, 0, g, 0);
            this.lblCurrentColorB.ForeColor =  Color.FromArgb(255, 0, 0, b);
        }
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (selectedElement <= 0)
            {
                return;
            }

            ElementsChanged = true;

            swap(selectedElement - 1, selectedElement);

            seamFinishBaseList.SwapElems(selectedElement - 1, selectedElement);

            selectedElement--;

            this.Refresh();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (selectedElement >= seamListElementList.Count - 1)
            {
                return;
            }

            ElementsChanged = true;

            swap(selectedElement + 1, selectedElement);

            seamFinishBaseList.SwapElems(selectedElement + 1, selectedElement);

            selectedElement++;

            this.Refresh();
        }

        private void swap(int loc1Index, int loc2Index)
        {
            Point locn1 = seamListElementList[loc1Index].Location;
            Point locn2 = seamListElementList[loc2Index].Location;

            UCSeamListElement tempSeamListElement = seamListElementList[loc1Index];

            seamListElementList[loc1Index] = seamListElementList[loc2Index];
            seamListElementList[loc2Index] = tempSeamListElement;

            seamListElementList[loc1Index].Location = locn1;
            seamListElementList[loc2Index].Location = locn2;

            seamListElementList[loc1Index].lineListIndex = loc1Index;
            seamListElementList[loc2Index].lineListIndex = loc2Index;

            ElementsChanged = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int lineCntlLocX = 4;
            int lineCntlLocY = 4;

            string seamName = getUniqueSeamName();

            SeamFinishBase finishSeamBase = new SeamFinishBase()
            {
                
                SeamName = seamName,
                VisioDashType = SeamFinishBase.DefaultVisioDashType,
                SeamWidthInPts = SeamFinishBase.DefaultLineWidthInPts,
                SeamColor = SeamFinishBase.DefaultLineColor
            };

            finishSeamBase.Guid = GuidMaintenance.CreateGuid(finishSeamBase);

            UCSeamListElement ucLineListElement = new UCSeamListElement(finishSeamBase, this.seamListElementList.Count);

            if (this.seamListElementList.Count > 0)
            {
                UCSeamListElement lastLineListElement = this.seamListElementList[this.seamListElementList.Count - 1];

                lineCntlLocY = lastLineListElement.Location.Y + lastLineListElement.Height + 2;
            }

            seamListElementList.Add(ucLineListElement);

            seamFinishBaseList.AddElem(finishSeamBase);

            this.pnlLineList.Controls.Add(ucLineListElement);

            ucLineListElement.Location = new Point(lineCntlLocX, lineCntlLocY);
            

            ucLineListElement.Click += UcLine_Click;

            select(ucLineListElement);

            this.pnlLineList.Refresh();

            this.pnlLineList.ScrollControlIntoView(ucLineListElement);

            ElementsChanged = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.seamListElementList.Count <= 0)
            {
                btnAdd_Click(sender, e);
                return;
            }

            string seamName = getUniqueSeamName();

            SeamFinishBase finishSeamBase = new SeamFinishBase()
            {
                
                SeamName = seamName,
                VisioDashType = SeamFinishBase.DefaultVisioDashType,
                SeamWidthInPts = SeamFinishBase.DefaultLineWidthInPts,
                SeamColor = SeamFinishBase.DefaultLineColor
            };

            finishSeamBase.Guid = GuidMaintenance.CreateGuid(finishSeamBase);

            UCSeamListElement ucLineListElement = new UCSeamListElement(finishSeamBase, selectedElement);

            int locnY = seamListElementList[selectedElement].Location.Y;
            int locnX = seamListElementList[selectedElement].Location.X;

            ucLineListElement.Location = new Point(locnX, locnY);

            seamListElementList.Insert(selectedElement, ucLineListElement);

            seamFinishBaseList.InsertElem(finishSeamBase, selectedElement);

            this.pnlLineList.Controls.Add(ucLineListElement);

            ucLineListElement.Click += UcLine_Click;

            locnY += ucLineListElement.Height + 1;

            for (int index = selectedElement + 1; index < seamListElementList.Count; index++)
            {
                UCSeamListElement ucLineListElement1 = this.seamListElementList[index];

                ucLineListElement1.lineListIndex = index;
                ucLineListElement1.Location = new Point(locnX, locnY);

                locnY += 2 + ucLineListElement1.Height;
            }

            select(ucLineListElement);
           

            this.pnlLineList.Refresh();

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.seamListElementList.Count <= 0)
            {
                return;
            }

            if (this.seamListElementList.Count <= 1)
            {
                MessageBox.Show("At least one seam must remain on the list.");
                return;
            }

            ElementsChanged = true;

            UCSeamListElement ucSeamListElement = this.seamListElementList[selectedElement];

            if (IsInUse(ucSeamListElement.SeamFinishBase.Guid))
            {
                ManagedMessageBox.Show("This seam type is currently in use.");

                return;
            }

            this.seamListElementList.RemoveAt(selectedElement);

            this.seamFinishBaseList.RemoveElem(selectedElement);

            this.pnlLineList.Controls.Remove(ucSeamListElement);

            int locnX = ucSeamListElement.Location.X;
            int locnY = ucSeamListElement.Location.Y;

            for (int index = selectedElement; index < this.seamListElementList.Count; index++)
            {
                Point locn = new Point(locnX, locnY);

                UCSeamListElement ucLine1 = this.seamListElementList[index];

                locnX = ucLine1.Location.X;
                locnY = ucLine1.Location.Y;

                ucLine1.lineListIndex = index;
                ucLine1.Location = locn;
            }

            if (this.seamListElementList.Count <= 0)
            {
                return;
            }

            if (selectedElement >= this.seamListElementList.Count)
            {
                selectedElement = this.seamListElementList.Count - 1;
            }

            this.select(selectedElement);
        }

        private string getUniqueSeamName()
        {
            HashSet<string> existingSeamNames = new HashSet<string>();

            foreach (SeamFinishBase finishSeamBase in this.seamFinishBaseList)
            {
                string seamName = finishSeamBase.SeamName;

                if (seamName.StartsWith("Seam-"))
                {
                    existingSeamNames.Add(seamName);
                }
            }

            for (int i = 1; i < 1000; i++)
            {
                string seamName = "Seam-" + i.ToString();

                if (!existingSeamNames.Contains(seamName))
                {
                    return seamName;
                }
            }

            return string.Empty;
        }

        private bool IsInUse(string guid)
        {
            foreach (AreaFinishBase areaFinishBase in areaFinishBaseList)
            {
                if (areaFinishBase.SeamFinishBase is null)
                {
                    continue;
                }

                if (areaFinishBase.SeamFinishBase.Guid == guid)
                {
                    return true;
                }
            }

            return false;
        }
        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            SeamFinishBaseList finishSeamList = new SeamFinishBaseList();

            foreach (SeamFinishBase seamFinish in seamFinishBaseList)
            {
                SeamFinishBase clonedSeamFinishBase= seamFinish.Clone();

                clonedSeamFinishBase.LengthInInches = 0;

                finishSeamList.AddElem(clonedSeamFinishBase);
            }

            if (SystemGlobals.paletteSource == "Baseline")
            {
                if (Program.AppConfig.ContainsKey("seamlinebasefinishinitpath"))
                {
                    string outpFilePath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["seamlinebasefinishinitpath"]);

                    if (finishSeamList.Save(outpFilePath))
                    {
                        ManagedMessageBox.Show("The default baseline seam line specification has been updated.");

                        ElementsChanged = false;
                    }
                    else
                    {
                        ManagedMessageBox.Show("Attempt to update the baseline default seam line specification failed.");
                    }
                }

                else
                {
                    ManagedMessageBox.Show("No default output file path found for baseline seam defaults.");
                }

                return;
            }

            else
            {
                if (Program.AppConfig.ContainsKey("seamlinefinishinitpath"))
                {
                    string outpFilePath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["seamlinefinishinitpath"]);

                    if (finishSeamList.Save(outpFilePath))
                    {
                        ManagedMessageBox.Show("The default seam line specification has been updated.");

                        ElementsChanged = false;
                    }
                    else
                    {
                        ManagedMessageBox.Show("Attempt to update the default seam line specification failed.");
                    }
                }

                else
                {
                    ManagedMessageBox.Show("No default output file path found.");
                }

                return;
            }
        }
        
        private void txbProduct_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            seamListElementList[selectedElement].Product = this.txbProduct.Text.Trim();

            SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

            baseForm.UpdateSeamDefinition(finishSeamBase);
        }

        private HashSet<string> usedSeamFinishTags = new HashSet<string>();

        private string currTag = null;

        private void TxbTag_GotFocus(object sender, EventArgs e)
        {
            usedSeamFinishTags.Clear();

            for (int i = 0; i < this.seamFinishBaseList.Count; i++)
            {
                if (i == selectedElement)
                {
                    continue;
                }

                usedSeamFinishTags.Add(seamFinishBaseList[i].SeamName);
            }

            currTag = this.seamFinishBaseList[selectedElement].SeamName;

            string tag = this.txbTag.Text.Trim();

            if (usedSeamFinishTags.Contains(tag))
            {
                this.txbTag.BackColor = Color.Pink;
            }

            else
            {
                this.txbTag.BackColor = SystemColors.ControlLightLight;
            }
        }

        bool ignoreTxbTag_LostFocus = false;

        private void TxbTag_LostFocus(object sender, EventArgs e)
        {
           
            if (ignoreTxbTag_LostFocus)
            {
                return;
            }

            validateCurrentTag();


        }

        private void TxbTag_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            string tag = this.txbTag.Text.Trim();

            if (tag.Contains(";"))
            {

                ignoreTxbTag_LostFocus = true;

                MessageBox.Show("Invalid character, ';' in tag name.");

                ignoreTxbTag_LostFocus = false;

                return;
            }

            if (usedSeamFinishTags.Contains(tag))
            {
                this.txbTag.BackColor = Color.Pink;
            }

            else
            {
                this.txbTag.BackColor = SystemColors.ControlLightLight;
            }

            this.seamFinishBaseList[selectedElement].SeamName = tag;

            this.seamListElementList[selectedElement].Invalidate();

            //baseForm.areaPalette.lblTagLabel.Text = tag;

            SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

            baseForm.UpdateSeamDefinition(finishSeamBase);
        }

        private void txbNotes_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            seamListElementList[selectedElement].Notes = this.txbNotes.Text.Trim();

            SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

            baseForm.UpdateSeamDefinition(finishSeamBase);
        }

        private void btnSeamFinishEditorHide_Click(object sender, EventArgs e)
        {
            if (!modal)
            {
                this.WindowState = FormWindowState.Minimized;
            }

            else
            {
                this.Close();
            }
        }

        #region Closing and Disposing
        private void SeamFinishesEditForm_Disposed(object sender, EventArgs e)
        {
            this.RemoveFromCursorManagementList();

            this.areaFinishBaseList.ItemSelected -= AreaFinishBaseList_ItemSelected;

            this.ucCustomColorPalette.ColorSelected -= UcCustomColorPalette_ColorSelected;
            this.ucCustomDashType.DashTypeSelected -= UcCustomDashType_DashTypeSelected;
            this.ucLineWidth.LineWidthSelected -= UcLineWidth_LineWidthSelected;

            this.txbTag.TextChanged -= TxbTag_TextChanged;
            this.txbTag.GotFocus -= TxbTag_GotFocus;
            this.txbTag.LostFocus -= TxbTag_LostFocus;
        }

        private void SeamFinishesEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ignoreTxbTag_LostFocus = true;

            if (!validateCurrentTag())
            {

                e.Cancel = true;
                ignoreTxbTag_LostFocus = false;
                return;
            }
          
        }
        #endregion

        private bool validateCurrentTag()
        {
            string tag = this.txbTag.Text.Trim();

            if (tag.Contains(";"))
            {
                ignoreTxbTag_LostFocus = true;

                MessageBox.Show("Remove invalid character, ';', from tag name before proceeding.");

                ignoreTxbTag_LostFocus = false;

                return false;
            }

            if (usedSeamFinishTags.Contains(tag))
            {
                ManagedMessageBox.Show("The tag '" + tag + "' is already being used by another finish.");

                this.txbTag.Text = currTag;

                this.seamFinishBaseList[selectedElement].SeamName = currTag;

                this.seamListElementList[selectedElement].Invalidate();

                SeamFinishBase finishSeamBase = seamFinishBaseList[selectedElement];

                baseForm.UpdateSeamDefinition(finishSeamBase);

return false;
            }

            return true;
        }

        #region Cursor Management
        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
        }

        private void FinishesEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveFromCursorManagementList();
        }

        public bool CursorWithinBounds()
        {
            return base.Bounds.Contains(Cursor.Position);
        }

        public void AddToCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Add(this);
        }

        public void RemoveFromCursorManagementList()
        {
            CursorManager.CursorManagerFormList.Remove(this);
        }

        public bool IsTopMost { get; set; } = false;

        #endregion
    }
}
