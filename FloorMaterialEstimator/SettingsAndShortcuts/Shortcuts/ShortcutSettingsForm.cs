
namespace SettingsLib
{ 
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Utilities;

    public partial class ShortcutSettingsForm : Form
    {
        private List<Label> areaModeShortcutList;
        private List<Label> lineModeShortcutList;
        private List<Label> seamModeShortcutList;
        private List<Label> menuModeShortcutList;
        private List<Label> miscModeShortcutList;

        private List<Label> areaModeTitleList;
        private List<Label> lineModeTitleList;
        private List<Label> seamModeTitleList;
        private List<Label> menuModeTitleList;
        private List<Label> miscModeTitleList;

        private List<GroupBox> groupboxList = new List<GroupBox>();

        private bool userAccess = true;

        public bool SetAsDefault = false;

        public ShortcutMode ShortcutMode { get; set; }  = ShortcutMode.None;

        private int shortcutSelected = -1;

        public ShortcutSettingsForm()
        {
            InitializeComponent();

            this.userAccess = GlobalSettings.AllowEditingOfShortcutKeys;

            buildLabelLists();

            setupShortcutTextBoxes();

            setupTitleClicks();

            SetUserAccess(this.userAccess);

            if (ShortcutSettings.UserType == UserType.Administrator)
            {
                this.lbxShortcuts.DoubleClick += LbxShortcuts_DoubleClick;
            }

            this.grbLineMode.Location = this.grbAreaMode.Location;
            this.grbSeamMode.Location = this.grbAreaMode.Location;
            this.grbMenuMode.Location = this.grbAreaMode.Location;
            this.grbMiscMode.Location = this.grbAreaMode.Location;

            this.groupboxList = new List<GroupBox>()
            {
                this.grbAreaMode
                ,this.grbLineMode
                ,this.grbSeamMode
                ,this.grbMenuMode
                ,this.grbMiscMode
            };

            this.txbDescription.TextChanged += TxbDescription_TextChanged;
            this.Size = new Size(586, 1000);
        }

        public void SetUserAccess(bool access)
        {
            userAccess = access;

            if (userAccess)
            {
                this.txbDescription.Enabled = true;
                this.lbxShortcuts.Enabled = true;
                this.btnClearCurrentSelection.Enabled= true;
            }

            else
            {
                this.txbDescription.Enabled = false;
                this.lbxShortcuts.Enabled = false;
                this.btnClearCurrentSelection.Enabled = false;
            }
        }
        private void buildLabelLists()
        {
            areaModeShortcutList = new List<Label>()
            {
                lblAreaModeCompleteShape
                ,lblAreaModeCompleteByIntersection
                ,lblAreaModeCompleteByMax
                ,lblAreaModeCompleteByMin
                ,lblAreaModeCompleteRotatedShape
                ,lblAreaModeEraseLastLine
                ,lblAreaModeCancelShapeInProgress
                ,lblAreaModeDeleteSelectedShape
                ,lblAreaModeToggleZeroLineMode
                ,lblAreaModeAreaTakeout
                ,lblAreaModeTakeoutAndFill
                ,lblAreaModeEmbedShape
                ,lblAreaModeCopyAndPaste
                ,lblAreaModeToggleFixedWidth
                ,lblAreaModeFixedWidthJump
                ,lblAreaMode2xLineH
                ,lblAreaMode2xLineV
                ,lblAreaSetSelectedArea
            };

            lineModeShortcutList = new List<Label>()
            {
                lblLineModeJump
                ,lblLineMode1XLineMode
                ,lblLineMode2XLineMode
                ,lblLineModeDuplicateLine
                ,lblLineModeToggleDoorTakeout
                ,lblLineModeEraseLastLine
                ,lblLineModeDeleteSelectedLine
                ,lblLineMode2xLineH
                ,lblLineMode2xLineV
                ,lblLineSetSelectedLine
            };

            seamModeShortcutList = new List<Label>()
            {
                lblSeamModeEraseLastLine
                ,lblSeamModeCompleteShape
                ,lblSeamModeCancelShapeInProgress
            };

            menuModeShortcutList = new List<Label>()
            {
                lblMenuModeSaveProject
                ,lblMenuModeZoomIn
                ,lblMenuModeZoomOut
                ,lblMenuModeFitToCanvas
                ,lblMenuModePanMode
                ,lblMenuModeDrawMode
                ,lblMenuModeAreaMode
                ,lblMenuModeLineMode
                ,lblMenuModeSeamMode
                ,lblMenuModeShowFieldGuides
                ,lblMenuModeHideFieldGuides
                ,lblMenuModeDeleteFieldGuides
                ,lblMenuModeToggleHighlightSeamAreas
                ,lblMenuModeAlignToGrid
                ,lblMenuModeFlipAlignToGrid
                ,lblMenuModeLineMeasuringTool
                ,lblMenuModeOversUnders
                ,lblMenuModeFullScreenMode
            };

            miscModeShortcutList = new List<Label>()
            {
                lblMiscModeZoomIn
                ,lblMiscModeZoomOut
                ,lblMiscToggleMeasuringStickVisible
                ,lblMiscShowExtendedCrosshairs
                ,lblMiscShowUnseamedAreas
                ,lblMiscShowZeroAreas
            };

            areaModeTitleList = new List<Label>()
            {
                lblAreaModeTitle0
                ,lblAreaModeTitle1
                ,lblAreaModeTitle2
                ,lblAreaModeTitle3
                ,lblAreaModeTitle4
                ,lblAreaModeTitle5
                ,lblAreaModeTitle6
                ,lblAreaModeTitle7
                ,lblAreaModeTitle8
                ,lblAreaModeTitle9
                ,lblAreaModeTitle10
                ,lblAreaModeTitle11
                ,lblAreaModeTitle12
                ,lblAreaModeTitle13
                ,lblAreaModeTitle14
                ,lblAreaModeTitle15
                ,lblAreaModeTitle16
                ,lblAreaModeTitle17
              
            };

            lineModeTitleList = new List<Label>()
            {
                lblLineModeTitle0
                ,lblLineModeTitle1
                ,lblLineModeTitle2
                ,lblLineModeTitle3
                ,lblLineModeTitle4
                ,lblLineModeTitle5
                ,lblLineModeTitle6
                ,lblLineModeTitle7
                ,lblLineModeTitle8
                ,lblLineModeTitle9
            };

            seamModeTitleList = new List<Label>()
            {
                lblSeamModeTitle0
                ,lblSeamModeTitle1
                ,lblSeamModeTitle2
            };

            menuModeTitleList = new List<Label>()
            {
                lblMenuModeTitle0
                ,lblMenuModeTitle1
                ,lblMenuModeTitle2
                ,lblMenuModeTitle3
                ,lblMenuModeTitle4
                ,lblMenuModeTitle5
                ,lblMenuModeTitle6
                ,lblMenuModeTitle7
                ,lblMenuModeTitle8
                ,lblMenuModeTitle9
                ,lblMenuModeTitle10
                ,lblMenuModeTitle11
                ,lblMenuModeTitle12
                ,lblMenuModeTitle13
                ,lblMenuModeTitle14
                ,lblMenuModeTitle15
                ,lblMenuModeTitle16
                ,lblMenuModeTitle17
            };

            miscModeTitleList = new List<Label>()
            {
                lblMiscModeTitle0
                ,lblMiscModeTitle1
                ,lblMiscModeTitle2
                ,lblMiscModeTitle3
                ,lblMiscModeTitle4
                ,lblMiscModeTitle5
            };
        }

        private void setupShortcutTextBoxes()
        {
            setupShortcutTextBoxes(ShortcutSettings.AreaStateShortcuts, areaModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.LineStateShortcuts, lineModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.SeamStateShortcuts, seamModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.MenuStateShortcuts, menuModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.MiscStateShortcuts, miscModeShortcutList);
        }

        private void setupTitleClicks()
        {
            setupTitleClicks(areaModeTitleList, 100);
            setupTitleClicks(lineModeTitleList, 200);
            setupTitleClicks(seamModeTitleList, 300);
            setupTitleClicks(menuModeTitleList, 400);
            setupTitleClicks(miscModeTitleList, 500);
        }

        private void setupTitleClicks(List<Label> titleList, int modeIndex)
        {
            for (int i = 0; i < titleList.Count; i++)
            {
                titleList[i].Tag = modeIndex + i;
                titleList[i].Click += Label_Click;
            }
        }
        private void Label_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            int tagIndex = (int) label.Tag;

            int lblIndex = tagIndex % 100;
            int modeIndex = tagIndex / 100;

            switch (modeIndex)
            {
                case 1: areaModeTitleClick(lblIndex); return;

                case 2: lineModeTitleClick(lblIndex); return;

                case 3: seamModeTitleClick(lblIndex); return;

                case 4: menuModeTitleClick(lblIndex);return;

                case 5: miscModeTitleClick(lblIndex); return;

                default: throw new NotImplementedException("Invalid shortcut mode.");
            }
            
        }

        private void setupShortcutTextBoxes(string[] shortcuts, List<Label> labelList)
        {
            int shortcutCount = labelList.Count;

            labelList.ForEach(t => t.Text = string.Empty);

            for (int i = 0; i < shortcutCount; i++)
            {
                labelList[i].Text = shortcuts[i];
                labelList[i].BackColor = SystemColors.ControlLightLight;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void LbxShortcuts_DoubleClick(object sender, EventArgs e)
        {
            if (ShortcutMode == ShortcutMode.None)
            {
                return;
            }

            if (shortcutSelected < 0)
            {
                return;
            }

            switch (ShortcutMode)
            {
                case ShortcutMode.AreaMode:
                    updateShortcut(ShortcutSettings.AreaStateKeyToActionMap, areaModeShortcutList, ShortcutSettings.AreaStateShortcuts, ShortcutSettings.AreaStateShortcutKeys, ShortcutSettings.AreaStateShortcutDescriptions);
                    break;

                case ShortcutMode.LineMode:
                    updateShortcut(ShortcutSettings.LineStateKeyToActionMap, lineModeShortcutList, ShortcutSettings.LineStateShortcuts, ShortcutSettings.LineStateShortcutKeys, ShortcutSettings.LineStateShortcutDescriptions);
                    break;
                    
                case ShortcutMode.SeamMode:
                    updateShortcut(ShortcutSettings.SeamStateKeyToActionMap, seamModeShortcutList, ShortcutSettings.SeamStateShortcuts, ShortcutSettings.SeamStateShortcutKeys, ShortcutSettings.SeamStateShortcutDescriptions);
                    break;

                case ShortcutMode.MenuMode:
                    updateShortcut(ShortcutSettings.MenuStateKeyToActionMap, menuModeShortcutList, ShortcutSettings.MenuStateShortcuts, ShortcutSettings.MenuStateShortcutKeys, ShortcutSettings.MenuStateShortcutDescriptions);
                    break;

                case ShortcutMode.MiscMode:
                    updateShortcut(ShortcutSettings.MiscStateKeyToActionMap, miscModeShortcutList, ShortcutSettings.MiscStateShortcuts, ShortcutSettings.MiscStateShortcutKeys, ShortcutSettings.MiscStateShortcutDescriptions);
                    break;
            }
        }

        private void updateShortcut(int[] keyToActionMap, List<Label> shortcutList, string[] shortcuts, string[] shortcutKeys, string[] shortcutDescriptions)
        {
            string orientation = GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? "Rght" : "Left";

            string oldShortcut = shortcutList[shortcutSelected].Text;
            string newShortcut = this.lbxShortcuts.SelectedItem.ToString();
            string newShortcutDescription = this.txbDescription.Text.Trim();

            shortcutList[shortcutSelected].Text = newShortcut;
            shortcuts[shortcutSelected] = newShortcut;

            string regKey = shortcutKeys[shortcutSelected] + '[' + orientation + ']';

            RegistryUtils.SetRegistryValue(regKey, newShortcut);

            int newShortcutIndex = ShortcutSettings.StringToAsciiTable[newShortcut];
            keyToActionMap[newShortcutIndex] = shortcutSelected;

            if (ShortcutSettings.StringToAsciiTable.ContainsKey(oldShortcut))
            {
                int oldShortcutIndex = ShortcutSettings.StringToAsciiTable[oldShortcut];
                keyToActionMap[oldShortcutIndex] = -1;
            }

            setupDesignStateShortcutList(shortcutList);
        }

        bool ignoreTextChangeEvent = true;

        private void TxbDescription_TextChanged(object sender, EventArgs e)
        {
            if (ignoreTextChangeEvent)
            {
                return;
            }

            if (this.shortcutSelected < 0)
            {
                return;
            }

            if (ShortcutMode == ShortcutMode.None)
            {
                return;
            }

            string descText = this.txbDescription.Text.Trim();

            switch (ShortcutMode)
            {
                case ShortcutMode.AreaMode:
                    setDescription(ShortcutSettings.AreaStateShortcutKeys, ShortcutSettings.AreaStateShortcutDescriptions, descText);
                    break;

                case ShortcutMode.LineMode:
                    setDescription(ShortcutSettings.LineStateShortcutKeys, ShortcutSettings.LineStateShortcutDescriptions, descText);
                    break;

                case ShortcutMode.SeamMode:
                    setDescription(ShortcutSettings.SeamStateShortcutKeys, ShortcutSettings.SeamStateShortcutDescriptions, descText);
                    break;

                case ShortcutMode.MenuMode:
                    setDescription(ShortcutSettings.MenuStateShortcutKeys, ShortcutSettings.MenuStateShortcutDescriptions, descText);
                    break;

                case ShortcutMode.MiscMode:
                    setDescription(ShortcutSettings.MiscStateShortcutKeys, ShortcutSettings.MiscStateShortcutDescriptions, descText);
                    break;

                default: break;
            }

        }

        private void setDescription(string[] shortcutKeys, string[] shortcutDescriptions, string descText)
        {
            string regKey = shortcutKeys[shortcutSelected] + "[Desc]";

            RegistryUtils.SetRegistryValue(regKey, descText);

            shortcutDescriptions[shortcutSelected] = descText;
        }

        private void areaModeTitleClick(int shortcutIndex)
        {
            ignoreTextChangeEvent = true;

            doAreaModeTitleClick(shortcutIndex);

            ignoreTextChangeEvent = false;

        }

        private void doAreaModeTitleClick(int shortcutIndex)
        {
            if (areaModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(areaModeTitleList[shortcutIndex]);

                this.txbDescription.Text = string.Empty;
            }

            else
            {
                setDesignStateSelection(areaModeShortcutList, areaModeTitleList, shortcutIndex, ShortcutMode.AreaMode);
                
                this.txbDescription.Text = ShortcutSettings.AreaStateShortcutDescriptions[shortcutIndex];
            }
        }
        private void lineModeTitleClick(int shortcutIndex)
        {
            ignoreTextChangeEvent = true;

            doLineModeTitleClick(shortcutIndex);

            ignoreTextChangeEvent = false;
        }

        private void doLineModeTitleClick(int shortcutIndex)
        {
            if (lineModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(lineModeTitleList[shortcutIndex]);

                this.txbDescription.Text = string.Empty;
            }

            else
            {
                setDesignStateSelection(lineModeShortcutList, lineModeTitleList, shortcutIndex, ShortcutMode.LineMode);

                this.txbDescription.Text = ShortcutSettings.LineStateShortcutDescriptions[shortcutIndex];
            }
        }

        private void seamModeTitleClick(int shortcutIndex)
        {
            ignoreTextChangeEvent = true;

            doSeamModeTitleClick(shortcutIndex);

            ignoreTextChangeEvent = false;

        }

        private void doSeamModeTitleClick(int shortcutIndex)
        {
            if (seamModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(seamModeTitleList[shortcutIndex]);

                this.txbDescription.Text = string.Empty;
            }

            else
            {
                setDesignStateSelection(seamModeShortcutList, seamModeTitleList, shortcutIndex, ShortcutMode.SeamMode);

                this.txbDescription.Text = ShortcutSettings.SeamStateShortcutDescriptions[shortcutIndex];
            }
        }

        private void menuModeTitleClick(int shortcutIndex)
        {
            ignoreTextChangeEvent = true;

            doMenuModeTitleClick(shortcutIndex);

            ignoreTextChangeEvent = false;

        }

        private void doMenuModeTitleClick(int shortcutIndex)
        {
            if (menuModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(menuModeTitleList[shortcutIndex]);

                this.txbDescription.Text = string.Empty;
            }

            else
            {
                setMenuModeSelection(menuModeTitleList, shortcutIndex, ShortcutMode.MenuMode);

                this.txbDescription.Text = ShortcutSettings.MenuStateShortcutDescriptions[shortcutIndex];
            }
        }

        private void miscModeTitleClick(int shortcutIndex)
        {
            ignoreTextChangeEvent = true;

            doMiscModeTitleClick(shortcutIndex);

            ignoreTextChangeEvent = false;
        }

        private void doMiscModeTitleClick(int shortcutIndex)
        {
            if (miscModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(miscModeTitleList[shortcutIndex]);

                this.txbDescription.Text = string.Empty;
            }

            else
            {
                setMiscModeSelection(miscModeTitleList, shortcutIndex, ShortcutMode.MiscMode);

                this.txbDescription.Text = ShortcutSettings.MiscStateShortcutDescriptions[shortcutIndex];
            }
        }

        private void setupDesignStateShortcutList(List<Label> shortcutList)
        {
            if (ShortcutSettings.UserType != UserType.Administrator)
            {
                return;
            }

            HashSet<string> exclusionSet = new HashSet<string>();

            populateFromShortcutList(exclusionSet, shortcutList);
            populateFromShortcutList(exclusionSet, miscModeShortcutList);
            populateFromShortcutList(exclusionSet, menuModeShortcutList);

            populateListBox(exclusionSet);
        }

        private void setupMenuModeShortcutList()
        {
            if (ShortcutSettings.UserType != UserType.Administrator)
            {
                return;
            }

            HashSet<string> exclusionSet = new HashSet<string>();

            populateFromShortcutList(exclusionSet, menuModeShortcutList);
            populateFromShortcutList(exclusionSet, miscModeShortcutList);
            populateFromShortcutList(exclusionSet, areaModeShortcutList);
            populateFromShortcutList(exclusionSet, lineModeShortcutList);
            populateFromShortcutList(exclusionSet, seamModeShortcutList);

            populateListBox(exclusionSet);
        }

        private void setupMiscModeShortcutList()
        {
            if (ShortcutSettings.UserType != UserType.Administrator)
            {
                return;
            }

            HashSet<string> exclusionSet = new HashSet<string>();

            populateFromShortcutList(exclusionSet, menuModeShortcutList);
            populateFromShortcutList(exclusionSet, miscModeShortcutList);
            populateFromShortcutList(exclusionSet, areaModeShortcutList);
            populateFromShortcutList(exclusionSet, lineModeShortcutList);
            populateFromShortcutList(exclusionSet, seamModeShortcutList);

            populateListBox(exclusionSet);
        }

        private void populateFromShortcutList(HashSet<string> exclusionSet, List<Label> shortcutList)
        {
            foreach (Label label in shortcutList)
            {
                string shortcut = label.Text.Trim();

                if (string.IsNullOrEmpty(shortcut))
                {
                    continue;
                }

                if (exclusionSet.Contains(shortcut))
                {
                    continue;
                }

                exclusionSet.Add(shortcut);
            }
        }

        private void populateListBox(HashSet<string> exclusionSet)
        { 
            this.lbxShortcuts.BeginUpdate();

            this.lbxShortcuts.Items.Clear();

            foreach (KeyValuePair<string, int> kvp in ShortcutSettings.StringToAsciiTable)
            {
                if ((kvp.Value >= 106 && kvp.Value <= 109) || (kvp.Value >= 111 && kvp.Value <= 122))
                {
                    continue;
                }

                if (!exclusionSet.Contains(kvp.Key))
                {
                    this.lbxShortcuts.Items.Add(kvp.Key);
                }
            }

            this.lbxShortcuts.EndUpdate();
        }

        public void SetDesignStateSelection(ShortcutMode shortcutMode)
        {
            clearTitleBackgrounds();

            this.ShortcutMode = shortcutMode;

            this.shortcutSelected = -1;

            switch (shortcutMode)
            {
                case ShortcutMode.AreaMode:
                    setModeVisibility(this.grbAreaMode);
                    break;

                case ShortcutMode.LineMode:
                    setModeVisibility(this.grbLineMode);
                    break;

                case ShortcutMode.SeamMode:
                    setModeVisibility(this.grbSeamMode);
                    break;

                case ShortcutMode.MenuMode:
                    setModeVisibility(this.grbMenuMode);
                    break;

                case ShortcutMode.MiscMode:
                    setModeVisibility(this.grbMiscMode);
                    break;

            }

            ignoreTextChangeEvent = true;

            this.txbDescription.Text = string.Empty;

            ignoreTextChangeEvent = false;

            ShortcutMode = shortcutMode;
        }

        private void setModeVisibility(GroupBox modeGroupBox)
        {
            modeGroupBox.Visible = true;
            modeGroupBox.BringToFront();

            foreach (GroupBox groupBox in this.groupboxList)
            {
                if (groupBox != modeGroupBox)
                {
                    groupBox.Visible = false;
                    groupBox.SendToBack();
                }
            }
        }

        private void setDesignStateSelection(List<Label> shortcutList, List<Label> titleList, int shortcutIndex, ShortcutMode shortcutMode)
        {
            clearTitleBackgrounds();

            titleList[shortcutIndex].BackColor = Color.Orange;

            if (userAccess)
            {
                setupDesignStateShortcutList(shortcutList);
            }

            ShortcutMode = shortcutMode;

            shortcutSelected = shortcutIndex;
        }

        private void setMenuModeSelection(List<Label> titleList, int shortcutIndex, ShortcutMode shortcutMode)
        {
            clearTitleBackgrounds();

            titleList[shortcutIndex].BackColor = Color.Orange;

            if (userAccess)
            {
                setupMenuModeShortcutList();
            }

            ShortcutMode = shortcutMode;

            shortcutSelected = shortcutIndex;
        }

        private void setMiscModeSelection(List<Label> titleList, int shortcutIndex, ShortcutMode shortcutMode)
        {
            clearTitleBackgrounds();

            titleList[shortcutIndex].BackColor = Color.Orange;

            if (userAccess)
            {
                setupMiscModeShortcutList();
            }

            ShortcutMode = shortcutMode;

            shortcutSelected = shortcutIndex;
        }

        private void resetSelection(Label label)
        {
            this.lbxShortcuts.Items.Clear();

            label.BackColor = SystemColors.Control;

            ShortcutMode = ShortcutMode.None;

            shortcutSelected = -1;
        }

        private void clearTitleBackgrounds()
        {
            areaModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            lineModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            seamModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            menuModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            miscModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
        }

        private void btnClearCurrentSelection_Click(object sender, EventArgs e)
        {
            if (ShortcutMode == ShortcutMode.None)
            {
                return;
            }

            if (shortcutSelected < 0)
            {
                return;
            }

            switch (ShortcutMode)
            {
                case ShortcutMode.AreaMode:
                    clearShortCut(ShortcutSettings.AreaStateKeyToActionMap, areaModeShortcutList, ShortcutSettings.AreaStateShortcuts, ShortcutSettings.AreaStateShortcutKeys);
                    break;

                case ShortcutMode.LineMode:
                    clearShortCut(ShortcutSettings.LineStateKeyToActionMap, lineModeShortcutList, ShortcutSettings.LineStateShortcuts, ShortcutSettings.LineStateShortcutKeys);
                    break;

                case ShortcutMode.SeamMode:
                    clearShortCut(ShortcutSettings.SeamStateKeyToActionMap, seamModeShortcutList, ShortcutSettings.SeamStateShortcuts, ShortcutSettings.SeamStateShortcutKeys);
                    break;

                case ShortcutMode.MenuMode:
                    clearShortCut(ShortcutSettings.MenuStateKeyToActionMap, menuModeShortcutList, ShortcutSettings.MenuStateShortcuts, ShortcutSettings.MenuStateShortcutKeys);
                    break;

                case ShortcutMode.MiscMode:
                    clearShortCut(ShortcutSettings.MiscStateKeyToActionMap, miscModeShortcutList, ShortcutSettings.MiscStateShortcuts, ShortcutSettings.MiscStateShortcutKeys);
                    break;
            }
        }

        private void clearShortCut(int[] keyToActionMap, List<Label> shortcutList, string[] shortcuts, string[] shortcutKeys)
        {
            string orientation = GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? "Rght" : "Left";

            string oldShortcut = shortcutList[shortcutSelected].Text;

            shortcutList[shortcutSelected].Text = string.Empty;
            shortcuts[shortcutSelected] = null;

            string regKey = shortcutKeys[shortcutSelected] + '[' + orientation + ']';

            RegistryUtils.SetRegistryValue(regKey, "");

            if (ShortcutSettings.StringToAsciiTable.ContainsKey(oldShortcut))
            {
                int oldShortcutIndex = ShortcutSettings.StringToAsciiTable[oldShortcut];
                keyToActionMap[oldShortcutIndex] = -1;
            }

            setupDesignStateShortcutList(shortcutList);
        }
    }
}
