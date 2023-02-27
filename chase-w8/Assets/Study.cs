using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Study : MonoBehaviour
{
    public static Study instance;
    public GameObject tip1,tip2,tip3,tip4;
    public GameObject jiantou;
    public GameObject door;
    public List<GameObject> doors = new List<GameObject>();
    public GameObject startBtn;

    public int doorIndex = 0;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
