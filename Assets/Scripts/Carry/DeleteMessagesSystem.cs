using Unity.Entities;

namespace Beach.Carry
{
    [UpdateAfter(typeof(DoPickupSystem))]
    [UpdateAfter(typeof(DoPutDownSystem))]
    public class DeleteMessagesSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAny<PickingUp,PuttingDown>()
                .ForEach(
                (Entity e) =>
                {
                    EntityManager.DestroyEntity(e);
                }
                );
        }
    }
}
