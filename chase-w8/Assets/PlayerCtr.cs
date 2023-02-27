using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//玩家控制
public class PlayerCtr : MonoBehaviour
{
    private float speedValue=3f;
    float speed = 0;
    public  Rigidbody2D rig;
    public bool canInput = true;
    public bool isJump = false;
    public bool isStart = true;
    //private float firstDoorTime = 0;
    //private bool canTiao = true;
    // Start is called before the first frame update
    void Start()
    {
        rig = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInput) return;//不能输入的判断 不能输入就直接return 

        speed = speedValue * Input.GetAxisRaw("Horizontal");//左右移动

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);//旋转180 
            if (transform.GetComponentInChildren<Canvas>())
            {
                transform.GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject.SetActive(false);
                transform.GetComponentInChildren<Canvas>().transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            if (transform.GetComponentInChildren<Canvas>())
            {
                transform.GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject.SetActive(false);
                transform.GetComponentInChildren<Canvas>().transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        transform.Translate(new Vector2(speed, 0) * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space)&&!isJump)
        {
            rig.AddForce(Vector2.up * 350);
            isJump = true;
        }

        if (!Input.anyKey)//没有按键的时候
        {
            rig.velocity = new Vector2(0, rig.velocity.y);//停止移动
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
           
            if (GameManager.instance.doorIndex <2)
            {
                GameObject go = Instantiate(GameManager.instance.door);
                go.transform.position = transform.position;
                GameManager.instance.doors.Add(go);
                if (GameManager.instance.doorIndex == 1)
                {
                    GameManager.instance.doors[1].GetComponent<Door>().target = GameManager.instance.doors[0].gameObject;
                    GameManager.instance.doors[0].GetComponent<Door>().target = GameManager.instance.doors[1].gameObject;
                }
            }
            else
            {
                GameManager.instance.doors[GameManager.instance.doorIndex % 2].transform.position = transform.position;
            }
            GameManager.instance.doorIndex++;
           
            //if (GameManager.instance.tempDoor != null)
            //{
            //    go.transform.GetComponent<Door>().target = GameManager.instance.tempDoor;
            //}
            //else
            //{
            //    GameManager.instance.tempDoor = go;
            //}
            //if(doorIndex == 1)
            //{
            //    firstDoorTime = Time.time;
            //}
            //if (doorIndex == 2)
            //{
            //    GameManager.timeBetweenPresses = Time.time - firstDoorTime;
            //    GameManager.instance.tempDoor = null;
            //    // doorIndex = 0;
            //}
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if (collider2D.transform.tag == "Enemy")
        {
            GameManager.instance.times++;
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, GameManager.instance.times);
        
       
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "door")
        {
            if (collision.transform.GetComponent<Door>().target != null)
            {
              
                float offsetX = Input.GetAxisRaw("Horizontal") > 0 ? 1 : -1;
                Debug.Log(offsetX);
                transform.position = collision.transform.GetComponent<Door>().target.transform.position+new Vector3(1.5f* offsetX, 0,0);
            }
        }
    }
}
