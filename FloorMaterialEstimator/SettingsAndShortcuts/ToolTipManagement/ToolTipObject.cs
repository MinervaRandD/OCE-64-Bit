using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using System.Xml.Serialization;

namespace SettingsLib
{
    [Serializable]
    public class ToolTipObject
    {
        [XmlIgnore]
        public object BaseToolTipObject { get; set; } = null;

        public string ObjectName { get; set; } = string.Empty;

        public string ObjectType { get; set; }

        public string ToolTipText { get; set; } = string.Empty;

        public string ToolTipShortcut { get; set; } = string.Empty;

        public ToolTipObject() { }

        public ToolTipObject(
            object baseToolTipObject
            ,string objectName
            ,string objectType
            ,string toolTipText)
        {
            BaseToolTipObject = baseToolTipObject;
            ObjectType = objectType;
            ObjectName = objectName;
            ToolTipText = toolTipText;
        }

        public ToolTipObject(object baseObject)
        {
            
            ObjectType = baseObject.GetType().ToString();

            try
            {
                object prop = baseObject.GetType().GetProperties().Single(pi => pi.Name == "Name").GetValue(baseObject, null);

                if (Utilities.Utilities.IsNotNull(prop))
                {
                    ObjectName = prop.ToString();
                }
            }

            catch { }

            try
            {
                object toolTip = baseObject.GetType().GetProperties().Single(pi => pi.Name == "ToolTipText").GetValue(baseObject, null);

                if (Utilities.Utilities.IsNotNull(toolTip))
                {
                    ToolTipText = toolTip.ToString();
                }
            }

            catch { }
        }
    }
}
