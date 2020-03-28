using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Transforms;

namespace Beach.Carry
{
    /// <summary>
    /// Attempts to find Carryable entities for Carriers to pick up.
    /// </summary>
    public class TryToPickupSystem : JobComponentSystem
    {
        EntityQuery CandidateQuery;
        EntityQuery CarrierQuery;
        EntityCommandBufferSystem CommandBuffer;

        protected override void OnCreate()
        {
            CandidateQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] {
                    ComponentType.ReadOnly<Translation>(),
                    ComponentType.ReadOnly<Carryable>()
                },
                None = new [] { ComponentType.ReadOnly<Carried>() }
            });

            CarrierQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] {
                    ComponentType.ReadOnly<Translation>(),
                    ComponentType.ReadOnly<Carrier>()
                },
                None = new [] { ComponentType.ReadOnly<Carrying>() }
            });

            CommandBuffer = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            // Candidate variables
            int numberOfCandidates = CandidateQuery.CalculateEntityCount();
            var candidatePositions = CandidateQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
            var candidates = CandidateQuery.ToEntityArray(Allocator.TempJob);

            // Carrier variables
            int numberOfCarriers = CarrierQuery.CalculateEntityCount();
            var choice = new NativeArray<Entity>(numberOfCarriers, Allocator.TempJob, NativeArrayOptions.ClearMemory);

            var buffer = CommandBuffer.CreateCommandBuffer().ToConcurrent();

            var findCandidateJobHandle = Entities
                .WithNone<Carrying>()
                .ForEach(
                (Entity carrierE, int entityInQueryIndex, in Translation translation, in Carrier carrier) =>
                {
                    if (!carrier.WantsToPickUp)
                        return;

                    float currentScore = float.PositiveInfinity;

                    for (var candidate = 0; candidate < numberOfCandidates; candidate++)
                    {
                        // See if we *could* pick this up.
                        // Calculate a score.
                        // See if this is preferable to our current choice.
                        // If it is, update the choice.

                        float3 delta = (translation.Value - candidatePositions[candidate].Value);
                        float distance = math.length(delta);
                        if (distance > carrier.PickupRange)
                            continue;

                        float score = distance;
                        if (score < currentScore)
                        {
                            choice[entityInQueryIndex] = candidates[candidate];
                            currentScore = score;
                        }
                    }

                    // If we found a choice, let's flag it.
                    if (choice[entityInQueryIndex] != Entity.Null)
                    {
                        var pickup = buffer.CreateEntity(entityInQueryIndex);
                        buffer.AddComponent(entityInQueryIndex, pickup,
                            new PickingUp { Carrier = carrierE, Carryable = choice[entityInQueryIndex] }
                            );
                    }
                }
                ).Schedule(inputDeps);

            CommandBuffer.AddJobHandleForProducer(findCandidateJobHandle);

            return JobHandle.CombineDependencies(
                candidatePositions.Dispose(findCandidateJobHandle),
                candidates.Dispose(findCandidateJobHandle),
                choice.Dispose(findCandidateJobHandle)
                );
        }
    }
}