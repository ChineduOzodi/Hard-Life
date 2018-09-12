using HardLife.JobSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class GameManager : MonoBehaviour {

    public int width;
    public int height;
    public float growthSpeed;
    public GameObject obj;


    TransformAccessArray transforms;
    GrowthJob growthJob;
    JobHandle growthHandle;

    private void OnDisable()
    {
        growthHandle.Complete();
        transforms.Dispose();
    }

    // Use this for initialization
    void Start () {
        transforms = new TransformAccessArray(0,-1);

        GameObject parent = new GameObject();
		for ( int y = -height; y < height; y++ )
        {
            for (int x = -width; x < width; x++ )
            {
                GameObject inst = Instantiate(obj, parent.transform);
                inst.transform.position = new Vector3(x,0, y);
                transforms.Add(inst.transform);

            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        growthHandle.Complete();

        growthJob = new GrowthJob()
        {
            growthSpeed = this.growthSpeed,
            deltaTime = Time.deltaTime
        };

        growthHandle = growthJob.Schedule(transforms);

        JobHandle.ScheduleBatchedJobs();
		
	}
}
