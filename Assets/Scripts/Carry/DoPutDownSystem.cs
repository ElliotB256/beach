using Unity.Entities;

namespace Beach.Carry
{
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
