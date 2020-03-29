using Unity.Entities;
using Beach.Messages;
using Beach.Scoring;
using Beach.Rubbish;
using Beach.Sound;
using UnityEngine;
using Beach.Time;

namespace Beach.Station
{
    [UpdateAfter(typeof(DepositBufferSystem))]
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoDepositRubbishSystem : ComponentSystem
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
            var scoreHolders = GetComponentDataFromEntity<ScoreHolder>(false);

            var timeHolder = GetSingleton<TimeHolder>();

            Entities.ForEach(
                (ref Depositing depositing) =>
                {
                    if (scoreHolders.HasComponent(depositing.Depositor)
                    && EntityManager.HasComponent<ScoreValue>(depositing.Depositee)
                    && EntityManager.HasComponent<Rubbish.Rubbish>(depositing.Depositee)
                    && EntityManager.HasComponent<RubbishStation>(depositing.Deposit)
                    && timeHolder.HasTimeLeft()
                    )
                    {
                        var rubbishScore = EntityManager.GetComponentData<ScoreValue>(depositing.Depositee);
                        var rubbish = EntityManager.GetComponentData<Rubbish.Rubbish>(depositing.Depositee);
                        var station = EntityManager.GetComponentData<RubbishStation>(depositing.Deposit);

                        var scoreHolder = scoreHolders[depositing.Depositor];

                        if (station.Material == rubbish.Material || station.Material == RubbishType.All)
                        {
                            scoreHolder.TotalScore += rubbishScore.Score;
                        }
                        else
                        {
                            var halfScore = rubbishScore.Score / 2;
                            scoreHolder.TotalScore -= halfScore;
                        }
                        scoreHolders[depositing.Depositor] = scoreHolder;

                        SoundPlayer.AudioSource.PlayOneShot(SoundPlayer.Pluck);
                    }

                    PostUpdateCommands.DestroyEntity(depositing.Depositee);
                });
        }
    }
}