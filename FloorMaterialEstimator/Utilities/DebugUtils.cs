using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utilities
{
    public static class DebugUtils
    {
        public static bool ConditionTest(bool condition, string message, bool recoverable = false)
        {
#if !MONITORLEVEL1
            return true;
#endif

        }

        public static void ValidateNetVsGross(bool isSeamed, double netAreaInSqrInches, double? grossAreaInSqrInches)
        {
       
            if (!isSeamed)
            {
                return;
            }

            if (grossAreaInSqrInches == null)
            {
                return;
            }

            if (netAreaInSqrInches > grossAreaInSqrInches.Value)
            {
                MessageBoxAdv.Show("Gross area < net area", "Gross < Net", MessageBoxAdv.Buttons.OK, MessageBoxAdv.Icon.Error);
            }
        }
    }
}
