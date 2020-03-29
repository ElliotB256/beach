using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Beach.Scenery
{
    [RequiresEntityConversion]
    public class TideRubbishElementProxy : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        public List<GameObject> Archetypes;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var buffer = dstManager.AddBuffer<TideRubbishElement>(entity);
            foreach (var archetype in Archetypes)
            {
                var prefab = conversionSystem.GetPrimaryEntity(archetype);
                buffer.Add(new TideRubbishElement { Template = prefab });
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            foreach (var archetype in Archetypes)
            {
                referencedPrefabs.Add(archetype);
            }
        }
    }
}
