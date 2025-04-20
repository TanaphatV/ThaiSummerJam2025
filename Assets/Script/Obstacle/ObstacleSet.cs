using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSet : MonoBehaviour
{
    public float length;
    public bool isActive { get; private set; }

    List<Obstacle> obstacle;
    float speed;

   public void Init(float speed)
    {
        obstacle = new(GetComponentsInChildren<Obstacle>());
        this.speed = speed;
    }

    public void OnSpawn()
    {
        isActive = true;
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        isActive = false;
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = new Color(1,0,0,0.2f);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z - length / 2.0f), new Vector3(2, 1, length));
        //Gizmos.DrawCube(new Vector3(transform.position.x,transform.position.y,transform.position.z - length/2.0f),new Vector3(2,1,length));
    }
}
