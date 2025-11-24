
namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.CanvasManager;
    using FloorMaterialEstimator.Finish_Controls;
    using Graphics;
    using MaterialsLayout;
    using SettingsLib;
    using System;
    using System.Collections.Generic;
   
    using System.Drawing;
   
    using System.Windows.Forms;

    using Utilities;

    using Visio = Microsoft.Office.Interop.Visio;

    public partial class CombosBaseForm : Form, IMessageFilter
    {
        public FloorMaterialEstimatorBaseForm BaseForm;

        public CanvasPage currentPage => BaseForm.CanvasManager.CurrentPage;

        public Visio.Window VsoWindow { get; set; }

        public Visio.Document VsoDocument { get; set; }
        
        public GraphicsPage ComboCurrentPage { get; set; }

        public GraphicsWindow ComboCurrentWindow { get; set; }

        public List<Shape> rolloutList = new List<Shape>();

        public Dictionary<string, CanvasCut> rolloutDict = new Dictionary<string, CanvasCut>();

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

            this.MouseEnter += CombosBaseForm_MouseEnter;
            this.MouseLeave += CombosBaseForm_MouseLeave;

            VsoWindow.MouseDown += VsoWindow_MouseDown;
            this.SizeChanged += CombosBaseForm_SizeChanged;
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
            UCAreaFinishPalletElement ucAreaFinish = BaseForm.areaPallet.SelectedFinish;

            List<CanvasCut> cutList = new List<CanvasCut>();

            int cutIndex = 1;

            foreach (CanvasLayoutArea layoutArea in ucAreaFinish.CanvasLayoutAreas)
            {
                Shape shape = layoutArea.Draw(ComboCurrentWindow, ComboCurrentPage);

                VisioInterop.SetFillOpacity(shape, ucAreaFinish.Opacity);

                foreach (GraphicsCut graphicsCut in layoutArea.GraphicsCutList)
                {
                    if (graphicsCut.CutPolygon.AreaInSqrInches(currentPage.DrawingScaleInInches) / graphicsCut.Rollout.AreaInSqrInches(currentPage.DrawingScaleInInches) >= .5)
                    {
                        cutList.Add(graphicsCut);

                        graphicsCut.Tag = cutIndex++;
                    }
                }
            }

            this.flpCutChoice.Controls.Clear();
            
            foreach (CanvasCut cut in cutList)
            {
                //Shape shape = cut.DrawRollout(ComboCurrentPage, Color.Red, Color.FromArgb(0, 0, 0, 0));

                //rolloutList.Add(shape);

                //rolloutDict.Add(shape.NameID, cut);

                //cut.Shape = shape;

                //VisioInterop.SetShapeText(shape, cut.Tag.ToString(), Color.Black, 14);

                //UCComboElement comboElement = new UCComboElement(cut);

                //comboElement.ComboElementClick += ComboElement_ComboElementClick;

                //flpCutChoice.Controls.Add(comboElement);
            }

            VsoWindow.DeselectAll();
        }

        private void VsoWindow_MouseDown(int Button, int KeyButtonState, double x, double y, ref bool CancelDefault)
        {
            CancelDefault = true;

            CanvasCut cut = GetSelectedRollout(x, y);

            updateDisplay(cut);
        }


        private void ComboElement_ComboElementClick(UCComboElement sender, string label, bool cntlKeyPressed)
        {
            CanvasCut cut = sender.canvasCut;

            if (label == "Flipped")
            {
                updateDisplay(cut, cntlKeyPressed);
            }

            else
            {
                updateDisplay(cut, false);
            }
        }

        private void updateDisplay(CanvasCut cut, bool cntlKeyPressed = false)
        {
            if (cut == null)
            {
                return;
            }

            int indx = (int)cut.Tag;

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

                if (comboElement.lblFlipped.Text == "X")
                {
                    comboElement.lblFlipped.Text = string.Empty;
                }

                else
                {
                    comboElement.lblFlipped.Text = "X";
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
                VsoWindow.DeselectAll();

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

                CanvasCut cut1 = combElem.canvasCut;

                string groupLabel = minLabl + ((char)('a' + selectedIndex)).ToString();
                combElem.lblGroupNumber.Text = groupLabel;

                VisioInterop.SetShapeText(cut1.Shape, cut1.Tag.ToString() + "\n" + groupLabel, Color.Black, 12);

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
            
            VsoWindow.ShowRulers = GlobalSettings.ShowRulers ? (short) 1: (short) 0;
            VsoWindow.ShowGrid = GlobalSettings.ShowGrid ? (short)1 : (short)0;

            VsoWindow.ShowPageBreaks = 0;

            VsoWindow.Page = pages[1];

            ComboCurrentPage = new GraphicsPage(VsoWindow.Page);

            ComboCurrentWindow = new GraphicsWindow(VsoWindow);

            VisioInterop.SetPageSize(ComboCurrentPage, 16, 12);
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
            int cntlSizeY = formSizeY -  64;

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
            int flowSizeY = grbDefineSizeY - flowLocnY- 32;

            this.flpCutChoice.Size = new Size(flowSizeX, flowSizeY);
        }
        
        private CanvasCut GetSelectedRollout(double x, double y)
        {
            Visio.Selection selection = ComboCurrentPage.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

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
                if (rolloutDict.ContainsKey(visioShape.NameID))
                {
                    return this.rolloutDict[visioShape.NameID];
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

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            // install message filter when form activates
            Application.AddMessageFilter(BaseForm);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // remove message filter when form deactivates
            Application.RemoveMessageFilter(BaseForm);
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == (int)WindowsMessage.WM_MOUSEMOVE)
            {
                //BaseForm.SetCursorForCurrentLocation();
#if DEBUG
                BaseForm.UpdateMousePositionDisplay();
#endif
            }

            return false;
        }
    }
}
