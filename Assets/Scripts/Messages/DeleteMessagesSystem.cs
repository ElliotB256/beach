using Unity.Entities;

namespace Beach.Messages
{
    public class DeleteMessagesSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAny<Message>()
                .ForEach(
                (Entity e) =>
                {
                    EntityManager.DestroyEntity(e);
                }
                );
        }
    }
}
