
namespace FloorMaterialEstimator
{
    using ComboLib;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ComboPathElement
    {
        public GraphicsComboElem GraphicsComboElem { get; set; }

        public double Offset
        {
            get;
            set;
        }
        
        public double EndpointOffset
        {
            get;
            set;
        }

        public ComboPathElement(GraphicsComboElem graphicsComboElem, double offset, double fullOffset)
        {
            this.GraphicsComboElem = graphicsComboElem;
            this.Offset = offset;
            this.EndpointOffset = fullOffset;
        }

        public double Length => GraphicsComboElem.GraphicsDirectedPolygon.Length;

    }
}
