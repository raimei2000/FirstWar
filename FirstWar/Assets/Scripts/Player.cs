using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Animator ani;
    public GameObject bulletPrefab;
    public float speed;
    Vector3 StartPos;
    Vector3 EndPos;
    public float bullet_speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        StartCoroutine(Bullet_Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            EndPos = Input.mousePosition;
            Vector3 Distance = EndPos - StartPos;
            //Debug.Log("StartPos : " + StartPos + ", EndPos : " + EndPos + ", dir : " + Distance.x);
            //sign of Distance.x (1 or -1)
            int value = (int)Mathf.Sign(Distance.x);

            if (Vector3.Distance(StartPos, EndPos) > 0.5f)
            {
                if (value == 1)
                {
                    //Debug.Log("Right Swipe");
                    //transform.Translate(Vector3.right * speed * Time.deltaTime);
                    StartPos = new Vector3(EndPos.x - 1.0f, StartPos.y, StartPos.z);
                    rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);

                    AnimatorChange("RUN");
                }
                else if (value == -1)
                {
                    //Debug.Log("Left Swipe");
                    //transform.Translate(Vector3.left * speed * Time.deltaTime);
                    StartPos = new Vector3(EndPos.x + 1.0f, StartPos.y, StartPos.z);
                    rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
                    AnimatorChange("RUN");
                }
                else
                {
                    Debug.Log("No Swipe");
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartPos = Vector3.zero;
            EndPos = Vector3.zero;
            rb.velocity = Vector3.zero;
            AnimatorChange("IDLE");
        }
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Bullet_Make();
        //    AnimatorChange("SHOOT");
        //}
    }

    private IEnumerator Bullet_Coroutine()
    {
        Bullet_Make();
        yield return new WaitForSeconds(bullet_speed);

        StartCoroutine(Bullet_Coroutine());
    }

    private void AnimatorChange(string temp)
    {

        if (temp == "SHOOT")
        {
            ani.SetTrigger("SHOOT");
            return;
        }

        ani.SetBool("RUN", false);
        ani.SetBool("IDLE", false);

        ani.SetBool(temp, true);
    }

    private void Bullet_Make()
    {
        AnimatorChange("SHOOT");

        GameObject go = Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1.0f), Quaternion.identity);
        Destroy(go, 3.0f);
    }
}
