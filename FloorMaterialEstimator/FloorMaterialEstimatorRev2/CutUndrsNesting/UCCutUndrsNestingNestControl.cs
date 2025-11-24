using FloorMaterialEstimator.CanvasManager;
using FloorMaterialEstimator.Finish_Controls;
using Geometry;
using Graphics;
using MaterialsLayout;
using SettingsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using CutOversNestingLib;
using System.Drawing;
using CanvasLib.Tape_Measure;
using System.Windows.Forms;

using Utilities;

using Visio = Microsoft.Office.Interop.Visio;

namespace FloorMaterialEstimator
{
    public partial class UCCutUndrsNestingNestControl : UserControl
    {
        public FloorMaterialEstimatorBaseForm FloorMaterialEstimatorBaseForm => CutOversNestingBaseForm.BaseForm;

        public CutUndrsNestingBaseForm CutOversNestingBaseForm { get; set; }

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }

        public Visio.Page VsoPage { get; set; }

        public Visio.Application VsoApplication { get; set; }

        public GraphicsPage page { get; set; }

        public GraphicsWindow window { get; set; }

        public List<GraphicShape> CutElemList = new List<GraphicShape>();

        public Dictionary<string, GraphicsCutElem> CutElemDict = new Dictionary<string, GraphicsCutElem>();

        public List<GraphicsUndrElem> UndrElemList = new List<GraphicsUndrElem>();

        public double DrawingScaleInInches => FloorMaterialEstimatorBaseForm.CanvasManager.CurrentPage.DrawingScaleInInches;

        private string rghtSideBndrLineGuid = Utilities.GuidMaintenance.GenerateGuid();

        private string leftSideBndrLineGuid = Utilities.GuidMaintenance.GenerateGuid();

        GraphicShape leftBndryLineShape;
        GraphicShape rghtBndryLineShape ;

        public GraphicsLayer boundaryLayer { get; set; }

        public static int boundaryLayerIndx { get; set; } = 1;

        private const double baseLineYMin = 12;

        private double baseLineYMax = 14;

        private GraphicShape baseLineYMinShape;

        private GraphicShape baseLineYMaxShape;

        private const int pageWidth = 24;

        private const int pageHeight = 16;

        private const double baseArrowVal = 0.125;

        // Unders pallet

        private double palletLineUppr = 10;


        private double palletLineLowr = 1;

        private double palletLineLeft = 1;

        private double palletLineRght = 23;

        private GraphicShape undersPalletBoundary;

        private GraphicShape undersPalletTitle;

        private static List<Coordinate> rghtArrowCoordList = new List<Coordinate>()
        {
            new Coordinate(0.0, baseArrowVal)
            ,new Coordinate(1.5 * baseArrowVal, baseArrowVal)
            ,new Coordinate(1.5 * baseArrowVal, 0.0)
            ,new Coordinate(4.0 * baseArrowVal, 1.5 * baseArrowVal)
            ,new Coordinate(1.5 * baseArrowVal, 3.0 * baseArrowVal)
            ,new Coordinate(1.5 * baseArrowVal, 2.0 * baseArrowVal)
            ,new Coordinate(0.0, 2.0 * baseArrowVal)
        };

        TapeMeasureController tapeMeasureController;

        //private GraphicsDirectedPolygon rghtArrowBase = null;

        //private GraphicsDirectedPolygon leftArrowBase = null;

        public UCCutUndrsNestingNestControl()
        {
            InitializeComponent();
        }

        public void Init(
            CutUndrsNestingBaseForm cutOversNestingBaseForm)
        {
            this.CutOversNestingBaseForm = cutOversNestingBaseForm;

            initCanvas();
            setSize();

            VisioInterop.ShowPanAndZoomBox(window, false);

            //this.MouseEnter += CutOversNestingBaseForm_MouseEnter;
            //this.MouseLeave += CutOversNestingBaseForm_MouseLeave;

            VsoWindow.MouseDown += VsoWindow_MouseDown;

            VsoWindow.MouseUp += VsoWindow_MouseUp;

            VsoWindow.MouseMove += VsoWindow_MouseMove;

            VsoWindow.KeyDown += VsoWindow_KeyDown;

            VsoWindow.KeyUp += VsoWindow_KeyUp;

            this.SizeChanged += CutOversNestingNestControl_SizeChanged;

            foreach (Control control in this.Controls)
            {
                control.Cursor = Cursors.Cross;
            }

            cutOversNestingBaseForm.FormClosed += CutOversNestingBaseForm_FormClosed;

            tapeMeasureController = new TapeMeasureController(window, page);
        }

        private void initCanvas()
        {
            VsoWindow = this.axDrawingControl.Window;
            VsoDocument = this.axDrawingControl.Document;

            this.axDrawingControl.Window.ShowPageTabs = false;

            this.VsoDocument.PrintLandscape = true;
            this.VsoDocument.PaperSize = Visio.VisPaperSizes.visPaperSizeLegal;

            Visio.Pages pages = this.VsoDocument.Pages;

            VsoApplication = VsoWindow.Application;

            VsoWindow.ShowRulers = (short)1;
            VsoWindow.ShowGrid = GlobalSettings.ShowGrid ? (short)1 : (short)0;
            VsoWindow.ShowScrollBars = (short)0;

            VsoWindow.ShowPageBreaks = 0;

            VsoPage = pages[1];

            window = new GraphicsWindow(VsoWindow);

            page = new GraphicsPage(window, VsoDocument, VsoPage);

            page.SetPageSize(pageWidth, pageHeight);

            boundaryLayer = new GraphicsLayer(null, window, page, "[BoundaryLayer]" + boundaryLayerIndx.ToString(), GraphicsLayerType.BoundaryLayer, GraphicsLayerStyle.Dynamic);

            boundaryLayerIndx++;


            baseLineYMinShape = page.DrawLine(this, 0, baseLineYMin, pageWidth, baseLineYMin, Utilities.GuidMaintenance.GenerateGuid());
            baseLineYMaxShape = page.DrawLine(this, 0, baseLineYMax, pageWidth, baseLineYMax, Utilities.GuidMaintenance.GenerateGuid());

            VisioInterop.SetBaseLineColor(baseLineYMinShape, Color.Red);
            VisioInterop.SetBaseLineColor(baseLineYMaxShape, Color.Red);

            VisioInterop.SetBaseLineStyle(baseLineYMinShape, (int) VisioLineStyle.Dashed);
            VisioInterop.SetBaseLineStyle(baseLineYMaxShape, (int)VisioLineStyle.Dashed);

            VisioInterop.SetLineWidth(baseLineYMinShape, 3);
            VisioInterop.SetLineWidth(baseLineYMaxShape, 3);

            VisioInterop.LockShape(baseLineYMinShape);
            VisioInterop.LockShape(baseLineYMaxShape);

            leftBndryLineShape = page.DrawLine(this, 0, baseLineYMin, 0, baseLineYMax, leftSideBndrLineGuid);
            rghtBndryLineShape = page.DrawLine(this, pageWidth, baseLineYMin, pageWidth, baseLineYMax, rghtSideBndrLineGuid);

            VisioInterop.SetShapeData1(leftBndryLineShape, "LeftBoundaryLine");
            VisioInterop.SetShapeData1(rghtBndryLineShape, "RghtBoundaryLine");

            page.AddToPageShapeDict(leftBndryLineShape);
            page.AddToPageShapeDict(rghtBndryLineShape);

            //Page.GuidShapeDict.Add(leftBndryLineShape.Guid, leftBndryLineShape);
            //Page.GuidShapeDict.Add(rghtBndryLineShape.Guid, rghtBndryLineShape);

            leftBndryLineShape.SetLineWidth(3);
            rghtBndryLineShape.SetLineWidth(3);

            leftBndryLineShape.SetLineColor(Color.Red);
            rghtBndryLineShape.SetLineColor(Color.Red);


            //rghtArrowBase = new GraphicsDirectedPolygon(Window, Page, rghtArrowCoordList);
            //leftArrowBase = rghtArrowBase.Clone();

            //leftArrowBase.Rotate(Math.PI);

            //leftArrowBase.Translate(new Coordinate(0.0, -leftArrowBase.MinY));

            //Shape rghtArrowShape = rghtArrowBase.Draw(Window, Page, Color.Red, Color.FromArgb(0, 0, 0, 0));
            //Shape leftArrowShape = leftArrowBase.Draw(Window, Page, Color.Red, Color.FromArgb(0, 0, 0, 0));

            VisioInterop.LockShapeExceptForHorizontalMovement(leftBndryLineShape);
            VisioInterop.LockShapeExceptForHorizontalMovement(rghtBndryLineShape);

            boundaryLayer.AddShape(leftBndryLineShape, 1);
            boundaryLayer.AddShape(rghtBndryLineShape, 1);

            undersPalletBoundary = page.DrawRectangle(this, palletLineLeft, palletLineUppr, palletLineRght, palletLineLowr);

            VisioInterop.SetBaseLineColor(undersPalletBoundary, Color.DarkGray);
            VisioInterop.SetLineWidth(undersPalletBoundary, 2);
            VisioInterop.SetFillOpacity(undersPalletBoundary, 0);

            boundaryLayer.AddShape(undersPalletBoundary, 1);

            VisioInterop.LockShape(undersPalletBoundary);

            this.undersPalletTitle = page.DrawTextBox(this, pageWidth * 0.5 - 2, palletLineUppr + 0.25, pageWidth * 0.5 + 2, palletLineUppr + .75, "Unders Pallet");


            VisioInterop.SetTextFontSize(this.undersPalletTitle, 32);
            VisioInterop.SetBaseFillColor(this.undersPalletTitle, Color.FromArgb(0, 255, 255, 255));
            VisioInterop.SetBaseLineOpacity(this.undersPalletTitle, 0);

            VisioInterop.LockShape(this.undersPalletTitle);

            boundaryLayer.AddShape(undersPalletTitle, 1);

            VisioInterop.SetLayerVisibility(boundaryLayer, true);

            window.DeselectAll();
        }

        private void CutOversNestingNestControl_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int formSizeX = this.Width;
            int formSizeY = this.Height;

            int cntlSizeX = formSizeX - 12;
            int cntlSizeY = formSizeY - 64;

            this.axDrawingControl.Size = new Size(cntlSizeX, cntlSizeY);
            this.axDrawingControl.Location = new Point(4, 4);

        }

        private void CutOversNestingBaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            boundaryLayer.Delete();
        }

        GraphicsDirectedPolygon selectedGraphicsDirectedPolygon = null;

        internal void CutElementSelected(UCCutElement ucCutElement)
        {

            if (ucCutElement is null)
            {
                if (Utilities.Utilities.IsNotNull(selectedGraphicsDirectedPolygon))
                {
                    selectedGraphicsDirectedPolygon.Delete();

                    selectedGraphicsDirectedPolygon = null;
                }

                boundaryLayer.SetLayerVisibility(false);

                return;
            }

            if (Utilities.Utilities.IsNotNull(selectedGraphicsDirectedPolygon))
            {
                selectedGraphicsDirectedPolygon.Delete();
            }

            drawSelectedCut(ucCutElement);

            updateBoundaryLines(ucCutElement);
        }

        private void drawSelectedCut(UCCutElement ucCutElement)
        {
            selectedGraphicsDirectedPolygon = ucCutElement.GraphicsCutElem.GraphicsDirectedPolygon.Clone();

            double theta = ucCutElement.GraphicsCutElem.CutAngle;

            selectedGraphicsDirectedPolygon.Rotate(theta);

            double minX = selectedGraphicsDirectedPolygon.MinX;

            double minY = selectedGraphicsDirectedPolygon.MinY;

            double maxX = selectedGraphicsDirectedPolygon.MaxX;

            double maxY = selectedGraphicsDirectedPolygon.MaxY;

            Coordinate translateCoordinate = new Coordinate(-minX + 12 - (maxX - minX) / 2, -minY + baseLineYMin);

            selectedGraphicsDirectedPolygon.Translate(translateCoordinate);

            selectedGraphicsDirectedPolygon.Draw(window, page, Color.Red, Color.FromArgb(0, 0, 0, 0));

            selectedGraphicsDirectedPolygon.Shape.SetLineColor(ucCutElement.GraphicsCutElem.LineColor);

            VisioInterop.SetShapeText(selectedGraphicsDirectedPolygon.Shape, ucCutElement.CutIndex.ToString(), Color.Black, 24);
        }

        private void updateBoundaryLines(UCCutElement ucCutElement)
        {
            double minX = selectedGraphicsDirectedPolygon.MinX;

            double minY = selectedGraphicsDirectedPolygon.MinY;

            double maxX = selectedGraphicsDirectedPolygon.MaxX;

            double maxY = selectedGraphicsDirectedPolygon.MaxY;

            double materialWidthInInches = ucCutElement.GraphicsCutElem.MaterialWidthInInches;

            double nextbaseLineYMax = baseLineYMin + materialWidthInInches / DrawingScaleInInches;

            if (nextbaseLineYMax != baseLineYMax)
            {
                baseLineYMax = nextbaseLineYMax;

                VisioInterop.UnlockShape(baseLineYMaxShape);

                VisioInterop.SetShapeBegin(baseLineYMaxShape, 0, baseLineYMax);
                VisioInterop.SetShapeEnd(baseLineYMaxShape, pageWidth, baseLineYMax);

                VisioInterop.LockShape(baseLineYMaxShape);
            }

            VisioInterop.SetShapeBegin(leftBndryLineShape, minX, baseLineYMin);
            VisioInterop.SetShapeEnd(leftBndryLineShape, minX, baseLineYMax);

            VisioInterop.SetShapeBegin(rghtBndryLineShape, maxX, baseLineYMin);
            VisioInterop.SetShapeEnd(rghtBndryLineShape, maxX, baseLineYMax);

            bool drawingShape = false;

            tapeMeasureController.MeasureLineFrstPointClick(minX, maxY + 1.0);
            tapeMeasureController.MeasureLineScndPointClick(maxX, maxY + 1.0);
           
            boundaryLayer.SetLayerVisibility(true);

            window.DeselectAll();
        }
        private void CutOversNestingBaseForm_MouseEnter(object sender, EventArgs e)
        {
            FloorMaterialEstimatorBaseForm.Cursor = Cursors.Arrow;
        }

        private void CutOversNestingBaseForm_MouseLeave(object sender, EventArgs e)
        {

        }

        GraphicShape selectedShape
        {
            get;
            set;
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = false;

            selectedShape = GetSelectedElement(page, x, y);

            if (selectedShape is null)
            {
                return;
            }

            VisioInterop.SelectShape(window, selectedShape);

            //selectedShape.VisioShape.CellChanged += VisioShape_CellChanged;
        }

        //private void VisioShape_CellChanged(Visio.Cell Cell)
        //{

        //    if (selectedShape.VisioShape.Data1 == "LeftBoundaryLine" || selectedShape.VisioShape.Data1 == "RghtBoundaryLine")
        //    {
        //        Window.DeselectAll();

        //        //CancelDefault = false;

        //        double leftX = VisioInterop.GetShapePinLocation(leftBndryLineShape).X;
        //        double rghtX = VisioInterop.GetShapePinLocation(rghtBndryLineShape).X;

        //        tapeMeasureController.CancelTapeMeasure();

        //        bool drawingShape = false;

        //        tapeMeasureController.MeasureLineFrstPointClick(leftX, baseLineYMax + 1.0, ref drawingShape);
        //        tapeMeasureController.MeasureLineScndPointClick(rghtX, baseLineYMax + 1.0, ref drawingShape);

        //        return;
        //    }
        //}

        private void VsoWindow_MouseUp(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (selectedShape is null)
            {
                return;
            }

            if (selectedShape.VisioShape is null)
            {
                return;
            }

            if (selectedShape.VisioShape.Data1 == "LeftBoundaryLine" || selectedShape.VisioShape.Data1 == "RghtBoundaryLine")
            {
                window.DeselectAll();

                CancelDefault = false;

                double leftX = VisioInterop.GetShapePinLocation(leftBndryLineShape).X;
                double rghtX = VisioInterop.GetShapePinLocation(rghtBndryLineShape).X;

                tapeMeasureController.CancelTapeMeasure();

                bool drawingShape = false;

                tapeMeasureController.MeasureLineFrstPointClick(leftX, baseLineYMax + 1.0);
                tapeMeasureController.MeasureLineScndPointClick(rghtX, baseLineYMax + 1.0);

                return;
            }

            if (selectedShape.VisioShape.Data1 == "GraphicsUndrElem")
            {
                CancelDefault = false;

                return;
            }
        }


        private void VsoWindow_KeyUp(int KeyCode, int KeyButtonState, ref bool CancelDefault)
        {
            if (KeyCode != 9)
            {
                return;
            }

            if (selectedShape is null)
            {
                return;
            }

            if (selectedShape.VisioShape is null)
            {
                return;
            }

            if (selectedShape.VisioShape.Data1 == "LeftBoundaryLine" || selectedShape.VisioShape.Data1 == "RghtBoundaryLine")
            {
                window.DeselectAll();

                CancelDefault = false;

                double leftX = VisioInterop.GetShapePinLocation(leftBndryLineShape).X;
                double rghtX = VisioInterop.GetShapePinLocation(rghtBndryLineShape).X;

                tapeMeasureController.CancelTapeMeasure();

                bool drawingShape = false;

                tapeMeasureController.MeasureLineFrstPointClick(leftX, baseLineYMax + 1.0);
                tapeMeasureController.MeasureLineScndPointClick(rghtX, baseLineYMax + 1.0);

                return;
            }
        }

        private void VsoWindow_KeyDown(int KeyCode, int KeyButtonState, ref bool CancelDefault)
        {

        }

        private void VsoWindow_MouseMove(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            if (selectedShape is null)
            {
                return;
            }

            else
            {

            }
            //if (selectedShape.VisioShape.Data1 == "LeftBoundaryLine" || selectedShape.VisioShape.Data1 == "RghtBoundaryLine")
            //{
            //    Window.DeselectAll();

            //    CancelDefault = false;

            //    double leftY = VisioInterop.GetShapePinLocation(leftBndryLineShape).Y;
            //    double rghtY = VisioInterop.GetShapePinLocation(rghtBndryLineShape).Y;

            //    tapeMeasureController.CancelTapeMeasure();

            //    bool drawingShape = false;

            //    tapeMeasureController.MeasureLineFrstPointClick(leftY, baseLineYMax + 1.0, ref drawingShape);
            //    tapeMeasureController.MeasureLineScndPointClick(rghtY, baseLineYMax + 1.0, ref drawingShape);

            //    return;
            //}
        }


        private void updateDisplay(GraphicsCutElem graphicsCutElem)
        {
            
        }

        private void updateDisplay(GraphicsUndrElem graphicsUndrElem)
        {
           
        }
   
        private GraphicShape GetSelectedElement(GraphicsPage page, double x, double y)
        {
            Visio.Selection selection = page.VisioPage.SpatialSearch[
                x
                , y
                , (short)(Visio.VisSpatialRelationCodes.visSpatialContainedIn | Visio.VisSpatialRelationCodes.visSpatialContain | Visio.VisSpatialRelationCodes.visSpatialTouching | Visio.VisSpatialRelationCodes.visSpatialOverlap)
                , 0.1
                , 0];

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

                if (string.IsNullOrEmpty(guid))
                {
                    continue;
                }

                if (page.PageShapeDictContains(guid))
                {
                    return ((GraphicShape) page.PageShapeDictGetShape(guid));
                }
            }

            return null;
        }


        internal void RemoveUndrElement(GraphicsUndrElem graphicsUndrElem)
        {
            if (UndrElemList.Contains(graphicsUndrElem))
            {
                graphicsUndrElem.Undraw();

                UndrElemList.Remove(graphicsUndrElem);
            }

            updateUndrsPallet();
        }

        internal void AddUndrElement(GraphicsUndrElem graphicsUndrElem)
        {
            if (!UndrElemList.Contains(graphicsUndrElem))
            {
                GraphicsUndrElem clonedGraphicsUndrElem = graphicsUndrElem.Clone(window, page);

                //clonedGraphicsUndrElem.Draw(Window, Page, Color.Green, Color.FromArgb(0, 255, 255, 255), 1);

                UndrElemList.Add(clonedGraphicsUndrElem);
            }

            updateUndrsPallet();
        }

        private void updateUndrsPallet()
        {
            if (UndrElemList.Count <= 0)
            {
                return;
            }

            foreach (GraphicsUndrElem graphicsUndrElem in UndrElemList)
            {
                if (graphicsUndrElem.Shape is null)
                {
                    continue;
                }

                if (page.PageShapeDictContains(graphicsUndrElem.Shape.Guid))
                {
                    page.RemoveFromPageShapeDict(graphicsUndrElem.Shape.Guid);
                }

                graphicsUndrElem.Undraw();
            }

            double maxHeight = UndrElemList.Max(x => x.Height);
            double maxLength = UndrElemList.Max(y => y.Length);

            int cellHeight = ((int)Math.Ceiling(maxHeight)) + 1;
            int cellLength = ((int)Math.Ceiling(maxLength)) + 1;

            int palletLength = (int) Math.Floor(palletLineRght - palletLineLeft);
            int palletHeight = (int) Math.Floor(palletLineUppr - palletLineLowr);

            int ncols = palletLength / cellLength;
            int nrows = palletHeight / cellHeight;

            for (int elem = 0; elem < UndrElemList.Count; elem++)
            {
                GraphicsUndrElem graphicsUndrElem = UndrElemList[elem];

                int row = elem / ncols;
                int col = elem % ncols;

                double x = palletLineLeft + col * cellLength + 0.5 * cellLength;
                double y = palletLineUppr - row * cellHeight - 0.5 * cellHeight;

                GraphicShape shape = graphicsUndrElem.Draw(window, page, x, y, Color.Green, Color.FromArgb(0, 255, 255, 255), 1);

                VisioInterop.SetShapeData1(shape, "GraphicsUndrElem");

                page.AddToPageShapeDict(shape);

                //VisioInterop.SetShapePinLocation(graphicsUndrElem.Shape, x, y);
            }
          
        }
    }
}
