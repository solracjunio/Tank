using Code.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

namespace Code.Systems
{
    [BurstCompile]
    partial struct TurretShootingSystem : ISystem
    {
        ComponentLookup<WorldTransform> _mWorldTransformLookup;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _mWorldTransformLookup = state.GetComponentLookup<WorldTransform>(true);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            _mWorldTransformLookup.Update(ref state);

            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            var turretShootJob = new TurretShoot
            {
                WorldTransformLookup = _mWorldTransformLookup, 
                ECB = ecb
            };

            turretShootJob.Schedule();
        }
    }
}

[BurstCompile]
partial struct TurretShoot : IJobEntity
{
    [ReadOnly] 
    public ComponentLookup<WorldTransform> WorldTransformLookup;
    public EntityCommandBuffer ECB;

    void Execute(in TurretAspect turret)
    {
        var instance = ECB.Instantiate(turret.CannonBallPrefab);
        var spawnLocalToWorld = WorldTransformLookup[turret.CannonBallSpawn];
        var cannonBallTransform = LocalTransform.FromPosition(spawnLocalToWorld.Position);

        cannonBallTransform.Scale = WorldTransformLookup[turret.CannonBallPrefab].Scale;
        ECB.SetComponent(instance, cannonBallTransform);
        ECB.SetComponent(instance, new CannonBall
        {
            Speed = spawnLocalToWorld.Forward() * 20.0f
        });
        
        ECB.SetComponent(instance, new URPMaterialPropertyBaseColor { Value = turret.Color });
    }
}
