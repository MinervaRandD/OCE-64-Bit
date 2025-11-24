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

    /// <summary>
    /// The basic form element of the overs / unders form.
    /// </summary>
    public partial class UCVirtualOversOversUndersFormElement : UserControl
    {
        public VirtualOverage Overage { get; set; }
        
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
        /// 

        public UCVirtualOversOversUndersFormElement()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes the element for a cut
        /// </summary>
        /// <param name="index">Index of the cut</param>
        /// <param name="cut">The cut</param>
        /// <param name="drawingScaleInInches">Current drawing scale</param>
        internal void Init(OversUndersForm oversUndersForm, int index, VirtualOverage overage, double drawingScaleInInches = 1.0)
        {
            this.oversUndersForm = oversUndersForm;

            this.Overage = overage;

            this.drawingScaleInInches = drawingScaleInInches;

            this.lblNmbr.Text = Utilities.IndexToLowerCaseString(index+1);

            int hghtInTotlInchs = (int)Math.Round(drawingScaleInInches * overage.EffectiveDimensions.Item1);
            int wdthInTotlInchs = (int)Math.Round(drawingScaleInInches * overage.EffectiveDimensions.Item2);

            OrigLnthInch = hghtInTotlInchs % 12;
            OrigWdthInch = wdthInTotlInchs % 12;

            OrigLnthFeet = (hghtInTotlInchs - OrigLnthInch) / 12;
            OrigWdthFeet = (wdthInTotlInchs - OrigWdthInch) / 12;

            if (overage.ShapeHasBeenOverridden)
            {
                hghtInTotlInchs = (int)Math.Round(drawingScaleInInches * overage.OverrideEffectiveDimensions.Item1);
                wdthInTotlInchs = (int)Math.Round(drawingScaleInInches * overage.OverrideEffectiveDimensions.Item2);

                OverLnthInch = hghtInTotlInchs % 12;
                OverWdthInch = wdthInTotlInchs % 12;

                OverLnthFeet = (hghtInTotlInchs - OverLnthInch) / 12;
                OverWdthFeet = (wdthInTotlInchs - OverWdthInch) / 12;

                this.txbLnth.Text = OverLnthFeet.ToString() + "' " + OverLnthInch.ToString().PadLeft(2) + "\" ";
                this.txbWdth.Text = OverWdthFeet.ToString() + "' " + OverWdthInch.ToString().PadLeft(2) + "\" ";

                if (OverWdthFeet.Value != OrigWdthFeet || OverWdthInch.Value != OrigWdthInch)
                {
                    this.txbWdth.BackColor = Color.Yellow;
                }

                else
                {
                    this.txbWdth.BackColor = SystemColors.ControlLightLight;
                }

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

                this.txbWdth.Text = OrigWdthFeet.ToString() + "' " + OrigWdthInch.ToString().PadLeft(2) + "\" ";
                this.txbLnth.Text = OrigLnthFeet.ToString() + "' " + OrigLnthInch.ToString().PadLeft(2) + "\" ";

                if (OrigWdthFeet <= 0)
                {
                    this.txbWdth.BackColor = Color.Pink;
                }

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
                    this.txbWdth.BackColor = SystemColors.ControlLightLight;

                    oversUndersForm.SetDoFillsBackgroundColor();
                }

            }

            this.txbLnth.TextChanged += TxbLnth_TextChanged;
            this.txbWdth.TextChanged += TxbWdth_TextChanged;

            this.lblNmbr.Click += LblNmbr_Click;
            this.lblX.Click += LblX_Click;
            this.Click += UCVirtualOversOversUndersFormElement_Click;
        }

        public bool Selected { get; set; } = false;

        private void UCVirtualOversOversUndersFormElement_Click(object sender, EventArgs e)
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
        private void TxbWdth_TextChanged(object sender, EventArgs e)
        {
            handleElementChanged();
        }

        private void TxbLnth_TextChanged(object sender, EventArgs e)
        {
            handleElementChanged();
        }

        private void handleElementChanged()
        {
            int? lnthFeet = null;
            int? lnthInch = null;

            int? wdthFeet = null;
            int? wdthInch = null;

            bool isValid = OversUndersCommon.HandleTextBoxChange(
                this.txbWdth, this.txbLnth
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
                Overage.OverrideEffectiveDimensions = null;

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

                Overage.OverrideEffectiveDimensions =
                    new Tuple<double, double>
                    (
                        (double)(OverLnthFeet.Value * 12 + OverLnthInch.Value) / drawingScaleInInches,
                        (double)(OverWdthFeet.Value * 12 + OverWdthInch.Value) / drawingScaleInInches
                    );
            }
        }

        public bool IsValid()
        {
            int? lnthFeet = null;
            int? lnthInches = null;

            int? wdthFeet = null;
            int? wdthInches = null;

            if (!Utilities.CheckTextBoxValidMeasurement(this.txbLnth, out lnthFeet, out lnthInches))
            {
                return false;
            }

            if (lnthFeet.Value <= 0)
            {
                return false;
            }

            if (!Utilities.CheckTextBoxValidMeasurement(this.txbWdth, out wdthFeet, out wdthInches))
            {
                return false;
            }

            if (wdthFeet.Value <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
