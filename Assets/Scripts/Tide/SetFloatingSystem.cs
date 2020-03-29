using Beach.Carry;
using Beach.Digging;
using Unity.Entities;
using Unity.Transforms;

namespace Beach.Scenery
{
    /// <summary>
    /// Decides when Entities should be floating.
    /// </summary>
    public class SetFloatingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Tide tide = new Tide();
            Translation tideTranslation = new Translation();
            Entities.ForEach((ref Tide t, ref Translation translation) => { tide = t; tideTranslation = translation; });

            // Make floatable objects start floating.
            Entities.WithAll<Floatable>().WithNone<Buried, Carried>().ForEach(
                (Entity e, ref Translation translation) =>
                {
                    if (translation.Value.y < tideTranslation.Value.y - 1.0f)
                    {
                        EntityManager.AddComponent<Floating>(e);
                        EntityManager.AddComponent<FloatingStart>(e);
                    }
                }
                );

            // Objects stop floating when picked up.
            Entities.WithAll<Floating, Carried>().ForEach((Entity e) =>
            {
                EntityManager.RemoveComponent<Floating>(e);
                EntityManager.AddComponent<FloatingEnd>(e);
            });

            // Put objects down if wave moves away from them.
            Entities.WithAll<Floating>().WithNone<Buried, Carried>().ForEach(
                (Entity e, ref Translation translation) =>
                {
                    if (translation.Value.y > tideTranslation.Value.y - 0.5f)
                    {
                        EntityManager.RemoveComponent<Floating>(e);
                        EntityManager.AddComponent<FloatingEnd>(e);
                    }
                }
            );
        }
    }

    [UpdateAfter(typeof(SetFloatingSystem))]
    public class RemoveStartEndFloatingSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            EntityManager.RemoveComponent<FloatingStart>(Entities.WithAll<FloatingStart>().ToEntityQuery());
            EntityManager.RemoveComponent<FloatingEnd>(Entities.WithAll<FloatingEnd>().ToEntityQuery());
        }
    }
}