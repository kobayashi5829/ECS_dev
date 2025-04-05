using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;
using Agent;

namespace System
{
    public partial struct NpcWalk : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new UpdateJob() { DeltaTime = SystemAPI.Time.DeltaTime };
            job.ScheduleParallel();
        }

        public partial struct UpdateJob : IJobEntity
        {
            public float DeltaTime;

            public void Execute(in Npc npc, in Walker walker, ref LocalTransform transform)
            {
                var rotate = quaternion.RotateY(DeltaTime);
                var forward = math.mul(rotate, transform.Forward());
                transform.Position += forward * walker.Speed * DeltaTime;
                transform.Rotation = quaternion.LookRotation(forward, transform.Up());
            }
        }
    }
}
