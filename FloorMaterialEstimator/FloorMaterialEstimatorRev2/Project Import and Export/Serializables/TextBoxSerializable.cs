using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    using Graphics;

    class TextBoxSerializable
    {
        public string Text { get; set; }

        public string FontFamily { get; set; }

        public int FontSizeInPts { get; set; }

        public int FontAlign { get; set; }

        public int Font_R { get; set; }

        public int Font_G { get; set; }

        public int Font_B { get; set; }

        public int Font_A { get; set; }

        public int Fill_R { get; set; }

        public int Fill_G { get; set; }

        public int Fill_B { get; set; }

        public int Fill_A { get; set; }

        public int LineStyle { get; set; }

        public int LineWidth { get; set; }

        public int Line_R { get; set; }

        public int Line_G { get; set; }

        public int Line_B { get; set; }

        public int Line_A { get; set; }

        public TextBoxSerializable(GraphicsTextBox graphicsTextBox)
        {
            Text = graphicsTextBox.Text;

            FontFamily = graphicsTextBox.FontFamily;

            FontSizeInPts = graphicsTextBox.FontSizeInPts;

            Font_R = graphicsTextBox.FontColor.R;

            Font_G = graphicsTextBox.FontColor.G;

            Font_B = graphicsTextBox.FontColor.B;

            Font_A = graphicsTextBox.FontColor.A;

            FontAlign = graphicsTextBox.FontAlign;

            Fill_R = graphicsTextBox.FillColor.R;

            Fill_G = graphicsTextBox.FillColor.G;

            Fill_B = graphicsTextBox.FillColor.B;

            Fill_A = graphicsTextBox.FillColor.A;

            LineStyle = graphicsTextBox.LineStyle;

            LineWidth = graphicsTextBox.LineWidth;

            Line_R = graphicsTextBox.LineColor.R;

            Line_G = graphicsTextBox.LineColor.G;

            Line_B = graphicsTextBox.LineColor.B;

            Line_A = graphicsTextBox.LineColor.A;
        }

    }
}
