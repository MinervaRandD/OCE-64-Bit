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
    using FloorMaterialEstimator.Finish_Controls;
    using SettingsLib;

    /// <summary>
    /// The basic form element of the overs / unders form.
    /// </summary>
    public partial class UCCutsExtraFormElement : UserControl
    {
        private int materialWdthInch;
        private int materialWdthFeet;

        private int? xtraLgthInch;
        private int? xtraLgthFeet;

        //private double drawingScale;

        OversUndersForm oversUndersForm = null;

        /// <summary>
        /// Overs / unders form element constructor
        /// </summary>
        public UCCutsExtraFormElement()
        {
            InitializeComponent();

            OversUndersCommon.FormElementSetup(this.lblTitle, this.lblWdth, this.lblX, this.txbLnth);

            this.txbLnth.TextChanged += TxbLnth_TextChanged;
        }


        /// <summary>
        /// Initializes the element for a cut
        /// </summary>
        /// <param name="index">Index of the cut</param>
        /// <param name="cut">The cut</param>
        /// <param name="drawingScale">Current drawing scale</param>
        internal void Init(OversUndersForm oversUndersForm)
        {
            this.oversUndersForm = oversUndersForm;
        }

        private void TxbLnth_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txbLnth.Text))
            {
                xtraLgthFeet = 0; xtraLgthInch = 0;

                this.txbLnth.BackColor = SystemColors.ControlLightLight;
            }
            
            else if (!Utilities.CheckTextBoxValidMeasurement(this.txbLnth, out xtraLgthFeet, out xtraLgthInch, true))
            {
                this.txbLnth.BackColor = Color.Pink;

                xtraLgthFeet = 0; xtraLgthInch = 0;
            }

            else
            {
                this.txbLnth.BackColor = SystemColors.ControlLightLight;
            }

            oversUndersForm.ResetTotals();
        }

        internal void Update(double rollWdthInInches, int fillHghtFeet, int fillHghtInch, int fillWdthFeet, int fillWdthInch)
        {
            int MaterialWidthInTotalInches = (int)Math.Round(rollWdthInInches);

            this.materialWdthInch = MaterialWidthInTotalInches % 12;
            this.materialWdthFeet = MaterialWidthInTotalInches / 12;

            this.lblWdth.Text = this.materialWdthFeet.ToString() + "' " + this.materialWdthInch.ToString() + '"';
        }

        internal double Value()
        {
            if (xtraLgthFeet == null || xtraLgthInch == null)
            {
                return 0;
            }

            return xtraLgthFeet.Value * 12 + xtraLgthInch.Value;
        }

        public string Index => this.lblTitle.Text.Trim();

        public new string Width => this.lblWdth.Text.Trim();

        public string Length => this.txbLnth.Text.Trim();
    }
}
