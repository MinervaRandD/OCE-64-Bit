using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SettingsLib
{
 
    [Serializable]
    public class ToolTipObjectList
    {
        public List<ToolTipObject> ToolTipObjectListing { get; set; } = new List<ToolTipObject>();

        public ToolTipObjectList() { }

        
        public ToolTipObjectList(List<ToolTipObject> toolTipObjectListing)
        {
            ToolTipObjectListing = new List<ToolTipObject>(toolTipObjectListing);
        }

        public bool Serialize(string toolTipDefinitionsFilePath)
        {
            StreamWriter serialWriter = null;

            try
            {
                serialWriter = new StreamWriter(toolTipDefinitionsFilePath);
            }

            catch
            {
                MessageBox.Show("Unable to create the default tool tips file: '" + toolTipDefinitionsFilePath + "'");
                return false;
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ToolTipObjectList));

            xmlSerializer.Serialize(serialWriter, this);

            serialWriter.Flush();
            serialWriter.Close();

            return true;

        }

        public static ToolTipObjectList Deserialize(string toolTipDefinitionsFilePath)
        {
            if (string.IsNullOrEmpty(toolTipDefinitionsFilePath))
            {
                return new ToolTipObjectList();
            }

            StreamReader serialReader = null;

            if (!File.Exists(toolTipDefinitionsFilePath))
            {
                return new ToolTipObjectList();
            }

            try
            {
                serialReader = new StreamReader(toolTipDefinitionsFilePath);
            }

            catch
            {
                return new ToolTipObjectList();
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ToolTipObjectList));

            ToolTipObjectList toolTipObjectList = (ToolTipObjectList)xmlSerializer.Deserialize(serialReader);

            serialReader.Close();

            return toolTipObjectList;

        }
    }
}
