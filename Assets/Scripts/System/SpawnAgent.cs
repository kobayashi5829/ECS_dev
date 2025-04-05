using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using Agent;

namespace System
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct SpawnAgent : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Spawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var spawner = SystemAPI.GetSingleton<Spawner>();
            var instances = state.EntityManager.Instantiate(spawner.Prefab, spawner.SpawnNum, Allocator.Temp);
            var rand = new Unity.Mathematics.Random(spawner.Seed);
            foreach (var entity in instances)
            {
                var transform = SystemAPI.GetComponentRW<LocalTransform>(entity);

                float x = rand.NextFloat(-10, 10);
                float z = rand.NextFloat(-10, 10);
                transform.ValueRW.Position = new float3(x, 1, z);
            }
            state.Enabled = false;
        }
    }
}
