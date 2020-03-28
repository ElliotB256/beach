﻿using Beach.Messages;
using Unity.Entities;

namespace Beach.Carry
{
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoPickupSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach(
                (Entity e, ref PickingUp pickingUp) =>
                {
                    EntityManager.AddComponentData(pickingUp.Carrier, new Carrying { Entity = pickingUp.Carryable });
                    EntityManager.AddComponentData(pickingUp.Carryable, new Carried { By = pickingUp.Carrier });
                }
                );
        }
    }
}
