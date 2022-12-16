using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealByContact : MonoBehaviour
{
    private GameController gameController;

    // Start is called before the first frame update
    void Start(){
        GameObject gameObj =  GameObject.FindWithTag("GameController");

        if(gameObj != null){
            Debug.Log("full");
            gameController = gameObj.GetComponent<GameController>();
        }
        else{
            Debug.Log("null");
        }
    }

    void OnTriggerEnter(Collider other){

        if(other.tag == "Player"){
            gameController.ChangeLives(1);
            Destroy(gameObject);
        }
        if(other.tag == "Player2"){
            gameController.ChangeLives2(1);
            Destroy(gameObject);
        }
    }
}
