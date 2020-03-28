using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Scenery
{
    public class GenerateSceneSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach(
                (Entity generatorEntity, ref SceneGenerator generator) =>
            {
                for (var x=-generator.Width/2; x<generator.Width/2; x++)
                {
                    for (var y=-generator.Height/2; y<generator.Height/2; y++)
                    {
                        var e = EntityManager.Instantiate(generator.SandTile);
                        EntityManager.SetComponentData(e, new Translation
                        {
                            Value = new float3(x * 1f, 1f * y, 0f)
                        });
                        int sandVariant = UnityEngine.Random.Range(0, 8);
                        int pebbleVariant = UnityEngine.Random.Range(0, 8);
                        var tileVariant = new TileVariant { Value = new float4(sandVariant * 0.125f, 0.125f, pebbleVariant * 0.125f, 0.125f) };
                        EntityManager.AddComponentData(e, tileVariant);
                    }
                }

                EntityManager.DestroyEntity(generatorEntity);
            }
            );
        }
    }
}