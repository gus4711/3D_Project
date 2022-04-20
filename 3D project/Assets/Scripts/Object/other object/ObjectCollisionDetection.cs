using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionDetection : MonoBehaviour
{
    Rigidbody rigidbody;

    float dropTime;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dropTime += Time.deltaTime;
        if(rigidbody.velocity.magnitude == 0 && dropTime > 1f)
        {
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            GameObject.Destroy(this);
        }
    }
}
