using System;
using Unity.Entities;

namespace Beach.Digging
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Buried : IComponentData
    {
        public float Depth;
    }
}
