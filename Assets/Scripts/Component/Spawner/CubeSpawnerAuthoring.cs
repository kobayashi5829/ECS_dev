using Unity.Entities;
using UnityEngine;

namespace Spawner
{
    public struct CubeSpawner : IComponentData
    {
        public Entity Cube;
    }

    [DisallowMultipleComponent]
    public class CubeSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _cube;

        class Baker : Baker<CubeSpawnerAuthoring>
        {
            public override void Bake(CubeSpawnerAuthoring authoring)
            {
                CubeSpawner component = default(CubeSpawner);
                component.Cube = GetEntity(authoring._cube, TransformUsageFlags.Dynamic);
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, component);
            }
        }
    }
}
