

namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using System.Collections;
    using Utilities;

    [Serializable]
    public class SeamFinishBase
    {
        public string Guid { get; set; }

        public string SeamName { get; set; }

        public int VisioDashType { get; set; }

        public double LineWidthInPts { get; set; }

        // Color specification

        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        [XmlIgnore]
        public Color SeamColor
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }

            set
            {
                A = value.A;
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public string Product { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        [XmlIgnore]
        public static SeamFinishBase DefaultFinishSeamLine = new SeamFinishBase()
        {
            SeamName = "",
            VisioDashType = 1,
            LineWidthInPts = 2,
            A = Color.Cyan.A,
            R = Color.Cyan.R,
            G = Color.Cyan.G,
            B = Color.Cyan.B
        };

        [XmlIgnore]
        public static Color DefaultLineColor = Color.Cyan;

        [XmlIgnore]
        public static double DefaultLineWidthInPts = 2;

        [XmlIgnore]
        public static short DefaultVisioDashType = 1;

        internal SeamFinishBase Clone()
        {
            SeamFinishBase clonedFinishSeamBase = new SeamFinishBase()
            {
                SeamName = this.SeamName,
                VisioDashType = this.VisioDashType,
                LineWidthInPts = this.LineWidthInPts,
                SeamColor = this.SeamColor,
                Product = this.Product,
                Notes = this.Notes
            };

            clonedFinishSeamBase.Guid = GuidMaintenance.CreateGuid(clonedFinishSeamBase);

            return clonedFinishSeamBase;
        }
    }

    [Serializable]
    public class SeamFinishBaseList: List<SeamFinishBase>
    {
        public bool Load(string inptFilePath)
        {
            if (!File.Exists(inptFilePath))
            {
                return false;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(SeamFinishBaseList));

            SeamFinishBaseList finishSeamLineList = (SeamFinishBaseList)serializer.Deserialize(sr);

            finishSeamLineList.ForEach(
                e => { if (string.IsNullOrEmpty(e.Guid)) e.Guid = GuidMaintenance.CreateGuid(this); else GuidMaintenance.AddGuid(e.Guid, this); });

            this.AddRange(finishSeamLineList);

            return true;
        }

        public void Swap(int indx1, int indx2)
        {
            SeamFinishBase sfb1 = this[indx1];
            SeamFinishBase sfb2 = this[indx2];

            this[indx2] = sfb1;
            this[indx1] = sfb2;
        }

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SeamFinishBaseList));

                StreamWriter sw = new StreamWriter(outpFilePath);

                serializer.Serialize(sw, this);
            }

            catch
            {
                return false;
            }

            return true;
        }
    }
}
