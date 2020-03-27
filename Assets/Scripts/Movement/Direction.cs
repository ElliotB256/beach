using System;
using Unity.Entities;

namespace Beach.Movement
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Direction : IComponentData
    {
        public Facing Value;

        public enum Facing : byte
        {
            Left,
            Top,
            Right,
            Bottom
        }
    }
}
