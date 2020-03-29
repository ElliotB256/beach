using Unity.Entities;
using Beach.Misc;
using UnityEngine;

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

            Entities.ForEach((Entity e, ref PreRoundPhase preRound) =>
            {
                if (!preRound.Running)
                    return;
                preRound.Remaining -= Time.DeltaTime;
                if (preRound.Remaining < 0f)
                    EntityManager.RemoveComponent<PreRoundPhase>(e);
            }
            );

            Entities.WithNone<PreRoundPhase>().ForEach((ref TimeHolder timeHolder) =>
            {
                timeHolder.TimeRemaining -= Time.DeltaTime;
                if (timeHolder.TimeRemaining < 0f)
                    timeHolder.TimeRemaining = 0f;
            }
            );
        }

        public void UpdatePrephase()
        {
        }

        public void UpdateInRound()
        {

        }
    }
}
