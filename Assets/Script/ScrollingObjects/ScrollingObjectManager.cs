using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScrollingObjectSetInfo
{
    public int weight;
    public ScrollingObjectSet objectSet;
}

public class ScrollingObjectManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform deSpawnPoint;

    public float speed;

    public List<ScrollingObjectSetInfo> objectSetInfoList;

    private List<ScrollingObjectSet> objectSetList;

    private List<ScrollingObjectSet> activeObstacleList = new();

    private ScrollingObjectSet lastSpawnedSet;
    private ScrollingObjectSet upcomingSet;

    private bool start;

    private void Update()
    {
        if (start)
        {
            if (lastSpawnedSet.transform.position.z < (spawnPoint.position.z - upcomingSet.length))
            {
                SpawnObjectSet();
            }

        }
        CheckObjectSet();
    }

    public void Init()
    {
        objectSetList = new();

        foreach (var info in objectSetInfoList)
        {
            for(int i = 0; i < info.weight; i++)
            {
                objectSetList.Add(info.objectSet);
            }
        }
    }

    public void StartSpawner()
    {
        PreInitNextSet();
        SpawnObjectSet();
        start = true;
    }

    public void StopSpawner()
    {
        start = false;
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
        set.Init(speed * GameManager.instance.speedMultiplier);
        lastSpawnedSet = set;
        activeObstacleList.Add(set);
        PreInitNextSet();
    }

    void PreInitNextSet()
    {
        upcomingSet = objectSetList[Random.Range(0, objectSetList.Count)];
    }

}
