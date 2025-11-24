

namespace FloorMaterialEstimator
{
    using FloorMaterialEstimator.CanvasManager;
    using Geometry;
    using Graphics;
    using FinishesLib;
    using MaterialsLayout;
    using System;

    [Serializable]
    public class SeamSerializable
    {
        public string LayoutAreaGuid { get; set; }

        public Coordinate Coord1 { get; set; }

        public Coordinate Coord2 { get; set; }

        public SeamType SeamType { get; set;}

        public bool IsHideable { get; set; } 

        public SeamSerializable() { }

        public SeamSerializable(CanvasSeam canvasSeam)
        {
            if (canvasSeam.layoutArea != null)
            {
                this.LayoutAreaGuid = canvasSeam.layoutArea.Guid;
            }

            Coord1 = canvasSeam.GraphicsSeam.Seam.Coord1;
            Coord2 = canvasSeam.GraphicsSeam.Seam.Coord2;

            SeamType = canvasSeam.SeamType;

            IsHideable = canvasSeam.IsHideable;
        }

        public CanvasSeam Deserialize(GraphicsWindow window, GraphicsPage page, AreaFinishLayers areaFinishLayers)
        {
            Seam seam = new Seam(this.Coord1, this.Coord2, this.SeamType, this.IsHideable);

            GraphicsSeam graphicsSeam = new GraphicsSeam(window, page, seam);

            CanvasSeam canvasSeam = new CanvasSeam(graphicsSeam);

            canvasSeam.areaFinishLayers = areaFinishLayers;

            return canvasSeam;
        }

    }
}
