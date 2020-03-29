using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Tide : IComponentData
    {
        public float TimePeriod;

        /// <summary>
        /// Phase of the wave, between 0 and 1.
        /// </summary>
        public float Phase;

        public float TranslationAmplitude;

        public float3 Origin;

        public DynamicBuffer<Entity> RubbishTemplates;

        public float SpawnRate;

        /// <summary>
        /// Speed with which the tide moves. Positive for tide coming onto the beach.
        /// </summary>
        public float Speed;

        public bool IsComingIn()
        {
            return Phase < 0.5f;
        }
    }
}
