using Geometry;
using SettingsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CanvasLib.Labels
{
    public class Label
    {
        public string Guid { get; set; }

        public Coordinate Center { get; set; }

        public double Radius { get; set; }

        public int ColorA { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }

        public string Text { get; set; }

        public double LabelSize;

        public LabelContainer Container { get; set; }

        [XmlIgnore]
        public Color Color
        {
            get
            {
                return Color.FromArgb(ColorA, ColorR, ColorG, ColorB);
            }
        }
        public Label() 
        {
            Text = "1A";

            Color color = GlobalSettings.CutIndexFontColor;

            ColorA = color.A;
            ColorR = color.R;
            ColorG = color.G;
            ColorB = color.B;

            LabelSize = GlobalSettings.CutIndexFontInPts;
            Container = LabelContainer.Circle;
        }

        public Label(Coordinate coordinate, double radius, Color color, string text, double labelSize, LabelContainer container)
        {
            this.Center = coordinate;
            this.Radius = radius;
            this.ColorA = color.A;
            this.ColorR = color.R;
            this.ColorG = color.G;
            this.ColorB = color.B;
            this.Text = text;
            this.LabelSize = labelSize;
            this.Container = container;
        }

        public Label(Label label, Coordinate coordinate)
        {
            this.Center = coordinate;
            this.Radius = label.Radius;
            this.ColorA = label.ColorA;
            this.ColorR = label.ColorR;
            this.ColorG = label.ColorG;
            this.ColorB = label.ColorB;
            this.Text = label.Text;
            this.LabelSize = label.LabelSize;
            this.Container = label.Container;
        }

        public void CopyFrom(Label label)
        {
            this.Radius = label.Radius;
            this.ColorA = label.ColorA;
            this.ColorR = label.ColorR;
            this.ColorG = label.ColorG;
            this.ColorB = label.ColorB;
            this.Text = label.Text;
            this.LabelSize = label.LabelSize;
            this.Container = label.Container;
        }
    }

    public enum LabelContainer
    {
        None = 1,
        Circle = 2,
        Rectangle = 3
    }
}
