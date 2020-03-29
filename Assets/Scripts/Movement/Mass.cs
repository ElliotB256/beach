using System;
using Unity.Entities;

namespace Beach.Movement
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Mass : IComponentData
    {
        public float Value;
    }
}
