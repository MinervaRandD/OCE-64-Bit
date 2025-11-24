namespace FloorMaterialEstimator.CanvasManager
{
    using System;
    using System.Collections.Generic;
    using FinishesLib;
    using MaterialsLayout;
    using OversUnders;
    using SettingsLib;
    using Graphics;
    using Utilities;

    public class RolloutStatsCalculator
    {
        private AreaFinishBase areaFinishBase;

        private GraphicsPage page;

        public List<MaterialArea> OversMaterialAreaList = new List<MaterialArea>();
        public List<MaterialArea> UndrsMaterialAreaList = new List<MaterialArea>();

        public RolloutStatsCalculator(GraphicsPage page, AreaFinishBase areaFinishBase)
        {
            this.page = page;
            this.areaFinishBase = areaFinishBase;
        }

        public double? CalculateRolloutStats(IEnumerable<CanvasLayoutArea> layoutAreas)
        {
            double totalNetAreaOfSeamedAreas = 0;
            double totalRollOutAreaOfSeamedAreas = 0;

            double scaleFactor = page.DrawingScaleInInches;

            OversMaterialAreaList.Clear();
            UndrsMaterialAreaList.Clear();

            double rollWidthInInches = areaFinishBase.RollWidthInInches;

            foreach (CanvasLayoutArea layoutArea in layoutAreas)
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

                totalNetAreaOfSeamedAreas += layoutArea.NetAreaInSqrInches();

                foreach (GraphicsCut graphicsCut in layoutArea.GraphicsCutList)
                {
                    //totalCutAreaOfSeamedAreas += canvasCut.Rollout.AreaInSqrInches(this.canvasManager.CurrentPage.DrawingScaleInInches);

                    List<MaterialArea> conditionedOverageList = null;// generateConditionedOverageList(index, canvasCut.OverageList);
                    List<MaterialArea> conditionedUndrageList = null;// generateConditionedUndrageList(index, canvasCut.UndrageList);

                    //OversMaterialAreaList.AddRange(conditionedOverageList);
                    //UndrsMaterialAreaList.AddRange(conditionedUndrageList);

                    //if (conditionedUndrageList.Count <= 0)
                    //{
                    //    totalRollOutAreaOfSeamedAreas += canvasCut.Rollout.AreaInSqrInches(scaleFactor);
                    //}
                }
            }

            if (totalNetAreaOfSeamedAreas <= 0)
            {
                //areaFinishBase.WastePercent = 0;

                areaFinishBase.RolloutLengthInInches = 0;

                return 0;
            }

            OversToUndrsMapper oversToUndersMapper = new OversToUndrsMapper(OversMaterialAreaList, UndrsMaterialAreaList);

            int l = oversToUndersMapper.GenerateWasteEstimate(rollWidthInInches);

            totalRollOutAreaOfSeamedAreas += (double)l * rollWidthInInches;

            double wp = totalRollOutAreaOfSeamedAreas / totalNetAreaOfSeamedAreas - 1.0;

            double totalRollOutLengthInFeet = totalRollOutAreaOfSeamedAreas / (rollWidthInInches * 12.0);
            double rollWidthInFeet = rollWidthInInches / 12.0;
            double totalRollOutLengthInInches = totalRollOutLengthInFeet * 12.0;

            areaFinishBase.RolloutLengthInInches = totalRollOutLengthInInches;


            return wp;
        }

        private List<MaterialArea> generateConditionedOverageList(int index, List<GraphicsOverage> overageList)
        {
            List<MaterialArea> rtrnList = new List<MaterialArea>();

            if (overageList is null)
            {
                return rtrnList;
            }

            double scaleFactor = page.DrawingScaleInInches;
            double rollWidthInInches = areaFinishBase.RollWidthInInches;


            foreach (GraphicsOverage overage in overageList)
            {
                double width = overage.EffectiveDimensions.Item1 * scaleFactor;
                double lngth = overage.EffectiveDimensions.Item2 * scaleFactor;

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

            double scaleFactor = page.DrawingScaleInInches;
            double rollWidthInInches = areaFinishBase.RollWidthInInches;


            foreach (GraphicsUndrage undrage in undrageList)
            {
                double width = undrage.EffectiveDimensions.Item1 * scaleFactor;
                double lngth = undrage.EffectiveDimensions.Item2 * scaleFactor;

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
