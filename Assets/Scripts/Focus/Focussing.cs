using System;
using Unity.Entities;

namespace Beach.Focus
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Focussing : IComponentData
    {
        public Entity Entity;
        public FocusType Intention;
        public float Range;
    }
}
