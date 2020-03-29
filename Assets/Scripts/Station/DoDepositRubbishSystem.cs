using Unity.Entities;
using Beach.Messages;
using Beach.Scoring;

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
                    if (EntityManager.HasComponent<ScoreValue>(depositing.Depositee))
                    {
                        var rubbishScore = EntityManager.GetComponentData<ScoreValue>(depositing.Depositee);

                        var scoreHolder = scoreHolders[depositing.Depositor];
                        scoreHolder.TotalScore += rubbishScore.Score;
                        scoreHolders[depositing.Depositor] = scoreHolder;
                    }

                    PostUpdateCommands.DestroyEntity(depositing.Depositee);
                });
        }
    }
}