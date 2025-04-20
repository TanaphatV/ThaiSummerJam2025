using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform deSpawnPoint;

    public float speed;

    public List<ObstacleSet> obstacleList;

    private List<ObstacleSet> activeObstacleList = new();

    private ObstacleSet lastSpawnedSet;
    private ObstacleSet upcomingSet;

    public bool start;

    private void Start()
    {
        StartSpawner();
    }
    private void Update()
    {
        if (!start)
            return;

        if (lastSpawnedSet.transform.position.z < (spawnPoint.position.z - upcomingSet.length))
        {
            SpawnObstacle();
        }

        CheckObstacle();
    }

    public void StartSpawner()
    {
        start = true;
        PreInitNextSet();
        SpawnObstacle();
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
        ObstacleSet set = Instantiate(upcomingSet);
        set.transform.position = spawnPoint.position;
        set.Init(speed);
        lastSpawnedSet = set;
        activeObstacleList.Add(set);
        PreInitNextSet();
    }

    void PreInitNextSet()
    {
        upcomingSet = obstacleList[Random.Range(0, obstacleList.Count)];
    }

}
