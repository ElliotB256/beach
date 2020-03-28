using System;
using Unity.Entities;

namespace Beach.Rubbish
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Rubbish : IComponentData
    {
        public RubbishType Material;
    }
}
