using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }

    private float timer = 2f;
    public static float timeBetweenPresses = 0;

    public int killNum;
    private static int enemyKill = 0;
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private float startTime;
    public GameObject door;
    public GameObject tempDoor;
    public GameObject winWindow;
    public Text timeText;
    public Text time2Text, timesText;
    public bool win = false;
    public List<GameObject> doors = new List<GameObject>();
    public int doorIndex =0;

    public GameObject enemyTip;
   public int times;
    public bool isKill = false;
    public float killTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        startTime = Time.time;
        enemyKill = 0;
        win = false;
        timeBetweenPresses = 0;
        winWindow.SetActive(false);


        string gameName = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.HasKey(gameName))
        {
            times=PlayerPrefs.GetInt(gameName);
        }
        else
        {
            times=1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKill)
        {
            killTimer += Time.deltaTime;

        }
        if(killNum == enemyKill && !win)
        {
            win = true;
            SpawnWin();
        }
    }

  

    public static void EnemyKilled()
    {
        enemyKill++;
    }

    public void SpawnWin()
    {
        Debug.Log("WIN! " + (int)Mathf.Round(Time.time - startTime));
        timeText.text = "Time: " + (int)Mathf.Round(Time.time - startTime);
        StartCoroutine(FirebaseDataSender.SendWinTime((int)Mathf.Round(Time.time - startTime), timeBetweenPresses));
        timesText.text = "Times:  " + times;
        time2Text.text = "OnceTime:" + killTimer;
        winWindow.SetActive(true);
        times = 1;
        string gameName = SceneManager.GetActiveScene().name;
        PlayerPrefs.DeleteKey(gameName);
    }

    public void Next()
    {
        enemyKill = 0;
        startTime = Time.time;
        win = false;
        timeBetweenPresses = 0;
        Debug.Log("next");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
