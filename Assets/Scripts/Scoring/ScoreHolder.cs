using System;
using Unity.Entities;

namespace Beach.Scoring
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct ScoreHolder : IComponentData
    {
        public int TotalScore;
    }
}
