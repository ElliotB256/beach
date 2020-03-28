using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Scenery
{
    public class GrassSpawnSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach(
                (Entity entity, ref GrassSpawner spawner, ref Translation translation) =>
                {
                    for (var i = 0; i < spawner.Number; i++)
                    {
                        // Create a new entity based on the grass template.
                        var e = EntityManager.Instantiate(spawner.Template);

                        // Place at random position
                        var delta = UnityEngine.Random.insideUnitSphere * spawner.Radius;
                        var newTranslation = translation.Value;
                        newTranslation.xy += new float2(delta.x, delta.y);
                        EntityManager.SetComponentData(e, new Translation { Value = newTranslation });

                        // Choose random grass variants
                        int grassVariant = UnityEngine.Random.Range(0, 4);
                        var tileVariant = new TileVariant { Value = new float4(grassVariant * 0.25f, 0.25f, 0f, 0f) };
                        EntityManager.SetComponentData(e, tileVariant);
                    }

                    // Delete spawner
                    EntityManager.DestroyEntity(entity);
                }
                );
        }
    }
}