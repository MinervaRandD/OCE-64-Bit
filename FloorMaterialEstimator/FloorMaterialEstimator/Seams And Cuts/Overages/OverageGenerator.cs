using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator.Seams_And_Cuts
{
    public partial class SeamsAndCutsGenerator
    {
        public List<Overage> OverageList;

        public List<Overage> GenerateOverages(double overageWidthInInches)
        {
            if (this.CutList  == null)
            {
                GenerateCutList();
            }

            OverageList = new List<Overage>();

            foreach (Cut cut in CutList)
            {
                HorizontalOverageGenerator horizontalOverageGenerator = new HorizontalOverageGenerator(cut, overageWidthInInches);

                List<Overage> horizontalOverageList = horizontalOverageGenerator.GenerateOverages();

                OverageList.AddRange(horizontalOverageList);
            }

            return OverageList;
        }
    }
}
