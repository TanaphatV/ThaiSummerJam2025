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
        scrollingObject.physicalObject.transform.position = new Vector3(beginPointTF.position.x, scrollingObject.physicalObject.transform.position.y, beginPointTF.position.z + 30);
    }

    public override void UpdateModule()
    {
        scrollingObject.physicalObject.transform.position += new Vector3(speed * GameManager.instance.speedMultiplier, 0,0) * Time.deltaTime; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0,1,0,0.5f);
        Gizmos.DrawSphere(beginPointTF.position,0.5f);
    }
}
