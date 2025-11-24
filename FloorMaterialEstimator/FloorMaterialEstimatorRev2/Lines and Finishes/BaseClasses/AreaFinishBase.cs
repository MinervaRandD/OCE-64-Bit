

namespace FloorMaterialEstimator.Finish_Controls
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections;
    using System.Diagnostics;

    using Utilities;

    [Serializable]
    public class AreaFinishBase
    {
        public string Guid { get; set; }

        public string AreaName { get; set; }

        public double Opacity { get; set; }

        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        [XmlIgnore]
        public Color Color
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }

            set
            {
                A = value.A;
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        [XmlIgnore]
        public static AreaFinishBase DefaultAreaFinish = new AreaFinishBase()
        {
            AreaName = "",
            Opacity = 1,
            Color = Color.Cyan
        };

        public string Product { get; set; }

        public string Notes { get; set; }

        private SeamFinishBase finishSeamBase = null;

        //public FinishSeamBase FinishSeamBase { get; set; } = null;

        public SeamFinishBase FinishSeamBase
        {
            get
            {
                return finishSeamBase;
            }
            set
            {
                if (value == null)
                {
                    finishSeamBase = null;
                    return;
                }

                if (value.VisioDashType <= 0)
                {
                    return;
                }

                finishSeamBase = value;
            }
        }

        public double TileHeightInInches { get;  set; }
        public double RollWidthInInches { get;  set; }
        public double RollRepeat1InInches { get;  set; }
        public double RollRepeat2InInches { get;  set; }
        public bool Seamed { get;  set; }
        public bool Cuts { get;  set; }
        public double TileWidthInInches { get;  set; }

        internal double dArea { get; set; }
        internal double dWaste { get; set; }

        [XmlIgnore]
        public static Color DefaultAreaColor = Color.Cyan;
        
        internal AreaFinishBase Clone()
        {
            AreaFinishBase clonedAreaFinishBase = new AreaFinishBase()
            {
                AreaName = this.AreaName,
                Opacity = this.Opacity,
                Color = this.Color,
                Product = this.Product,
                Notes = this.Notes,
                FinishSeamBase = this.FinishSeamBase
            };

            clonedAreaFinishBase.Guid = GuidMaintenance.CreateGuid(clonedAreaFinishBase);

            return clonedAreaFinishBase;
        }
    }

    [Serializable]
    public class AreaFinishBaseList : List<AreaFinishBase>// IEnumerable<areaFinishBase>
    {
        public delegate void ItemAddedEventHandler(AreaFinishBase item);

        public event ItemAddedEventHandler ItemAdded;

        public void Add(AreaFinishBase areaFinishBase, int? selectPosition = null)
        {
            base.Add(areaFinishBase);

            if (ItemAdded != null)
            {
                ItemAdded.Invoke(areaFinishBase);
            }


            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }

        public delegate void ItemInsertedEventHandler(AreaFinishBase item, int position);

        public event ItemInsertedEventHandler ItemInserted;

        public void Insert(AreaFinishBase areaFinishBase, int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position <= Count);

            if (position == Count)
            {
                Add(areaFinishBase);
                return;
            }

            base.Insert(position, areaFinishBase);

            if (ItemInserted != null)
            {
                ItemInserted.Invoke(areaFinishBase, position);
            }

            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }

        public delegate void ItemRemovedEventHandler(int position);

        public event ItemRemovedEventHandler ItemRemoved;

        public void Remove(int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position < Count);

            AreaFinishBase areaFinishBase = base[position];

            base.RemoveAt(position);

            if (ItemRemoved != null)
            {
                ItemRemoved.Invoke(position);
            }

            if (selectPosition.HasValue)
            {
                Select(selectPosition.Value);
            }
        }

        public delegate void ItemsSwappedEventHandler(int position1, int position2);

        public event ItemsSwappedEventHandler ItemsSwapped;

        public void Swap(int position1, int position2, int? selectPosition = null)
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
                Select(selectPosition.Value);
            }
        }

        public delegate void ItemSelectedEventHandler(int itemIndex);

        public event ItemSelectedEventHandler ItemSelected;

        public void Select(int itemIndex)
        {
            SelectedItemIndex = itemIndex;

            if (ItemSelected != null)
            {
                ItemSelected.Invoke(itemIndex);
            }
        }

        public int SelectedItemIndex { get; set; } = 0;


        public bool Load(string inptFilePath)
        {
            if (!File.Exists(inptFilePath))
            {
                return false;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(AreaFinishBaseList));

            AreaFinishBaseList areaFinishBaseList = (AreaFinishBaseList)serializer.Deserialize(sr);

            areaFinishBaseList.ForEach(
                e => { if (string.IsNullOrEmpty(e.Guid)) e.Guid = GuidMaintenance.CreateGuid(this); else GuidMaintenance.AddGuid(e.Guid, this); });

            this.AddRange(areaFinishBaseList);

            return true;
        }

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(AreaFinishBaseList));

                StreamWriter sw = new StreamWriter(outpFilePath);

                serializer.Serialize(sw, this);
            }

            catch
            {
                return false;
            }

            return true;
        }
    }
}

