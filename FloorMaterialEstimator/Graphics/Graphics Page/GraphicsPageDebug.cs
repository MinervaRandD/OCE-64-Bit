using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public partial class GraphicsPage
    {

        public bool CheckForNullDataShape()
        {

            foreach (IGraphicsShape iShape in PageShapeDictValues)
            {
                GraphicShape shape = (GraphicShape)iShape;

                try
                {
                    if (shape is null)
                    {
                        return false;
                    }

                    if (string.IsNullOrEmpty(shape.Data1) && string.IsNullOrEmpty(shape.Data2) &&
                        string.IsNullOrEmpty(shape.Data3))
                    {
                        return false;
                    }
                }

                catch (Exception ex)
                {
                    {
                        ;
                    }
                }
            }

            return false;
        }
    }
}
