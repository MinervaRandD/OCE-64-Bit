//-------------------------------------------------------------------------------//
// <copyright file="AreaFinishBaseList.cs" company="Bruun Estimating, LLC">      // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using Globals;

namespace FinishesLib
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Diagnostics;
    using TracerLib;
    using Utilities;

    [Serializable]
    public class AreaFinishBaseList : List<AreaFinishBase>// IEnumerable<areaFinishBase>
    {
        public Dictionary<string, AreaFinishBase> AreaFinishByGuid = new Dictionary<string, AreaFinishBase>();

        #region Delegates and Events

        public delegate void ItemAddedEventHandler(AreaFinishBase item);

        public event ItemAddedEventHandler ItemAdded;

        public delegate void ItemInsertedEventHandler(AreaFinishBase item, int position);

        public List<Delegate> ItemAddedEventHandlerList()
        {
            List<Delegate> rtrnList = new List<Delegate>();

            if (ItemAdded == null)
            {
                return rtrnList;
            }

            foreach (Delegate del in ItemAdded.GetInvocationList())
            {
                rtrnList.Add(del);
            }

            return rtrnList;

        }
        public event ItemInsertedEventHandler ItemInserted;

        public delegate void ItemRemovedEventHandler(string guid, int position);

        public event ItemRemovedEventHandler ItemRemoved;

        public delegate void ItemsSwappedEventHandler(int position1, int position2);

        public event ItemsSwappedEventHandler ItemsSwapped;

        public delegate void ItemSelectedEventHandler(int itemIndex);

        public event ItemSelectedEventHandler ItemSelected;

        #endregion

        public void AddElem(AreaFinishBase areaFinishBase, int? selectPosition = null)
        {
            base.Add(areaFinishBase);

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(areaFinishBase);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }

            AreaFinishByGuid.Add(areaFinishBase.Guid, areaFinishBase);
            //AreaFinishByName.Add(areaFinishBase.AreaName, areaFinishBase);
        }

        public void InsertElem(AreaFinishBase areaFinishBase, int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position <= Count);

            if (position == Count)
            {
                AddElem(areaFinishBase);
                return;
            }

            base.Insert(position, areaFinishBase);

            if (ItemInserted != null)
            {
                 ItemInserted.Invoke(areaFinishBase, position);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }

            AreaFinishByGuid.Add(areaFinishBase.Guid, areaFinishBase);
            //AreaFinishByName.Add(areaFinishBase.AreaName, areaFinishBase);
        }

        public void RemoveElem(int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position < Count);

            AreaFinishBase areaFinishBase = base[position];

            base.RemoveAt(position);

            if (ItemRemoved != null)
            {
                ItemRemoved.Invoke(areaFinishBase.Guid, position);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }

            AreaFinishByGuid.Remove(areaFinishBase.Guid);
            //AreaFinishByName.Remove(areaFinishBase.AreaName);
        }

        public void SwapElems(int position1, int position2, int? selectPosition = null)
        {
            Debug.Assert(position1 >= 0 && position1 < Count);
            Debug.Assert(position2 >= 0 && position2 < Count);

            AreaFinishBase temp = base[position1];

            base[position1] = base[position2];
            base[position2] = temp;

            if (ItemsSwapped != null)
            {
                ItemsSwapped.Invoke(position1, position2);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }
        }

        public void SelectElem(int itemIndex)
        {
            //if (itemIndex == SelectedItemIndex)
            //{
            //    return;
            //}

            //SystemGlobals.SetAreaCopyAndPastState(false);

            SelectedItemIndex = itemIndex;

            if (ItemSelected != null)
            {
                ItemSelected.Invoke(itemIndex);
            }
        }

        public void SelectElem(AreaFinishBase finish)
        {
            SelectElem(this.IndexOf(finish));
        }

        public int SelectedItemIndex { get; set; } = 0;

        public AreaFinishBase SelectedItem => this[SelectedItemIndex];

        public bool Load(string inptFilePath, SeamFinishBaseList seamFinishBaseList)
        {
#if TRACE0
            Tracer.TraceGen.TraceMethodCall(1, false, new object[] { inptFilePath, seamFinishBaseList });
#endif

            if (!File.Exists(inptFilePath))
            {
                Tracer.TraceGen.TraceError("Input file does not exist in call to AreaFinishBaseList:Load.", 1, false);

                return false;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(AreaFinishBaseList));

            // TODO: Fix initial values on load.

            AreaFinishBaseList areaFinishBaseList = (AreaFinishBaseList)serializer.Deserialize(sr);

            areaFinishBaseList.ForEach(
                e => { if (string.IsNullOrEmpty(e.Guid)) e.Guid = GuidMaintenance.CreateGuid(this); else GuidMaintenance.AddGuid(e.Guid, this); });

            this.AddRange(areaFinishBaseList);

            this.ForEach(a => { AreaFinishByGuid.Add(a.Guid, a); /*AreaFinishByName.Add(a.AreaName, a); */});

            // At this point, link the seam base as deserialized to the seam finish base list. When serialized, the entire
            // seam is saved, but this is for debugging purposes. We link to the actual seam for consistency.

            foreach (AreaFinishBase areaFinishBase in this)
            {
                // Set all display types to square feet, regardless of how it is saved.


                areaFinishBase.AreaDisplayUnits = AreaDisplayUnits.SquareFeet;

                areaFinishBase.ClearTallyCounts();

                if (areaFinishBase.MaterialsType == MaterialsType.Rolls)
                {
                    if (areaFinishBase.SeamFinishBase is null)
                    {
                        continue;
                    }

                    string seamGuid = areaFinishBase.SeamFinishBase.Guid;

                    if (seamFinishBaseList.SeamFinishByGuid.ContainsKey(seamGuid))
                    {
                        areaFinishBase.SeamFinishBase = seamFinishBaseList.SeamFinishByGuid[seamGuid];

                        continue;
                    }

                    string seamName = areaFinishBase.SeamFinishBase.SeamName;

                    if (seamFinishBaseList.SeamFinishByName.ContainsKey(seamName))
                    {
                        areaFinishBase.SeamFinishBase = seamFinishBaseList.SeamFinishByName[seamName];

                        continue;
                    }

                    Tracer.TraceGen.TraceError("Unable to link seam to area finish " + areaFinishBase.AreaName + "in call to AreaFinishBaseList:Load.", 1, false);

                    areaFinishBase.SeamFinishBase = null;
                    //throw new Exception("Unable to link seam finish base into area finish base.");
                }

                else
                {
                    areaFinishBase.SeamFinishBase = null;
                }
            }

            return true;
        }

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(AreaFinishBaseList));

                StreamWriter sw = new StreamWriter(outpFilePath);

                serializer.Serialize(sw, this);

                sw.Flush();

                sw.Close();
            }

            catch
            {
                return false;
            }

            return true;
        }

        public bool ItemsFiltered()
        {
            return Exists(s => s.Filtered);
        }

        public int GetIndex(AreaFinishBase areaFinishBase)
        {
            return base.FindIndex(f => f == areaFinishBase);
        }

        public AreaFinishBaseList Clone(bool zeroOutValues = false)
        {
            AreaFinishBaseList clonedAreaFinishBaseList = new AreaFinishBaseList();

            foreach (AreaFinishBase areaFinishBase in this)
            {
                clonedAreaFinishBaseList.Add(areaFinishBase.Clone());

                if (zeroOutValues)
                {
                    areaFinishBase.Count = 0;
                    areaFinishBase.Cuts = false;
                }
            }

            return clonedAreaFinishBaseList;
        }

    }
}
