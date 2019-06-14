using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Initialize the public variables
    public float rotationSpeed;

    // Run this code every single frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f);
    }
}
