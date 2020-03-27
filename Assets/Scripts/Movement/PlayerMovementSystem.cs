using Beach.Movement;
using Beach.Player;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities
            .WithAll<Controlled>()
            .ForEach(
            (ref Translation translation, ref MaxSpeed maxSpeed) =>
            {
                float3 direction = new float3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
                if (math.length(direction) > 1.0e-3f)
                    direction = math.normalize(direction);
                direction = maxSpeed.Value * direction;
                translation.Value += direction * Time.DeltaTime;
            }
            );
    }
}