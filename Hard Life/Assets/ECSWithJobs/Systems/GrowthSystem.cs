using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSWithJob
{
    public class GrowthSystem : JobComponentSystem
    {

        struct GrowthJob : IJobProcessComponentData<Scale, Position, GrowthSpeed>
        {
            public float deltaTime;

            public void Execute(ref Scale scale,  ref Position pos, [ReadOnly] ref GrowthSpeed growthSpeed)
            {
                float3 value = scale.Value;

                value += deltaTime * growthSpeed.Value * new float3(0, 1, 0);

                scale.Value = value;
            }
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            GrowthJob growthJob = new GrowthJob
            {
                deltaTime = Time.deltaTime
            };

            JobHandle jobHandle = growthJob.Schedule(this,inputDeps);

            return jobHandle;
        }
    }

}

