
namespace CanvasShapes
{
    using MaterialsLayout;

    public partial class CanvasLayoutArea : GraphicsLayoutArea, IAreaShape
    {
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
