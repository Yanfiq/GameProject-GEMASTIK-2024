using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

// SystemBase for Manage Component
// ISystem for Unmanage Component
public partial struct RotatingCubeSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        return;

        //RefRW for Read and Write
        //RefRO for Read only
        /*
            If not specified it will be considered
            Read only, but using copy instead of
            pointer
        */

        /* foreach((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed) 
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithNone<Player>())
        {
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }*/

        //or 

        /* foreach((RefRW<LocalTransform> localTransform, RefRO<RotateSpeed> rotateSpeed) 
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>())
        {
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }*/

        /*RotatingCubeJob rotatingCubeJob = new RotatingCubeJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        rotatingCubeJob.Schedule();*/
        // state.Dependency = rotatingCubeJob.Schedule(state.Dependency); // use this if using other than IJobEntity
    }

    [BurstCompile]
    //[WithNone(typeof(Player))]
    [WithAll(typeof(RotatingCube))]
    public partial struct RotatingCubeJob : IJobEntity
    {
        public float deltaTime;

        // ref = read write
        // in = read only
        public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
        {
            float x = 1;
            for(int i = 0; i < 10000; i++)
            {
                x *= 2;
                x /= 2;
            }
            localTransform = localTransform.RotateY(rotateSpeed.value * deltaTime * x);
        }
    }
}
