//-------------------------------------------------------------------------------//
// <copyright file="LineFinishBase.cs" company="Bruun Estimating, LLC">          // 
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
    public class LineFinishBase
    {
        #region Events and Delegates

        public delegate void LineNameChangedHandler(LineFinishBase lineFinishBase, string lineName);

        public event LineNameChangedHandler LineNameChanged;

        public delegate void LineTypeChangedHandler(LineFinishBase lineFinishBase, int lineType);

        public event LineTypeChangedHandler LineTypeChanged;

        public delegate void LineWidthChangedHandler(LineFinishBase LineFinishBase, double lineWidthInPts);

        public event LineWidthChangedHandler LineWidthChanged;

        public delegate void FilteredChangedHandler(LineFinishBase LineFinishBase, bool filtered);

        public event FilteredChangedHandler FilteredChanged;

        public delegate void SelectedChangedHandler(LineFinishBase LineFinishBase, bool selected);

        public event SelectedChangedHandler SelectedChanged;

        public delegate void LineColorChangedHandler(LineFinishBase LineFinishBase, Color lineColor);

        public event LineColorChangedHandler LineColorChanged;

        public delegate void LengthChangedHandler(LineFinishBase LineFinishBase, double lengthInInches);

        public event LengthChangedHandler LengthChanged;

        public delegate void WalllHeightChangedHandler(LineFinishBase LineFinishBase, double? wallHeightInFeet);

        public event WalllHeightChangedHandler WallHeightChanged;

        public delegate void DefinitionChangedHandler(LineFinishBase LineFinishBase);

        public event DefinitionChangedHandler DefinitionChanged;
        
        #endregion

        public string Guid { get; set; }

        private double? _wallHeightInFeet = null;

        public double? WallHeightInFeet
        {
            get => _wallHeightInFeet;

            set
            {
                if (value == _wallHeightInFeet)
                {
                    return;
                }

                _wallHeightInFeet = value;

                if (WallHeightChanged != null)
                {
                    WallHeightChanged.Invoke(this, _wallHeightInFeet);
                }

                if (DefinitionChanged != null)
                {
                    DefinitionChanged.Invoke(this);
                }
            }
        }

        [XmlIgnore]
        public double? sqrAreaInFeet
        {
            get
            {
                if (WallHeightInFeet == null)
                {
                    return null;
                }

                return WallHeightInFeet.Value * LengthInInches / 12.0;
            }
        }

        [XmlIgnore]
        public bool IsWallLine => (WallHeightInFeet != null);

        private string _lineName;

        public string LineName
        {
            get
            {
                return _lineName;
            }

            set
            {
                if (_lineName == value)
                {
                    return;
                }

                _lineName = value;

                if (LineNameChanged != null)
                {
                    LineNameChanged.Invoke(this, _lineName);
                }

                if (DefinitionChanged != null)
                {
                    DefinitionChanged.Invoke(this);
                }
            }
        }

        private int _visioLineType = 0;

        public int VisioLineType
        {
            get
            {
                return _visioLineType;
            }

            set
            {
                if (_visioLineType == value)
                {
                    return;
                }

                _visioLineType = value;

                if (LineTypeChanged != null)
                {
                    LineTypeChanged.Invoke(this, _visioLineType);
                }

                if (DefinitionChanged != null)
                {
                    DefinitionChanged.Invoke(this);
                }
            }
        }

        private double _lineWidthInPts;

        public double LineWidthInPts
        {
            get
            {
                return _lineWidthInPts;
            }

            set
            {
                if (_lineWidthInPts == value)
                {
                    return;
                }

                _lineWidthInPts = value;

                if (LineWidthChanged != null)
                {
                    LineWidthChanged.Invoke(this, _lineWidthInPts);
                }

                if (DefinitionChanged != null)
                {
                    DefinitionChanged.Invoke(this);
                }
            }
        }

        private bool _filtered = false;

        public bool Filtered
        {
            get
            {
                return _filtered;
            }

            set
            {
                if (_filtered == value)
                {
                    return;
                }

                _filtered = value;

                if (FilteredChanged != null)
                {
                    FilteredChanged.Invoke(this, _filtered);
                }
            }
        }

        private double _lengthInInches = 0;

        public double LengthInInches
        {
            get
            {
                return _lengthInInches;
            }

            set
            {
                if (_lengthInInches == value)
                {
                    return;
                }

                _lengthInInches = value;

                if (LengthChanged != null)
                {
                    LengthChanged.Invoke(this, _lengthInInches);
                }
                
                if (DefinitionChanged != null)
                {
                    DefinitionChanged.Invoke(this);
                }
            }
        }

        public double LgthInFeet
        {
            get
            {
                return LengthInInches / 12.0;
            }
        }

        public double? AreaInSqrFeet
        {
            get
            {
                if (!IsWallLine)
                {
                    return null;
                }

                return LgthInFeet * WallHeightInFeet.Value;
            }
        }

        private bool _selected = false;

        public bool Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                if (_selected == value)
                {
                    return;
                }

                _selected = value;

                if (SelectedChanged != null)
                {
                    SelectedChanged.Invoke(this, _selected);
                }

            }
        }

        // Color specification

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

                if (LineColorChanged != null)
                {
                    LineColorChanged.Invoke(this, Color.FromArgb(A, R, G, B));
                }

                if (DefinitionChanged != null)
                {
                    DefinitionChanged.Invoke(this);
                }
            }
        }

        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public string Product { get; set; }

        public string Notes { get; set; }


        public static LineFinishBase DefaultLineFinish = new LineFinishBase()
        {
            LineName = "",
            VisioLineType = 1,
            LineWidthInPts = 2,
            A = Color.Gray.A,
            R = Color.Gray.R,
            G = Color.Gray.G,
            B = Color.Gray.B
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

        public LineFinishBase Clone()
        {
            LineFinishBase clonedLineFinishBase = new LineFinishBase()
            {
                LineName = this.LineName,
                VisioLineType = this.VisioLineType,
                LineWidthInPts = this.LineWidthInPts,
                WallHeightInFeet = this.WallHeightInFeet,
                LineColor = this.LineColor,
                Product = this.Product,
                Notes = this.Notes
            };

            clonedLineFinishBase.Guid = GuidMaintenance.CreateGuid(clonedLineFinishBase);

            return clonedLineFinishBase;
        }

        public static LineFinishBase Load(string inptFilePath)
        {
            if (!File.Exists(inptFilePath))
            {
                return null;
            }

            StreamReader sr = new StreamReader(inptFilePath);

            var serializer = new XmlSerializer(typeof(LineFinishBase));

            LineFinishBase lineFinishBase = (LineFinishBase)serializer.Deserialize(sr);

            return lineFinishBase;

        }

        public bool Save(string outpFilePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(LineFinishBase));

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
    }

}

