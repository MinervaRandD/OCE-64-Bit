using MaterialsLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutOversNestingLib
{
    public class CutSet: List<GraphicsCutElem>
    {
        public uint MinIndex => this.Min(e => e.Index);

		public GraphicsCut GraphicsCut { get; set; }

		public CutSet(GraphicsCut graphicsCut)
        {
            GraphicsCut = graphicsCut;
        }
    }
}
