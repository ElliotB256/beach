using Beach.Carry;
using Beach.Digging;
using Unity.Entities;

namespace Beach.Scenery
{
    /// <summary>
    /// Tide has a small chance to bury floating items as it goes out.
    /// </summary>
    public class TideBuriesRubbishSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            //Eurgh I hate this, but we are approaching the deadline...Carried here it is...
            Entities.WithNone<Carried>().WithAll<FloatingEnd>().ForEach((Entity e) =>
            {
                if (UnityEngine.Random.value < 0.15f)
                    EntityManager.AddComponentData(e, new Buried { Depth = UnityEngine.Random.Range(0.5f, 1f) });
            });
        }
    }
}