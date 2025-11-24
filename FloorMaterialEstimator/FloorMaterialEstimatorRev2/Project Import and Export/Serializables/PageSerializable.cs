

namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.CanvasManager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PageSerializable
    {
        public double PageHeight { get; set; }
        public double PageWidth { get; set; }

        public double DrawingScaleInInches { get; set; }


        public bool ScaleHasBeenSet { get; set; }
        public byte[] DrawingInBytes { get; set; }

        public PageSerializable() { }

        public PageSerializable(CanvasPage page)
        {
            this.PageHeight = page.PageHeight;
            this.PageWidth = page.PageWidth;

            this.DrawingScaleInInches = page.DrawingScaleInInches;

            this.ScaleHasBeenSet = page.ScaleHasBeenSet;

            this.DrawingInBytes = page.DrawingInBytes;
        }
    }
}
