namespace CanvasLib.Counters
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    public class Counter
    {
        public Color Color
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }
        }

        public int A;
        public int R;
        public int G;
        public int B;

        public int Count;
        public char Tag;
        public string Description;
        public bool Show;

        public Counter()
        {
           
        }

        public Counter(int count, Color color, char tag, string description, bool show)
        {
            this.Count = count;
            
            this.A = this.Color.A;
            this.R = this.Color.R;
            this.G = this.Color.G;
            this.B = this.Color.B;

            this.Tag = tag;
            this.Description = description;
            this.Show = show;
        }

        internal Counter Clone()
        {
            Counter counter = new Counter(this.Count, this.Color, this.Tag, this.Description, this.Show);

            counter.A = this.A;
            counter.R = this.R;
            counter.G = this.G;
            counter.B = this.B;

            return counter;
        }
    }
}
