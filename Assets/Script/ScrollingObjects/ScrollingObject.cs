using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private List<ScrollingObjectModule> moduleList = new();

    public int health;
    public GameObject physicalObject;


    public virtual void Init()
    {
        moduleList = new(GetComponents<ScrollingObjectModule>());
        foreach (var mod in moduleList)
            mod.Init(this);
    }

    private void Update()
    {
        UpdateModules();
    }

    public virtual void OnHitByBullet(bool oneShot)
    {
        health -= 1;
        if (health <= 0 || oneShot)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnHitPlayer(FPSController player)
    {

    }

    private void UpdateModules()
    {
        foreach (var mod in moduleList)
            mod.UpdateModule();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<FPSController>();
        if (player != null)
        {
            if (!GameManager.instance.started)
                return;

            OnHitPlayer(player);
            Destroy(gameObject);
        }
    }
}
