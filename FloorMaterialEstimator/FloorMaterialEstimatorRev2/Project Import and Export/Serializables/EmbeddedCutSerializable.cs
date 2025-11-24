

namespace FloorMaterialEstimator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geometry;
    using MaterialsLayout;

    public class EmbeddedCutSerializable
    {
        public RectangleSerializable CutRectangle { get; set; }

        public EmbeddedCutSerializable() { }

        public EmbeddedCutSerializable(EmbeddedCut cut)
        {
            this.CutRectangle = new RectangleSerializable(cut.CutRectangle);
            
        }

        public EmbeddedCut Deserialize()
        {
            Coordinate upperLeft = this.CutRectangle.UpperLeft.Deserialize();
            Coordinate lowerRght = this.CutRectangle.LowerRght.Deserialize();

            return new EmbeddedCut(new Rectangle(upperLeft, lowerRght));
        }

    }
}
