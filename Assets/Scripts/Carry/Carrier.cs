using System;
using Unity.Entities;

namespace Beach.Carry
{
    /// <summary>
    /// An entity which can carry *something*.
    /// </summary>
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Carrier : IComponentData
    {
        /// <summary>
        /// Distance at which this carrier can things up.
        /// </summary>
        public float PickupRange;

        /// <summary>
        /// Does this entity want to pick something up?
        /// </summary>
        public bool WantsToPickUp;
    }
}
