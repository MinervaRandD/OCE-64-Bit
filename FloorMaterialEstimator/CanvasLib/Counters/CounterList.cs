namespace CanvasLib.Counters
{
    using Globals;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using TracerLib;
    using Utilities;

    [Serializable]
    public class CounterList
    {
        public const int CountersSize = 26;

        public Counter[] Counters;

        public CounterList() { }

        private Color[] defaultColorArray = new Color[CountersSize]
        {
            Color.Red, Color.Green, Color.Blue, Color.Gold, Color.Cyan, Color.Magenta, Color.OrangeRed, Color.PaleGreen,
            Color.PaleGreen, Color.LightBlue, Color.Yellow, Color.Purple, Color.Brown, Color.Olive, Color.Olive, Color.Goldenrod,
            Color.Pink, Color.Turquoise, Color.Tomato, Color.Teal, Color.SpringGreen, Color.Silver, Color.SeaShell, Color.SandyBrown,
            Color.Salmon, Color.Sienna
        };

        public void Init(string inptFilePath)
        {
            SystemState.LegendFormFirstLoad = false;

            if (string.IsNullOrWhiteSpace(inptFilePath))
            {
                InitDefault();

                return;
            }

            if (File.Exists(inptFilePath))
            {
                if (Load(inptFilePath))
                {
                    ClearDescriptionsSizes();

                    return;
                }
            }
            
            InitDefault();
        }

        private void ClearDescriptionsSizes()
        {
            foreach (var counter in Counters)
            {
                counter.Description = "";
                counter.Size = 0.0;
            }
        }

        public void ResetCounters()
        {
            for (int i = 0; i < CountersSize; i++)
            {
                Counters[i].ResetCounter(defaultColorArray[i]);
                Counters[i].Description = "";
                Counters[i].Size = 0.0;
            }
        }

        public void InitDefault()
        {
            Counters = new Counter[CountersSize];

            for (int i = 0; i < CountersSize; i++)
            {
                char tag = (char)((int)'A' + i);

                string description = "";

                Counter counter = new Counter(0, Color.Red, tag, description, 0.0, CounterDisplaySize.Medium, true, true);

                counter.A = defaultColorArray[i].A;
                counter.R = defaultColorArray[i].R;
                counter.G = defaultColorArray[i].G;
                counter.B = defaultColorArray[i].B;

                Counters[i] = counter;
            }
        }
        public void UpdateCountersList(CounterList countersList)
        {
            if (countersList != null)
            {
                for (int i = 0; i < CountersSize; i++)
                {
                    Counters[i].Description = countersList[i].Description;
                    Counters[i].Size = countersList[i].Size;
                    Counters[i].A = countersList[i].A;
                    Counters[i].R = countersList[i].R;
                    Counters[i].G = countersList[i].G;
                    Counters[i].B = countersList[i].B;
                }
            }
        }

        public bool Load(string inptFilePath)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { inptFilePath });
#endif
            try
            {
                StreamReader sr = new StreamReader(inptFilePath);

                var serializer = new XmlSerializer(typeof(CounterList));

                CounterList counterList = (CounterList)serializer.Deserialize(sr);

                this.Counters = counterList.Counters;

                return true;
            }

            catch(Exception ex)
            {
                Tracer.TraceGen.TraceException("CounterList:Load throws an exception.", ex, 1, true);

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

            sw.Close();
        }

        public CounterList Clone(bool zeroOutValue = false)
        {
            CounterList counterList = new CounterList();

            counterList.Counters = new Counter[CountersSize];

            for (int i = 0; i < CountersSize; i++)
            {
                counterList.Counters[i] = Counters[i].Clone();

                if (zeroOutValue)
                {
                    counterList.Counters[i].Count = 0;
                }
            }

            return counterList;
        }

        public Counter this[int indx]
        {
            get
            {
                if (indx < 0 || indx >= CountersSize)
                {
                    return null;
                }

                return Counters[indx];
            }
        }

        public Counter FindByDescriptionSize(string description, double sizeInches)
        {
            double sizeFeet = sizeInches / 12.0;
            Counter savedEmptyCounter = null;

            foreach (Counter counter in Counters)
            {
                if (counter.Description == description && counter.Size == sizeFeet)
                {
                    return counter;
                }

                if (savedEmptyCounter == null)
                {
                    if (string.IsNullOrEmpty(counter.Description) && (counter.Size == 0.0))
                    {
                        savedEmptyCounter = counter;
                    }
                }
            }

            return savedEmptyCounter;
        }
    }
}
