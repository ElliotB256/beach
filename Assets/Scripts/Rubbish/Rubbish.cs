using System;
using Unity.Entities;

namespace Beach.Rubbish
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Rubbish : IComponentData
    {
        public Type Material;

        public enum Type
        {
            Plastic,
            Metal,
            Glass,
            Paper,
            Misc
        }
    }
}
