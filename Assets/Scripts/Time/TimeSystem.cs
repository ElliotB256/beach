using Unity.Entities;
using Beach.Misc;

namespace Beach.Time
{
    public class TimeSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<Initialising>().ForEach(
                (Entity e, ref TimeHolder timeHolder) =>
            {
                timeHolder.TimeRemaining = timeHolder.RoundLength;
                EntityManager.RemoveComponent<Initialising>(e);
            });

            Entities.ForEach((ref TimeHolder timeHolder) =>
            {
                timeHolder.TimeRemaining -= Time.DeltaTime;
                if (timeHolder.TimeRemaining < 0f)
                    timeHolder.TimeRemaining = 0f;
            });
        }
    }
}
