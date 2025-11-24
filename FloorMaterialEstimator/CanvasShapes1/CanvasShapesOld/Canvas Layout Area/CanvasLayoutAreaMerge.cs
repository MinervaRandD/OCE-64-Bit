using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasManagerLib
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Geometry;
    using Graphics;
    using MaterialsLayout;

    using FloorMaterialEstimator.Finish_Controls;
    using FinishesLib;
    using SettingsLib;
    using Utilities;
    using Globals;
    using MaterialsLayout.MaterialsLayout;
    using CanvasLib.Markers_and_Guides;
    using DebugSupport;
    using TracerLib;

    public partial class CanvasLayoutArea : GraphicsLayoutArea, IAreaShape
    {
#if false
        List<CanvasLayoutArea> AreasSelectedForMergingList = new List<CanvasLayoutArea>();

        public void SelectAreaForMerging(double x, double y)
        {
            CanvasLayoutArea layoutAreaForMerging = canvasManager.CurrentPage.GetContainingLayoutArea(x, y);

            if (layoutAreaForMerging is null)
            {
                return;
            }

            if (layoutAreaForMerging.IsSubdivided())
            {
                ManagedMessageBox.Show("Cannot merge this area: it is further subdivided.");

                return;
            }

            CanvasLayoutArea parentLayoutArea = layoutAreaForMerging.ParentArea;

            if (parentLayoutArea is null)
            {
                ManagedMessageBox.Show("Cannot merge this area: it is a top level layout area.");

                return;
            }

            List<CanvasLayoutArea> siblingCanvasLayoutAreaList = new List<CanvasLayoutArea>();

            foreach (CanvasLayoutArea canvasLayoutArea in parentLayoutArea.OffspringAreas)
            {
                if (canvasLayoutArea.IsSubdivided())
                {
                    ManagedMessageBox.Show("Cannot merge this area: at least one sibling is further subdivided.");

                    return;
                }

                siblingCanvasLayoutAreaList.Add(canvasLayoutArea);
            }

            if (siblingCanvasLayoutAreaList.Count <= 1)
            {
                ManagedMessageBox.Show("This area does not have any siblings for merging");

                return;
            }

            foreach (CanvasLayoutArea canvasLayoutArea in siblingCanvasLayoutAreaList)
            {
                canvasLayoutArea.SetFillColor(Color.LightGray);
            }

            DialogResult dr = ManagedMessageBox.Show("OK to merge areas in gray?", "Confirm Merge", MessageBoxButtons.OKCancel);

            if (dr != DialogResult.OK)
            {
                return;
            }

            foreach (CanvasLayoutArea siblingLayoutArea in siblingCanvasLayoutAreaList)
            {
                // Cannot update finish stats in 'RemoveLayoutArea' until all siblings are removed.

                siblingLayoutArea.AreaFinishManager.SeamDesignStateLayer.RemoveShapeFromLayer(siblingLayoutArea, 1);

                siblingLayoutArea.UCAreaFinish.RemoveLayoutArea(siblingLayoutArea, false);
            }

            parentLayoutArea.MergeOffsprings();

            parentLayoutArea.AreaFinishManager.SeamDesignStateLayer.AddShapeToLayer(parentLayoutArea, 1);

            parentLayoutArea.SetSeamDesignStateFormat(SeamMode.Subdivision);

            // Make the area and the associated lines visible.

            Page.InvisibleWorkspaceLayer.RemoveShapeFromLayer(parentLayoutArea, 1);

            foreach (CanvasDirectedLine externAreaLine in parentLayoutArea.ExternalArea)
            {
                Page.InvisibleWorkspaceLayer.RemoveShapeFromLayer(externAreaLine, 1);

                externAreaLine.ucLine.LineFinishManager.SeamDesignStateLayer.AddShapeToLayer(externAreaLine, 1);
            }

            foreach (CanvasDirectedPolygon internalArea in parentLayoutArea.InternalAreas)
            {
                foreach (CanvasDirectedLine internalAreaLine in internalArea)
                {
                    Page.InvisibleWorkspaceLayer.RemoveShapeFromLayer(internalAreaLine, 1);

                    internalAreaLine.ucLine.LineFinishManager.SeamDesignStateLayer.AddShapeToLayer(internalAreaLine, 1);
                }
            }

            return;


            //AddSeamCanvasLayoutAreaToCanvas(parentLayoutArea, parentLayoutArea.UCAreaFinish);

            //if (parentLayoutArea.ParentArea is null)
            //{
            //    parentLayoutArea.RemoveFromLayer(parentLayoutArea.AreaFinishManager.SeamDesignStateLayer);


            //}

            //parentLayoutArea.ExternalArea.SetLineGraphics(DesignState.Seam, false, AreaShapeBuildStatus.Completed);

            //--------------------------------------------------------------------------------------------------------------------//

            //-----------------------------------------------------------------------------//
            // The following adds back the external perimeter and internal perimeter lines //
            // to the line dictionary. It shouldn't be necessary, but until the code is    //
            // cleaned up... Note the defensive measures.                                  //
            //-----------------------------------------------------------------------------//

            foreach (CanvasDirectedLine externalPerimeterLine in parentLayoutArea.ExternalArea.Perimeter)
            {
                UCLineFinishPaletteElement ucLine = externalPerimeterLine.ucLine;
                GraphicsLayer layer = ucLine.SeamDesignStateLayer.GetBaseLayer();
                bool visibility = layer.Visibility;

                // externalPerimeterLine.Delete();
                // externalPerimeterLine.Draw(Color.Red, 0.25);

                layer.AddShape(externalPerimeterLine.Shape, 1);
                layer.AddToShapeDict(externalPerimeterLine.Shape);
                //-------------------------------------//
                // Defensive. Should not be necessary. //
                //-------------------------------------//

                if (canvasManager.CurrentPage.DirectedLineDictContains(externalPerimeterLine))
                {
                    continue;
                }


                canvasManager.CurrentPage.AddToDirectedLineDict(externalPerimeterLine);
            }

            foreach (CanvasDirectedPolygon internalArea in parentLayoutArea.InternalAreas)
            {
                foreach (CanvasDirectedLine internalPerimeterLine in internalArea.Perimeter)
                {
                    //-------------------------------------//
                    // Defensive. Should not be necessary. //
                    //-------------------------------------//

                    if (canvasManager.CurrentPage.DirectedLineDictContains(internalPerimeterLine))
                    {
                        continue;
                    }

                    canvasManager.CurrentPage.AddToDirectedLineDict(internalPerimeterLine);
                }
            }

            //--------------------------------------------------------------------------------------------------------------------//


            parentLayoutArea.ExternalArea.Perimeter.ForEach(l => VisioInterop.SetBaseLineOpacity(l.Shape, 0));
            parentLayoutArea.ExternalArea.Perimeter.ForEach(l => VisioInterop.SetBaseLineColor(l.Shape, Color.Blue));

            parentLayoutArea.InternalAreas.ForEach(
                ia => ia.SetLineGraphics(DesignState.Seam, false, AreaShapeBuildStatus.Completed));

            parentLayoutArea.SetSeamDesignStateFormat(SeamMode.Selection);

            Window?.DeselectAll();
        }

#endif

        internal void MergeOffsprings()
        {
            foreach (CanvasLayoutArea offspringLayoutArea in OffspringAreas)
            {
                offspringLayoutArea.Delete();
            }

            ClearOffspringAreas();

            if (this.ParentArea is null)
            {
                // We do not need to redraw the shape if it is a top level layout area as it will already
                // exist in area mode.

                return;
            }
        }

    }
}
