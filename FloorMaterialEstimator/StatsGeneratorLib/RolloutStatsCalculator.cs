namespace StatsGeneratorLib
{
    using System;
    using System.Collections.Generic;
    using MaterialsLayout;
    using OversUnders;
    using SettingsLib;
    using Utilities;

    public class RolloutStatsCalculator
    {
        double RolloutLengthInInches;

        double drawingScaleInInches;
        double rollWidthInInches;

        public List<MaterialArea> OversMaterialAreaList = new List<MaterialArea>();
        public List<MaterialArea> UndrsMaterialAreaList = new List<MaterialArea>();

        public RolloutStatsCalculator(
            double drawingScaleInInches
            ,double rollWidthInInches)
        {
            this.drawingScaleInInches = drawingScaleInInches;
            this.rollWidthInInches = rollWidthInInches;
        }

        public void CalculateRolloutStats(IEnumerable<GraphicsLayoutArea> layoutAreas)
        {
            double totalNetAreaOfSeamedAreas = 0;
            double totalRollOutAreaOfSeamedAreas = 0;

            OversMaterialAreaList.Clear();
            UndrsMaterialAreaList.Clear();

            foreach (GraphicsLayoutArea layoutArea in layoutAreas)
            {
                if (layoutArea.IsSubdivided())
                {
                    continue;
                }

                if (!layoutArea.IsSeamed())
                {
                    continue;
                }

                int index = layoutArea.SeamAreaIndex;

                if (index <= 0)
                {
                    continue;
                }

                totalNetAreaOfSeamedAreas += layoutArea.AreaInSqrInches();

                foreach (GraphicsCut graphicsCut in layoutArea.GraphicsCutList)
                {
                    

                    List<MaterialArea> conditionedOverageList = null;// generateConditionedOverageList(index, canvasCut.OverageList);
                    List<MaterialArea> conditionedUndrageList = null;// generateConditionedUndrageList(index, canvasCut.GraphicsUndrageList);
                }
            }

            if (totalNetAreaOfSeamedAreas <= 0)
            {
                RolloutLengthInInches = 0;

                return;
            }

            OversToUndrsMapper oversToUndersMapper = new OversToUndrsMapper(OversMaterialAreaList, UndrsMaterialAreaList);

            int l = oversToUndersMapper.GenerateWasteEstimate(rollWidthInInches);

            totalRollOutAreaOfSeamedAreas += (double)l * rollWidthInInches;

            double wp = totalRollOutAreaOfSeamedAreas / totalNetAreaOfSeamedAreas - 1.0;

            double totalRollOutLengthInFeet = totalRollOutAreaOfSeamedAreas / (rollWidthInInches * 12.0);
            double rollWidthInFeet = rollWidthInInches / 12.0;
            double totalRollOutLengthInInches = totalRollOutLengthInFeet * 12.0;

            RolloutLengthInInches = totalRollOutLengthInInches;
        }

        private List<MaterialArea> generateConditionedOverageList(int index, List<GraphicsOverage> overageList)
        {
            List<MaterialArea> rtrnList = new List<MaterialArea>();

            if (overageList is null)
            {
                return rtrnList;
            }

            foreach (GraphicsOverage overage in overageList)
            {
                double width = overage.EffectiveDimensions.Item1 * drawingScaleInInches;
                double lngth = overage.EffectiveDimensions.Item2 * drawingScaleInInches;

                if (width > lngth)
                {
                    Utilities.Swap(ref width, ref lngth);
                }

                if (width < GlobalSettings.MinOverageWidthInInches || lngth < GlobalSettings.MinOverageLengthInInches)
                {
                    continue;
                }

                if (width > 0.5 * rollWidthInInches)
                {
                    continue;
                }

                width = Math.Round(width);
                lngth = Math.Round(lngth);


                MaterialArea materialArea = new MaterialArea(MaterialAreaType.Over, (int)width, (int)lngth)
                {
                    SeamAreaIndex = index
                };

                rtrnList.Add(materialArea);
            }

            return rtrnList;
        }

        private List<MaterialArea> generateConditionedUndrageList(int index, List<GraphicsUndrage> undrageList)
        {
            List<MaterialArea> rtrnList = new List<MaterialArea>();

            if (undrageList is null)
            {
                return rtrnList;
            }

            foreach (GraphicsUndrage undrage in undrageList)
            {
                double width = undrage.EffectiveDimensions.Item1 * drawingScaleInInches;
                double lngth = undrage.EffectiveDimensions.Item2 * drawingScaleInInches;

                if (width > lngth)
                {
                    Utilities.Swap(ref width, ref lngth);
                }

                if (width < GlobalSettings.MinOverageWidthInInches || lngth < GlobalSettings.MinOverageLengthInInches)
                {
                    continue;
                }

                if (width > 0.5 * rollWidthInInches)
                {
                    continue;
                }

                width = Math.Round(width);
                lngth = Math.Round(lngth);

                MaterialArea materialArea = new MaterialArea(MaterialAreaType.Undr, (int)width, (int)lngth)
                {
                    SeamAreaIndex = index
                };

                rtrnList.Add(materialArea);
            }

            return rtrnList;
        }
    }
}
