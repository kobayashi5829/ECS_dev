using UnityEngine;
using Unity.Entities;

namespace Agent
{
    public struct Npc : IComponentData { }

    public class NpcAuthoring : MonoBehaviour
    {
        class Baker : Baker<NpcAuthoring>
        {
            public override void Bake(NpcAuthoring src)
            {
                var component = new Npc() { };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), component);
            }
        }
    }
}
