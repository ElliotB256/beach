using System;
using Unity.Entities;

namespace Beach.Scenery
{
    //[GenerateAuthoringComponent]
    [Serializable]
    public struct TideRubbishElement : IBufferElementData
    {
        public Entity Template;
    }
}
