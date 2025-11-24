using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasShapes
{
    using Graphics;

    using Visio = Microsoft.Office.Interop.Visio;

    public class CanvasWindow : GraphicsWindow
    {
        public CanvasWindow(Visio.Window visioWindow): base(visioWindow)
        {

        }
    }
}
