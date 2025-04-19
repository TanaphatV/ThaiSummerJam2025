using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSet : MonoBehaviour
{
    List<Obstacle> obstacle;
    public bool isActive;

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
}
