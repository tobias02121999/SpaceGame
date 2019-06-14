using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Initialize the public variables
    public float movementSpeed;

    // Run this code every single frame
    void Update()
    {
        transform.position -= transform.right * movementSpeed;
    }
}
