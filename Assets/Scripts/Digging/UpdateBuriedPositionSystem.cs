using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Digging
{
    public class UpdateBuriedPositionSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var translations = GetComponentDataFromEntity<Translation>(true);

            Entities
                .ForEach(
                (ref Buried buried, ref Translation translation) =>
                {
                    translation.Value.z = buried.Depth;
                }
                );
        }
    }
}
