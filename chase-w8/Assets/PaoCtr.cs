using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoCtr : MonoBehaviour
{
  public  LineRenderer line;

    // Update is called once per frame
    void Update()
    {
        float dirX=Input.GetAxisRaw("Horizontal");//左右
        if (dirX != 0 && !Study.instance.tip4.activeSelf)
        {
            Study.instance.tip2.SetActive(false);
            Study.instance.tip3.SetActive(true);
        }
        float z = transform.eulerAngles.z;
        z += Time.deltaTime * -dirX * 180;
        if (z >= 180)
        {
            z -= 360;
        }
        z = Mathf.Clamp(z, -80, 80);
        transform.eulerAngles = new Vector3(0, 0, z);
        RaycastHit2D hit2D;
        line.SetPosition(0, transform.position);
        if (Physics2D.Raycast(transform.position, transform.up, 100))
        {
            hit2D=Physics2D.Raycast(transform.position, transform.up, 100);
    
            //Debug.DrawLine(transform.position, hit2D.point,Color.red);
            line.SetPosition(1, hit2D.point);

            if (Input.GetKeyDown(KeyCode.S) &&hit2D.transform.tag == "Wall")
            {
                Study.instance.tip3.SetActive(false);
                Study.instance.tip4.SetActive(true);
                float x = ((Vector2)transform.position - hit2D.point).x;
                if (Study.instance.doorIndex < 2)
                {
                    GameObject go = Instantiate(Study.instance.door);
                    go.transform.position = hit2D.point+(new Vector2(x>0?0.5f:-0.5f,0));
                    Study.instance.doors.Add(go);
                    if (Study.instance.doorIndex == 1)
                    {
                        Study.instance.doors[1].GetComponent<Door>().target = Study.instance.doors[0].gameObject;
                        Study.instance.doors[0].GetComponent<Door>().target = Study.instance.doors[1].gameObject;
                    }
                }
                else
                {
                    Study.instance.doors[Study.instance.doorIndex % 2].transform.position = hit2D.point + (new Vector2(x > 0 ? 0.5f : -0.5f, 0));
                }
                Study.instance.doorIndex++;
            }
        }
     

    }
}
