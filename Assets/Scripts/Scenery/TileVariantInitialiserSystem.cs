using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Scenery
{
    public class TileVariantInitialiserSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach(
                (ref TileVariant tileVariant, ref TileVariantInitialiser initialiser) =>
                {
                    float width = 1f / initialiser.TotalPages;
                    if (initialiser.Randomise)
                        initialiser.Page = UnityEngine.Random.Range(0, initialiser.TotalPages);
                    tileVariant.Value = new float4(
                        initialiser.Page * width,
                        width,
                        0f,
                        0f
                        );
                }
            );
            EntityManager.RemoveComponent<TileVariantInitialiser>(Entities.WithAll<TileVariantInitialiser>().ToEntityQuery());
        }
    }
}