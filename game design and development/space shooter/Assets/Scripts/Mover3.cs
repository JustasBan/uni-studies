using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover3 : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        rb.angularVelocity = new Vector3(0,int.MaxValue, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
