using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedByContact : MonoBehaviour
{
    private PlayerController gameController;
    private Player2Controller gameController2;

    // Start is called before the first frame update
    void Start(){
        GameObject gameObj =  GameObject.FindWithTag("Player");
        GameObject gameObj2 =  GameObject.FindWithTag("Player2");

        if(gameObj != null){
            Debug.Log("full");
            gameController = gameObj.GetComponent<PlayerController>();
        }
        else{
            Debug.Log("null");
        }

        if(gameObj2 != null){
       
            gameController2 = gameObj2.GetComponent<Player2Controller>();
        }
    }

    void OnTriggerEnter(Collider other){

        if(other.tag == "Player"){
            gameController.makeFaster = true;
            gameController.startTime = DateTime.Now;
            Destroy(gameObject);
        }

        if(other.tag == "Player2"){
            gameController2.makeFaster = true;
            gameController2.startTime = DateTime.Now;
            Destroy(gameObject);
        }
    }
}
