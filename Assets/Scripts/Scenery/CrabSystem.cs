using Beach.Carry;
using Beach.Digging;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Beach.Scenery
{
    public class CrabSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            // Spawn new crabs
            Entities.ForEach((ref CrabSpawner spawner) =>
            {
                float spawnChance = spawner.Rate * Time.DeltaTime;
                if (UnityEngine.Random.value > spawnChance)
                    return;

                var newCrab = EntityManager.Instantiate(spawner.Template);
                var y = UnityEngine.Random.Range(-8f, 8f);
                var x = UnityEngine.Random.value > 0.5f ? -10f: 10f;
                var position = new float3(x, y, 0f);
                EntityManager.SetComponentData(newCrab,
                    new Translation { Value = position }
                    );

                var targetPosition = new float2(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f));
                var delta = targetPosition - position.xy;
                EntityManager.SetComponentData(newCrab,
                    new Crab { Velocity = math.normalize(delta.xy) * 2f }
                    );
                Debug.Log("Spawned a crab!");
            });

            // Update position of crabs
            Entities.WithNone<Carried,Buried>().ForEach((ref Translation translation, ref Crab crab) =>
            {
                translation.Value.xy += crab.Velocity * Time.DeltaTime;
            }
            );
        }
    }
}