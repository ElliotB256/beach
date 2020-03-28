using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Carry
{
    /// <summary>
    /// Finds PickingUp 
    /// </summary>
    public class UpdateCarriedPositionSystem : ComponentSystem
    {
        public const float CARRY_POSITION_OFFSET_Y = 0.5f;
        public const float CARRY_POSITION_OFFSET_Z = -0.1f;

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
