using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnCubeConfigAuthoring : MonoBehaviour
{
    public GameObject cubePrefabs;
    public int amountToSpawn;

    public class Baker : Baker<SpawnCubeConfigAuthoring>
    {
        public override void Bake(SpawnCubeConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new SpawnCubeConfig
            {
                cubePrefabsEntity = GetEntity(authoring.cubePrefabs, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn,
            });
        }
    }
}

public struct SpawnCubeConfig : IComponentData
{
    public Entity cubePrefabsEntity;
    public int amountToSpawn;
}
