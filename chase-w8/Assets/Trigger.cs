using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//触发
public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//触发到地板  把跳跃状态变成false
    {
        if (collision.transform.tag == "Ground")
        {
            transform.parent.transform.GetComponent<PlayerCtr>().isJump = false;
        }
        else if (collision.transform.tag == "StudyGround")
        {
            transform.parent.transform.GetComponent<PlayerStudy>().isJump = false;
        }
    }

}
