using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObjectManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform deSpawnPoint;

    public float speed;

    public List<ScrollingObjectSet> obstacleList;

    private List<ScrollingObjectSet> activeObstacleList = new();

    private ScrollingObjectSet lastSpawnedSet;
    private ScrollingObjectSet upcomingSet;

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
            SpawnObjectSet();
        }

        CheckObjectSet();
    }

    public void StartSpawner()
    {
        start = true;
        PreInitNextSet();
        SpawnObjectSet();
    }


    void CheckObjectSet()
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

    void SpawnObjectSet()
    {
        ScrollingObjectSet set = Instantiate(upcomingSet);
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
