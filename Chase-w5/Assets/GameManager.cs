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
    public static int enemyNum = 3;
    private int enemySpawn;
    private static int enemyKill = 0;
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private float startTime;
    public GameObject door;
    public GameObject tempDoor;
    public GameObject winWindow;
    public Text timeText;
    private bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = enemyNum;
        startTime = Time.time;
        enemyKill = 0;
        win = false;
        timeBetweenPresses = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawn > 0)
        {
            win = false;
            winWindow.SetActive(false);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnEnemy();
                timer = 3;
            }
        }
        if(enemyNum == enemyKill && !win)
        {
            win = true;
            SpawnWin();
        }
    }

    public void SpawnEnemy()
    {
        GameObject go=Instantiate(enemyPrefab);
        go.transform.position = spawnPoint.position;
        enemySpawn--;
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
        winWindow.SetActive(true);
    }

    public void Next()
    {
        enemySpawn = enemyNum;
        enemyKill = 0;
        startTime = Time.time;
        win = false;
        timeBetweenPresses = 0;
        Debug.Log("next");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
