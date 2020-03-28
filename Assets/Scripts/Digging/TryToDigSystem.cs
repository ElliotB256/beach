using Beach.Focus;
using Beach.Messages;
using Unity.Entities;

namespace Beach.Digging
{
    public class TryToDigSystem : ComponentSystem
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

            Entities
                .WithAll<Digger>()
                .ForEach(
                (Entity e, ref Focussing focussing, ref Digger digger) =>
                {
                    if (!digger.WantsToDig || focussing.Intention != FocusType.Buried || focussing.Entity == Entity.Null)
                        return;

                    var digging = buffer.CreateEntity();
                    buffer.AddComponent(digging,
                        new Digging { Digger = e, Target = focussing.Entity }
                    );
                    buffer.AddComponent(digging, new Message());
                });
        }
    }
}
