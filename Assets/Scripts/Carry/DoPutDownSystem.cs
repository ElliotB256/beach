using Beach.Messages;
using Unity.Entities;

namespace Beach.Carry
{
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoPutDownSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach(
                (ref PuttingDown puttingDown) =>
                {
                    EntityManager.RemoveComponent<Carrying>(puttingDown.Carrier);
                    EntityManager.RemoveComponent<Carried>(puttingDown.Carryable);
                }
                );
        }
    }
}
