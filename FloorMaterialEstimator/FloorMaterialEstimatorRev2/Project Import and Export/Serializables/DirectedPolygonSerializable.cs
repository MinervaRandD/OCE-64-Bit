using FloorMaterialEstimator.CanvasManager;
using Geometry;
using Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FloorMaterialEstimator
{
    public class DirectedPolygonSerializable
    {
        public string Guid { get; set; }

        public string ParentAreaGuid { get; set; }

        public List<string> PerimeterGuidList;
        
        public DirectedPolygonSerializable() { }

        public DirectedPolygonSerializable(CanvasDirectedPolygon polygon)
        {
            this.Guid = polygon.Guid;

            if (polygon.ParentLayoutArea != null)
            {
                this.ParentAreaGuid = polygon.ParentLayoutArea.Guid;
            }

            if (polygon.Perimeter != null)
            {
                this.PerimeterGuidList = (List<string>)polygon.Perimeter.Select(p=>p.Guid).ToList();
            }
        }

        public DirectedPolygonSerializable(GraphicsDirectedPolygon polygon)
        {
            if (polygon.Perimeter != null)
            {
                this.PerimeterGuidList = (List<string>)polygon.Perimeter.Select(p => p.Guid).ToList();
            }
        }

        public CanvasDirectedPolygon Deserialize(
            FloorMaterialEstimatorBaseForm baseForm
            ,ProjectImporter projectImporter)
        {
            List<CanvasDirectedLine> perimeterLineList = new List<CanvasDirectedLine>();

            foreach (string guid in this.PerimeterGuidList)
            {
                // Need to fix.
                try
                {
                    if (projectImporter.CanvasDirectedLineDict.ContainsKey(guid))
                    {
                        perimeterLineList.Add(projectImporter.CanvasDirectedLineDict[guid]);
                    }

                }

                catch { }
            }

            // Patch.

            if (this.Guid is null)
            {
                this.Guid = Utilities.GuidMaintenance.GenerateGuid();
            }

            CanvasDirectedPolygon canvasDirectedPolygon = new CanvasDirectedPolygon(baseForm.CanvasManager, this.Guid, perimeterLineList);

            projectImporter.CanvasDirectedPolygonDict.Add(canvasDirectedPolygon.Guid, canvasDirectedPolygon);

            return canvasDirectedPolygon;
        }
    }
}
