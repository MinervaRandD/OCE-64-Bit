

namespace FloorMaterialEstimator
{
    using MaterialsLayout;
    using System;

    public class VirtualUndrageSerializable
    {
        public string AreaFinishGuid { get; set; }

        public DoubleTupleSerializable EffectiveDimensions { get; set; } = null;

        public DoubleTupleSerializable OverrideEffectiveDimensions { get; set; } = null;

        public bool Deleted { get; set; }

        public VirtualUndrageSerializable() { }

        public VirtualUndrageSerializable(VirtualUndrage virtualUndrage, string areaFinishGuid)
        {
            AreaFinishGuid = areaFinishGuid;

            if (!(virtualUndrage.EffectiveDimensions is null))
            {
                EffectiveDimensions = new DoubleTupleSerializable(virtualUndrage.EffectiveDimensions);
            }

            if (!(virtualUndrage.OverrideEffectiveDimensions is null))
            {
                OverrideEffectiveDimensions = new DoubleTupleSerializable(virtualUndrage.OverrideEffectiveDimensions);
            }

        }

        public VirtualUndrage Deserialize()
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

            VirtualUndrage virtualUndrage = new VirtualUndrage()
            {
                EffectiveDimensions = effectiveDimensions
               ,
                OverrideEffectiveDimensions = overrideEffectiveDimensions
            };

            return virtualUndrage;
        }
    }

}