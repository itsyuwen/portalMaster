using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float maxHeight;
    Vector3 direction = Vector3.left;
    bool leavingWall = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid=GetComponent<Rigidbody2D>();
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
                transform.position = collision.transform.GetComponent<Door>().target.transform.position;
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
                GameManager.EnemyKilled();
            }
        }
    }

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
