using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Carry
{
    public class UpdateCarriedPositionSystem : ComponentSystem
    {
        public const float CARRY_POSITION_OFFSET_Y = -0.1f;
        public const float CARRY_POSITION_OFFSET_Z = -1.5f;

        protected override void OnUpdate()
        {
            var translations = GetComponentDataFromEntity<Translation>(true);

            Entities
                .ForEach(
                (ref Carried carried, ref Translation translation) =>
                {
                    translation.Value = translations[carried.By].Value + new float3(0f, CARRY_POSITION_OFFSET_Y, CARRY_POSITION_OFFSET_Z);
                }
                );
        }
    }
}
