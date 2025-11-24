using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OversUnders;
using OversUndersLib;
using Utilities;
using Geometry;
using MaterialsLayout;

namespace StatsGeneratorLib
{
    public class AreaStatsCalculator
    {
        double drawingScaleInInches;

        double rollWidthInInches;

        double netAreaInSqrInches;

        public double TotalGrossAreaInSquareFeet = 0;

        public double TotalNetAreaInSquareFeet = 0;

        public double WastePercent;

        public AreaStatsCalculator(
            double drawingScaleInInches
            , double rollWidthInInches
            , double netAreaInSqrInches)
        {
            this.drawingScaleInInches = drawingScaleInInches;
            this.rollWidthInInches = rollWidthInInches;
        }

        public void UpdateTileStats(
            double perimeterInInches
            ,double trimInInches
            ,double netAreaInSqrInches
            )
        {
            double perimeterInFeet = perimeterInInches / 12.0;

            double waste = perimeterInFeet * (trimInInches / 12.0);
            double netAreaInSqrFeet = netAreaInSqrInches / 144.0;

            TotalGrossAreaInSquareFeet = (netAreaInSqrFeet + waste) * 144.0;;

            if (netAreaInSqrFeet != 0)
            {
                WastePercent = TotalGrossAreaInSquareFeet / netAreaInSqrFeet - 1.0;
            }

            else
            {
                WastePercent = 0;
            }
        }

        public void UpdateRollStats(
            List<Cut> cutList
            , List<Undrage> undrageList
            , List<VirtualOverage> virtualOverageList
            , List<VirtualUndrage> virtualUndrageList
            , double smallFillElementWidthInInches
            , double cutExtraElement)
        {
           
            double rollWidthInFeet = Math.Round(rollWidthInInches / 12.0);

            int intRollWidthInInches = (int)Math.Round(rollWidthInInches);

            List<MaterialArea> oversMaterialList = new List<MaterialArea>();
            List<MaterialArea> undrsMaterialList = new List<MaterialArea>();

            generateMaterialsLists(cutList, undrageList, virtualOverageList, virtualUndrageList, oversMaterialList, undrsMaterialList);
         
            double totlFillLengthInFeet = 0;
            double largeWastePercent = 0;

            OversUndersMainProcessor oversUndersMainProcessor = new OversUndersMainProcessor(
                oversMaterialList
                , undrsMaterialList
                , intRollWidthInInches);

            double dTemp = 0.0;

            oversUndersMainProcessor.GetOUsOutput(out totlFillLengthInFeet, out largeWastePercent, out dTemp);

            double? totalLargeUndrageArea = dTemp;

            double? largeFillPiece = rollWidthInInches * (totlFillLengthInFeet / 12.0);

            double? largeFillWastePercent;

            if (largeFillPiece.HasValue && totalLargeUndrageArea.HasValue)
            {
                if (largeFillPiece.Value <= 0 && totalLargeUndrageArea.Value <= 0)
                {
                    largeFillPiece = null;
                    totalLargeUndrageArea = null;
                    largeFillWastePercent = null;

                }

                else if (totalLargeUndrageArea.Value > 0)
                {
                    largeFillWastePercent = largeFillPiece.Value / totalLargeUndrageArea.Value - 1.0;
                }


                else
                {
                    largeFillWastePercent = null;
                }
            }


            int fillWdthFeet = (int)Math.Floor(totlFillLengthInFeet);
            int fillWdthInch = (int)Math.Round(12.0 * (totlFillLengthInFeet - (double)fillWdthFeet));

            string fillWdthLbl = fillWdthFeet.ToString() + "' " + fillWdthInch.ToString() + '"';

            int fillHghtInch = intRollWidthInInches % 12;
            int fillHghtFeet = (intRollWidthInInches - fillHghtInch) / 12;

            string fillHghtLbl = fillHghtFeet.ToString() + "' " + fillHghtInch.ToString() + '"';

            setupTotals(cutList, smallFillElementWidthInInches, largeFillPiece, cutExtraElement);
        }

        private void setupTotals(
           List<Cut> cutList
         , double smallFillElementWidthInInches
         , double? largeFillPiece
         , double cutsExtraElement)
        {
            // The following has been changed in that it assumes that all cuts are equal to the roll width.

            //double averageWidthInFeet = 0;
            double averageCount = 0;

            double totalLengthInFeet = 0;

            foreach (Cut cut in cutList)
            {
                if (cut.Deleted)
                {
                    continue;
                }

                if (cut.ShapeHasBeenOverridden)
                {
                    //averageWidthInFeet += cut.OverrideCutRectangle.Width; // This is mixed up. Should be corrected.
                    averageCount++;
                    totalLengthInFeet += cut.OverrideCutRectangle.Height;
                }

                else
                {
                    //averageWidthInFeet += cut.MaterialWidth; // This is mixed up. Should be corrected.
                    averageCount++;
                    totalLengthInFeet += cut.CutRectangle.Width;
                }
            }

            if (largeFillPiece.HasValue)
            {
                totalLengthInFeet += largeFillPiece.Value / drawingScaleInInches / 12.0;
                //averageWidthInFeet += rollWidthInInches / drawingScaleInInches / 12.0;
                averageCount++;

            }

            totalLengthInFeet += cutsExtraElement / drawingScaleInInches / 12.0;

            double widthInFeet = rollWidthInInches / 12.0; // drawingScale * averageWidthInFeet;
            double lngthInFeet = drawingScaleInInches * totalLengthInFeet + smallFillElementWidthInInches / 12.0;

            int hghtInTotlInch = (int)Math.Round(lngthInFeet * 12.0);
            int wdthInTotlInch = (int)Math.Round(widthInFeet * 12.0);

            TotalGrossAreaInSquareFeet = (double)(hghtInTotlInch * wdthInTotlInch) / 144.0;

            double totlNetAreaInSquareFeet = 0;

            TotalNetAreaInSquareFeet = netAreaInSqrInches / 144.0;

            if (totlNetAreaInSquareFeet != 0)
            {
                WastePercent = TotalGrossAreaInSquareFeet / TotalNetAreaInSquareFeet - 1.0;
            }

            else
            {
                WastePercent = 0;
            }
        }

        private void generateMaterialsLists(
            List<Cut> cutList
            , List<Undrage> undrageList
            , List<VirtualOverage> virtualOverageList
            , List<VirtualUndrage> virtualUndrageList
            , List<MaterialArea> oversMaterialList
            , List<MaterialArea> undrsMaterialList)
        {
            if (Utilities.Utilities.IsNotNull(cutList))
            {
                foreach (Cut cut in cutList)
                {
                    if (Utilities.Utilities.IsNotNull(cut.OverageList))
                    {
                        foreach (Overage overage in cut.OverageList)
                        {
                            if (overage.Deleted)
                            {
                                continue;
                            }

                            int hght = 0;
                            int wdth = 0;

                            if (overage.ShapeHasBeenOverridden)
                            {
                                hght = (int)Math.Round(overage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                                wdth = (int)Math.Round(overage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                            }

                            else
                            {
                                hght = (int)Math.Round(overage.EffectiveDimensions.Item1 * drawingScaleInInches);
                                wdth = (int)Math.Round(overage.EffectiveDimensions.Item2 * drawingScaleInInches);
                            }

                            if (hght > 0 && wdth > 0)
                            {
                                MaterialArea m = new MaterialArea(MaterialAreaType.Over, wdth, hght);

                                oversMaterialList.Add(m);
                            }
                        }
                    }
                }
            }

            if (Utilities.Utilities.IsNotNull(undrageList))
            {
                foreach (Undrage undrage in undrageList)
                {
                    if (undrage.Deleted)
                    {
                        continue;
                    }

                    int hght = 0;
                    int wdth = 0;

                    if (undrage.ShapeHasBeenOverridden)
                    {
                        hght = (int)Math.Round(undrage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                        wdth = (int)Math.Round(undrage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    else
                    {
                        hght = (int)Math.Round(undrage.EffectiveDimensions.Item1 * drawingScaleInInches);
                        wdth = (int)Math.Round(undrage.EffectiveDimensions.Item2 * drawingScaleInInches);

                        wdth = (int)Math.Round(wdth + undrage.MaterialOverlap); // Added overlap amount in this case.

                        int halfWidth = (int)Math.Round(undrage.MaterialWidth * 0.5 * drawingScaleInInches);

                        if (wdth > halfWidth)
                        {
                            wdth = halfWidth;
                        }
                    }


                    if (hght > 0 && wdth > 0)
                    {
                        MaterialArea m = new MaterialArea(MaterialAreaType.Undr, wdth, hght);

                        undrsMaterialList.Add(m);
                    }
                }


            }

            if (Utilities.Utilities.IsNotNull(virtualOverageList))
            {
                foreach (VirtualOverage virtualOverage in virtualOverageList)
                {
                    int hght = 0;
                    int wdth = 0;

                    if (virtualOverage.ShapeHasBeenOverridden)
                    {
                        hght = (int)Math.Round(virtualOverage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                        wdth = (int)Math.Round(virtualOverage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    else
                    {
                        hght = (int)Math.Round(virtualOverage.EffectiveDimensions.Item1 * drawingScaleInInches);
                        wdth = (int)Math.Round(virtualOverage.EffectiveDimensions.Item2 * drawingScaleInInches);
                    }


                    if (hght > 0 && wdth > 0)
                    {
                        MaterialArea m = new MaterialArea(MaterialAreaType.Over, wdth, hght);

                        oversMaterialList.Add(m);
                    }
                }
            }

            if (Utilities.Utilities.IsNotNull(virtualUndrageList))
            {
                foreach (VirtualUndrage virtualUndrage in virtualUndrageList)
                {
                    int hght = 0;
                    int wdth = 0;

                    if (virtualUndrage.ShapeHasBeenOverridden)
                    {
                        hght = (int)Math.Round(virtualUndrage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                        wdth = (int)Math.Round(virtualUndrage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    else
                    {
                        hght = (int)Math.Round(virtualUndrage.EffectiveDimensions.Item1 * drawingScaleInInches);
                        wdth = (int)Math.Round(virtualUndrage.EffectiveDimensions.Item2 * drawingScaleInInches);
                    }


                    MaterialArea m = new MaterialArea(MaterialAreaType.Undr, wdth, hght);

                    undrsMaterialList.Add(m);
                }
            }

            oversMaterialList.Sort();
            undrsMaterialList.Sort();
        }

    }
}
