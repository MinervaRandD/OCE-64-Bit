//-------------------------------------------------------------------------------//
// <copyright file="OversUndersForm.cs"                                          //
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


namespace MaterialsLayout
{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using OversUnders;
    using Utilities;

    /// <summary>
    /// OversUndersForm displays the cuts, overs and unders from a seaming operation
    /// </summary>
    public partial class OversUndersForm : Form
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public OversUndersForm()
        {
            InitializeComponent();
        }

        List<Cut> cutList = new List<Cut>();
        List<Undrage> undrageList = new List<Undrage>();

        double drawingScale;
        int rollWidthInInches;
        /// <summary>
        /// Updates the form with new values for cuts, overs and unders.
        /// </summary>
        /// <param name="layoutAreaList">A list of layout areas to display</param>
        /// <param name="drawingScale">The current drawing scale</param>
        public void Update(List<LayoutArea> layoutAreaList, double drawingScale, int rollWidthInInches)
        {
            this.drawingScale = drawingScale;
            this.rollWidthInInches = rollWidthInInches;

            cutList.Clear();
            undrageList.Clear();

            foreach (LayoutArea layoutArea in layoutAreaList)
            {
                cutList.AddRange(layoutArea.CutList);
                undrageList.AddRange(layoutArea.UndrageList);
            }

            buildCutsList(cutList, drawingScale);
            buildOversList(cutList, drawingScale);
            buildUndrsList(undrageList, drawingScale);
        }

        /// <summary>
        /// Build the cuts list and place values on the form.
        /// </summary>
        /// <param name="cutList">The list of cuts to display</param>
        /// <param name="drawingScale"></param>
        private void buildCutsList(List<Cut> cutList, double drawingScale)
        {
            this.pnlCuts.Controls.Clear();

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = 1;

            foreach (Cut cut in cutList)
            {
                UCOversUndersFormElement oversUndersFormElement = new UCOversUndersFormElement();

                oversUndersFormElement.Init(count.ToString(), cut, drawingScale);

                this.pnlCuts.Controls.Add(oversUndersFormElement);

                oversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);
                oversUndersFormElement.Size = new Size(165, 24);

                cntlPosnY += oversUndersFormElement.Height;

                count++;
            }
        }

        /// <summary>
        /// Build the overs list and places values on the form.
        /// </summary>
        /// <param name="cutList">List of cuts to display</param>
        /// <param name="drawingScale">Current drawing scale</param>
        private void buildOversList(List<Cut> cutList, double drawingScale)
        {
           this.pnlOvers.Controls.Clear();

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = 0;

            foreach (Cut cut in cutList)
            {
                foreach (Overage overage in cut.OverageList)
                {
                    UCOversUndersFormElement oversUndersFormElement = new UCOversUndersFormElement();

                    oversUndersFormElement.Init(((char) ('a' + count)).ToString(), overage, drawingScale);

                    this.pnlOvers.Controls.Add(oversUndersFormElement);

                    oversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                    cntlPosnY += oversUndersFormElement.Height;

                    count++;
                }
            }
        }

        /// <summary>
        /// Builds the unders list and places values on the form.
        /// </summary>
        /// <param name="undrageList">List of unders to display</param>
        /// <param name="drawingScale">Current drawing scale</param>
        private void buildUndrsList(List<Undrage> undrageList, double drawingScale)
        {
            this.pnlUndrs.Controls.Clear();

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = 0;

            foreach (Undrage undrage in undrageList)
            {
                UCOversUndersFormElement oversUndersFormElement = new UCOversUndersFormElement();

                oversUndersFormElement = new UCOversUndersFormElement();

                oversUndersFormElement.Init(((char)('A' + count)).ToString(), undrage, drawingScale);

                this.pnlUndrs.Controls.Add(oversUndersFormElement);

                oversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += oversUndersFormElement.Height;

                count++;
            }
        }

        private OversUnders oversUnders;

        private List<MaterialArea> oversList = new List<MaterialArea>();

        private List<MaterialArea> undrsList = new List<MaterialArea>();

        /// <summary>
        /// Does the fills routine. Note that the cut list, unders list and drawing scale are taken from the most recent 'Update'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoFills_Click(object sender, System.EventArgs e)
        {
            oversList.Clear();
            undrsList.Clear();

            if (Utilities.IsNotNull(this.cutList))
            {
                foreach (Cut cut in this.cutList)
                {
                    if (Utilities.IsNotNull(cut.OverageList))
                    {
                        foreach (Overage overage in cut.OverageList)
                        {
                            int hght = (int)Math.Round(overage.EffectiveDimensions.Item1 * drawingScale);
                            int wdth = (int)Math.Round(overage.EffectiveDimensions.Item2 * drawingScale);

                            MaterialArea m = new MaterialArea(MaterialAreaType.Over, wdth, hght);

                            oversList.Add(m);
                        }
                    }
                }
            }

            if (Utilities.IsNotNull(undrageList))
            {
                foreach (Undrage undrage in undrageList)
                {

                    int hght = (int)Math.Round(undrage.EffectiveDimensions.Item1 * drawingScale);
                    int wdth = (int)Math.Round(undrage.EffectiveDimensions.Item2 * drawingScale);

                    MaterialArea m = new MaterialArea(MaterialAreaType.Undr, wdth, hght);

                    undrsList.Add(m);
                }
            }
            
            oversUnders = new OversUnders(oversList, undrsList);

            int fillWdthInInches = oversUnders.GenerateWasteEstimate(rollWidthInInches);

            int fillWdthInch = fillWdthInInches % 12;
            int fillWdthFeet = (fillWdthInInches - fillWdthInInches) / 12;

            string fillWdthLbl = fillWdthFeet.ToString() + "' " + fillWdthInch.ToString() + '"';

            int fillHghtInch = rollWidthInInches % 12;
            int fillHghtFeet = (rollWidthInInches - fillHghtInch) / 12;

            string fillHghtLbl = fillHghtFeet.ToString() + "' " + fillHghtInch.ToString() + '"';

            this.lblFillWidth.Text = fillWdthLbl;
            this.lblFillHeight.Text = fillHghtLbl;


        }
    }
}
