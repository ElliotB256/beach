using Unity.Entities;

namespace Beach.Digging
{
    public class DoDigSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach(
                (ref Digging digging) =>
                {
                    var buried = EntityManager.GetComponentData<Buried>(digging.Target);

                    if(buried.Depth-- <= 0)
                    {
                        EntityManager.RemoveComponent<Buried>(digging.Target);
                    }
                });
        }
    }
}
