using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Agent;

namespace System
{
    public partial struct NpcWalk : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (npc, walker, localTransform) in SystemAPI.Query<
                RefRO<Npc>,
                RefRO<Walker>,
                RefRW<LocalTransform>>())
            {
                localTransform.ValueRW.Position.x += deltaTime * walker.ValueRO.Speed;
            }
        }
    }
}
