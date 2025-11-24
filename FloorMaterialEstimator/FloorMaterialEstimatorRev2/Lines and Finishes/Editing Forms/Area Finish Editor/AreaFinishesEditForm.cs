//#define UsingOldMethod
#define UsingNewMethod

namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using SettingsLib;
    using FloorMaterialEstimator;
    using FinishesLib;
    using PaletteLib;
    using Utilities;
    using Utilities.Supporting_Controls;
    using System.Diagnostics;
    using System.Collections;
    using FloorMaterialEstimator.CanvasManager;
    using Globals;
    using System.Text.RegularExpressions;

    public partial class AreaFinishesEditForm : Form, IEnumerable<UCAreaFinishEditFormElement>, ICursorManagementForm, IDisposable
    {
        FloorMaterialEstimatorBaseForm baseForm;

        CanvasManager canvasManager => baseForm.CanvasManager;

        CanvasPage currentPage => canvasManager.CurrentPage;

        public AreaFinishBaseList AreaFinishBaseList;

        public AreaFinishManagerList AreaFinishManagerList => baseForm.AreaFinishManagerList;

        public UCAreaFinishPalette ucAreaFinishPalette;

        private SeamFinishBaseList seamFinishBaseList;

        private List<UCSeamListElement> seamListElementList = new List<UCSeamListElement>();

        private int selectedElement = 0;

        private bool modal;

        private AreaFinishBase currAreaFinishBase;

        private argb[] palletColors = new argb[]
        {
            
            #region Palette Colors

            new argb(255, 255, 255, 192),
            new argb(255, 255, 192, 192),
            new argb(255, 192, 255, 192),
            new argb(255, 192, 192, 255),
            new argb(255, 224, 192, 255),
            new argb(255, 255, 255, 0),
            new argb(255, 255, 128, 128),
            new argb(255, 64, 255, 64),
            new argb(255, 128, 128, 255),
            new argb(255, 192, 128, 255),
            new argb(255, 255, 224, 192),
            new argb(255, 255, 0, 0),
            new argb(255, 0, 192, 0),
            new argb(255, 64, 64, 255),
            new argb(255, 128, 0, 255),
            new argb(255, 255, 192, 128),
            new argb(255, 255, 192, 224),
            new argb(255, 224, 255, 192),
            new argb(255, 192, 224, 255),
            new argb(255, 0, 192, 192),
            new argb(255, 255, 160, 64),
            new argb(255, 255, 128, 192),
            new argb(255, 128, 255, 192),
            new argb(255, 64, 160, 255),
            new argb(255, 0, 128, 128),
            new argb(255, 192, 96, 0),
            new argb(255, 255, 64, 160),
            new argb(255, 128, 255, 0),
            new argb(255, 0, 128, 255),
            new argb(255, 128, 128, 128)

            #endregion
        };

        public int SelectedItemIndex
        {
            get
            {
                return AreaFinishBaseList.SelectedItemIndex;
            }

            set
            {
                AreaFinishBaseList.SelectedItemIndex = value;
            }
        }


        public UCFlowLayoutPanel<UCAreaFinishEditFormElement> ucFlowLayoutPanel;

        public AreaFinishesEditForm(
            FloorMaterialEstimatorBaseForm baseForm,
            UCAreaFinishPalette ucAreaFinishPalette,
            AreaFinishBaseList areaFinishBaseList,
            SeamFinishBaseList seamFinishBaseList,
            bool modal)
        {
            InitializeComponent();

            this.ucCustomColorPalette.Init(palletColors);
            this.ucCustomPatternPallet.Init(areaFinishBaseList, ucCustomColorPalette);

            this.baseForm = baseForm;

            this.ucAreaFinishPalette = ucAreaFinishPalette;

            this.AreaFinishBaseList = areaFinishBaseList;

            this.seamFinishBaseList = seamFinishBaseList;

            currAreaFinishBase = areaFinishBaseList.SelectedItem;

            this.cmbExtraInchesPerCut.Text = currAreaFinishBase.ExtraInchesPerCut.ToString();
            this.cmbRollOverlap.Text = currAreaFinishBase.OverlapInInches.ToString();

            this.modal = modal;

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCAreaFinishEditFormElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;

            this.Controls.Add(ucFlowLayoutPanel);

            ucFlowLayoutPanel.Location = new Point(22, 36);
            ucFlowLayoutPanel.Size = new Size(270, 284);

            
            for (int i = 0; i <= 6; i++)
            {
                this.cmbRollOverlap.Items.Add(i.ToString());
            }

            int position = 0;

            foreach (AreaFinishBase finishAreaBase in AreaFinishBaseList)
            {
                Add(finishAreaBase, position++);

                finishAreaBase.AreaFinishBaseSummary = new AreaFinishBaseSummary(finishAreaBase);
            }

            //if (!modal)
            //{
            //    this.MouseEnter += AreaFinishesEditForm_MouseEnter;
            //    this.MouseLeave += AreaFinishesEditForm_MouseLeave;
            //}

            this.FormClosing += AreaFinishesEditForm_FormClosing;

            this.ucCustomColorPalette.ColorSelected += UcCustomColorPalette_ColorSelected;

            AreaFinishBaseList.ItemAdded += AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted += AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved += AreaFinishBaseList_ItemRemoved;
            AreaFinishBaseList.ItemsSwapped += AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected += AreaFinishBaseList_ItemSelected;
            
            //this.txbRollWidthFeet.Text = "12";
            //this.txbRollWidthInch.Text = "0.0";

            this.txbAreaFinishTag.GotFocus += txbTag_GotFocus;
            this.txbAreaFinishTag.LostFocus += txbTag_LostFocus;
            this.txbAreaFinishTag.TextChanged += txbTag_TextChanged;

            this.cmbRollOverlap.TextChanged += CmbRollOverlap_TextChanged;
            this.cmbRollOverlap.SelectedIndexChanged += CmbRollOverlap_SelectedIndexChanged;

            this.cmbExtraInchesPerCut.SelectedIndexChanged += CmbExtraInchesPerCut_SelectedIndexChanged;
            this.cmbExtraInchesPerCut.TextChanged += CmbExtraInchesPerCut_TextChanged;


            ElementsChanged = false;

            if (modal)
            {
                this.btnAreaFinishEditorHide.Text = "Close";
            }

            else
            {
                this.btnAreaFinishEditorHide.Text = "Hide";
            }

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            buildSeamList();

            this.VisibleChanged += AreaFinishesEditForm_VisibleChanged;

            Select(AreaFinishBaseList.SelectedItemIndex);


            this.pnlFillPattern.Enabled = SelectedItem.MaterialsType == MaterialsType.Tiles;

            // AddToCursorManagementList();

            // Activate to use old method
#if UsingOldMethod
            txbRollWidthFeet.LostFocus += TxbRollWidthFeet_LostFocus;
            txbRollWidthInch.LostFocus += TxbRollWidthInch_LostFocus;
#else

#endif
           
            this.KeyPress += AreaFinishesEditForm_KeyPress;

           // this.KeyPreview = true;

            this.nudPatternLineWidth.Value = (Decimal) this.AreaFinishBaseList.SelectedItem.FillPatternLineWidthInPts;
            this.nudPatternLineDensity.Value = (Decimal)this.AreaFinishBaseList.SelectedItem.FillPatternInterLineIndex;

            this.Disposed += AreaFinishesEditForm_Disposed;
        }

        private void AreaFinishesEditForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                UCAreaFinishEditFormElement ucAreaFinish = this[SelectedItemIndex];

                ucFlowLayoutPanel.ScrollControlIntoView(ucAreaFinish);
            }

            else
            {
                foreach (UCAreaFinishEditFormElement ucFlowLayoutPanel in this)
                {
                    ucFlowLayoutPanel.UseFullOpacity = false;
                    ucFlowLayoutPanel.UpdateColor();
                }
            }
        }

        private void CmbRollOverlap_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblOverlap.Focus();
        }

        private void CmbRollOverlap_TextChanged(object sender, EventArgs e)
        {
            string overlap = this.cmbRollOverlap.Text.Trim();

            if (string.IsNullOrEmpty(overlap))
            {
                this.cmbRollOverlap.BackColor = SystemColors.ControlLightLight;

                return;
            }

            if (!Utilities.IsAllDigits(overlap))
            {
                this.cmbRollOverlap.BackColor = Color.Pink;
            }

            else
            {
                this.cmbRollOverlap.BackColor = SystemColors.ControlLightLight;

                FinishManagerGlobals.SelectedAreaFinishManager.OverlapInInches = int.Parse(overlap);
                //this.ucAreaFinishPalette.SelectedFinish.AreaFinishManager.OverlapInInches = int.Parse(overlap);
            }
        }

        private void CmbExtraInchesPerCut_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblExtraInchesPerCut.Focus();
        }

        private void CmbExtraInchesPerCut_TextChanged(object sender, EventArgs e)
        {
            string extraInchesPerCut = this.cmbExtraInchesPerCut.Text.Trim();

            if (string.IsNullOrEmpty(extraInchesPerCut))
            {
                this.cmbExtraInchesPerCut.BackColor = SystemColors.ControlLightLight;

                return;
            }

            if (!Utilities.IsAllDigits(extraInchesPerCut))
            {
                this.cmbExtraInchesPerCut.BackColor = Color.Pink;
            }

            else
            {
                this.cmbExtraInchesPerCut.BackColor = SystemColors.ControlLightLight;

                FinishManagerGlobals.SelectedAreaFinishManager.ExtraInchesPerCut = int.Parse(extraInchesPerCut);
                //this.ucAreaFinishPalette.SelectedFinish.AreaFinishManager.ExtraInchesPerCut = int.Parse(extraInchesPerCut);

                baseForm.OversUndersFormUpdate(false);
            }
        }

        private void buildSeamList()
        {
            int lineCntlLocX = 4;
            int lineCntlLocY = 4;

            for (int i = 0; i < seamFinishBaseList.Count; i++)
            {
                UCSeamListElement ucSeam = new UCSeamListElement(seamFinishBaseList[i], i);

                this.pnlSeamList.Controls.Add(ucSeam);

                this.seamListElementList.Add(ucSeam);

                ucSeam.Location = new Point(lineCntlLocX, lineCntlLocY);
                ucSeam.Size = new Size(this.pnlSeamList.Width - 2 * lineCntlLocX, ucSeam.Height);

                lineCntlLocY += 2 + ucSeam.Height;

                ucSeam.Click += UcLine_Click;
            }

            seamFinishBaseList.ItemAdded += SeamFinishBaseList_ItemAdded;
            seamFinishBaseList.ItemInserted += SeamFinishBaseList_ItemInserted;
            seamFinishBaseList.ItemsSwapped += SeamFinishBaseList_ItemsSwapped;
            seamFinishBaseList.ItemRemoved += SeamFinishBaseList_ItemRemoved;
        }


        private void SeamFinishBaseList_ItemAdded(SeamFinishBase item)
        {
            Point addedItemLocation = new Point(4, 4);

            int count = this.pnlSeamList.Controls.Count;

            if (count > 0)
            {
                UCSeamListElement lastSeamListElem = (UCSeamListElement)this.pnlSeamList.Controls[count - 1];

                addedItemLocation = new Point(lastSeamListElem.Location.X, lastSeamListElem.Location.Y + lastSeamListElem.Height + 2);
            }

            count = seamFinishBaseList.Count;

            UCSeamListElement ucSeam = new UCSeamListElement(seamFinishBaseList[count - 1], count - 1);

            this.pnlSeamList.Controls.Add(ucSeam);

            this.seamListElementList.Add(ucSeam);

            ucSeam.Location = addedItemLocation;
            ucSeam.Size = new Size(this.pnlSeamList.Width - 2 * addedItemLocation.X, ucSeam.Height);

            ucSeam.Click += UcLine_Click;

            // This scrolls the new seam into view. There is a question as to whether or not this should be done
            // or whether it is better to keep the focus on the selected area finish.

            this.pnlSeamList.ScrollControlIntoView(ucSeam);
        }

        private void SeamFinishBaseList_ItemInserted(SeamFinishBase item, int position)
        {
            UCSeamListElement newSeamListElem = new UCSeamListElement(item, position);

            newSeamListElem.Size = new Size(this.pnlSeamList.Width - 2 * 4, newSeamListElem.Height);

            this.pnlSeamList.Controls.Add(newSeamListElem);

            newSeamListElem.Click += UcLine_Click;

            this.seamListElementList.Insert(position, newSeamListElem);

            for (int i = position; i < seamListElementList.Count - 1; i++)
            {
                this.seamListElementList[i].Location = this.seamListElementList[i + 1].Location;

                this.seamListElementList[i].lineListIndex = i;
            }

            this.seamListElementList[seamListElementList.Count - 1].Location
                = new Point(
                    this.seamListElementList[seamListElementList.Count - 2].Location.X,
                    this.seamListElementList[seamListElementList.Count - 2].Location.Y + this.seamListElementList[seamListElementList.Count - 2].Height + 2);

            this.seamListElementList[seamListElementList.Count - 1].lineListIndex = seamListElementList.Count - 1;
        }

        private void SeamFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            UCSeamListElement seamListElement1 = seamListElementList[position1];
            UCSeamListElement seamListElement2 = seamListElementList[position2];

            Point location1 = seamListElementList[position1].Location;
            Point location2 = seamListElementList[position2].Location;

            seamListElementList[position1] = seamListElement2;
            seamListElementList[position2] = seamListElement1;

            seamListElement1.Location = location2;
            seamListElement2.Location = location1;

            seamListElement1.lineListIndex = position2;
            seamListElement2.lineListIndex = position1;
        }

        private void SeamFinishBaseList_ItemRemoved(string guid, int position)
        {
            if (position < 0 || position >= seamListElementList.Count)
            {
                return;
            }

            UCSeamListElement seamListElem = seamListElementList[position];

            this.pnlSeamList.Controls.Remove(seamListElem);

            for (int i = position + 1; i < seamListElementList.Count; i++)
            {
                this.seamListElementList[i].Location = this.seamListElementList[i - 1].Location;
            }

            this.seamListElementList.RemoveAt(position);
        }

        private void UcLine_Click(object sender, EventArgs e)
        {
            UCSeamListElement ucLine = (UCSeamListElement)sender;

            int lineListIndex = ucLine.lineListIndex;

            // Note that in this case, we don't subscribe and use the seam selected event to change the selected seam.
            // The reason is that we don't want to update other elements (seam editor) when we select the seam here.

            seamFinishBaseList.SelectElem(ucLine.SeamFinishBase);


            selectSeam(lineListIndex);
        }

        private void selectSeam(SeamFinishBase seamFinishBase)
        {
            if (seamFinishBase is null)
            {
                return;
            }

            selectSeam(seamFinishBaseList.IndexOf(seamFinishBase));
        }

        private void selectSeam(int index)
        {
            if (index < 0 || index >= this.seamListElementList.Count)
            {
                return;
            }

            selectedElement = index;

            for (int i = 0; i < this.seamListElementList.Count; i++)
            {
                if (i != selectedElement)
                {
                    ((UCSeamListElement)this.seamListElementList[i]).Deselect();
                }
            }

            UCSeamListElement ucSeam = (UCSeamListElement)this.seamListElementList[selectedElement];

            ucSeam.Select();

            this.pnlSeamList.ScrollControlIntoView(ucSeam);

            UCAreaFinishEditFormElement editFormElement = this[SelectedItemIndex];

            if (editFormElement.AreaFinishBase.MaterialsType != MaterialsType.Rolls)
            {
                return;
            }

            editFormElement.AreaFinishBase.SeamFinishBase = ucSeam.SeamFinishBase;

            editFormElement.UpdateSeam();

            UCAreaFinishPaletteElement finishPaletteElement = PalettesGlobal.AreaFinishPalette[SelectedItemIndex];

            finishPaletteElement.UpdateSeam();

            this.lblSeamLabel.Text = ucSeam.SeamName;

            baseForm.seamPalette.UpdateSeamList();

            currentPage.UpdateSeamTotals();
        }

        private void AreaFinishesEditForm_MouseEnter(object sender, EventArgs e)
        {
            baseForm.SetCursorForCurrentLocation();
        }

        private void AreaFinishesEditForm_MouseLeave(object sender, EventArgs e)
        {
            //baseForm.SetCursorForCurrentLocation();
        }

        internal void UpdateSeam()
        {
            UCAreaFinishEditFormElement editFormElement = this[SelectedItemIndex];
            editFormElement.UpdateSeam();

            ElementsChanged = true;
        }

        private void AreaFinishesEditForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ElementsChanged = true;

            if (e.KeyChar == 26 && KeyboardUtils.CntlKeyPressed)
            {
                processUndoColor();
            }

            if (e.KeyChar == 68 || e.KeyChar == 100 || e.KeyChar == 4)
            {
                btnDuplicate_Click(null, null);

                return;
            }

            if (e.KeyChar < '1' || e.KeyChar > '9')
            {
                return;
            }

            int KeyAscii = e.KeyChar - '0';

            if (baseForm.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcAreaModeIndex)
            {

                if (this.txbAreaFinishTag.Focused || this.txbProduct.Focused || this.txbNotes.Focused)
                {
                    return;
                }

                baseForm.CanvasManager.ProcessAreaModeFinishNumericShortCut(KeyAscii);
                return;

            }

            else if (baseForm.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcLineModeIndex)
            {
                baseForm.CanvasManager.ProcessLineModeFinishNumericShortCut(KeyAscii);
                return;

            }
        }

        private void UcCustomColorPalette_ColorSelected(ColorSelectedEventArgs args)
        {
            ElementsChanged = true;

            UCAreaFinishPaletteElement ucFinish = ucAreaFinishPalette[SelectedItemIndex];

            UCAreaFinishEditFormElement editFormElement = this[SelectedItemIndex];

            editFormElement.colorHistory.Push(ucFinish.AreaFinishBase.Color);

            int A = (int)Math.Max(0, Math.Min(255, Math.Round(ucFinish.Opacity * 255.0)));

            ucFinish.AreaFinishBase.Color = Color.FromArgb(A, args.R, args.G, args.B);

            this.lblRed.Text = "R-" + args.R.ToString();
            this.lblGreen.Text = "G-" + args.G.ToString();
            this.lblBlue.Text = "B-" + args.B.ToString();

            //ucFinish.pnlColor.BackColor = Color.FromArgb(args.A, args.R, args.G, args.B);

            editFormElement.UseFullOpacity = true;

            editFormElement.UpdateColor();

            updateColorDisplay(args.R, args.G, args.B, args.A);

            //ucAreaFinishPalette.BaseForm.pnlFinishColor.BackColor = ucFinish.FinishColor;
        }

        private void processUndoColor()
        {

            UCAreaFinishPaletteElement ucFinish = ucAreaFinishPalette[SelectedItemIndex];

            UCAreaFinishEditFormElement editFormElement = this[SelectedItemIndex];

            if (editFormElement.colorHistory.Count > 0)
            {
                Color prevColor = editFormElement.colorHistory.Pop();

                ucFinish.AreaFinishBase.Color = prevColor;

                this.ucCustomColorPalette.SetSelectedButtonFormat(prevColor);

                this.ucCustomPatternPallet.UCCustomColorPallet_ColorSelected(null);

                editFormElement.UpdateColor();
            }
        }

        #region Palette Changes

        #region Add Element

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string areaName = getUniqueAreaName();

            AreaFinishBase finishAreaBase = AreaFinishBase.DefaultAreaFinish.Clone();
            finishAreaBase.AreaName = areaName;

            this.AreaFinishBaseList.AddElem(finishAreaBase, this.AreaFinishBaseList.Count);
        }

        private void AreaFinishBaseList_ItemAdded(AreaFinishBase finishAreaBase)
        {
            
            Add(finishAreaBase, this.AreaFinishBaseList.Count - 1);

            this.pnlFillPattern.Enabled = SelectedItem.MaterialsType == MaterialsType.Tiles;
        }

        private void Add(AreaFinishBase finishAreaBase, int position)
        {
            ElementsChanged = true;

            UCAreaFinishEditFormElement ucAreaListElement = new UCAreaFinishEditFormElement(this, finishAreaBase, position);

            Add(ucAreaListElement);

            ucAreaListElement.positionOnPalette = position;

            ucFlowLayoutPanel.ScrollControlIntoView(ucAreaListElement);


            ucAreaListElement.ControlClicked += UcAreaListElement_ControlClicked;
        }

        #endregion

        #region Insert Element

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string areaName = getUniqueAreaName();

            AreaFinishBase finishAreaBase = AreaFinishBase.DefaultAreaFinish.Clone();
            finishAreaBase.AreaName = areaName;

            this.AreaFinishBaseList.InsertElem(finishAreaBase, SelectedItemIndex, SelectedItemIndex);
        }


        Random random = new Random(128);

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (SelectedItemIndex < 0 || SelectedItemIndex >= ucAreaFinishPalette.Count)
            {
                MessageBox.Show("A area finish must be selected to duplicate.");

                return;
            }

            UCAreaFinishPaletteElement ucFinish = ucAreaFinishPalette[SelectedItemIndex];

            AreaFinishBase areaFinishBase = ucFinish.AreaFinishBase;

            string areaName = getUniqueAreaNameFromBase(areaFinishBase);

            if (string.IsNullOrEmpty(areaName))
            {
                MessageBox.Show("Cannot create duplicate area: unable to create unique area name.");
                return;
            }

            AreaFinishBase duplicateAreaFinishBase = areaFinishBase.Clone();


            if (KeyboardUtils.CntlKeyPressed)
            {
                Color color = areaFinishBase.Color;

                double testPct = getUpdatePct(color);

                double updatePct = 0.1;

                if (testPct < 0.1)
                {
                    updatePct = testPct;
                }

                else if (testPct > .15)
                {
                    updatePct = .15;
                }

                Color duplicateColor = ColorUtils.Lighten(color, (float)updatePct);

                duplicateAreaFinishBase.Color = duplicateColor;
            }


            duplicateAreaFinishBase.AreaName = areaName;

            this.AreaFinishBaseList.InsertElem(duplicateAreaFinishBase, SelectedItemIndex + 1, SelectedItemIndex + 1);
        }

        private double getUpdatePct(Color color)
        {
            int maxNotMax = -1;

            int r = color.R;
            int g = color.G;
            int b = color.B;

            if (r < 255)
            {
                maxNotMax = r;
            }

            if (g < 255)
            {
                if (g > maxNotMax)
                {
                    maxNotMax = g;
                }
            }

            if (b < 255)
            {
                if (b > maxNotMax)
                {
                    maxNotMax = b;
                }
            }

            double pct = (255.0 - (double)maxNotMax) / 255.0;

            return pct;
        }

        private string getUniqueAreaNameFromBase(AreaFinishBase areaFinishBase)
        {
            Regex regex = new Regex("^((?'base'.+?)(?'extension' [a-z])?)$");

            string areaFinishBaseName = areaFinishBase.AreaName.Trim();

            if (string.IsNullOrEmpty(areaFinishBaseName))
            {
                return String.Empty;
            }

            Match match = regex.Match(areaFinishBaseName);

            if (!match.Success)
            {
                return String.Empty;
            }

            Group baseGroup = match.Groups["base"];

            if (!baseGroup.Success)
            {
                return String.Empty;
            }


            string baseName = match.Groups["base"].Value;

            if (string.IsNullOrEmpty(baseName))
            {
                return String.Empty;
            }

            HashSet<char> existingExtensions = new HashSet<char>();

            Group extension = match.Groups["extension"];

            if (extension.Success)
            {
                existingExtensions.Add(extension.Value[1]);
            }

            foreach (UCAreaFinishEditFormElement ucAreaElement in this)
            {
                string compareLineName = ucAreaElement.AreaName.Trim();

                Match compareMatch = regex.Match(compareLineName);

                if (!compareMatch.Success)
                {
                    continue;
                }

                Group compareBaseGroup = compareMatch.Groups["base"];

                if (!compareBaseGroup.Success)
                {
                    continue;
                }

                string compareBase = compareBaseGroup.Value;

                if (baseName != compareBase)
                {
                    continue;
                }

                Group compareExtensionGroup = compareMatch.Groups["extension"];

                if (!compareExtensionGroup.Success)
                {
                    continue;
                }

                existingExtensions.Add(compareExtensionGroup.Value[1]);
            }

            char newExtension = (char)0;

            for (int i = 0; i < 26; i++)
            {
                if (!existingExtensions.Contains((char)('a' + i)))
                {
                    newExtension = (char)('a' + i);

                    break;
                }
            }

            if (newExtension > (char)0)
            {
                return baseName + ' ' + newExtension;
            }

            return string.Empty;


        }

        private void AreaFinishBaseList_ItemInserted(AreaFinishBase finishAreaBase, int position)
        {
            Insert(finishAreaBase, position);

            this.pnlFillPattern.Enabled = SelectedItem.MaterialsType == MaterialsType.Tiles;
        }

        internal void Insert(AreaFinishBase finishAreaBase, int position)
        {
            ElementsChanged = true;

            if (Count <= 0)
            {
                Debug.Assert(position == 0);

                Add(finishAreaBase, 0);

                return;
            }

            Debug.Assert(!areaFinishListContainsName(finishAreaBase.AreaName));
            Debug.Assert(position >= 0 && position < Count);

            if (areaFinishListContainsName(finishAreaBase.AreaName))
            {
                return;
            }

            if (position < 0 || position >= Count)
            {
                return;
            }

            UCAreaFinishEditFormElement ucAreaListElement = new UCAreaFinishEditFormElement(this, finishAreaBase, position);

            ucAreaListElement.ControlClicked += UcAreaListElement_ControlClicked;


            Add(ucAreaListElement);

            this[position].BorderStyle = BorderStyle.None;

            SetChildIndex(ucAreaListElement, position);

            for (int i = 0; i < Count; i++)
            {
                this[i].positionOnPalette = i;
            }

            if (finishAreaBase.MaterialsType == MaterialsType.Rolls)
            {
                if (!this.ckbRolls.Checked)
                {
                    this.ckbRolls.Checked = true;
                }
            }

            else if (finishAreaBase.MaterialsType == MaterialsType.Tiles)
            {
                if (!this.ckbTiles.Checked)
                {
                    this.ckbTiles.Checked = true;
                }
            }
            
            ucFlowLayoutPanel.ScrollControlIntoView(ucAreaListElement);

            return;
        }

        #endregion

        #region Remove Element

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (Count <= 0)
            {
                return;
            }

            if (Count <= 1)
            {
                MessageBox.Show("At least one area finish must remain on the list.");
                return;
            }

            if (IsInUse(SelectedItemIndex))
            {
                ManagedMessageBox.Show("This area finish is in use and therefore cannot be removed.");

                return;
            }

            AreaFinishBaseList.RemoveElem(SelectedItemIndex, Math.Min(SelectedItemIndex, Count - 2));
        }

        private void AreaFinishBaseList_ItemRemoved(string guid, int position)
        {
            Remove(position);

            this.pnlFillPattern.Enabled = SelectedItem.MaterialsType == MaterialsType.Tiles;
        }

        internal void Remove(int position)
        {
            ElementsChanged = true;

            Debug.Assert(position >= 0 && position < Count);

            if (position < 0 || position >= Count)
            {
                return;
            }

            UCAreaFinishEditFormElement ucAreaListElement = this[position];

            //this.areaFinishList.RemoveAt(position);

            RemoveAt(position);

            ucAreaListElement.ControlClicked -= UcAreaListElement_ControlClicked;

            for (int index = position; index < Count; index++)
            {
                UCAreaFinishEditFormElement ucAreaListElement1 = this[index];

                ucAreaListElement1.positionOnPalette = index;
            }
        }

        private bool IsInUse(int index)
        {
            return FinishManagerGlobals.AreaFinishManagerList[index].CanvasLayoutAreaDict.Count > 0;
        }

        #endregion

        #region Swap Elements

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (SelectedItemIndex <= 0)
            {
                return;
            }
            ElementsChanged = true;

            AreaFinishBaseList.SwapElems(SelectedItemIndex - 1, SelectedItemIndex, SelectedItemIndex - 1);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (SelectedItemIndex >= Count - 1)
            {
                return;
            }

            ElementsChanged = true;

            AreaFinishBaseList.SwapElems(SelectedItemIndex + 1, SelectedItemIndex, SelectedItemIndex + 1);
        }


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

            this.pnlFillPattern.Enabled = SelectedItem.MaterialsType == MaterialsType.Tiles;
        }

        public void Swap(int position1, int position2)
        {
            Debug.Assert(position1 >= 0 && position1 < Count);
            Debug.Assert(position2 >= 0 && position2 < Count);

            UCAreaFinishEditFormElement ucAreaListElement1 = this[position1];
            UCAreaFinishEditFormElement ucAreaListElement2 = this[position2];

            SetChildIndex(ucAreaListElement2, position1);
            SetChildIndex(ucAreaListElement1, position2);

            ucAreaListElement1.positionOnPalette = position2;
            ucAreaListElement2.positionOnPalette = position1;

            ElementsChanged = true;
        }

        #endregion

        #region Select Element

        private void UcAreaListElement_ControlClicked(UCAreaFinishEditFormElement ucAreaListElement)
        {
            if (SelectedItem.AreaFinishBase.MaterialsType == MaterialsType.Rolls && SelectedItem.AreaFinishBase.SeamFinishBase == null)
            {
                ManagedMessageBox.Show("A seam type has not been selected for the current finish type.");
                return;
            }

            AreaFinishBaseList.SelectElem(ucAreaListElement.positionOnPalette);
        }

        bool withinAreaFinishBaseListItemSelected = false;

        private void AreaFinishBaseList_ItemSelected(int position)
        {
            if (withinAreaFinishBaseListItemSelected)
            {
                return;
            }

            withinAreaFinishBaseListItemSelected = true;

            doAreaFinishBaseList_ItemSelected(position);

            withinAreaFinishBaseListItemSelected = false;

            this.pnlFillPattern.Enabled = SelectedItem.MaterialsType == MaterialsType.Tiles;
        }

        private void doAreaFinishBaseList_ItemSelected(int position)
        {
#if UsingNewMethod
            AreaFinishBase nextAreaFinishBase = AreaFinishBaseList[position];

            if (currAreaFinishBase != nextAreaFinishBase)
            {
                /* DialogResult dr = */

                QueryRemoveSeams(currAreaFinishBase);

                /*
                if (dr == DialogResult.Cancel)
                {
                    int prevPosition = AreaFinishBaseList.GetIndex(currAreaFinishBase);

                    SelectedItemIndex = prevPosition;

                    doSelect(prevPosition);

                    return;
                } */

                currAreaFinishBase = nextAreaFinishBase;
            }
#endif
            this.nudPatternLineWidth.Value = (Decimal)this.AreaFinishBaseList.SelectedItem.FillPatternLineWidthInPts;
            this.nudPatternLineDensity.Value = (Decimal)this.AreaFinishBaseList.SelectedItem.FillPatternInterLineIndex;

            Select(position);
        }

        bool ignoreUpdates = false;

        private void Select(int position)
        {
            ignoreUpdates = true;

            doSelect(position);

            ignoreUpdates = false;
        }

        private void EnableTilesUI()
        {
            this.pnlRolls.Enabled = false;
            this.pnlTiles.Enabled = true;
           // this.grpAreaDisplay.Enabled = true;
        }

        private void EnableRollsUI()
        {
            this.pnlRolls.Enabled = true;
            this.pnlTiles.Enabled = false;
           // this.grpAreaDisplay.Enabled = false;
        }

        private void doSelect(int position)
        {

            if (position < 0)
            {
                position = 0;
            }

            else if (position >= AreaFinishBaseList.Count)
            {
                position = AreaFinishBaseList.Count - 1;
            }


            for (int i = 0; i < Count; i++)
            {
                if (i != SelectedItemIndex)
                {
                    this[i].DeselectElem();
                }
            }

            UCAreaFinishEditFormElement ucAreaFinish = this[SelectedItemIndex];

            ucFlowLayoutPanel.ScrollControlIntoView(ucAreaFinish);

            Color selectedColor = ucAreaFinish.AreaFinishBase.Color;

            ucCustomColorPalette.SetSelectedButtonFormat(selectedColor);

            updateColorDisplay(selectedColor);

            ucAreaFinish.SelectElem();

            if (!(ucAreaFinish.AreaFinishBase.SeamFinishBase is null))
            {
                selectSeam(ucAreaFinish.AreaFinishBase.SeamFinishBase);
            }


            this.txbTag_GotFocus(null, null);

            if (ucAreaFinish.MaterialsType == MaterialsType.Tiles)
            {
                if (this.ckbTiles.Checked == false)
                {
                    this.ckbTiles.Checked = true;
                }

                this.lblSeamLabel.Text = string.Empty;
                EnableTilesUI();
            }

            else
            {
                if (this.ckbRolls.Checked == false)
                {
                    this.ckbRolls.Checked = true;
                }

                EnableRollsUI();

                //ucAreaFinish.AreaFinishBase.AreaDisplayUnits = AreaDisplayUnits.SquareYards;   // rols are always sq yards

                if (!(ucAreaFinish.AreaFinishBase.SeamFinishBase is null))
                {
                    this.lblSeamLabel.Text = "Seam: " + ucAreaFinish.AreaFinishBase.SeamFinishBase.SeamName;
                }

                else
                {
                    this.lblSeamLabel.Text = "Seam: None";
                }
            }

            this.txbAreaFinishTag.Text = ucAreaFinish.AreaName;

            this.txbProduct.Text = ucAreaFinish.Product;

            this.txbNotes.Text = ucAreaFinish.Notes;

            this.cmbTileTrim.Text = this.ucAreaFinishPalette.SelectedFinish.AreaFinishBase.TrimInInches.ToString() + '"';

            if (ucAreaFinish.TileWidthInInches != 0.0)
            {
                this.txbTileWidthFeet.Text = ucAreaFinish.TileWidthFeet.ToString();// + "'";
                this.txbTileWidthInch.Text = ucAreaFinish.TileWidthInch.ToString("0.0");// + "\"";
            }

            else
            {
                this.txbTileWidthFeet.Text = string.Empty;
                this.txbTileWidthInch.Text = string.Empty;
            }

            if (ucAreaFinish.TileHeightInInches != 0.0)
            {
                this.txbTileHeightFeet.Text = ucAreaFinish.TileHeightFeet.ToString();// + "'";
                this.txbTileHeightInch.Text = ucAreaFinish.TileHeightInch.ToString("0.0");// + "\"";
            }

            else
            {
                this.txbTileHeightFeet.Text = string.Empty;
                this.txbTileHeightInch.Text = string.Empty;
            }

            if (ucAreaFinish.RollWidthInInches != 0.0)
            {
                this.txbRollWidthFeet.Text = ucAreaFinish.RollWidthFeet.ToString()+ "'";
                this.txbRollWidthInch.Text = ucAreaFinish.RollWidthInch.ToString("0.0") + "\"";
            }

            else
            {
                this.txbRollWidthFeet.Text = string.Empty;
                this.txbRollWidthInch.Text = string.Empty;
            }

            if (ucAreaFinish.RollRepeat1InInches != 0.0)
            {
                this.txbRollRepeatWidthFeet.Text = ucAreaFinish.RollRepeat1Feet.ToString();// + "'";
                this.txbRollRepeatWidthInch.Text = ucAreaFinish.RollRepeat1Inch.ToString("0.0");// + "\"";
            }

            else
            {
                this.txbRollRepeatWidthFeet.Text = string.Empty;
                this.txbRollRepeatWidthInch.Text = string.Empty;
            }

            if (ucAreaFinish.RollRepeat2InInches != 0.0)
            {
                this.txbRollRepeatLengthFeet.Text = ucAreaFinish.RollRepeat2Feet.ToString();// + "'";
                this.txbRollRepeatLengthInch.Text = ucAreaFinish.RollRepeat2Inch.ToString("0.0");// + "\"";
            }

            else
            {
                this.txbRollRepeatLengthFeet.Text = string.Empty;
                this.txbRollRepeatLengthInch.Text = string.Empty;
            }

            this.cmbRollOverlap.Text = ucAreaFinish.AreaFinishBase.OverlapInInches.ToString();

            this.cmbExtraInchesPerCut.Text = ucAreaFinish.AreaFinishBase.ExtraInchesPerCut.ToString();

            this.lblNet.Text = (ucAreaFinish.NetAreaInSqrInches / 144.0).ToString("#,##0.0");

            if (ucAreaFinish.GrossAreaInSqrInches.HasValue)
            {
                this.lblGross.Text = (ucAreaFinish.GrossAreaInSqrInches.Value / 144.0).ToString("#,##0.0");
            }

            else
            {
                this.lblGross.Text = "N/A";
            }

            if (ucAreaFinish.Waste < 0)
            {
                this.lblWaste.ForeColor = Color.Red;
            }

            else
            {
                this.lblWaste.ForeColor = Color.Black;
            }

            if (ucAreaFinish.Waste.HasValue)
            {
                this.lblWaste.Text = (ucAreaFinish.Waste.Value / 144.0).ToString("#,##0.0");
            }
            else
            {
                this.lblWaste.Text = "N/A";
            }
            

            // Do not want to go through the cycle of updating base finish here since we are selecting only.

            ignoreAreaDisplayFormatCheckedChange = true;

            if (ucAreaFinish.AreaFinishBase.AreaDisplayUnits == AreaDisplayUnits.SquareFeet)
            {
                this.rbnSquareFeet.Checked = true;
            }

            else
            {
                this.rbnSquareYards.Checked = true;
            }

            ignoreAreaDisplayFormatCheckedChange = false;

        }

        #endregion

        #endregion

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

            this.lblCurrentColorR.ForeColor = Color.FromArgb(255, r, 0, 0);
            this.lblCurrentColorG.ForeColor = Color.FromArgb(255, 0, g, 0);
            this.lblCurrentColorB.ForeColor = Color.FromArgb(255, 0, 0, b);
            this.lblCurrentColorA.BackColor = Color.FromArgb(a, 128, 128, 128);
        }

        private bool areaFinishListContainsName(string areaNameParm)
        {
            foreach (UCAreaFinishEditFormElement ucAreaListElement in this)
            {
                if (ucAreaListElement.AreaName == areaNameParm)
                {
                    return true;
                }
            }

            return false;
        }

        private string getUniqueAreaName()
        {
            HashSet<string> existingAreaNames = new HashSet<string>();

            //foreach (UCAreaListElement ucAreaElement in this.areaListElementList)
            foreach (AreaFinishBase finishAreaBase in AreaFinishBaseList)
            {
                string areaName = finishAreaBase.AreaName;

                if (areaName.StartsWith("Finish-"))
                {
                    existingAreaNames.Add(areaName);
                }
            }

            for (int i = 1; i < 1000; i++)
            {
                string areaName = "Finish-" + i.ToString();

                if (!existingAreaNames.Contains(areaName))
                {
                    return areaName;
                }
            }

            return string.Empty;
        }

        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            foreach (UCAreaFinishEditFormElement element in this)
            {
                if (element.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    if (element.AreaFinishBase.SeamFinishBase is null)
                    {
                        MessageBox.Show("One or more roll type finishes do not have seam types assigned to them. Please correct this before saving the defaults.");

                        return;
                    }
                }
            }

            AreaFinishBaseList areaFinishBaseList = new AreaFinishBaseList();

            foreach (AreaFinishBase areaFinishBase in AreaFinishBaseList)
            {
                string guid = areaFinishBase.Guid;

                AreaFinishBase clonedAreaFinishBase = areaFinishBase.Clone();

                clonedAreaFinishBase.NetAreaInSqrInches = 0;
                clonedAreaFinishBase.GrossAreaInSqrInches = 0;
                clonedAreaFinishBase.PerimeterInInches = 0;
                clonedAreaFinishBase.Count = 0;
                clonedAreaFinishBase.RolloutLengthInInches = 0;

                areaFinishBaseList.AddElem(clonedAreaFinishBase);
            }

            string outpFilePath = string.Empty;

            if (SystemGlobals.paletteSource == "Baseline")
            {
                if (Program.AppConfig.ContainsKey("areabasefinishinitpath"))
                {
                    outpFilePath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["areabasefinishinitpath"]);
                }

                else
                {
                    MessageBox.Show("Unable to save baseline area finishes: the source path cannot be found.");
                    return;
                }

                areaFinishBaseList.Save(outpFilePath);

                MessageBox.Show("Baseline area finishes have been saved.");

                return;
            }

            else
            {
                if (Program.AppConfig.ContainsKey("areafinishinitpath"))
                {
                    outpFilePath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["areafinishinitpath"]);

                    areaFinishBaseList.Save(outpFilePath);

                    MessageBox.Show("The default area finishes have been updated.");

                    return;
                }

                else
                {
                    MessageBox.Show("No default output file path found.");

                    return;
                }

            }

        }

        private HashSet<string> usedAreaFinishTags = new HashSet<string>();

        private string currTag = null;

        private void txbTag_GotFocus(object sender, EventArgs e)
        {
            usedAreaFinishTags.Clear();

            for (int i = 0; i < this.ucAreaFinishPalette.Count; i++)
            {
                if (i == SelectedItemIndex)
                {
                    continue;
                }

                usedAreaFinishTags.Add(AreaFinishBaseList[i].AreaName);
            }

            currTag = this.AreaFinishBaseList[SelectedItemIndex].AreaName;

            string tag = this.txbAreaFinishTag.Text.Trim();

            if (usedAreaFinishTags.Contains(tag))
            {
                this.txbAreaFinishTag.BackColor = Color.Pink;
            }

            else
            {
                this.txbAreaFinishTag.BackColor = SystemColors.ControlLightLight;
            }
        }

        bool ignoreTxbTag_LostFocus =false;
       
        private void txbTag_LostFocus(object sender, EventArgs e)
        {
            if (ignoreTxbTag_LostFocus)
            {
                return;
            }

            string tag = this.txbAreaFinishTag.Text.Trim();

            if (tag.Contains(";"))
            {
                ignoreTxbTag_LostFocus = true;

                MessageBox.Show("Remove invalid character, ';', from tag name before proceeding.");
               
                ignoreTxbTag_LostFocus = false;
            }

            else if (usedAreaFinishTags.Contains(tag))
            {
                ManagedMessageBox.Show("The tag '" + tag + "' is already being used by another finish.");

                this.txbAreaFinishTag.Text = currTag;

            }

            else
            {

                //this.AreaFinishBaseList.UpdateAreaName(currTag, tag);
                //this[SelectedItemIndex].AreaName = tag;

            }
        }

        private void txbTag_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;
     
            string tag = this.txbAreaFinishTag.Text.Trim();

            if (tag.Contains(";"))
            {

                ignoreTxbTag_LostFocus = true;

                MessageBox.Show("Invalid character, ';' in tag name.");

                ignoreTxbTag_LostFocus = false;

                return;
            }

            if (usedAreaFinishTags.Contains(tag))
            {
                this.txbAreaFinishTag.BackColor = Color.Pink;
            }

            else
            {
                this.txbAreaFinishTag.BackColor = SystemColors.ControlLightLight;

                this[SelectedItemIndex].AreaName = tag;
            }

        }

        private void txbProduct_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            this[SelectedItemIndex].Product = this.txbProduct.Text.Trim();
            FinishGlobals.AreaFinishBaseList[SelectedItemIndex].Product = this.txbProduct.Text.Trim();
        }

        private void txbNotes_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            this[SelectedItemIndex].Notes = this.txbNotes.Text.Trim();
            FinishGlobals.AreaFinishBaseList[SelectedItemIndex].Notes = this.txbNotes.Text.Trim();
        }

        private void AreaFinishesEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txbAreaFinishTag.Text.Contains(";"))
            {

                MessageBox.Show("Remove invalid character, ';', from tag name before proceeding.");

                e.Cancel = true;

                return;
            }

            foreach (UCAreaFinishEditFormElement element in this)
            {
                if (element.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    if (element.AreaFinishBase.SeamFinishBase is null)
                    {
                        MessageBox.Show("One or more roll type finishes do not have seam types assigned to them. Please correct this before closing this form.");

                        e.Cancel = true;

                        return;
                    }
                }
            }

#if UsingNewMethod
            /* dr = */
            QueryRemoveSeams(AreaFinishBaseList.SelectedItem);

            /*
            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;

                return;
            } */
#endif
            UpdateAddCounters();

            if (modal)
            {
                return;
            }


        }

        internal void QueryRemoveSeams(AreaFinishBase areaFinishBase)
        {
            if (!areaFinishBase.HasChanged())
            {
                return /* DialogResult.None */;
            }

            List<CanvasLayoutArea> changedAndSeamedLayoutAreas = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in currentPage.LayoutAreas)
            {
                if (canvasLayoutArea.AreaFinishBase == areaFinishBase)
                {
                    if (canvasLayoutArea.IsSeamed())
                    {
                        changedAndSeamedLayoutAreas.Add(canvasLayoutArea);
                    }
                }
            }

            if (changedAndSeamedLayoutAreas.Count <= 0)
            {
                return /* DialogResult.None */ ;
            }

            string msg =
                "The specification of the area finish '"
                + areaFinishBase.AreaName
                + "' has changed in a way that impacts the current seaming of one or more layout areas.\n\n"
                + "Click 'Yes' to remove all existing seams for this finish type.\n\n"
                + "Click 'No' to leave all existing seams in place.\n\n"
                //+ "Click 'Cancel' to reset all changes made."
                ;

            DialogResult dr = MessageBox.Show(msg,
                "Area Finish Updated", MessageBoxButtons.YesNo);

            if (dr == DialogResult.No)
            {
                return /* DialogResult.No */ ;
            }

            if (dr == DialogResult.Yes)
            {

                foreach (CanvasLayoutArea canvasLayoutArea in changedAndSeamedLayoutAreas)
                {
                    canvasLayoutArea.RemoveSeamsAndRollouts();

                    canvasLayoutArea.RemoveSeamIndexTag();

                }

                return /* DialogResult.Yes */ ;
            }

            //if (dr == DialogResult.Cancel)
            //{
            //    areaFinishBase.Reset();

            //    return DialogResult.Cancel;
            //}

            return /* DialogResult.None */ ;

        }

        private void cbmTrimFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            UCAreaFinishEditFormElement ucAreaFinish = this[SelectedItemIndex];

            double tileTrimFactor = 4;

            string tileTrimFactorStr = cmbTileTrim.Text;

            Debug.Assert(tileTrimFactorStr.EndsWith("\""));

            tileTrimFactorStr = tileTrimFactorStr.Substring(0, tileTrimFactorStr.Length - 1);

            double.TryParse(tileTrimFactorStr, out tileTrimFactor);

            FinishGlobals.AreaFinishBaseList.SelectedItem.TrimInInches = tileTrimFactor;

            //this.ucAreaFinishPalette.SelectedFinish.SetTrimInInches(tileTrimFactor);

            this.lblNet.Text = (ucAreaFinish.NetAreaInSqrInches / 144.0).ToString("#,##0.0");

            if (ucAreaFinish.GrossAreaInSqrInches.HasValue)
            {
                this.lblGross.Text = (ucAreaFinish.GrossAreaInSqrInches.Value / 144.0).ToString("#,##0.0");
            }

            else
            {
                this.lblGross.Text = "N/A";
            }

            if (ucAreaFinish.Waste < 0)
            {
                this.lblWaste.ForeColor = Color.Red;
            }

            else
            {
                this.lblWaste.ForeColor = Color.Black;
            }

            if (ucAreaFinish.Waste.HasValue)
            {
                this.lblWaste.Text = (ucAreaFinish.Waste.Value / 144.0).ToString("#,##0.0");
            }
            
            else
            {
                this.lblWaste.Text = "N/A";
            }

            this.lblWaste.Select();
        }

        private void txbTileWidthFeet_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? tileWidthInInches = parseFeetAndInchText(this.txbTileWidthFeet.Text, this.txbTileWidthInch.Text);

            if (tileWidthInInches.HasValue)
            {
                FinishManagerGlobals.SelectedAreaFinishManager.TileWidthInInches = tileWidthInInches.Value;
                
                this.txbTileWidthFeet.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbTileWidthFeet.BackColor = Color.Pink;
            }
        }

        private void txbTileWidthInch_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? tileWidthInInches = parseFeetAndInchText(this.txbTileWidthFeet.Text, this.txbTileWidthInch.Text);

            if (tileWidthInInches.HasValue)
            {
                FinishManagerGlobals.SelectedAreaFinishManager.TileWidthInInches = tileWidthInInches.Value;

                this.txbTileWidthInch.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbTileWidthFeet.BackColor = Color.Pink;
            }
        }

        private void txbTileHeightFeet_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? tileHeightInInches = parseFeetAndInchText(this.txbTileHeightFeet.Text, this.txbTileHeightInch.Text);

            if (tileHeightInInches.HasValue)
            {
                FinishManagerGlobals.SelectedAreaFinishManager.TileHeightInInches = tileHeightInInches.Value;

                this.txbTileHeightFeet.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbTileHeightFeet.BackColor = Color.Pink;
            }
        }

        private void txbTileHeightInch_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? tileHeightInInches = parseFeetAndInchText(this.txbTileHeightFeet.Text, this.txbTileHeightInch.Text);

            if (tileHeightInInches.HasValue)
            {
                FinishManagerGlobals.SelectedAreaFinishManager.TileHeightInInches = tileHeightInInches.Value;

                this.txbTileHeightInch.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbTileHeightInch.BackColor = Color.Pink;
            }
        }

        private void txbRollWidthFeet_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? rollWidthInInches = parseFeetAndInchText(this.txbRollWidthFeet.Text, this.txbRollWidthInch.Text);

            if (rollWidthInInches.HasValue)
            {
                FinishManagerGlobals.SelectedAreaFinishManager.RollWidthInInches = rollWidthInInches.Value; 

                this.txbRollWidthFeet.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbRollWidthFeet.BackColor = Color.Pink;
            }
        }

        private void txbRollWidthInch_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

           double? rollWidthInInches = parseFeetAndInchText(this.txbRollWidthFeet.Text, this.txbRollWidthInch.Text);

            if (rollWidthInInches.HasValue)
            {
                FinishManagerGlobals.SelectedAreaFinishManager.RollWidthInInches = rollWidthInInches.Value;

                this.txbRollWidthInch.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbRollWidthInch.BackColor = Color.Pink;
            }
        }

        private void txbRollRepeatWidthFeet_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? rollRepeat1InInches = parseFeetAndInchText(this.txbRollRepeatWidthFeet.Text, this.txbRollRepeatWidthInch.Text);

            if (rollRepeat1InInches.HasValue)
            {
                this.ucAreaFinishPalette.SelectedFinish.AreaFinishBase.RollRepeatWidthInInches = rollRepeat1InInches.Value;

                this.txbRollRepeatWidthFeet.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbRollRepeatWidthFeet.BackColor = Color.Pink;
            }
        }

        private void txbRollRepeatWidthInch_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? rollRepeat1InInches = parseFeetAndInchText(this.txbRollRepeatWidthFeet.Text, this.txbRollRepeatWidthInch.Text);

            if (rollRepeat1InInches.HasValue)
            {
                this.ucAreaFinishPalette.SelectedFinish.AreaFinishBase.RollRepeatWidthInInches = rollRepeat1InInches.Value;

                this.txbRollRepeatWidthInch.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbRollRepeatWidthInch.BackColor = Color.Pink;
            }
        }

        private void txbRollRepeatLengthFeet_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? rollRepeat2InInches = parseFeetAndInchText(this.txbRollRepeatLengthFeet.Text, this.txbRollRepeatLengthInch.Text);

            if (rollRepeat2InInches.HasValue)
            {
                this.ucAreaFinishPalette.SelectedFinish.AreaFinishBase.RollRepeatLengthInInches = rollRepeat2InInches.Value;

                this.txbRollRepeatLengthFeet.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbRollRepeatLengthFeet.BackColor = Color.Pink;
            }
        }

        private void txbRollRepeatLengthInch_TextChanged(object sender, EventArgs e)
        {
            if (ignoreUpdates)
            {
                return;
            }

            ElementsChanged = true;

            double? rollRepeat2InInches = parseFeetAndInchText(this.txbRollRepeatLengthFeet.Text, this.txbRollRepeatLengthInch.Text);

            if (rollRepeat2InInches.HasValue)
            {
                this.ucAreaFinishPalette.SelectedFinish.AreaFinishBase.RollRepeatLengthInInches = rollRepeat2InInches.Value;

                this.txbRollRepeatLengthInch.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                this.txbRollRepeatLengthInch.BackColor = Color.Pink;
            }
        }

        private double? parseFeetAndInchText(string strFeet, string strInch)
        {
            int feet = 0;
            double inch = 0.0;

            if (!string.IsNullOrWhiteSpace(strFeet))
            {
                strFeet = strFeet.Trim();

                if (strFeet.EndsWith("'"))
                {
                    strFeet = strFeet.Substring(0, strFeet.Length - 1);
                }

                if (!Utilities.IsAllDigits(strFeet))
                {
                    return null;
                }

                if (!int.TryParse(strFeet, out feet))
                {
                    return null;
                }
            }

            if (!string.IsNullOrWhiteSpace(strInch))
            {
                strInch = strInch.Trim();

                if (strInch.EndsWith("\""))
                {
                    strInch = strInch.Substring(0, strInch.Length - 1);
                }

                if (!double.TryParse(strInch, out inch))
                {
                    return null;
                }
            }

            return 12.0 * (double)feet + inch;

        }

        public IEnumerator<UCAreaFinishEditFormElement> GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        UCAreaFinishEditFormElement this[int index]
        {
            get
            {
                return ucFlowLayoutPanel[index];
            }
        }

        public int Count => this.ucFlowLayoutPanel.Count;

        private void Add(UCAreaFinishEditFormElement ucFinish) => this.ucFlowLayoutPanel.Add(ucFinish);

        private void RemoveAt(int selectedElement) => this.ucFlowLayoutPanel.RemoveAt(selectedElement);

        private int GetChildIndex(UCAreaFinishEditFormElement ucFinish) => this.ucFlowLayoutPanel.GetChildIndex(ucFinish);

        private void SetChildIndex(UCAreaFinishEditFormElement ucFinish, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucFinish, position);

        public UCAreaFinishEditFormElement SelectedItem => this[this.SelectedItemIndex];

        private bool elementsChanged = false;

        public bool ElementsChanged
        {
            get
            {
                return elementsChanged;
            }

            set
            {
                elementsChanged = value;
            }
        }

        private void ckbRolls_CheckedChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            if (ckbRolls.Checked)
            {
                SelectedItem.AreaFinishBase.Pattern = 0;

                this.pnlFillPattern.Enabled = false;

                resetTilesArea();

                EnableRollsUI();

                if (this.SelectedItem.MaterialsType == MaterialsType.Rolls)
                {
                    txbRollWidthFeet.Text = this.SelectedItem.RollWidthFeet.ToString() + "\"";
                    txbRollWidthInch.Text = this.SelectedItem.RollWidthInch.ToString() + "'";
                    cmbRollOverlap.Text = this.SelectedItem.OverlapInInches.ToString();
                    cmbExtraInchesPerCut.Text = this.SelectedItem.AreaFinishBase.ExtraInchesPerCut.ToString();
                }

                else
                {
                    this.SelectedItem.MaterialsType = MaterialsType.Rolls;

                    txbRollWidthFeet.Text = GlobalSettings.RollWidthDefaultValueFeet.ToString() + "'";
                    txbRollWidthInch.Text = GlobalSettings.RollWidthDefaultValueInches.ToString() + "\"";
                    cmbRollOverlap.Text = GlobalSettings.RollOverlapDefaultValueInches.ToString();
                    cmbExtraInchesPerCut.Text = GlobalSettings.RollExtraPerCutDefaultValueInches.ToString();

                }
                

                //this.pnlColorPalette.SendToBack();
                //this.pnlColorPalette.Visible = false;

                //this.pnlSeams.BringToFront();
                //this.pnlSeams.Visible = true;

                //this.rbnSquareYards.Checked = true;

                if (ckbTiles.Checked)
                {
                    ckbTiles.Checked = false;
                }

                baseForm.seamPalette.UpdateSeamList();

               // SelectedItem.AreaFinishBase;
            }

            else
            {
                if (!ckbTiles.Checked)
                {
                    ckbTiles.Checked = true;
                }
            }

           
        }

        private void ckbTiles_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTiles.Checked)
            {
                this.pnlFillPattern.Enabled = true;

                if (SeamTypeIsBeingUsed(SelectedItem))
                {
                    DialogResult dr = ManagedMessageBox.Show(
                        SelectedItem.AreaFinishBase.AreaName + " has existing seams and/or cuts. "
                        + "Changing from rolls to tiles will delete any seams and cuts and their associated quantities. "
                        + "The shapes for this finish type will remain as colored (without seams) and will be figured with their net square footage (plus trim if any). "
                        + "Do you still want to continue?",
                        "Warning: Seams Being Used",
                        MessageBoxButtons.YesNo);

                    if (dr == DialogResult.No)
                    {
                        ckbRolls.Checked = true;

                        return;
                    }

                    ElementsChanged = true;

                    RemoveAllSeamsAndCuts(SelectedItem);
                }

                DeselectSeamSelectedAreas(SelectedItem);

                resetRollsArea();
                EnableTilesUI();

                this.SelectedItem.MaterialsType = MaterialsType.Tiles;
                this.SelectedItem.UpdateSeam();

                UCAreaFinishPaletteElement finishPaletteElement = PalettesGlobal.AreaFinishPalette[SelectedItemIndex];
                AreaFinishManager selectedAreaFinishManger = FinishManagerGlobals.SelectedAreaFinishManager;

                selectedAreaFinishManger.FinishSeamBase = null;
                selectedAreaFinishManger.MaterialsType = MaterialsType.Tiles;
                finishPaletteElement.UpdateSeam();

                if (ckbRolls.Checked)
                {
                    ckbRolls.Checked = false;
                }

                this.lblSeamLabel.Text = string.Empty;

                baseForm.seamPalette.UpdateSeamList();
            }

            else
            {
                if (!ckbRolls.Checked)
                {
                    ckbRolls.Checked = true;
                }
            }

            ElementsChanged = true;
        }


        private bool SeamTypeIsBeingUsed(UCAreaFinishEditFormElement selectedItem)
        {
            foreach (CanvasLayoutArea canvasLayoutArea in baseForm.CanvasManager.CurrentPage.LayoutAreas)
            {
                if (canvasLayoutArea.AreaFinishBase != selectedItem.AreaFinishBase)
                {
                    continue;
                }

                if (canvasLayoutArea.CanvasSeamList != null)
                {
                    if (canvasLayoutArea.CanvasSeamList.Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void RemoveAllSeamsAndCuts(UCAreaFinishEditFormElement selectedItem)
        {
            foreach (CanvasLayoutArea canvasLayoutArea in baseForm.CanvasManager.CurrentPage.LayoutAreas)
            {
                if (canvasLayoutArea.AreaFinishBase != selectedItem.AreaFinishBase)
                {
                    continue;
                }

                // Note sure why checking for null here.

                if (canvasLayoutArea.CanvasSeamList != null)
                {
                    foreach (CanvasSeam canvasSeam in canvasLayoutArea.CanvasSeamList)
                    {
                        canvasSeam.Delete();
                    }


                    foreach (CanvasSeam canvasSeam in canvasLayoutArea.DisplayCanvasSeamList)
                    {
                        currentPage.DisplaySeamDict.Remove(canvasSeam.Guid);

                        canvasSeam.Delete();
                    }

                    foreach (MaterialsLayout.GraphicsCut graphicsCut in canvasLayoutArea.GraphicsCutList)
                    {
                        graphicsCut.Delete();
                    }
                }

                canvasLayoutArea.CanvasSeamList.Clear();
                canvasLayoutArea.GraphicsSeamList.Clear();
                canvasLayoutArea.SeamList.Clear();


                canvasLayoutArea.DisplayCanvasSeamList.Clear();
                canvasLayoutArea.GraphicsDisplaySeamList.Clear();
                canvasLayoutArea.DisplaySeamList.Clear();

                canvasLayoutArea.GraphicsCutList.Clear();
                canvasLayoutArea.GraphicsOverageList.Clear();
                canvasLayoutArea.GraphicsUndrageList.Clear();
            }
        }

        private void DeselectSeamSelectedAreas(UCAreaFinishEditFormElement selectedItem)
        {
            foreach (CanvasLayoutArea canvasLayoutArea in selectedItem.AreaFinishManager.CanvasLayoutAreas)
            {
                if (canvasLayoutArea.SeamDesignStateSelectionModeSelected)
                {
                    baseForm.CanvasManager.CurrentPage.UpdateSeamDesignStateAreaSelectionStatus(canvasLayoutArea, canvasLayoutArea.AreaFinishManager.Guid);
                }
            }

            baseForm.CanvasManager.UpdateAreaSeamsUndrsOversDataDisplay();
        }

        private void resetRollsArea()
        {

            ElementsChanged = true;

            this.txbRollWidthFeet.Text = GlobalSettings.RollWidthDefaultValueFeet.ToString() + "'";
            this.txbRollWidthInch.Text = GlobalSettings.RollWidthDefaultValueInches.ToString() + "\"";

            this.txbRollRepeatWidthFeet.Text = string.Empty;
            this.txbRollRepeatWidthInch.Text = string.Empty;

            this.txbRollRepeatLengthFeet.Text = string.Empty;
            this.txbRollRepeatLengthInch.Text = string.Empty;

            this.cmbRollOverlap.Text = string.Empty;

            this.cmbExtraInchesPerCut.Text = string.Empty;

        }

        private void resetTilesArea()
        {
            ElementsChanged = true;
            //this.cmbRollOverlap.Text = "3";
            //this.cmbExtraInchesPerCut.Text = "3";

            this.txbTileHeightFeet.Text = string.Empty;
            this.txbTileHeightInch.Text = string.Empty;

            this.txbTileWidthFeet.Text = string.Empty;
            this.txbTileWidthInch.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string error = validateAreaFinishSpecCompleted();

            if (!string.IsNullOrEmpty(error))
            {
                DialogResult dr = ManagedMessageBox.Show(
                    "There are roll material definition errors:\r\n\r\n" +
                    error +
                    "\r\n\r\nMaterial definitions have not been updated.", "Roll Definition Errors", MessageBoxButtons.OK);


                return;
            }
        }

        private void btnAreaFinishEditorHide_Click(object sender, EventArgs e)
        {
            foreach (UCAreaFinishEditFormElement element in this)
            {
                if (element.AreaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    if (element.AreaFinishBase.SeamFinishBase is null)
                    {
                        MessageBox.Show("One or more roll type finishes do not have seam types assigned to them. Please correct this before hiding this form.");


                        return;
                    }
                }
            }

            
            if (!modal)
            {
                this.WindowState = FormWindowState.Minimized;

                foreach (UCAreaFinishEditFormElement ucFlowLayoutPanel in this)
                {
                    ucFlowLayoutPanel.UseFullOpacity = false;
                    ucFlowLayoutPanel.UpdateColor();
                }
            }

            else
            {
                this.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            currAreaFinishBase.Reset();

            doSelect(SelectedItemIndex);
        }

        private void UpdateAddCounters()
        {
            foreach (var counterElement in this.ucFlowLayoutPanel)
            {
                AreaFinishBase finishBase = counterElement.AreaFinishBase;

                if ((counterElement.OriginalSizeField != finishBase.RollRepeatLengthInInches)
                    || (counterElement.OriginalAreaName != finishBase.AreaName))
                {
                    baseForm.CanvasManager.CounterController.SelectUpdateCounterSize(counterElement.OriginalAreaName, finishBase.AreaName, counterElement.OriginalSizeField, finishBase.RollRepeatLengthInInches);
                }
            }
        }

        private string validateAreaFinishSpecCompleted()
        {
            if (!this.ckbRolls.Checked)
            {
                return string.Empty;
            }

            string rollWidthFeet = this.txbRollWidthFeet.Text.Trim();
            string rollWidthInch = this.txbRollWidthInch.Text.Trim();

            List<string> errorList = new List<string>();

            if (string.IsNullOrEmpty(rollWidthFeet) && string.IsNullOrEmpty(rollWidthInch))
            {
                errorList.Add("A valid roll width must be specified for a rolled product.");
            }

            if (!parseFeetAndInchText(rollWidthFeet, rollWidthInch).HasValue)
            {
                errorList.Add("A valid roll width must be specified for a rolled product.");
            }

            if (AreaFinishBaseList.SelectedItem.SeamFinishBase is null)
            {
                errorList.Add("A seam type must be specified for a rolled product.");
            }

            if (errorList.Count <= 0)
            {
                return string.Empty;
            }

            else
            {
                return string.Join("\r\n", errorList);
            }
        }

        #region Closing and Disposing
        private void AreaFinishesEditForm_Disposed(object sender, EventArgs e)
        {
            RemoveFromCursorManagementList();

            AreaFinishBaseList.ItemAdded -= AreaFinishBaseList_ItemAdded;
            AreaFinishBaseList.ItemInserted -= AreaFinishBaseList_ItemInserted;
            AreaFinishBaseList.ItemRemoved -= AreaFinishBaseList_ItemRemoved;
            AreaFinishBaseList.ItemsSwapped -= AreaFinishBaseList_ItemsSwapped;
            AreaFinishBaseList.ItemSelected -= AreaFinishBaseList_ItemSelected;
#if false
            Tuple<List<AreaFinishBase>, List<CanvasLayoutArea>> changedAreaFinishBaseAndSeamedLayoutAreas = generateChangedAreaFinishes();

            List<AreaFinishBase> changedAreaFinishBase = changedAreaFinishBaseAndSeamedLayoutAreas.Item1;
            List<CanvasLayoutArea> seamedCanvasLayoutArea = changedAreaFinishBaseAndSeamedLayoutAreas.Item2;

            if (changedAreaFinishBase.Count > 0)
            {
                AreaFinishesChangedConfirmForm areaFinishesChangedConfirmForm =
                    new AreaFinishesChangedConfirmForm(changedAreaFinishBase);

                DialogResult dr = areaFinishesChangedConfirmForm.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    foreach (CanvasLayoutArea canvasLayoutArea in seamedCanvasLayoutArea)
                    {
                        canvasLayoutArea.RemoveSeamsAndRollouts();
                    }
                }
            }
#endif
        }

        #endregion

        #region Cursor Management
        protected override void WndProc(ref Message m)
        {
            CursorManager.WndProc(this);

            base.WndProc(ref m);
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

        private bool ignoreAreaDisplayFormatCheckedChange = false;

        private void rbnSquareFeet_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreAreaDisplayFormatCheckedChange)
            {
                return;
            }

            if (rbnSquareFeet.Checked)
            {
                SelectedItem.AreaFinishBase.AreaDisplayUnits = AreaDisplayUnits.SquareFeet;
            }

        }

        private void rbnSquareYards_CheckedChanged(object sender, EventArgs e)
        {
            if (ignoreAreaDisplayFormatCheckedChange)
            {
                return;
            }

            if (rbnSquareYards.Checked)
            {
                SelectedItem.AreaFinishBase.AreaDisplayUnits = AreaDisplayUnits.SquareYards;
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void nudPatternLineWidth_ValueChanged(object sender, EventArgs e)
        {
            SelectedItem.AreaFinishBase.FillPatternLineWidthInPts = (double) nudPatternLineWidth.Value;
        }

        private void nudPatternLineDensity_ValueChanged(object sender, EventArgs e)
        {
            double density = 0.25 + ((double)nudPatternLineDensity.Value - 5.0) * 0.025;


            SelectedItem.AreaFinishBase.FillPatternInterlineDistanceInFt = density;

            SelectedItem.AreaFinishBase.FillPatternInterLineIndex = (int)nudPatternLineDensity.Value;
        }

    }

}
