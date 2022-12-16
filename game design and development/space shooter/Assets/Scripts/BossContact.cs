using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    private GameController gameController;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObj =  GameObject.FindWithTag("GameController");    

        if(gameObj != null){
            Debug.Log("full");
            gameController = gameObj.GetComponent<GameController>();    
        }
        else{
            Debug.Log("null");
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bolt"){
            lives--;
        }
        if(lives<=0){
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            gameController.nextWave = true;
        }

        if(other.tag == "Player"){
            gameController.ChangeLives(-1);
        }
        if(other.tag == "Player2"){
            gameController.ChangeLives2(-1);
        }
    }
}
