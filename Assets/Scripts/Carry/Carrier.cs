using System;
using Unity.Entities;

namespace Beach.Carry
{
    /// <summary>
    /// An entity which can carry *something*.
    /// </summary>
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Carrier : IComponentData
    {
    }
}
