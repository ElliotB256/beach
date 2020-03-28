using System;
using Unity.Entities;

namespace Beach.Digging
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Digging : IComponentData
    {
        public Entity Digger;
        public Entity Target;
    }
}
