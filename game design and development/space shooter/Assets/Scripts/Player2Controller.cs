using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary2
{
    public float xMax, xMin, zMax, zMin;
}
public class Player2Controller : MonoBehaviour
{
    private Rigidbody rb;
    public float speed, tilt;
    public Boundary2 boundary2;
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

        if(Time.time > nextFire && Input.GetKey("enter")){
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
        float moveVertical=0;
        float moveHorizontal=0;
    void FixedUpdate(){

        if(Input.GetKey(KeyCode.Keypad4)){
            moveHorizontal += -0.05f;
        }
        if(rb.position.x < boundary2.xMin){
            moveHorizontal =0.05f;
        }

        if(Input.GetKey(KeyCode.Keypad6)){
            moveHorizontal += 0.05f;
        }
        if(rb.position.x > boundary2.xMax){
            moveHorizontal =-0.05f;
        }
        if(!Input.GetKey(KeyCode.Keypad6) && !Input.GetKey(KeyCode.Keypad4)){
            moveHorizontal =0;
        }

        if(Input.GetKey(KeyCode.Keypad8)){
            moveVertical += 0.05f;
        }
        if(rb.position.z < boundary2.zMin){
            moveVertical =0.05f;
        }
        if(Input.GetKey(KeyCode.Keypad5)){
            moveVertical += -0.05f;
        }
        if(rb.position.z > boundary2.zMax){
            moveVertical =-0.05f;
        }

        if(!Input.GetKey(KeyCode.Keypad8) && !Input.GetKey(KeyCode.Keypad5)){
            moveVertical = 0;
        }
       

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

        if(rb.position.x > boundary2.xMin && rb.position.x < boundary2.xMax)

        /*rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary2.xMin, boundary2.xMax), 
            0.0f, 
            Mathf.Clamp(rb.position.z, boundary2.zMin, boundary2.zMax));
*/
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.position.x * -tilt);
    }
}
