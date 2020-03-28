using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Beach.Movement
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Moveable : IComponentData
    {
        public float2 Force;
    }
}