using Unity.Entities;
using Beach.Messages;

namespace Beach.Station
{
    [UpdateAfter(typeof(DepositBufferSystem))]
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoDepositRubbishSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach(
                (ref Depositing depositing) =>
                {
                    PostUpdateCommands.DestroyEntity(depositing.Depositee);
                });
        }
    }
}