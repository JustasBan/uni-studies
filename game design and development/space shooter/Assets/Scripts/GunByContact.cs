using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunByContact : MonoBehaviour
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
            gameController.secondWeapon = true;
            Destroy(gameObject);
        }
       if(other.tag == "Player2"){
            gameController2.secondWeapon = true;
            Destroy(gameObject);
        }
    }
}
