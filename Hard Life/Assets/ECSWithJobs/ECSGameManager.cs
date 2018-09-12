using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ECSWithJob {
    public class ECSGameManager : MonoBehaviour
    {

        public int width;
        public int height;
        public float growthSpeed;
        public GameObject obj;

        EntityManager manager;

        // Use this for initialization
        void Start()
        {
            manager = World.Active.GetOrCreateManager<EntityManager>();

            NativeArray<Entity> entities = new NativeArray<Entity>(height * 2 * width * 2, Allocator.Temp);
            manager.Instantiate(obj, entities);

            GameObject parent = new GameObject();
            for (int y = -height; y < height; y++)
            {
                for (int x = -width; x < width; x++)
                {
                    float randomValue = UnityEngine.Random.Range(.1f, 1f);
                    manager.SetComponentData(entities[(x + width) * height * 2 + y + height], new Position { Value = new float3(x, 0, y) });
                    manager.SetComponentData(entities[(x + width) * height * 2 + y + height], new Scale { Value = new float3(1, 1, 1) });
                    manager.SetComponentData(entities[(x + width) * height * 2 + y + height], new GrowthSpeed { Value = growthSpeed});
                }
            }

            entities.Dispose();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

