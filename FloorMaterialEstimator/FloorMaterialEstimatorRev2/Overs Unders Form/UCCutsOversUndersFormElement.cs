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

namespace FloorMaterialEstimator.OversUndersForm
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
    public partial class UCCutsOversUndersFormElement : UserControl, IDisposable
    {
        public Cut Cut { get; set; }

        public int OrigHghtInch { get; set; }

        public int OrigWdthInch { get; set; }

        public int OrigHghtFeet { get; set; }

        public int OrigWdthFeet { get; set; }

        public int? OverHghtInch { get; set; }

        public int? OverWdthInch { get; set; }

        public int? OverHghtFeet { get; set; }

        public int? OverWdthFeet { get; set; }

        private double drawingScaleInInches;

        private int extraInchesPerCut;

        public OversUndersForm oversUndersForm = null;

        /// <summary>
        /// Overs / unders form element constructor
        /// </summary>
        public UCCutsOversUndersFormElement()
        {
            InitializeComponent();

            OversUndersCommon.FormElementSetup(this.lblNmbr, this.lblWdth, this.lblX, this.txbLnth, this.txbRepeats);

            this.Disposed += UCCutsOversUndersFormElement_Disposed;
        }


        /// <summary>
        /// Initializes the element for a graphicsCut
        /// </summary>
        /// <param name="index">Index of the graphicsCut</param>
        /// <param name="graphicsCut">The graphicsCut</param>
        /// <param name="drawingScaleInInches">Current drawing scale</param>
        internal void Init(
            OversUndersForm oversUndersForm
            , GraphicsCut graphicsCut
            , double drawingScaleInInches
            , double rollWdthInInches
            , int extraInchesPerCut)
        {
            this.extraInchesPerCut = extraInchesPerCut;
        
            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {
                double totlWdthInInches =  Math.Round(drawingScaleInInches * graphicsCut.CutRectangle.Height);

                if (Math.Abs(totlWdthInInches - rollWdthInInches) > 1e-1)
                {
                    MessageBox.Show("Invalid graphicsCut roll width found.");
                }
            }


            this.oversUndersForm = oversUndersForm;

            this.Cut = graphicsCut;

            this.txbRepeats.Text = Cut.PatternRepeats.ToString();

            this.drawingScaleInInches = drawingScaleInInches;

            if (graphicsCut.Deleted)
            {
                this.lblNmbr.Text = '(' + graphicsCut.CutIndex.ToString() + ')';

                this.lblNmbr.BackColor = Color.Red;
                this.lblNmbr.Font = new Font(this.lblNmbr.Font, FontStyle.Strikeout);
            }

            else
            {

                this.lblNmbr.Text = graphicsCut.CutIndex.ToString();

                this.lblNmbr.BackColor = SystemColors.ControlLight;
                this.lblNmbr.Font = new Font(this.lblNmbr.Font, FontStyle.Regular);
            }

            // Currently, the width and length of the graphicsCut seem to be reversed in sense. For proper
            // display, until this is fixed, we swap them around.

            int hghtInTotlInchs = (int) Math.Round(drawingScaleInInches * graphicsCut.CutRectangle.Width) + extraInchesPerCut;
            int wdthInTotlInchs = (int) Math.Round(drawingScaleInInches * graphicsCut.MaterialWidth);

            OrigHghtInch = hghtInTotlInchs % 12;
            OrigWdthInch = wdthInTotlInchs % 12;

            OrigHghtFeet = (hghtInTotlInchs - OrigHghtInch) / 12;
            OrigWdthFeet = (wdthInTotlInchs - OrigWdthInch) / 12;

            if (graphicsCut.ShapeHasBeenOverridden)
            {
                hghtInTotlInchs = (int)Math.Round(drawingScaleInInches * graphicsCut.OverrideCutRectangle.Height);
                wdthInTotlInchs = (int)Math.Round(drawingScaleInInches * graphicsCut.OverrideCutRectangle.Width);

                OverHghtInch = hghtInTotlInchs % 12;
                OverWdthInch = wdthInTotlInchs % 12;

                OverHghtFeet = (hghtInTotlInchs - OverHghtInch) / 12;
                OverWdthFeet = (wdthInTotlInchs - OverWdthInch) / 12;

                this.txbLnth.Text = OverHghtFeet.ToString() + "' " + OverHghtInch.ToString().PadLeft(2) + "\" ";
                this.lblWdth.Text = OverWdthFeet.ToString() + "' " + OverWdthInch.ToString().PadLeft(2) + "\" ";

                this.txbLnth.BackColor = Color.Yellow;
            }

            else
            {
                OverHghtInch = null;
                OverWdthInch = null;
                OverHghtFeet = null;
                OverWdthFeet = null;

                this.txbLnth.Text = OrigHghtFeet.ToString() + "' " + OrigHghtInch.ToString().PadLeft(2) + "\" ";
                this.lblWdth.Text = OrigWdthFeet.ToString() + "' " + OrigWdthInch.ToString().PadLeft(2) + "\" ";

                this.txbLnth.BackColor = SystemColors.ControlLightLight;
            }

           
            if (GlobalSettings.ValidateRolloutAndCutWidth)
            {
                int wdthFeet = 0;

                int index = this.lblWdth.Text.IndexOf('\'');

                string testString = this.lblWdth.Text.Substring(0, index);

                int.TryParse(testString, out wdthFeet);

                int totlWdthInInches = (int) Math.Round(drawingScaleInInches * graphicsCut.CutRectangle.Height);

                if (Math.Abs(wdthFeet - totlWdthInInches / 12) > 1e-1)
                {
                    MessageBox.Show("Invalid graphicsCut width detected in textboxes.");
                }
            }

            this.txbLnth.TextChanged += TxbLnth_TextChanged;
            this.lblNmbr.Click += LblNmbr_Click;
            this.lblX.Click += LblX_Click;
            this.Click += UCCutsOversUndersFormElement_Click;
        }

        public bool Selected
        {
            get;
            set;
        } = false;

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
            int? feet = null;
            int? inches = null;

            if (!Utilities.CheckTextBoxValidMeasurement(this.txbLnth, out feet, out inches))
            {
                this.txbLnth.BackColor = Color.Pink;
            }

            else if (feet == OrigHghtFeet && inches == OrigHghtInch)
            {
                this.txbLnth.BackColor = SystemColors.ControlLightLight;

                resetOverrideRectangle();
            }

            else
            {
                this.txbLnth.BackColor = Color.Yellow;

                setOverrideRectangle(feet.Value, inches.Value);
            }


            oversUndersForm.ResetTotals();
        }

        private void setOverrideRectangle(int feet, int inches)
        {
            OverHghtInch = inches;
            OverWdthInch = OrigWdthInch;
            OverHghtFeet = feet;
            OverWdthFeet = OrigWdthFeet;

            double hghtInTotlInchs = (double)(OverHghtFeet.Value * 12 + OverHghtInch.Value) / drawingScaleInInches;
            double wdthInTotlInchs = (double)(OverWdthFeet.Value * 12 + OverWdthInch.Value) / drawingScaleInInches;

            Cut.OverrideCutRectangle = new Geometry.Rectangle()
            {
                Angle = 0
                , Offset = new Coordinate(0, 0)
                , UpperLeft = new Coordinate(0, hghtInTotlInchs)
                , LowerRght = new Coordinate(wdthInTotlInchs, 0)
            };

        }

        private void resetOverrideRectangle()
        {
            Cut.OverrideCutRectangle = null;
        }

        public string Index => this.lblNmbr.Text.Trim();

        public new string Width => this.lblWdth.Text.Trim();

        public string Length => this.txbLnth.Text.Trim();

        private void txbRepeats_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txbRepeats.Text))
            {
                Cut.PatternRepeats = 0M;

                this.txbRepeats.BackColor = SystemColors.ControlLightLight;
            }

            else if (!Utilities.IsValidSingleDigitFrac(this.txbRepeats.Text))
            {
                this.txbRepeats.BackColor = Color.Pink;

                Cut.PatternRepeats = 0M;
            }

            else
            {
                this.txbRepeats.BackColor = SystemColors.ControlLightLight;
                Cut.PatternRepeats = Decimal.Parse(this.txbRepeats.Text);
            }
        }


        private void UCCutsOversUndersFormElement_Disposed(object sender, EventArgs e)
        {

            this.txbLnth.TextChanged -= TxbLnth_TextChanged;
            this.lblNmbr.Click -= LblNmbr_Click;
            this.lblX.Click -= LblX_Click;
            this.Click -= UCCutsOversUndersFormElement_Click;
        }

    }
}
