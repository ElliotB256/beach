using System;
using Unity.Entities;

namespace Beach.Scenery
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct SceneGenerator : IComponentData
    {
        public Entity SandTile;
        public int Width;
        public int Height;
    }
}
