using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreByContact : MonoBehaviour
{
    private GameController gameController;

    // Start is called before the first frame update
    void Start(){
        GameObject gameObj =  GameObject.FindWithTag("GameController");

        if(gameObj != null){
            gameController = gameObj.GetComponent<GameController>();
        }
        else{
            Debug.Log("null");
        }
    }

    void OnTriggerEnter(Collider other){

        if(other.tag == "Player" || other.tag == "Player2"){
            gameController.AddScore(100);
            Destroy(gameObject);
        }
    }
}
