using Beach.Misc;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Scenery
{
    /// <summary>
    /// Spawns rubbishs when the tide comes in.
    /// </summary>
    public class TideSpawnRubbishSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Tide tide, ref Translation translation) =>
            {
                tide.Phase += Time.DeltaTime / tide.TimePeriod;
                if (tide.Phase > 1f)
                    tide.Phase = 0f;

                translation.Value = tide.Origin + tide.TranslationAmplitude * new float3(0.0f, 1.0f, 0.0f) * math.cos(math.PI * 2 * tide.Phase);
            });
        }
    }
}