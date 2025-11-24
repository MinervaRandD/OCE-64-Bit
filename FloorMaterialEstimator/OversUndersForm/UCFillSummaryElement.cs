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
    public partial class UCFillSummaryElement : UserControl
    {
        /// <summary>
        /// Overs / unders form element constructor
        /// </summary>
        public UCFillSummaryElement()
        {
            InitializeComponent();

            OversUndersCommon.FormElementSetup(this.lblTitle, this.lblWdth, this.lblX, this.lblLnth);
        }

        internal void Update(int fillWdthFeet, int fillWdthInch, int fillLnthFeet, int fillLnthInch)
        {
            this.lblLnth.Text = fillLnthFeet.ToString() + "' " + fillLnthInch.ToString().PadLeft(2) + "\" ";
            this.lblWdth.Text = fillWdthFeet.ToString() + "' " + fillWdthInch.ToString().PadLeft(2) + "\" ";
        }

        public void SetTitle(string title)
        {
            this.lblTitle.Text = title;
        }

        public void SetFont(Font font)
        {
            this.lblTitle.Font = font;
            this.lblWdth.Font = font;
            this.lblLnth.Font = font;
            this.lblX.Font = font;
        }
        internal void Update(int wdthInTotlInch, int hghtInTotlInch)
        {
            int lnthInch = hghtInTotlInch % 12;
            int wdthInch = wdthInTotlInch % 12;

            int lnthFeet = hghtInTotlInch / 12;
            int wdthFeet = wdthInTotlInch / 12;

            this.lblLnth.Text = lnthFeet.ToString() + "' " + lnthInch.ToString().PadLeft(2) + "\" ";
            this.lblWdth.Text = wdthFeet.ToString() + "' " + wdthInch.ToString().PadLeft(2) + "\" ";

        }

        public string Index => this.lblTitle.Text.Trim();

        public new string Width => this.lblWdth.Text.Trim();

        public string Length => this.lblLnth.Text.Trim();
    }
}
