namespace CanvasLib.Counters
{
    using System;
    using System.Drawing;
    using System.Xml.Serialization;

    public class Counter
    {
        public delegate void CounterColorChangedHandler(Counter counter, Color color);

        public event CounterColorChangedHandler CounterColorChanged;

        public delegate void CounterDisplaySizeChangedHandler(Counter counter, CounterDisplaySize counterSize);

        public event CounterDisplaySizeChangedHandler CounterDisplaySizeChanged;

        public delegate void CounterShowCircleChangedHandler(Counter counter, bool showCircle);

        public event CounterShowCircleChangedHandler CounterShowCircleChanged;

        public delegate void CounterCountChangedHandler(Counter counter, int count);

        public event CounterCountChangedHandler CounterCountChanged;

        public delegate void CounterDescriptionChangedHandler(Counter counter, string description);

        public event CounterDescriptionChangedHandler CounterDescriptionChanged;

        public delegate void CounterSizeChangedHandler(Counter counter, double size);

        public event CounterSizeChangedHandler CounterSizeChanged;

        public delegate void CounterFilteredChangedHandler(Counter counter, bool filtered);

        public event CounterFilteredChangedHandler CounterFilteredChanged;

        private bool _filtered = true;

        public bool Filtered
        {
            get
            {
                return _filtered;
            }

            set
            {
                if (_filtered == value)
                {
                    return;
                }

                _filtered = value;

                if (CounterFilteredChanged != null)
                {
                    CounterFilteredChanged.Invoke(this, _filtered);
                }
            }
        }

        [XmlIgnore]
        public Color Color
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

                if (CounterColorChanged != null)
                {
                    CounterColorChanged.Invoke(this, value);
                }
            }
        }

        public int A;
        public int R;
        public int G;
        public int B;

        public char Tag;
   
        public void ResetCounter(Color color)
        {
            _description = "";

            _size = 0.0;

            A = color.A;
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public int CounterIndex => (int)(Tag - 'A');

        private int _count = 0;

        public int Count
        {
            get
            {
                return _count;
            }

            set
            {
                if (_count == value)
                {
                    return;
                }

                _count = value;

                if (CounterCountChanged != null)
                {
                    CounterCountChanged.Invoke(this, _count);
                }
            }
        }

        private string _description = "";

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;

                if (CounterDescriptionChanged != null)
                {
                    CounterDescriptionChanged.Invoke(this, _description);
                }
            }
        }

        private double _size = 0;

        public double Size
        {
            get
            {
                return _size;
            }

            set
            {
                if (_size == value)
                {
                    return;
                }

                _size = value;

                if (CounterSizeChanged != null)
                {
                    CounterSizeChanged.Invoke(this, _size);
                }
            }
        }

        private CounterDisplaySize _counterDisplaySize = CounterDisplaySize.Medium;

        public CounterDisplaySize CounterDisplaySize
        {
            get
            {
                return _counterDisplaySize;
            }

            set
            {
                if (_counterDisplaySize == value)
                {
                    return;
                }

                _counterDisplaySize = value;

                if (CounterDisplaySizeChanged != null)
                {
                    CounterDisplaySizeChanged.Invoke(this, _counterDisplaySize);
                }
            }
        }

        private bool _showCircle = true;

        public bool ShowCircle
        {
            get
            {
                return _showCircle;
            }

            set
            {
                if (_showCircle == value)
                {
                    return;
                }

                _showCircle = value;

                if (CounterShowCircleChanged != null)
                {
                    CounterShowCircleChanged.Invoke(this, value);
                }
            }
        }

        public bool Show
        {
            get
            {
                return !Filtered;
            }

            set
            {
                Filtered = !value;
            }
        }

        public Counter()
        {
           
        }

        public Counter(int count, Color color, char tag, string description, double size, CounterDisplaySize counterSize, bool showCircle, bool show)
        {
            this.Count = count;
            
            this.A = this.Color.A;
            this.R = this.Color.R;
            this.G = this.Color.G;
            this.B = this.Color.B;

            this.Tag = tag;
            this.Description = description;
            this.Size = size;
            this.CounterDisplaySize = counterSize;
            this.ShowCircle = showCircle;
            this.Show = show;
        }

        internal Counter Clone()
        {
            Counter counter = new Counter(this.Count, this.Color, this.Tag, this.Description, this.Size, this.CounterDisplaySize, this.ShowCircle, this.Show);

            counter.A = this.A;
            counter.R = this.R;
            counter.G = this.G;
            counter.B = this.B;

            return counter;
        }

    }

    public enum CounterDisplaySize
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }
}
