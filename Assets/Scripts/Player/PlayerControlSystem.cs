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
                    carrier.WantsToPickUp = Input.GetKey(KeyCode.Space);
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
