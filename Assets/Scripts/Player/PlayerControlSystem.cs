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

            // If the player is holding the spade, they are 'digging'.
            Entities.WithAll<Controlled>().ForEach((ref Digger digger) => digger.WantsToDig = false);
            Entities
                .WithAll<Controlled>()
                .ForEach(
                (ref Carrying carrying, ref Digger digger) =>
                {
                    digger.WantsToDig = EntityManager.HasComponent<Spade>(carrying.Entity);                        
                });
        }
    }
}
