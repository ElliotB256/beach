using System;
using Unity.Entities;

namespace Beach.Scoring
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct ScoreValue : IComponentData
    {
        public int Score;
    }
}
