using Unity.Entities;

namespace Code.Authoring
{
    class TankAuthoring : UnityEngine.MonoBehaviour
    {
        class TankBaker : Baker<TankAuthoring>
        {
            public override void Bake(TankAuthoring authoring)
            {
                AddComponent<Tank>();
            }
        }
    }
}

struct Tank : IComponentData
{
}
