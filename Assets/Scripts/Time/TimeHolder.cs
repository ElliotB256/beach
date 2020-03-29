using System;
using Unity.Entities;

namespace Beach.Time
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct TimeHolder: IComponentData
    {
        public float TimeRemaining;
        public float RoundLength;

        public bool HasTimeLeft() => TimeRemaining > 0f;

        public float Elapsed() => RoundLength - TimeRemaining;
    }
}
