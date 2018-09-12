using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

namespace HardLife.JobSystem {

    [BurstCompile]
    public struct GrowthJob : IJobParallelForTransform
    {

        public float growthSpeed;
        public float deltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            Vector3 scale = transform.localScale;
            scale += growthSpeed * deltaTime * (Vector3.up);

            transform.localScale = scale;
        }
    }
}
