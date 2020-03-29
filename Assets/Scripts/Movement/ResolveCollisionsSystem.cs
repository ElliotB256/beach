using Beach.Carry;
using Beach.Digging;
using Beach.Movement;
using Beach.Player;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

/// <summary>
/// Adjusts the positions of Movable entities that are overlapping.
/// </summary>
public class ResolveCollisionsSystem : ComponentSystem
{
    EntityQuery ColliderQuery;
    EntityQuery MoveableQuery;

    protected override void OnCreate()
    {
        ColliderQuery = Entities.WithNone<Carried,Buried>().WithAll<CircularCollider, Translation>().ToEntityQuery();
        MoveableQuery = Entities.WithNone<Carried,Buried>().WithAll<Moveable, Translation, CircularCollider>().ToEntityQuery();
    }

    protected override void OnUpdate()
    {
        var colliders = ColliderQuery.ToComponentDataArray<CircularCollider>(Allocator.TempJob);
        var colliderPositions = ColliderQuery.ToComponentDataArray<Translation>(Allocator.TempJob);

        Entities.With(MoveableQuery).ForEach((ref Moveable moveable) => moveable.Force = new float2());

        Entities
            .ForEach(
            (ref Moveable movable, ref Translation translation, ref CircularCollider collider) =>
            {
                // Loop through colliders. Check for overlap with each.
                for (var i = 0; i < colliders.Length; i++)
                {
                    var delta = (colliderPositions[i].Value+colliders[i].Offset - translation.Value).xy;
                    var distance = math.length(delta);

                    if (distance < 1e-3) // this is to prevent zero-length vectors from giving nan when we take direction.
                        continue;

                    float overlap = collider.Radius + colliders[i].Radius - distance;
                    overlap = math.max(0f, overlap);
                    movable.Force -= overlap * math.normalize(delta);
                }
            }
            );

        // Adjust entity positions to remove overlap.
        Entities.With(MoveableQuery).ForEach(
            (ref Translation translation, ref Moveable moveable) =>
            {
                translation.Value.xy += moveable.Force;
            }
            );

        colliders.Dispose();
        colliderPositions.Dispose();
    }
}