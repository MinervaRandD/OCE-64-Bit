using Geometry;
using Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visio = Microsoft.Office.Interop.Visio;

namespace CanvasLib.LockIcon
{
    public class LockIcon
    {
        //Coordinate lockIconLocation = null;

        bool lockIconVisible = false;

        private GraphicShape _lockIcon = null;

        public GraphicShape lockIcon
        {
            get;
            set;
        } = null;

        private GraphicsWindow window = null;
        private GraphicsPage page = null;
        private GraphicShape shape = null;

        public Visio.Shape visioLockIconShape
        {
            get;
            set;
        } = null;

        public LockIcon(GraphicsWindow window, GraphicsPage page)
        {
            this.window = window;
            this.page = page;

            createLockIcon();
        }

        private void createLockIcon()
        {
            visioLockIconShape = VisioInterop.CreateLockIcon(page.VisioPage);

            lockIcon = new GraphicShape(this, window, page, visioLockIconShape, ShapeType.LockIcon);

            lockIcon.SetShapeData("[LockIcon]", "Image", lockIcon.Guid);

            page.AddToPageShapeDict(lockIcon);

            VisioInterop.SetShapeSize(lockIcon, 0, 0);
        }

        public void ShowLockIcon(GraphicShape shape)
        {

            if (this.shape == shape && lockIconVisible)
            {
                return;
            }

            this.shape = shape;

            double shapeArea = VisioInterop.GetShapeArea(shape);

            VisioInterop.BeginFastUpdate();

            VisioInterop.SetShapeCenter(lockIcon, shape.CenterPoint);

            double w = Math.Sqrt(shapeArea) * 0.4;
            double h = w;

            double d = Math.Min(w, h) * 0.25;

            VisioInterop.SetShapeSize(lockIcon, h, w);

            lockIconVisible = true;

            VisioInterop.EndFastUpdate();

        }
        public void HideLockIcon()
        {
            if (lockIconVisible == false)
            {
                return;
            }


            VisioInterop.BeginFastUpdate();
            VisioInterop.SetShapeSize(lockIcon, 0, 0);
            VisioInterop.EndFastUpdate();

            lockIconVisible = false;

            this.shape = null;
        }

    }
}
