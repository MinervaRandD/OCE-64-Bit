using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasShapes
{
    public interface ICanvasManager
    {
        Dictionary<string, CanvasLayoutArea> LayoutAreaForSubdivisionDict { get; set; }
    }
}
