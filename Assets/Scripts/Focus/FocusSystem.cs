using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Transforms;
using Beach.Focus;

namespace Beach.Carry
{
    [UpdateInGroup(typeof(FocusSystemGroup))]
    public class FocusSystem : JobComponentSystem
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
                    ComponentType.ReadOnly<Focusable>()
                }
            });

            CarrierQuery = GetEntityQuery(new EntityQueryDesc
            {
                All = new[] {
                    ComponentType.ReadOnly<Translation>(),
                    ComponentType.ReadWrite<Focussing>()
                }
            });

            CommandBuffer = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            // Candidate variables
            int numberOfCandidates = CandidateQuery.CalculateEntityCount();
            var positions = CandidateQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
            var focusables = CandidateQuery.ToComponentDataArray<Focusable>(Allocator.TempJob);
            var candidates = CandidateQuery.ToEntityArray(Allocator.TempJob);

            // Carrier variables
            int numberOfCarriers = CarrierQuery.CalculateEntityCount();

            var buffer = CommandBuffer.CreateCommandBuffer().ToConcurrent();

            var findCandidateJobHandle = Entities
                .ForEach(
                (Entity carrierE, int entityInQueryIndex, ref Focussing focussing, in Translation translation) =>
                {
                    var choice = Entity.Null;

                    if (focussing.Intention == FocusType.None)
                        return;

                    float currentScore = float.PositiveInfinity;
                    for (var candidate = 0; candidate < numberOfCandidates; candidate++)
                    {
                        // See if we *could* pick this up.
                        // Calculate a score.
                        // See if this is preferable to our current choice.
                        // If it is, update the choice.

                        float3 delta = (translation.Value - positions[candidate].Value);
                        float distance = math.length(delta);
                        if (distance > focussing.Range)
                            continue;

                        float score = distance;
                        if (score < currentScore)
                        {
                            choice = candidates[candidate];
                            currentScore = score;
                        }
                    }

                    // If we found a choice, let's flag it.
                    focussing.Entity = choice;
                }
                ).Schedule(inputDeps);

            CommandBuffer.AddJobHandleForProducer(findCandidateJobHandle);

            return JobHandle.CombineDependencies(
                positions.Dispose(findCandidateJobHandle),
                candidates.Dispose(findCandidateJobHandle),
                focusables.Dispose(findCandidateJobHandle)
                );
        }
    }
}