using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;

    private readonly float xSpawnRange = 13.0f;
    private readonly float ySpawn = 1.0f;
    private readonly float zPowerupRange = 7.0f;
    private readonly float zEnemySpawn = 10.0f;

    private float powerupSpawnTime;
    private float enemySpawnTime;
    public int score;
    private int enemiesToSpawn = 1;
    private int powerupsToSpawn = 1;
    public bool isGameActive;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // While game is active spawn a random enemy
    IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(enemySpawnTime);
            for(int i = 0; i < enemiesToSpawn; ++i)
            {
                int randomIndex = Random.Range(0, enemies.Length);
                Vector3 spawnPos = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawn, zEnemySpawn);
                Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
            }
            ++enemiesToSpawn;
            
        }
    }

    // While game is active spawn a random powerup
    IEnumerator SpawnPowerup()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(powerupSpawnTime);
            for( int i = 0; i < powerupsToSpawn; ++i)
            {
                int randomIndex = Random.Range(0, powerups.Length);
                Vector3 spawnPos = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawn, Random.Range(-zPowerupRange, zPowerupRange));
                Instantiate(powerups[randomIndex], spawnPos, powerups[randomIndex].gameObject.transform.rotation);
            }
            ++powerupsToSpawn;
        }
    }
    

    public void UpdateLivesText(int lives)
    {
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

     void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    //Add one score every second and decrease time betweeen spawns
    IEnumerator Timer()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1f);
            ++score;
            UpdateScore();
        }  
    }

    // Start the game, remove title screen, reset score, lives and spawn times
    public void StartGame()
    {
        titleScreen.SetActive(false);
        isGameActive = true;
        enemySpawnTime = 5.0f;
        powerupSpawnTime = 10.0f;
        score = 0;
        playerController.lives = 3;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(Timer());
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Restart game by reloading the scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
    }
}
