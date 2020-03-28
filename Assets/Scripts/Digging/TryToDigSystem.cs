using Beach.Messages;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Beach.Digging
{
    public class TryToDigSystem : ComponentSystem
    {
        EntityQuery DiggerQuery;

        protected override void OnCreate()
        {
            DiggerQuery = Entities.WithAll<Digger, Translation>().ToEntityQuery();
        }

        protected override void OnUpdate()
        {
            var translations = GetComponentDataFromEntity<Translation>(true);

            var diggerNumber = DiggerQuery.CalculateEntityCount();
            var diggers = DiggerQuery.ToEntityArray(Allocator.TempJob);
            var diggerData = DiggerQuery.ToComponentDataArray<Digger>(Allocator.TempJob);

            var runLoop = false;

            for (var i = 0; i < diggerNumber; i++)
            {
                if (diggerData[i].WantsToDig)
                {
                    runLoop = true;
                    break;
                }
            }

            if (runLoop)
            {
                Entities.ForEach(
                    (Entity entity, ref Buried buried) =>
                    {

                    });
            }

            diggers.Dispose();
            diggerData.Dispose();
        }
    }
}
