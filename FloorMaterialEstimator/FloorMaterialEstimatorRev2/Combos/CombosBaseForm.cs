
namespace FloorMaterialEstimator
{
    using ComboLib;
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using Geometry;
    using Graphics;
    using MaterialsLayout;
    using SettingsLib;
    using System;
    using System.Collections.Generic;

    using System.Drawing;

    using System.Windows.Forms;

    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CombosBaseForm : Form //, IMessageFilter
    {
        public FloorMaterialEstimatorBaseForm BaseForm;

        public GraphicsPage combosPage ;

        public GraphicsWindow combosWindow;

        public Visio.Window VsoWindow { get; set; }


        public Visio.Document VsoDocument { get; set; }

        //public GraphicsPage ComboCurrentPage { get; set; }

        //public GraphicsWindow ComboCurrentWindow { get; set; }

        public List<GraphicShape> ComboElemList = new List<GraphicShape>();

        public Dictionary<string, GraphicsComboElem> ComboElemDict = new Dictionary<string, GraphicsComboElem>();

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

        public CombosBaseForm(FloorMaterialEstimatorBaseForm baseForm)
        {
            InitializeComponent();

            this.BaseForm = baseForm;

            initCanvas();
            setSize();
            initDisplay();

            combosWindow = new GraphicsWindow(this.VsoWindow);

            this.MouseEnter += CombosBaseForm_MouseEnter;
            this.MouseLeave += CombosBaseForm_MouseLeave;

            VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.SizeChanged += CombosBaseForm_SizeChanged;

            foreach (Control control in this.Controls)
            {
                control.Cursor = Cursors.Cross;
            }

        }

        private void CombosBaseForm_MouseEnter(object sender, EventArgs e)
        {
            BaseForm.Cursor = Cursors.Arrow;
        }

        private void CombosBaseForm_MouseLeave(object sender, EventArgs e)
        {
            //BaseForm.SetCursorForCurrentLocation();
        }

        private void initDisplay()
        {
           
            AreaFinishManager selectedAreaFinishManager = FinishManagerGlobals.AreaFinishManagerList.SelectedAreaFinishManager;

            List<CutComboSet> CutComboSetList = new List<CutComboSet>();

            int cutIndex = 1;

#if true
            foreach (CanvasLayoutArea layoutArea in selectedAreaFinishManager.CanvasLayoutAreas)
            {
                CanvasLayoutArea comboLayoutArea = layoutArea.CloneBasic(combosWindow, combosPage);

                GraphicShape shape = comboLayoutArea.Draw(combosWindow, combosPage);


                VisioInterop.SetFillOpacity(shape, selectedAreaFinishManager.Opacity);

                GraphicsDirectedPolygon ExternalArea = layoutArea.ExternalArea;

                foreach (GraphicsCut graphicsCut in layoutArea.GraphicsCutList)
                {
                    CutComboSet cutComboSet = new CutComboSet(graphicsCut);

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

                                GraphicsComboElem comboElem = new GraphicsComboElem(graphicsCut.CutIndex, graphicsCut, new GraphicsDirectedPolygon(combosWindow, combosPage, directedPolygon), layoutArea.AreaFinishManager.Color);

                                cutComboSet.Add(comboElem);
                            }
                        }
                    }

                    if (cutComboSet.Count > 0)
                    {
                        cutComboSet.Sort((c1, c2) => (int) c1.Index - (int)c2.Index);

                        CutComboSetList.Add(cutComboSet);
                    }
                }
            }
#endif
            CutComboSetList.Sort((c1, c2) => (int) c1.MinIndex - (int)c2.MinIndex);

            this.flpCutChoice.Controls.Clear();

            foreach (CutComboSet cutComboSet in CutComboSetList)
            {
                foreach (GraphicsComboElem graphicsComboElem in cutComboSet)
                {
                    GraphicShape shape = graphicsComboElem.Draw(combosWindow, combosPage, Color.Red, Color.FromArgb(0, 0, 0, 0), 3.0);

                    ComboElemList.Add(shape);

                    ComboElemDict.Add(shape.Guid, graphicsComboElem);

                    //graphicsCut.Shape = shape;

                    shape.SetShapeText(graphicsComboElem.Index.ToString(), Color.Black, 14);

                    UCComboElement comboElement = new UCComboElement(graphicsComboElem);

                    comboElement.ComboElementClick += ComboElement_ComboElementClick;

                    flpCutChoice.Controls.Add(comboElement);
                }
            }

            combosWindow?.DeselectAll();
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = true;

            GraphicsComboElem graphicsComboElem = GetSelectedRollout(x, y);

            updateDisplay(graphicsComboElem);
        }


        private void ComboElement_ComboElementClick(UCComboElement sender, string label, bool cntlKeyPressed)
        {
            GraphicsComboElem graphicsComboElem = sender.GraphicsComboElem;

            if (label == "Flipped")
            {
                updateDisplay(graphicsComboElem, cntlKeyPressed);
            }

            else
            {
                updateDisplay(graphicsComboElem, false);
            }
        }

        private void updateDisplay(GraphicsComboElem graphicsComboElem, bool cntlKeyPressed = false)
        {
            if (graphicsComboElem == null)
            {
                return;
            }

            int indx = (int)graphicsComboElem.Index;

            UCComboElement comboElement = (UCComboElement)flpCutChoice.Controls[indx - 1];

            if (comboElement.Combined)
            {
                return;
            }

            if (cntlKeyPressed)
            {
                if (!comboElement.Selected)
                {
                    return;
                }

                if (string.IsNullOrEmpty(comboElement.lblFlipped.Text))
                {
                    comboElement.lblFlipped.Text = "O";
                }
                
                else if (comboElement.lblFlipped.Text == "O")
                {
                    comboElement.lblFlipped.Text = "M";
                }

                else
                {
                    comboElement.lblFlipped.Text = string.Empty;
                }

                return;
            }

            if (comboElement.Selected)
            {
                comboElement.SetSelected(false);
            }

            else
            {
                ((UCComboElement)flpCutChoice.Controls[indx - 1]).SetSelected(true);
            }

            int minIndx = int.MaxValue;

            bool selectionsFound = false;

            foreach (UCComboElement combElem in flpCutChoice.Controls)
            {
                if (combElem.Selected)
                {
                    if (combElem.CutIndex < minIndx)
                    {
                        minIndx = combElem.CutIndex;
                    }

                    selectionsFound = true;
                }
            }

            if (!selectionsFound)
            {
                combosWindow?.DeselectAll();

                return;
            }

            string minLabl = minIndx.ToString();

            int selectedIndex = 0;

            for (int i = 0; i < flpCutChoice.Controls.Count; i++)
            {
                UCComboElement combElem = (UCComboElement)flpCutChoice.Controls[i];

                if (!combElem.Selected || combElem.Combined)
                {
                    continue;
                }

                GraphicsComboElem graphicsComboElem1 = combElem.GraphicsComboElem;

                string groupLabel = minLabl + ((char)('a' + selectedIndex)).ToString();
                combElem.lblGroupNumber.Text = groupLabel;

                graphicsComboElem1.Shape.SetShapeText(graphicsComboElem1.Index.ToString() + "\n" + groupLabel, Color.Black, 12);

                selectedIndex++;
            }

        }

        private void BtnClearSelections_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.flpCutChoice.Controls.Count; i++)
            {
                UCComboElement combElem = (UCComboElement)this.flpCutChoice.Controls[i];

                if (combElem.Combined)
                {
                    continue;
                }

                if (combElem.Selected)
                {
                    combElem.SetSelected(false);
                }
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

            combosWindow = new GraphicsWindow(VsoWindow);

            combosPage = new GraphicsPage(combosWindow, VsoDocument, VsoWindow.Page);


            int width = (int)Math.Ceiling(BaseForm.CanvasManager.CurrentPage.PageWidth);
            int height = (int)Math.Ceiling(BaseForm.CanvasManager.CurrentPage.PageHeight);

            combosPage.SetPageSize(width, height);
        }

        private void CombosBaseForm_SizeChanged(object sender, EventArgs e)
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

            int grbProcessSizeX = this.grbProcessGroups.Width;
            int grbProcessSizeY = this.grbProcessGroups.Height;

            int grbDefineSizeX = this.grbDefineGroups.Width;
            int grbDefineSizeY = panlSizeY - grbProcessSizeY - 32;

            int grbDefineLocnX = 8;
            int grbDefineLocnY = 8;

            int grbProcessLocnX = 8;
            int grbProcessLocnY = grbDefineLocnY + grbDefineSizeY + 16;

            this.grbDefineGroups.Size = new Size(grbDefineSizeX, grbDefineSizeY);
            this.grbDefineGroups.Location = new Point(grbDefineLocnX, grbDefineLocnY);

            this.grbProcessGroups.Size = new Size(grbProcessSizeX, grbProcessSizeY);
            this.grbProcessGroups.Location = new Point(grbProcessLocnX, grbProcessLocnY);

            // Set up choice panel size and location

            int flowLocnX = this.flpCutChoice.Location.X;
            int flowLocnY = this.flpCutChoice.Location.Y;

            int flowSizeX = this.flpCutChoice.Width;
            int flowSizeY = grbDefineSizeY - flowLocnY - 32;

            this.flpCutChoice.Size = new Size(flowSizeX, flowSizeY);
        }

        private GraphicsComboElem GetSelectedRollout(double x, double y)
        {
            Visio.Selection selection = combosPage.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

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

                if (ComboElemDict.ContainsKey(guid))
                {
                    return this.ComboElemDict[guid];
                }
            }

            return null;
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            List<UCComboElement> combElemList = new List<UCComboElement>();

            int groupNumber = int.MaxValue;

            foreach (UCComboElement combElem in this.flpCutChoice.Controls)
            {
                if (combElem.Combined || !combElem.Selected)
                {
                    continue;
                }

                // (Re) define the group number as the smallest element in the list. There is 
                // probably a better way to do this down the road.

                if (combElem.CutIndex < groupNumber)
                {
                    groupNumber = combElem.CutIndex;
                }

                combElemList.Add(combElem);
            }

            if (combElemList.Count <= 0)
            {
                ManagedMessageBox.Show("No elements selected to form a group");
                return;
            }

            combElemList.ForEach(c => c.SetCombined(true));

            UCGroupElement groupElem = new UCGroupElement(this, groupNumber, combElemList);

            this.flpGroups.Controls.Add(groupElem);
        }

        internal void DeleteGroup(UCGroupElement ucGroupElement)
        {
            foreach (UCComboElement combElem in ucGroupElement.GroupList)
            {
                combElem.SetCombined(false);
            }

            this.flpGroups.Controls.Remove(ucGroupElement);
        }

//        protected override void OnActivated(EventArgs e)
//        {
//            base.OnActivated(e);
//            // install message filter when form activates
//            Application.AddMessageFilter(BaseForm);
//        }

//        protected override void OnDeactivate(EventArgs e)
//        {
//            base.OnDeactivate(e);
//            // remove message filter when form deactivates
//            Application.RemoveMessageFilter(BaseForm);
//        }

//        public bool PreFilterMessage(ref Message m)
//        {
//            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
//            {
//                //BaseForm.SetCursorForCurrentLocation();
//#if DEBUG
//                BaseForm.UpdateMousePositionDisplay();
//#endif
//            }

//            return false;
//        }
    }
}
