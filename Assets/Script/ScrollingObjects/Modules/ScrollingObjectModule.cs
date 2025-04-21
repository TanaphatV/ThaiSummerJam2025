using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScrollingObjectModule : MonoBehaviour
{
    protected ScrollingObject scrollingObject;

    public virtual void Init(ScrollingObject scrollingObject)
    {
        this.scrollingObject = scrollingObject;
    }

    public virtual void UpdateModule()
    {

    }
}
