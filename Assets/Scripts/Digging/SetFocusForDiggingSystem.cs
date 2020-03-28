using Beach.Digging;
using Beach.Focus;
using Unity.Entities;
using Unity.Jobs;

namespace Beach.Carry
{
    [UpdateInGroup(typeof(FocusSystemGroup))]
    [UpdateAfter(typeof(ClearFocusFlagsSystem))]
    [UpdateBefore(typeof(FocusSystem))]
    public class SetFocusForDiggingSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var carryableFlagsJobHandle =
                Entities.WithAll<Buried>().ForEach((ref Focusable f) => f.Category = FocusType.Buried)
                .Schedule(inputDeps);

            var carrierFlagsJobHandle =
                Entities.ForEach(
                    (ref Focussing f, ref Digger digger) =>
                    {
                        if (digger.WantsToDig)
                        {
                            f.Intention = FocusType.Buried;
                            f.Range = digger.DigRange;
                        }
                    })
                .Schedule(carryableFlagsJobHandle);
            return carrierFlagsJobHandle;
        }
    }
}
