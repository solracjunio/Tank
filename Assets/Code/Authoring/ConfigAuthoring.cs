using UnityEngine;
using Unity.Entities;

namespace Code.Authoring
{
    class ConfigAuthoring : MonoBehaviour
    {
        public GameObject tankPrefab;
        public int tankCount;
        public float safeZoneRadius;

        class ConfigBaker : Baker<ConfigAuthoring>
        {
            public override void Bake(ConfigAuthoring authoring)
            {
                AddComponent(new Config
                {
                    TankPrefab = GetEntity(authoring.tankPrefab),
                    TankCount = authoring.tankCount,
                    SafeZoneRadius = authoring.safeZoneRadius
                });
            }
        }
    }
}

struct Config : IComponentData
{
    public Entity TankPrefab;
    public int TankCount;
    public float SafeZoneRadius;
}