using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Utilities;

namespace SettingsLib
{
    

    public class ToolTipEditManager
    {
        //private List<object> toolTipObjectList = new List<object>();

        private ToolTip toolTip;

        private Dictionary<string, object> objectDict;

        private string toolTipDefinitionsFilePath;

        public ToolTipEditManager(
            ToolTip toolTip
            ,List<object> toolTipInptObjectList
            ,string toolTipDefinitionsFilePath)
        {
            this.toolTip = toolTip;

            this.toolTipDefinitionsFilePath = toolTipDefinitionsFilePath;
            // Create a list (dictionary) of objects to be tool tipped

            objectDict = generateObjectNameDict(toolTipInptObjectList);

            // Load the default tool tip definitions. Note result is an update to the tool tip itself.

            loadToolTipDefinitions(toolTipDefinitionsFilePath);
        }

        private Dictionary<string, object> generateObjectNameDict(List<object> toolTipInptObjectList)
        {
            Dictionary<string, object> rtrnDict = new Dictionary<string, object>();

            foreach (object obj in toolTipInptObjectList)
            {
                Type objType = obj.GetType();

                if (objType == typeof(ToolStripButton))
                {
                    ToolStripButton toolStripButton = (ToolStripButton)obj;

                    rtrnDict.Add(toolStripButton.Name, toolStripButton);

                    continue;
                }

                if (objType == typeof(Button))
                {
                    Button button = (Button)obj;

                    //toolTip.SetToolTip(button, "");

                    rtrnDict.Add(button.Name, button);

                    continue;
                }

                if (objType == typeof(RadioButton))
                {
                    RadioButton radioButton = (RadioButton)obj;

                    //toolTip.SetToolTip(radioButton, "");

                    rtrnDict.Add(radioButton.Name, obj);

                    continue;
                }

                if (objType == typeof(CheckBox))
                {
                    CheckBox checkBox = (CheckBox)obj;

                    //toolTip.SetToolTip(checkBox, "");

                    rtrnDict.Add(checkBox.Name, checkBox);

                    continue;
                }
            }

            return rtrnDict;
        }

        private ToolTipObjectList generateToolTipObjectList()
        {
            List<ToolTipObject> rtrnList = new List<ToolTipObject>();

            ToolTipObject toolTipObject = null;

            foreach (object obj in objectDict.Values)
            {
                Type type = obj.GetType();

                if (type == typeof(ToolStripButton))
                {
                    ToolStripButton toolStripButton = (ToolStripButton)obj;

                    toolTipObject = new ToolTipObject(obj, toolStripButton.Name, toolStripButton.GetType().ToString(), toolStripButton.ToolTipText);

                    rtrnList.Add(toolTipObject);

                    continue;
                }

                if (type == typeof(Button))
                {
                    Button button = (Button)obj;

                    toolTipObject = new ToolTipObject(obj, button.Name, button.GetType().ToString(), toolTip.GetToolTip(button));

                    rtrnList.Add(toolTipObject);

                    continue;
                }

                if (type == typeof(RadioButton))
                {
                    RadioButton radioButton = (RadioButton)obj;

                    toolTipObject = new ToolTipObject(obj, radioButton.Name, radioButton.GetType().ToString(), toolTip.GetToolTip(radioButton));

                    rtrnList.Add(toolTipObject);

                    continue;
                }

                if (type == typeof(CheckBox))
                {
                    CheckBox checkBox = (CheckBox)obj;

                    toolTipObject = new ToolTipObject(obj, checkBox.Name, checkBox.GetType().ToString(), toolTip.GetToolTip(checkBox));

                    rtrnList.Add(toolTipObject);

                    continue;
                }

            }

            return new ToolTipObjectList(rtrnList);
        }

        private void loadToolTipDefinitions(string toolTipDefinitionsFilePath)
        {
            Dictionary<string, string> toolTipTextDict = new Dictionary<string, string>();

            ToolTipObjectList toolTipObjectListing = ToolTipObjectList.Deserialize(toolTipDefinitionsFilePath);

            foreach (ToolTipObject toolTipObject in toolTipObjectListing.ToolTipObjectListing)
            {
                string toolTip = toolTipObject.ToolTipText;
                if (!String.IsNullOrEmpty(toolTip))
                {
                    if (toolTip[0] == '$')
                    {
                        toolTip = ShortcutSettings.GetShortCutKey(toolTip.Substring(1));
                        if (!String.IsNullOrEmpty(toolTip))
                        {
                            toolTip = "Shortcut: " + toolTip;
                        }
                    }
                }
                toolTipTextDict.Add(toolTipObject.ObjectName, toolTip);
            }
          
            foreach (object obj in objectDict.Values)
            {
                Type objType = obj.GetType();

                if (objType == typeof(ToolStripButton))
                {
                    ToolStripButton toolStripButton = (ToolStripButton)obj;

                    string toolTipText = toolStripButton.ToolTipText;

                    if (toolTipTextDict.TryGetValue(toolStripButton.Name, out toolTipText))
                    {
                        toolStripButton.ToolTipText = toolTipText;
                    }

                    continue;
                }

                if (objType == typeof(Button))
                {
                    Button button = (Button)obj;

                    string toolTipText = toolTip.GetToolTip(button);

                    toolTipTextDict.TryGetValue(button.Name, out toolTipText);
                    
                    if (!string.IsNullOrEmpty(toolTipText))
                    {
                        toolTip.SetToolTip(button, toolTipText);
                    }

                    continue;
                }

                if (objType == typeof(RadioButton))
                {
                    RadioButton radioButton = (RadioButton)obj;

                    string toolTipText = toolTip.GetToolTip(radioButton);

                    if (toolTipTextDict.TryGetValue(radioButton.Name, out toolTipText))
                    {
                        toolTip.SetToolTip(radioButton, toolTipText);
                    }

                    continue;
                }

                if (objType == typeof(RadioButton))
                {
                    RadioButton radioButton = (RadioButton)obj;

                    string toolTipText = toolTip.GetToolTip(radioButton);

                    if (toolTipTextDict.TryGetValue(radioButton.Name, out toolTipText))
                    {
                        toolTip.SetToolTip(radioButton, toolTipText);
                    }

                    continue;
                }

                if (objType == typeof(CheckBox))
                {
                    CheckBox checkBox = (CheckBox)obj;

                    string toolTipText = toolTip.GetToolTip(checkBox);

                    if (toolTipTextDict.TryGetValue(checkBox.Name, out toolTipText))
                    {
                        toolTip.SetToolTip(checkBox, toolTipText);
                    }

                    continue;
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            string toolTipText = toolTip.GetToolTip((Button) sender);

            //toolTip.sho
        }

        public void SaveToolTipDefinitions()
        {
            ToolTipObjectList toolTipObjectList = generateToolTipObjectList();

            toolTipObjectList.Serialize(toolTipDefinitionsFilePath);
        }

        ToolTipSettingsForm toolTipSettingsForm = null;

        public void ShowToolTipEditForm()
        {
            if (toolTipSettingsForm is null)
            {
                toolTipSettingsForm = new ToolTipSettingsForm();
                toolTipSettingsForm.Init(this, objectDict.Values, toolTip);

                toolTipSettingsForm.FormClosed += ToolTipSettingsForm_FormClosed;
            }

            toolTipSettingsForm.WindowState = FormWindowState.Normal;
            toolTipSettingsForm.Show();
            toolTipSettingsForm.BringToFront();
        }

        private void ToolTipSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            toolTipSettingsForm = null;
        }
    }
}