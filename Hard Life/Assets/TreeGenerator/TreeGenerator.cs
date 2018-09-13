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
        List<Vector2> availablePositions = new List<Vector2>();

        for (int y = -height; y < height; y++)
        {
            for (int x = -width; x < width; x++)
            {
                availablePositions.Add(new Vector2(x, y));
            }
        }

        


        

        //get terrain settings
        HeightMap hm = HeightMapGenerator.GenerateHeightMap(width * 2, height * 2, tg.heightMapSettings, Vector2.zero);

        //Remove all locations that are two high or low
        for (int i = availablePositions.Count - 1; i >= 0; i--)
        {
            Vector2 pos = availablePositions[i];
            float height = hm.values[(int)pos.x + width, (int) pos.y + this.height];
            if (height < treeSpawnHeight.x || height > treeSpawnHeight.y)
            {
                availablePositions.Remove(pos);
            }
        }

        //adjust treeCount if needed
        if (availablePositions.Count < numberOfTrees)
            numberOfTrees = availablePositions.Count;

        NativeArray<Entity> entities = new NativeArray<Entity>(numberOfTrees, Allocator.Temp);
        manager.Instantiate(obj, entities);

        for (int i = 0; i < numberOfTrees; i++)
        {
            float randomGrowthSpeed = UnityEngine.Random.Range(growthSpeed.x, growthSpeed.y);
            int randomPosIndex = UnityEngine.Random.Range(0, availablePositions.Count);
            Vector2 randomPos = availablePositions[randomPosIndex];

            manager.SetComponentData(entities[i], new Position { Value = new float3(randomPos.x, hm.values[(int) randomPos.x + width,(int) randomPos.y + height], -randomPos.y) });
            manager.SetComponentData(entities[i], new Scale { Value = new float3(1, 1, 1) });
            manager.SetComponentData(entities[i], new ECSWithJob.GrowthSpeed { Value = randomGrowthSpeed });
            availablePositions.Remove(randomPos);
        }

        entities.Dispose();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
