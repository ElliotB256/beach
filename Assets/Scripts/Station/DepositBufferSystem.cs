using Beach.Carry;
using Beach.Messages;
using Unity.Entities;

namespace Beach.Station
{
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    [UpdateBefore(typeof(DoPutDownSystem))]
    public class DepositBufferSystem : EntityCommandBufferSystem
    {
    }
}