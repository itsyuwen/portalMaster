using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public static int enemyNum = 3;
    private int enemySpawn;
    private float timer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = enemyNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawn > 0)
        {
           GameManager.instance.win = false;
            GameManager.instance.winWindow.SetActive(false);
           timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnEnemy();
                timer = 3;
            }
        }
    }
    public void SpawnEnemy()
    {
        GameObject go = Instantiate(GameManager.instance.enemyPrefab);
        go.transform.position = transform.position;
        go.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        enemySpawn--;
    }
}
