using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Scenery
{
    /// <summary>
    /// Makes entities bob up and down when they are floating.
    /// </summary>
    public class BobbingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Floating>().ForEach(
                (ref Translation translation) =>
                {
                    translation.Value.z = -0.5f*math.pow(math.sin(math.dot(translation.Value.xy, new float2(1.0f, 1.0f)) - (float)Time.ElapsedTime), 2);
                }
                );
        }
    }
}