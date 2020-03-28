using Beach.Digging;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Rubbish
{
    /// <summary>
    /// Spawns rubbish at random positions for each spawner.
    /// </summary>
    public class RubbishSpawnSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach(
                (Entity entity, ref RubbishSpawner spawner, ref Translation translation) =>
                {
                    // Create junk for each spawner
                    for (var i = 0; i < spawner.Number; i++)
                    {
                        var e = EntityManager.Instantiate(spawner.Template);

                        // Create random position
                        var delta = new float3(
                            UnityEngine.Random.Range(-spawner.Width / 2f, spawner.Width / 2f),
                            UnityEngine.Random.Range(-spawner.Height / 2f, spawner.Height / 2f),
                            0f
                            );

                        // Should the rubbish be buried?
                        if (UnityEngine.Random.value < spawner.BuriedChance)
                        {
                            EntityManager.AddComponentData(e, new Buried { Depth = 3 });
                        }

                        var newTranslation = translation.Value + delta;
                        EntityManager.SetComponentData(e, new Translation { Value = newTranslation });
                    }

                    // Delete spawner
                    EntityManager.DestroyEntity(entity);
                }
                );
        }
    }
}