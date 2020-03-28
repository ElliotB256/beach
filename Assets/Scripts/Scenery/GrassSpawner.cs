using System;
using Unity.Entities;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct GrassSpawner : IComponentData
    {
        public Entity Template;
        public float Radius;
        public int Number;
    }
}
