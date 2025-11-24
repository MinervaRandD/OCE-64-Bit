using CanvasLib.Markers_and_Guides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class FieldGuideSerializable
    {
        public string Guid { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public FieldGuideSerializable() { }

        public FieldGuideSerializable(FieldGuide fieldGuide)
        {
            this.Guid = fieldGuide.Guid;

            this.X = fieldGuide.X;
            this.Y = fieldGuide.Y;
        }
    }
}
