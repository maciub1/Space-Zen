using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThreeProjectiles : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    private readonly float spawnInterval = 6.0f;
    private readonly float spawnDelay = 1.5f;
    private readonly float xOffset = 1.5f;
    private readonly float rotation = 30.0f;
    private Quaternion rot = Quaternion.Euler(90, 0, 0);
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating("SpawnThree", spawnDelay, spawnInterval);
    }

    void SpawnThree()
    {
        //if game is active spawn 3 projectiles
        if (gameManager.isGameActive)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation * rot);

            Vector3 rotationLeft = new Vector3(0, 0, rotation);
            Vector3 offsetLeft = new Vector3(xOffset, 0, 0);
            Instantiate(projectilePrefab, projectileSpawnPoint.position + offsetLeft, transform.rotation * rot * Quaternion.Euler(rotationLeft));

            Vector3 rotationRight = new Vector3(0, 0, -rotation);
            Vector3 offsetRight = new Vector3(-xOffset, 0, 0);
            Instantiate(projectilePrefab, projectileSpawnPoint.position + offsetRight, transform.rotation * rot * Quaternion.Euler(rotationRight));
        } 
    }
}
