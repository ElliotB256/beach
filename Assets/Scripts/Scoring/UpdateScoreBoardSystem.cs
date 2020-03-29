using Beach.Time;
using Unity.Entities;
using UnityEngine;

namespace Beach.Scoring
{
    public class UpdateScoreBoardSystem : ComponentSystem
    {
        public TextMesh ScoreText;
        public TextMesh TimeText;

        protected override void OnStartRunning()
        {
            var go = GameObject.FindGameObjectWithTag("ScoreText");
            if (go == null)
                Debug.LogError("Cannot find ScoreText gameobject.");
            else
                ScoreText = go.GetComponent<TextMesh>();

            go = GameObject.Find("TimeText");
            if (go == null)
                Debug.LogError("Cannot find TimeText gameobject.");
            else
                TimeText = go.GetComponent<TextMesh>();
        }

        protected override void OnUpdate()
        {
            var scoreHolder = GetSingleton<ScoreHolder>();
            var timeHolder = GetSingleton<TimeHolder>();

            if (ScoreText != null)
                ScoreText.text = string.Format("Score: {0,-5:F0}", scoreHolder.TotalScore);
            
            if (TimeText != null)
                TimeText.text = string.Format("Time: {0,-6:F2}", timeHolder.TimeRemaining);
        }
    }
}
