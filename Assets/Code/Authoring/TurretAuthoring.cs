using Code.Components;
using Unity.Entities;
using UnityEngine;

namespace Code.Authoring
{
    class TurretAuthoring : MonoBehaviour
    {
        public GameObject CannonBallPrefab;
        public Transform CannonBallSpawn;
        
        class TurretBaker : Baker<TurretAuthoring>
        {
            public override void Bake(TurretAuthoring authoring)
            {
                AddComponent(new Turret
                {
                    CannonBallPrefab = GetEntity(authoring.CannonBallPrefab),
                    CannonBallSpawn = GetEntity(authoring.CannonBallSpawn)
                });
                
                AddComponent<Shooting>();
            }
        }
    }
}

struct Turret : IComponentData
{
    public Entity CannonBallSpawn;
    public Entity CannonBallPrefab;
}