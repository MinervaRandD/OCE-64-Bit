//-------------------------------------------------------------------------------//
// <copyright file="LineFinishBaseList.cs" company="Bruun Estimating, LLC">      // 
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
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Utilities;

    [Serializable]
    public class LineFinishBaseList : List<LineFinishBase>
    {
        public Dictionary<string, LineFinishBase> LineFinishByGuid = new Dictionary<string, LineFinishBase>();

        public delegate void ItemAddedEventHandler(LineFinishBase item);

        public event ItemAddedEventHandler ItemAdded;

        #region Delegates and Events

        public delegate void ItemInsertedEventHandler(LineFinishBase item, int position);

        public event ItemInsertedEventHandler ItemInserted;

        public delegate void ItemRemovedEventHandler(string guid, int position);

        public event ItemRemovedEventHandler ItemRemoved;

        public delegate void ItemsSwappedEventHandler(int position1, int position2);

        public event ItemsSwappedEventHandler ItemsSwapped;

        public delegate void ItemSelectedEventHandler(int itemIndex);

        public event ItemSelectedEventHandler ItemSelected;

        #endregion

        public void Add(LineFinishBase lineFinishBase, int? selectPosition = null)
        {
            base.Add(lineFinishBase);

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(lineFinishBase);
            }

            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }

        public void Insert(LineFinishBase lineFinishBase, int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position <= Count);

            if (position == Count)
            {
                Add(lineFinishBase);
                return;
            }

            base.Insert(position, lineFinishBase);

            if (ItemInserted != null)
            {
                ItemInserted.Invoke(lineFinishBase, position);
            }

            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }

        public void Remove(int position, int? selectPosition = null)
        {
           
            Debug.Assert(position >= 0 && position < Count);

            LineFinishBase lineFinishBase = base[position];

            base.RemoveAt(position);

            if (ItemRemoved != null)
            {
                ItemRemoved.Invoke(lineFinishBase.Guid, position);
            }

            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }

        public void Swap(int position1, int position2, int? selectPosition = null)
        {
            Debug.Assert(position1 >= 0 && position1 < Count);
            Debug.Assert(position2 >= 0 && position2 < Count);

            LineFinishBase temp = base[position1];

            base[position1] = base[position2];
            base[position2] = temp;

            if (ItemsSwapped != null)
            {
                ItemsSwapped.Invoke(position1, position2);
            }

            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }


        public void Select(int itemIndex)
        {
            if (itemIndex == SelectedItemIndex)
            {
                return;
            }

            SelectedItemIndex = itemIndex;

            if (ItemSelected != null)
            {
                ItemSelected.Invoke(itemIndex);
            }
        }

        public void SelectElem(int itemIndex)
        {
            if (itemIndex == SelectedItemIndex)
            {
                return;
            }

            SelectedItemIndex = itemIndex;

            if (ItemSelected != null)
            {
                ItemSelected.Invoke(itemIndex);
            }
        }

        public void SelectElem(LineFinishBase finish)
        {
            SelectElem(this.IndexOf(finish));
        }

        public int SelectedItemIndex
        {
            get;
            set;
        } = 0;

        public LineFinishBase SelectedItem => this[SelectedItemIndex];

        public bool Load(string inptFilePath)
        {
            if (!File.Exists(inptFilePath))
            {
                return false;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(LineFinishBaseList));

            LineFinishBaseList lineFinishBaseList = (LineFinishBaseList)serializer.Deserialize(sr);

            foreach (LineFinishBase lineFinishBase in lineFinishBaseList)
            {
                if (string.IsNullOrEmpty(lineFinishBase.Guid))
                {
                    lineFinishBase.Guid = GuidMaintenance.CreateGuid(lineFinishBase);
                }
            }

            lineFinishBaseList.ForEach(
                e => { if (string.IsNullOrEmpty(e.Guid)) e.Guid = GuidMaintenance.CreateGuid(this); else GuidMaintenance.AddGuid(e.Guid, this); });

            this.AddRange(lineFinishBaseList);

            return true;
        }

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(LineFinishBaseList));

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

        public void AddElem(LineFinishBase lineFinishBase, int? selectPosition = null)
        {
            base.Add(lineFinishBase);

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(lineFinishBase);
            }

            if (selectPosition.HasValue)
            {
                SelectElem(selectPosition.Value);
            }

            LineFinishByGuid.Add(lineFinishBase.Guid, lineFinishBase);
            //AreaFinishByName.Add(areaFinishBase.AreaName, areaFinishBase);
        }

        public bool ItemsFiltered()
        {
            return Exists(l => l.Filtered);
        }

        public LineFinishBaseList Clone(bool zeroOutValues = false)
        {
            LineFinishBaseList clonedLineFinishBaseList = new LineFinishBaseList();

            foreach (LineFinishBase lineFinishBase in this)
            {
                clonedLineFinishBaseList.Add(lineFinishBase);

                if (zeroOutValues)
                {
                    lineFinishBase.LengthInInches = 0;
                }
            }

            return clonedLineFinishBaseList;
        }
    }
}
