using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace ECSWithJob
{
    [Serializable]
    public struct GrowthSpeed : IComponentData
    {
        public float Value;        
    }

    public class GrowthSpeedComponent: ComponentDataWrapper<GrowthSpeed> { }
}

