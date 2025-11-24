//-------------------------------------------------------------------------------//
// <copyright file="AreaFinishBase.cs" company="Bruun Estimating, LLC">          // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright> 
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//        Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>, 2019        //
//-------------------------------------------------------------------------------//

using System.Text;

namespace FinishesLib
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Xml.Serialization;
    using System.Diagnostics;
    using Utilities;

    [Serializable]
    public class AreaFinishBase
    {
        #region Events and Delegates

        public delegate void MaterialsTypeChangedHandler(AreaFinishBase finishBase, MaterialsType materialsType);

        public event MaterialsTypeChangedHandler MaterialsTypeChanged;

        public delegate void TileTrimChangedHandler(AreaFinishBase finishBase, double tileTrimInInches);

        public event TileTrimChangedHandler TileTrimChanged;

        public delegate void RollWidthChangedHandler(AreaFinishBase finishBase, double rollWidthInInches);

        public event RollWidthChangedHandler RollWidthChanged;

        public delegate void ExtraInchesPerCutChangedHandler(AreaFinishBase finishBase, int extraInchesPerCut);

        public event ExtraInchesPerCutChangedHandler ExtraInchesPerCutChanged;

        public delegate void RolloutLengthChangedHandler(AreaFinishBase finishBase, double rollOutLengthInInches);

        public event RolloutLengthChangedHandler RolloutLengthChanged;

        public delegate void FinishSeamChangedHandler(AreaFinishBase finishBase, SeamFinishBase seamFinishBase);

        public event FinishSeamChangedHandler FinishSeamChanged;

        public delegate void FinishSeamGraphicsChangedHandler(AreaFinishBase finishBase, SeamFinishBase seamFinishBase);

        public event FinishSeamGraphicsChangedHandler FinishSeamGraphicsChanged;

        //public delegate void WastePercentChangedHandler(AreaFinishBase finishBase, double wastePercent);

        //public event WastePercentChangedHandler WastePercentChanged;

        public delegate void AreaChangedHandler(AreaFinishBase finishBase, double area);

        public event AreaChangedHandler AreaChanged;

        public delegate void NetAreaChangedHandler(AreaFinishBase finishBase, double  netArea);

        public event NetAreaChangedHandler NetAreaChanged;

        public delegate void NetSeamedAreaChangedHandler(AreaFinishBase finishBase, double? netArea);

        public event NetSeamedAreaChangedHandler NetSeamedAreaChanged;

        public delegate void GrossAreaChangedHandler(AreaFinishBase finishBase, double? grossArea);

        public event GrossAreaChangedHandler GrossAreaChanged;

        public delegate void SeamedAreaLockedChangedHandler(AreaFinishBase finishBase, bool seamAreaLocked);

        public event SeamedAreaLockedChangedHandler SeamAreaLockedChanged;

        public delegate void PerimeterChangedHandler(AreaFinishBase finishBase, double perimeter);

        public event PerimeterChangedHandler PerimeterChanged;

        public delegate void FinishStatsUpdatedChangedHandler(
            AreaFinishBase finishBase, int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio);

        public event FinishStatsUpdatedChangedHandler FinishStatsUpdated;

        public delegate void ColorChangedHandler(AreaFinishBase finishBase, Color color);

        public event ColorChangedHandler ColorChanged;

        public delegate void PatternChangedHandler(AreaFinishBase finishBase, int pattern);

        public event PatternChangedHandler PatternChanged;

        public delegate void FillPatternLineWidthChangedHandler(AreaFinishBase finishBase, double widthInPts);

        public event FillPatternLineWidthChangedHandler FillPatternLineWidthChanged;

        public delegate void FillPatternLineStyleChangedHandler(AreaFinishBase finishBase, int lineStyle);

        public event FillPatternLineStyleChangedHandler FillPatternLineStyleChanged;

        public delegate void FillPatternLineInterlineDistanceInFtChangedHandler(AreaFinishBase finishBase, double InterlineDistanceInFt);

        public event FillPatternLineInterlineDistanceInFtChangedHandler FillPatternLineInterlineDistanceInFtChanged;

        public delegate void AreaNameChangedHandler(AreaFinishBase finishBase, string areaName);

        public event AreaNameChangedHandler AreaNameChanged;

        public delegate void OpacityChangedHandler(AreaFinishBase finishBase, double opacity);

        public event OpacityChangedHandler OpacityChanged;

        public delegate void FilteredChangedHandler(AreaFinishBase finishBase, bool filtered);

        public event FilteredChangedHandler FilteredChanged;

        public delegate void SelectedChangedHandler(AreaFinishBase finishBase, bool selected);

        public event SelectedChangedHandler SelectedChanged;

        public delegate void AreaDisplayUnitsChangedHandler(AreaFinishBase finishBase, AreaDisplayUnits areaDisplayUnits);

        public event AreaDisplayUnitsChangedHandler AreaDisplayUnitsChanged;

        #endregion

        public static int IndexCounter = 1;

        public int Index = 0;

        public string Guid { get; set; }

        private string areaName = string.Empty;

        public string AreaName
        {
            get
            {
                return areaName;
            }

            set
            {
                if (areaName == value)
                {
                    return;
                }

                areaName = value;

                if (AreaNameChanged != null)
                {
                    AreaNameChanged.Invoke(this, areaName);
                }
            }
        }

        private double opacity = 1.0;

        public double Opacity
        {
            get
            {
                return opacity;
            }

            set
            {
                if (opacity == value)
                {
                    return;
                }

                opacity = value;

                A = Math.Min(255, Math.Max(0, (int) Math.Round(opacity * 255.0)));

                if (OpacityChanged != null)
                {
                    OpacityChanged.Invoke(this, opacity);
                }
            }
        }

        public double Transparency
        {
            get
            {
                return 1.0 - Opacity;
            }

            set
            {
                Opacity = 1.0 - value;
            }
        }

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
                if (A == value.A && R == value.R && G == value.G && B == value.B)
                {
                    return;
                }

                A = value.A;
                R = value.R;
                G = value.G;
                B = value.B;

                if (ColorChanged != null)
                {
                    ColorChanged.Invoke(this, Color.FromArgb(A, R, G, B));
                }
            }
        }

        public bool UsesFillPattern => _fillPatternLineWidthInPts > 0;


        private double _fillPatternLineWidthInPts = 1;

        public double FillPatternLineWidthInPts
        {
            get
            {
                return _fillPatternLineWidthInPts;
            }

            set
            {
                // Unitialized existing projects will come in with this value as zero

                if (value <= 0)
                {
                    value = 1;
                }

                else if (value > 10)
                {
                    value = 10;
                }

                if (_fillPatternLineWidthInPts == value)
                {
                    return;
                }

                _fillPatternLineWidthInPts = value;

                if (FillPatternLineWidthChanged != null)
                {
                    FillPatternLineWidthChanged.Invoke(this, _fillPatternLineWidthInPts);
                }
            }
        }

        private int _fillPatternLineLineStyle = 1;

        public int FillPatternLineStyle
        {
            get
            {
                return _fillPatternLineLineStyle;
            }

            set
            {
                // Unitialized existing projects will come in with this value as zero

                if (value <= 0)
                {
                    value = 1;
                }

                else if (value > 10)
                {
                    value = 10;
                }

                if (_fillPatternLineLineStyle == value)
                {
                    return;
                }

                _fillPatternLineLineStyle= value;

                if (FillPatternLineStyleChanged != null)
                {
                    FillPatternLineStyleChanged.Invoke(this, _fillPatternLineLineStyle);
                }
            }
        }

        private double _fillPatternInterlineDistanceInFt = 0.25;

        public double FillPatternInterlineDistanceInFt
        {
            get
            {
                return _fillPatternInterlineDistanceInFt;
            }

            set
            {
                // Unitialized existing projects will come in with this value as zero

                if (value <= 0)
                {
                    value = 0.25;
                }

                if (_fillPatternInterlineDistanceInFt == value)
                {
                    return;
                }

                _fillPatternInterlineDistanceInFt = value;

                if (FillPatternLineInterlineDistanceInFtChanged != null)
                {
                    FillPatternLineInterlineDistanceInFtChanged.Invoke(this, _fillPatternInterlineDistanceInFt);
                }
            }
        }

        private int _fillPatternInterLineIndex = 5;

        public int FillPatternInterLineIndex
        {
            get
            {
                return _fillPatternInterLineIndex;
            }

            set
            {
                // Unitialized existing projects will come in with this value as zero

                if (value <= 0)
                {
                    value = 5;
                }

                else if (value > 10)
                {
                    value = 10;
                }

                if (_fillPatternInterLineIndex == value)
                {
                    return;
                }

                _fillPatternInterLineIndex = value;

                //if (FillPatternLineInterlineDistanceInFtChanged != null)
                //{
                //    FillPatternLineInterlineDistanceInFtChanged.Invoke(this, _fillPatternInterlineDistanceInFt);
                //}
            }
        }



        private int pattern = 0;

        // Remove pattern when done.
        public int Pattern
        {
            get
            {
                return pattern;
            }

            set
            {
                if (pattern == value)
                {
                    return;
                }

                pattern = value;

                if (PatternChanged != null)
                {
                    PatternChanged.Invoke(this, pattern);
                }
            }
        }

        [XmlIgnore]
        public static AreaFinishBase DefaultAreaFinish = new AreaFinishBase()
        {
            Index = IndexCounter++,

            AreaName = "",
            Opacity = 1,
            Color = Color.LightGray,
            MaterialsType = MaterialsType.Tiles,
            trimInInches = 4
        };

        public string Product { get; set; }

        public string Notes { get; set; }

        private SeamFinishBase seamFinishBase = null;

        public SeamFinishBase SeamFinishBase
        {
            get
            {
                return seamFinishBase;
            }

            set
            {
                if (value == null)
                {
                    seamFinishBase = null;
                    return;
                }

                if (value.VisioDashType <= 0)
                {
                    return;
                }

                // Remove the subscription to the seam graphics changed event for the previous
                // seam, now that the seam will be replaced

                if (seamFinishBase != null)
                {
                    seamFinishBase.SeamGraphicsChanged -= SeamFinishBase_SeamGraphicsChanged;
                }

                seamFinishBase = value;
   
                if (FinishSeamChanged != null)
                {
                    FinishSeamChanged.Invoke(this, seamFinishBase);
                }

                // Subscribe to the new seam, seam graphics changed event.

                seamFinishBase.SeamGraphicsChanged += SeamFinishBase_SeamGraphicsChanged;
            }
        }

        // This is in place to handle the situation where the seam graphics definition has been changed
        // but the seam itself has not changed for the current area finish.
        private void SeamFinishBase_SeamGraphicsChanged(SeamFinishBase SeamFinishBase)
        {
            if (FinishSeamGraphicsChanged != null)
            {
                FinishSeamGraphicsChanged.Invoke(this, this.SeamFinishBase);
            }
        }

        private MaterialsType materialsType = MaterialsType.Tiles;

        public MaterialsType MaterialsType
        {
            get
            {
                return materialsType;
            }

            set
            {
                if (materialsType == value)
                {
                    return;
                }

                materialsType = value;

                if (MaterialsTypeChanged != null)
                {
                    MaterialsTypeChanged.Invoke(this, materialsType);
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


        private AreaDisplayUnits areaDisplayUnits = AreaDisplayUnits.SquareFeet;

        public AreaDisplayUnits AreaDisplayUnits
        {
            get
            {
                return areaDisplayUnits;
            }

            set
            {
                if (areaDisplayUnits == value)
                {
                    return;
                }

                areaDisplayUnits = value;

                if (AreaDisplayUnitsChanged != null)
                {
                    AreaDisplayUnitsChanged.Invoke(this, areaDisplayUnits);
                }
            }
        }

        public double TileHeightInInches { get;  set; }

        public double RollRepeatWidthInInches { get;  set; }

        public double RollRepeatLengthInInches { get;  set; }


        public bool Seamed { get; set; } = false;

        public bool Cuts { get; set; } = false;

        public double TileWidthInInches { get;  set; }

        public double WRepeatsInSqrFeet { get; set; } = 0;

        public double LRepeatsInSqrFeet { get; set; } = 0;

        public int FixedWidthInches { get; set; } = 12;



        private double perimeterInInches = 0;

        public double PerimeterInInches
        {
            get
            {
                return perimeterInInches;
            }

            set
            {
                if (perimeterInInches == value)
                {
                    return;
                }

                perimeterInInches = value;

                if (PerimeterChanged != null)
                {
                    PerimeterChanged.Invoke(this, perimeterInInches);
                }

                if (FinishStatsUpdated != null)
                {
                    FinishStatsUpdated.Invoke(this, Count, _netAreaInSqrInches, GrossAreaInSqrInches, perimeterInInches, WasteRatio);
                }
            }
        }

        private double _netAreaInSqrInches;

        public double NetAreaInSqrInches
        {
            get
            {
                return _netAreaInSqrInches;
            }

            set
            {
                if (_netAreaInSqrInches == value)
                {
                    return;
                }

         
                _netAreaInSqrInches = value;

                if (NetAreaChanged != null)
                {
                    NetAreaChanged.Invoke(this, _netAreaInSqrInches);
                }

                if (AreaChanged != null)
                {
                    AreaChanged.Invoke(this, _netAreaInSqrInches);
                }

                if (FinishStatsUpdated != null)
                {
                    FinishStatsUpdated.Invoke(this, Count, _netAreaInSqrInches, GrossAreaInSqrInches, perimeterInInches, WasteRatio);
                }
            }
        }

        private double? _grossAreaInSqrInches = null;

      

        public double? GrossAreaInSqrInches
        {
            get
            {
                return _grossAreaInSqrInches;
            }

            set
            {
                if (_grossAreaInSqrInches == value)
                {
                    return;
                }

                _grossAreaInSqrInches = value;


                if (AreaChanged != null)
                {
                    AreaChanged.Invoke(this, _netAreaInSqrInches);
                }

                if (FinishStatsUpdated != null)
                {
                    FinishStatsUpdated.Invoke(this, Count, _netAreaInSqrInches, GrossAreaInSqrInches, perimeterInInches, WasteRatio);
                }
            }
        }

        private double? _netSeamedAreaInSqrInches = null;

        public double? NetSeamedAreaInSqrInches
        {
            get
            {
                return _netSeamedAreaInSqrInches;
            }


            set
            {
                if (_netSeamedAreaInSqrInches == value)
                {
                    return;
                }

                _netSeamedAreaInSqrInches = value;


                if (NetSeamedAreaChanged != null)
                {
                    NetSeamedAreaChanged.Invoke(this, _netSeamedAreaInSqrInches);
                }

                if (FinishStatsUpdated != null)
                {
                    FinishStatsUpdated.Invoke(this, Count, _netAreaInSqrInches, GrossAreaInSqrInches, perimeterInInches, WasteRatio);
                }
            }
        }

        public double? TileAreaGrossAreaInSqrInches
        {
            get
            {
                double perimeterInFeet = PerimeterInInches / 12.0;

                double waste = perimeterInFeet * (TrimInInches / 12.0);
                double netAreaInSqrFeet = NetAreaInSqrInches / 144.0;

                GrossAreaInSqrInches = (netAreaInSqrFeet + waste) * 144.0;

                return GrossAreaInSqrInches;
            }
        }

        public double? WastePct
        {
            get
            {
                if (NetAreaInSqrInches == 0)
                {
                    return null;
                }

                return GrossAreaInSqrInches / NetAreaInSqrInches - 1.0;
            }
        }

        public double? CalculateTileWasteFactor(bool scaleHasBeenSet)
        {
            if (!scaleHasBeenSet)
            {
                return null;
            }

            if (_netAreaInSqrInches <= 0)
            {
                return null;
            }

            double perimeterInLinearFeet = this.perimeterInInches / 12.0;

            double waste = (this.trimInInches / 12.0) * perimeterInLinearFeet;

            double netAreaInSqrFeet = this._netAreaInSqrInches / 144.0;

            double wasteRatio = (netAreaInSqrFeet + waste) / netAreaInSqrFeet - 1.0;

            return wasteRatio;
        }

        public double? Waste =>
            GrossAreaInSqrInches.HasValue ?
                GrossAreaInSqrInches - NetAreaInSqrInches :
                null;

        public double? WasteRatio
        {
            get;
            set;
        } = null;

        public int Count { get; set; } = 0;

        public void UpdateFinishStats(int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            this.Count = count;
            this.GrossAreaInSqrInches = grossAreaInSqrInches;
            this.NetAreaInSqrInches = netAreaInSqrInches;
            this.perimeterInInches = perimeterInInches;
            this.WasteRatio = wasteRatio;

            if (FinishStatsUpdated != null)
            {
                FinishStatsUpdated.Invoke(this, count, netAreaInSqrInches, GrossAreaInSqrInches, perimeterInInches, wasteRatio);
            }
        }


        public void UpdateFinishStatsBasic(int count, double netAreaInSqrInches, double? grossAreaInSqrInches, double perimeterInInches, double? wasteRatio)
        {
            this.Count = count;
            this._grossAreaInSqrInches = grossAreaInSqrInches;
            this._netAreaInSqrInches = netAreaInSqrInches;
            this.perimeterInInches = perimeterInInches;
            this.WasteRatio = wasteRatio;

            if (FinishStatsUpdated != null)
            {
                FinishStatsUpdated.Invoke(this, count, netAreaInSqrInches, GrossAreaInSqrInches, perimeterInInches, wasteRatio);
            }
        }


        private double rollOutLengthInInches = 0;

        public double RolloutLengthInInches
        {
            get
            {
                return rollOutLengthInInches;
            }

            set
            {
                if (rollOutLengthInInches == value)
                {
                    return;
                }

                rollOutLengthInInches = value;


                if (RolloutLengthChanged != null)
                {
                    RolloutLengthChanged.Invoke(this, rollOutLengthInInches);
                }
            }
        }


        private double trimInInches = 4;


        public double TrimInInches
        {
            get
            {
                return trimInInches;
            }

            set
            {
                if (value == trimInInches)
                {
                    return;
                }

                trimInInches = value;

                if (TileTrimChanged != null)
                {
                    TileTrimChanged.Invoke(this, trimInInches);
                }
            }

        }

        private double rollWidthInInches = 144;

        public double RollWidthInInches
        {
            get
            {
                return rollWidthInInches;
            }

            set
            {
                if (value == rollWidthInInches)
                {
                    return;
                }

                rollWidthInInches = value;

                if (rollWidthInInches < FixedWidthInches)
                {
                    FixedWidthInches = (int) rollWidthInInches;
                }

                if (RollWidthChanged != null)
                {
                    RollWidthChanged.Invoke(this, rollWidthInInches);
                }

            }
        }

        private int overlapInInches;

        public int OverlapInInches
        {
            get
            {
                return overlapInInches;
            }

            set
            {
                if (overlapInInches == value)
                {
                    return;
                }

                overlapInInches = value;

                if (RollWidthChanged != null)
                {
                    RollWidthChanged.Invoke(this, rollWidthInInches);
                }
            }
        }

        private int extraInchesPerCut;

        public int ExtraInchesPerCut
        {
            get
            {
                return extraInchesPerCut;
            }

            set
            {
                if (extraInchesPerCut == value)
                {
                    return;
                }

                extraInchesPerCut = value;

                if (ExtraInchesPerCutChanged != null)
                {
                    ExtraInchesPerCutChanged.Invoke(this, extraInchesPerCut);
                }
            }
        }

        public AreaFinishBase()
        {
            Index = IndexCounter++;
        }

        [XmlIgnore]
        public static Color DefaultAreaColor = Color.Cyan;
        
        public AreaFinishBase Clone()
        {
            AreaFinishBase clonedAreaFinishBase = new AreaFinishBase()
            {
                Index = IndexCounter++
                ,areaName = this.AreaName
                ,opacity = this.Opacity
                ,Color = this.Color
                ,Pattern= this.Pattern
                ,Product = this.Product
                ,Notes = this.Notes
                ,SeamFinishBase = this.SeamFinishBase
                ,MaterialsType = this.MaterialsType
                ,TileWidthInInches = this.TileWidthInInches
                ,_filtered = this._filtered
                ,rollWidthInInches = this.rollWidthInInches
                ,RollRepeatLengthInInches = this.RollRepeatLengthInInches
                ,RollRepeatWidthInInches = this.RollRepeatWidthInInches
                ,WRepeatsInSqrFeet = this.WRepeatsInSqrFeet
                ,LRepeatsInSqrFeet = this.LRepeatsInSqrFeet
                ,areaDisplayUnits = this.areaDisplayUnits
                ,trimInInches = this.trimInInches
                ,overlapInInches = this.overlapInInches
                ,extraInchesPerCut = this.extraInchesPerCut
                ,FillPatternInterlineDistanceInFt = this.FillPatternInterlineDistanceInFt
                ,FillPatternLineStyle = this.FillPatternLineStyle
                ,FillPatternInterLineIndex = this.FillPatternInterLineIndex
                ,FillPatternLineWidthInPts = this.FillPatternLineWidthInPts
                ,SeamAreaLocked = this.SeamAreaLocked
            };

            clonedAreaFinishBase.Guid = GuidMaintenance.CreateGuid(clonedAreaFinishBase);

            return clonedAreaFinishBase;
        }

        // The following is used to determine if the area finish has been changed in the editor.

        public AreaFinishBaseSummary AreaFinishBaseSummary
        {
            get;
            set;
        }

        bool _seamAreaLocked = false;

        public bool SeamAreaLocked
        {
            get
            {
                return _seamAreaLocked;
            }

            set
            {
                if (_seamAreaLocked == value)
                {
                    return;
                }

                _seamAreaLocked = value;

                if (this.SeamAreaLockedChanged != null)
                {
                    this.SeamAreaLockedChanged.Invoke(this, _seamAreaLocked);
                }
            }
        }


        public bool HasChanged()
        {
            if (AreaFinishBaseSummary is null)
            {
                return false;
            }

            if (AreaFinishBaseSummary.MaterialsType == MaterialsType.Tiles)
            {
                return false;
            }

            // Materials type changed from rolls to tile

            if (MaterialsType == MaterialsType.Tiles)
            {
                return true;
            }

            //if (SeamFinishBase != AreaFinishBaseSummary.SeamFinishBase)
            //{
            //    return true;
            //}

            if (RollRepeatWidthInInches != AreaFinishBaseSummary.RollRepeatWidthInInches)
            {
                return true;
            }

         

            if (RollWidthInInches != AreaFinishBaseSummary.RollWidthInInches)
            {
                return true;
            }

            if (OverlapInInches != AreaFinishBaseSummary.OverlapInInches)
            {
                return true;
            }

            return false;
        }

        public void Reset()
        {
            if (AreaFinishBaseSummary is null)
            {
                return; // Defensive
            }

            SeamFinishBase = AreaFinishBaseSummary.SeamFinishBase;
            MaterialsType = AreaFinishBaseSummary.MaterialsType;
            AreaDisplayUnits = AreaFinishBaseSummary.AreaDisplayUnits;
            RollRepeatWidthInInches = AreaFinishBaseSummary.RollRepeatWidthInInches;
            RollRepeatLengthInInches = AreaFinishBaseSummary.RollRepeatLengthInInches;
            Seamed = AreaFinishBaseSummary.Seamed;
            TileWidthInInches = AreaFinishBaseSummary.TileWidthInInches;
            WRepeatsInSqrFeet = AreaFinishBaseSummary.WRepeatsInSqrFeet;
            LRepeatsInSqrFeet = AreaFinishBaseSummary.LRepeatsInSqrFeet;
            FixedWidthInches = AreaFinishBaseSummary.FixedWidthInches;
            TrimInInches = AreaFinishBaseSummary.TrimInInches;
            RollWidthInInches = AreaFinishBaseSummary.RollWidthInInches;
            OverlapInInches = AreaFinishBaseSummary.OverlapInInches;
            AreaName = AreaFinishBaseSummary.AreaName;
            Product = AreaFinishBaseSummary.Product;
            Notes = AreaFinishBaseSummary.Notes;
        }

        public void ClearTallyCounts()
        {
            NetAreaInSqrInches = 0.0;
            PerimeterInInches = 0.0;
        }

        public override string ToString()
        {
            return $"Name: {AreaName} {MaterialsType}";
        }
    }
}

