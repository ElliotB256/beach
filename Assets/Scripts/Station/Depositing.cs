using System;
using Unity.Entities;

namespace Beach.Station
{
    [GenerateAuthoringComponent]
    [Serializable]
    public struct Depositing : IComponentData
    {
        public Entity Depositor;
        public Entity Depositee;
        public Entity Deposit;
    }
}
