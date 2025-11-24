
namespace FloorMaterialEstimator.ShortcutsAndSettings
{
    using SettingsLib;
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
        private List<Label> generalShortcutList;

        private List<Label> areaModeTitleList;
        private List<Label> lineModeTitleList;
        private List<Label> seamModeTitleList;
        private List<Label> generalTitleList;

        public bool SetAsDefault = false;

        private ShortcutModeSelected shortcutModeSelected = ShortcutModeSelected.None;
        private int shortcutSelected = -1;

        public ShortcutSettingsForm()
        {
            InitializeComponent();

            buildLabelLists();

            setupShortcutTextBoxes();

            if (GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded)
            {
                this.rbnRightHanded.Checked = true;
            }

            else
            {
                this.rbnLeftHanded.Checked = true;
            }

            this.rbnRightHanded.CheckedChanged += RbnRightHanded_CheckedChanged;
            this.rbnLeftHanded.CheckedChanged += RbnLeftHanded_CheckedChanged;
            this.lbxShortcuts.DoubleClick += LbxShortcuts_DoubleClick;
        }

        private void LbxShortcuts_DoubleClick(object sender, EventArgs e)
        {
            if (shortcutModeSelected == ShortcutModeSelected.None)
            {
                return;
            }

            if (shortcutSelected < 0)
            {
                return;
            }

            switch (shortcutModeSelected)
            {
                case ShortcutModeSelected.AreaMode:
                    updateShortcut(ShortcutSettings.AreaStateKeyToActionMap, areaModeShortcutList, ShortcutSettings.AreaStateShortcuts, ShortcutSettings.AreaStateShortcutKeys);
                    break;

                case ShortcutModeSelected.LineMode:
                    updateShortcut(ShortcutSettings.LineStateKeyToActionMap, lineModeShortcutList, ShortcutSettings.LineStateShortcuts, ShortcutSettings.LineStateShortcutKeys);
                    break;
                    
                case ShortcutModeSelected.SeamMode:
                    updateShortcut(ShortcutSettings.SeamStateKeyToActionMap,seamModeShortcutList, ShortcutSettings.SeamStateShortcuts, ShortcutSettings.SeamStateShortcutKeys);
                    break;

                case ShortcutModeSelected.General:
                    updateShortcut(ShortcutSettings.GeneralKeyToActionMap, generalShortcutList, ShortcutSettings.GeneralShortcuts, ShortcutSettings.GeneralShortcutKeys);
                    break;
            }
        }

        private void updateShortcut(int[] keyToActionMap, List<Label> shortcutList, string[] shortcuts, string[] shortcutKeys)
        {
            string orientation = GlobalSettings.ShortcutOrientation == ShortCutOrientation.RightHanded ? "Rght" : "Left";

            string oldShortcut = shortcutList[shortcutSelected].Text;
            string newShortcut = this.lbxShortcuts.SelectedItem.ToString();

            shortcutList[shortcutSelected].Text = newShortcut;
            shortcuts[shortcutSelected] = newShortcut;

            string regKey = shortcutKeys[shortcutSelected] + orientation;

            RegistryUtils.SetRegistryValue(regKey, newShortcut);

            int oldShortcutIndex = ShortcutSettings.StringToAsciiTable[oldShortcut];
            int newShortcutIndex = ShortcutSettings.StringToAsciiTable[newShortcut];

            keyToActionMap[oldShortcutIndex] = -1;
            keyToActionMap[newShortcutIndex] = shortcutSelected;

            setupDesignStateShortcutList(shortcutList);
        }

        private void RbnRightHanded_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnRightHanded.Checked)
            {
                GlobalSettings.ShortcutOrientation = ShortCutOrientation.RightHanded;
                setupShortcutTextBoxes();
            }
        }

        private void RbnLeftHanded_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnLeftHanded.Checked)
            {
                GlobalSettings.ShortcutOrientation = ShortCutOrientation.LeftHanded;
                setupShortcutTextBoxes();
            }
        }

        private void buildLabelLists()
        {
            areaModeShortcutList = new List<Label>()
            {
                lblAreaModeEraseLastLine,
                lblAreaModeCompleteShape,
                lblAreaModeCompleteByIntersection,
                lblAreaModeCancelShapeInProgress,
                lblAreaModeToggleZeroLineMode
            };

            lineModeShortcutList = new List<Label>()
            {
                lblLineModeEraseLastLine,
                lblJump,
                lblSingleLine,
                lblDoubleLine,
                lblDuplicateLine
            };

            seamModeShortcutList = new List<Label>()
            {
                lblSeamModeEraseLastLine,
                lblSeamModeCompleteShape,
                lblSeamModeCancelShapeInProgress
            };

            generalShortcutList = new List<Label>()
            {
                lblZoomIn,
                lblZoomOut,
                lblToggleAreaLineMode,

            };

            areaModeTitleList = new List<Label>()
            {
                lblAreaModeTitle0,
                lblAreaModeTitle1,
                lblAreaModeTitle2,
                lblAreaModeTitle3,
                lblAreaModeTitle4
            };

            lineModeTitleList = new List<Label>()
            {
                lblLineModeTitle0,
                lblLineModeTitle1,
                lblLineModeTitle2,
                lblLineModeTitle3,
                lblLineModeTitle4
            };

            seamModeTitleList = new List<Label>()
            {
                lblSeamModeTitle0,
                lblSeamModeTitle1,
                lblSeamModeTitle2
            };

            generalTitleList = new List<Label>()
            {
                lblGeneralTitle0,
                lblGeneralTitle1,
                lblGeneralTitle2
            };
        }

        private void setupShortcutTextBoxes()
        {
            setupShortcutTextBoxes(ShortcutSettings.AreaStateShortcuts, areaModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.LineStateShortcuts, lineModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.SeamStateShortcuts, seamModeShortcutList);
            setupShortcutTextBoxes(ShortcutSettings.GeneralShortcuts, generalShortcutList);
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
        
        private void lblAreaModeTitle0_Click(object sender, EventArgs e)
        {
            areaModeTitleClick(0);
        }

        private void lblAreaModeTitle1_Click(object sender, EventArgs e)
        {
            areaModeTitleClick(1);
        }

        private void lblAreaModeTitle2_Click(object sender, EventArgs e)
        {
            areaModeTitleClick(2);
        }

        private void lblAreaModeTitle3_Click(object sender, EventArgs e)
        {
            areaModeTitleClick(3);
        }

        private void lblAreaModeTitle4_Click(object sender, EventArgs e)
        {
            areaModeTitleClick(4);
        }

        private void areaModeTitleClick(int shortcutIndex)
        {
            if (areaModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(areaModeTitleList[shortcutIndex]);
            }

            else
            {
                setDesignStateSelection(areaModeShortcutList, areaModeTitleList, shortcutIndex, ShortcutModeSelected.AreaMode);
            }
        }


        private void lblLineModeTitle0_Click(object sender, EventArgs e)
        {
            lineModeTitleClick(0);
        }

        private void lblLineModeTitle1_Click(object sender, EventArgs e)
        {
            lineModeTitleClick(1);
        }

        private void lblLineModeTitle2_Click(object sender, EventArgs e)
        {
            lineModeTitleClick(2);
        }

        private void lblLineModeTitle3_Click(object sender, EventArgs e)
        {
            lineModeTitleClick(3);
        }

        private void lblLineModeTitle4_Click(object sender, EventArgs e)
        {
            lineModeTitleClick(4);
        }

        private void lineModeTitleClick(int shortcutIndex)
        {
            if (lineModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(lineModeTitleList[shortcutIndex]);
            }

            else
            {
                setDesignStateSelection(lineModeShortcutList, lineModeTitleList, shortcutIndex, ShortcutModeSelected.LineMode);
            }
        }

        private void lblSeamModeTitle0_Click(object sender, EventArgs e)
        {
            seamModeTitleClick(0);
        }

        private void lblSeamModeTitle1_Click(object sender, EventArgs e)
        {
            seamModeTitleClick(1);
        }

        private void lblSeamModeTitle2_Click(object sender, EventArgs e)
        {
            seamModeTitleClick(2);
        }

        private void seamModeTitleClick(int shortcutIndex)
        {
            if (seamModeTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(seamModeTitleList[shortcutIndex]);
            }

            else
            {
                setDesignStateSelection(seamModeShortcutList, seamModeTitleList, shortcutIndex, ShortcutModeSelected.SeamMode);
            }
        }

        private void lblGeneralTitle0_Click(object sender, EventArgs e)
        {
            generalTitleClick(0);
        }

        private void lblGeneralTitle1_Click(object sender, EventArgs e)
        {
            generalTitleClick(1);
        }

        private void lblGeneralTitle2_Click(object sender, EventArgs e)
        {
            generalTitleClick(2);
        }

        private void generalTitleClick(int shortcutIndex)
        {
            if (generalTitleList[shortcutIndex].BackColor == Color.Orange)
            {
                resetSelection(generalTitleList[shortcutIndex]);
            }

            else
            {
                setGeneralSelection(shortcutIndex);
            }
        }

        private void setupDesignStateShortcutList(List<Label> shortcutList)
        {
            HashSet<string> exclusionSet = new HashSet<string>();

            foreach (Label label in shortcutList)
            {
                string shortcut = label.Text.Trim();

                if (!string.IsNullOrEmpty(shortcut))
                {
                    exclusionSet.Add(shortcut);
                }
            }

            foreach (Label label in generalShortcutList)
            {
                string shortcut = label.Text.Trim();

                if (!string.IsNullOrEmpty(shortcut))
                {
                    exclusionSet.Add(shortcut);
                }
            }

            populateListBox(exclusionSet);
        }

        private void setupGeneralShortcutList()
        {
            HashSet<string> exclusionSet = new HashSet<string>();

            foreach (Label label in areaModeShortcutList.Concat(lineModeShortcutList).Concat(seamModeShortcutList))
            {
                string shortcut = label.Text.Trim();

                if (!string.IsNullOrEmpty(shortcut))
                {
                    exclusionSet.Add(shortcut);
                }
            }

            foreach (Label label in generalShortcutList)
            {
                string shortcut = label.Text.Trim();

                if (!string.IsNullOrEmpty(shortcut))
                {
                    exclusionSet.Add(shortcut);
                }
            }

            populateListBox(exclusionSet);
        }

        private void populateListBox(HashSet<string> exclusionSet)
        { 
            this.lbxShortcuts.BeginUpdate();

            this.lbxShortcuts.Items.Clear();

            foreach (KeyValuePair<string, int> kvp in ShortcutSettings.StringToAsciiTable)
            {
                if (kvp.Value >= 97 && kvp.Value <= 122)
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

        private void setDesignStateSelection(List<Label> shortcutList, List<Label> titleList, int shortcutIndex, ShortcutModeSelected shortcutMode)
        {
            clearTitleBackgrounds();

            titleList[shortcutIndex].BackColor = Color.Orange;

            setupDesignStateShortcutList(shortcutList);

            shortcutModeSelected = shortcutMode;

            shortcutSelected = shortcutIndex;
        }

        private void setGeneralSelection(int shortcutIndex)
        {
            clearTitleBackgrounds();

            generalTitleList[shortcutIndex].BackColor = Color.Orange;

            setupGeneralShortcutList();

            shortcutModeSelected = ShortcutModeSelected.General;

            shortcutSelected = shortcutIndex;
        }

        private void resetSelection(Label label)
        {
            this.lbxShortcuts.Items.Clear();

            label.BackColor = SystemColors.Control;

            shortcutModeSelected = ShortcutModeSelected.None;

            shortcutSelected = -1;
        }

        private void clearTitleBackgrounds()
        {
            areaModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            lineModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            seamModeTitleList.ForEach(l => l.BackColor = SystemColors.Control);
            generalTitleList.ForEach(l => l.BackColor = SystemColors.Control);
        }

    }

    public enum ShortcutModeSelected
    {
        None = 0,
        AreaMode = 1,
        LineMode = 2,
        SeamMode = 3,
        General = 4
    }
}
