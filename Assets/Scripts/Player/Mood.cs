using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace Beach.Player
{
    [GenerateAuthoringComponent]
    [Serializable]
    [MaterialProperty("Mood", MaterialPropertyFormat.Float)]
    public struct Mood : IComponentData
    {
        public float Value;
    }
}
