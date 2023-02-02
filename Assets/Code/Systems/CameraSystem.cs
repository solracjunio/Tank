using Code.MonoBehaviours;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Code.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    partial struct CameraSystem : ISystem
    {
        Entity _target;
        Random _random;
        EntityQuery _tanksQuery;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _random = Random.CreateFromIndex(1234);
            _tanksQuery = SystemAPI.QueryBuilder().WithAll<Tank>().Build();
            state.RequireForUpdate(_tanksQuery);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        public void OnUpdate(ref SystemState state)
        {
            if (_target == Entity.Null || UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space))
            {
                var tanks = _tanksQuery.ToEntityArray(Allocator.Temp);
                _target = tanks[_random.NextInt(tanks.Length)];
            }

            var cameraTransform = CameraSingleton.Instance.transform;
            var tankTransform = SystemAPI.GetComponent<LocalToWorld>(_target);
            cameraTransform.position =
                tankTransform.Position - 10.0f * tankTransform.Forward + new float3(0.0f, 5.0f, 0.0f);
            cameraTransform.LookAt(tankTransform.Position, new float3(0.0f, 1.0f, 0.0f));
        }
    }
}
