using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int scoreValue;

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

        if(other.tag == "Boundary" || other.tag == "Heal"){
            return;
        }
    
        if(other.tag == "Enemy2" && gameObject.tag == "Enemy2"){
            return;
        }

        Instantiate(explosion, transform.position, transform.rotation);
        if(other.tag == "Player"){
            gameController.ChangeLives(-1);
        }
        if(other.tag == "Player2"){
            gameController.ChangeLives2(-1);
        }

        if(gameController.lives==0){
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }

        if(gameController.lives2==0){
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }
        
        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
