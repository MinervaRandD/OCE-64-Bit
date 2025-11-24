

namespace OversUnders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using SettingsLib;

    public class OversUnders
    {
        private List<MaterialArea> origOversbyWidthList = new List<MaterialArea>();
        private List<MaterialArea> origUndrsByWidthList = new List<MaterialArea>();

        public SortedDictionary<int, MaterialArea> CurrOversbyWidthDict = new SortedDictionary<int, MaterialArea>();
        public SortedDictionary<int, MaterialArea> CurrUndrsByWidthDict = new SortedDictionary<int, MaterialArea>();

        public Dictionary<int, int> widthPrioDict = new Dictionary<int, int>();
        // Settings

        private Dictionary<int, MaterialArea> materialAreaDict = new Dictionary<int, MaterialArea>();

        public OversUnders(List<Tuple<int, int>> overs, List<Tuple<int, int>> undrs)
        {
            overs.ForEach(o => {
                MaterialArea m = new MaterialArea(MaterialAreaType.Over, o.Item1, o.Item2);
                materialAreaDict.Add(m.MaterialAreaId, m);
                origOversbyWidthList.Add(m); });

            undrs.ForEach(u => {
                MaterialArea m = new MaterialArea(MaterialAreaType.Undr, u.Item1, u.Item2);
                materialAreaDict.Add(m.MaterialAreaId, m);
                origUndrsByWidthList.Add(m);
            });

        }

        public OversUnders(List<MaterialArea> oversList, List<MaterialArea> undrsList)
        {
            oversList.ForEach(o => {
                materialAreaDict.Add(o.MaterialAreaId, o);
                origOversbyWidthList.Add(o);
            });

            undrsList.ForEach(u => {
                materialAreaDict.Add(u.MaterialAreaId, u);
                origUndrsByWidthList.Add(u);
            });
        }

        public int GenerateWasteEstimate(int rollWidthInInches)
        {
            InitializeGeneration(rollWidthInInches);

            OversToUnders();

            int totlFill = fillCutsToUndrs(rollWidthInInches);

            return totlFill;
        }

        #region Setup

        public void InitializeGeneration(int rollWidthInInches)
        {
            LoadLists();

            setupPriorityList(rollWidthInInches);

            matchExactOversToUnders();

        }

        public void LoadLists()
        {
            origOversbyWidthList.ForEach(o => addConsolidated(CurrOversbyWidthDict, o));
            origUndrsByWidthList.ForEach(u => addConsolidated(CurrUndrsByWidthDict, u));

            removeZeroOverElements(CurrOversbyWidthDict);
            removeZeroUndrElements(CurrUndrsByWidthDict);
        }

        private void setupPriorityList(int rollWidthInInches)
        {
            SortedDictionary<int, List<int>> undrWidthPriorities = new SortedDictionary<int, List<int>>();

            foreach (int width in CurrUndrsByWidthDict.Keys)
            {
                int remainder = rollWidthInInches % width;

                List<int> prioList = null;

                if (undrWidthPriorities.ContainsKey(remainder))
                {
                    prioList = undrWidthPriorities[remainder];
                }

                else
                {
                    undrWidthPriorities[remainder] = prioList = new List<int>();
                }

                prioList.Add(width);
            }

            int prio = undrWidthPriorities.Count - 1;

            foreach (List<int> widthList in undrWidthPriorities.Values)
            {
                foreach (int width in widthList)
                {
                    widthPrioDict[width] = prio;
                }

                prio--;
            }

        }

        private void matchExactOversToUnders()
        {
            List<int> uwList = CurrUndrsByWidthDict.Keys.ToList();

            foreach (int w in uwList)
            {
                if (!CurrOversbyWidthDict.ContainsKey(w))
                {
                    continue;
                }

                MaterialArea undr = CurrUndrsByWidthDict[w];
                MaterialArea over = CurrOversbyWidthDict[w];

                if (over.LngthInInches == undr.LngthInInches)
                {
                    CurrOversbyWidthDict.Remove(w);
                    CurrUndrsByWidthDict.Remove(w);
                }

                else if (over.LngthInInches > undr.LngthInInches)
                {
                    MaterialArea m = new MaterialArea(MaterialAreaType.Over, over.WidthInInches, over.LngthInInches - undr.LngthInInches);

                    materialAreaDict.Add(m.MaterialAreaId, m);

                    m.parentMaterialAreaList.Add(over.MaterialAreaId);
                    m.parentMaterialAreaList.Add(undr.MaterialAreaId);

                    CurrOversbyWidthDict.Remove(w);
                    CurrUndrsByWidthDict.Remove(w);

                    CurrOversbyWidthDict.Add(w, m);
                }

                else // over.LngthInInches < undr.LngthInInches
                {
                    MaterialArea m = new MaterialArea(MaterialAreaType.Undr, undr.WidthInInches, undr.LngthInInches - over.LngthInInches);

                    materialAreaDict.Add(m.MaterialAreaId, m);

                    m.parentMaterialAreaList.Add(over.MaterialAreaId);
                    m.parentMaterialAreaList.Add(undr.MaterialAreaId);

                    CurrOversbyWidthDict.Remove(w);
                    CurrUndrsByWidthDict.Remove(w);

                    CurrUndrsByWidthDict.Add(w, m);
                }
            }
        }

        #endregion

        #region Overs to Unders

        public void OversToUnders()
        {
            while (CurrOversbyWidthDict.Count > 0 && CurrUndrsByWidthDict.Count > 0)
            {
                if (!DoOversToUndersCycle())
                {
                    break;
                }
            }
        }

        public bool DoOversToUndersCycle()
        {
            bool result = processRemainingOversToUnders();

            removeZeroOverElements(CurrOversbyWidthDict);
            removeZeroUndrElements(CurrUndrsByWidthDict);

            return result;
        }

        private void removeZeroOverElements(SortedDictionary<int, MaterialArea> materialsDict)
        {
            List<int> widthList = materialsDict.Where
                (e1 =>
                    e1.Value.WidthInInches < GlobalSettings.MinOverageWidthInInches)
                    .Select(e2 => e2.Key).ToList();

            foreach (int w in widthList)
            {
                materialsDict.Remove(w);
            }

            List<int> lngthList = materialsDict.Where
                (e1 => e1.Value.LngthInInches < GlobalSettings.MinOverageLengthInInches)
                .Select(e2 => e2.Key).ToList();

            foreach (int l in lngthList)
            {
                materialsDict.Remove(l);
            }
        }

        private void removeZeroUndrElements(SortedDictionary<int, MaterialArea> materialsDict)
        {


            List<int> widthList = materialsDict.Where
                (e1 =>
                    e1.Value.WidthInInches < GlobalSettings.MinUnderageWidthInInches)
                    .Select(e2 => e2.Key).ToList();

            foreach (int w in widthList)
            {
                materialsDict.Remove(w);
            }

            List<int> lngthList = materialsDict.Where
                (e1 =>
                    e1.Value.LngthInInches < GlobalSettings.MinUnderageLengthInInches)
                    .Select(e2 => e2.Key).ToList();

            foreach (int l in lngthList)
            {
                materialsDict.Remove(l);
            }
        }
        public int OversToUndersUsedOverWidth;

        public MaterialArea OversToUndersUsedOver;

        public int GenerateWasteEstimate(double rollWidthInInches)
        {
            throw new NotImplementedException();
        }

        public List<int> OversToUndersBestCombo;

        public List<MaterialArea> OversToUndersAppliedUnders = new List<MaterialArea>();

        private bool processRemainingOversToUnders()
        {
            OversToUndersUsedOver = null;

            if (CurrOversbyWidthDict.Count <= 0)
            {
                return false;
            }

            OversToUndersUsedOverWidth = getLowestQualifiedOverWidth();

            if (OversToUndersUsedOverWidth <= 0)
            {
                return false;
            }

            OversToUndersUsedOver = CurrOversbyWidthDict[OversToUndersUsedOverWidth];

            if (OversToUndersUsedOverWidth <= 0)
            {
                return false;
            }

            List<int> possibleComboItems = new List<int>();

            foreach (int undrWidth in CurrUndrsByWidthDict.Keys)
            {
                possibleComboItems.Add(undrWidth);
            }

            minGap = int.MaxValue;
            BestComboItems.Clear();

            List<int> buildingList = new List<int>();

            BestComboItems.Clear();

            genBestCombos(OversToUndersUsedOverWidth, 0, 0, buildingList, possibleComboItems);

            OversToUndersBestCombo = getBestCombo(BestComboItems);


#if DEBUG
            OversToUndersAppliedUnders.Clear();

            foreach (int elem in OversToUndersBestCombo)
            {
                OversToUndersAppliedUnders.Add(CurrUndrsByWidthDict[elem].Clone());
            }

#endif
            int maxUndrLength = getMaxAppliedLength(OversToUndersBestCombo);
            int maxOverLength = OversToUndersUsedOver.LngthInInches;

            int deductLength = Math.Min(maxUndrLength, maxOverLength);

            foreach (int width in OversToUndersBestCombo)
            {
                MaterialArea underArea = CurrUndrsByWidthDict[width];

                int undrLength = underArea.LngthInInches;

                if (undrLength <= deductLength)
                {
                    CurrUndrsByWidthDict.Remove(width);
                }

                else
                {
                    underArea.LngthInInches -= deductLength;
                }
            }
            
            CurrOversbyWidthDict.Remove(OversToUndersUsedOverWidth);
           
            return true;
        }

        List<int> getBestCombo(List<List<int>> comboItems)
        {
            Debug.Assert(comboItems.Count > 0);

            if (comboItems.Count == 1)
            {
                return comboItems[0];
            }

            double maxPrio = 0;

            List<int> bestCombo = null;

            foreach (List<int> combo in comboItems)
            {
                double nextPrio = GetPriority(combo);

                if (nextPrio > maxPrio)
                {
                    maxPrio = nextPrio;
                    bestCombo = combo;
                }
            }

            return bestCombo;
        }


        private int getLowestQualifiedOverWidth()
        {
            if (CurrOversbyWidthDict.Count <= 0)
            {
                return 0;
            }

            if (CurrUndrsByWidthDict.Count <= 0)
            {
                return 0;
            }

            int undrWidth = CurrUndrsByWidthDict.ElementAt(0).Key;

            foreach (int width in CurrOversbyWidthDict.Keys)
            {
                if (width >= undrWidth)
                {
                    return width;
                }
            }

            return 0;
        }

        public int TotalUnderage()
        {
            return CurrUndrsByWidthDict.Values.Select(u => u.Area()).Sum();
        }

        private bool getNextCombo(ref int oW, ref int uW)
        {
            if (CurrOversbyWidthDict.Count <= 0)
            {
                return false;
            }

            for (int i = 0; i < CurrOversbyWidthDict.Count; i++)
            {
                oW = CurrOversbyWidthDict.ElementAt(i).Key;

                if (getNextCombo(oW, ref uW))
                {
                    return true;
                }
            }

            return false;
        }

        private bool getNextCombo(int oW, ref int uW)
        {
            if (CurrUndrsByWidthDict.Count <= 0)
            {
                return false;
            }

            if (CurrUndrsByWidthDict.ElementAt(0).Key > oW)
            {
                return false;
            }

            for (int i = 1; i < CurrUndrsByWidthDict.Count; i++)
            {
                if (CurrUndrsByWidthDict.ElementAt(i).Key > oW)
                {
                    uW = CurrUndrsByWidthDict.ElementAt(i - 1).Key;

                    return true;
                }
            }

            return false;
        }

        private void addConsolidated(SortedDictionary<int, MaterialArea> materialAreaDict, MaterialArea materialArea)
        {
            if (!materialAreaDict.ContainsKey(materialArea.WidthInInches))
            {
                materialAreaDict[materialArea.WidthInInches] = materialArea.Clone();
            }

            else
            {
                materialAreaDict[materialArea.WidthInInches].LngthInInches += materialArea.LngthInInches;
                materialAreaDict[materialArea.WidthInInches].parentMaterialAreaList.Add(materialArea.MaterialAreaId);
            }
        }

        #endregion

        #region Fill Cuts to Unders

        private int fillCutsToUndrs(int rollWidthInInches)
        {
            int fillWidthInInches = 0;

            while (CurrUndrsByWidthDict.Count > 0)
            {
                int maxWidth = DoFillCutsToUndrsCycle(rollWidthInInches);

                // MDD Important -- there is a bug here that needs to be corrected. This kludge avoids infinite cycling when maxWidth == 0
                if (maxWidth <= 0)
                {
                    break;
                }

                fillWidthInInches += maxWidth;

                //fillWidthInInches += DoFillCutsToUndrsCycle(rollWidthInInches);
            }

            return fillWidthInInches;
        }

        List<int> FillsToUndersBestCombo;

        public int BestFillToUndrWidth;

        public int DoFillCutsToUndrsCycle(int rollWidthInInches)
        {
            if (CurrUndrsByWidthDict.Count <= 0)
            {
                return 0;
            }

            BestFillToUndrWidth = getHighestPriorityWidth();

            List<int> buildingList = new List<int>();

            List<int> possibleCombinations = new List<int>(CurrUndrsByWidthDict.Keys);

            BestComboItems.Clear();

            genBestCombos(rollWidthInInches - BestFillToUndrWidth, 0, 0, buildingList, possibleCombinations);

            if (BestComboItems.Count <= 0)
            {
                List<int> comboItem = new List<int>();

                comboItem.Add(BestFillToUndrWidth);

                BestComboItems = new List<List<int>>();

                BestComboItems.Add(comboItem);
            }

            else
            {
                foreach (List<int> comboItem in BestComboItems)
                {
                    comboItem.Insert(0, BestFillToUndrWidth);
                }
            }

            FillsToUndersBestCombo = getBestCombo(BestComboItems);

#if DEBUG
            OversToUndersAppliedUnders.Clear();

            foreach (int elem in FillsToUndersBestCombo)
            {
                OversToUndersAppliedUnders.Add(CurrUndrsByWidthDict[elem].Clone());
            }

#endif
            int maxLength = getMaxAppliedLength(FillsToUndersBestCombo);

            foreach (int elem in FillsToUndersBestCombo)
            {
                if (!CurrUndrsByWidthDict.ContainsKey(elem))
                {
                    continue;
                }

                MaterialArea underArea = CurrUndrsByWidthDict[elem];

                int undrLength = underArea.LngthInInches;

                if (undrLength <= maxLength)
                {
                    CurrUndrsByWidthDict.Remove(elem);
                }

                else
                {
                    underArea.LngthInInches -= maxLength;
                }
            }

            return maxLength;
        }

        private int getHighestPriorityWidth()
        {
            int minPrio = int.MaxValue;
            int minWdth = int.MaxValue;

            foreach (int width in CurrUndrsByWidthDict.Keys)
            {
                int nextPrio = widthPrioDict[width];

                if (nextPrio < minPrio)
                {
                    minPrio = nextPrio;
                    minWdth = width;
                }
            }

            return minWdth;
        }

        #endregion

        #region Supporting Routines

        public double GetPriority(List<int> widthList)
        {
            double priority = 0;

            foreach (int width in widthList)
            {
                priority += Math.Pow(10, (double)-widthPrioDict[width]);
            }

            return priority / 10.0;
        }

        private int minGap;

        public List<List<int>> BestComboItems = new List<List<int>>();

        private void genBestCombos(int overWidth, int sum, int indx, List<int> buildingList, List<int> possibleComboItems)
        {
            Debug.Assert(sum <= overWidth);

            if (!buildNext(overWidth, sum, indx, buildingList, possibleComboItems))
            {
                int gap = overWidth - sum;

                if (gap < minGap)
                {
                    minGap = gap;

                    BestComboItems.Clear();

                    BestComboItems.Add(new List<int>(buildingList));
                }

                else if (gap == minGap)
                {
                    BestComboItems.Add(new List<int>(buildingList));
                }

                return;
            }

            for (int indx1 = indx; indx1 < possibleComboItems.Count; indx1++)
            {
                int sum1 = sum + possibleComboItems[indx1];

                if (sum1 > overWidth)
                {
                    return;
                }

                buildingList.Add(possibleComboItems[indx1]);

                genBestCombos(overWidth, sum1, indx1, buildingList, possibleComboItems);

                buildingList.RemoveAt(buildingList.Count - 1);
            }
        }

        private bool buildNext(int overWidth, int sum, int indx, List<int> buildingList, List<int> possibleComboItems)
        {
            int sum1 = sum + possibleComboItems[indx];

            if (sum1 <= overWidth)
            {

                return true;
            }

            else
            {
                return false;
            }
        }

        private int getMaxAppliedLength(List<int> combo)
        {
            Dictionary<int, int> comboFreqDict = new Dictionary<int, int>();

            foreach (int elem in combo)
            {
                if (comboFreqDict.ContainsKey(elem))
                {
                    comboFreqDict[elem]++;
                }

                else
                {
                    comboFreqDict[elem] = 1;
                }
            }

            int maxLength = int.MinValue;

            foreach (KeyValuePair<int, int> kvp in comboFreqDict)
            {
                int length = (int) Math.Round((double) CurrUndrsByWidthDict[kvp.Key].LngthInInches / (double) kvp.Value);

                if (length > maxLength)
                {
                    maxLength = length;
                }
            }

            return maxLength;
        }

        #endregion

        public delegate void CycleCompletedEventHandler(
            SortedDictionary<int, MaterialArea> currOversbyWidthDict,
            SortedDictionary<int, MaterialArea> currUndrsByWidthDict);

        public event CycleCompletedEventHandler CycleCompleted;
    }
}
