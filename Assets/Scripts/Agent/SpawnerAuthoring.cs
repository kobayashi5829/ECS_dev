using UnityEngine;
using Unity.Entities;

namespace Agent
{
    public struct Spawner : IComponentData
    {
        public Entity Prefab;
        public int SpawnNum;
        public uint Seed;
    }

    public class SpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab = null;
        [SerializeField] private int _spawnNum = 10;
        [SerializeField] private uint _seed = 1000;

        class Baker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var data = new Spawner()
                {
                    Prefab = GetEntity(authoring._prefab, TransformUsageFlags.Dynamic),
                    SpawnNum = authoring._spawnNum,
                    Seed = authoring._seed,
                };
                AddComponent(GetEntity(TransformUsageFlags.None), data);
            }
        }
    }
}
