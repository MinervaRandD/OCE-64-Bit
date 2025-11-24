using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphics;
using Utilities;
using AxMicrosoft.Office.Interop.VisOcx;
using Visio = Microsoft.Office.Interop.Visio;

namespace FloorMaterialEstimator.CanvasManager
{
    public class SeamStateLockIconStencil
    {
        public string DisplayName { get; }
        public Color Color { get; }
        public Visio.Document Stencil { get; }
        
        public SeamStateLockIconStencil(string displayName, Color color, Visio.Document stencil) 
        {
            this.DisplayName = displayName;
            this.Color = color;
            this.Stencil = stencil;
        }

        static public SeamStateLockIconStencil CreateFromFile(string displayName, Color color, string seamStateLockIconPath, AxDrawingControl axDrawingControl) 
        {
            Visio.Document stencil = axDrawingControl.Document.Application.Documents.OpenEx(seamStateLockIconPath, (short)Visio.VisOpenSaveArgs.visOpenHidden);
            return new SeamStateLockIconStencil(displayName, color, stencil);
        }
    }
}
