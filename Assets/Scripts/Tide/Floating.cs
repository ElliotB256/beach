using System;
using Unity.Entities;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Floating : IComponentData
    {
    }

    [Serializable]
    public struct FloatingStart : IComponentData
    {
    }

    [Serializable]
    public struct FloatingEnd : IComponentData
    {
    }
}
