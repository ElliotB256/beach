using System;
using Unity.Entities;

namespace Beach.Movement
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct CircularCollider : IComponentData
    {
        public float Radius;
    }
}
