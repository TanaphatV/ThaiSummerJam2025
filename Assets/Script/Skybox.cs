using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Speed of skybox rotation

    // Update is called once per frame
    void Update()
    {
        // Rotate the skybox by modifying the "_Rotation" property
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}