using Beach.Focus;
using Unity.Entities;
using Unity.Jobs;

namespace Beach.Carry
{
    [UpdateInGroup(typeof(FocusSystemGroup))]
    public class ClearFocusFlagsSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var clearFocusableJobHandle = Entities.ForEach((ref Focusable f) => f.Category = FocusType.None).Schedule(inputDeps);
            var clearFocussingJobHandle = Entities
                .ForEach(
                (ref Focussing f) =>
                {
                    f.Intention = FocusType.None;
                    f.Range = 0f;
                }).Schedule(inputDeps);
            return JobHandle.CombineDependencies(clearFocusableJobHandle, clearFocussingJobHandle);
        }
    }
}
