using Unity.Entities;
using Unity.Transforms;

namespace Beach.Scenery
{
    /// <summary>
    /// Grounds entities on the floor after they have been floating.
    /// </summary>
    [UpdateAfter(typeof(SetFloatingSystem))]
    [UpdateBefore(typeof(RemoveStartEndFloatingSystem))]
    public class GroundAfterFloatingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<FloatingEnd>().ForEach((ref Translation translation) => translation.Value.z = 0f);
        }
    }
}