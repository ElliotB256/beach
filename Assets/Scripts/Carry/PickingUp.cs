using System;
using Unity.Entities;

namespace Beach.Carry
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct PickingUp : IComponentData
    {
        public Entity Carrier;
        public Entity Carryable;
    }
}
