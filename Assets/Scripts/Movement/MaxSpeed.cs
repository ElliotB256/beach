using System;
using Unity.Entities;

namespace Beach.Movement
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct MaxSpeed : IComponentData
    {
        public float Value;
    }
}