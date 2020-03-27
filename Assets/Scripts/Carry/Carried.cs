using System;
using Unity.Entities;

namespace Beach.Carry
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Carried : IComponentData
    {
        public Entity By;
    }
}
