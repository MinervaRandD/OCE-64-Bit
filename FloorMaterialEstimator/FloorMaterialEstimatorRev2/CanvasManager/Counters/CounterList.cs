
namespace CanvasLib.Counters
{
    using System.Drawing;
    using System.IO;
    using System.Xml.Serialization;

    public class CounterList
    {
        public Counter[] Counters;

        public CounterList() { }

        private Color[] defaultColorArray = new Color[26]
        {
            Color.Red, Color.Green, Color.Blue, Color.Gold, Color.Cyan, Color.Magenta, Color.OrangeRed, Color.PaleGreen,
            Color.PaleGreen, Color.LightBlue, Color.Yellow, Color.Purple, Color.Brown, Color.Olive, Color.Olive, Color.Goldenrod,
            Color.Pink, Color.Turquoise, Color.Tomato, Color.Teal, Color.SpringGreen, Color.Silver, Color.SeaShell, Color.SandyBrown,
            Color.Salmon, Color.Sienna
        };

        public void Init(string inptFilePath)
        {
            if (string.IsNullOrWhiteSpace(inptFilePath))
            {
                InitDefault();

                return;
            }

            if (File.Exists(inptFilePath))
            {
                if (Load(inptFilePath))
                {
                    return;
                }
            }

            InitDefault();
        }

        public void InitDefault()
        {
            Counters = new Counter[26];

            for (int i = 0; i < 26; i++)
            {
                char tag = (char)((int)'A' + i);

                string description = "Counter-" + tag;

                Counter counter = new Counter(0, Color.Red, tag, description, true);

                counter.A = defaultColorArray[i].A;
                counter.R = defaultColorArray[i].R;
                counter.G = defaultColorArray[i].G;
                counter.B = defaultColorArray[i].B;

                Counters[i] = counter;
            }
        }

        public bool Load(string inptFilePath)
        {
            try
            {
                StreamReader sr = new StreamReader(inptFilePath);

                var serializer = new XmlSerializer(typeof(CounterList));

                CounterList counterList = (CounterList)serializer.Deserialize(sr);

                this.Counters = counterList.Counters;

                return true;
            }

            catch
            {
                return false;
            }
        }

        public void Save(string outpFilePath)
        {
            var serializer = new XmlSerializer(typeof(CounterList));

            StreamWriter sw = new StreamWriter(outpFilePath);

            CounterList counterList = this.Clone();

            foreach (Counter counter in counterList.Counters)
            {
                counter.Count = 0;
            }

            serializer.Serialize(sw, counterList);
        }

        private CounterList Clone()
        {
            CounterList counterList = new CounterList();

            counterList.Counters = new Counter[26];

            for (int i = 0; i < 26; i++)
            {
                counterList.Counters[i] = Counters[i].Clone();
            }

            return counterList;
        }

        public Counter this[int indx]
        {
            get
            {
                if (indx < 0 || indx >= 26)
                {
                    return null;
                }

                return Counters[indx];
            }
        }
    }
}
