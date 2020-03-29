using System;
using Unity.Entities;
using UnityEngine;

namespace Beach.Digging
{
    [UpdateAfter(typeof(TryToDigSystem))]
    public class DoDigSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var burieds = GetComponentDataFromEntity<Buried>(false);

            Entities
                .ForEach(
                (ref Digging digging) =>
                {
                    if(!burieds.HasComponent(digging.Target))
                    {
                        return;
                    }

                    var buried = burieds[digging.Target];

                    buried.Depth -= Time.DeltaTime;
                    burieds[digging.Target] = buried;

                    if (buried.Depth <= 0)
                    {
                        EntityManager.RemoveComponent<Buried>(digging.Target);
                    }
                });
        }
    }
}
