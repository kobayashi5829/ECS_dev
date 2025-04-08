using Unity.Entities;
using UnityEngine;
using Unity.NetCode;
using Unity.Burst;
using Agent;
using Unity.Transforms;
using Unity.Mathematics;

namespace System
{
    [UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
    [BurstCompile]
    public partial struct CubeMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var speed = SystemAPI.Time.DeltaTime * 4;
            foreach (var (input, transform) in SystemAPI.Query<RefRO<CubeInput>, RefRW<LocalTransform>>().WithAll<Simulate>())
            {
                var moveInput = new float2(input.ValueRO.Horizontal, input.ValueRO.Vertical);
                moveInput = math.normalizesafe(moveInput) * speed;
                transform.ValueRW.Position += new float3(moveInput.x, 0, moveInput.y);
            }
        }
    }
}
