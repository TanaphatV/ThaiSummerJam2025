using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public virtual void Init()
    {

    }

    public virtual void OnHitByBullet()
    {

    }

    public virtual void OnHitPlayer(/*PlayerController player*/)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out PlayerController player))
        //{
        //    OnHitPlayer(player);
        //}
    }
}
