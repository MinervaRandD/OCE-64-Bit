//-------------------------------------------------------------------------------//
// <copyright file="ShortcutSettingsForm.cs" company="Bruun Estimating, LLC">    // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

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
        private List<TextBox> areaModeTextBoxList;
        private List<TextBox> lineModeTextBoxList;
        private List<TextBox> seamModeTextBoxList;
        private List<TextBox> generalTextBoxList;

        public bool SetAsDefault = false;

        public ShortcutSettingsForm()
        {
            InitializeComponent();

            btnShowErrors.Visible = false;

            buildTextBoxLists();

            setupShortcutTextBoxes();
        }

        private void buildTextBoxLists()
        {
            areaModeTextBoxList = new List<TextBox>()
            {
                txbAreaModeEraseLastLine,
                txbAreaModeCompleteShape,
                txbAreaModeCancelShapeInProgress,
                txbAreaModeToggleZeroLineMode
            };

            lineModeTextBoxList = new List<TextBox>()
            {
                txbLineModeEraseLastLine,
                txbJump,
                txbSingleLine,
                txbDoubleLine,
                txbDuplicateLine
            };

            seamModeTextBoxList = new List<TextBox>()
            {
                txbSeamModeEraseLastLine,
                txbSeamModeCompleteShape,
                txbSeamModeCancelShapeInProgress
            };

            generalTextBoxList = new List<TextBox>()
            {
                txbZoomIn,
                txbZoomOut,
                txbToggleAreaLineMode,

            };

            // Just to be sure everything is kept consistent

            Debug.Assert(areaModeTextBoxList.Count == ShortcutSettings.AreaStateShortcutCount);
            Debug.Assert(lineModeTextBoxList.Count == ShortcutSettings.LineStateShortcutCount);
            Debug.Assert(seamModeTextBoxList.Count == ShortcutSettings.SeamStateShortcutCount);
            Debug.Assert(generalTextBoxList.Count == ShortcutSettings.GeneralShortcutCount);
        }

        private void setupShortcutTextBoxes()
        {
            setupShortcutTextBoxes(ShortcutSettings.AreaStateShortcuts, areaModeTextBoxList, ShortcutSettings.AreaStateShortcutCount);
            setupShortcutTextBoxes(ShortcutSettings.LineStateShortcuts, lineModeTextBoxList, ShortcutSettings.LineStateShortcutCount);
            setupShortcutTextBoxes(ShortcutSettings.SeamStateShortcuts, seamModeTextBoxList, ShortcutSettings.SeamStateShortcutCount);
            setupShortcutTextBoxes(ShortcutSettings.GeneralShortcuts, generalTextBoxList, ShortcutSettings.GeneralShortcutCount);
        }

        private void setupShortcutTextBoxes(HashSet<string>[] shortcutArray, List<TextBox> textboxList, int shortcutCount)
        {
            textboxList.ForEach(t => t.Text = string.Empty);
         
            for (int i = 0; i < shortcutCount; i++)
            {
                HashSet<string> shortcutList = shortcutArray[i];

                if (shortcutList == null)
                {
                    continue;
                }

                if (shortcutList.Count <= 0)
                {
                    continue;
                }

                textboxList[i].Text = string.Join(",", shortcutList.Select(s => s.Trim()));

                textboxList[i].BackColor = SystemColors.ControlLightLight;
            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            buildShortcutTable();
            
            if (errorsFound())
            {
                btnShowErrors.Visible = true;

                MessageBox.Show("Errors found. Click on 'Show Errors' to see list.");

                return;
            }

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private int areaModeErrors = 0;
        private int lineModeErrors = 0;
        private int seamModeErrors = 0;
        private int generalErrors = 0;

        private bool errorsFound()
        {
            areaModeErrors = 0;
            lineModeErrors = 0;
            seamModeErrors = 0;
            generalErrors = 0;

            for (int i = 0; i < ShortcutSettings.AreaStateShortcutCount; i++)
            {
               if (areaModeErrorList[i].Count > 0)
                {
                    areaModeErrors++;

                    areaModeTextBoxList[i].BackColor = Color.Pink;
                }

               else
                {
                    areaModeTextBoxList[i].BackColor = SystemColors.ControlLightLight;
                }
            }

            return areaModeErrors > 0 || lineModeErrors > 0 || generalErrors > 0;
        }

        public List<string>[] areaModeErrorList;
        public List<string>[] lineModeErrorList;
        public List<string>[] seamModeErrorList;
        public List<string>[] generalErrorList;

        public List<string>[] AreaModeShortcuts;
        public List<string>[] LineModeShortcuts;
        public List<string>[] SeamModeShortcuts;
        public List<string>[] GeneralShortcuts;

        public HashSet<string>[] AreaModeShortcutSet;
        public HashSet<string>[] LineModeShortcutSet;
        public HashSet<string>[] SeamModeShortcutSet;
        public HashSet<string>[] GeneralShortcutSet;

        private void buildShortcutTable()
        {
            areaModeErrorList = new List<string>[ShortcutSettings.AreaStateShortcutCount];
            lineModeErrorList = new List<string>[ShortcutSettings.LineStateShortcutCount];
            seamModeErrorList = new List<string>[ShortcutSettings.SeamStateShortcutCount];
            generalErrorList = new List<string>[ShortcutSettings.GeneralShortcutCount];

            AreaModeShortcuts = new List<string>[ShortcutSettings.AreaStateShortcutCount];
            LineModeShortcuts = new List<string>[ShortcutSettings.LineStateShortcutCount];
            SeamModeShortcuts = new List<string>[ShortcutSettings.SeamStateShortcutCount];
            GeneralShortcuts = new List<string>[ShortcutSettings.GeneralShortcutCount];

            AreaModeShortcutSet = new HashSet<string>[ShortcutSettings.AreaStateShortcutCount];
            LineModeShortcutSet = new HashSet<string>[ShortcutSettings.LineStateShortcutCount];
            SeamModeShortcutSet = new HashSet<string>[ShortcutSettings.SeamStateShortcutCount];
            GeneralShortcutSet = new HashSet<string>[ShortcutSettings.GeneralShortcutCount];

            buildShortcutTable(generalTextBoxList, GeneralShortcuts, generalErrorList, GeneralShortcutSet, ShortcutSettings.GeneralShortcutCount, false);
            buildShortcutTable(areaModeTextBoxList, AreaModeShortcuts, areaModeErrorList, AreaModeShortcutSet, ShortcutSettings.AreaStateShortcutCount, true);
            buildShortcutTable(lineModeTextBoxList, LineModeShortcuts, lineModeErrorList, LineModeShortcutSet, ShortcutSettings.LineStateShortcutCount, true);
            buildShortcutTable(seamModeTextBoxList, SeamModeShortcuts, seamModeErrorList, SeamModeShortcutSet, ShortcutSettings.SeamStateShortcutCount, true);
        }

        private void buildShortcutTable(List<TextBox> textBoxList, List<string>[] shortcutList, List<string>[] errorList, HashSet<string>[] shortcutSet,  int shortCutCount, bool compareToGeneral)
        {
            for (int i = 0; i < shortCutCount;i++)
            {
                shortcutList[i] = new List<string>();
                shortcutSet[i] = new HashSet<string>();
                errorList[i] = new List<string>();
            }

            for (int i = 0; i < shortCutCount; i++)
            {
                string shortcutString = textBoxList[i].Text.Trim();
                
                parseshortcut(shortcutString, shortcutList[i], errorList[i], shortcutSet[i], compareToGeneral);
            }
        }

        private void parseshortcut(string textBoxStr, List<string> shortcutList, List<string> errorList, HashSet<string> shortcutSet, bool compareToGeneral)
        {
            if (string.IsNullOrEmpty(textBoxStr))
            {
                return;
            }
            
            string[] elementArray = textBoxStr.Split(',');

            foreach (string element in elementArray)
            {
                string element1 = element.Trim();

                if (string.IsNullOrEmpty(element1))
                {
                    continue;
                }

                string element2 = element1.ToUpper();

                if (shortcutList.Contains(element2))
                {
                    continue;
                }

                if (!ShortcutSettings.IsValidShortcut(element2))
                {
                    errorList.Add("Invalid shortcut: '" + element2 + "'");
                    continue;
                }

                if (shortcutSet.Contains(element2))
                {
                    errorList.Add("Duplicate shortcut: '" + element2 + "'");
                    continue;
                }

                if (compareToGeneral)
                {
                    if (GeneralShortcutSetContains(element2))
                    {
                        errorList.Add("Shortcut duplicates a shortcut in the general set of shortcuts: '" + element2 + "'");
                        continue;
                    }
                }

                shortcutSet.Add(element2);
                shortcutList.Add(element2);
            }
        }

        private bool GeneralShortcutSetContains(string element)
        {
            for (int i = 0; i < GeneralShortcutSet.Length; i++)
            {
                if (GeneralShortcutSet[i].Contains(element))
                {
                    return true;
                }
            }

            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void btnSetAsDefault_Click(object sender, EventArgs e)
        {
            if (this.btnSetAsDefault.BackColor == Color.Orange)
            {
                SetAsDefault = false;
                this.btnSetAsDefault.BackColor = SystemColors.ControlLightLight;
            }

            else
            {
                SetAsDefault = true;
                this.btnSetAsDefault.BackColor = Color.Orange;
            }
        }
    }
}
