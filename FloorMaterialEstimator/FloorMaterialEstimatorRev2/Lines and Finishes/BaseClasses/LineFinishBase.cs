

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
    public class LineFinishBase
    {
        public string Guid { get; set; }

        public string LineName { get; set; }

        public int VisioLineType { get; set; }

        public double LineWidthInPts { get; set; }

        // Color specification

        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public string Product { get; set; }

        public string Notes { get; set; }

        [XmlIgnore]
        public Color LineColor
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

        public static LineFinishBase DefaultLineFinish = new LineFinishBase()
        {
            LineName = "",
            VisioLineType = 1,
            LineWidthInPts = 2,
            A = Color.Cyan.A,
            R = Color.Cyan.R,
            G = Color.Cyan.G,
            B = Color.Cyan.B
        };

        [XmlIgnore]
        public static float[][] VisioToDrawingPatternDict = new float[][]
        {
            null,                       /*  0 */
            new float[] { 1 },          /*  1 */
            new float[] { 4, 1, 4, 1 }, /*  2 */
            null,                       /*  3 */
            new float[] { 4, 2, 2, 2 }, /*  4 */
            null,                       /*  5 */
            null,                       /*  6 */
            null,                       /*  7 */
            null,                       /*  8 */
            null,                       /*  9 */
            new float[] { 1, 1, 1, 1 }, /* 10 */
            null,                       /* 11 */
            null,                       /* 12 */
            null,                       /* 13 */
            new float[] { 8, 2, 3, 2 }, /* 14 */
            null,                       /* 15 */
            new float[] { 6, 2, 6, 2 }, /* 16 */
            null,                       /* 17 */
            null,                       /* 18 */
            null,                       /* 19 */
            null,                       /* 20 */
            null,                       /* 21 */
            null,                       /* 22 */
            new float[] { 2, 1, 2, 1 }  /* 23 */
        };

        [XmlIgnore]
        public static Color DefaultLineColor = Color.Cyan;

        [XmlIgnore]
        public static int DefaultLineWidthInPts = 2;

        [XmlIgnore]
        public static short DefaultVisioDashType = 1;

        internal LineFinishBase Clone()
        {
            LineFinishBase clonedLineFinishBase = new LineFinishBase()
            {
                LineName = this.LineName,
                VisioLineType = this.VisioLineType,
                LineWidthInPts = this.LineWidthInPts,
                LineColor = this.LineColor
            };

            clonedLineFinishBase.Guid = GuidMaintenance.CreateGuid(clonedLineFinishBase);

            return clonedLineFinishBase;
        }
    }

    [Serializable]
    public class LineFinishBaseList : List<LineFinishBase>
    {
        public delegate void ItemAddedEventHandler(LineFinishBase item);

        public event ItemAddedEventHandler ItemAdded;

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

        public delegate void ItemInsertedEventHandler(LineFinishBase item, int position);

        public event ItemInsertedEventHandler ItemInserted;

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

        public delegate void ItemRemovedEventHandler(int position);

        public event ItemRemovedEventHandler ItemRemoved;

        public void Remove(int position, int? selectPosition = null)
        {
            Debug.Assert(position >= 0 && position < Count);

            LineFinishBase lineFinishBase = base[position];

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
            }

            catch
            {
                return false;
            }

            return true;
        }
    }
}

