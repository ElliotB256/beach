using System;
using Unity.Entities;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct CrabSpawner : IComponentData
    {
        public Entity Template;
        public float BaseRate;
        public float RateIncreasePerMinute;
        public float StartTime;
    }
}
