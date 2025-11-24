using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialsLayout;
using Geometry;

namespace DebugSupport
{
    public static class DebugChecks
    {
        public static bool ValidateRolloutsAndCuts(GraphicsLayoutArea graphicsRolloutArea, double rollWidthInInches, double drawingScale)
        {
            if (graphicsRolloutArea.RolloutList is null)
            {
                return true;
            }

            foreach (Rollout rollout in graphicsRolloutArea.RolloutList)
            {
                if (!validateRollout(rollout, rollWidthInInches, drawingScale))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool validateRollout(Rollout rollout, double rollWidthInInches, double drawingScale)
        {
            if (Math.Abs(rollout.RolloutRectangle.Height * drawingScale - rollWidthInInches) >= 1e-1)
            {
                return false;
            }

            if (rollout.GraphicsCutList is null)
            {
                return true;
            }

            foreach (Cut cut in rollout.GraphicsCutList)
            {
                if (Math.Abs(cut.CutRectangle.Height * drawingScale - rollWidthInInches) >= 1e-1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
