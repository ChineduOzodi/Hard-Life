using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class TreeGenerator : MonoBehaviour {

    public int width;
    public int height;
    public int numberOfTrees;
    public Vector2 treeSpawnHeight;
    public Vector2 growthSpeed;
    public GameObject obj;
    public TerrainGenerator tg;

    EntityManager manager;

    // Use this for initialization
    void Start()
    {
        manager = World.Active.GetOrCreateManager<EntityManager>();

        //Set available positions
        HashSet<Vector2> takenPositions = new HashSet<Vector2>();

        NativeArray<Entity> entities = new NativeArray<Entity>(numberOfTrees, Allocator.Temp);
        manager.Instantiate(obj, entities);

        for (int i = 0; i < numberOfTrees; i++)
        {
            int randomHeight;
            int randomWidth;
            Vector2 randomPos;
            HeightMap hm;
            float height;
            int maxIter = 1000;

            do
            {
                maxIter--;
                randomHeight = UnityEngine.Random.Range(-this.height, this.height);
                randomWidth = UnityEngine.Random.Range(-width, width);
                randomPos = new Vector2(randomWidth, randomHeight);
                //get terrain settings
                hm = HeightMapGenerator.GenerateHeightMap(1, 1, tg.heightMapSettings, randomPos);
                height = hm.values[0, 0];

                //Check for collisions and height settings
                if (takenPositions.Contains(randomPos) ||
                    height < treeSpawnHeight.x ||
                    height > treeSpawnHeight.y
                    )
                {
                    continue;
                }
                break;
            }
            while (maxIter > 0);


            float randomGrowthSpeed = UnityEngine.Random.Range(growthSpeed.x, growthSpeed.y);

            manager.SetComponentData(entities[i], new Position { Value = new float3(randomPos.x, height, randomPos.y) });
            manager.SetComponentData(entities[i], new Scale { Value = new float3(1, 1, 1) });
            manager.SetComponentData(entities[i], new ECSWithJob.GrowthSpeed { Value = randomGrowthSpeed });
            takenPositions.Add(randomPos);
        }

        entities.Dispose();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
