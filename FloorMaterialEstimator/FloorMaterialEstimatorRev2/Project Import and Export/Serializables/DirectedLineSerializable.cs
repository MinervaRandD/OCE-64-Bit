

namespace FloorMaterialEstimator
{
    using System;

    using Globals;
    using FloorMaterialEstimator.CanvasManager;
    using Geometry;
    using Graphics;
    using SettingsLib;
    
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DirectedLineSerializable
    {
        public string Guid { get; set; }

        public string LineFinishGuid { get; set; }
        
        public LineDrawoutMode LineDrawoutMode { get; set; }

        public DesignState OriginatingDesignState { get; set; }

        public string ParentAreaGuid { get; set; }

        public string ParentPolygonGuid { get; set; }

        public string AssociatedLineGuid { get; set; }

        public bool IsZeroLine { get; set; }

        public bool IsCompleteLLine { get; set; }

        public bool IsSeamable { get; set; }

        public LineCompoundType LineCompoundType { get; set; }

        public Coordinate Coord1 { get; set; }

        public Coordinate Coord2 { get; set; }

        public LineRole LineRole { get; set; }

        public DirectedLineSerializable() { }

        public DirectedLineSerializable(CanvasDirectedLine line)
        {
            this.Guid = line.Guid;

            if (line.ucLine != null)
            {
                this.LineFinishGuid = line.ucLine.LineFinishBase.Guid;
            }

            else
            {
                this.LineFinishGuid = "";
            }

            this.LineDrawoutMode = line.LineDrawoutMode;

            this.OriginatingDesignState = line.OriginatingDesignState;

            this.IsZeroLine = line.IsZeroLine;

            this.IsCompleteLLine = line.IsCompleteLLine;

            if (line.ParentLayoutArea != null)
            {
                this.ParentAreaGuid = line.ParentLayoutArea.Guid;
            }

            if (line.ParentPolygon != null)
            {
                this.ParentPolygonGuid = line.ParentPolygon.Guid;
            }

            this.LineCompoundType = line.LineCompoundType;

            this.Coord1 = line.Coord1;
            this.Coord2 = line.Coord2;

            LineRole = line.LineRole;

            IsSeamable = line.IsSeamable;

            if (line.IsPerimeterRelatedLine)
            {
                AssociatedLineGuid = line.AssociatedDirectedLine.Guid;
            }
            else
            {
                AssociatedLineGuid = null;
            }
        }
    }
}
