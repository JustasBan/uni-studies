using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoltContact : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private GameController gameController;
    public GameObject playerExplosion;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject gameObj =  GameObject.FindWithTag("GameController");    

        if(gameObj != null){
            Debug.Log("full");
            gameController = gameObj.GetComponent<GameController>();    
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            gameController.ChangeLives(-1);
        }
        if(other.tag == "Enemy2" || other.tag == "Heal" || other.tag == "Boundary"){
            return;
        }
        if(other.tag == "Player2"){
            gameController.ChangeLives2(-1);
        }

        if(gameController.lives<=0){
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }

        if(gameController.lives2<=0){
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
            Destroy(other.gameObject);
        }
        
        Destroy(gameObject);
    }
}
