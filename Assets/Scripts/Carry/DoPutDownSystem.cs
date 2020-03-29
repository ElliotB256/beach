using Beach.Messages;
using Beach.Sound;
using Unity.Entities;
using UnityEngine;

namespace Beach.Carry
{
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoPutDownSystem : ComponentSystem
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
                (ref PuttingDown puttingDown) =>
                {
                    EntityManager.RemoveComponent<Carrying>(puttingDown.Carrier);
                    EntityManager.RemoveComponent<Carried>(puttingDown.Carryable);

                    if (puttingDown.PlayPutDownSound)
                    {
                        SoundPlayer.AudioSource.PlayOneShot(SoundPlayer.Down);
                    }
                }
                );
        }
    }
}
