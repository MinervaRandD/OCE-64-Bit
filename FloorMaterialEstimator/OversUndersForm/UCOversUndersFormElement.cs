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

    /// <summary>
    /// The basic form element of the overs / unders form.
    /// </summary>
    public partial class UCOversUndersFormElement : UserControl
    {
        /// <summary>
        /// Overs / unders form element constructor
        /// </summary>
        public UCOversUndersFormElement()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes the element for a cut
        /// </summary>
        /// <param name="index">Index of the cut</param>
        /// <param name="cut">The cut</param>
        /// <param name="drawingScale">Current drawing scale</param>
        //internal void Init(Cut cut, double drawingScale = 1.0)
        //{
        //    this.lblNmbr.Text = cut.CutIndex.ToString();

        //    // Currently, the width and length of the cut seem to be reversed in sense. For proper
        //    // display, until this is fixed, we swap them around.

        //    int hghtInTotlInchs = (int) Math.Round(drawingScale * cut.CutRectangle.Height);
        //    int wdthInTotlInchs = (int) Math.Round(drawingScale * cut.CutRectangle.Width);

        //    int hghtInch = hghtInTotlInchs % 12;
        //    int wdthInch = wdthInTotlInchs % 12;

        //    int hghtFeet = (hghtInTotlInchs - hghtInch) / 12;
        //    int wdthFeet = (wdthInTotlInchs - wdthInch) / 12;

        //    this.lblWdth.Text = hghtFeet.ToString() + "' " + hghtInch.ToString().PadLeft(2) + "\" ";
        //    this.lblLnth.Text = wdthFeet.ToString() + "' " + wdthInch.ToString().PadLeft(2) + "\" ";
        //}

        /// <summary>
        /// Initializes the element for a overage
        /// </summary>
        /// <param name="index">The index of the overage</param>
        /// <param name="overage">The overage</param>
        /// <param name="drawingScale">Current drawing scale</param>
        internal void Init(string index, Overage overage, double drawingScale = 1.0)
        {
            this.lblNmbr.Text = index;

            int hghtInTotlInchs = (int)Math.Round(drawingScale * overage.EffectiveDimensions.Item2);
            int wdthInTotlInchs = (int)Math.Round(drawingScale * overage.EffectiveDimensions.Item1);

            int hghtInch = hghtInTotlInchs % 12;
            int wdthInch = wdthInTotlInchs % 12;

            int hghtFeet = (hghtInTotlInchs - hghtInch) / 12;
            int wdthFeet = (wdthInTotlInchs - wdthInch) / 12;

            this.lblWdth.Text = hghtFeet.ToString() + "' " + hghtInch.ToString().PadLeft(2) + "\" ";
            this.lblLnth.Text = wdthFeet.ToString() + "' " + wdthInch.ToString().PadLeft(2) + "\" ";
        }

        /// <summary>
        /// Initializes the elemnt for an overage
        /// </summary>
        /// <param name="index">The index of the underage</param>
        /// <param name="undrage">The underage</param>
        /// <param name="drawingScale">Current drawing scale</param>
        internal void Init(string index, Undrage undrage, double drawingScale = 1.0)
        {
            this.lblNmbr.Text = index;

            int hghtInTotlInchs = (int)Math.Round(drawingScale * undrage.EffectiveDimensions.Item2);
            int wdthInTotlInchs = (int)Math.Round(drawingScale * undrage.EffectiveDimensions.Item1);

            int hghtInch = hghtInTotlInchs % 12;
            int wdthInch = wdthInTotlInchs % 12;

            int hghtFeet = (hghtInTotlInchs - hghtInch) / 12;
            int wdthFeet = (wdthInTotlInchs - wdthInch) / 12;

            this.lblWdth.Text = hghtFeet.ToString() + "' " + hghtInch.ToString().PadLeft(2) + "\" ";
            this.lblLnth.Text = wdthFeet.ToString() + "' " + wdthInch.ToString().PadLeft(2) + "\" ";
        }
    }
}
