using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorMaterialEstimator;
using CanvasLib;
using Geometry;
using Utilities;
using CanvasLib.Markers_and_Guides;
using System.Drawing;
using Graphics;
using System.Windows.Forms;

namespace FloorMaterialEstimator.CanvasManager
{
    public static class VertexEditor
    {
        private static CanvasLayoutArea canvasLayoutArea;
        
        private static CanvasDirectedLine line1 = null;
        private static CanvasDirectedLine line2 = null;

        private static Coordinate vertexCoord = Coordinate.NullCoordinate;

        private static CanvasPage canvasPage;

        private static GraphicsWindow window;

        private static GraphicsPage page;

        public static bool VertexEditStart(CanvasPage canvasPageParm, GraphicsWindow windowParm, GraphicsPage pageParm, double x, double y)
        {
            canvasPage = canvasPageParm;

            window = windowParm;

            page = pageParm;

            canvasLayoutArea = canvasPage.GetSelectedLayoutArea(x, y);

            if (Utilities.Utilities.IsNotNull(canvasLayoutArea))
            {
   
                vertexCoord = canvasLayoutArea.GetSelectedVertex(x, y, out line1, out line2);

                if (!Coordinate.IsNullCoordinate(vertexCoord))
                {
                    canvasLayoutArea.VertexEditMarker = new VertexEditMarker(window, page);

                    canvasLayoutArea.VertexEditMarker.Draw(vertexCoord.X, vertexCoord.Y);

                    return true;
                }
            }

            return false;
        }

        internal static void MoveVertexMarker(double x, double y)
        {

           canvasLayoutArea.VertexEditMarker.MoveTo(x, y);
        }

        internal static void TerminateVertexEditing()
        {
            canvasLayoutArea.VertexEditMarker.Delete();
        }

        internal static void CompleteVertexEditing(double x, double y)
        {
            Coordinate newVertexCoord = new Coordinate(x, y);

            if (Coordinate.H2Distance(line1.Coord1, vertexCoord) < 0.01)
            {
                line1.Coord1 = newVertexCoord;
            }

            else
            {
                line1.Coord2 = newVertexCoord;
            }

            if (Coordinate.H2Distance(line2.Coord1, vertexCoord) < 0.01)
            {
                line2.Coord1 = newVertexCoord;
            }

            else
            {
                line2.Coord2 = newVertexCoord;
            }

            canvasLayoutArea.VertexEditMarker.Delete();

            string guid = canvasLayoutArea.Guid;

            canvasPage.RemoveLayoutArea(guid);

            canvasLayoutArea.AreaFinishManager.RemoveLayoutArea(canvasLayoutArea);

            CanvasLayoutArea resizedCanvasLayoutArea = canvasLayoutArea.Clone();

            canvasLayoutArea.Delete(true);

            
            resizedCanvasLayoutArea.DrawCompositeShape(window, page);

            resizedCanvasLayoutArea.AreaFinishManager.AddNormalLayoutArea(resizedCanvasLayoutArea);

            resizedCanvasLayoutArea.AddAssociatedLines(window, page, line1.ucLine.LineName);

            canvasPage.AddLayoutArea(resizedCanvasLayoutArea);

            resizedCanvasLayoutArea.SetAreaDesignStateFormat(false);
        }
    }
}
