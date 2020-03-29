using Beach.Digging;
using Beach.Carry;
using Unity.Entities;
using UnityEngine;

namespace Beach.Time
{
    public class TimeSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref TimeHolder timeHolder) =>
            {
                timeHolder.TimeRemaining -= Time.DeltaTime;
            });
        }
    }
}
