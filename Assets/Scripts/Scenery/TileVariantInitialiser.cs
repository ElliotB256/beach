using System;
using Unity.Entities;

namespace Beach.Scenery
{
    /// <summary>
    /// Initialises a TileVariant component with the correct values for the shader.
    /// </summary>
    [GenerateAuthoringComponent]
    [Serializable]
    public struct TileVariantInitialiser : IComponentData
    {
        public int TotalPages;
        public int Page;
        public bool Randomise;
    }
}
