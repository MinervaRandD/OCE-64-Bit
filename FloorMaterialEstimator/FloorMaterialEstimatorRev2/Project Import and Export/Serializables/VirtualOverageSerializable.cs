

namespace FloorMaterialEstimator
{
    using MaterialsLayout;
    using System;

    public class VirtualOverageSerializable
    {
        public string AreaFinishGuid { get; set; }

        public DoubleTupleSerializable EffectiveDimensions { get; set; } = null;

        public DoubleTupleSerializable OverrideEffectiveDimensions { get; set; } = null;

        public VirtualOverageSerializable() { }

        public VirtualOverageSerializable(VirtualOverage virtualOverage, string areaFinishGuid)
        {
            AreaFinishGuid = areaFinishGuid;

            if (!(virtualOverage.EffectiveDimensions is null))
            {
                EffectiveDimensions = new DoubleTupleSerializable(virtualOverage.EffectiveDimensions);
            }

            if (!(virtualOverage.OverrideEffectiveDimensions is null))
            {
                OverrideEffectiveDimensions = new DoubleTupleSerializable(virtualOverage.OverrideEffectiveDimensions);
            }
        }

        public VirtualOverage Deserialize()
        {
            Tuple<double, double> effectiveDimensions = null;
            Tuple<double, double> overrideEffectiveDimensions = null;

            if (!(this.EffectiveDimensions is null))
            {
                effectiveDimensions = this.EffectiveDimensions.Deserialize();
            }


            if (!(this.OverrideEffectiveDimensions is null))
            {
                overrideEffectiveDimensions = this.OverrideEffectiveDimensions.Deserialize();
            }

            VirtualOverage virtualOverage = new VirtualOverage()
            {
               EffectiveDimensions = effectiveDimensions
               ,OverrideEffectiveDimensions = overrideEffectiveDimensions
            };

            return virtualOverage;
        }
    }
}