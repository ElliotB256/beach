using Beach.Messages;
using Beach.Sound;
using Unity.Entities;
using UnityEngine;

namespace Beach.Carry
{
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoPickupSystem : ComponentSystem
    {
        public SoundPlayer SoundPlayer;

        protected override void OnStartRunning()
        {
            var go = GameObject.FindGameObjectWithTag("SoundPlayer");
            if (go != null)
            {
                SoundPlayer = go.GetComponent<SoundPlayer>();
            }
        }

        protected override void OnUpdate()
        {
            Entities
                .ForEach(
                (ref PickingUp pickingUp) =>
                {
                    EntityManager.AddComponentData(pickingUp.Carrier, new Carrying { Entity = pickingUp.Carryable });
                    EntityManager.AddComponentData(pickingUp.Carryable, new Carried { By = pickingUp.Carrier });

                    SoundPlayer.AudioSource.PlayOneShot(SoundPlayer.Up);
                }
                );
        }
    }
}
