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

namespace FloorMaterialEstimator
{
    public partial class UCCutUndrsNestingSelectionControl : UserControl
    {
        public FloorMaterialEstimatorBaseForm FloorMaterialEstimatorBaseForm => CutOversNestingBaseForm.BaseForm;

        public CutUndrsNestingBaseForm CutOversNestingBaseForm { get; set; }

        public CanvasPage currentPage => FloorMaterialEstimatorBaseForm.CanvasManager.CurrentPage;

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public GraphicsPage page { get; set; }

        public GraphicsWindow window { get; set; }

        public List<GraphicShape> CutElemList = new List<GraphicShape>();

        public Dictionary<string, GraphicsCutElem> CutElemDict = new Dictionary<string, GraphicsCutElem>();

        public Dictionary<string, GraphicsUndrElem> UndrElemDict = new Dictionary<string, GraphicsUndrElem>();

        public double DrawingScaleInInches => FloorMaterialEstimatorBaseForm.CanvasManager.CurrentPage.DrawingScaleInInches;

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

        public UCCutUndrsNestingSelectionControl()
        {
            InitializeComponent();
        }

        public void Init(CutUndrsNestingBaseForm cutsOversNestingBaseForm)
        {
            this.CutOversNestingBaseForm = cutsOversNestingBaseForm;

            initCanvas();
            setSize();
            initDisplay();

            VisioInterop.ShowPanAndZoomBox(window, false);

            this.MouseEnter += CutOversNestingBaseForm_MouseEnter;
            this.MouseLeave += CutOversNestingBaseForm_MouseLeave;

            VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.SizeChanged += CutOversNestingSelectionControl_SizeChanged;

            foreach (Control control in this.Controls)
            {
                control.Cursor = Cursors.Cross;
            }
        }

        private void CutOversNestingBaseForm_MouseEnter(object sender, EventArgs e)
        {
            FloorMaterialEstimatorBaseForm.Cursor = Cursors.Arrow;
        }

        private void CutOversNestingBaseForm_MouseLeave(object sender, EventArgs e)
        {

        }

        private void initDisplay()
        {
            AreaFinishManager areaFinishManager = FinishManagerGlobals.SelectedAreaFinishManager;
            
            this.flpCutChoice.Controls.Clear();

            flpUndrChoice.Controls.Clear();

            foreach (CanvasLayoutArea layoutArea in areaFinishManager.CanvasLayoutAreas)
            {
                CanvasLayoutArea nestLayoutArea = layoutArea.Clone();

                GraphicShape shape = nestLayoutArea.Draw(window, page);

                VisioInterop.SetFillOpacity(shape, areaFinishManager.Opacity);

                initCuts(layoutArea);
                initUndrs(layoutArea);

            }


            window?.DeselectAll();
        }


        private void initCuts(CanvasLayoutArea layoutArea)
        {
            List<CutSet> CutNestSetList = new List<CutSet>();

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
                                , layoutArea.AreaFinishManager.RollWidthInInches
                                , layoutArea.AreaFinishManager.Color
                                , layoutArea.ExternalArea.FirstLine.ucLine.LineColor);

                            cutSet.Add(cutElem);
                        }
                    }
                }

                Color penColor = layoutArea.ExternalArea.FirstLine.ucLine.LineColor;

                GraphicShape cutShape = graphicsCut.DrawBoundingRectangle(window, page, penColor, Color.White, 1);

                graphicsCut.Shape = cutShape;

                cutShape.SetFillOpacity(0);

                cutShape.SetLineWidth(1);

                cutShape.BringToFront();

                if (cutSet.Count > 0)
                {
                    cutSet.Sort((c1, c2) => (int) c1.Index - (int)c2.Index);

                    CutNestSetList.Add(cutSet);
                }
            }


            CutNestSetList.Sort((c1, c2) => (int) c1.MinIndex - (int)c2.MinIndex);

            //int xPosn = 0;
            //int yPosn = 0;

            foreach (CutSet cutSet in CutNestSetList)
            {
                foreach (GraphicsCutElem graphicsCutElem in cutSet)
                {
                    GraphicShape shape = graphicsCutElem.Draw(window, page, Color.Red, Color.FromArgb(0, 0, 0, 0), 3.0);

                    CutElemList.Add(shape);

                    CutElemDict.Add(shape.Guid, graphicsCutElem);

                    //graphicsCut.Shape = shape;

                    shape.SetShapeText(graphicsCutElem.Index.ToString(), Color.Black, 14);

                    UCCutElement cutElement = new UCCutElement(graphicsCutElem);

                    cutElement.CutElementClick += CutElement_CutElementClick;

                    flpCutChoice.Controls.Add(cutElement);

                    //cutElement.Location = new Point(xPosn, yPosn);

                    //yPosn += cutElement.Height;
                }
            }
        }

        private void initUndrs(CanvasLayoutArea layoutArea)
        {
            foreach (GraphicsUndrage graphicsUndr in layoutArea.GraphicsUndrageList)
            {
                GraphicsUndrElem graphicsUndrElement = new GraphicsUndrElem(
                    window
                    , page
                    , graphicsUndr.UndrageIndex
                    , graphicsUndr
                    , Color.Red);

                UCUndrElement undrElement = new UCUndrElement(graphicsUndrElement);

                graphicsUndr.Shape = graphicsUndrElement.Draw(window, page, Color.Green, Color.White, 1);

                VisioInterop.SetFillOpacity(graphicsUndr.Shape, 0);

                graphicsUndr.Shape.BringToFront();


                graphicsUndr.Shape.SetShapeText(Utilities.Utilities.IndexToLowerCaseString(graphicsUndrElement.Index), Color.Black, 16);

                undrElement.UndrElementClick += UndrElement_UndrElementClick;

                UndrElemDict.Add(graphicsUndr.Shape.Guid, graphicsUndrElement);

                flpUndrChoice.Controls.Add(undrElement);
            }
        }

        private void UndrElement_UndrElementClick(UCUndrElement sender, int label)
        {
            GraphicsUndrElem graphicsUndrElem = sender.GraphicsUndrElem;

            updateDisplay(graphicsUndrElem);
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
                updateDisplay((GraphicsCutElem)element);
            }

            else if (element is GraphicsUndrElem)
            {
                updateDisplay((GraphicsUndrElem)element);
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

                CutOversNestingBaseForm.CutElementSelected((UCCutElement)null);
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

                CutOversNestingBaseForm.CutElementSelected((UCCutElement)flpCutChoice.Controls[indx - 1]);
            }

        }

        private void updateDisplay(GraphicsUndrElem graphicsUndrElem)
        {
            if (graphicsUndrElem == null)
            {
                return;
            }

            int indx = (int)graphicsUndrElem.Index;

            UCUndrElement undrElement = (UCUndrElement)flpUndrChoice.Controls[indx - 1];

            if (undrElement.Selected)
            {
                undrElement.SetSelected(false);

                CutOversNestingBaseForm.RemoveUndrElement(graphicsUndrElem);
            }

            else
            {
                undrElement.SetSelected(true);

                CutOversNestingBaseForm.AddUndrElement(graphicsUndrElem);
            }
        }

        private void initCanvas()
        {
            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.axDrawingControl.Window.ShowPageTabs = false;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            VsoWindow.ShowRulers = (short)0;
            VsoWindow.ShowGrid = GlobalSettings.ShowGrid ? (short)1 : (short)0;

            VsoWindow.ShowPageBreaks = 0;

            VsoWindow.Page = pages[1];

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, VsoDocument, VsoWindow.Page);

            int width = (int)Math.Ceiling(FloorMaterialEstimatorBaseForm.CanvasManager.CurrentPage.PageWidth);
            int height = (int)Math.Ceiling(FloorMaterialEstimatorBaseForm.CanvasManager.CurrentPage.PageHeight);

            page.SetPageSize(width, height);
        }

        private void CutOversNestingSelectionControl_SizeChanged(object sender, EventArgs e)
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

                if (UndrElemDict.ContainsKey(guid))
                {
                    return this.UndrElemDict[guid];
                }
            }

            return null;
        }
    }
}
