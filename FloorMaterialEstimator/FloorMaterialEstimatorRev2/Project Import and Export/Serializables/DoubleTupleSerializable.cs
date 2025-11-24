using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorMaterialEstimator
{
    public class DoubleTupleSerializable
    {
        public double? Item1 { get; set; }

        public double? Item2 { get; set; }

        public DoubleTupleSerializable() { }

        public DoubleTupleSerializable(Tuple<double, double> doubleTuple)
        {
            if (doubleTuple is null)
            {
                Item1 = null;
                Item2 = null;
            }

            else
            {
                Item1 = doubleTuple.Item1;
                Item2 = doubleTuple.Item2;
            }
        }

        public Tuple<double, double> Deserialize()
        {
            if (Item1 is null || Item2 is null)
            {
                return null;
            }

            return new Tuple<double, double>(Item1.Value, Item2.Value);
        }
    }
}
