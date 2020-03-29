using Beach.Digging;
using Beach.Carry;
using Unity.Entities;
using UnityEngine;

namespace Beach.Player
{
    public class PlayerControlSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAll<Controlled>()
                .ForEach(
                (ref Carrier carrier) =>
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                        carrier.WantsToPickUp = !carrier.WantsToPickUp;
                });

            Entities
                .WithAll<Controlled>()
                .ForEach(
                (ref Digger digger) =>
                {
                    digger.WantsToDig = Input.GetKey(KeyCode.F);
                });
        }
    }
}
