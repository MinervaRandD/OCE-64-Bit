using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasLib.Labels
{
    public interface ILabelManager
    {
        Label ActiveLabel { get; set; }
        void DeActivateLabels();
    }
}
