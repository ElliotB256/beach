using Unity.Entities;
using Beach.Messages;
using Beach.Scoring;
using Beach.Rubbish;

namespace Beach.Station
{
    [UpdateAfter(typeof(DepositBufferSystem))]
    [UpdateBefore(typeof(DeleteMessagesSystem))]
    public class DoDepositRubbishSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var scoreHolders = GetComponentDataFromEntity<ScoreHolder>(false);

            Entities.ForEach(
                (ref Depositing depositing) =>
                {
                    if (scoreHolders.HasComponent(depositing.Depositor)
                    && EntityManager.HasComponent<ScoreValue>(depositing.Depositee)
                    && EntityManager.HasComponent<Rubbish.Rubbish>(depositing.Depositee)
                    && EntityManager.HasComponent<RubbishStation>(depositing.Deposit))
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
                    }

                    PostUpdateCommands.DestroyEntity(depositing.Depositee);
                });
        }
    }
}