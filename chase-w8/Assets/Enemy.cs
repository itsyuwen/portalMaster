using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float maxHeight;
    Vector3 direction = Vector3.left;
    bool leavingWall = false;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
        maxHeight=transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
        //Debug.Log(rigid.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "door")
        {
            if (collision.transform.GetComponent<Door>().target != null)
            {
                float offsetX = transform.eulerAngles.y == 180 ? 1 : -1;
                Debug.Log(offsetX);
                transform.position = collision.transform.GetComponent<Door>().target.transform.position + new Vector3(1.5f * offsetX, 0, 0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collider2D)
    {
        if (collider2D.transform.tag == "Wall" && !leavingWall)
        {
            direction.x = - direction.x;
            leavingWall = true;
        }

        if (collider2D.transform.tag == "Ground")
        {
            Debug.Log(maxHeight - transform.position.y);
            if (maxHeight - transform.position.y >= 4)
            {
                Destroy(this.gameObject);
                GameManager.instance.isKill = true;
                GameManager.EnemyKilled();
            }
            else
            {
                if (transform.name == "tipenemy")
                {
                    index++;
                    if (index == 2)
                    {
                        GameManager.instance.enemyTip.SetActive(true);
                        //HideTip();
                    }
                    
                }
            }
        }
    }

    //public void HideTip()
    //{
    //    Destroy(GameManager.instance.enemyTip, 1);
    //}

    void OnCollisionExit2D(Collision2D collider2D)
    {
        if (collider2D.transform.tag == "Ground")
        {
            maxHeight = transform.position.y;
        }
        if (collider2D.transform.tag == "Wall")
        {
            leavingWall = false;
        }
    }

}
