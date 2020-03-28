using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace Beach.Scenery
{
    /// <summary>
    /// Stores the information used to choose the tile variant from the sprite map.
    /// 
    /// Index 0: UOffset
    /// Index 1: Width
    /// </summary>
    [GenerateAuthoringComponent]
    [Serializable]
    [MaterialProperty("_tileVariant", MaterialPropertyFormat.Float4)]
    public struct TileVariant : IComponentData
    {
        public float4 Value;
    }
}
