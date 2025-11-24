using CanvasLib.Labels;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics;
using Utilities;
using Visio = Microsoft.Office.Interop.Visio;

namespace FloorMaterialEstimator.CanvasManager
{
    public class LabelManager : ILabelManager
    {
        // UCAreaFinishPalette areaFinishPalette;

        private Label activeLabel;

        private LabelForm labelForm;

        private FloorMaterialEstimatorBaseForm baseForm;

        private System.Windows.Forms.ToolStripButton btnLabel;

        private GraphicsPage page;

        private GraphicsWindow window;

        private AreaFinishManagerList areaFinishManagerList;

        private AreaFinishManager currentAreaFinishManger => areaFinishManagerList[areaFinishManagerList.SelectedIndex];

        public Label ActiveLabel { get { return this.activeLabel; } set { } }

        private bool LabelsActive { get { return labelForm != null; } }

        public LabelManager(
            AreaFinishManagerList areaFinishManagerList,
            FloorMaterialEstimatorBaseForm baseForm,
            GraphicsWindow window,
            GraphicsPage page,
            System.Windows.Forms.ToolStripButton btnLabel)
        {
            this.areaFinishManagerList = areaFinishManagerList;
            this.baseForm = baseForm;
            //this.vsoWindow = vsoWindow;
            this.page = page;
            this.btnLabel = btnLabel;

            this.activeLabel = new Label();
        }

        public void ProcessLabelModeClick(double x, double y)
        {
            GraphicsLabel graphicsLabel = GetSelectedLabel(x, y);

            if (graphicsLabel == null)
            {
                if (!string.IsNullOrWhiteSpace(activeLabel.Text))
                {
                    AddLabel(x, y);
                }

            }
            else
            {
                this.activeLabel.CopyFrom(graphicsLabel.Label);
                this.labelForm.GetLabelInfo();

                currentAreaFinishManger.RemoveLabel(graphicsLabel);
            }
        }

        public List<Label> GetLabelList()
        {
            List<Label> labelList = new List<Label>();

            foreach (AreaFinishManager areaFinishManager in this.areaFinishManagerList)
            {
                foreach (GraphicsLabel graphicsLabel in areaFinishManager.GraphicsLabels)
                {
                    labelList.Add(graphicsLabel.Label);
                }
            }


            return labelList;
        }

        public void SetlabelList(List<Label> labelList)
        {
            ResetLabelList();

            foreach (Label newLabel in labelList)
            {
                AreaFinishManager areaFinishManager = this.areaFinishManagerList[newLabel.Guid];

                if (Utilities.Utilities.IsNotNull(areaFinishManager))
                {
                    AddNewGraphicLabel(newLabel, areaFinishManager);
                }

            }
        }

        private void AddLabel(double x, double y)
        {
            Label newLabel = new Label(activeLabel, new Geometry.Coordinate(x, y));

            AddNewGraphicLabel(newLabel, this.currentAreaFinishManger);
        }


        private void AddNewGraphicLabel(Label newLabel, AreaFinishManager element)
        {
            GraphicsLabel graphicsLabel = new GraphicsLabel(newLabel, this.window, this.page);

            graphicsLabel.Draw();

            window?.DeselectAll();

            element.AddLabel(graphicsLabel);

        }

        public void ResetLabelList()
        {
            foreach (AreaFinishManager areaFinishManager in this.areaFinishManagerList)
            {
                areaFinishManager.ResetLabelList();
            }
        }

        //public void ActivateLabels()
        //{
        //    ActivateLayers();

        //    if (this.labelForm == null)
        //    {
        //        this.labelForm = new LabelForm(this);
        //        this.labelForm.Show(this.baseForm);
        //        this.labelForm.BringToFront();
        //    }
        //    this.btnLabel.Checked = true;

        //}
        public void DeActivateLabels()
        {
            //ResetCurrentLayer();

            if (this.labelForm != null)
            {
                var form = this.labelForm;
                this.labelForm = null;

                form.Close();

            }

            this.baseForm.ResetDesignLayers();
            this.btnLabel.Checked = false;
        }

        //private void AreaFinishPalette_FinishSelected(UCAreaFinishPaletteElement selectedFinish)
        //{
        //    if (LabelsActive)
        //    {
        //        ActivateLayers();
        //    }
        //}

        private void ActivateLayers()
        {
            this.currentAreaFinishManger.SetupAllSeamLayers();
        }


        private GraphicsLabel GetSelectedLabel(double x, double y)
        {
            Visio.Selection selection = page.VisioPage.SpatialSearch[x, y, (short)Visio.VisSpatialRelationCodes.visSpatialContainedIn, 0.01, 0];

            if (selection.Count > 0)
            {
                foreach (Visio.Shape visioShape in selection)
                {
                    string guid = visioShape.Data3;

                    if (this.currentAreaFinishManger.GraphicsLabelDict.ContainsKey(guid))
                    {
                        return this.currentAreaFinishManger.GraphicsLabelDict[guid];
                    }
                }
            }

            return null;
        }


    }

}
