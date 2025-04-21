using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollModule : ScrollingObjectModule
{
    public Transform beginPointTF;
    public float speed;

    public override void Init(ScrollingObject scrollingObject)
    {
        base.Init(scrollingObject);
        scrollingObject.physicalObject.transform.position = beginPointTF.position;
    }

    public override void UpdateModule()
    {
        scrollingObject.physicalObject.transform.position += new Vector3(speed,0,0) * Time.deltaTime; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,1,0,0.5f);
        Gizmos.DrawSphere(beginPointTF.position,0.5f);
    }
}
