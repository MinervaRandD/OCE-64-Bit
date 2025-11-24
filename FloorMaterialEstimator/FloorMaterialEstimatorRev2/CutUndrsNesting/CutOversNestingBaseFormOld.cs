
namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using SettingsLib;
    using System;
    using System.Collections.Generic;
    using CutOversNestingLib;

    using System.Drawing;

    using System.Windows.Forms;

    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CutOversNestingBaseFormOld : Form
    {
        public FloorMaterialEstimatorBaseForm BaseForm;

        public CanvasPage currentPage => BaseForm.CanvasManager.CurrentPage;

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public GraphicsPage page { get; set; }

        public GraphicsWindow window { get; set; }

        public List<Shape> CutElemList = new List<Shape>();

        public Dictionary<string, GraphicsCutElem> CutElemDict = new Dictionary<string, GraphicsCutElem>();

        public Dictionary<string, GraphicsOverElem> OverElemDict = new Dictionary<string, GraphicsOverElem>();

        public double DrawingScaleInInches => BaseForm.CanvasManager.CurrentPage.DrawingScaleInInches;

        private bool showGrid
        {
            get
            {
                return GlobalSettings.ShowGrid;
            }
        }

        private bool showRulers
        {
            get
            {
                return GlobalSettings.ShowRulers;
            }
        }

        public CutOversNestingBaseFormOld(FloorMaterialEstimatorBaseForm baseForm)
        {
            InitializeComponent();

            this.BaseForm = baseForm;

            initCanvas();
            setSize();
            initDisplay();

            window = new GraphicsWindow(this.VsoWindow);

            this.MouseEnter += CutOversNestingBaseForm_MouseEnter;
            this.MouseLeave += CutOversNestingBaseForm_MouseLeave;

            VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.SizeChanged += CutOversNestingBaseForm_SizeChanged;

            foreach (Control control in this.Controls)
            {
                control.Cursor = Cursors.Cross;
            }

        }

        private void CutOversNestingBaseForm_MouseEnter(object sender, EventArgs e)
        {
            BaseForm.Cursor = Cursors.Arrow;
        }

        private void CutOversNestingBaseForm_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void initDisplay()
        {
            UCAreaFinishPalletElement ucAreaFinish = BaseForm.areaPallet.SelectedFinish;

            foreach (CanvasLayoutArea layoutArea in ucAreaFinish.CanvasLayoutAreas)
            {
                CanvasLayoutArea comboLayoutArea = layoutArea.Clone();

                Shape shape = comboLayoutArea.Draw(window, page);

                VisioInterop.SetFillOpacity(shape, ucAreaFinish.Opacity);

                initCuts(layoutArea);
                initOvers(layoutArea);

            }


            window?.DeselectAll();
        }


        private void initCuts(CanvasLayoutArea layoutArea)
        {

            List<CutSet> CutComboSetList = new List<CutSet>();

            GraphicsDirectedPolygon ExternalArea = layoutArea.ExternalArea;

            foreach (GraphicsCut graphicsCut in layoutArea.GraphicsCutList)
            {
                CutSet cutSet = new CutSet(graphicsCut);

                foreach (GraphicsDirectedPolygon cutPolygon in graphicsCut.CutPolygonList)
                {
                    // Truncate the rectangular cut by the layout area external polygon.
                    // Note: Only one polygon can result by design, but the general intersection list can contain more than one polygon result.

                    List<DirectedPolygon> truncatedCutPolygonList = cutPolygon.Intersect(ExternalArea);

                    // Foreach just to be general...

                    foreach (DirectedPolygon truncatedPolygon in truncatedCutPolygonList)
                    {
                        // Now we need to remove potential internal take out areas...

                        List<DirectedPolygon> inptList = new List<DirectedPolygon>() { truncatedPolygon };
                        List<DirectedPolygon> outpList = new List<DirectedPolygon>() { truncatedPolygon };

                        foreach (CanvasDirectedPolygon internalPolygon in layoutArea.InternalAreas)
                        {
                            outpList.Clear();

                            foreach (DirectedPolygon inptPolygon in inptList)
                            {
                                if (inptPolygon.Intersects(internalPolygon))
                                {
                                    outpList.AddRange(inptPolygon.Subtract(internalPolygon));
                                }

                                else
                                {
                                    outpList.Add(inptPolygon);
                                }
                            }

                            inptList.Clear();

                            inptList.AddRange(outpList);

                        }

                        foreach (DirectedPolygon directedPolygon in outpList)
                        {
                            //directedPolygon.Rotate(graphicsCut.CutAngle);

                            GraphicsCutElem cutElem = new GraphicsCutElem(
                                window
                                , page
                                , graphicsCut.CutIndex
                                , graphicsCut
                                , new GraphicsDirectedPolygon(window, page, directedPolygon)
                                , layoutArea.UCAreaFinish.RollWidthInInches
                                , layoutArea.UCAreaFinish.FinishColor
                                , layoutArea.ExternalArea.FirstLine.ucLine.LineColor);

                            cutSet.Add(cutElem);
                        }
                    }
                }

                Color penColor = layoutArea.ExternalArea.FirstLine.ucLine.LineColor;

                Shape cutShape = graphicsCut.DrawBoundingRectangle(window, page, penColor, Color.White, 1);

                graphicsCut.Shape = cutShape;

                cutShape.SetFillOpacity(0);

                cutShape.SetLineWidth(1);

                cutShape.BringToFront();

                if (cutSet.Count > 0)
                {
                    cutSet.Sort((c1, c2) => c1.Index - c2.Index);

                    CutComboSetList.Add(cutSet);
                }
            }


            CutComboSetList.Sort((c1, c2) => c1.MinIndex - c2.MinIndex);

            this.flpCutChoice.Controls.Clear();

            foreach (CutSet cutSet in CutComboSetList)
            {
                foreach (GraphicsCutElem graphicsCutElem in cutSet)
                {
                    Shape shape = graphicsCutElem.Draw(window, page, Color.Red, Color.FromArgb(0, 0, 0, 0), 3.0);

                    CutElemList.Add(shape);

                    CutElemDict.Add(shape.Guid, graphicsCutElem);

                    //graphicsCut.Shape = shape;

                    shape.SetShapeText(graphicsCutElem.Index.ToString(), Color.Black, 14);

                    UCCutElement cutElement = new UCCutElement(graphicsCutElem);

                    cutElement.CutElementClick += CutElement_CutElementClick;

                    flpCutChoice.Controls.Add(cutElement);
                }
            }
        }

        private void initOvers(CanvasLayoutArea layoutArea)
        {
            foreach (GraphicsOverage graphicsOver in layoutArea.GraphicsOverageList)
            {
                GraphicsOverElem graphicsOverElement = new GraphicsOverElem(
                    window
                    , page
                    , graphicsOver.OverageIndex
                    , graphicsOver
                    , Color.Red);

                UCOverElement overElement = new UCOverElement(graphicsOverElement);
            
                graphicsOver.Shape = graphicsOverElement.Draw(Color.Green, Color.White, 1);

                VisioInterop.SetFillOpacity(graphicsOver.Shape, 0);

                graphicsOver.Shape.BringToFront();


                graphicsOver.Shape.SetShapeText(Utilities.IndexToLowerCaseString(graphicsOverElement.Index), Color.Black, 16);

                overElement.OverElementClick += OverElement_OverElementClick;

                OverElemDict.Add(graphicsOver.Shape.Guid, graphicsOverElement);

                flpOverChoice.Controls.Add(overElement);


            }
        }

        private void OverElement_OverElementClick(UCOverElement sender, int label)
        {
            GraphicsOverElem graphicsOverElem = sender.GraphicsOverElem;

            updateDisplay(graphicsOverElem);
        }

        private void CutElement_CutElementClick(UCCutElement sender, int label)
        {
            GraphicsCutElem graphicsCutElem = sender.GraphicsCutElem;

            updateDisplay(graphicsCutElem);
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = true;

            object element = GetSelectedElement(x, y);

            if (element is GraphicsCutElem)
            {            
                updateDisplay((GraphicsCutElem) element);
            }

            else if (element is GraphicsOverElem)
            {
                updateDisplay((GraphicsOverElem)element);
            }
        }

        private void updateDisplay(GraphicsCutElem graphicsCutElem)
        {
            if (graphicsCutElem == null)
            {
                return;
            }

            int indx = (int)graphicsCutElem.Index;

            UCCutElement cutElement = (UCCutElement)flpCutChoice.Controls[indx - 1];

            if (cutElement.Selected)
            {
                cutElement.SetSelected(false);
            }

            else
            {
                for (int i = 1; i <= CutElemList.Count; i++)
                {
                    if (i == indx)
                    {
                        ((UCCutElement)flpCutChoice.Controls[i - 1]).SetSelected(true);
                    }

                    else
                    {
                        ((UCCutElement)flpCutChoice.Controls[i - 1]).SetSelected(false);
                    }
                }
               
            }

        }

        private void updateDisplay(GraphicsOverElem graphicsOverElem)
        {
            if (graphicsOverElem == null)
            {
                return;
            }

            int indx = (int)graphicsOverElem.Index;

            UCOverElement overElement = (UCOverElement)flpOverChoice.Controls[indx - 1];

            if (overElement.Selected)
            {
                overElement.SetSelected(false);
            }

            else
            {
                overElement.SetSelected(true);
            }
        }
        private void initCanvas()
        {
            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            VsoWindow.ShowRulers = GlobalSettings.ShowRulers ? (short)1 : (short)0;
            VsoWindow.ShowGrid = GlobalSettings.ShowGrid ? (short)1 : (short)0;

            VsoWindow.ShowPageBreaks = 0;

            VsoWindow.Page = pages[1];

            page = new GraphicsPage(window, VsoWindow.Page);

            window = new GraphicsWindow(VsoWindow);

            int width = (int)Math.Ceiling(BaseForm.CanvasManager.CurrentPage.PageWidth);
            int height = (int)Math.Ceiling(BaseForm.CanvasManager.CurrentPage.PageHeight);

            page.SetPageSize(width, height);
        }

        private void CutOversNestingBaseForm_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            // Set up selection panel size and location.

            int panlSizeX = this.pnlSelection.Width;
            int panlSizeY = formSizeY - 64;

            int panlLocnX = 12;
            int panlLocnY = 12;

            this.pnlSelection.Size = new Size(panlSizeX, panlSizeY);
            this.pnlSelection.Location = new Point(panlLocnX, panlLocnY);

            // Set up visio control size and location;

            int cntlLocnX = panlSizeX + panlLocnX + 24;
            int cntlLocnY = 12;

            int cntlSizeX = formSizeX - cntlLocnX - 12;
            int cntlSizeY = formSizeY - 64;

            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
            this.axDrawingControl.Location = new Point(cntlLocnX, cntlLocnY);

            // Set up group boxes sizes and locations

            int grbDefineSizeX = this.grbSelectCut.Width;
         // int grbDefineSizeY = panlSizeY - grbProcessSizeY - 32;
        }

        private object GetSelectedElement(double x, double y)
        {
            Visio.Selection selection = page.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection == null)
            {
                return null;
            }

            if (selection.Count <= 0)
            {
                return null;
            }

            foreach (Visio.Shape visioShape in selection)
            {
                string guid = visioShape.Data3;

                if (CutElemDict.ContainsKey(guid))
                {
                    return this.CutElemDict[guid];
                }

                if (OverElemDict.ContainsKey(guid))
                {
                    return this.OverElemDict[guid];
                }
            }

            return null;
        }
    }
}
