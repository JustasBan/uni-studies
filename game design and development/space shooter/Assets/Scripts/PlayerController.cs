using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMax, xMin, zMax, zMin;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed, tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;

    
    public GameObject shot2;
    public Transform shotSpawn2;
    
    
    public GameObject shot3;
    public Transform shotSpawn3;

    public float fireRate;
    private float nextFire;
    private AudioSource audioSource ;
    public bool makeFaster;
    public DateTime startTime;

    public bool secondWeapon;

    void Start ()
    {
        makeFaster = false;
        secondWeapon = false;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource >();
    }

    // For every frame
    void Update(){

        if(Time.time > nextFire && Input.GetKey("space")){
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            if(secondWeapon){
                Instantiate(shot2, shotSpawn2.position, shotSpawn2.rotation);
                Instantiate(shot3, shotSpawn3.position, shotSpawn3.rotation);
            }

            audioSource.Play();
        }     
    }

    // For every physics step
    void FixedUpdate(){
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement*speed;

        if(makeFaster){
            if(((TimeSpan)(DateTime.Now - startTime)).Seconds < 10){
                rb.velocity = movement*(speed+20);
            }
            else{
                makeFaster = false;
                startTime = DateTime.MinValue;
            }

        }

        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.position.x * -tilt);
    }
}
