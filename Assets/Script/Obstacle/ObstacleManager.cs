using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform deSpawnPoint;

    public float speed;
    public float spawnInterval;

    public List<ObstacleSet> obstacleList;

    private List<ObstacleSet> activeObstacleList = new();

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            timer = 0;
            SpawnObstacle();
        }

        CheckObstacle();
    }

    void CheckObstacle()
    {
        int count = activeObstacleList.Count;
        for (int i = 0; i < count; i++)
        {
            var set = activeObstacleList[i];
            if (set.transform.position.z < deSpawnPoint.position.z)
            {
                activeObstacleList.RemoveAt(i);
                i--;
                count--;
                Destroy(set.gameObject);
            }
        }
    }

    void SpawnObstacle()
    {
        ObstacleSet set = Instantiate(obstacleList[Random.Range(0,obstacleList.Count)]);
        set.transform.position = spawnPoint.position;
        set.Init(speed);

        activeObstacleList.Add(set);
    }

}
