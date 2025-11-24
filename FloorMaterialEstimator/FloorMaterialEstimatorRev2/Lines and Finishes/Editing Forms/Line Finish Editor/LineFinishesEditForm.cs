
namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using FinishesLib;
    using PaletteLib;
    using Utilities;
    using Utilities.Supporting_Controls;
    using System.Collections;
    using System.Diagnostics;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;
    using Globals;
    using System.Xml.Linq;
    using System.Text.RegularExpressions;

    public partial class LineFinishesEditForm : Form, IEnumerable<UCLineFinishEditFormElement>, ICursorManagementForm
    {
        private FloorMaterialEstimatorBaseForm baseForm;

        public LineFinishBaseList LineFinishBaseList;

        public LineFinishBase ZeroLineBase { get; set; }

        private bool zeroLineSelected = false;
       
        UCLineFinishEditFormElement ucZeroLineEditFormElement;

        private UCLineFinishPalette ucLineFinishPalette;

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

        public bool EditingWallHeight { get; set; } = false;

        public int SelectedItemIndex
        {
            get
            {
                if (zeroLineSelected)
                {
                    return -1;
                }
                return LineFinishBaseList.SelectedItemIndex;
            }
        }

        public UCFlowLayoutPanel<UCLineFinishEditFormElement> ucFlowLayoutPanel;

        public LineFinishesEditForm(
            FloorMaterialEstimatorBaseForm baseForm
            , UCLineFinishPalette ucLinePalette
            , LineFinishBaseList lineFinishBaseList
            , LineFinishBase zeroLineBase
            , bool modal)
        {
            InitializeComponent();

            this.ucCustomColorPalette.Init(palletColors);
            this.ucLineWidth.Init();

            this.baseForm = baseForm;

            this.ucLineFinishPalette = ucLinePalette;

            this.LineFinishBaseList = lineFinishBaseList;

            this.ZeroLineBase = zeroLineBase;

            this.modal = modal;

            ucFlowLayoutPanel = new UCFlowLayoutPanel<UCLineFinishEditFormElement>();

            ucFlowLayoutPanel.AutoScroll = true;
            ucFlowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            ucFlowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            ucFlowLayoutPanel.WrapContents = false;


            this.Controls.Add(ucFlowLayoutPanel);

            this.ucFlowLayoutPanel.Scroll += UcFlowLayoutPanel_Scroll;
            ucFlowLayoutPanel.Location = new Point(22, 36);
            ucFlowLayoutPanel.Size = new Size(200, 450);

            ucZeroLineEditFormElement = new UCLineFinishEditFormElement(zeroLineBase, -1);

            ucZeroLineEditFormElement.Size = new Size(ucFlowLayoutPanel.Width, ucZeroLineEditFormElement.Height);
            this.Controls.Add(ucZeroLineEditFormElement);

            ucZeroLineEditFormElement.Click += UcZeroLineEditFormElement_Click;

            ucZeroLineEditFormElement.Location = new Point(ucFlowLayoutPanel.Location.X, ucFlowLayoutPanel.Location.Y + ucFlowLayoutPanel.Height + 16);

            ucZeroLineEditFormElement.BorderStyle = BorderStyle.FixedSingle;

            int position = 0;

            zeroLineSelected = false;
            
            foreach (LineFinishBase lineFinishBase in LineFinishBaseList)
            {
                Add(lineFinishBase, position++);
            }

            //if (!modal)
            //{
            //    this.MouseEnter += LineFinishesEditForm_MouseEnter;
            //    this.MouseLeave += LineFinishesEditForm_MouseLeave;
            //}

            this.ucCustomColorPalette.ColorSelected += UcCustomColorPalette_ColorSelected;
            this.ucCustomDashType.DashTypeSelected += UcCustomDashType_DashTypeSelected;
            this.ucLineWidth.LineWidthSelected += UcLineWidth_LineWidthSelected;

            LineFinishBaseList.ItemAdded += LineFinishBaseList_ItemAdded;
            LineFinishBaseList.ItemInserted += LineFinishBaseList_ItemInserted;
            LineFinishBaseList.ItemRemoved += LineFinishBaseList_ItemRemoved;
            LineFinishBaseList.ItemsSwapped += LineFinishBaseList_ItemsSwapped;
            LineFinishBaseList.ItemSelected += LineFinishBaseList_ItemSelected;

            this.txbTag.TextChanged += this.txbTag_TextChanged;
            this.txbTag.GotFocus += txbTag_GotFocus;
            this.txbTag.LostFocus += txbTag_LostFocus;

            //this.KeyPreview = true;

            this.KeyPress += LineFinishesEditForm_KeyPress;

            ElementsChanged = false;

            if (modal)
            {
                this.btnLineFinishEditorHide.Text = "Close";
            }

            else
            {
                this.btnLineFinishEditorHide.Text = "Hide";
            }

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.txbWallHeight.GotFocus += TxbWallHeight_GotFocus;
            this.txbWallHeight.LostFocus += TxbWallHeight_LostFocus;

            Select(LineFinishBaseList.SelectedItemIndex);

            AddToCursorManagementList();

            this.FormClosing += LineFinishesEditForm_FormClosing;

            this.Disposed += LineFinishesEditForm_Disposed;
          
        }

        private void UcFlowLayoutPanel_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine(ucFlowLayoutPanel.VerticalScroll.Value.ToString());
        }

        private void TxbWallHeight_LostFocus(object sender, EventArgs e)
        {
            EditingWallHeight = false;
        }

        private void TxbWallHeight_GotFocus(object sender, EventArgs e)
        {
            EditingWallHeight = true;
        }

        private void UcZeroLineEditFormElement_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Count; i++)
            {
                this[i].Deselect();
            }

            zeroLineSelected = true;

            ucCustomColorPalette.SetSelectedButtonFormat(ZeroLineBase.LineColor);

            ucCustomDashType.SetSelectedDashElementFormat(ZeroLineBase.VisioLineType);

            ucLineWidth.SetSelectedLineWidth(ZeroLineBase.LineWidthInPts);

            this.ucZeroLineEditFormElement.Select();

            this.txbTag.Text = "Zero Line";

            this.txbProduct.Text = ZeroLineBase.Product;

            this.txbNotes.Text = ZeroLineBase.Notes;

            this.txbTag.Enabled = false;

            ignoreWallHeightChange = true;

            this.txbWallHeight.Text = string.Empty;

            ignoreWallHeightChange = false;

            this.txbWallHeight.Enabled = false;

            this.lblWallHeight.Enabled =false;
        }

        //private void LineFinishesEditForm_MouseEnter(object sender, EventArgs e)
        //{
        //    baseForm.Cursor = Cursors.Arrow;
        //}

        //private void LineFinishesEditForm_MouseLeave(object sender, EventArgs e)
        //{
        //    //baseForm.SetCursorForCurrentLocation();
        //}

        private void LineFinishesEditForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ElementsChanged = true;

            if (e.KeyChar == 68 ||  e.KeyChar == 100 || e.KeyChar == 4)
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
                baseForm.CanvasManager.ProcessAreaModeFinishNumericShortCut(KeyAscii);
                return;

            }

            else if (baseForm.tbcPageAreaLine.SelectedIndex == FloorMaterialEstimatorBaseForm.tbcLineModeIndex)
            {
                // The following prevents the switching to another line if any of the input text boxes are selected.

                if (this.txbTag.Focused || this.txbProduct.Focused || this.txbNotes.Focused)
                {
                    return;
                }

                baseForm.CanvasManager.ProcessLineModeFinishNumericShortCut(KeyAscii);
                return;

            }
        }

        private void UcCustomColorPalette_ColorSelected(ColorSelectedEventArgs args)
        {
            ElementsChanged = true;

            if (zeroLineSelected)
            {
                Color color = Color.FromArgb(args.A, args.R, args.G, args.B);

                this.ucZeroLineEditFormElement.LineFinishBase.LineColor = color;
                this.ucZeroLineEditFormElement.Invalidate();

                if (SystemState.DesignState != DesignState.Area)
                {
                    return;
                }

                if (baseForm.CanvasManager.BuildingPolyline == null)
                {
                    return;
                }

                foreach (var item in baseForm.CanvasManager.BuildingPolyline)
                {
                    if (!item.IsZeroLine) { continue; }

                    item.SetBaseLineColor(color);
                }

                return;
            }

            UCLineFinishPaletteElement ucLine = ucLineFinishPalette[SelectedItemIndex];

            ucLine.LineFinishBase.LineColor = Color.FromArgb(args.A, args.R, args.G, args.B);

            this[SelectedItemIndex].Invalidate();

            ucLine.Invalidate();

            updateColorDisplay(args.R, args.G, args.B, args.A);
           // ucLineFinishPalette.BaseForm.uclSelectedLine.Invalidate();
        }

        public void setLineColor(Color cl, double size)
        {
            ElementsChanged = true;

            if (zeroLineSelected)
            {
                this.ucZeroLineEditFormElement.LineFinishBase.LineColor = cl;
                this.ucZeroLineEditFormElement.LineFinishBase.LineWidthInPts = size;
                this.ucZeroLineEditFormElement.Invalidate();

                return;
            }

            UCLineFinishPaletteElement ucLine = ucLineFinishPalette[SelectedItemIndex];

            ucLine.LineFinishBase.LineColor = cl;
            ucLine.LineFinishBase.LineWidthInPts = size;
            this[SelectedItemIndex].Invalidate();

            ucLine.Invalidate();

            updateColorDisplay(this[SelectedItemIndex].ForeColor);
            //ucLineFinishPalette.BaseForm.uclSelectedLine.Invalidate();
        }
        #region Palette Changes

        #region Add Element

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string lineName = getUniqueLineName();

            LineFinishBase lineFinishBase = LineFinishBase.DefaultLineFinish.Clone();

            lineFinishBase.LineName = lineName;

            this.LineFinishBaseList.Add(lineFinishBase, this.LineFinishBaseList.Count);
        }

        private void LineFinishBaseList_ItemAdded(LineFinishBase lineFinishBase)
        {
            Add(lineFinishBase, this.LineFinishBaseList.Count - 1);
        }

        private void Add(LineFinishBase lineFinishBase, int position)
        {
            ElementsChanged = true;

            UCLineFinishEditFormElement ucLineListElement = new UCLineFinishEditFormElement(lineFinishBase, position);

            Add(ucLineListElement);


           // ucFlowLayoutPanel.ScrollControlIntoView(ucLineListElement);

            ucLineListElement.positionOnPalette = position;

            ucLineListElement.ControlClicked += UcAreaListElement_ControlClicked;
        }

        #endregion

        #region Insert Element

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

            string lineName = getUniqueLineName();

            LineFinishBase lineFinishBase = LineFinishBase.DefaultLineFinish.Clone();

            lineFinishBase.LineName = lineName;

            this.LineFinishBaseList.Insert(lineFinishBase, SelectedItemIndex, SelectedItemIndex);
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (SelectedItemIndex < 0 || SelectedItemIndex >= ucLineFinishPalette.Count)
            {
                MessageBox.Show("A line finish must be selected to duplicate.");

                return;
            }

            UCLineFinishPaletteElement ucLine = ucLineFinishPalette[SelectedItemIndex];

            LineFinishBase lineFinishBase = ucLine.LineFinishBase;

            string duplicateLineName = getUniqueAreaNameFromBase(lineFinishBase);

            if (string.IsNullOrEmpty(duplicateLineName))
            {
                MessageBox.Show("Cannot create duplicate line: unable to create unique line name.");
                return;
            }

            LineFinishBase duplicateLineFinishBase = lineFinishBase.Clone();

            duplicateLineFinishBase.LineName = duplicateLineName;

            if (KeyboardUtils.CntlKeyPressed)
            {
                Color color = lineFinishBase.LineColor;

                double testPct = getUpdatePct(color);

                double updatePct = 0.20;

                if (testPct < 0.1)
                { 
                    updatePct = testPct;
                }

                Color duplicateColor = ColorUtils.Lighten(color, (float)updatePct);

                duplicateLineFinishBase.LineColor = duplicateColor;

                if (duplicateLineFinishBase.VisioLineType == 1)
                {
                    duplicateLineFinishBase.VisioLineType = 2;
                }

                else if (duplicateLineFinishBase.VisioLineType == 2)
                {
                    duplicateLineFinishBase.VisioLineType = 1;
                }
            }

            this.LineFinishBaseList.Insert(duplicateLineFinishBase, SelectedItemIndex + 1, SelectedItemIndex + 1);
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

            return pct * 0.25;
        }

        private string getUniqueAreaNameFromBase(LineFinishBase lineFinishBase)
        {
            Regex regex = new Regex("^((?'base'.+?)(?'extension' [a-z])?)$");

            string lineFinishBaseName = lineFinishBase.LineName.Trim();

            if (string.IsNullOrEmpty(lineFinishBaseName))
            {
                return String.Empty;
            }

            Match match = regex.Match(lineFinishBaseName);

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

            foreach (UCLineFinishEditFormElement ucLineElement in this)
            {
                string compareLineName = ucLineElement.LineName.Trim();

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

            char newExtension = (char) 0;

            for (int i = 0; i < 26; i++)
            {
                if (!existingExtensions.Contains((char) ('a' + i)))
                {
                    newExtension = (char) ('a' + i);

                    break;
                }
            }

            if (newExtension > (char) 0)
            {
                return baseName + ' ' + newExtension;
            }

            return string.Empty;
            

        }

        private void LineFinishBaseList_ItemInserted(LineFinishBase lineFinishBase, int position)
        {
            Insert(lineFinishBase, position);
        }

        internal void Insert(LineFinishBase lineFinishBase, int position)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

            ElementsChanged = true;

            if (Count <= 0)
            {
                Debug.Assert(position == 0);

                Add(lineFinishBase, 0);

                return;
            }

            Debug.Assert(!lineFinishListContainsName(lineFinishBase.LineName));
            Debug.Assert(position >= 0 && position < Count);

            if (lineFinishListContainsName(lineFinishBase.LineName))
            {
                return;
            }

            if (position < 0 || position >= Count)
            {
                return;
            }

            UCLineFinishEditFormElement ucLineListElement = new UCLineFinishEditFormElement(lineFinishBase, position);

            ucLineListElement.ControlClicked += UcAreaListElement_ControlClicked;

            Add(ucLineListElement);

            SetChildIndex(ucLineListElement, position);

            ucFlowLayoutPanel.ScrollControlIntoView(ucLineListElement);

            for (int i = 0; i < Count; i++)
            {
                this[i].positionOnPalette = i;
            }

            return;
        }

        #endregion

        #region Remove Element

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

            if (Count <= 0)
            {
                return;
            }

            if (Count <= 1)
            {
                MessageBox.Show("At least one line finish must remain on the list.");
                return;
            }

            LineFinishBase lineFinishBase = this[SelectedItemIndex].LineFinishBase;

            foreach (var line in baseForm.CurrentPage.DirectedLines)
            {
                if (line.LineFinishManager.LineFinishBase == lineFinishBase)
                {
                    MessageBox.Show("Cannot delete this line finish because it is currently in use.");
                    return;
                }
            }

            LineFinishBaseList.Remove(SelectedItemIndex, Math.Min(SelectedItemIndex, Count - 2));
        }

        private void LineFinishBaseList_ItemRemoved(string guid, int position)
        {
         
            Remove(position);
        }

        internal void Remove(int position)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

            ElementsChanged = true;

            Debug.Assert(position >= 0 && position < Count);

            if (position < 0 || position >= Count)
            {
                return;
            }

            UCLineFinishEditFormElement ucLineListElement = this[position];

            //this.areaFinishList.RemoveAt(position);

            RemoveAt(position);

            ucLineListElement.ControlClicked -= UcAreaListElement_ControlClicked;

            for (int index = position; index < Count; index++)
            {
                UCLineFinishEditFormElement ucAreaListElement1 = this[index];

                ucAreaListElement1.positionOnPalette = index;
            }
        }

        #endregion

        #region Swap Elements

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

            if (SelectedItemIndex <= 0)
            {
                return;
            }

            ElementsChanged = true;

            LineFinishBaseList.Swap(SelectedItemIndex - 1, SelectedItemIndex, SelectedItemIndex - 1);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

            if (SelectedItemIndex >= Count - 1)
            {
                return;
            }

            ElementsChanged = true;

            LineFinishBaseList.Swap(SelectedItemIndex + 1, SelectedItemIndex, SelectedItemIndex + 1);
        }


        private void LineFinishBaseList_ItemsSwapped(int position1, int position2)
        {
            if (zeroLineSelected)
            {
                MessageBox.Show("This operation cannot be completed while editing the zero line.");
                return;
            }

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

            UCLineFinishEditFormElement ucLineListElement1 = this[position1];
            UCLineFinishEditFormElement ucLineListElement2 = this[position2];

            SetChildIndex(ucLineListElement2, position1);
            SetChildIndex(ucLineListElement1, position2);

            ucLineListElement1.positionOnPalette = position2;
            ucLineListElement2.positionOnPalette = position1;

            ElementsChanged = true;
        }

        #endregion

        #region Select Element

        private void UcAreaListElement_ControlClicked(UCLineFinishEditFormElement ucLineListElement)
        {
            LineFinishBaseList.Select(ucLineListElement.positionOnPalette);
        }

        private void LineFinishBaseList_ItemSelected(int position)
        {
            Select(position);
        }

        bool ignoreUpdates = false;

        private void Select(int position)
        {
            ignoreUpdates = true;

            doSelect(position);

            ignoreUpdates = false;
        }

        private void doSelect(int position)
        {
            if (position < 0)
            {
                position = 0;
            }


            if (position >= LineFinishBaseList.Count)
            {
                position = LineFinishBaseList.Count - 1;
            }

            for (int i = 0; i < Count; i++)
            {
                if (i != SelectedItemIndex)
                {
                    this[i].Deselect();
                }
            }

            UCLineFinishEditFormElement ucLineFinish = this[SelectedItemIndex];

            ucLineFinish.Select();


            ucFlowLayoutPanel.ScrollControlIntoView(ucFlowLayoutPanel.Controls[SelectedItemIndex]);
         
            zeroLineSelected = false;


            this.txbWallHeight.Enabled = true;

            this.lblWallHeight.Enabled = true;

            LineFinishBase lineFinishBase = this[SelectedItemIndex].LineFinishBase;

            if (lineFinishBase.WallHeightInFeet == null)
            {
                ignoreWallHeightChange = true;

                this.txbWallHeight.Text = string.Empty;

                ignoreWallHeightChange = false;
            }

            else
            {
                ignoreWallHeightChange = true;

                this.txbWallHeight.Text = lineFinishBase.WallHeightInFeet.Value.ToString("#,##0.00");

                ignoreWallHeightChange = false;

            }

            this.ucZeroLineEditFormElement.Deselect();

           // UCLineFinishEditFormElement ucLineFinish = this[position];

            ucCustomColorPalette.SetSelectedButtonFormat(ucLineFinish.LineFinishBase.LineColor);

            ucCustomDashType.SetSelectedDashElementFormat(ucLineFinish.LineFinishBase.VisioLineType);

            ucLineWidth.SetSelectedLineWidth(ucLineFinish.LineFinishBase.LineWidthInPts);

            ucLineFinish.Select();

            this.txbTag.Enabled = true;

            //this.txbTag_GotFocus(null, null);

            this.txbTag.Text = ucLineFinish.LineName;

            this.txbProduct.Text = ucLineFinish.Product;

            this.txbNotes.Text = ucLineFinish.Notes;

            updateColorDisplay(lineFinishBase.LineColor);
        }

        #endregion

        #endregion

        private bool lineFinishListContainsName(string areaLineParm)
        {
            foreach (UCLineFinishEditFormElement ucLineListElement in this)
            {
                if (ucLineListElement.LineName == areaLineParm)
                {
                    return true;
                }
            }

            return false;
        }

        private string getUniqueLineName()
        {
            HashSet<string> existingLineNames = new HashSet<string>();

            foreach (UCLineFinishEditFormElement ucLineElement in this)
            {
                string lineName = ucLineElement.LineName;

                if (lineName.StartsWith("Line-"))
                {
                    existingLineNames.Add(lineName);
                }
            }

            for (int i = 1; i < 1000; i++)
            {
                string lineName = "Line-" + i.ToString();

                if (!existingLineNames.Contains(lineName))
                {
                    return lineName;
                }
            }

            return string.Empty;
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

            this.lblCurrentColorR.ForeColor = Color.FromArgb(255, r, 0, 0);
            this.lblCurrentColorG.ForeColor = Color.FromArgb(255, 0, g, 0);
            this.lblCurrentColorB.ForeColor = Color.FromArgb(255, 0, 0, b);
            this.lblCurrentColorA.BackColor = Color.FromArgb(a, 128, 128, 128);
        }

        private void btnSaveAsDefault_Click(object sender, EventArgs e)
        {
            LineFinishBaseList lineFinishBaseList = new LineFinishBaseList();

            foreach (LineFinishBase lineFinishBase in LineFinishBaseList)
            {
                string guid = lineFinishBase.Guid;

                LineFinishBase clonedLineFinishBase = lineFinishBase.Clone();

                clonedLineFinishBase.LengthInInches = 0;

                lineFinishBaseList.AddElem(clonedLineFinishBase);
            }

            if (SystemGlobals.paletteSource == "Baseline")
            {
                if (Program.AppConfig.ContainsKey("linebasefinishinitpath") && Program.AppConfig.ContainsKey("zerolinebaseinitpath"))
                {
                    string lineBaseFinishOutputPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["linebasefinishinitpath"]);
                    string zeroLineBaseOutputPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["zerolinebaseinitpath"]);

                    try
                    {
                        if (!lineFinishBaseList.Save(lineBaseFinishOutputPath))
                        {
                            MessageBox.Show("Attempt to save baseline default values failed");
                            return;
                        }

                        if (!ZeroLineBase.Save(zeroLineBaseOutputPath))
                        {
                            MessageBox.Show("Attempt to save baseline default values failed");
                            return;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Attempt to save baseline default values failed: " + ex.Message);

                        return;
                    }

                    ManagedMessageBox.Show("The default baseline line and zeroline finishes have been updated.");

                    ElementsChanged = false;
                }

                else
                {
                    ManagedMessageBox.Show("No default baseline output file path found.");
                }

                return;
            }
            else
            {

                if (Program.AppConfig.ContainsKey("linefinishinitpath") && Program.AppConfig.ContainsKey("zerolineinitpath"))
                {
                    string lineFinishOutputPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["linefinishinitpath"]);
                    string zeroLineOutputPath = Path.Combine(Program.OCEOperatingDataFolder, Program.AppConfig["zerolineinitpath"]);

                    try
                    {
                        if (!lineFinishBaseList.Save(lineFinishOutputPath))
                        {
                            MessageBox.Show("Attempt to save default values failed");
                            return;
                        }

                        if (!ZeroLineBase.Save(zeroLineOutputPath))
                        {
                            MessageBox.Show("Attempt to save default values failed");
                            return;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Attempt to save default values failed: " + ex.Message);

                        return;
                    }

                    ManagedMessageBox.Show("The default line finishes have been updated.");

                    ElementsChanged = false;
                }

                else
                {
                    ManagedMessageBox.Show("No default output file path found.");
                }

                return;
            }
        }

        private HashSet<string> usedLineFinishTags = new HashSet<string>();

        private string currTag = null;

        private void txbTag_GotFocus(object sender, EventArgs e)
        {
            usedLineFinishTags.Clear();

            for (int i = 0; i < this.LineFinishBaseList.Count; i++)
            {
                if (i == SelectedItemIndex)
                {
                    continue;
                }

                usedLineFinishTags.Add(LineFinishBaseList[i].LineName);
            }

            currTag = this.LineFinishBaseList[SelectedItemIndex].LineName;

            string tag = this.txbTag.Text.Trim();

            if (usedLineFinishTags.Contains(tag))
            {
                this.txbTag.BackColor = Color.Pink;
            }

            else
            {
                this.txbTag.BackColor = SystemColors.ControlLightLight;
            }
        }

        bool ignoreTxbTag_LostFocus = false;

        private void txbTag_LostFocus(object sender, EventArgs e)
        {
            string tag = this.txbTag.Text.Trim();

            if (ignoreTxbTag_LostFocus)
            {
                return;
            }
            if (tag.Contains(";"))
            {
                ignoreTxbTag_LostFocus = true;

                MessageBox.Show("Remove invalid character, ';', from tag name before proceeding.");
                //this.txbTag.Focus();

                ignoreTxbTag_LostFocus = false;
                return;
            }

            if (usedLineFinishTags.Contains(tag))
            {
                ManagedMessageBox.Show("The tag '" + tag + "' is already being used by another finish.");

                this.txbTag.Text = currTag;

                this[SelectedItemIndex].LineName = currTag;
                this.ucLineFinishPalette[SelectedItemIndex].LineFinishBase.LineName = currTag;
                //this.ucLineFinishPalette.BaseForm.lblFinishName.Text = currTag;
            }
        }

        private void txbTag_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            if (SelectedItemIndex < 0)
            {
                return;
            }

            string tag = this.txbTag.Text;

            if (tag.Contains(";"))
            {
                ignoreTxbTag_LostFocus= true;

                MessageBox.Show("Invalid character, ';', in tag.");

                ignoreTxbTag_LostFocus= false;

                return;
            }

            if (usedLineFinishTags.Contains(tag))
            {
                this.txbTag.BackColor = Color.Pink;
            }

            else
            {
                this.txbTag.BackColor = SystemColors.ControlLightLight;
            }

            this[SelectedItemIndex].LineName = tag;
            this.ucLineFinishPalette[SelectedItemIndex].LineFinishBase.LineName = tag;
           // this.ucLineFinishPalette.BaseForm.lblFinishName.Text = tag;
        }


        private void txbProduct_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            if (SelectedItemIndex < 0)
            {
                ZeroLineBase.Product = this.txbProduct.Text.Trim();
                return;
            }

            this[SelectedItemIndex].Product = this.txbProduct.Text.Trim();
        }

        private void txbNotes_TextChanged(object sender, EventArgs e)
        {
            ElementsChanged = true;

            if (SelectedItemIndex < 0)
            {
                this.ZeroLineBase.Notes = this.txbNotes.Text.Trim();

                return;
            }

            this[SelectedItemIndex].Notes = this.txbNotes.Text.Trim();
        }

        private void LineFinishesEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (txbTag.Text.Contains(";"))
            {
                ignoreTxbTag_LostFocus= true;   

                MessageBox.Show("Remove invalid character, ';', from tag name before proceeding.");

                e.Cancel = true;

                ignoreTxbTag_LostFocus= false;

                return;
            }

            if (modal)
            {
                return;
            }

            this.ucCustomColorPalette.ColorSelected -= UcCustomColorPalette_ColorSelected;

            LineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            LineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            LineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            LineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;
            LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;
        }

        private void UcCustomDashType_DashTypeSelected(DashTypeSelectedEventArgs args)
        {
            ElementsChanged = true;

            if (zeroLineSelected)
            {
                short visioDashType = (short)args.VisioDashTypeIndex;

                this.ucZeroLineEditFormElement.LineFinishBase.VisioLineType = visioDashType;
                this.ucZeroLineEditFormElement.Invalidate();

                if (SystemState.DesignState != DesignState.Area)
                {
                    return;
                }

                if (baseForm.CanvasManager.BuildingPolyline == null)
                {
                    return;
                }


                foreach (var item in baseForm.CanvasManager.BuildingPolyline)
                {
                    if (!item.IsZeroLine) { continue; }
                    item.SetBaseLineStyle(visioDashType);
                }

                return;
            }

            UCLineFinishPaletteElement ucLine = ucLineFinishPalette[SelectedItemIndex];

            ucLine.LineFinishBase.VisioLineType = (short)args.VisioDashTypeIndex;

            this[SelectedItemIndex].Invalidate();

            ucLine.Invalidate();

           // ucLineFinishPalette.BaseForm.uclSelectedLine.Invalidate();
        }

        private void UcLineWidth_LineWidthSelected(LineWidthSelectedEventArgs args)
        {
            ElementsChanged = true;

            if (zeroLineSelected)
            {
                double widthInPts = (double)args.WidthInPts;

                this.ucZeroLineEditFormElement.LineFinishBase.LineWidthInPts = widthInPts;
                this.ucZeroLineEditFormElement.Invalidate();

                if (SystemState.DesignState != DesignState.Area)
                {
                    return;
                }

                if (baseForm.CanvasManager.BuildingPolyline == null)
                {
                    return;
                }

                foreach (var item in baseForm.CanvasManager.BuildingPolyline)
                {
                    if (! item.IsZeroLine) { continue; }
                    item.SetBaseLineWidth(widthInPts);
                }

                return;
            }

            UCLineFinishPaletteElement ucLine = ucLineFinishPalette[SelectedItemIndex];

            ucLine.LineFinishBase.LineWidthInPts = (double)args.WidthInPts;

            this[SelectedItemIndex].Invalidate();

           
            ucLine.Invalidate();

            //  ucLineFinishPalette.BaseForm.uclSelectedLine.Invalidate();
        }

        private bool ignoreWallHeightChange = false;

        private void txbWallHeight_TextChanged(object sender, EventArgs e)
        {
            if (ignoreWallHeightChange) { return; }

            if (SelectedItemIndex < 0) { return; }

            string wallHeightText = txbWallHeight.Text.Trim();

            LineFinishBase lineFinishBase = this[SelectedItemIndex].LineFinishBase;

            if (string.IsNullOrEmpty(wallHeightText))
            {
                lineFinishBase.WallHeightInFeet = null;

                this.txbWallHeight.BackColor = SystemColors.ControlLightLight;

                return;
            }


            if (!Utilities.IsValidPosDbl(wallHeightText))
            {
                this.txbWallHeight.BackColor = Color.Pink;
                return;
            }

            double wallHeightInFeet = 0;

            if (!double.TryParse(wallHeightText, out wallHeightInFeet))
            {
                this.txbWallHeight.BackColor= Color.Pink;

                return;
            }

            this.txbWallHeight.BackColor = SystemColors.ControlLightLight;

            lineFinishBase.WallHeightInFeet = wallHeightInFeet;
        }

        public IEnumerator<UCLineFinishEditFormElement> GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ucFlowLayoutPanel.GetEnumerator();
        }

        UCLineFinishEditFormElement this[int index]
        {
            get
            {
                return ucFlowLayoutPanel[index];
            }
        }
        public int Count => this.ucFlowLayoutPanel.Count;

        public bool ElementsChanged { get; set; } = true;

        private void Add(UCLineFinishEditFormElement ucLine) => this.ucFlowLayoutPanel.Add(ucLine);

        private void RemoveAt(int selectedElement) => this.ucFlowLayoutPanel.RemoveAt(selectedElement);

        private int GetChildIndex(UCLineFinishEditFormElement ucLine) => this.ucFlowLayoutPanel.GetChildIndex(ucLine);

        private void SetChildIndex(UCLineFinishEditFormElement ucLine, int position) => this.ucFlowLayoutPanel.SetChildIndex(ucLine, position);

        private void btnLineFinishEditorHide_Click(object sender, EventArgs e)
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
        private void LineFinishesEditForm_Disposed(object sender, EventArgs e)
        {
            RemoveFromCursorManagementList();

            LineFinishBaseList.ItemAdded -= LineFinishBaseList_ItemAdded;
            LineFinishBaseList.ItemInserted -= LineFinishBaseList_ItemInserted;
            LineFinishBaseList.ItemRemoved -= LineFinishBaseList_ItemRemoved;
            LineFinishBaseList.ItemsSwapped -= LineFinishBaseList_ItemsSwapped;
            LineFinishBaseList.ItemSelected -= LineFinishBaseList_ItemSelected;
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

    }
}
