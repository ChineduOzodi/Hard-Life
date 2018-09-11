using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int width;
    public int height;
    public GameObject obj;


	// Use this for initialization
	void Start () {
		for ( int y = -height; y < height; y++ )
        {
            for (int x = -width; x < width; x++ )
            {
                GameObject parent = new GameObject();
                GameObject inst = Instantiate(obj, parent.transform);
                inst.transform.position = new Vector3(x, y);

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
