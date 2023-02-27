using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//玩家控制
public class PlayerStudy : MonoBehaviour
{
    private float speedValue = 3f;
    float speed = 0;
    public Rigidbody2D rig;
    public bool canInput = true;
    public bool isJump = false;
    public bool isStart = true;

    public bool isPao = false;
    public Transform pao;

    public bool isIn = false;//已进入门
    private float timer = 0.5f;
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
        if (!isIn)
        {
            timer -= Time.deltaTime;
        }
        if (isPao)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!pao.GetComponent<PaoCtr>().enabled)
                {
                    Study.instance.tip1.SetActive(false);
                    Study.instance.tip2.SetActive(true);
                    pao.GetComponent<PaoCtr>().enabled = true;
                }
                else
                {
                    pao.GetComponent<PaoCtr>().enabled = false;
                    canInput = true;
                }

            }
        }


        if (!canInput) return;//不能输入的判断 不能输入就直接return 

        speed = speedValue * Input.GetAxisRaw("Horizontal");//左右移动

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            Study.instance.tip1.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 0, 0);//旋转180 
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            Study.instance.tip1.SetActive(false);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        transform.Translate(new Vector2(speed, 0) * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
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

            if (Study.instance.doorIndex < 2)
            {
                GameObject go = Instantiate(Study.instance.door);
                go.transform.position = transform.position;
                Study.instance.doors.Add(go);
                if (Study.instance.doorIndex == 1)
                {
                    Study.instance.doors[1].GetComponent<Door>().target = Study.instance.doors[0].gameObject;
                    Study.instance.doors[0].GetComponent<Door>().target = Study.instance.doors[1].gameObject;
                }
            }
            else
            {
                Study.instance.doors[Study.instance.doorIndex % 2].transform.position = transform.position;
            }
            Study.instance.doorIndex++;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if (collider2D.transform.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "door"&&!isIn&& timer<=0)
        {
            if (collision.transform.GetComponent<Door>().target != null)
            {

                float offsetX = transform.eulerAngles.y == 180 ? -1 : 1;
                //transform.position = collision.transform.GetComponent<Door>().target.transform.position + new Vector3(1.5f * offsetX, 0, 0);
                transform.position = collision.transform.GetComponent<Door>().target.transform.position;
                Study.instance.startBtn.SetActive(true);
                isIn = true;
                timer = 0.5f;
            }
        }
        if (collision.transform.tag == "tip2")
        {
            Study.instance.tip2.SetActive(true);
            Study.instance.jiantou.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "tip3")
        {
            Study.instance.tip2.SetActive(false);
            Study.instance.tip3.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "pao" && !isPao)
        {
            canInput = false;
            isPao = true;
            Study.instance.tip1.SetActive(true);
            Debug.Log(123);
           
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "door" && isIn)
        {
            isIn = false;
        }
    }
}
