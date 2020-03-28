using System;
using Unity.Entities;

namespace Beach.Digging
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Digger : IComponentData
    {
        public bool WantsToDig;
    }
}
