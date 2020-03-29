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
            
            Entities.ForEach(
                (
                    DynamicBuffer <TideRubbishElement> buffer,
                    ref Tide tide,
                    ref Translation translation
                    ) =>
            {
                if (!tide.IsComingIn())
                    return;

                float spawnChance = tide.SpawnRate * Time.DeltaTime;
                if (UnityEngine.Random.value > spawnChance)
                    return;

                // Select a template
                int index = UnityEngine.Random.Range(0, buffer.Length);
                var element = buffer[index];

                var spawned = EntityManager.Instantiate(element.Template);
                EntityManager.SetComponentData(spawned,
                    new Translation
                    {
                        Value = new float3(UnityEngine.Random.Range(-10f, 10f), -7f, 0f)
                    }
                    );
            });
        }
    }
}