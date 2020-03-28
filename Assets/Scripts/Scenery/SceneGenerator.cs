using System;
using Unity.Entities;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct SceneGenerator : IComponentData
    {
        public int Width;
        public int Height;
    }
}
