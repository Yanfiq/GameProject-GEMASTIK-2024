using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

// SystemBase for Manage Component
// ISystem for Unmanage Component
public partial class SpawnCubeSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubeConfig>();
    }
    protected override void OnUpdate()
    {
        this.Enabled = false;

        //Check if SpawnCubeConfig is exist and there is only one SpawnCubeConfig
        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();

        for (int i = 0; i < spawnCubeConfig.amountToSpawn; i++)
        {
            Entity spawnedEntity = EntityManager.Instantiate(spawnCubeConfig.cubePrefabsEntity);
            EntityManager.SetComponentData(spawnedEntity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-10f, +5), 0.6f, UnityEngine.Random.Range(-4f, +7)),
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }
    }
}
