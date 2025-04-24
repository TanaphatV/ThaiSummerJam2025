using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinVisual : MonoBehaviour
{
    public float spinSpeed;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,spinSpeed * Time.deltaTime,0));
    }
}
