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
    public playerCanvas playerCanvas;
    public ScoreCounter scoreCounter;
    public ScrollVisualManager scrollManager;
    public ScrollingObjectManager objectManager;
    public ScrollingObjectManager leftDecoManager;
    public ScrollingObjectManager rightDecoManager;
    public CameraManager cameraManager;
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
        leftDecoManager.Init();
        rightDecoManager.Init();
        player.OnEndGame();
        //playerCanvas.SetActive(false);
        StartCoroutine(ShowMainMenu());
        mainMenu.UpdateScoreText(0);

        leftDecoManager.StartSpawner();
        rightDecoManager.StartSpawner();
    }

    public void Update()
    {
        if(started)
        {
            scoreCounter.AddScore((int)(scorePerMeter * speedMultiplier * Time.deltaTime * 10.0f));

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
        mainMenu.UpdateScoreText(scoreCounter.score);
        objectManager.StopSpawner();
        player.OnEndGame();
        StartCoroutine(EndGameIE());
    }

    IEnumerator EndGameIE()
    {
        float diff = 1.0f - speedMultiplier;
        float speedChangeDelta = diff / 2.0f;

        // Set camera to Main Menu View
        cameraManager.SwitchToCam(0);

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

        yield return new WaitForSeconds(0.1f);

        yield return ShowMainMenu();

        ending = false;
    }

    IEnumerator ShowMainMenu()
    {
        
        player.transform.position = new(playerSpawnPoint.position.x, player.transform.position.y ,playerSpawnPoint.position.z);

        while (player.transform.position.z < playerStartPoint.position.z)
        {
            player.transform.position += new Vector3(0, 0, player.moveSpeed * 1.5f * Time.deltaTime);
            yield return null;
        }
        // Hide player canvas
        //playerCanvas.SetActive(false);
        playerCanvas.RotateCanvasOut();

        mainMenu.RotateCanvasBackIn();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameIE());
    }

    IEnumerator StartGameIE()
    {
        player.Restart();
        scoreCounter.Restart();

        // Set camera to TPS
        cameraManager.SwitchToCam(1);

        elapsedTime = 0;
        milestoneCount = 0;
        started = true;

        // Show player canvas
        //playerCanvas.SetActive(true);
        playerCanvas.RotateCanvasBackIn();

        yield return new WaitForSeconds(3.0f);
        
        objectManager.StartSpawner();
    }
}
