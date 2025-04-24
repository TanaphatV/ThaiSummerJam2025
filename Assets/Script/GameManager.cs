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

    public MainMenuFunctions mainMenu;
    public ScoreCounter scoreCounter;
    public ScrollVisualManager scrollManager;
    public ScrollingObjectManager objectManager;
    public FPSController player;

    public Transform playerSpawnPoint;
    public Transform playerStartPoint;

    public float scorePerMeter;

    public List<SpeedStep> milestones = new();
    int milestoneCount = 0;
    public float speedMultiplier { get; private set; } = 1.0f;

    private float elapsedTime;
    public bool started { get; private set; } = false;
    
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        started = false;
        objectManager.Init();
        player.OnEndGame(); 
        StartCoroutine(ShowMainMenu());
    }

    public void Update()
    {
        if(started)
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

    bool ending = false;
    public void EndGame()
    {
        if (ending)
            return;

        ending = true;
        started = false;
        scoreCounter.RegisterScore();
        objectManager.StopSpawner();
        player.OnEndGame();
        StartCoroutine(EndGameIE());
    }

    IEnumerator EndGameIE()
    {
        float diff = 1.0f - speedMultiplier;
        float speedChangeDelta = diff / 2.0f;

        while(player.transform.position.z > playerSpawnPoint.position.z)
        {
            player.transform.position -= new Vector3(0,0, objectManager.speed * speedMultiplier * Time.deltaTime);
            yield return null;
        }

        while (!Mathf.Approximately(1.0f,speedMultiplier))
        {
            speedMultiplier = Mathf.MoveTowards(speedMultiplier,1.0f, Mathf.Abs(speedChangeDelta) * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        yield return ShowMainMenu();

        ending = false;
    }

    IEnumerator ShowMainMenu()
    {
        
        player.transform.position = new(playerSpawnPoint.position.x, player.transform.position.y ,playerSpawnPoint.position.z);

        while (player.transform.position.z < playerStartPoint.position.z)
        {
            player.transform.position += new Vector3(0, 0, player.moveSpeed * 0.6f * Time.deltaTime);
            yield return null;
        }

        mainMenu.RotateCanvasBackIn();
    }

    public void StartGame()
    {
        player.Restart();
        objectManager.StartSpawner();
        scoreCounter.Restart();
        elapsedTime = 0;
        milestoneCount = 0;
        started = true;
    }
}
