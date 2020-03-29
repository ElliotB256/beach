using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Beach.Carry;
using Unity.Collections;
using Beach.Messages;

namespace Beach.Station
{
    [UpdateBefore(typeof(DepositBufferSystem))]
    public class TryDepositRubbishSystem : ComponentSystem
    {
        EntityQuery StationQuery;
        DepositBufferSystem BufferSystem;

        protected override void OnCreate()
        {
            StationQuery = Entities.WithAll<RubbishStation, Translation>().ToEntityQuery();
            BufferSystem = World.GetOrCreateSystem<DepositBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var translations = GetComponentDataFromEntity<Translation>(true);

            // Store Rubbish stations in arrays.
            var stationNumber = StationQuery.CalculateEntityCount();
            var stations = StationQuery.ToEntityArray(Allocator.TempJob);
            var stationPositions = StationQuery.ToComponentDataArray<Translation>(Allocator.TempJob);
            var rubbishStations = StationQuery.ToComponentDataArray<RubbishStation>(Allocator.TempJob);

            var buffer = BufferSystem.CreateCommandBuffer();

            Entities.ForEach(
                (Entity puttingDownEntity, ref PuttingDown puttingDown) =>
                {
                    var isDepositable = EntityManager.HasComponent<Depositable>(puttingDown.Carryable);

                    if (!isDepositable)
                        return;

                    var carrierPosition = translations[puttingDown.Carrier];

                    // Loop through rubbish stations
                    var score = float.MaxValue;
                    Entity bestMatch = Entity.Null;
                    for (var i = 0; i < stationNumber; i++)
                    {
                        var delta = carrierPosition.Value.xy - stationPositions[i].Value.xy;
                        var distance = math.length(delta);

                        if (distance > RubbishStation.DEPOSIT_RANGE)
                            continue;

                        if (distance > score)
                            continue;

                        score = distance;
                        bestMatch = stations[i];
                    }

                    if (bestMatch != Entity.Null)
                    {
                        var e = buffer.CreateEntity();
                        buffer.AddComponent(e,
                            new Depositing
                            {
                                Deposit = bestMatch,
                                Depositee = puttingDown.Carryable,
                                Depositor = puttingDown.Carrier
                            });
                        buffer.AddComponent(e, new Message());
                    }
                }
                );

            stations.Dispose();
            stationPositions.Dispose();
            rubbishStations.Dispose();
        }
    }
}