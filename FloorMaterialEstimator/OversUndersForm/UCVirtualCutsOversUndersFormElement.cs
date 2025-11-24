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
    public partial class UCvirtualCutsOversUndersFormElement : UserControl
    {
        public VirtualCut Cut { get; set; }


        public int OrigWdthFeet { get; set; }

        public int OrigWdthInch { get; set; }

        public int OrigLnthFeet { get; set; }

        public int OrigLnthInch { get; set; }

        public int? OverLnthFeet { get; set; }

        public int? OverLnthInch { get; set; }

        public int? OverWdthFeet { get; set; }

        public int? OverWdthInch { get; set; }

        private double drawingScaleInInches;

        OversUndersForm oversUndersForm = null;

        /// <summary>
        /// Overs / unders form element constructor
        /// </summary>
        public UCvirtualCutsOversUndersFormElement()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes the element for a cut
        /// </summary>
        /// <param name="index">Index of the cut</param>
        /// <param name="cut">The cut</param>
        /// <param name="drawingScale">Current drawing scale</param>
        internal void Init(OversUndersForm oversUndersForm, int count, VirtualCut cut, double drawingScaleInInches)
        {
            this.oversUndersForm = oversUndersForm;

            this.Cut = cut;

            this.drawingScaleInInches = drawingScaleInInches;

            this.lblNmbr.Text = count.ToString();

            int hghtInTotlInchs = (int)Math.Round(drawingScaleInInches * cut.EffectiveDimensions.Item1);
            int wdthInTotlInchs = (int)Math.Round(drawingScaleInInches * cut.EffectiveDimensions.Item2);

            OrigLnthInch = hghtInTotlInchs % 12;
            OrigWdthInch = wdthInTotlInchs % 12;

            OrigLnthFeet = (hghtInTotlInchs - OrigLnthInch) / 12;
            OrigWdthFeet = (wdthInTotlInchs - OrigWdthInch) / 12;

            if (cut.ShapeHasBeenOverridden)
            {
                hghtInTotlInchs = (int)Math.Round(drawingScaleInInches * cut.OverrideEffectiveDimensions.Item1);
                wdthInTotlInchs = (int)Math.Round(drawingScaleInInches * cut.OverrideEffectiveDimensions.Item2);

                OverLnthInch = hghtInTotlInchs % 12;
                OverWdthInch = wdthInTotlInchs % 12;

                OverLnthFeet = (hghtInTotlInchs - OverLnthInch) / 12;
                OverWdthFeet = (wdthInTotlInchs - OverWdthInch) / 12;

                this.txbLnth.Text = OverLnthFeet.ToString() + "' " + OverLnthInch.ToString().PadLeft(2) + "\" ";
                

                //if (OverWdthFeet.Value != OrigWdthFeet || OverWdthInch.Value != OrigWdthInch)
                //{
                //    this.txbWdth.BackColor = Color.Yellow;
                //}

                //else
                //{
                //    this.txbWdth.BackColor = SystemColors.ControlLightLight;
                //}

                if (OverLnthFeet.Value != OrigLnthFeet || OverLnthInch.Value != OrigLnthInch)
                {
                    this.txbLnth.BackColor = Color.Yellow;
                }

                else
                {
                    this.txbLnth.BackColor = SystemColors.ControlLightLight;
                }
            }

            else
            {
                OverLnthInch = null;
                OverWdthInch = null;
                OverLnthFeet = null;
                OverWdthFeet = null;

                this.lblWdth.Text = OrigWdthFeet.ToString() + "' " + OrigWdthInch.ToString().PadLeft(2) + "\" ";
                this.txbLnth.Text = OrigLnthFeet.ToString() + "' " + OrigLnthInch.ToString().PadLeft(2) + "\" ";

                //if (OrigWdthFeet <= 0)
                //{
                //    this.txbWdth.BackColor = Color.Pink;
                //}

                if (OrigLnthFeet <= 0)
                {
                    this.txbLnth.BackColor = Color.Pink;
                }

                if (OrigWdthFeet <= 0 || OrigLnthFeet <= 0)
                {
                    oversUndersForm.ClearFillsDisplay();
                }

                else
                {
                    this.txbLnth.BackColor = SystemColors.ControlLightLight;
                    //this.txbWdth.BackColor = SystemColors.ControlLightLight;

                    oversUndersForm.SetDoFillsBackgroundColor();
                }

            }

            this.txbLnth.TextChanged += TxbLnth_TextChanged;
            //this.txbWdth.TextChanged += TxbWdth_TextChanged;

            this.txbLnth.TextChanged += TxbLnth_TextChanged;


            this.lblNmbr.Click += LblNmbr_Click;
            this.lblX.Click += LblX_Click;
            this.Click += UCCutsOversUndersFormElement_Click;
        }

        public bool Selected { get; set; } = false;

        private void UCCutsOversUndersFormElement_Click(object sender, EventArgs e)
        {
            setSelectedStatus();
        }

        private void LblX_Click(object sender, EventArgs e)
        {
            setSelectedStatus();
        }

        private void LblNmbr_Click(object sender, EventArgs e)
        {
            setSelectedStatus();
        }

        private void setSelectedStatus()
        {
            if (!Selected)
            {
                Selected = true;

                this.BorderStyle = BorderStyle.Fixed3D;
            }

            else
            {
                Selected = false;

                this.BorderStyle = BorderStyle.None;
            }
        }

        private void TxbLnth_TextChanged(object sender, EventArgs e)
        {
            handleElementChanged();
            //int? feet = null;
            //int? inches = null;

            //if (!Utilities.CheckTextBoxValidMeasurement(this.txbLnth, out feet, out inches))
            //{
            //    this.txbLnth.BackColor = Color.Pink;
            //}

            //else if (feet == OrigHghtFeet && inches == OrigHghtInch)
            //{
            //    this.txbLnth.BackColor = SystemColors.ControlLightLight;

            //    resetOverrideRectangle();
            //}

            //else
            //{
            //    this.txbLnth.BackColor = Color.Yellow;

            //    setOverrideRectangle(feet.Value, inches.Value);
            //}


            //oversUndersForm.ResetTotals();
        }

        private void handleElementChanged()
        {
            int? lnthFeet = null;
            int? lnthInch = null;

            int? wdthFeet = null;
            int? wdthInch = null;

            bool isValid = OversUndersCommon.HandleTextBoxChange(
                null, this.txbLnth
                , OrigWdthFeet, OrigWdthInch, OrigLnthFeet, OrigLnthInch
                , out wdthFeet, out wdthInch, out lnthFeet, out lnthInch);

            if (!isValid)
            {
                return;
            }

            resetOverrideEffectiveDimensions(lnthFeet.Value, lnthInch.Value, wdthFeet.Value, wdthInch.Value);

            if (oversUndersForm.UndrsListIsValid())
            {
                oversUndersForm.SetDoFillsBackgroundColor();
            }

            else
            {
                oversUndersForm.ClearFillsDisplay();
            }
        }


        private void resetOverrideEffectiveDimensions(int lnthFeet, int lnthInches, int wdthFeet, int wdthInches)
        {
            if (lnthFeet == OrigLnthFeet && lnthInches == OrigLnthInch && wdthFeet == OrigWdthFeet && wdthInches == OrigWdthInch)
            {
                Cut.OverrideEffectiveDimensions = null;

                OverLnthFeet = null;
                OverLnthInch = null;
                OverWdthFeet = null;
                OverWdthInch = null;
            }

            else
            {
                OverLnthFeet = lnthFeet;
                OverLnthInch = lnthInches;
                OverWdthFeet = wdthFeet;
                OverWdthInch = wdthInches;

                Cut.OverrideEffectiveDimensions =
                    new Tuple<double, double>
                    (
                        (double)(OverLnthFeet.Value * 12 + OverLnthInch.Value) / drawingScaleInInches,
                        (double)(OverWdthFeet.Value * 12 + OverWdthInch.Value) / drawingScaleInInches
                    );
            }
        }

        
    }
}
