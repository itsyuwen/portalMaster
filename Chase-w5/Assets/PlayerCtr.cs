using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//��ҿ���
public class PlayerCtr : MonoBehaviour
{
    private float speedValue=3f;
    float speed = 0;
    public  Rigidbody2D rig;
    public bool canInput = true;
    public bool isJump = false;
    public int doorIndex = 0;
    public bool isStart = true;
    private float firstDoorTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rig = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInput) return;//����������ж� ���������ֱ��return 

        speed = speedValue * Input.GetAxisRaw("Horizontal");//�����ƶ�

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);//��ת180 
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        transform.Translate(new Vector2(speed, 0) * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space)&&!isJump)
        {
            rig.AddForce(Vector2.up * 350);
            isJump = true;
        }

        if (!Input.anyKey)//û�а�����ʱ��
        {
            rig.velocity = new Vector2(0, rig.velocity.y);//ֹͣ�ƶ�
        }

        if (Input.GetKeyDown(KeyCode.S)&& doorIndex<2)
        {
            doorIndex++;
            GameObject go = Instantiate(GameManager.instance.door);
            go.transform.position=transform.position;
            if (GameManager.instance.tempDoor != null)
            {
                go.transform.GetComponent<Door>().target = GameManager.instance.tempDoor;
            }
            else
            {
                GameManager.instance.tempDoor = go;
            }
            if(doorIndex == 1)
            {
                firstDoorTime = Time.time;
            }
            if (doorIndex == 2)
            {
                GameManager.timeBetweenPresses = Time.time - firstDoorTime;
                GameManager.instance.tempDoor = null;
                // doorIndex = 0;
            }
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
 
}
