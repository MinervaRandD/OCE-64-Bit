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
    public class MeasuringStickStencil
    {
        public string DisplayName { get; }
        public Color Color { get; }
        public Visio.Document Stencil { get; }
        public MeasuringStickStencil(string displayName, Color color, Visio.Document stencil) 
        {
            this.DisplayName = displayName;
            this.Color = color;
            this.Stencil = stencil;
        }

        static public MeasuringStickStencil CreateFromFile(string displayName, Color color, string measuringStickPath, AxDrawingControl axDrawingControl) 
        {
            Visio.Document stencil = axDrawingControl.Document.Application.Documents.OpenEx(measuringStickPath, (short)Visio.VisOpenSaveArgs.visOpenHidden);
            return new MeasuringStickStencil(displayName, color, stencil);
        }
    }
}
