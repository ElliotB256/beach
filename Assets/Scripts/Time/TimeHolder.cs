using System;
using Unity.Entities;

namespace Beach.Time
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct TimeHolder: IComponentData
    {
        public float TimeRemaining;
    }
}
