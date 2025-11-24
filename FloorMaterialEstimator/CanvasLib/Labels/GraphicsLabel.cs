using Graphics;
using Geometry;
using SettingsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasLib.Labels
{
    public class GraphicsLabel : GraphicsCircle
    {
        public Label Label { get; private set; }

        public string LabelText { get { return Label.Text; } set { Label.Text = value; } }

        public double CircleSize;
        private double FontSizeInPts;

        public GraphicsLabel(Label label, GraphicsWindow window, GraphicsPage page)
            : base(window, page, label.Center, GlobalSettings.CounterMediumCircleRadius)
        {
            this.Label = label;
        }

        public void Draw()
        {
            ReLoadConstants();

            double quanta = .05 * (this.FontSizeInPts / 12.0);
            double length = (quanta * 3.0) + (quanta * LabelText.Length);
            double height = (quanta * 4.0);// * (this.FontSizeInPts / 12.0);

            if (Label.Container == LabelContainer.Circle)
            {
                Shape = Page.DrawCircle(Guid, Center, height, Label.Color);
            }
            else
            {
                double ulX = Center.X - length;
                double ulY = Center.Y + height;
                double lrX = Center.X + (length);
                double lrY = Center.Y - (height);

                Shape = Page.DrawRectangle(this, ulX, ulY, lrX, lrY);
                Shape.SetLineColor(Label.Color);
            }

            base.Guid = Shape.Guid;

            if (Label.Container == LabelContainer.None)
            {
                Shape.ShowShapeOutline(false);
            }
            else
            {
                Shape.ShowShapeOutline(true);
            }

            Shape.VisioShape.Data1 = "Label";

            Shape.SetShapeText(LabelText, Label.Color, this.FontSizeInPts);
            Shape.SetFill("0");
            Shape.SetLineWidth(1.5);
        }

        private void ReLoadConstants()
        {
            this.FontSizeInPts = Label.LabelSize;

        }

        //public void Delete()
        //{
        //    VisioInterop.DeleteShape(this.Shape);
        //}

    }
}
