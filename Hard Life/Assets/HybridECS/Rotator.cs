using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float speed;
}

class RotatorSystem : ComponentSystem
{
    struct Components
    {
        public Rotator rotator;
        public Transform transform;
    }

    //Behavior
    protected override void OnUpdate()
    {
        foreach(var entity in GetEntities<Components>())
        {
            entity.transform.Rotate(0, entity.rotator.speed, 0);
        } 
    }
}
