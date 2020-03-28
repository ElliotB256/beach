using Unity.Entities;
using Beach.Messages;
using Beach.Focus;

namespace Beach.Carry
{
    public class TryToPickupSystem : ComponentSystem
    {
        EntityCommandBufferSystem CommandBuffer;

        protected override void OnCreate()
        {
            CommandBuffer = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override void OnUpdate()
        {
            var buffer = CommandBuffer.CreateCommandBuffer();

            Entities.WithAll<Carrier>().ForEach(
                (Entity e, ref Focussing focussing, ref Carrier carrier) =>
                {
                    if (focussing.Entity == Entity.Null)
                        return;
                    if (!carrier.WantsToPickUp || focussing.Intention != FocusType.Carryable)
                        return;
                    var pickup = buffer.CreateEntity();
                    buffer.AddComponent(pickup,
                        new PickingUp { Carrier = e, Carryable = focussing.Entity }
                        );
                    buffer.AddComponent(pickup, new Message());
                }
                );
        }
    }
}