using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpeedStep
{
    public float speedMultiplier;
    public float timeElapsed;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager instance null");
            return _instance;
        }
    }
    private static GameManager _instance;

    public ScoreCounter scoreCounter;
    public ScrollVisualManager scrollManager;
    public ScrollingObjectManager objectManager;

    public float scorePerMeter;

    public List<SpeedStep> milestones = new();
    int milestoneCount = 0;
    public float speedMultiplier { get; private set; } = 1.0f;

    private float elapsedTime;
    bool start;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        start = false;
        objectManager.Init();
        Restart();
    }

    public void Update()
    {
        if(start)
        {
            scoreCounter.AddScore((int)(scorePerMeter * speedMultiplier * Time.deltaTime));

            if(elapsedTime >= milestones[milestoneCount].timeElapsed)
            {
                speedMultiplier = milestones[milestoneCount].speedMultiplier;
                if (milestoneCount < milestones.Count - 1)
                {
                    milestoneCount++;
                }
            }

            elapsedTime += Time.deltaTime;
        }
    }

    void EndGame()
    {
        scoreCounter.RegisterScore();
        objectManager.StopSpawner();
        StartCoroutine(EndGameIE());   
    }

    IEnumerator EndGameIE()
    {
        float diff = 1.0f - speedMultiplier;
        float speedChangeDelta = diff / 2.0f;
        while (!Mathf.Approximately(1.0f,speedMultiplier))
        {
            speedMultiplier = Mathf.MoveTowards(speedMultiplier,1.0f, speedChangeDelta * Time.deltaTime);
            yield return null;
        }
    }

    void Restart()
    {
        objectManager.StartSpawner();
        scoreCounter.Restart();
        elapsedTime = 0;
        milestoneCount = 0;
        start = true;
    }
}
