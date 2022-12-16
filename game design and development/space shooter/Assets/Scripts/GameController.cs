using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public GameObject heal;
    public GameObject more;
    public GameObject speed;
    public GameObject gun;
    public GameObject alien1;
    public GameObject alien2;
    public GameObject alien3;
    public GameObject player2;
    public int hazardCount;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text livesText;
    public Text livesText2;
    public Text bestScoreText;

    private bool gameOver;
    private bool gameRestart;
    private int score;
    private int bestScore;
    public int lives;
    public int lives2;
    public bool nextWave;
    public bool palyer2spawned;

    void Start(){

        gameOver=false;
        gameRestart=false;
        restartText.text="";
        gameOverText.text="";
        score = 0;
        lives = 3;
        lives2 = 3;
        nextWave = true;
        palyer2spawned = false;

        string path = Application.dataPath+ "/save.txt";
        
        using(System.IO.StreamReader Textfile = new (path)){
            string line = Textfile.ReadLine();
            bestScore = System.Convert.ToInt32(line);
        }
        bestScoreText.text = "Best score: " + bestScore.ToString();

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves(){
        yield return new WaitForSeconds(spawnWait);

        while (true)
        {
            if(nextWave){
                nextWave = false;
                for (int i = 0; i < hazardCount; i++)
                {
                    Vector3 spawnPosition = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
            
                    Instantiate(hazard, spawnPosition, spawnRotation);
        
                    yield return new WaitForSeconds(spawnWait);
                }

                Vector3 spawnPosition2 = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation2 = Quaternion.identity;
                Instantiate(heal, spawnPosition2, spawnRotation2);
                
                Vector3 spawnPosition3 = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation3 = Quaternion.identity;
                Instantiate(more, spawnPosition3, spawnRotation3);
                
                Vector3 spawnPosition4 = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation4 = Quaternion.identity;
                Instantiate(speed, spawnPosition4, spawnRotation4);
                
                Vector3 spawnPosition5 = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation5 = Quaternion.identity;
                Instantiate(gun, spawnPosition5, spawnRotation5);

                yield return new WaitForSeconds(waveWait);

                Vector3 spawnPosition6 = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation6 = Quaternion.identity;
                Instantiate(alien1, spawnPosition6, spawnRotation6);

                if(gameOver){
                    restartText.text = "Press R for restart";
                    gameRestart = true;
                    break;
                }
                yield return new WaitForSeconds(waveWait);

                Vector3 spawnPosition7 = new(UnityEngine.Random.Range(-spawnValues.x, spawnValues.x), 0, spawnValues.z);
                Quaternion spawnRotation7 = Quaternion.identity;
                Instantiate(alien2, spawnPosition7, spawnRotation7);

                if(gameOver){
                    restartText.text = "Press R for restart";
                    gameRestart = true;
                    break;
                }
                yield return new WaitForSeconds(waveWait);

                Vector3 spawnPosition8 = new(0, 0, 10);
                Quaternion spawnRotation8 = Quaternion.identity;
                Instantiate(alien3, spawnPosition8, spawnRotation8);

                yield return new WaitForSeconds(waveWait+15);
                if(gameOver){
                    restartText.text = "Press R for restart";
                    gameRestart = true;
                    break;
                }
            }
	        
        }
    }

    void Update(){

        if(Input.GetKeyDown(KeyCode.Keypad2) && !palyer2spawned){
            
            Vector3 spawnPosition = new(0, 0, 0);
	        Quaternion spawnRotation = Quaternion.identity;
	    
	        Instantiate(player2, spawnPosition, spawnRotation);
            livesText2.text = "Lives2: 3";
            palyer2spawned = true;
        }

        if(gameRestart){
            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        UpdateBestScore();
    }

    public void AddScore(int newScoreValue){
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore(){
        scoreText.text = "Score: " + score;
    }
    public void ChangeLives(int newLivesValue){
        lives += newLivesValue;
        UpdateLives();
    }
    void UpdateLives(){
        livesText.text = "Lives: " + lives;
    }
    public void ChangeLives2(int newLivesValue){
        lives2 += newLivesValue;
        UpdateLives2();
    }
    void UpdateLives2(){
        livesText2.text = "Lives2: " + lives2;
    }
    
    public void GameOver(){
        gameOverText.text = "Game over!";
        gameOver = true;
    }

    void UpdateBestScore(){
        if(score>bestScore){
            string path = Application.dataPath+ "/save.txt";
            using(System.IO.StreamWriter Textfile = new (path)){
                Textfile.WriteLine(score);
            }

            bestScoreText.text = "Best score: " + score + ". New!";
        }
    }
}
