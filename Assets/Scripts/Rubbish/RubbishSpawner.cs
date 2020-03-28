using System;
using Unity.Entities;

namespace Beach.Rubbish
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct RubbishSpawner : IComponentData
    {
        public Entity Template;
        public float Width;
        public float Height;
        public int Number;
        public float BuriedChance;
    }
}
