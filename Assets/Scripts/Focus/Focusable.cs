using System;
using Unity.Entities;

namespace Beach.Focus
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Focusable : IComponentData
    {
        public FocusType Category;
    }
}
