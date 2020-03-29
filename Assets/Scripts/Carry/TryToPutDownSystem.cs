using Beach.Messages;
using Unity.Entities;

namespace Beach.Carry
{
    [UpdateBefore(typeof(DoPutDownSystem))]
    public class TryToPutDownSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach(
                (Entity e, ref Carrying carrying, ref Carrier carrier) =>
                {
                    if (!carrier.WantsToPickUp)
                    {
                        var message = EntityManager.CreateEntity();
                        EntityManager.AddComponentData(message, new PuttingDown { Carrier = e, Carryable = carrying.Entity, PlayPutDownSound = true });
                        EntityManager.AddComponentData(message, new Message());
                    }
                }
                );
        }
    }
}
