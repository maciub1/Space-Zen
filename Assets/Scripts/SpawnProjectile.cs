using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    private readonly float spawnInterval = 3.0f;
    private readonly float spawnDelay = 0.5f;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
    }
    void Spawn()
    {
        if (gameManager.isGameActive)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation * Quaternion.Euler(90, 0, 0));
        }
        
    }
}
