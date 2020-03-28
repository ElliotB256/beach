using Beach.Focus;
using Unity.Entities;
using Unity.Jobs;

namespace Beach.Carry
{
    [UpdateInGroup(typeof(FocusSystemGroup))]
    [UpdateAfter(typeof(ClearFocusFlagsSystem))]
    [UpdateBefore(typeof(FocusSystem))]
    public class SetFocusForPickupSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var carryableFlagsJobHandle =
                Entities.WithNone<Carried>().WithAll<Carryable>().ForEach((ref Focusable f) => f.Category = FocusType.Carryable)
                .Schedule(inputDeps);
            var carrierFlagsJobHandle =
                Entities.WithNone<Carrying>().ForEach(
                    (ref Focussing f, ref Carrier carrier) =>
                    {
                        f.Intention = FocusType.Carryable;
                        f.Range = carrier.PickupRange;
                    })
                .Schedule(carryableFlagsJobHandle);
            return carrierFlagsJobHandle;
        }
    }
}
