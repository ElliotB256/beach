using Unity.Entities;
using Unity.Transforms;

namespace Beach.Scenery
{
    /// <summary>
    /// Moves floating entities with the tide.
    /// </summary>
    public class MoveFloatingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var tide = GetSingleton<Tide>();

            Entities.WithAll<Floating>().ForEach(
                (ref Translation translation) =>
                {
                    var delta = tide.Speed * Time.DeltaTime;
                    // We favor moving things inland - so reduce negative values by some factor.
                    delta = delta < 0 ? delta * 0.7f : delta;
                    translation.Value.y += delta;
                }
                );
        }
    }
}