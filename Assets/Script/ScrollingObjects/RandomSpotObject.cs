using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class RandomSpotObject : ScrollingObject
{
    public Transform obstacleTF;
    public Vector2 spawnZoneSize;

    public override void Init()
    {
        Vector2 halfSize = spawnZoneSize / 2.0f;
        Vector3 randomSpot = new Vector3(
            transform.position.x + Random.Range(-halfSize.x,halfSize.x),
            obstacleTF.position.y,
            transform.position.z + Random.Range(-halfSize.y, halfSize.y)
            );
        obstacleTF.position = randomSpot;
       
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(transform.position,new Vector3(spawnZoneSize.x,0.1f,spawnZoneSize.y));
    }
}
