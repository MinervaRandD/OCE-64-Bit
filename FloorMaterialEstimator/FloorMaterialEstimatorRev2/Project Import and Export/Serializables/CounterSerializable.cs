

namespace FloorMaterialEstimator
{
    using Geometry;
    
    using System.Drawing;
    using CanvasLib.Counters;
    using System.Xml.Serialization;
    using Graphics;

    public class CounterSerializable
    {
        public string Guid { get; set; }

        public Coordinate Center { get; set; }

        public double Radius { get; set; }

        public int ColorA { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }

        public char CounterTag { get; set; }

        public CounterDisplaySize CounterSize;

        [XmlIgnore]
        public Color Color
        {
            get
            {
                return Color.FromArgb(ColorA, ColorR, ColorG, ColorB);
            }
        }

        public CounterSerializable() { }

        public CounterSerializable(GraphicsCounter counter)
        {
            this.Guid = counter.Guid;

            this.Center = counter.Center;
            this.Radius = counter.Radius;

            this.ColorA = counter.CounterColor.A;
            this.ColorR = counter.CounterColor.R;
            this.ColorG = counter.CounterColor.G;
            this.ColorB = counter.CounterColor.B;

            this.CounterTag = counter.CounterTag;
            this.CounterSize = counter.CounterSize;

            this.Guid = counter.Guid;
        }

        public GraphicsCounter Deserialize(GraphicsWindow window, GraphicsPage page, CounterController counterController)
        {
            Counter counter = counterController.CounterList.Counters[this.CounterTag - 'A'];

            GraphicsCounter graphicsCounter = new GraphicsCounter(page, this.Center, counter, this.Guid);

            return graphicsCounter;
        }
    }
}
