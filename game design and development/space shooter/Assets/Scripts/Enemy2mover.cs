using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2mover : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        rb.angularVelocity = new Vector3(0,Random.value*speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
