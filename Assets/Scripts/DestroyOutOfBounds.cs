using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private readonly float topBound = 11;
    private readonly float lowBound = -9;
    private readonly float sideBound = 19;

    // Destroy objects off screen
    void Update()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < lowBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > sideBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -sideBound)
        {
            Destroy(gameObject);
        }

    }
}
