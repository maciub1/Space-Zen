using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 40.0f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (gameObject.CompareTag("Projectile"))
                transform.Translate(speed * Time.deltaTime * -gameObject.transform.forward);
            else
                transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
        
    }
}
