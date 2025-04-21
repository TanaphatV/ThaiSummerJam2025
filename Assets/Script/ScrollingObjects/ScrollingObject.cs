using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private List<ScrollingObjectModule> moduleList = new();

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

    public virtual void OnHitByBullet()
    {

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
        if (other.TryGetComponent(out FPSController player))
        {
            OnHitPlayer(player);
        }
    }
}
