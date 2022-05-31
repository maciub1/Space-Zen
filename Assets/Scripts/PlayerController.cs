using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 13.0f;
    private readonly float xRange = 13.0f;
    private readonly float zRange = 7.0f;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    private GameManager gameManager;
    public int lives = 3;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            MovePlayer();
            ConstrainPlayerPosition();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation * Quaternion.Euler(90, 0, 0));
            }
        }
        
    }

    //Moves the player based on arrow key input
    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.right);
        transform.Translate(speed * Time.deltaTime * verticalInput * Vector3.forward);
    }

    //Prevents player from leaving the screen
    void ConstrainPlayerPosition()
    {
        //Right of the screen
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //Left of the screen
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        //Bottom of the screen
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }

        //Top of the screen
        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            --lives;
            gameManager.UpdateLivesText(lives);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Extra life"))
        {
            Destroy(other.gameObject);
            ++lives;
            gameManager.UpdateLivesText(lives);
        }
        else
        {
            Destroy(other.gameObject);
            Debug.Log("Extra speed picked up!");
            speed = 20;
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        speed = 13.0f;
    }

}
