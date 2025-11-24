
namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.CanvasManager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using Globals;
    using PaletteLib;
    using MaterialsLayout;
    using Utilities;
    using Geometry;
    using Graphics;
    using FloorMaterialEstimator.Finish_Controls;
    using System.ComponentModel;
    using System.Security.Cryptography.X509Certificates;
    using FinishesLib;

    public class LayoutAreaSerializable
    {
        public string Guid { get; set; }

        public LayoutAreaType LayoutAreaType { get; set; }

        public string ParentAreaGuid {
            get;
            set;
        }

        public string AreaFinishGuid { get; set; }

        public string PrevAreaFinishGuid { get; set; }

        public List<string> OffspringAreaGuidList { get; set; }

        public DirectedPolygonSerializable ExternalPerimeter { get; set; }

        public List<DirectedPolygonSerializable> InternalPerimeters { get; set; }

        public List<SeamSerializable> SeamList { get; set; }

        public List<SeamSerializable> DisplaySeamList { get; set; }

        public List<RolloutSerializable> RolloutList { get; set; }

        public double FixedWidthWidth { get; set; }

        public bool CreatedFromFixedWidth { get; set; }

        public int PrevSeamAreaIndex { get; set; }

        public bool AreaDesignStateEditModeSelected { get; set; }

        public bool SeamDesignStateSelectionModeSelected { get; set; }

        public bool SeamDesignStateSubdivisionModeSelected { get; set; }

        public DirectedLineGeometricSerializable BaseSeamLineWall { get; set; }

        public SeamTagSerializable SeamTag;

        public bool IsVisible { get; set; }

        public bool IsZeroAreaLayoutArea { get; set; }

        public DesignState OriginatingDesignState { get; set; }

        //public bool IsSeamStateLocked { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public LayoutAreaSerializable() { }

        public LayoutAreaSerializable(CanvasLayoutArea layoutArea)
        {
            this.Guid = layoutArea.Guid;

            this.LayoutAreaType = layoutArea.LayoutAreaType;

            if (layoutArea.AreaFinishManager != null)
            {
                AreaFinishGuid = layoutArea.AreaFinishManager.Guid;
            }

            if (layoutArea.PrevAreaFinishManager != null)
            {
                PrevAreaFinishGuid = layoutArea.PrevAreaFinishManager.AreaFinishBase.Guid;
            }

            if (layoutArea.ParentArea != null)
            {
                this.ParentAreaGuid = layoutArea.ParentArea.Guid;
            }

            if (layoutArea.OffspringAreas != null)
            {
                this.OffspringAreaGuidList = (List<string>)layoutArea.OffspringAreas.Select(a => a.Guid).ToList();
            }

            if (layoutArea.ExternalArea != null)
            {
                this.ExternalPerimeter = new DirectedPolygonSerializable(layoutArea.ExternalArea);
            }

            if (layoutArea.InternalAreas != null)
            {
                this.InternalPerimeters =
                    (List<DirectedPolygonSerializable>)layoutArea.InternalAreas.ConvertAll(p => new DirectedPolygonSerializable(p));
            }

            if (layoutArea.CanvasSeamList != null)
            {
                this.SeamList
                    = (List<SeamSerializable>)layoutArea.CanvasSeamList.ConvertAll(s => new SeamSerializable((CanvasSeam)s));
            }

            if (layoutArea.DisplayCanvasSeamList != null)
            {
                DisplaySeamList = new List<SeamSerializable>();

                foreach (CanvasSeam canvasSeam in layoutArea.DisplayCanvasSeamList)
                {
                    GraphicShape shape = canvasSeam.GraphicsSeam.Shape;

                    canvasSeam.GraphicsSeam.Seam.Coord1 = VisioInterop.GetLineStartpoint(shape);
                    canvasSeam.GraphicsSeam.Seam.Coord2 = VisioInterop.GetLineEndpoint(shape);

                    canvasSeam.GraphicsSeam.Shape.Data1 = "[DisplaySeam]";


                    DisplaySeamList.Add(new SeamSerializable(canvasSeam));
                }
            }

            if (layoutArea.RolloutList != null)
            {
                this.RolloutList = (List<RolloutSerializable>)layoutArea.GraphicsRolloutList.ConvertAll(r => new RolloutSerializable(r));
            }

            IsVisible = layoutArea.IsVisible;

            OriginatingDesignState = layoutArea.OriginatingDesignState;

            //SeamAreaIndex = layoutArea.SeamAreaIndex;

            PrevSeamAreaIndex = layoutArea.PrevSeamAreaIndex;

            // Bad things seem to happen if these values are maintained in the current state.

            AreaDesignStateEditModeSelected = layoutArea.AreaDesignStateEditModeSelected;

            SeamDesignStateSelectionModeSelected = layoutArea.SeamDesignStateSelectionModeSelected;

            SeamDesignStateSubdivisionModeSelected = layoutArea.SeamDesignStateSubdivisionModeSelected;

            SeamTag = null;

            if (Utilities.IsNotNull(layoutArea.SeamIndexTag))
            {
                SeamTag = new SeamTagSerializable(layoutArea.SeamIndexTag);
            }

            if (Utilities.IsNotNull(layoutArea.BaseSeamLineWall))
            {
                this.BaseSeamLineWall = new DirectedLineGeometricSerializable(layoutArea.BaseSeamLineWall);
            }

            this.FixedWidthWidth = layoutArea.BorderWidthInInches;

            this.CreatedFromFixedWidth = layoutArea.CreatedFromFixedWidth;

            IsZeroAreaLayoutArea = layoutArea.IsZeroAreaLayoutArea;

            //IsSeamStateLocked = layoutArea.IsSeamStateLocked;

            this.CreatedDateTime = layoutArea.CreatedDateTime;
        }

        public CanvasLayoutArea Deserialize(
            FloorMaterialEstimatorBaseForm baseForm
            , ProjectImporter projectImporter
            , GraphicsWindow window
            , GraphicsPage page)
        {
            CanvasDirectedPolygon externalPerimeter = this.ExternalPerimeter.Deserialize(baseForm, projectImporter); // deserializeCanvasDirectedPolygon(this.ExternalPerimeter);

            List<CanvasDirectedPolygon> internalPerimeterList = new List<CanvasDirectedPolygon>();

            foreach (DirectedPolygonSerializable directedPolygon in this.InternalPerimeters)
            {
                internalPerimeterList.Add(directedPolygon.Deserialize(baseForm, projectImporter));
            }

            AreaFinishManager areaFinishManager = FinishManagerGlobals.AreaFinishManagerList[this.AreaFinishGuid];

            List<GraphicsRollout> graphicsRolloutList = new List<GraphicsRollout>();

            FinishesLibElements finishesLibElements = new FinishesLibElements(
                areaFinishManager.AreaFinishBase, null, null, areaFinishManager.AreaFinishLayers, null, null);

            foreach (RolloutSerializable rolloutSerializable in this.RolloutList)
            {
                graphicsRolloutList.Add(rolloutSerializable. Deserialize(
                    window
                    , page
                    , finishesLibElements
                    , areaFinishManager.AreaFinishLayers.CutsIndexLayer
                    , areaFinishManager.AreaFinishLayers.OversLayer
                    , areaFinishManager.AreaFinishLayers.OversIndexLayer));
            }

            UCAreaFinishPaletteElement ucAreaPaletteElement = PalettesGlobal.AreaFinishPalette[this.AreaFinishGuid];

            AreaFinishManager prevAreaFinishManger = null;

            if (!string.IsNullOrEmpty(this.PrevAreaFinishGuid))
            {
                prevAreaFinishManger = FinishManagerGlobals.AreaFinishManagerList[this.PrevAreaFinishGuid];
            }

            CanvasSeamTag canvasSeamTag = this.SeamTag is null ? null : new CanvasSeamTag(window, page, this.SeamTag);

            List<CanvasSeam> canvasSeamList = new List<CanvasSeam>();

            foreach (SeamSerializable seamSerializable in this.SeamList)
            {
                canvasSeamList.Add(seamSerializable.Deserialize(window, page, areaFinishManager.AreaFinishLayers));
            }

            List<CanvasSeam> displayCanvasSeamList = new List<CanvasSeam>();

            foreach (SeamSerializable seamSerializable1in in this.DisplaySeamList)
            {
                displayCanvasSeamList.Add(seamSerializable1in.Deserialize(window, page, areaFinishManager.AreaFinishLayers));
            }

            CanvasLayoutArea canvasLayoutArea = new CanvasLayoutArea(
                baseForm.CanvasManager
                , this.Guid
                , this.LayoutAreaType
                , this.ParentAreaGuid
                , this.OffspringAreaGuidList
                , externalPerimeter
                , internalPerimeterList
                , canvasSeamList
                , displayCanvasSeamList
                , graphicsRolloutList
                , ucAreaPaletteElement
                , areaFinishManager
                , prevAreaFinishManger
                , null
                , this.IsVisible
                //, this.IsSeamStateLocked
                , this.FixedWidthWidth
                , this.OriginatingDesignState
                , this.PrevSeamAreaIndex
                , this.AreaDesignStateEditModeSelected
                , this.SeamDesignStateSelectionModeSelected
                , this.SeamDesignStateSubdivisionModeSelected
                , canvasSeamTag
                , this.IsZeroAreaLayoutArea
                )
            {
                FinishesLibElements = finishesLibElements,
                CreatedDateTime = this.CreatedDateTime
            };


            canvasLayoutArea.CreatedFromFixedWidth = this.CreatedFromFixedWidth;

            canvasLayoutArea.BaseSeamLineWall = null;

            if (Utilities.IsNotNull(this.BaseSeamLineWall))
            {
                canvasLayoutArea.BaseSeamLineWall = this.BaseSeamLineWall.Deserialize();
            }

            canvasLayoutArea.ExternalArea.ParentLayoutArea = canvasLayoutArea;

            canvasLayoutArea.InternalAreas.ForEach(p => p.ParentLayoutArea = canvasLayoutArea);

           
            return canvasLayoutArea;
        }
    }
}
