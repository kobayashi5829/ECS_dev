using UnityEngine;
using Unity.Entities;

namespace Agent
{
    public struct Walker : IComponentData
    {
        public float Speed;
    }

    public class WalkerAuthoring : MonoBehaviour
    {
        public float _speed = 1;

        class Baker : Baker<WalkerAuthoring>
        {
            public override void Bake(WalkerAuthoring src)
            {
                var component = new Walker() { Speed = src._speed };
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), component);
            }
        }
    }
}