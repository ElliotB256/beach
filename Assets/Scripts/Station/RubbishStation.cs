using System;
using Unity.Entities;
using Beach.Rubbish;

namespace Beach.Station
{
    /// <summary>
    /// A place where rubbish can be dropped off.
    /// </summary>
    [GenerateAuthoringComponent]
    [Serializable]
    public struct RubbishStation : IComponentData
    {
        RubbishType Material;

        public const float DEPOSIT_RANGE = 1f;
    }
}
