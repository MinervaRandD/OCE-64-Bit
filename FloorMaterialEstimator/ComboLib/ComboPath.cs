

namespace FloorMaterialEstimator
{
    using ComboLib;
    using MaterialsLayout;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ComboPath
    {
        public ComboPathElement[] Path;
        
        public ComboPath(int[] path, GraphicsComboElem[] graphicsComboElemList, double[,] wghtMtrx)
        {
            double offset = 0;

            double endpointOffset = 0;

            double maxY = getMaxYForPath(graphicsComboElemList); ;

            Path = new ComboPathElement[path.Length];

            if (path.Length <= 0)
            {
                return;
            }

            GraphicsComboElem graphicsComboElem = graphicsComboElemList[path[0]];

            ComboPathElement comboPathElement = new ComboPathElement(graphicsComboElemList[path[0]], 0.0, graphicsComboElem.Length);

            Path[0] = comboPathElement;

            for (int i1 = 1; i1 < path.Length; i1++)
            {
                graphicsComboElem = graphicsComboElemList[path[i1]];

                offset = wghtMtrx[path[i1 - 1], path[i1]];

                endpointOffset = getMaxPrevFullOffset(path, Path, i1, wghtMtrx, maxY);

                endpointOffset += graphicsComboElem.Length;

                comboPathElement = new ComboPathElement(graphicsComboElemList[path[i1]], offset, endpointOffset);

                Path[i1] = comboPathElement;
            }
        }

        private double getMaxYForPath(GraphicsComboElem[] graphicsComboElemList)
        {
            if (graphicsComboElemList.Length == 0)
            {
                return 0;
            }

            return graphicsComboElemList.Max(x => x.GraphicsDirectedPolygon.MaxY);
        }

        private double getMaxPrevFullOffset(int[] path, ComboPathElement[] comboPath, int i, double[,] wghtMtrx, double maxY)
        {
            if (i <= 0)
            {
                return 0;
            }

            double maxFullOffset = double.MinValue;

            for (int i1 = i-1; i1 >= 0; i1--)
            {
                double nxtFullOffset = comboPath[i1].EndpointOffset - wghtMtrx[path[i1], path[i]];

                if (nxtFullOffset > maxFullOffset)
                {
                    maxFullOffset = nxtFullOffset;
                }

                if (Math.Abs(comboPath[i1].GraphicsComboElem.GraphicsDirectedPolygon.MaxY - maxY) < 1.0e-6)
                {
                    return maxFullOffset;
                }
            }

            return maxFullOffset;
        }


        public int Length => Path.Length;

        public double Width()
        {
            if (Path == null)
            {
                return 0;
            }

            if (Path.Length <= 0)
            {
                return 0;
            }

            ComboPathElement pathElement = Path[Length - 1];

            double length1 = pathElement.EndpointOffset;

            //double offset = 0;



            //double length = getCutLength(pathElement.GraphicsComboElem);


            //// MDD Remove
            //if (Path.Length == 3)
            //{
            //    return length + getCutLength(Path[2].GraphicsComboElem);
            //}

            //for (int i = 1; i < Path.Length; i++)
            //{
            //    pathElement = Path[i];

            //    offset += length - pathElement.Offset;

            //    length = getCutLength(pathElement.GraphicsComboElem);
            //}

            //double length2 = (offset + length);

            return length1;
        }

        private double getCutLength(GraphicsComboElem graphicsComboElem) => graphicsComboElem.GraphicsDirectedPolygon.MaxX - graphicsComboElem.GraphicsDirectedPolygon.MinX;

        public double MaxY => Path.Max(p => p.GraphicsComboElem.GraphicsDirectedPolygon.MaxY);
        
        public ComboPathElement this[int i]
        {
            get
            {
                return Path[i];
            }
        }
    }
}
