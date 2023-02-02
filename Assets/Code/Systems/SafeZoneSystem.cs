using Code.Authoring.Components;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Code.Systems
{
    [WithAll(typeof(Turret))]
    [BurstCompile]
    partial struct SafeZoneJob : IJobEntity
    {
        [NativeDisableContainerSafetyRestriction]
        public ComponentLookup<Shooting> ShootingLookup;

        public float SquaredRadius;

        void Execute(Entity entity, TransformAspect transform)
        {
            ShootingLookup.SetComponentEnabled(entity, math.lengthsq(transform.Position) > SquaredRadius);
        }
    }

    [BurstCompile]
    partial struct SafeZoneSystem : ISystem
    {
        ComponentLookup<Shooting> _mShootingLookup;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Config>();
            
            _mShootingLookup = state.GetComponentLookup<Shooting>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float radius = SystemAPI.GetSingleton<Config>().SafeZoneRadius;
            const float debugRenderStepInDegrees = 20;

            for (float angle = 0; angle < 360; angle += debugRenderStepInDegrees)
            {
                var a = float3.zero;
                var b = float3.zero;
                math.sincos(math.radians(angle), out a.x, out a.z);
                math.sincos(math.radians(angle + debugRenderStepInDegrees), out b.x, out b.z);
                UnityEngine.Debug.DrawLine(a * radius, b * radius);
            }
            
            _mShootingLookup.Update(ref state);
            var safeZoneJob = new SafeZoneJob()
            {
                ShootingLookup = _mShootingLookup, 
                SquaredRadius = radius * radius
            };
            safeZoneJob.ScheduleParallel();
        }
    }
}
