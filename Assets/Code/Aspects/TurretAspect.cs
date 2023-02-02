using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace Code.Aspects
{
    readonly partial struct TurretAspect : IAspect
    {
        readonly RefRO<Turret> _mTurret;
        readonly RefRO<URPMaterialPropertyBaseColor> _mBaseColor;

        public Entity CannonBallSpawn => _mTurret.ValueRO.CannonBallSpawn;
        public Entity CannonBallPrefab => _mTurret.ValueRO.CannonBallPrefab;
        public float4 Color => _mBaseColor.ValueRO.Value;
    }
}
