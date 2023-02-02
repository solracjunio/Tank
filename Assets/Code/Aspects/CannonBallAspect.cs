using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Code.Aspects
{
    readonly partial struct CannonBallAspect : IAspect
    {
        public readonly Entity Self;

        readonly TransformAspect _transform;

        readonly RefRW<CannonBall> _cannonBall;

        public float3 Position
        {
            get => _transform.LocalPosition;
            set => _transform.LocalPosition = value;
        }

        public float3 Speed
        {
            get => _cannonBall.ValueRO.Speed;
            set => _cannonBall.ValueRW.Speed = value;
        }
    }
}
