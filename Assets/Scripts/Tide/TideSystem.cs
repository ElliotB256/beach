using Beach.Misc;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Scenery
{
    /// <summary>
    /// Handles the ebb and flow of the tide.
    /// </summary>
    public class TideSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Initialising>().ForEach((Entity e, ref Tide tide, ref Translation translation) =>
            {
                tide.Origin = translation.Value;
                EntityManager.RemoveComponent<Initialising>(e);
            });

            Entities.ForEach((ref Tide tide, ref Translation translation) =>
            {
                tide.Phase += Time.DeltaTime / tide.TimePeriod;
                if (tide.Phase > 1f)
                    tide.Phase = 0f;

                tide.Speed = -tide.TranslationAmplitude * math.sin(math.PI * 2 * tide.Phase) / 2f;
                translation.Value = tide.Origin + tide.TranslationAmplitude * new float3(0.0f, 1.0f, 0.0f) * math.cos(math.PI * 2 * tide.Phase);
            });
        }
    }
}