using System;
using Unity.Entities;

namespace Beach.Carry
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct PuttingDown : IComponentData
    {
        public Entity Carrier;
        public Entity Carryable;
    }
}
