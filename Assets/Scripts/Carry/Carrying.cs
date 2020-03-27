using System;
using Unity.Entities;

namespace Beach.Carry
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Carrying : IComponentData
    {
        public Entity Entity;
    }
}
