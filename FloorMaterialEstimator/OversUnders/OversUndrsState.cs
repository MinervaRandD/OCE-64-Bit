using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OversUnders
{
    public class OversUndrsState
    {

        public SortedDictionary<int, MaterialArea> CurrOversbyWidthDict = new SortedDictionary<int, MaterialArea>();
        public SortedDictionary<int, MaterialArea> CurrUndrsByWidthDict = new SortedDictionary<int, MaterialArea>();

        public int UpperBound { get; set; }

        public int LowerBound { get; set; }

        public void GenUpperBound()
        {
            for (int o = 0; o < CurrOversbyWidthDict.Count; o++)
            {
                MaterialArea mo = CurrOversbyWidthDict.ElementAt(o).Value;

                for (int u = 0; u < CurrUndrsByWidthDict.Count; u++)
                {

                }
            }
        }

        public void GenLowerBound()
        {
            LowerBound = CurrUndrsByWidthDict.Values.Sum(u => u.Area()) - CurrOversbyWidthDict.Values.Sum(o => o.Area());
        }
    }
}
