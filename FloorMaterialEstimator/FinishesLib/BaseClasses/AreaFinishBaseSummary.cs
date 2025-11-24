//-------------------------------------------------------------------------------//
// <copyright file="AreaFinishBaseSummary.cs" company="Bruun Estimating, LLC">   // 
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

    /// <summary>
    /// Primary purpose of this class is to retain certain attributes of the area finish base
    /// to be able to compare with the original area finish to see if reseaming is necessary
    /// after an edit session.
    /// </summary>
    public class AreaFinishBaseSummary
    {

        public AreaFinishBaseSummary() { }

        public AreaFinishBaseSummary(AreaFinishBase areaFinishBase)
        {
            SeamFinishBase = areaFinishBase.SeamFinishBase;
            MaterialsType = areaFinishBase.MaterialsType;
            AreaDisplayUnits = areaFinishBase.AreaDisplayUnits;
            RollRepeatWidthInInches = areaFinishBase.RollRepeatWidthInInches;
            RollRepeatLengthInInches = areaFinishBase.RollRepeatLengthInInches;
            Seamed = areaFinishBase.Seamed;
            TileWidthInInches = areaFinishBase.TileWidthInInches;
            WRepeatsInSqrFeet = areaFinishBase.WRepeatsInSqrFeet;
            LRepeatsInSqrFeet = areaFinishBase.LRepeatsInSqrFeet;
            FixedWidthInches = areaFinishBase.FixedWidthInches;
            TrimInInches = areaFinishBase.TrimInInches;
            RollWidthInInches = areaFinishBase.RollWidthInInches;
            OverlapInInches = areaFinishBase.OverlapInInches;
            AreaName = areaFinishBase.AreaName;
            Product = areaFinishBase.Product;
            Notes = areaFinishBase.Notes;
        }

        public string AreaName
        {
            get;
            set;
        }

        public string Product
        {
            get;
            set;
        }

        public string Notes
        {
            get;
            set;
        }

        public SeamFinishBase SeamFinishBase
        {
            get;
            set;
        }

        public MaterialsType MaterialsType
        {
            get;
            set;
        }

        public AreaDisplayUnits AreaDisplayUnits
        {
            get;
            set;
        }

        public double RollRepeatWidthInInches { get; set; }

        public double RollRepeatLengthInInches { get; set; }

        public bool Seamed { get; set; }

        public double TileWidthInInches { get; set; }

        public double WRepeatsInSqrFeet { get; set; } = 0;

        public double LRepeatsInSqrFeet { get; set; } = 0;

        public int FixedWidthInches { get; set; } = 12;


        public double TrimInInches
        {
            get;
            set;

        }

        public double RollWidthInInches
        {
            get;
            set;
        }

        public int OverlapInInches
        {
            get;
            set;
        }
    }
}