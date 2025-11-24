//-------------------------------------------------------------------------------//
// <copyright file="SeamFinishBase.cs" company="Bruun Estimating, LLC">          // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

namespace FinishesLib
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using Utilities;
    using System.Diagnostics;

    [Serializable]
    public class SeamFinishBaseList : List<SeamFinishBase>
    {
        public Dictionary<string, SeamFinishBase> SeamFinishByGuid = new Dictionary<string, SeamFinishBase>();
        public Dictionary<string, SeamFinishBase> SeamFinishByName = new Dictionary<string, SeamFinishBase>();

        #region Delegates and Events

        public delegate void ItemAddedEventHandler(SeamFinishBase item);

        public event ItemAddedEventHandler ItemAdded;

        public delegate void ItemInsertedEventHandler(SeamFinishBase item, int position);

        public event ItemInsertedEventHandler ItemInserted;

        public delegate void ItemRemovedEventHandler(string guid, int position);

        public event ItemRemovedEventHandler ItemRemoved;

        public delegate void ItemsSwappedEventHandler(int position1, int position2);

        public event ItemsSwappedEventHandler ItemsSwapped;

        public delegate void ItemSelectedEventHandler(int itemIndex);

        public event ItemSelectedEventHandler ItemSelected;

        #endregion
        public bool Load(string inptFilePath)
        {
            if (!File.Exists(inptFilePath))
            {
                return false;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(SeamFinishBaseList));

            SeamFinishBaseList finishSeamLineList = (SeamFinishBaseList)serializer.Deserialize(sr);

            finishSeamLineList.ForEach(
                e => { if (string.IsNullOrEmpty(e.Guid)) e.Guid = GuidMaintenance.CreateGuid(this); else GuidMaintenance.AddGuid(e.Guid, this); });

            this.AddRange(finishSeamLineList);

            this.ForEach(s => { SeamFinishByGuid.Add(s.Guid, s); SeamFinishByName.Add(s.SeamName, s); });

            return true;
        }

        public void AddElem(SeamFinishBase seamFinishBase, int? selectPosition = null)
        {
            base.Add(seamFinishBase);

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(seamFinishBase);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }
        }

        public void InsertElem(SeamFinishBase seamFinishBase, int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position <= Count);

            if (position == Count)
            {
                AddElem(seamFinishBase);
                return;
            }

            base.Insert(position, seamFinishBase);

            if (ItemInserted != null)
            {
                ItemInserted.Invoke(seamFinishBase, position);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }
        }

        public void RemoveElem(int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position < Count);

            SeamFinishBase seamFinishBase = base[position];

            base.RemoveAt(position);

            if (ItemRemoved != null)
            {
                ItemRemoved.Invoke(seamFinishBase.Guid, position);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }
        }

        public void SwapElems(int position1, int position2, int? selectPosition = null)
        {
            Debug.Assert(position1 >= 0 && position1 < Count);
            Debug.Assert(position2 >= 0 && position2 < Count);

            SeamFinishBase temp = base[position1];

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
            SelectedItemIndex = itemIndex;

            if (ItemSelected != null)
            {
                ItemSelected.Invoke(itemIndex);
            }
        }

        public void SelectElem(SeamFinishBase seam)
        {
            SelectElem(this.IndexOf(seam));
        }

        public int SelectedItemIndex { get; set; } = 0;

        public SeamFinishBase SelectedItem => this[SelectedItemIndex];

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(SeamFinishBaseList));

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

        public SeamFinishBaseList Clone(bool zeroOutValues = false)
        {
            SeamFinishBaseList clonedSeamFinishBaseList = new SeamFinishBaseList();

            foreach (SeamFinishBase seamFinishBase in this)
            {
                clonedSeamFinishBaseList.Add(seamFinishBase.Clone());

                if (zeroOutValues)
                {
                    seamFinishBase.LengthInInches = 0;
                }
            }

            return clonedSeamFinishBaseList;
        }
    }
}
