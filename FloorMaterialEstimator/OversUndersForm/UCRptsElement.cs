//-------------------------------------------------------------------------------//
// <copyright file="UCOversUndersFormElement.cs"                                 //
//                company="Bruun Estimating, LLC">                               // 
//     Copyright (C) Bruun Estimating, LLC - All Rights Reserved.                //
//     Unauthorized copying of this file, via any medium is strictly prohibited. //
//     Proprietary and confidential.                                             //
// </copyright>                                                                  //
//-------------------------------------------------------------------------------//

//-------------------------------------------------------------------------------//
//    Written by Marc Diamond <marc.d.diamond@MinervaRandD.com>,                 //
//               Minerva Research and Development LLC, 2019, 2020                //
//-------------------------------------------------------------------------------//

namespace OversUndersForm
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;
    using MaterialsLayout;
    using Utilities;
    using Geometry;

    /// <summary>
    /// The basic form element of the overs / unders form.
    /// </summary>
    public partial class UCRptsElement : UserControl
    {
        double drawingScale = 0;

        /// <summary>
        /// Overs / unders form element constructor
        /// </summary>
        public UCRptsElement()
        {
            InitializeComponent();

            this.lblLngth.Text = "";
            this.lblWdth.Text = "";
        }

        //internal void Init(double totalSmallUndrageArea, double drawingScaleInInches, double rollWidthInInches)
        //{
        //    this.drawingScale = drawingScale;

        //    this.lblLnth.Text = fillLnthFeet.ToString() + "' " + fillLnthInch.ToString().PadLeft(2) + "\" ";
        //    this.lblWdth.Text = fillWdthFeet.ToString() + "' " + fillWdthInch.ToString().PadLeft(2) + "\" ";
        //}

        //internal void Update(int fillWdthFeet, int fillWdthInch, int fillLnthFeet, int fillLnthInch)
        //{

        //    this.lblLnth.Text = fillLnthFeet.ToString() + "' " + fillLnthInch.ToString().PadLeft(2) + "\" ";
        //    this.lblWdth.Text = fillWdthFeet.ToString() + "' " + fillWdthInch.ToString().PadLeft(2) + "\" ";
        //}

        //public void SetTitle(string title)
        //{
        //    this.lblTitle.Text = title;
        //}

        //public void SetFont(Font font)
        //{
        //    this.lblTitle.Font = font;
        //    this.lblWdth.Font = font;
        //    this.lblLnth.Font = font;
        //    this.lblX.Font = font;
        //}
        internal void Update(double lnthInInches, double wdthInInches)
        {
            int hghtInTotlInch = (int)Math.Round(lnthInInches);
            int wdthInTotlInch = (int)Math.Round(wdthInInches);

            int lnthInch = hghtInTotlInch % 12;
            int wdthInch = wdthInTotlInch % 12;

            int lnthFeet = hghtInTotlInch / 12;
            int wdthFeet = wdthInTotlInch / 12;

            this.lblWdth.Text = lnthFeet.ToString() + "' " + lnthInch.ToString().PadLeft(2) + "\" ";
            this.lblLngth.Text = wdthFeet.ToString() + "' " + wdthInch.ToString().PadLeft(2) + "\" ";

        }

    }
}
