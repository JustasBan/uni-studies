using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allien1 : MonoBehaviour
{
    
    private Rigidbody rb;
    private AudioSource audioSource ;
    public float fireRate;
    private float nextFire;

    public GameObject shot;
    public Transform shotSpawn;

    
    public GameObject shot2;
    public Transform shotSpawn2;
    
    
    public GameObject shot3;
    public Transform shotSpawn3;

    public GameObject shot4;
    public Transform shotSpawn4;

    
    public GameObject shot5;
    public Transform shotSpawn5;
    
    
    public GameObject shot6;
    public Transform shotSpawn6;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource >();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Instantiate(shot2, shotSpawn2.position, shotSpawn2.rotation);
            Instantiate(shot3, shotSpawn3.position, shotSpawn3.rotation);
            Instantiate(shot4, shotSpawn4.position, shotSpawn4.rotation);
            Instantiate(shot5, shotSpawn5.position, shotSpawn5.rotation);
            Instantiate(shot6, shotSpawn6.position, shotSpawn6.rotation);

            audioSource.Play();
        }
    }

    void FixedUpdate(){

    }
}
