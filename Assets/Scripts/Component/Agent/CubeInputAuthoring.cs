using Unity.NetCode;
using Unity.Entities;
using UnityEngine;
using Spawner;
using Unity.Burst;

namespace Agent
{
    public struct CubeInput : IInputComponentData
    {
        public int Horizontal;
        public int Vertical;
    }

    [DisallowMultipleComponent]
    public class CubeInputAuthoring : MonoBehaviour
    {
        class Baker : Baker<CubeInputAuthoring>
        {
            public override void Bake(CubeInputAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<CubeInput>(entity);
            }
        }
    }

    [BurstCompile]
    [UpdateInGroup(typeof(GhostInputSystemGroup))]
    public partial struct SampleCubeInput : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<NetworkStreamInGame>();
            state.RequireForUpdate<CubeSpawner>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var playerInput in SystemAPI.Query<RefRW<CubeInput>>().WithAll<GhostOwnerIsLocal>())
            {
                playerInput.ValueRW = default;
                if (Input.GetKey("left"))
                    playerInput.ValueRW.Horizontal -= 1;
                if (Input.GetKey("right"))
                    playerInput.ValueRW.Horizontal += 1;
                if (Input.GetKey("down"))
                    playerInput.ValueRW.Vertical -= 1;
                if (Input.GetKey("up"))
                    playerInput.ValueRW.Vertical += 1;
            }
        }
    }
}
