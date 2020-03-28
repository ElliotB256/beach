﻿using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Beach.Carry
{
    [UpdateBefore(typeof(DoPutDownSystem))]
    [UpdateAfter(typeof(TryToPutDownSystem))]
    public class AdjustPositionOnPutDownSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var translations = GetComponentDataFromEntity<Translation>();

            Entities
                .ForEach(
                (ref PuttingDown puttingDown) =>
                {
                    var translation = translations[puttingDown.Carryable];
                    translation.Value -= new float3(0f, UpdateCarriedPositionSystem.CARRY_POSITION_OFFSET_Y, UpdateCarriedPositionSystem.CARRY_POSITION_OFFSET_Z);
                    translations[puttingDown.Carryable] = translation;
                }
                );
        }
    }
}
