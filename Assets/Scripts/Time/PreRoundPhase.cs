using System;
using Unity.Entities;

namespace Beach.Time
{
    /// <summary>
    /// Defines a phase at the start of the game before we can do anything.
    /// </summary>
    [GenerateAuthoringComponent]
    [Serializable]
    public struct PreRoundPhase: IComponentData
    {
        public float Remaining;
        public bool Running;
    }
}
