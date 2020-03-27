using System;
using Unity.Entities;

namespace Beach.Carry
{
    /// <summary>
    /// A thing which can be carried.
    /// </summary>
    [Serializable]
    [GenerateAuthoringComponent]
    public struct Carryable : IComponentData
    {
    }
}
