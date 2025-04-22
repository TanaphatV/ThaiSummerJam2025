using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// From this tutorial: https://youtu.be/bVzRCqYZzNw

public class ScrollingTexture : MonoBehaviour
{
    [SerializeField] private float _speedX = 0;
    [SerializeField] private float _speedY = 0;
    private Renderer _renderer;
    float _currentX, _currentY;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _currentX = _renderer.material.mainTextureOffset.x;
        _currentY = _renderer.material.mainTextureOffset.y;
    }

    void FixedUpdate()
    {
        _currentX += _speedX * Time.deltaTime;
        _currentY += _speedY * Time.deltaTime;
        Shader.SetGlobalVector("_Offset", new Vector4(_currentX, _currentY, 0, 0));
    }
}
