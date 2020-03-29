using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Crab : IComponentData
    {
        public float2 Velocity;
    }
}
