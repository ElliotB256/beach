using Beach.Time;
using Unity.Entities;
using UnityEngine;

namespace Beach.Scoring
{
    public class UpdateScoreBoardSystem : ComponentSystem
    {
        public TextMesh ScoreText;
        public TextMesh TimeText;
        public TextMesh PreroundCountdown;

        protected override void OnStartRunning()
        {
            var go = GameObject.FindGameObjectWithTag("ScoreText");
            if (go == null)
                Debug.LogError("Cannot find ScoreText gameobject.");
            else
                ScoreText = go.GetComponent<TextMesh>();

            go = GameObject.FindGameObjectWithTag("TimeText");
            if (go == null)
                Debug.LogError("Cannot find TimeText gameobject.");
            else
                TimeText = go.GetComponent<TextMesh>();

            go = GameObject.FindGameObjectWithTag("PreroundCountdown");
            if (go == null)
                Debug.LogError("Cannot find PreroundCountdown gameobject.");
            else
                PreroundCountdown = go.GetComponent<TextMesh>();
        }

        protected override void OnUpdate()
        {
            var scoreHolder = GetSingleton<ScoreHolder>();
            var timeHolder = GetSingleton<TimeHolder>();

            if (ScoreText != null)
                ScoreText.text = string.Format("Score: {0,-5:F0}", scoreHolder.TotalScore);
            
            if (TimeText != null)
                TimeText.text = string.Format("Time: {0,-6:F2}", timeHolder.TimeRemaining);

            if (PreroundCountdown != null)
            {
                if (Entities.WithAll<PreRoundPhase>().ToEntityQuery().CalculateEntityCount() > 0)
                {
                    var preRound = GetSingleton<PreRoundPhase>();
                    if (preRound.Running)
                        PreroundCountdown.text = string.Format("{0}", (int)(preRound.Remaining+1));
                }
                else
                    PreroundCountdown.text = "";
            }
        }
    }
}
