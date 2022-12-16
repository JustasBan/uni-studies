using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allien2 : MonoBehaviour
{
    
    private Rigidbody rb;
    private AudioSource audioSource ;
    public float fireRate;
    private float nextFire;

    public GameObject shot;
    public Transform shotSpawn;

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
            
            audioSource.Play();
        }
    }

    void FixedUpdate(){

    }
}
