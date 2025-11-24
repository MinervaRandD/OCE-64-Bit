using MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComboLib
{
    public class CutComboSet: List<GraphicsComboElem>
    {
        public uint MinIndex => this.Min(e => e.Index);

		public GraphicsCut GraphicsCut { get; set; }

		public CutComboSet(GraphicsCut graphicsCut)
        {
            GraphicsCut = graphicsCut;
        }
    }
}
