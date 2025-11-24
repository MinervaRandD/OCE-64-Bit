
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

    [Serializable]
    public class FinishSeamBase
    {
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

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
        public static FinishSeamBase DefaultFinishSeamLine = new FinishSeamBase()
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
    }

    [Serializable]
    public class FinishSeamBaseList : IEnumerable<FinishSeamBase>
    {
        public List<FinishSeamBase> SeamList { get; set; } = new List<FinishSeamBase>();

        [XmlIgnore]
        public int Count => SeamList.Count;

        [XmlIgnore]
        public FinishSeamBase this[int i]
        {
            get
            {
                return SeamList[i];
            }
        }

        public void Add(FinishSeamBase finishSeamBase)
        {
            this.SeamList.Add(finishSeamBase);
        }

        public bool Load(string inptFilePath)
        {
            if (!File.Exists(inptFilePath))
            {
                return false;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(FinishSeamBaseList));

            FinishSeamBaseList finishSeamLineList = (FinishSeamBaseList)serializer.Deserialize(sr);

            this.SeamList = finishSeamLineList.SeamList;

            return true;
        }

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(FinishSeamBaseList));

                StreamWriter sw = new StreamWriter(outpFilePath);

                serializer.Serialize(sw, this);
            }

            catch
            {
                return false;
            }

            return true;
        }

        public IEnumerator<FinishSeamBase> GetEnumerator()
        {
            return SeamList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return SeamList.GetEnumerator();
        }
    }
}
