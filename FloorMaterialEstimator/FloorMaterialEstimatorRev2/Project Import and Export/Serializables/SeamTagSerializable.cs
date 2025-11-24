using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloorMaterialEstimator.CanvasManager;

namespace FloorMaterialEstimator
{
    [Serializable]
    public class SeamTagSerializable
    {
        public SeamTagSerializable() { }

        public SeamTagSerializable(CanvasSeamTag seamIndexTag)
        {
            this.X = seamIndexTag.X;
            this.Y = seamIndexTag.Y;
            this.SeamAreaIndex = seamIndexTag.SeamAreaIndex;
            this.Guid = seamIndexTag.Guid;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public string Guid { get; set; }

        public int SeamAreaIndex { get; set; }
    }
}
