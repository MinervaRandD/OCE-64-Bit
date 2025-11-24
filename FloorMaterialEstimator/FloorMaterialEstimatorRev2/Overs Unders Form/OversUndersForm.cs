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


using Globals;

namespace FloorMaterialEstimator.OversUndersForm
{

    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using MaterialsLayout;
    using OversUnders;
    using Utilities;
    using FloorMaterialEstimator;
    using FloorMaterialEstimator.Finish_Controls;
    using FloorMaterialEstimator.CanvasManager;
    using OversUndersLib;
    using SettingsLib;
    using DebugSupport;
    using System.Linq;
   // using OversUndersLibOldVersion;
    using Geometry;
    using StatsGeneratorLib;
    using FinishesLib;

    /// <summary>
    /// OversUndersForm displays the cuts, overs and unders from a seaming operation
    /// </summary>
    public partial class OversUndersForm : Form
    {

        FloorMaterialEstimatorBaseForm baseForm;

        public AreaFinishManager  areaFinishManager => FinishManagerGlobals.SelectedAreaFinishManager;

        //public AreaFinishBase areaFinishBase => areaFinishManager.AreaFinishBase;

        UCFillSummaryElement fillSummaryElement = null;
        UCSmallFillElement smallFillElement = null;
        UCRptsElement rptsElement = null;
        UCCutsExtraFormElement cutsExtraElement = null;
        UCFillSummaryElement summaryElementTotal = null;

        // values to switch from shortened to fullsize etc
        private int oversXPositionOriginal;
        private int undersXPositionOriginal;
        private int oversUndersYPositionOriginal;
        private Size formSizeOriginal;
        private const int oversXPositionShortened = 10;
        private const int oversUndersYPositionShortened = 10;
        private bool FormIsFullSize = true;

        
        string defaultTitleText;

        double drawingScaleInFeet => baseForm.CurrentPage.DrawingScaleInInches / 12.0;

        double drawingScaleInInches => baseForm.CurrentPage.DrawingScaleInInches;

        //double netAreaInSqrInches => baseForm.selectedAreaFinish.AreaFinishBase.NetAreaInSqrInches;


        double? _smallFillPieceGrossArea = null;

        double? smallFillPieceGrossAreaInFeet
        {
            get
            {
                return _smallFillPieceGrossArea;
            }

            set
            {
                _smallFillPieceGrossArea = value;

                if (_smallFillPieceGrossArea.HasValue)
                {
                    this.lblSmallFillGrossArea.Text = _smallFillPieceGrossArea.Value.ToString("#,##0.0");
                }

                else
                {
                    this.lblSmallFillGrossArea.Text = string.Empty;
                }
            }
        }


        double? _totalSmallUndrageAreaInFeet = null;

        double? totalSmallUndrageAreaInFeet
        {
            get
            {
                return _totalSmallUndrageAreaInFeet;
            }

            set
            {
                _totalSmallUndrageAreaInFeet = value;

                if (_totalSmallUndrageAreaInFeet.HasValue)
                {
                    this.lblSmallFillNetArea.Text = _totalSmallUndrageAreaInFeet.Value.ToString("#,##0.0");
                }

                else
                {
                    this.lblSmallFillNetArea.Text = string.Empty;
                }
            }
        }

        double? _largeFillPieceArea = null;

        double? largeFillPieceArea
        {
            get
            {
                return _largeFillPieceArea;
            }

            set
            {
                _largeFillPieceArea = value;

                if (_largeFillPieceArea.HasValue)
                {
                    this.lblLargeAreaFillPiece.Text = _largeFillPieceArea.Value.ToString("#,##0.0");
                }

                else
                {
                    this.lblLargeAreaFillPiece.Text = string.Empty;
                }
            }
        }

        double? _totalLargeUndrageArea = null;

        double? totalLargeUndrageArea
        {
            get
            {
                return _totalLargeUndrageArea;
            }

            set
            {
                _totalLargeUndrageArea = value;

                if (_totalLargeUndrageArea.HasValue)
                {
                    this.lblLargeFillNetArea.Text = _totalLargeUndrageArea.Value.ToString("#,##0.0");
                }

                else
                {
                    this.lblLargeFillNetArea.Text = string.Empty;
                }
            }
        }

        double? _largeFillWastePercent = null;

        double? largeFillWastePercent
        {
            get
            {
                return _largeFillWastePercent;
            }

            set
            {
                _largeFillWastePercent = value;

                if (_largeFillWastePercent.HasValue)
                {
                    this.lblLargeFillWastePercent.Text = (_largeFillWastePercent.Value * 100.0).ToString("0.0") + '%';
                }

                else
                {
                    this.lblLargeFillWastePercent.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public OversUndersForm(FloorMaterialEstimatorBaseForm baseForm = null)
        {
            InitializeComponent();

            FloorMaterialEstimator.CanvasManager.FinishManagerGlobals.UpdateOversUndersStats = this.UpdatePanelDisplayForSelectedElementNumber;

            this.defaultTitleText = this.Text;

            this.tbNotes.BackColor = this.BackColor;

            this.baseForm = baseForm;

            this.summaryElementTotal = new UCFillSummaryElement();
            
            this.summaryElementTotal.SetTitle("Gross");

            OversUndersCommon.Form2ElementSetup(
                summaryElementTotal
                , summaryElementTotal.lblTitle
                , summaryElementTotal.lblWdth
                , summaryElementTotal.lblX
                , summaryElementTotal.lblLnth
            );

            this.fillSummaryElement = new UCFillSummaryElement();

            OversUndersCommon.Form2ElementSetup(
                fillSummaryElement
                , fillSummaryElement.lblTitle
                , fillSummaryElement.lblWdth
                , fillSummaryElement.lblX
                , fillSummaryElement.lblLnth
            );

            this.cutsExtraElement = new UCCutsExtraFormElement();
            this.cutsExtraElement.Init(this);

            OversUndersCommon.Form2ElementSetup(
                cutsExtraElement
                , cutsExtraElement.lblTitle
                , cutsExtraElement.lblWdth
                , cutsExtraElement.lblX
                , cutsExtraElement.txbLnth
            );

            this.smallFillElement = new UCSmallFillElement();

            OversUndersCommon.Form2ElementSetup(
                smallFillElement
                , smallFillElement.lblTitle
                , smallFillElement.lblWdth
                , smallFillElement.lblX
                , smallFillElement.lblLnth
            );

            this.rptsElement = new UCRptsElement();

            OversUndersCommon.Form2ElementSetup(
                rptsElement
                , rptsElement.lblTitle
                , rptsElement.lblWdth
                , rptsElement.lblX
                , rptsElement.lblLngth
            );

            this.summaryElementTotal.SetFont(new Font(this.summaryElementTotal.Font, FontStyle.Bold));

            this.txbSmallFillsWastePct.Text = (GlobalSettings.SmallFillWastePercent * 100.0).ToString("0.0");

            this.txbSmallFillsWastePct.TextChanged += TxbSmallFillsWastePct_TextChanged;

            this.tbNotes.BackColor = SystemColors.ControlLightLight;

            this.oversXPositionOriginal = this.gbxOvers.Location.X;
            this.undersXPositionOriginal = this.grbUnders.Location.X;
            this.oversUndersYPositionOriginal = this.gbxOvers.Location.Y;
            this.formSizeOriginal = this.Size;

            this.lblGrossAreaTextShortened.Visible = false;
            this.lblGrossAreaShortened.Visible = false;

            this.FormClosing += OversUndersForm_FormClosing;
        }

        private void OversUndersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void TxbSmallFillsWastePct_TextChanged(object sender, EventArgs e)
        {
            Utilities.SetTextFormatForValidPositiveDouble(txbSmallFillsWastePct);
            
            if (txbSmallFillsWastePct.BackColor == SystemColors.ControlLightLight)
            {
                double smallFillWastePct = double.Parse(txbSmallFillsWastePct.Text.Trim()) * 0.01;

                if (totalSmallUndrageAreaInFeet.HasValue)
                {
                    smallFillPieceGrossAreaInFeet = (1.0 + smallFillWastePct) * totalSmallUndrageAreaInFeet.Value;
                }

                GlobalSettings.SmallFillWastePercent = smallFillWastePct;
            }

            else
            {
                smallFillPieceGrossAreaInFeet = null;
            }
        }

        // The following should be at the canvas level
        // TODO: Create canvas levels cuts, overs and unders and change the following code

        List<Undrage> smallUndrageList = new List<Undrage>();
        List<Overage> smallOverageList = new List<Overage>();

        List<GraphicsCut> graphicsCutList = new List<GraphicsCut>();
        List<Undrage> undrageList = new List<Undrage>();
        List<Overage> overageList = new List<Overage>();
        //List<VirtualUndrage> virtualUndrageList = new List<VirtualUndrage>();
        // List<VirtualOverage> virtualOverageList = new List<VirtualOverage>();

        List<VirtualOverage> virtualOverageList => this.areaFinishManager.VirtualOverageList;
        List<VirtualUndrage> virtualUndrageList => this.areaFinishManager.VirtualUndrageList;


        List<VirtualCut> virtualCutsList = new List<VirtualCut>();


        int rollWidthInInches;
        double rollWidthInFeet;

        List<CanvasLayoutArea> layoutAreaList = new List<CanvasLayoutArea>();
        public String Notes { get; set; }

        //AreaFinishManager SelectedElement = null;

        /// <summary>
        /// Updates the form with new values for cuts, overs and unders.
        /// </summary>
        /// <param name="layoutAreaList">A list of layout areas to display</param>
        /// <param name="drawingScale">The current drawing scale</param>
        
        public void UpdatePanelDisplayForSelectedElementNumber(int selectedElementNumber)
        {
            AreaFinishManager selectedElement = FinishManagerGlobals.AreaFinishManagerList[selectedElementNumber];

            UpdatePanelDisplay(selectedElement, true);
        }

        public void UpdatePanelDisplay(AreaFinishManager selectedElement, bool doUpdateTotals)
        {
            layoutAreaList.Clear();


            this.Text = this.defaultTitleText + " - " + selectedElement.AreaName;

            this.rollWidthInInches = (int) Math.Round(selectedElement.RollWidthInInches);

            foreach (CanvasLayoutArea canvasLayoutArea in selectedElement.CanvasLayoutAreas)
            {
                if (canvasLayoutArea.LayoutAreaType == LayoutAreaType.Normal || canvasLayoutArea.LayoutAreaType == LayoutAreaType.OversGenerator)
                {
                    layoutAreaList.Add(canvasLayoutArea);
                }
            }

            smallUndrageList.Clear();
            smallOverageList.Clear();

            ClearCutList();
            undrageList.Clear();
            overageList.Clear();
            //virtualUndrageList.Clear();
            //virtualOverageList.Clear();
            virtualCutsList.Clear();

            //selectedElement.VirtualOverageList.Clear();
            //selectedElement.VirtualUndrageList.Clear();
            selectedElement.VirtualCutsList.Clear();

            this.pnlCuts.Controls.Clear();
            this.pnlCutsTotals.Controls.Clear();
            this.pnlOvers.Controls.Clear();
            this.pnlUndrs.Controls.Clear();

            generateMaterialsLists(layoutAreaList);

            

            //virtualOverageList.AddRange(areaFinishManager.VirtualOverageList);
            //virtualUndrageList.AddRange(areaFinishManager.VirtualUndrageList);
       
            buildCutsPanel(graphicsCutList, drawingScaleInInches);
            buildOversPanel(overageList, drawingScaleInInches);
            buildUndrsPanel(undrageList, drawingScaleInInches);
            buildVirtualOversPanel(virtualOverageList, drawingScaleInInches);
            buildVirtualUndrsPanel(virtualUndrageList, drawingScaleInInches);
            buildVirtualCutsPanel(virtualCutsList, drawingScaleInInches);

            setupLengthRepeats(selectedElement, graphicsCutList);

            ////setupTotals(graphicsCutList, rollWidthInInches, drawingScaleInInches);

            //lblFillHeight.Text = string.Empty;
            //lblFillWidth.Text = string.Empty;


            largeFillPieceArea = null;
            totalLargeUndrageArea = null;

            this.lblLargeAreaFillPiece.Text = string.Empty;
            this.lblLargeFillNetArea.Text = string.Empty;
            this.lblLargeFillWastePercent.Text = string.Empty;
             
            largeFillPieceArea = null;
            totalLargeUndrageArea = null;
            largeFillWastePercent = null;

            //totalFillPiece = null;
            //totalUndrageArea = null;
            //totalWastePercent = null;

            if (doUpdateTotals)
            {
                UpdateTotals(selectedElement);
            }

            else
            {
                this.btnDoFills.BackColor = Color.Orange;
            }

            this.cutsExtraElement.Setup();

            //areaFinishManager.CutList = this.graphicsCutList;

        }

        private void setupLengthRepeats(AreaFinishManager selectedElement, List<GraphicsCut> graphicsCutList)
        {
            string lRepeatsFormatted = FormatUtils.FormatInchesToFeetAndInches(selectedElement.RollRepeatLengthInInches);

            decimal totalRepeats = graphicsCutList.Sum(c => c.PatternRepeats);

            this.lblLengthRepeat.Text = "Length repeat: " + lRepeatsFormatted + " X " + totalRepeats.ToString();

        }

        private void ClearCutList()
        {
            foreach (Cut cut in graphicsCutList)
            {
                cut.PropertyChanged -= Cut_PropertyChanged;
            }

            graphicsCutList.Clear();
        }

        private void generateMaterialsLists(List<CanvasLayoutArea> layoutAreaList)
        {
            foreach (GraphicsLayoutArea layoutArea in layoutAreaList)
            {
                if (GlobalSettings.ValidateRolloutAndCutWidth)
                {
                    if (!DebugChecks.ValidateRolloutsAndCuts(layoutArea, rollWidthInInches, drawingScaleInInches))
                    {
                        MessageBox.Show("Invalid rollout or cuts width found.");
                    }
                }

                if (layoutArea.LayoutAreaType == LayoutAreaType.OversGenerator)
                {
                    double width = 0;
                    double lngth = 0;
                    double count = 0;

                    foreach (GraphicsOverage overage in layoutArea.EmbeddedOversList)
                    {
                        lngth += overage.BoundingRectangle.Width;
                        width += overage.BoundingRectangle.Height;

                        count++;
                    }

                    if (count > 0)
                    {
                        width /= count;

                        Coordinate upperLeft = new Coordinate(0, width);
                        Coordinate lowerRght = new Coordinate(lngth, 0);

                        Geometry.Rectangle boundingRectangle = new Geometry.Rectangle(upperLeft, lowerRght);

                        Overage compositeOverage = new Overage(boundingRectangle);

                        compositeOverage.OverageIndex = layoutArea.EmbeddedOversList[0].OverageIndex;

                        compositeOverage.EffectiveDimensions = new Tuple<double, double>(lngth, width);

                        overageList.Add(compositeOverage);
                    }
                }

                else if (layoutArea.IsBorderArea)
                {

                    if (Utilities.IsNotNull(layoutArea.BorderAreaUndrage))
                    {
                        double width = layoutArea.BorderAreaUndrage.EffectiveDimensions.Item1 * drawingScaleInFeet;
                        double lngth = layoutArea.BorderAreaUndrage.EffectiveDimensions.Item2 * drawingScaleInFeet;

                        if (width < 1.0 || lngth < 1.0)
                        {
                            smallUndrageList.Add(layoutArea.BorderAreaUndrage);
                        }

                        else
                        {
                            undrageList.Add(layoutArea.BorderAreaUndrage);
                        }
                    }

                    if (Utilities.IsNotNull(layoutArea.BorderAreaOverage))
                    {
                        double width = layoutArea.BorderAreaOverage.EffectiveDimensions.Item1 * drawingScaleInFeet;
                        double lngth = layoutArea.BorderAreaOverage.EffectiveDimensions.Item2 * drawingScaleInFeet;

                        if (width < 1.0 || lngth < 1.0)
                        {
                            smallOverageList.Add(layoutArea.BorderAreaOverage);
                        }

                        else
                        {
                            overageList.Add(layoutArea.BorderAreaOverage);
                        }
                    }
                }

                else if (layoutArea.LayoutAreaType == LayoutAreaType.Normal)
                {
                    foreach (Undrage undrage in layoutArea.UndrageList)
                    {
                        double width = Math.Round(undrage.BoundingRectangle.Width * drawingScaleInFeet, 3);
                        double lngth = Math.Round(undrage.BoundingRectangle.Height * drawingScaleInFeet, 3);

                        if (width < 1.0 || lngth < 1.0)
                        {
                            smallUndrageList.Add(undrage);
                        }

                        else
                        {
                            undrageList.Add(undrage);
                        }
                    }

                    foreach (GraphicsCut graphicsCut in layoutArea.GraphicsCutList)
                    {
                        graphicsCutList.Add(graphicsCut);

                        graphicsCut.PropertyChanged += Cut_PropertyChanged;

                        foreach (Overage overage in graphicsCut.OverageList)
                        {
                            double width = overage.BoundingRectangle.Width * drawingScaleInFeet;
                            double lngth = overage.BoundingRectangle.Height * drawingScaleInFeet;

                            if (width < 1.0 || lngth < 1.0)
                            {
                                smallOverageList.Add(overage);
                            }

                            else
                            {
                                overageList.Add(overage);
                            }
                        }
                    }
                }
            }

            graphicsCutList.Sort((c1, c2) => c1.CutIndex - c2.CutIndex);

            totalSmallUndrageAreaInFeet = null;

            if (smallUndrageList.Count > 0)
            {
                totalSmallUndrageAreaInFeet = 0;

                foreach (Undrage undrage in smallUndrageList)
                {
                    double width = undrage.BoundingRectangle.Width * drawingScaleInFeet;
                    double lngth = undrage.BoundingRectangle.Height * drawingScaleInFeet;

                    totalSmallUndrageAreaInFeet += width * lngth;
                }
            }

            smallFillPieceGrossAreaInFeet = totalSmallUndrageAreaInFeet * (1.0 + GlobalSettings.SmallFillWastePercent);
        }

        private void Cut_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            doUpdateTotals(FinishManagerGlobals.SelectedAreaFinishManager);
        }

        private void setupTotals(
            AreaFinishManager selectedAreaFinishManager
            , List<GraphicsCut> graphicsCutList
            , double smallFillElementWidthInInches
            , double rollWidthInInches
            , double drawingScale)
        {
            if (selectedAreaFinishManager.MaterialsType != MaterialsType.Rolls)
            {
                return;
            }

            int extraInchesPerCut = FinishManagerGlobals.SelectedAreaFinishManager.ExtraInchesPerCut;

            // The following has been changed in that it assumes that all cuts are equal to the roll width.

            double averageCount = 0;

            double totalLengthInFeet = 0;

            int totalExtraInches = 0;

            decimal totalRepeats = 0M;

            foreach (Cut cut in graphicsCutList)
            {
                if (cut.Deleted)
                {
                    continue;
                }

                if (cut.ShapeHasBeenOverridden)
                {
                    //averageWidthInFeet += graphicsCut.OverrideCutRectangle.Width; // This is mixed up. Should be corrected.
                    averageCount++;
                    totalLengthInFeet += cut.OverrideCutRectangle.Height;
                }

                else
                {
                    //averageWidthInFeet += graphicsCut.MaterialWidth; // This is mixed up. Should be corrected.
                    averageCount++;
                    totalLengthInFeet += cut.CutRectangle.Width ;
                    totalExtraInches += extraInchesPerCut;
                    totalRepeats += cut.PatternRepeats;

                    double actlWidth = cut.CutRectangle.Width * drawingScale;
                }
            }

            if (largeFillPieceArea.HasValue)
            {
                if (largeFillPieceArea.Value > 0)
                {
                    totalLengthInFeet += largeFillPieceArea.Value / (rollWidthInInches / 12) / drawingScale ;
                   // totalExtraInches += extraInchesPerCut;
                }

                averageCount++;
            }

            if (cutsExtraElement.Value() > 0)
            {
                totalLengthInFeet += cutsExtraElement.Value() / drawingScale / 12.0 ;
               // totalExtraInches += extraInchesPerCut;
            }

            if (smallFillElementWidthInInches > 0)
            {
                totalExtraInches += extraInchesPerCut;
            }

            double widthInFeet = rollWidthInInches / 12.0; // drawingScale * averageWidthInFeet;
            double lngthInFeet = drawingScale * totalLengthInFeet + smallFillElementWidthInInches / 12.0;

            int hghtInTotlInch = (int)Math.Round(lngthInFeet * 12.0) + totalExtraInches;
            int wdthInTotlInch = (int)Math.Round(widthInFeet * 12.0);

            int rollRepatInches = (int)selectedAreaFinishManager.AreaFinishBase.RollRepeatLengthInInches;
            int repeatLengthInch = (int)Math.Round(rollRepatInches * totalRepeats);

            this.rptsElement.Update(wdthInTotlInch, repeatLengthInch);

            hghtInTotlInch += repeatLengthInch;

            this.summaryElementTotal.Update(wdthInTotlInch, hghtInTotlInch);

            double? totlGrossAreaInSquareFeet = (double) (hghtInTotlInch * wdthInTotlInch) / 144.0;

            double? totlGrossAreaInSquareFeet1 = selectedAreaFinishManager.AreaFinishBase.GrossAreaInSqrInches / 144;

            if (totlGrossAreaInSquareFeet <= 0)
            {
                totlGrossAreaInSquareFeet = null;
            }

            double totlNetAreaInSquareFeet = 0;

            totlNetAreaInSquareFeet = selectedAreaFinishManager.AreaFinishBase.NetAreaInSqrInches /144.0;

            if (totlGrossAreaInSquareFeet.HasValue)
            {
                this.lblGrossArea.Text = totlGrossAreaInSquareFeet.Value.ToString("#,##0.0");
                this.lblGrossAreaShortened.Text = totlGrossAreaInSquareFeet.Value.ToString("#,##0.0");
            }

            else
            {
                this.lblGrossArea.Text = "N/A";
                this.lblGrossAreaShortened.Text = "N/A";
            }
           

            this.lblTotalNetArea.Text = totlNetAreaInSquareFeet.ToString("#,##0.0");

            this.lblTotalLength.Text = FormatUtils.FormatInchesToFeetAndInches(hghtInTotlInch);

            this.lblTotalWastePct.ForeColor = Color.Black;

            //this.lblTotalWastePercent.Text =
            if (totlNetAreaInSquareFeet > 0 && totlGrossAreaInSquareFeet.HasValue)
            {
                double totlWastePercent = totlGrossAreaInSquareFeet.Value / totlNetAreaInSquareFeet - 1.0;

                if (totlWastePercent < 0)
                {
                    this.lblTotalWastePct.ForeColor = Color.Red;
                }

                this.lblTotalWastePct.Text = (totlWastePercent * 100.0).ToString("0.0");
            }

            else
            {
                this.lblTotalWastePct.Text = "N/A";
            }

            setupLengthRepeats(selectedAreaFinishManager, this.graphicsCutList);

            selectedAreaFinishManager.GrossAreaInSqrInches = totlGrossAreaInSquareFeet * 144.0;
            selectedAreaFinishManager.NetAreaInSqrInches = totlNetAreaInSquareFeet * 144.0;

            this.Notes = baseForm.Notes;
        }

        /// <summary>
        /// Build the cuts list and place values on the form.
        /// </summary>
        /// <param name="graphicsCutList">The list of cuts to display</param>
        /// <param name="drawingScaleInInInches"></param>
        
        private void buildCutsPanel(List<GraphicsCut> graphicsCutList, double drawingScaleInInInches)
        {
 
            int cntlPosnX = 8;
            int cntlPosnY = 12;

            int count = 0;

            foreach (GraphicsCut graphicsCut in graphicsCutList)
            {
                if (graphicsCut.Deleted)
                {
                    continue;
                }

                UCCutsOversUndersFormElement cutsOversUndersFormElement = new UCCutsOversUndersFormElement();

                int extraInchesPerCut = FinishManagerGlobals.SelectedAreaFinishManager.ExtraInchesPerCut;

                cutsOversUndersFormElement.Init(this, graphicsCut, drawingScaleInInInches, rollWidthInInches, extraInchesPerCut);

                this.pnlCuts.Controls.Add(cutsOversUndersFormElement);

                cutsOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += cutsOversUndersFormElement.Height;

                count++;
            }

            this.pnlCuts.Invalidate();

            BuildCutsPanelTotal();

            this.pnlCutsTotals.Invalidate();
        }

        private void BuildCutsPanelTotal()
        {
            int cntlPosnX = 8;
            int cntlPosnY = 4;

            this.pnlCutsTotals.Controls.Add(this.fillSummaryElement);
            this.fillSummaryElement.Location = new Point(cntlPosnX, cntlPosnY);

            cntlPosnY += fillSummaryElement.Height;

            this.pnlCutsTotals.Controls.Add(this.smallFillElement);
            this.smallFillElement.Location = new Point(cntlPosnX, cntlPosnY);
            
            cntlPosnY += smallFillElement.Height;

            this.pnlCutsTotals.Controls.Add(this.cutsExtraElement);
            this.cutsExtraElement.Location = new Point(cntlPosnX, cntlPosnY);

            cntlPosnY += cutsExtraElement.Height + 4 ;

            this.pnlCutsTotals.Controls.Add(this.summaryElementTotal);
            this.summaryElementTotal.Location = new Point(cntlPosnX, cntlPosnY);

            cntlPosnY += summaryElementTotal.Height + 4;

            this.pnlCutsTotals.Controls.Add(this.rptsElement);
            this.rptsElement.Location = new Point(cntlPosnX, cntlPosnY);
           
        }

        public void ResetTotals()
        {
            setupTotals(FinishManagerGlobals.SelectedAreaFinishManager, graphicsCutList, 0, rollWidthInInches, drawingScaleInFeet);
        }

        /// <summary>
        /// Build the overs list and places values on the form.
        /// </summary>
        /// <param name="cutList">List of cuts to display</param>
        /// <param name="drawingScale">Current drawing scale</param>
        private void buildOversPanel(List<Overage> overageList, double drawingScale)
        {
            List<Overage> loclOverageList = new List<Overage>(overageList);

            loclOverageList.Sort((o1, o2) => o1.OverageIndex - o2.OverageIndex);

           this.pnlOvers.Controls.Clear();

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = 0;

            int extraInchesPerCut = FinishManagerGlobals.SelectedAreaFinishManager.ExtraInchesPerCut;

            foreach (Overage overage in loclOverageList)
            {
                if (overage.Deleted)
                {
                    continue;
                }

                UCOversOversUndersFormElement oversOversUndersFormElement = new UCOversOversUndersFormElement();

                oversOversUndersFormElement.Init(this, overage, drawingScale, extraInchesPerCut);

                this.pnlOvers.Controls.Add(oversOversUndersFormElement);

                oversOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += oversOversUndersFormElement.Height;

                count++;
            }
        }

        /// <summary>
        /// Builds the unders list and places values on the form.
        /// </summary>
        /// <param name="undrageList">List of unders to display</param>
        /// <param name="drawingScaleInInches">Current drawing scale</param>
        private void buildUndrsPanel(List<Undrage> undrageList, double drawingScaleInInches)
        {
            List<Undrage> loclUndrageList = new List<Undrage>(undrageList);

            loclUndrageList.Sort((u1, u2) => u1.UndrageIndex - u2.UndrageIndex);

            this.pnlUndrs.Controls.Clear();

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = 0;

            int extraInchesPerCut = FinishManagerGlobals.SelectedAreaFinishManager.ExtraInchesPerCut;

            foreach (Undrage undrage in loclUndrageList)
            {
                if (undrage.Deleted)
                {
                    continue;
                }

                UCUndrsOversUndersFormElement undrsOversUndersFormElement = new UCUndrsOversUndersFormElement();

                undrsOversUndersFormElement.Init(this, undrage, drawingScaleInInches, extraInchesPerCut);

                this.pnlUndrs.Controls.Add(undrsOversUndersFormElement);

                undrsOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += undrsOversUndersFormElement.Height;

                count++;
            }
        }

        public void SetDoFillsBackgroundColor()
        {

            currOversList.Clear();
            currUndrsList.Clear();

            generateMaterialsLists(currOversList, currUndrsList, layoutAreaList);

            if (currOversList.Count != prevOversList.Count)
            {
                setDoFillsActive();
               
                return;
            }

            if (currUndrsList.Count != prevUndrsList.Count)
            {
                setDoFillsActive();
               
                return;
            }

            for (int i = 0; i < currOversList.Count; i++)
            {
                if (currOversList[i].CompareTo(prevOversList[i]) != 0)
                {
                    setDoFillsActive();

                    return;
                }
            }

            for (int i = 0; i < currUndrsList.Count; i++)
            {
                if (currUndrsList[i].CompareTo(prevUndrsList[i]) != 0)
                {
                    setDoFillsActive();

                    return;
                }
            }

            if (this.RbnUpdateFillsAutomatically.Checked)
            {
                this.UpdateTotals(FinishManagerGlobals.SelectedAreaFinishManager);
            }

            this.btnDoFills.BackColor = SystemColors.ControlLightLight;
        }

        public void ClearFillsDisplay()
        {
            if (this.RbnUpdateFillsAutomatically.Checked)
            {
                largeFillPieceArea = null;
                totalLargeUndrageArea = null;
                largeFillWastePercent = null;

                //totalFillPiece = null;
                //totalUndrageArea = null;
                //totalWastePercent = null;
            }
        }

        private void setDoFillsActive()
        {
            largeFillPieceArea = null;
            totalLargeUndrageArea = null;
            largeFillWastePercent = null;

            //totalFillPiece = null;
            //totalUndrageArea = null;
            //totalWastePercent = null;

            if (this.RbnUpdateFillsAutomatically.Checked)
            {
                UpdateTotals(FinishManagerGlobals.SelectedAreaFinishManager);
            }

            else
            {
                this.btnDoFills.BackColor = Color.Orange;
            }

        }

        private void buildVirtualOversPanel(List<VirtualOverage> virtualOverageList, double drawingScaleInInches)
        {
            if (virtualOverageList.Count <= 0)
            {
                return;
            }

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = this.pnlOvers.Controls.Count;

            if (count > 0)
            {
                Point lastCntlLoc = this.pnlOvers.Controls[count - 1].Location;

                cntlPosnX = lastCntlLoc.X;
                cntlPosnY = lastCntlLoc.Y + this.pnlOvers.Controls[count - 1].Height;
            }

            foreach (VirtualOverage overage in virtualOverageList)
            {
                UCVirtualOversOversUndersFormElement oversOversUndersFormElement = new UCVirtualOversOversUndersFormElement();

                oversOversUndersFormElement.Init(this, count, overage, drawingScaleInInches);

                this.pnlOvers.Controls.Add(oversOversUndersFormElement);

                oversOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += oversOversUndersFormElement.Height;

                count++;
            }
        }

        private void buildVirtualUndrsPanel(List<VirtualUndrage> virtualUndrageList, double drawingScaleInInches)
        {
            if (virtualUndrageList.Count <= 0)
            {
                return;
            }

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = this.pnlUndrs.Controls.Count;

            if (count > 0)
            {
                Point lastCntlLoc = this.pnlUndrs.Controls[count - 1].Location;

                cntlPosnX = lastCntlLoc.X;
                cntlPosnY = lastCntlLoc.Y + this.pnlUndrs.Controls[count - 1].Height;
            }

            foreach (VirtualUndrage undrage in virtualUndrageList)
            {
                UCVirtualUndrsOversUndersFormElement undrsOversUndersFormElement = new UCVirtualUndrsOversUndersFormElement();

                undrsOversUndersFormElement.Init(this, count, undrage, drawingScaleInInches);

                this.pnlUndrs.Controls.Add(undrsOversUndersFormElement);

                undrsOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += undrsOversUndersFormElement.Height;

                count++;
            }
        }

        private void buildVirtualCutsPanel(List<VirtualCut> virtualCutList, double drawingScaleInInches)
        {
            if (virtualCutsList.Count <= 0)
            {
                return;
            }

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = this.pnlCuts.Controls.Count;

            if (count > 0)
            {
                Point lastCntlLoc = this.pnlCuts.Controls[count - 1].Location;

                cntlPosnX = lastCntlLoc.X;
                cntlPosnY = lastCntlLoc.Y + this.pnlCuts.Controls[count - 1].Height;
            }

            foreach (VirtualCut cut in virtualCutList)
            {
                UCvirtualCutsOversUndersFormElement cutsOversUndersFormElement = new UCvirtualCutsOversUndersFormElement();

                cutsOversUndersFormElement.Init(this, count+1, cut, drawingScaleInInches);

                this.pnlCuts.Controls.Add(cutsOversUndersFormElement);

                cutsOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

                cntlPosnY += cutsOversUndersFormElement.Height;

                count++;
            }
        }

        //private OversUnders oversUnders;

        private List<MaterialArea> currOversList = new List<MaterialArea>();

        private List<MaterialArea> currUndrsList = new List<MaterialArea>();

        private List<MaterialArea> prevOversList = new List<MaterialArea>();

        private List<MaterialArea> prevUndrsList = new List<MaterialArea>();

        /// <summary>
        /// Does the fills routine. Note that the graphicsCut list, unders list and drawing scale are taken from the most recent 'Update'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoFills_Click(object sender, System.EventArgs e)
        {
            AreaFinishManager selectedAreaFinishManager = FinishManagerGlobals.SelectedAreaFinishManager;

            UpdateTotals(selectedAreaFinishManager);
        }

        public void UpdateTotals(AreaFinishManager selectedAreaFinishManager)
        {
            doUpdateTotals(selectedAreaFinishManager);
        }

        int smallFillElementWidthInInches;

        private void doUpdateTotals(AreaFinishManager selectedAreaFinishManager)
        {

            if (!UndrsListIsValid())
            {
                if (this.rbnUpdateFillsManually.Checked)
                {
                    MessageBox.Show("Invalid unders are defined. Cannot update fills until this is resolved.");
                }

                return;
            }

            if (!OversListIsValid())
            {
                if (this.rbnUpdateFillsManually.Checked)
                {
                    MessageBox.Show("Invalid overs are defined. Cannot update fills until this is resolved.");
                }

                return;
            }

            //AreaFinishManager selectedAreaFinishManager = FinishManagerGlobals.SelectedAreaFinishManager;

            this.rollWidthInInches = (int)Math.Round(selectedAreaFinishManager.RollWidthInInches);

            this.rollWidthInFeet = Math.Round(selectedAreaFinishManager.RollWidthInInches / 12.0);

            currOversList.Clear();
            currUndrsList.Clear();

            generateMaterialsLists(currOversList, currUndrsList, layoutAreaList);
            
            //oversUnders = new OversUnders(oversList, undrsList);

            //int fillWdthInInches = oversUnders.GenerateWasteEstimate(rollWidthInInches);
            double totlFillLength = 0;
            double largeWastePercent = 0;
          

            OversUndersMainProcessor oversUndersMainProcessor = new OversUndersMainProcessor(
                currOversList
                , currUndrsList
                , rollWidthInInches);

            double dTemp = 0.0;

            oversUndersMainProcessor.GetOUsOutput(out totlFillLength, out largeWastePercent, out dTemp);

            totalLargeUndrageArea = dTemp;

            largeFillPieceArea = rollWidthInInches * totlFillLength / 12.0;

            if (largeFillPieceArea.HasValue && totalLargeUndrageArea.HasValue)
            {
                if (largeFillPieceArea.Value <= 0 && totalLargeUndrageArea.Value <= 0)
                {
                    largeFillPieceArea = null;
                    totalLargeUndrageArea = null;
                    largeFillWastePercent = null;

                }
                
                else if (totalLargeUndrageArea.Value > 0)
                {
                    largeFillWastePercent = largeFillPieceArea.Value / totalLargeUndrageArea.Value-1.0;
                }


                else
                {
                    largeFillWastePercent = null;
                }
            }


            //OversUndersProcessorOldVersion.GetOUsOutput(oversList, undrsList, rollWidthInInches, out totlFillLength, out wasteFactor, out netUnders);


            int fillWdthFeet = (int)Math.Floor(totlFillLength);
            int fillWdthInch = (int)Math.Round(12.0 * (totlFillLength - (double) fillWdthFeet));

            string fillWdthLbl = fillWdthFeet.ToString() + "' " + fillWdthInch.ToString() + '"';

            int fillHghtInch = rollWidthInInches % 12;
            int fillHghtFeet = (rollWidthInInches - fillHghtInch) / 12;

            string fillHghtLbl = fillHghtFeet.ToString() + "' " + fillHghtInch.ToString() + '"';

            this.fillSummaryElement.Update(fillHghtFeet, fillHghtInch, fillWdthFeet, fillWdthInch);

            this.cutsExtraElement.Update(rollWidthInInches, fillHghtFeet, fillHghtInch, fillWdthFeet, fillWdthInch);

            int yPosn = getPnlCutsLastPosn();

            smallFillElementWidthInInches = 0;

            if (totalSmallUndrageAreaInFeet.HasValue)
            {
                if (totalSmallUndrageAreaInFeet.Value > 0)
                {

                    smallFillElementWidthInInches = (int) Math.Round(12.0 * totalSmallUndrageAreaInFeet.Value / rollWidthInFeet);

                    if (smallFillElementWidthInInches >= 1.0)
                    {
                        //if (smallFillElement is null)
                        //{
                        //    smallFillElement = new UCSmallFillElement();
                        //}

                        //this.pnlCuts.Controls.Add(this.smallFillElement);

                        //this.smallFillElement.Location = new Point(8, yPosn + 12);

                        this.smallFillElement.Update(rollWidthInInches, smallFillElementWidthInInches);

                        this.lblSmallFillFillPiece.Text = FormatUtils.FormatInchesToFeetAndInches(smallFillElementWidthInInches);
                    }
                }

            }

            else
            {
                this.lblSmallFillFillPiece.Text = string.Empty;
                this.smallFillElement.Update(rollWidthInInches, 0);
            }

            setupTotals(selectedAreaFinishManager, graphicsCutList, smallFillElementWidthInInches, rollWidthInInches, drawingScaleInFeet);

            prevOversList.Clear();
            prevUndrsList.Clear();

            prevOversList.AddRange(currOversList);
            prevUndrsList.AddRange(currUndrsList);

            this.btnDoFills.BackColor = SystemColors.ControlLightLight;
        }

        private void generateMaterialsLists(
            List<MaterialArea> oversList
            , List<MaterialArea> undrsList
            , List<CanvasLayoutArea> layoutAreaList)
        {
            int extraInchesPerCut = FinishManagerGlobals.SelectedAreaFinishManager.ExtraInchesPerCut;

            if (Utilities.IsNotNull(this.graphicsCutList))
            {
                foreach (Cut cut in this.graphicsCutList)
                {
                    if (Utilities.IsNotNull(cut.OverageList))
                    {
                        foreach (Overage overage in cut.OverageList)
                        {
                            if (overage.Deleted)
                            {
                                continue;
                            }

                            int hghtInInches = 0;
                            int wdthInInches = 0;

                            if (overage.ShapeHasBeenOverridden)
                            {
                                hghtInInches = (int)Math.Round(overage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                                wdthInInches = (int)Math.Round(overage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                            }

                            else
                            {
                                hghtInInches = (int)Math.Round(overage.EffectiveDimensions.Item1 * drawingScaleInInches) + extraInchesPerCut;
                                wdthInInches = (int)Math.Round(overage.EffectiveDimensions.Item2 * drawingScaleInInches);
                            }

                            //---------------------------------------------//
                            // Overs must meet global minimums to be added //
                            //---------------------------------------------//
                            
                            if (hghtInInches >= GlobalSettings.MinOverComboLengthInInches && wdthInInches >= GlobalSettings.MinOverComboWidthInInches)
                            {
                                MaterialArea m = new MaterialArea(MaterialAreaType.Over, wdthInInches, hghtInInches);

                                oversList.Add(m);
                            }
                        }
                    }
                }
            }

            //-------------------------------------------------------------------------------------------//
            // The following section was added late in the game. It covers overages that were explicitly //
            // created with an overs generator that would otherwise not be part of a graphicsCut                 //
            //-------------------------------------------------------------------------------------------//

            foreach (CanvasLayoutArea layoutArea in layoutAreaList)
            {
                if (layoutArea.LayoutAreaType != LayoutAreaType.OversGenerator)
                {
                    continue;
                }
                
                double wdthInInches = 0;
                double lnthInInches = 0;
                double count = 0;

                foreach (GraphicsOverage overage in layoutArea.EmbeddedOversList)
                {
                    lnthInInches += overage.BoundingRectangle.Width * drawingScaleInInches;
                    wdthInInches += overage.BoundingRectangle.Height * drawingScaleInInches;

                    count++;
                }

                if (count > 0)
                {
                    wdthInInches /= count;
                }

                int iWdthInInches = (int)Math.Round(wdthInInches);
                int iLnthInInches = (int)Math.Round(lnthInInches);

                //---------------------------------------------//
                // Overs must meet global minimums to be added //
                //---------------------------------------------//

                if (iLnthInInches >= GlobalSettings.MinOverComboLengthInInches && iWdthInInches >= GlobalSettings.MinOverComboWidthInInches)
                {
                    MaterialArea m = new MaterialArea(MaterialAreaType.Over, iWdthInInches, iLnthInInches);

                    oversList.Add(m);
                }
            }

            if (Utilities.IsNotNull(undrageList))
            {
                foreach (Undrage undrage in undrageList)
                {
                    if (undrage.Deleted)
                    {
                        continue;
                    }

                    int hghtInInches = 0;
                    int wdthInInches = 0;

                    if (undrage.ShapeHasBeenOverridden)
                    {
                        hghtInInches = (int)Math.Round(undrage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches );
                        wdthInInches = (int)Math.Round(undrage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    else
                    {
                        hghtInInches = (int)Math.Round(undrage.EffectiveDimensions.Item1 * drawingScaleInInches) + extraInchesPerCut;
                        wdthInInches = (int)Math.Round(undrage.EffectiveDimensions.Item2 * drawingScaleInInches);

                        wdthInInches = (int) Math.Round(wdthInInches + undrage.MaterialOverlap); // Added overlap amount in this case.

                        int halfWidth = (int) Math.Round(undrage.MaterialWidth * 0.5 * drawingScaleInInches);

                        if (wdthInInches > halfWidth)
                        {
                            wdthInInches = halfWidth;
                        }
                    }

                    //----------------------------------------------//
                    // Unders must meet global minimums to be added //
                    //----------------------------------------------//

                    if (hghtInInches >= GlobalSettings.MinUnderComboLengthInInches && wdthInInches >= GlobalSettings.MinUnderComboWidthInInches)
                    {
                        MaterialArea m = new MaterialArea(MaterialAreaType.Undr, wdthInInches, hghtInInches);

                        undrsList.Add(m);
                    }
                }


            }

            if (Utilities.IsNotNull(this.virtualOverageList))
            {
                foreach (VirtualOverage virtualOverage in this.virtualOverageList)
                {
                    int hghtInInches = 0;
                    int wdthInInches = 0;

                    if (virtualOverage.ShapeHasBeenOverridden)
                    {
                        hghtInInches = (int)Math.Round(virtualOverage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                        wdthInInches = (int)Math.Round(virtualOverage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    else
                    {
                        hghtInInches = (int)Math.Round(virtualOverage.EffectiveDimensions.Item1 * drawingScaleInInches);
                        wdthInInches = (int)Math.Round(virtualOverage.EffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    //---------------------------------------------//
                    // Overs must meet global minimums to be added //
                    //---------------------------------------------//

                    if (hghtInInches >= GlobalSettings.MinOverComboLengthInInches && wdthInInches >= GlobalSettings.MinOverComboWidthInInches)
                    {
                        MaterialArea m = new MaterialArea(MaterialAreaType.Over, wdthInInches, hghtInInches);

                        oversList.Add(m);
                    }
                }
            }

            if (Utilities.IsNotNull(this.virtualUndrageList))
            {
                foreach (VirtualUndrage virtualUndrage in this.virtualUndrageList)
                {
                    int hghtInInches = 0;
                    int wdthInInches = 0;

                    if (virtualUndrage.ShapeHasBeenOverridden)
                    {
                        hghtInInches = (int)Math.Round(virtualUndrage.OverrideEffectiveDimensions.Item1 * drawingScaleInInches);
                        wdthInInches = (int)Math.Round(virtualUndrage.OverrideEffectiveDimensions.Item2 * drawingScaleInInches);
                    }

                    else
                    {
                        hghtInInches = (int)Math.Round(virtualUndrage.EffectiveDimensions.Item1 * drawingScaleInInches);
                        wdthInInches = (int)Math.Round(virtualUndrage.EffectiveDimensions.Item2 * drawingScaleInInches);
                    }


                    //----------------------------------------------//
                    // Unders must meet global minimums to be added //
                    //----------------------------------------------//

                    if (hghtInInches >= GlobalSettings.MinUnderComboLengthInInches && wdthInInches >= GlobalSettings.MinUnderComboWidthInInches)
                    {
                        MaterialArea m = new MaterialArea(MaterialAreaType.Undr, wdthInInches, hghtInInches);

                        undrsList.Add(m);
                    }
                }
            }

            oversList.Sort();
            undrsList.Sort();
        }


        #region Add / Delete Overs

        private void btnAddVirtualOver_Click(object sender, EventArgs e)
        {
            VirtualOverage overage = new VirtualOverage();

           // FinishManagerGlobals.SelectedAreaFinishManager.VirtualOverageList.Add(overage);

            this.virtualOverageList.Add(overage);

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = this.pnlOvers.Controls.Count;

            if (count > 0)
            {
                Point lastCntlLoc = this.pnlOvers.Controls[count - 1].Location;

                cntlPosnX = lastCntlLoc.X;
                cntlPosnY = lastCntlLoc.Y + this.pnlOvers.Controls[count - 1].Height;
            }

            UCVirtualOversOversUndersFormElement oversOversUndersFormElement = new UCVirtualOversOversUndersFormElement();

            oversOversUndersFormElement.Init(this, count, overage, drawingScaleInInches);

            this.pnlOvers.Controls.Add(oversOversUndersFormElement);

            oversOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

            SetDoFillsBackgroundColor();
        }

        private void btnDeleteSelectedOvers_Click(object sender, EventArgs e)
        {
            oversPanlChanged = false;

            foreach (Control control in this.pnlOvers.Controls)
            {
                if (control is UCOversOversUndersFormElement)
                {
                    deleteOverFormElem((UCOversOversUndersFormElement)control);
                }

                else
                {
                    deleteOverFormElem((UCVirtualOversOversUndersFormElement)control);
                }
            }

            if (!oversPanlChanged)
            {
                return;
            }

            int overIndx = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateOverIndicesAndVisibility(ref overIndx);
            }


            buildOversPanel(overageList, drawingScaleInInches);
            buildVirtualOversPanel(virtualOverageList, drawingScaleInInches);

            SetDoFillsBackgroundColor();
        }

        bool oversPanlChanged = false;


        private void deleteOverFormElem(UCOversOversUndersFormElement formElem)
        {
            if (!formElem.Selected)
            {
                return;
            }

            oversPanlChanged = true;

            Overage overage = formElem.Overage;

            overage.Deleted = true;
        }

        private void deleteOverFormElem(UCVirtualOversOversUndersFormElement formElem)
        {
            if (!formElem.Selected)
            {
                return;
            }

            oversPanlChanged = true;

            VirtualOverage overage = formElem.Overage;


            FinishManagerGlobals.SelectedAreaFinishManager.VirtualOverageList.Remove(overage);

            this.virtualOverageList.Remove(overage);
        }

        #endregion

        #region Add / Delete Unders

        private void btnAddVirtualUndr_Click(object sender, EventArgs e)
        {
            VirtualUndrage undrage = new VirtualUndrage();

            //FinishManagerGlobals.SelectedAreaFinishManager.VirtualUndrageList.Add(undrage);

            this.virtualUndrageList.Add(undrage);

            int cntlPosnX = 12;
            int cntlPosnY = 12;

            int count = this.pnlUndrs.Controls.Count;

            if (count > 0)
            {
                Point lastCntlLoc = this.pnlUndrs.Controls[count - 1].Location;

                cntlPosnX = lastCntlLoc.X;
                cntlPosnY = lastCntlLoc.Y + this.pnlUndrs.Controls[count - 1].Height;
            }

            UCVirtualUndrsOversUndersFormElement undrsOversUndersFormElement = new UCVirtualUndrsOversUndersFormElement();

            undrsOversUndersFormElement.Init(this, count, undrage, drawingScaleInInches);

            this.pnlUndrs.Controls.Add(undrsOversUndersFormElement);

            undrsOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);

            ClearFillsDisplay();
        }

        bool undrsPanlChanged = false;

        private void btnDeleteSelectedUnders_Click(object sender, EventArgs e)
        {
            undrsPanlChanged = false;

            foreach (Control control in this.pnlUndrs.Controls)
            {
                if (control is UCUndrsOversUndersFormElement)
                {
                    deleteUndrFormElem((UCUndrsOversUndersFormElement)control);
                }

                else
                {
                    deleteUndrFormElem((UCVirtualUndrsOversUndersFormElement)control);
                }
            }

            if (!undrsPanlChanged)
            {
                return;
            }

            int undrIndx = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateUndrIndicesAndVisibility(ref undrIndx);
            }


            buildUndrsPanel(undrageList, drawingScaleInInches);
            buildVirtualUndrsPanel(virtualUndrageList, drawingScaleInInches);

            if (!UndrsListIsValid())
            {
                ClearFillsDisplay();
            }

            else
            {
                SetDoFillsBackgroundColor();
            }

        }

        private void deleteUndrFormElem(UCUndrsOversUndersFormElement formElem)
        {
            if (!formElem.Selected)
            {
                return;
            }

            undrsPanlChanged = true;

            Undrage undrage = formElem.Undrage;

            undrage.Deleted = true;
        }

        private void deleteUndrFormElem(UCVirtualUndrsOversUndersFormElement formElem)
        {
            if (!formElem.Selected)
            {
                return;
            }

            undrsPanlChanged = true;

            VirtualUndrage undrage = formElem.Undrage;


            FinishManagerGlobals.SelectedAreaFinishManager.VirtualUndrageList.Remove(undrage);

            this.virtualUndrageList.Remove(undrage);
        }

        #endregion


        #region Add / Delete Cuts

#if false

        private void btnAddVirtualCut_Click(object sender, EventArgs e)
        {

            VirtualCut cut = new VirtualCut((double) rollWidthInInches / drawingScaleInInches);

            baseForm.selectedAreaFinish.AreaFinishManager.VirtualCutsList.Add(cut);

            this.virtualCutsList.Add(cut);

            UCvirtualCutsOversUndersFormElement cutsOversUndersFormElement = new UCvirtualCutsOversUndersFormElement();

            int cntlPosnX = 8;
            int cntlPosnY = 12;

            Control lastControl = getPnlCutsLastNonFillSummaryControl();

            UCFillSummaryElement ucFillSummaryElement = getPnlCutsFillSummaryElement();

            int count = this.pnlCuts.Controls.Count;

            if (Utilities.IsNotNull(ucFillSummaryElement))
            {
                count--;
            }

            cutsOversUndersFormElement.Init(this, count + 1, cut, drawingScaleInInches);

            if (Utilities.IsNotNull(lastControl))
            {
                cntlPosnY = lastControl.Location.Y + lastControl.Height;
            }

            this.pnlCuts.Controls.Add(cutsOversUndersFormElement);

            cutsOversUndersFormElement.Location = new Point(cntlPosnX, cntlPosnY);
            
            if (Utilities.IsNotNull(ucFillSummaryElement))
            {
                ucFillSummaryElement.Location = new Point(cntlPosnX, cntlPosnY + cutsOversUndersFormElement.Height + 8);
            }

            SetDoFillsBackgroundColor();
        }
#endif
        
        bool cutsPanlChanged = false;

        private void btnDeleteSelectedCuts_Click(object sender, EventArgs e)
        {
            cutsPanlChanged = false;

            foreach (Control control in this.pnlCuts.Controls)
            {
                if (control is UCFillSummaryElement)
                {
                    continue;
                }

                if (control is UCCutsOversUndersFormElement)
                {
                    deleteCutsFormElem((UCCutsOversUndersFormElement)control);

                    continue;
                }

                if (control is UCvirtualCutsOversUndersFormElement)
                {
                    deleteCutsFormElem((UCvirtualCutsOversUndersFormElement)control);
                }
            }

            if (!cutsPanlChanged)
            {
                return;
            }

            int cutIndx = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateCutIndicesAndVisibility(ref cutIndx);
            }

            this.pnlCuts.Controls.Clear();
            this.pnlCutsTotals.Controls.Clear();

            buildCutsPanel(graphicsCutList, drawingScaleInInches);

            this.UpdateTotals(FinishManagerGlobals.SelectedAreaFinishManager);

            SetDoFillsBackgroundColor();

            this.pnlCuts.Invalidate();
        }


        private void deleteCutsFormElem(UCCutsOversUndersFormElement formElem)
        {
            if (!formElem.Selected)
            {
                return;
            }

            cutsPanlChanged = true;

            Cut cut = formElem.Cut;

            cut.Deleted = true;
        }

        private void deleteCutsFormElem(UCvirtualCutsOversUndersFormElement formElem)
        {
            if (!formElem.Selected)
            {
                return;
            }

            cutsPanlChanged = true;

            VirtualCut cut = formElem.Cut;


            FinishManagerGlobals.SelectedAreaFinishManager.VirtualCutsList.Remove(cut);

            this.virtualCutsList.Remove(cut);
        }


        private void btnAddBackSelectedCuts_Click(object sender, EventArgs e)
        {
            cutsPanlChanged = false;

            foreach (Control control in this.pnlCuts.Controls)
            {
                if (control is UCFillSummaryElement)
                {
                    continue;
                }

                if (control is UCCutsOversUndersFormElement)
                {
                    UCCutsOversUndersFormElement ucCutsOversUndersFormElement = (UCCutsOversUndersFormElement)control;

                    if (ucCutsOversUndersFormElement.Selected)
                    {
                        Cut cut = ucCutsOversUndersFormElement.Cut;

                        cut.Deleted = false;

                        cutsPanlChanged = true;

                        continue;
                    }
                }

                if (control is UCvirtualCutsOversUndersFormElement)
                {
                    continue;
                }
            }

            if (!cutsPanlChanged)
            {
                return;
            }

            buildCutsPanel(graphicsCutList, drawingScaleInInches);
            //buildVirtualCutsList(virtualCutsList, drawingScaleInInches);

        }

        #endregion

        private void btnResetCuts_Click(object sender, EventArgs e)
        {
            graphicsCutList.ForEach(c => c.Deleted = false);

            int cutIndx = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateCutIndicesAndVisibility(ref cutIndx);
            }

            pnlCuts.Controls.Clear();

            buildCutsPanel(graphicsCutList, drawingScaleInInches);

            SetDoFillsBackgroundColor();
        }

        private void btnResetOvers_Click(object sender, EventArgs e)
        {
            overageList.ForEach(o => o.Deleted = false);

            virtualOverageList.Clear();

            int overIndx = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateOverIndicesAndVisibility(ref overIndx );
            }

            pnlOvers.Controls.Clear();

            buildOversPanel(overageList, drawingScaleInInches);

            this.UpdateTotals(FinishManagerGlobals.SelectedAreaFinishManager);

            SetDoFillsBackgroundColor();
        }

        private void btnResetUnders_Click(object sender, EventArgs e)
        {
            undrageList.ForEach(u => u.Deleted = false);
  
            virtualUndrageList.Clear();

            int undrIndx = 1;

            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                canvasLayoutArea.UpdateOverIndicesAndVisibility(ref undrIndx);
            }


            foreach (CanvasLayoutArea canvasLayoutArea in FinishManagerGlobals.SelectedAreaFinishManager.CanvasLayoutAreas)
            {
                 canvasLayoutArea.UpdateUndrIndicesAndVisibility(ref undrIndx );
            }

            pnlUndrs.Controls.Clear();

            buildUndrsPanel(undrageList, drawingScaleInInches);

            SetDoFillsBackgroundColor();
        }

        public bool UndrsListIsValid()
        {
            foreach (Control control in this.pnlUndrs.Controls)
            {
                if (control is UCUndrsOversUndersFormElement)
                {
                    if (!((UCUndrsOversUndersFormElement) control).IsValid())
                    {
                        return false;
                    }
                }

                else if (control is UCVirtualUndrsOversUndersFormElement)
                {
                    if (!((UCVirtualUndrsOversUndersFormElement)control).IsValid())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        int getPnlCutsLastPosn()
        {

            int maxY = 0;

            foreach (Control control in this.pnlCuts.Controls)
            {
                int nextY = control.Location.Y + control.Height;

                if (nextY > maxY)
                {
                    maxY = nextY;
                }
            }

            return maxY;

        }


        private Control getPnlCutsLastNonFillSummaryControl()
        {
            int maxY = 0;
            Control maxControl = null;

            foreach (Control control in this.pnlCuts.Controls)
            {
                if (control is UCFillSummaryElement)
                {
                    continue;
                }

                if (control.Location.Y > maxY)
                {
                    maxY = control.Location.Y;
                    maxControl = control;
                }
            }

            return maxControl;
        }

        private UCFillSummaryElement getPnlCutsFillSummaryElement()
        {

            foreach (Control control in this.pnlCuts.Controls)
            {
                if (control is UCFillSummaryElement)
                {
                    return (UCFillSummaryElement) control;
                }
            }

            return null;
        }

        public bool OversListIsValid()
        {
            foreach (Control control in this.pnlOvers.Controls)
            {
                if (control is UCOversOversUndersFormElement)
                {
                    if (!((UCOversOversUndersFormElement)control).IsValid())
                    {
                        return false;
                    }
                }

                else if (control is UCVirtualOversOversUndersFormElement)
                {
                    if (!((UCVirtualOversOversUndersFormElement)control).IsValid())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void OversUndersForm_Load(object sender, EventArgs e)
        {
            tbNotes.DataBindings.Add("Text", this, "Notes");
        }

        private void btnSwitchSize_Click(object sender, EventArgs e)
        {
            if (this.FormIsFullSize)
            {
                SwitchToShortenedSize();
            }
            else
            {
                SwitchToFullSize();
            }
        }

        private void SwitchToShortenedSize()
        {
            this.grbCuts.Visible = false;
            this.labelNotes.Visible = false;

            this.gbxOvers.Location = new Point(oversXPositionShortened, oversUndersYPositionShortened);

            int undersXPosition = this.gbxOvers.Location.X + this.gbxOvers.Width + oversXPositionShortened;

            this.grbUnders.Location = new Point(undersXPosition, oversUndersYPositionShortened);

            int formWidth = (oversXPositionShortened * 3) + undersXPosition + this.grbUnders.Width;
            int formHeight = (oversUndersYPositionShortened * 2) + this.gbxOvers.Height + 50;

            this.Size = new Size(formWidth, formHeight);

            this.lblGrossAreaTextShortened.Visible = true;
            this.lblGrossAreaShortened.Visible = true;

            this.btnSwitchSize.Text = "Full";
            this.toolTip1.SetToolTip(this.btnSwitchSize, "Show the whole form");
            this.FormIsFullSize = false;
        }

        private void SwitchToFullSize()
        {
            this.gbxOvers.Location = new Point(oversXPositionOriginal, oversUndersYPositionOriginal);
            this.grbUnders.Location = new Point(undersXPositionOriginal, oversUndersYPositionOriginal);

            this.grbCuts.Visible = true;
            this.labelNotes.Visible = true;

            this.lblGrossAreaTextShortened.Visible = false;
            this.lblGrossAreaShortened.Visible = false;

            this.Size = formSizeOriginal;
            this.btnSwitchSize.Text = "O/U";
            this.toolTip1.SetToolTip(this.btnSwitchSize, "Show only Overs and Unders");
            this.FormIsFullSize = true;
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            ExportReportForm exportReportForm = new ExportReportForm(pnlCuts, pnlOvers, pnlUndrs);

            exportReportForm.ShowDialog();
        }

        private void btnSwitchCutsSize_Click(object sender, EventArgs e)
        {
            if (this.FormIsFullSize)
            {
                SwitchToShortenedCutSize();
            }
            else
            {
                SwitchToCutsFullSize();
            }
        }

        private void SwitchToShortenedCutSize()
        {
            int cutsXPosition = this.grbCuts.Location.X + this.grbCuts.Width + oversXPositionShortened;

            int formWidth = (oversXPositionShortened * 2) + cutsXPosition; 
            int formHeight = (oversUndersYPositionShortened * 2) + this.grbCuts.Height + 50;

            this.Size = new Size(formWidth, formHeight);

            this.btnSwitchCutsSize.Text = "Full";
            this.toolTip1.SetToolTip(this.btnSwitchCutsSize, "Show the whole form");
            this.FormIsFullSize = false;
        }

        private void SwitchToCutsFullSize()
        {
            this.Size = formSizeOriginal;
            this.btnSwitchSize.Text = "Cuts";
            this.toolTip1.SetToolTip(this.btnSwitchCutsSize, "Show Cuts Only");
            this.FormIsFullSize = true;
        }

        private void btnNestUndrs_Click(object sender, EventArgs e)
        {
            UndersNestingForm undersNestingForm = new UndersNestingForm(this.baseForm, undrageList);

            undersNestingForm.ShowDialog();
        }
    }
}
