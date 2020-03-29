using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Beach.Movement
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct CircularCollider : IComponentData
    {
        public float Radius;
        public float3 Offset;
    }
}
